import { AfterViewInit, ChangeDetectorRef, Component, EventEmitter, OnDestroy, Output, ViewChild } from '@angular/core';
import { EventoAulaAgendamentoHorarioModel } from '../../moldels/evento-aula-agendamento-horario.model';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { PoCheckboxGroupOption, PoSelectOption, PoTableColumn, PoTableComponent } from '@po-ui/ng-components';
import { SmcModalComponent } from 'projects/shared/components/smc-modal/smc-modal.component';
import { CalendarEvent } from 'angular-calendar';
import { EventoAulaService } from '../../services/evento-aula.service';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import * as moment from 'moment';
import { EventoAulaSimulacaoModel } from '../../moldels/evento-aula-simulacao.model';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { EventoAulaDivisaoTurmaModel } from './../../moldels/evento-aula-divisao-turma.model';
import { EventoAulaModel } from './../../moldels/evento-aula.model';
import { BehaviorSubject, Subscription } from 'rxjs';
import { EventoAulaValidacaoColisaoColaboradorModel } from '../../moldels/evento-aula-validacao-colisao-colaborador.model';
import { EventoAulaBiTreeLocalComponent } from '../evento-aula-bi-tree-local/evento-aula-bi-tree-local.component';
import { SmcButtonComponent } from './../../../../../../../shared/components/smc-button/smc-button.component';

@Component({
  selector: 'sga-evento-aula-agendamento-add-reduzido-min',
  templateUrl: './evento-aula-agendamento-add-reduzido.component.html',
  styles: [],
})
export class EventoAulaAgendamentoAddReduzidoComponent implements AfterViewInit, OnDestroy {
  private _subs: Subscription[] = [];
  private set subs(value: Subscription) {
    this._subs.push(value);
  }
  itensTabelaHorario: EventoAulaAgendamentoHorarioModel[] = [];
  formAgendamento: FormGroup;
  dataSourceDivisoes: SmcKeyValueModel[] = [];
  dataSourceTurno: SmcKeyValueModel[] = [];
  dataSourceDiasSemana: PoCheckboxGroupOption[] = [];
  dataSourceTipoOcorrencia: SmcKeyValueModel[] = [];
  dataSourceHorarios: SmcKeyValueModel[] = [];
  dataSourceTabelaHorario: PoSelectOption[] = [];
  dataSourceColaboradores: SmcKeyValueModel[] = [];
  formValido = false;
  dataInicioEvento: string;
  diaSemana: number;
  feriado: boolean;
  sabado: boolean;
  descricaoEventoAulaDiaFormatado: string;
  divisaoSelecionada: EventoAulaDivisaoTurmaModel;
  ultimaDivisaoSelecionada: string;
  exibirNotificacao = false;
  camposDisabled = true;
  horarioDisabled = true;
  turmaIntegradaSEF = true;
  alunosCoincidentes: string[] = [];
  protocolosCoincidentes: string[] = [];
  mensagemInformativa: string = null;
  classeMensagemInformativa: 'smc-sga-mensagem-local-informa' | 'smc-sga-mensagem-local-erro' =
    'smc-sga-mensagem-local-informa';
  validacaoBackend$ = new BehaviorSubject(false);
  mensagemAssert = '';
  @Output() novoEvento = new EventEmitter<CalendarEvent>();
  @ViewChild(SmcModalComponent) modal: SmcModalComponent;
  @ViewChild(EventoAulaBiTreeLocalComponent) treeView: EventoAulaBiTreeLocalComponent;
  @ViewChild('confirmar') modalAssertConfirmar: SmcModalComponent;
  @ViewChild('btNao') btNao: SmcButtonComponent;
  @ViewChild('modalMensagemInformativa') modalMensagemInformtiva: SmcModalComponent;

  constructor(
    private fb: FormBuilder,
    private service: EventoAulaService,
    private dataService: EventoAulaDataService,
    private changeDetection: ChangeDetectorRef,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private notificationService: SmcNotificationService
  ) {}

  ngOnDestroy(): void {
    this._subs.forEach(f => f.unsubscribe());
  }

