import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PoMultiselectOption } from '@po-ui/ng-components';
import moment from 'moment';
import { PrimeNgMultiselectOptionsModel } from 'projects/shared/models/primeng-multiselect-options.model';
import { isNullOrEmpty, runOnNexEventLoop } from 'projects/shared/utils/util';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApuracaoFrequenciaAlunoModel } from '../models/apuracao-frequencia-aluno.model';
import { ApuracaoFrequenciaAulaModel } from '../models/apuracao-frequencia-aula.model';
import { ApuracaoFrequenciaDiaModel } from '../models/apuracao-frequencia-dia.model';
import { SituacaoApuracaoFrequencia } from '../models/apuracao-frequencia-types.model';
import { ApuracaoFrequenciaModel } from '../models/apuracao-frequencia.model';
import { ApuracaoFrequenciaDataService } from '../services/apuracao-frequencia-data.service';

@Component({
  selector: 'sga-apuracao-frequencia-filtro',
  templateUrl: './apuracao-frequencia-filtro.component.html',
  standalone: false,
})
export class ApuracaoFrequenciaFiltroComponent implements OnInit, OnDestroy {
  @Input() maximizar: boolean;
  @Output() maximizarClick = new EventEmitter<boolean>();
  formAulas: FormGroup;
  formAluno: FormGroup;
  datasDataSource: PrimeNgMultiselectOptionsModel[] = [];
  get naoApurada(): SituacaoApuracaoFrequencia {
    return 'Não apurada';
  }
  get dataSourceSituacaoApuracaoFrequencia(): PrimeNgMultiselectOptionsModel[] {
    return this.dataService.dataSourceSituacaoApuracaoFrequencia;
  }
  private _subs: Subscription[] = [];
  private set sub(value: Subscription) {
    this._subs.push(value);
  }
  get alunos(): ApuracaoFrequenciaAlunoModel[] {
    return this.dataService.modelSnapshot.alunos;
  }
  get dias(): ApuracaoFrequenciaDiaModel[] {
    return this.dataService.modelSnapshot.dias;
  }
  get aulas(): ApuracaoFrequenciaAulaModel[] {
    return this.dataService.modelSnapshot.aulas;
  }

  constructor(
    private dataService: ApuracaoFrequenciaDataService,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.criarForms();
    this.configurarDepency();
  }

  ngOnDestroy(): void {
    this._subs.forEach(f => f.unsubscribe());
  }

  clickMaximizar() {
    this.maximizar = !this.maximizar;
    this.maximizarClick.emit(this.maximizar);
  }

  classeSituacaoData(data: string, seqEventoAula: string) {
    const aula = this.dataService.model$.value.dias
      .find(f => f.descricaoFormatada === data)
      .aulas.find(f => f.seqEventoAula === seqEventoAula);

    switch (aula.situacaoApuracaoFrequencia) {
      case 'Em apuração':
        return 'smc-legend-blue3';
      case 'Não apurada':
        return 'smc-legend-yellow2';
      case 'Executada':
        return 'smc-legend-green2';
      default:
        return 'smc-legend-red2';
    }
  }

  classeSituacao(situacao: SituacaoApuracaoFrequencia) {
    switch (situacao) {
      case 'Executada':
        return 'smc-legend-green2';
      case 'Não apurada':
        return 'smc-legend-yellow2';
      case 'Não executada':
        return 'smc-legend-red2';
    }
  }

  recuperarEventosData(data: string): ApuracaoFrequenciaAulaModel[] {
    const dataFormatada = moment(data, 'DD/MM/YYYY').format('YYYYMMDD');
    const eventos = this.dataService.model$.value.aulas.filter(f => f.dataFormatada === dataFormatada);
    return eventos;
  }

  private criarForms(): void {
    this.formAulas = this.fb.group({
      situacoes: [[]],
      datas: [[]],
    });

    this.formAluno = this.fb.group({
      ra: [],
      nome: [],
    });
  }

  private configurarDepency(): void {
    this.preencherFiltroPadraoDia();
    this.dependencyFiltroDatas();
    this.dependencyFiltroAlunos();
    this.dependencyData();
    this.dependencySituacao();
  }

