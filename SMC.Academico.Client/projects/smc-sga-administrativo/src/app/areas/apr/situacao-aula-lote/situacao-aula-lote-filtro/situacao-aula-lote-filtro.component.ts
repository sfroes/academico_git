import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { Subscription } from 'rxjs';
import { SituacaoAulaLoteDataService } from './../services/situacao-aula-lote-data.service';
import { isNullOrEmpty } from './../../../../../../../shared/utils/util';
import { PoSelectOption } from '@po-ui/ng-components';
import { SituacaoAulaLoteService } from './../services/situacao-aula-lote.service';
import { SituacaoAulaLoteFiltroModel } from '../models/situacao-aula-lote-filtro.model';
import { conditionalValidator } from 'projects/shared/validators/smc-validator.directive';

@Component({
  selector: 'sga-situacao-aula-lote-filtro',
  templateUrl: './situacao-aula-lote-filtro.component.html',
  standalone: false,
})
export class SituacaoAulaLoteFiltroComponent implements OnInit, OnDestroy {
  formFiltro: FormGroup;
  dataSourceSituacaoAula: SmcKeyValueModel[] = [];
  dataSourceDivisoesTurma: PoSelectOption[] = [];
  cicloLetivoObrigatorio = true;
  datasObrigatorias = false;
  divisaoTurmaDisabled = true;
  professorObrigatorio = true;
  turmaObrigatoria = true;
  carrengando = false;
  dataMaxima: string;
  private _subscription: Subscription[] = [];
  private set subscription(sub: Subscription) {
    this._subscription.push(sub);
  }

  constructor(
    private fb: FormBuilder,
    private dataService: SituacaoAulaLoteDataService,
    private services: SituacaoAulaLoteService
  ) {}

  ngOnInit(): void {
    this.formFiltro = this.inicializarFormFiltro();
    this.dataSourceSituacaoAula = this.dataService.dataSourceSituacaoAula;
    this.dataMaxima = new Date().toISOString().slice(0, 10);
    this.configurarDepency();
    this.subscription = this.dataService.$carregando.subscribe(value => {
      this.carrengando = value;
    });
    this.subscription = this.dataService.$filtra.subscribe(_ => {
      this.onSubmit();
    });
    this.buscarTokensSeguranca();
  }

  ngOnDestroy(): void {
    this._subscription.forEach(f => f.unsubscribe());
  }

  onSubmit() {
    const modelo: SituacaoAulaLoteFiltroModel = {
      seqsColaborador: this.formFiltro.value.seqColaborador,
      seqCicloLetivo: this.formFiltro.value.seqCicloLetivo,
      seqsDivisaoTurma: this.formFiltro.value.seqsDivisao,
      seqTurma: this.formFiltro.value.seqTurma,
      fimPeriodo: this.formFiltro.value.dataFim,
      inicioPeriodo: this.formFiltro.value.dataInicio,
      situacaoApuracaoFrequencia: this.formFiltro.value.situacaoAula,
    };

    if (this.formFiltro.value.situacaoAula == 22) {
      modelo.situacaoApuracaoFrequencia = 2;
      modelo.dentroPerido = true;
    }

    this.dataService.situacaoEscolhida = this.dataService.dataSourceSituacaoAula.find(
      f => f.key === this.formFiltro.value.situacaoAula
    ).value;

    //validação para não passa valor vazio para o modelo pois o modelo do back end de evento aula é um array
    //desta foram ele faz o biding em um array com o valor zero e fará o filtro de forma errada.
    if (isNullOrEmpty(modelo.seqsColaborador)) {
      modelo.seqsColaborador = null;
    }

    this.buscarEventos(modelo);
  }

  limparForm() {
    this.formFiltro.reset();
    this.dataService.listaAulas = [];
    this.dataService.$atualizarListaAulas.next();
  }

  private inicializarFormFiltro() {
    return this.fb.group({
      seqCicloLetivo: [null, Validators.required],
      seqTurma: [null, Validators.required],
      dataInicio: [
        null,
        conditionalValidator(() => !isNullOrEmpty(this.formFiltro?.value.dataFim), Validators.required),
      ],
      dataFim: [
        null,
        conditionalValidator(() => !isNullOrEmpty(this.formFiltro?.value.dataInicio), Validators.required),
      ],
      seqsDivisao: [null],
      seqColaborador: [null, Validators.required],
      situacaoAula: [null, Validators.required],
    });
  }