  ngAfterViewInit(): void {
    if (!this.dataService.dadosTurma) {
      this.fecharModal();
      return;
    }
    this.subs = this.activatedRoute.params.subscribe(params => {
      this.dataInicioEvento = params['dataInicio'] as string;
      const dia = moment(this.dataInicioEvento).day();
      const data = moment(this.dataInicioEvento, 'YYYY-MM-DD').toDate();
      //Baseado no dia da semana retorna o valor do enum no SMC.Calendario
      //Elevar a 2 para que a cobinação do valor dia equivala ao enum
      this.diaSemana = 2 ** dia;
      this.descricaoEventoAulaDiaFormatado = `${moment(this.dataInicioEvento, 'YYYY-MM-DD').format(
        'DD/MM/YYYY'
      )} - ${moment(this.dataInicioEvento).locale('pt-BR').format('dddd')}`;
      this.sabado = dia === 6;
      this.feriado = this.dataService.dataSourceFeriados.some(f => data >= f.dataInicio && data <= f.dataFim);
      this.abrirModal();
      this.changeDetection.detectChanges();
    });
  }

  abrirModal() {
    this.turmaIntegradaSEF = this.dataService.turmaIntegradaSEF;
    this.modal.open();
    this.formAgendamento = this.inicializarModelo();
    this.preenchimentoDataSources();
    this.subs = this.formAgendamento.valueChanges.subscribe(result => {
      this.formValido = this.formAgendamento.valid;
      const professoresSelecionados: string[] = result.colaboradores
        .filter(f => !isNullOrEmpty(f.seqColaborador))
        .map(m => m.seqColaborador);
      this.dataSourceColaboradores = this.dataService.dataSourceColaboradores.filter(
        f => !professoresSelecionados.includes(f.key)
      );
      this.habilitarHorarios();
      this.mensagemInformativa = null;
      if (this.formValido) {
        const eventos = this.gerarSimulacao();
        this.validarColisaoHorarios(eventos) && this.validarColisoesAunosEProfessores(eventos);
      }
      this.mensagemInformativa || this.validarPeriodoVigente();
      this.mensagemAssert = `Confirma a inclusão do agendamento do dia ${this.descricaoEventoAulaDiaFormatado}, conforme os parâmetros informados?`;
      this.mensagemInformativa || this.validarCargaHorariaTotal();
    });
    this.subs = this.formAgendamento.controls.seqDivisaoTurma.valueChanges.subscribe((seqDivisaoTurma: string) => {
      this.dependecyDivisaoTurma(seqDivisaoTurma);
    });
    this.validarPeriodoVigente();
  }

  validarPeriodoVigente() {
    const dataEvento = moment(this.dataInicioEvento, 'YYYY-MM-DD').toDate();
    const dataInicioPeriodo = moment(
      this.dataService.dadosTurma.eventoAulaTurmaCabecalho.inicioPeriodoLetivo,
      'DD/MM/YYYY'
    ).toDate();
    const dataFimPeriodo = moment(
      this.dataService.dadosTurma.eventoAulaTurmaCabecalho.fimPeriodoLetivo,
      'DD/MM/YYYY'
    ).toDate();
    if (dataEvento < dataInicioPeriodo || dataEvento > dataFimPeriodo) {
      this.camposDisabled = true;
      this.mensagemInformativa = 'A data informada deverá estar dentro do periodo letivo da turma.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
    }
  }

  inicializarModelo() {
    const modelo = this.fb.group({
      seqDivisaoTurma: ['', Validators.required],
      diasSemana: [[this.diaSemana]],
      turno: ['', Validators.required],
      comeca: [this.dataInicioEvento],
      termina: [this.dataInicioEvento],
      local: [''],
      codigoLocalSEF: [''],
      horarios: ['', Validators.required],
      colaboradores: this.fb.array([this.inicialProfessor()]),
    });
    return modelo;
  }

  inicialProfessor() {
    return this.fb.group({
      seqColaborador: [''],
    });
  }

  addProfessor() {
    const control = this.formAgendamento.get('colaboradores') as FormArray;
    control.push(this.inicialProfessor());
  }

  getProfessores() {
    return (this.formAgendamento.controls.colaboradores as FormArray).controls;
  }

  removeProfessor(indexProfessor) {
    const control = this.formAgendamento.get('colaboradores') as FormArray;
    control.removeAt(indexProfessor);
  }