  private dependencyFiltroDatas() {
    this.sub = this.formAulas.valueChanges.subscribe(v => {
      this.filtrarDatas(v);
    });
    this.sub = this.dataService.diaNotificacao$.subscribe(data => {
      const datasSelecionadas = this.formAulas.controls.datas.value;
      if (!datasSelecionadas.some(s => s.code === data.code)) {
        this.formAulas.controls.datas.setValue([...datasSelecionadas, ...[data]]);
      }
    });
  }

  private propagarMostrarAula(aula: ApuracaoFrequenciaAulaModel, dia: ApuracaoFrequenciaDiaModel) {
    this.alunos.forEach(aluno => {
      aluno.apuracoes.find(f => f.seqEventoAula === aula.seqEventoAula).mostrar = aula.mostrar;
    });
    dia.mostrar = dia.mostrar || aula.mostrar;
  }

  private dependencyFiltroAlunos() {
    this.sub = this.formAluno.valueChanges.subscribe(v => {
      this.filtrarAlunos(v);
    });
  }

  private preencherFiltroPadraoDia() {
    const subFiltro = this.dataService.model$.pipe(map(m => m.dias)).subscribe(diasSub => {
      if (diasSub.length > 0) {
        runOnNexEventLoop(() => {
          this.datasDataSource = this.mapearDatasourceDias(diasSub);
          runOnNexEventLoop(() => this.selecionarDiasPadroes());
          subFiltro.unsubscribe();
        });
      }
    });
  }

  private selecionarDiasPadroes() {
    const dataAtual = moment(this.dataService.dataHoraServidor).startOf('day').format('YYYYMMDD');
    const datasDataSource = this.dataService.modelSnapshot.dias
      .filter(f => moment(f.data).format('YYYYMMDD') === dataAtual)
      .map(m => ({ code: m.descricaoFormatada, name: m.descricaoFormatada }));
    this.formAulas.controls.datas.pristine && this.formAulas.controls.datas.setValue(datasDataSource);
  }

  private dependencyData() {
    this.sub = this.dataService.model$.subscribe(_ => {
      this.filtrarAlunos(this.formAluno.value);
      this.filtrarDatas(this.formAulas.value);
    });
  }

  private filtrarDatas(filtro: {
    situacoes: PrimeNgMultiselectOptionsModel[];
    datas: PrimeNgMultiselectOptionsModel[];
  }) {
    this.dias
      .sort((a, b) => new Date(a.data).getTime() - new Date(b.data).getTime())
      .forEach(dia => {
        dia.mostrar = false;
        dia.aulas.forEach(aula => {
          aula.mostrar = filtro.datas.some(s => s.code === dia.descricaoFormatada);
          this.propagarMostrarAula(aula, dia);
        });
      });
  }

  private filtrarAlunos(filtro: { ra: string; nome: string }) {
    this.alunos.forEach(aluno => {
      aluno.mostrar =
        (isNullOrEmpty(filtro.ra) || aluno.numeroRegistroAcademico.includes(filtro.ra)) &&
        (isNullOrEmpty(filtro.nome) || aluno.nome.includes(filtro.nome.toUpperCase()));
    });
  }

  private dependencySituacao() {
    this.sub = this.formAulas.controls.situacoes.valueChanges.subscribe(value => {
      const situacoes = value as PrimeNgMultiselectOptionsModel[];
      this.formAulas.controls.datas.setValue([]);
      const diasSituacao =
        situacoes.length > 0
          ? this.dataService.model$.value.dias.filter(f =>
              f.aulas.some(sa => situacoes.some(s => s.code === sa.situacaoApuracaoFrequencia))
            )
          : this.dataService.model$.value.dias;
      this.datasDataSource = this.mapearDatasourceDias(diasSituacao);
    });
  }

  private mapearDatasourceDias(dias: ApuracaoFrequenciaDiaModel[]): PrimeNgMultiselectOptionsModel[] {
    return dias
      .sort((a, b) => new Date(b.data).getTime() - new Date(a.data).getTime())
      .map(ma => ({ code: ma.descricaoFormatada, name: ma.descricaoFormatada }));
  }
}
