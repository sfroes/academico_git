import { Component, OnInit, Output, EventEmitter, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';
import { EventoAulaListarComponent } from '../evento-aula-listar/evento-aula-listar.component';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { Subscription } from 'rxjs';

@Component({
  selector: 'sga-evento-aula-filtro',
  templateUrl: './evento-aula-filtro.component.html',
  standalone: false,
})
export class EventoAulaFiltroComponent implements OnInit, OnDestroy {
  formFiltro: FormGroup;
  readonlyLookup: boolean = false;
  desabilitarBotao = true;
  get somenteLeitura() {
    return this.dataService.dadosTurma?.eventoAulaTurmaCabecalho.somenteLeitura ?? false;
  }
  get cargaHorariaCompleta() {
    return this.dataService.dadosTurma?.eventoAulaTurmaCabecalho.cargaHorariaCompleta ?? false;
  }
  get mensagemFalha() {
    return this.dataService.dadosTurma?.eventoAulaTurmaCabecalho.mensagemFalha;
  }
  get dataSourceCarregando$() {
    return this.service.loadingDataSource;
  }
  get tokenSegurancaIncluir() {
    return this.dataService.tokensSeguranca.find(f => f.nome === 'incluirEventoAula')?.permitido !== true;
  }
  @Output() novoAgendamento = new EventEmitter();
  @Output() pesquisarTurmaSelecionada = new EventEmitter<string>();
  private _seqTurma: string;
  private _subscription: Subscription[] = [];
  private set subscription(sub: Subscription) {
    this._subscription.push(sub);
  }

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private service: EventoAulaService,
    private dataService: EventoAulaDataService
  ) {}

  ngOnInit(): void {
    this.formFiltro = this.inicializarModelo();

    // Inicializa o filtro pois o init roda após a primeira execução da rota.
    this._seqTurma = this.activatedRoute.firstChild.snapshot.params.seqTurma as string;
    this.preencherFiltro(this._seqTurma);

    // Escuta qualquer modificação de rota
    this.subscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const seqTurmaRoute = this.activatedRoute.firstChild.snapshot.params.seqTurma as string;
        this.preencherFiltro(seqTurmaRoute);
        this._seqTurma = seqTurmaRoute;
      }
    });

    this.subscription = this.service.loadingDataSource.subscribe(result => {
      if (!result) {
        return;
      }
      if (isNullOrEmpty(this.formFiltro.controls.seqCicloLetivoInicio.value)) {
        this.formFiltro.controls.seqCicloLetivoInicio.setValue(
          this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqCicloLetivoInicio
        );
      }
    });
    this.configurarDepencyCicloLetivoInicio();
  }

  ngOnDestroy(): void {
    this._subscription.forEach(f => f.unsubscribe());
  }

  onSubmit() {
    this.pesquisarTurmaSelecionada.emit(this.formFiltro.get('seqTurma').value);
    this.readonlyLookup = true;
    this.desabilitarBotao = false;
  }

  limparForm() {
    this.formFiltro.reset();
    this.readonlyLookup = false;
    this.desabilitarBotao = true;
    this.dataService.dadosTurma.eventoAulaTurmaCabecalho.mensagemFalha = '';
    this.pesquisarTurmaSelecionada.emit(this.formFiltro.get('seqTurma').value);
  }

  private inicializarModelo() {
    return this.fb.group({
      seqCicloLetivoInicio: [''],
      seqTurma: ['', Validators.required],
    });
  }

  private preencherFiltro(seqTurma: string) {
    const readOnly = this.activatedRoute.firstChild.component === EventoAulaListarComponent;
    this.formFiltro.get('seqTurma').setValue(seqTurma);
    this.readonlyLookup = readOnly;
    this.desabilitarBotao = !this.readonlyLookup;
  }

  private configurarDepencyCicloLetivoInicio() {
    this.subscription = this.formFiltro.controls.seqCicloLetivoInicio.valueChanges.subscribe(
      _ => !this.readonlyLookup && this.formFiltro.controls.seqTurma.reset()
    );
  }
}