  salvar() {
    this.modalAssertConfirmar.open();
    this.btNao.focus();
  }

  async onSubmit() {
    const eventos = this.gerarSimulacao();

    this.notificationService.information(`Iniciando gravação do evento ${eventos[0].diaSemanaFormatada}`);
    this.service.registrarEventosProcessando(eventos);
    this.fecharModal();
    try {
      await this.service.salvarEventos(eventos);
      await this.service.atualizarEventos();
      this.notificationService.success(`Gravação do evento ${eventos[0].diaSemanaFormatada} realizada com sucesso`);
    } catch {
      this.service.cancelarEventosProcessando(eventos);
    }
  }

  dependecyDivisaoTurma(seqDivisaoTurma: string) {
    this.divisaoSelecionada = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq === seqDivisaoTurma);

    if (this.divisaoSelecionada?.seq !== this.ultimaDivisaoSelecionada) {
      this.exibirNotificacao = false;
    } else {
      this.exibirNotificacao = true;
    }

    if (this.divisaoSelecionada) {
      this.ultimaDivisaoSelecionada = this.divisaoSelecionada.seq;
      this.divisaoSelecionada.dscTabelaHorario = this.dataService.tabelaHorarioAgd.descricao;
    }

    //NV 29 - Se o dia selecionado for um 'Sábado' e a configuração da divisão não permitir aulas aos sábados
    //enviar a mensagem de erro ' Não é permitido o agendamento de aula aos sábados para esta divisão de turma'
    if (
      this.divisaoSelecionada &&
      !this.divisaoSelecionada?.aulaSabado &&
      this.diaSemana === 64 &&
      this.exibirNotificacao
    ) {
      this.notificationService.error('Não é permitido o agendamento de aula aos sábados para esta divisão de turma.');
      this.camposDisabled = true;
      return;
    }

    //Se porventura tiver divisao habilita todos os campos...
    if (this.divisaoSelecionada) {
      this.camposDisabled = false;
    } else {
      this.camposDisabled = true;
      this.resetCampos();
    }
    //this.camposDisabled = this.divisaoSelecionada ? false : true;