  private configurarDepency() {
    this.subscription = this.formFiltro.controls.seqCicloLetivo.valueChanges.subscribe(seqCicloLetivo => {
      if (seqCicloLetivo) {
        this.formFiltro.controls.seqTurma.reset();
      }
      if (!isNullOrEmpty(this.formFiltro.value.seqTurma) && !isNullOrEmpty(this.formFiltro.value.seqColaborador)) {
        this.cicloLetivoObrigatorio = false;
      }
      this.validarObrigatorios();
    });

    this.subscription = this.formFiltro.controls.seqColaborador.valueChanges.subscribe(seqColaborador => {
      const seqTurma = this.formFiltro.controls.seqTurma.value;
      this.turmaObrigatoria = true;
      this.professorObrigatorio = true;
      this.cicloLetivoObrigatorio = true;
      if (seqColaborador && isNullOrEmpty(seqTurma)) {
        this.turmaObrigatoria = false;
      } else if (isNullOrEmpty(seqColaborador) && !isNullOrEmpty(seqTurma)) {
        this.professorObrigatorio = false;
        this.cicloLetivoObrigatorio = false;
      } else if (!isNullOrEmpty(seqColaborador) && !isNullOrEmpty(seqTurma)) {
        this.cicloLetivoObrigatorio = false;
      }
      this.validarObrigatorios();
    });

    this.subscription = this.formFiltro.controls.seqTurma.valueChanges.subscribe(seqTurma => {
      const seqColaborador = this.formFiltro.controls.seqColaborador.value;
      this.preencherDataSourceDivisoesTurma(seqTurma);
      this.professorObrigatorio = true;
      this.turmaObrigatoria = true;
      if (seqTurma && isNullOrEmpty(seqColaborador)) {
        this.cicloLetivoObrigatorio = false;
        this.professorObrigatorio = false;
        this.divisaoTurmaDisabled = false;
      } else if (isNullOrEmpty(seqTurma) && !isNullOrEmpty(seqColaborador)) {
        this.turmaObrigatoria = false;
        this.divisaoTurmaDisabled = true;
        this.cicloLetivoObrigatorio = true;
        this.formFiltro.controls.seqsDivisao.reset();
      } else if (isNullOrEmpty(seqTurma)) {
        this.cicloLetivoObrigatorio = true;
        this.divisaoTurmaDisabled = true;
        this.formFiltro.controls.seqsDivisao.reset();
      }
      this.validarObrigatorios();
    });

    this.subscription = this.formFiltro.controls.dataFim.valueChanges.subscribe(dataFim => {
      const dataInicio = this.formFiltro.controls.dataInicio.value;
      this.datasObrigatorias = false;
      if (!isNullOrEmpty(dataInicio) || !isNullOrEmpty(dataFim)) {
        this.datasObrigatorias = true;
      }
    });

    this.subscription = this.formFiltro.controls.dataInicio.valueChanges.subscribe(dataInicio => {
      const dataFim = this.formFiltro.controls.dataFim.value;
      this.datasObrigatorias = false;
      if (!isNullOrEmpty(dataInicio) || !isNullOrEmpty(dataFim)) {
        this.datasObrigatorias = true;
      }
    });
  }

  private async buscarTokensSeguranca() {
    this.dataService.tokensSeguranca = await this.services.buscarTokensSeguranca();
  }

  private async preencherDataSourceDivisoesTurma(seqTurma: string) {
    this.dataService.$carregando.next(true);
    this.dataSourceDivisoesTurma = seqTurma ? await this.services.buscarDetalheDivisaoTurma(seqTurma) : [];
    this.dataService.$carregando.next(false);
  }

  private async buscarEventos(modelo: SituacaoAulaLoteFiltroModel) {
    this.dataService.$carregando.next(true);
    this.dataService.listaAulas = await this.services.buscarAulas(modelo);
    this.dataService.$atualizarListaAulas.next();
    this.dataService.$carregando.next(false);
  }

  private validarObrigatorios() {
    const controlTurma = this.formFiltro.controls.seqTurma;
    const controlColaborador = this.formFiltro.controls.seqColaborador;
    const controlCicloLetivo = this.formFiltro.controls.seqCicloLetivo;

    if (!isNullOrEmpty(controlTurma.value)) {
      controlColaborador.setValidators(null);
      controlCicloLetivo.setValidators(null);
      controlColaborador.setErrors(null);
      controlCicloLetivo.setErrors(null);
      this.formFiltro.updateValueAndValidity();
    }

    if (!isNullOrEmpty(controlColaborador.value) && isNullOrEmpty(controlCicloLetivo.value)) {
      controlTurma.setValidators(null);
      controlTurma.setErrors(null);
      controlCicloLetivo.setValidators(Validators.required);
      controlCicloLetivo.setErrors({ required: true });
      this.formFiltro.updateValueAndValidity();
    }

    if (!isNullOrEmpty(controlColaborador.value) && !isNullOrEmpty(controlCicloLetivo.value)) {
      controlTurma.setValidators(null);
      controlTurma.setErrors(null);
      this.formFiltro.updateValueAndValidity();
    }

    if (isNullOrEmpty(controlTurma.value) && isNullOrEmpty(controlColaborador.value)) {
      controlColaborador.setValidators(Validators.required);
      controlTurma.setValidators(Validators.required);
      controlColaborador.setErrors({ required: true });
      controlTurma.setErrors({ required: true });
      this.formFiltro.updateValueAndValidity();
    }

    if (!isNullOrEmpty(controlColaborador.value) && !isNullOrEmpty(controlTurma.value)) {
      controlCicloLetivo.setValidators(null);
      controlCicloLetivo.setErrors(null);
      this.formFiltro.updateValueAndValidity();
    }
  }
}