    //Se por ventura tiver aula aos sabados habilita o sabado
    if (this.divisaoSelecionada?.aulaSabado) {
      this.dataSourceDiasSemana.forEach(f => {
        if (f.label === 'Sábado') {
          f.disabled = false;
        }
      });
      this.dataSourceDiasSemana = [...this.dataSourceDiasSemana];
    }
  }

  preenchimentoDataSources() {
    //NV01 - A  divisão do componente associada a divisão da turma possuir  carga horária de grade maior que zero e
    this.dataService.dadosTurma.eventoAulaDivisoesTurma.forEach(divisao => {
      const cargaHorariaDisponivel = +divisao?.cargaHorariaLancada >= divisao?.cargaHorariaGrade;
      const validacaoAulaSabado = !this.sabado || divisao.aulaSabado;
      const validacaoFeriado = !this.feriado || divisao.tipoPulaFeriado === 'Não Pula';
      if (cargaHorariaDisponivel && validacaoAulaSabado && validacaoFeriado) {
        let option: SmcKeyValueModel = { key: divisao.seq, value: divisao.descricaoDivisaoFormatada };
        this.dataSourceDivisoes.push(option);
      }
    });
    this.dataSourceTipoOcorrencia = this.dataService.dataSourceTipoOcorrencia;
    this.dataSourceDiasSemana = this.dataService.dataSourceDiasSemana;
    this.dataSourceTurno = this.dataService.dataSourceTurno;
    this.dataSourceColaboradores = this.dataService.dataSourceColaboradores;
  }

  filtrarTabelaHorario() {
    const seqturno = this.formAgendamento.get('turno').value;
    const seqsdiaSemana = this.formAgendamento.get('diasSemana').value as [];

    //Se somente tenha escolhido so dois
    if (!isNullOrEmpty(seqturno) && seqsdiaSemana?.length > 0) {
      const itensTemporarios: PoSelectOption[] = [];
      this.dataService.tabelaHorarioAgd.horarios.forEach(f => {
        if (seqsdiaSemana.find(d => d === f.seqDiaSemana) && f.seqTurno === seqturno) {
          const itemFiltrado = this.dataSourceTabelaHorario.find(i => f.seq === i.value);
          // Preserva itens selecionados
          if (itemFiltrado) {
            itensTemporarios.push(itemFiltrado);
          } else {
            itensTemporarios.push({ value: f.seq, label: `${f.diaSemana} ${f.horaInicio} - ${f.horaFim}` });
          }
        }
      });
      this.dataSourceTabelaHorario = itensTemporarios;
    } else {
      this.dataSourceTabelaHorario = [];
    }
    const options = { onlySelf: true, emitEvent: false };
    this.formAgendamento.get('horarios').setValue('', options);
  }

  fecharModal() {
    this.router.navigate([{ outlets: { modais: null } }], { queryParamsHandling: 'preserve' });
    this.formAgendamento = undefined;
    this.formValido = false;
    this.itensTabelaHorario = [];
    this.modal.close();
  }

  horariosSelecionados(poTable: PoTableComponent) {
    const array = [];
    poTable.getSelectedRows().forEach(f => array.push(f.seq));
    this.formAgendamento.get('recorrencia.horarios').setValue(array);
  }

  addAgendamento() {
    this.router.navigate([{ outlets: { modais: ['Add'] } }], { queryParamsHandling: 'preserve' });
  }

  localSelecionado(local: SmcKeyValueModel) {
    this.formAgendamento.get('local').setValue(local.value);
    this.formAgendamento.get('codigoLocalSEF').setValue(local.key);
  }

  exibirModalMensagemInformativa() {
    if (this.alunosCoincidentes.length > 0 || this.protocolosCoincidentes.length > 0) {
      this.modalMensagemInformtiva.open();
    }
  }

  private habilitarHorarios() {
    this.horarioDisabled = true;
    const turno = this.formAgendamento.value.turno;
    if (!isNullOrEmpty(turno)) {
      this.horarioDisabled = false;
    }
  }

  private gerarSimulacao() {
    const parametros: EventoAulaSimulacaoModel = {
      seqDivisaoTurma: this.formAgendamento.value.seqDivisaoTurma,
      seqsHorarios: this.formAgendamento.value.horarios,
      descricaoDivisao: this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
        f => f.seq === this.formAgendamento.value.seqDivisaoTurma
      ).descricaoDivisaoFormatada,
      idtTipoRecorrencia: 2,
      comeca: this.formAgendamento.value.comeca,
      termina: this.formAgendamento.value.termina,
      repetir: '2',
      local: this.formAgendamento.value.local,
      codigoLocalSEF: this.formAgendamento.value.codigoLocalSEF,
      colaboradores: this.formAgendamento.value.colaboradores.filter(f => f.seqColaborador),
    };
    const eventos = this.service
      .gerarSimulacaoEventos(parametros)
      .map(m => ({ ...m, turno: this.formAgendamento.value.turno }));
    eventos[0].codigoRecorrencia = null;
    return eventos;
  }

  private async validarColisaoAlunos(eventosSimulados: EventoAulaModel[]) {
    this.alunosCoincidentes = await this.service.validarColisaoHorarioAluno(eventosSimulados);
  }

  private async validarColisaoHorarioSolicitacaoServico(eventosSimulados: EventoAulaModel[]) {
    this.protocolosCoincidentes = await this.service.validarColisaoHorarioSolicitacaoServico(eventosSimulados);
  }

  private async validarColisaoProfessores(eventosSimulados: EventoAulaModel[]) {
    const model: EventoAulaValidacaoColisaoColaboradorModel[] = [];
    eventosSimulados.forEach(evento => {
      evento.colaboradores?.forEach(colaborador => {
        model.push({
          seqColaborador: colaborador.seqColaborador,
          seqDivisaoTurma: this.divisaoSelecionada.seq,
          codigoLocalSEF: evento.codigoLocalSEF,
          dataAula: evento.data,
          horaInicio: evento.horaInicio,
          horaFim: evento.horaFim,
        });
      });
    });
    if (model.length) {
      const validacao = await this.service.validarColisao(model);
      if (!isNullOrEmpty(validacao)) {
        this.mensagemInformativa = validacao;
        this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      }
    }
  }

  private async validarColisoesAunosEProfessores(eventosSimulados: EventoAulaModel[]) {
    this.formValido = false;
    this.alunosCoincidentes = [];
    this.protocolosCoincidentes = [];
    if (!eventosSimulados?.length) {
      return;
    }
    this.validacaoBackend$.next(true);
    await Promise.all([
      this.validarColisaoAlunos(eventosSimulados),
      this.validarColisaoHorarioSolicitacaoServico(eventosSimulados),
    ]);

    if (this.alunosCoincidentes.length > 0 || this.protocolosCoincidentes.length > 0) {
      this.mensagemInformativa = `Inserção de horário não realizada, devido a coincidência(s)
                                    de horário(s) ocorrida(s). <a id="saibaMais">Saiba mais</a>`;
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
    }

    if (this.alunosCoincidentes.length === 0 && this.protocolosCoincidentes.length === 0) {
      await this.validarColisaoProfessores(eventosSimulados);
    }

    if (!isNullOrEmpty(this.mensagemInformativa) && this.classeMensagemInformativa === 'smc-sga-mensagem-local-erro') {
      this.formValido = false;
    } else {
      this.formValido = this.formAgendamento.valid;
    }

    this.validacaoBackend$.next(false);
  }

  private validarCargaHorariaTotal() {
    this.formValido = false;
    const data = this.formAgendamento.controls.comeca.value;
    const horariosSelecionados = Array.from(this.formAgendamento.controls.horarios.value ?? []);
    let qtdHorarios = 0;
    horariosSelecionados?.forEach(horario => {
      if (
        !this.divisaoSelecionada.eventoAulas.find(
          s => s.seqHorarioAgd === horario && moment(s.data).format('YYYY-MM-DD') === data
        )
      ) {
        qtdHorarios++;
      }
    });
    if (qtdHorarios === 0) {
      return;
    }

    const totalCargaLancadaSimulada = this.divisaoSelecionada?.cargaHorariaLancada + qtdHorarios;
    const totalCargaHorariaGrade = this.divisaoSelecionada?.cargaHorariaGrade;
    const tipoDistribuicaoAula = this.divisaoSelecionada.tipoDistribuicaoAula;

    if (totalCargaLancadaSimulada < totalCargaHorariaGrade) {
      this.mensagemAssert =
        'A carga horária a ser lançada está menor que a carga horária cadastrada para a divisão do componente. Deseja continuar?';
      this.changeDetection.detectChanges();
    }

    if (totalCargaLancadaSimulada > totalCargaHorariaGrade && tipoDistribuicaoAula === 'Concentrada') {
      this.mensagemAssert =
        'A carga horária total lançada está ultrapassando a carga horária da divisão do componente. Deseja continuar?';
      this.changeDetection.detectChanges();
    }

    if (totalCargaLancadaSimulada > totalCargaHorariaGrade && tipoDistribuicaoAula === 'Livre') {
      this.mensagemAssert =
        'A carga horária total lançada está ultrapassando a carga horária da divisão do componente. Deseja continuar?';
      this.changeDetection.detectChanges();
    }
  }

  private resetCampos() {
    this.formAgendamento.controls.turno.setValue('');
    this.formAgendamento.controls.horarios.setValue('');
    this.formAgendamento.controls.local.setValue('');
    this.formAgendamento.controls.codigoLocalSEF.setValue('');
    this.treeView?.clearSelection();
    const colaboradores = this.formAgendamento.controls.colaboradores as FormArray;
    colaboradores.clear();
    colaboradores.push(this.inicialProfessor());
  }

  private validarColisaoHorarios(novosEventos: EventoAulaModel[]): boolean {
    if (!novosEventos || novosEventos.length === 0) {
      return true;
    }
    const colisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma
      .filter(divisao => divisao.numero != this.divisaoSelecionada.numero)
      .some(divisao =>
        divisao.eventoAulas.some(eventoDivisao =>
          novosEventos.some(
            novoEvento =>
              moment(novoEvento.data).format('YYYYMMDD') === moment(eventoDivisao.data).format('YYYYMMDD') &&
              novoEvento.seqHorarioAgd === eventoDivisao.seqHorarioAgd
          )
        )
      );
    if (colisao) {
      this.mensagemInformativa = 'Já existe horário criado para o dia e hora informado.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
    }
    return !colisao;
  }
}
