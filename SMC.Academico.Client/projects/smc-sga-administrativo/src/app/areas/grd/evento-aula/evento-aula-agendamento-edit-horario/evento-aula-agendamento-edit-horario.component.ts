import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  Renderer2,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PoCheckboxGroupOption } from '@po-ui/ng-components';
import moment from 'moment';
import { SmcModalComponent } from 'projects/shared/components/smc-modal/smc-modal.component';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { distinctArray, isNullOrEmpty } from 'projects/shared/utils/util';
import { BehaviorSubject, Subscription } from 'rxjs';
import { EventoAulaAgendamentoEditModel } from '../../moldels/evento-aula-agendamento-edit.model';
import { EventoAulaAgendamentoProfessorModel } from '../../moldels/evento-aula-agendamento-professor.model';
import { EventoAulaDivisaoTurmaModel } from '../../moldels/evento-aula-divisao-turma.model';
import { EventoAulaSimulacaoModel } from '../../moldels/evento-aula-simulacao.model';
import { EventoAulaValidacaoColisaoColaboradorModel } from '../../moldels/evento-aula-validacao-colisao-colaborador.model';
import { EventoAulaModel } from '../../moldels/evento-aula.model';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';
import { diaUtil } from '../evento-aula.directive';
import { conditionalValidator } from '../../../../../../../shared/validators/smc-validator.directive';
import { SmcButtonComponent } from 'projects/shared/components/smc-button/smc-button.component';

@Component({
  selector: 'sga-evento-aula-agendamento-edit-horario',
  templateUrl: './evento-aula-agendamento-edit-horario.component.html',
  standalone: false,
})
export class EventoAulaAgendamentoEditHorarioComponent implements AfterViewInit, OnDestroy {
  formEdit: FormGroup;
  divisao: EventoAulaDivisaoTurmaModel;
  eventoAula: EventoAulaModel;
  dataSourceDivisoes: SmcKeyValueModel[] = [];
  dataSourceTurno: SmcKeyValueModel[] = [];
  dataSourceDiasSemana: PoCheckboxGroupOption[] = [];
  dataSourceHorarios: SmcKeyValueModel[] = [];
  dataSourceColaboradores: SmcKeyValueModel[] = [];
  modeloCrud: EventoAulaAgendamentoEditModel;
  esconderDataPeriodo: boolean = true;
  dataObrigatoria: boolean = false;
  turmaIntegradaSEF: boolean = true;
  inicioPeriodoLetivo: string;
  fimPeriodoLetivo: string ;
  tipoRecorrenciaDisabled = true;
  camposDisabled = true;
  dataSourceTipoOcorrencia: SmcKeyValueModel[] = [];
  tipoRecorrenciaMensal = false;
  repetirDisabled = true;
  edicaoMultipla = false;
  dataSourceTipoInicioOcorrencia: SmcKeyValueModel[] = [];
  paginaValidada = false;
  colaboradoresDiferentes = false;
  locaisDiferentes = false;
  alunosCoincidentes: string[] = [];
  protocolosCoincidentes: string[] = [];
  mensagemErroPattern = '';

  mensagemConfirmacao: string = null;
  mensagemInformativa: string = null;
  classeMensagemInformativa:
    | 'smc-sga-mensagem-local-informa'
    | 'smc-sga-mensagem-local-alerta'
    | 'smc-sga-mensagem-local-sucesso'
    | 'smc-sga-mensagem-local-erro' = 'smc-sga-mensagem-local-informa';
  mensagemInicialNotificacao: string = '';
  mensagemSucessoNotificacao: string = '';
  mensagemAssert: string = '';
  validacaoBackend$ = new BehaviorSubject(false);
  get formValido() {
    return (
      this.formEdit &&
      this.formEdit.valid &&
      this.formEdit.dirty &&
      (isNullOrEmpty(this.mensagemInformativa) || this.classeMensagemInformativa !== 'smc-sga-mensagem-local-erro')
    );
  }
  private _eventoAulaSimulacao: EventoAulaModel[] = [];
  private _options = { onlySelf: true, emitEvent: false };
  private _subs: Subscription[] = [];
  private set subs(sub: Subscription) {
    this._subs.push(sub);
  }

  @ViewChild(SmcModalComponent) modal: SmcModalComponent;
  @ViewChild('confirmar') modalAssert: SmcModalComponent;
  @ViewChild('btNao') btNao: SmcButtonComponent;
  @ViewChild('modalMensagemInformativa') modalMensagemInformtiva: SmcModalComponent;

  constructor(
    private fb: FormBuilder,
    private service: EventoAulaService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private changeDetection: ChangeDetectorRef,
    private dataService: EventoAulaDataService,
    private notificationService: SmcNotificationService,
    private renderer: Renderer2,
    private elementRef: ElementRef
  ) {}

  onChangeTipoRecorrencia(tipoRecorrencia: number) {
    this.tipoRecorrenciaMensal = tipoRecorrencia !== 2;
  }

  ngAfterViewInit(): void {
    if (!this.dataService.dadosTurma) {
      this.fecharModal();
      return;
    }
    this.subs = this.activatedRoute.params.subscribe(params => {
      this.abrirModal(params['seqDivisaoTurma'] as string, params['seqEventoAula'] as string);
      this.changeDetection.detectChanges();
    });
  }

  ngOnDestroy(): void {
    this._subs.forEach(f => f.unsubscribe());
  }

  abrirModal(seqDivisaoTurma: string, seqEventoAula: string) {
    this.prepararModelo(seqDivisaoTurma, seqEventoAula);
    this.formEdit = this.inicializarModelo();
    this.preenchimentoDataSources();
    this.dependencyDivisaoTurma();
    this.filtrarTabelaHorario(false, false);
    this.subs = this.formEdit.controls.colaboradores.valueChanges.subscribe(novosColaboradores =>
      this.validarColaboradorDuplicado(novosColaboradores)
    );
    this.subs = this.formEdit.valueChanges.subscribe(_ => {
      this.validarMensagensInformativas();
      if (this.formEdit.valid && this.paginaValidada) {
        this.gerarSimulacao();
        this.validarColisoesAunosEProfessores(this.filtrarEventosSimuladosNovos());
        // this.validarColisaoAlunos(this.filtrarEventosSimuladosNovos());
        // this.validarColisaoProfessores(this.filtrarEventosSimuladosNovos());
        const eventosSimulados = this.filtrarEventosSimuladosNovos();
        this.validarCargaHorariaTotal(eventosSimulados.length);
      } else if (!this.paginaValidada) {
        this.formEdit.markAsPristine();
      }
      this.paginaValidada = true;
      this.changeDetection.detectChanges();
    });
    this.subs = this.formEdit.controls.turno.valueChanges.subscribe(_ => {
      this.filtrarTabelaHorario(true, false);
    });
    this.subs = this.formEdit.controls.diasSemana.valueChanges.subscribe(_ => {
      this.filtrarTabelaHorario(false, true);
    });
    this.subs = this.formEdit.statusChanges.subscribe(_ => {
      this.changeDetection.detectChanges();
    });
    this.subs = this.formEdit.controls.tipoEdicao.valueChanges.subscribe(tipoEdicao => {
      this.changeTipoEdicao(tipoEdicao);
    });
    this.modal.open();
  }

  prepararModelo(seqDivisaoTurma: string, seqEventoAula: string) {
    this.divisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq == seqDivisaoTurma);
    this.divisao.dscTabelaHorario = this.dataService.tabelaHorarioAgd.descricao;
    this.eventoAula = this.divisao.eventoAulas.find(f => f.seq == seqEventoAula);
  }

  inicializarModelo() {
    const modelo = this.fb.group({
      seq: [this.eventoAula.seq],
      seqDivisaoTurma: [this.eventoAula.seqDivisaoTurma],
      seqEventoAulaAgd: [this.eventoAula.seqEventoAgd],
      novaData: [moment(this.eventoAula.data).format('YYYY-MM-DD'), diaUtil(this.service, this.divisao)],
      idtTipoInicioRecorrencia: [
        '',
        conditionalValidator(() => this.formEdit?.controls.idtTipoRecorrencia.value == 4, Validators.required), //mensal
      ],
      idtTipoRecorrencia: [
        '',
        conditionalValidator(
          () => this.formEdit && this.formEdit.controls.tipoEdicao.value !== '1',
          Validators.required
        ),
      ],
      data: [this.eventoAula.data],
      diasSemana: [this.eventoAula.diaSemana],
      horarios: [this.eventoAula.seqHorarioAgd],
      horaInicio: [this.eventoAula.horaInicio],
      horaFim: [this.eventoAula.horaFim],
      turno: [this.eventoAula.turno],
      local: [this.eventoAula.local],
      codigoLocalSEF: [this.eventoAula.codigoLocalSEF],
      codigoRecorrencia: [this.eventoAula.codigoRecorrencia],
      colaboradores: this.inicialProfessor(this.eventoAula.colaboradores),
      repetir: [
        '',
        conditionalValidator(
          () => this.formEdit && this.formEdit.controls.tipoEdicao.value != '1',
          Validators.required
        ),
      ],
      comeca: [''],
      termina: [''],
      tipoEdicao: ['1', Validators.required],
      dataInicio: [''],
      dataFim: [''],
    });
    return modelo;
  }

  inicialProfessor(model: EventoAulaAgendamentoProfessorModel[]) {
    const colaboradores = this.fb.array([]);
    model.forEach(prof => {
      const groupProf = this.fb.group({
        seqColaborador: [prof.seqColaborador],
        seqColaboradorBanco: [prof.seqColaborador],
        seqColaboradorSubstituto: [prof.seqColaboradorSubstituto],
      });
      colaboradores.push(groupProf as any);
    });
    return colaboradores;
  }

  dependencyDivisaoTurma() {
    //Se por ventura tiver aula aos sabados habilita o sabado
    if (this.divisao.aulaSabado) {
      this.dataSourceDiasSemana.forEach(f => {
        if (f.label === 'Sábado') {
          f.disabled = false;
        }
      });
      this.dataSourceDiasSemana = [...this.dataSourceDiasSemana];
    }
    this.inicioPeriodoLetivo = moment(this.divisao?.inicioPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD');
    this.fimPeriodoLetivo = moment(this.divisao?.fimPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD');

    //Caso o tipo de tipoDistribuicaoAula - SMC.Calendarios.Common.Areas.CLD.Enums
    //this.datasComecoTerminoDisabled = this.dataService.dadosTurma.permiteAlterarDataAgendamentoAula ? false : true;
    if (this.divisao.tipoDistribuicaoAula === 'Semanal' || this.divisao.tipoDistribuicaoAula === 'Quinzenal') {
      //NV's tipo de recorrencia
      this.formEdit.get('idtTipoRecorrencia').setValue(2, this._options); //Semanal
      this.tipoRecorrenciaDisabled = true;
      this.tipoRecorrenciaMensal = false;
    } else {
      this.formEdit.get('idtTipoRecorrencia').setValue('', this._options);
      this.tipoRecorrenciaDisabled = false;
      this.tipoRecorrenciaMensal = true;
    }
    //NV** Se a distribuicao de aula configurada for quinzenal , este campo deverá vir preenchido com o valor 2 e o usuário não poderá alterá-lo
    if (this.divisao.tipoDistribuicaoAula === 'Quinzenal') {
      this.formEdit.get('repetir').setValue(2, this._options);
      this.repetirDisabled = true;
    } else {
      this.formEdit.get('repetir').setValue('', this._options);
      this.repetirDisabled = false;
    }
  }

  async onSubmit() {
    let seqEventosExcluir: string[] = [];
    // Seleção eventos
    switch (this.formEdit.value.tipoEdicao) {
      case '1':
        seqEventosExcluir.push(this.eventoAula.seq);
        break;
      case '2':
        seqEventosExcluir = this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
          .map(m => m.seq);
        break;
      case '3':
        const dataInicio = moment(this.formEdit.value.dataInicio, 'YYYY-MM-DD');
        const dataFim = moment(this.formEdit.value.dataFim, 'YYYY-MM-DD');
        seqEventosExcluir = this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
          .filter(f => {
            const data = moment(f.data, 'YYYY-MM-DD');
            return data >= dataInicio && data <= dataFim;
          })
          .map(m => m.seq);
        break;
    }

    this.gerarSimulacao();
    const eventos = this._eventoAulaSimulacao;
    if (eventos.length === 0) {
      this.notificationService.warning('Conforme regras não existe eventos a serem alterados!');
      return;
    }

    this.divisao.eventoAulas = this.divisao.eventoAulas.filter(f => !seqEventosExcluir.includes(f.seq));

    this.notificationService.information(this.mensagemInicialNotificacao);
    // Move eventos para processando
    this.service.registrarEventosProcessando(eventos);
    this.service.refresh.next();
    // Gavação alteração
    this.eventoAula = null;
    this.fecharModal();
    try {
      await this.service.editarEventos(eventos, seqEventosExcluir);
      await this.service.atualizarEventos();
      this.notificationService.success(this.mensagemSucessoNotificacao);
    } catch {
      this.service.cancelarEventosProcessando(eventos);
    }
  }

  dependencyProfessor(professor: AbstractControl) {
    if (!professor.value.seqColaborador) {
      professor.get('seqColaboradorSubstituto').setValue('', this._options);
    }
  }

  async preenchimentoDataSources() {
    this.dataSourceDiasSemana = this.dataService.dataSourceDiasSemana;
    this.dataSourceTurno = this.dataService.dataSourceTurno;
    this.dataSourceColaboradores = this.dataService.dataSourceColaboradores;
    this.dataSourceTipoOcorrencia = this.dataService.dataSourceTipoOcorrencia;
    this.dataSourceTipoInicioOcorrencia = this.dataService.dataSourceTipoInicioOcorrencia;
  }

  filtrarTabelaHorario(trocaTurno: boolean, trocaDia: boolean) {
    let seqTurno = this.formEdit.get('turno').value;
    let seqDiaSemana = this.formEdit.get('diasSemana').value;
    this.dataSourceHorarios = [];

    //Se somente tenha escolhido so dois
    if (!isNullOrEmpty(seqTurno) && !isNullOrEmpty(seqDiaSemana)) {
      this.dataService?.tabelaHorarioAgd.horarios.forEach(f => {
        if (f.seqDiaSemana === seqDiaSemana && f.seqTurno === seqTurno) {
          this.dataSourceHorarios.push({ key: f.seq, value: `${f.diaSemana} ${f.horaInicio} - ${f.horaFim}` });
        }
      });
    }

    if (trocaTurno) {
      this.formEdit.controls.horarios.setValue('', this._options);
    } else if (trocaDia) {
      const horaInicio = this.formEdit.controls.horaInicio.value as string;
      const horaFim = this.formEdit.controls.horaFim.value as string;
      const horarioAgd = this.dataService.tabelaHorarioAgd.horarios.find(
        f => f.seqDiaSemana === seqDiaSemana && f.horaInicio === horaInicio && f.horaFim === horaFim
      )?.seq;
      this.formEdit.controls.horarios.setValue(horarioAgd, this._options);
    }
  }

  fecharModal() {
    this.modal.close();
    this.router.navigate(
      [
        {
          outlets: {
            modais: this.eventoAula ? ['Detalhes', this.eventoAula.seq, this.eventoAula.seqDivisaoTurma] : null,
          },
        },
      ],
      { queryParamsHandling: 'preserve' }
    );
  }

  changeTipoEdicao(tipoEdicao) {
    if (tipoEdicao < '3') {
      this.esconderDataPeriodo = true;
      this.formEdit.get('dataInicio').setValue('');
      this.formEdit.get('dataFim').setValue('');
      this.dataObrigatoria = false;
    } else {
      this.esconderDataPeriodo = false;
      this.dataObrigatoria = true;
      if (this.divisao.tipoDistribuicaoAula !== 'Livre') {
        this.formEdit.get('dataFim').setValue(moment(this.divisao.fimPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD'));
      }
    }

    this.edicaoMultipla = tipoEdicao !== '1';
    this.validarMensagensInformativas();
  }

  localSelecionado(local: SmcKeyValueModel) {
    this.formEdit.get('local').setValue(local.value);
    this.formEdit.get('codigoLocalSEF').setValue(local.key);
  }

  calcularDiaSemana() {
    const novaData = moment(this.formEdit.value.novaData, 'YYYY-MM-DD');
    const diaSemana = novaData.weekday();
    const diaSemanaAgd = Math.pow(2, diaSemana);
    const campoDiaSemana = this.formEdit.get('diasSemana');
    const diaSemanaCorrente = campoDiaSemana.value;
    campoDiaSemana.setValue(diaSemanaAgd, this._options);
    if (diaSemanaCorrente != diaSemanaAgd) {
      this.formEdit.get('horarios').setValue('', this._options);
      this.filtrarTabelaHorario(false, true);
      this.validarMensagensInformativas();
    }
    this.validarMensagemErroPattern(novaData.toDate());
  }

  validarMensagemErroPattern(data: Date) {
    this.mensagemErroPattern = '';
    if (data < moment(this.inicioPeriodoLetivo).toDate() || data > moment(this.fimPeriodoLetivo).toDate()) {
      this.mensagemErroPattern = 'A data informada deverá estar dentro do periodo letivo da turma.';
    } else if (!this.service.validarDiaUtil(data, this.divisao)) {
      this.mensagemErroPattern = 'Não é dia letivo.';
    }
  }

  validarColaboradorDuplicado(novosColaboradores: any) {
    const professoresSelecionados: string[] = [
      ...novosColaboradores.filter(f => !isNullOrEmpty(f.seqColaborador)).map(m => m.seqColaborador),
      ...novosColaboradores
        .filter(f => !isNullOrEmpty(f.seqColaboradorSubstituto))
        .map(m => m.seqColaboradorSubstituto),
    ];
    const duplicados = professoresSelecionados.filter(f => professoresSelecionados.filter(ff => f === ff).length > 1);
    (this.formEdit.controls.colaboradores as FormArray).controls.forEach(f => {
      const professorEventoAula = f as FormGroup;
      if (duplicados.includes(professorEventoAula.controls.seqColaborador.value)) {
        professorEventoAula.controls.seqColaborador.setErrors({ duplicado: true });
      } else {
        professorEventoAula.controls.seqColaborador.setErrors(null);
      }
      if (duplicados.includes(professorEventoAula.controls.seqColaboradorSubstituto.value)) {
        professorEventoAula.controls.seqColaboradorSubstituto.setErrors({ duplicado: true });
      } else {
        professorEventoAula.controls.seqColaboradorSubstituto.setErrors(null);
      }
    });
    this.changeDetection.detectChanges();
  }

  localFormatado() {
    return this.service.valorPadrao(this.eventoAula.local, 'Local não informado');
  }

  formatarMensagens() {
    let quantidadeEventos = 0;
    const descricaoEvento = `${this.eventoAula.diaSemanaDescricao.toLowerCase()} de ${this.eventoAula.horaInicio} - ${
      this.eventoAula.horaFim
    }`;
    switch (this.formEdit.value.tipoEdicao) {
      case '1':
        quantidadeEventos = 1;
        this.mensagemInicialNotificacao = `Iniciando alteração de horário do evento do dia ${this.eventoAula.diaSemanaFormatada.toLowerCase()} de ${
          this.eventoAula.horaInicio
        } - ${this.eventoAula.horaFim}.`;
        this.mensagemSucessoNotificacao = `Alteração de horário do evento do dia ${this.eventoAula.diaSemanaFormatada.toLowerCase()} de ${
          this.eventoAula.horaInicio
        } - ${this.eventoAula.horaFim}`;
        this.mensagemAssert = `O horário do agendamento do dia ${this.eventoAula.diaSemanaFormatada.toLowerCase()} de ${
          this.eventoAula.horaInicio
        } - ${this.eventoAula.horaFim} será alterado.`
        break;
      case '2':
        quantidadeEventos = this.divisao.eventoAulas.filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
          .length;
        this.mensagemInicialNotificacao = `Iniciando a alteração de horário dos agendamentos recorrentes de ${descricaoEvento}.`;
        this.mensagemSucessoNotificacao = `Alteração de horário dos agendamentos recorrentes de ${descricaoEvento}`;
        this.mensagemAssert = `Ao salvar esta alteração, ${quantidadeEventos} agendamento(s), de ${descricaoEvento}, terão o horário alterado.`;
        break;
      case '3':
        const dataInicio = moment(this.formEdit.value.dataInicio, 'YYYY-MM-DD');
        const dataFim = moment(this.formEdit.value.dataFim, 'YYYY-MM-DD');
        quantidadeEventos = this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
          .filter(f => {
            const data = moment(f.data, 'YYYY-MM-DD');
            return data >= dataInicio && data <= dataFim;
          }).length;
        this.mensagemInicialNotificacao = `Iniciando a alteração de horário dos agendamento(s) de
                                           ${descricaoEvento}, no período de
                                           ${dataInicio.format('DD/MM/YYYY')} a ${dataFim.format('DD/MM/YYYY')}.`;
        this.mensagemSucessoNotificacao = `Alteração de horário do(s) agendamento(s) de ${descricaoEvento}, no período de
                                          ${dataInicio.format('DD/MM/YYYY')} a ${dataFim.format('DD/MM/YYYY')}`;
        this.mensagemAssert = `Ao salvar esta alteração, ${quantidadeEventos} agendamento(s) de ${descricaoEvento}, de ${dataInicio.format(
          'DD/MM/YYYY'
        )} a ${dataFim.format('DD/MM/YYYY')}, terão o horário alterado.`;
        break;
    }

    const eventosSimulados = this.filtrarEventosSimuladosNovos();
    this.validarCargaHorariaTotal(eventosSimulados.length);
    this.mensagemSucessoNotificacao += ', realizada com sucesso.';
    this.modalAssert.open();
    this.btNao.focus();
  }

  private validarMensagensInformativas() {
    let falha = false;
    this.mensagemInformativa = null;
    this.classeMensagemInformativa = 'smc-sga-mensagem-local-informa';
    if (!this.paginaValidada || !this.formEdit.valid) {
      return;
    }
    this.locaisDiferentes = this.validarLocaisDiferentes();
    this.colaboradoresDiferentes = this.validarColaboradoresDiferentes();
    falha = this.validarEdicaoEventosSelecionados();
    if (!falha) {
      falha = this.validarColisaoHorarios();
    }
    if (!falha && this.colaboradoresDiferentes && this.locaisDiferentes) {
      this.mensagemConfirmacao = this.mensagemInformativa =
        'Existem colaboradores e/ou locais distintos associados aos eventos no período informado. Os mesmos serão desvinculados, sendo necessário uma nova alocação após a alteração do horário.';
      falha = true;
    }
    if (!falha && !this.colaboradoresDiferentes && !this.locaisDiferentes) {
      this.mensagemConfirmacao = this.mensagemInformativa =
        'Os professores e locais serão mantidos no novo horário caso não seja encontrada nenhuma inconsistência.';
    }
    this.changeDetection.detectChanges();
  }

  private validarColisaoHorarios(): boolean {
    const colisao = this.filtrarEventosColisao().length > 0;
    if (colisao) {
      this.mensagemInformativa = 'Já existe horário criado para o dia e hora informado.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
    }
    return colisao;
  }

private validarCargaHorariaTotal(totalCargaSimulada: number) {
    if (totalCargaSimulada === 0) {
      return;
    }

    let seqEventosExcluir: string[] = [];
    let qtdeEventosForaRecorrencia = 0;
    let totalCargaLancadaSimulada = 0;

    switch (this.formEdit.value.tipoEdicao) {
      case '1':
        //totalCargaSimulada = 0; //Considera 0 porque na alteração de somente um registro não adiciona novos horários, então continua com a carga horária lançada
        //totalCargaLancadaSimulada = this.divisao?.cargaHorariaLancada;

        seqEventosExcluir.push(this.eventoAula.seq);
        qtdeEventosForaRecorrencia = this.divisao.eventoAulas.filter(f => !seqEventosExcluir.includes(f.seq)).length;
        totalCargaLancadaSimulada = qtdeEventosForaRecorrencia + totalCargaSimulada;
        break;
      case '2':
        seqEventosExcluir = this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
          .map(m => m.seq);

        qtdeEventosForaRecorrencia = this.divisao.eventoAulas.filter(f => !seqEventosExcluir.includes(f.seq)).length;
        totalCargaLancadaSimulada = qtdeEventosForaRecorrencia + totalCargaSimulada;
        break;
      case '3':
        const dataInicio = moment(this.formEdit.value.dataInicio, 'YYYY-MM-DD');
        const dataFim = moment(this.formEdit.value.dataFim, 'YYYY-MM-DD');
        seqEventosExcluir = this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
          .filter(f => {
            const data = moment(f.data, 'YYYY-MM-DD');
            return data >= dataInicio && data <= dataFim;
          })
          .map(m => m.seq);

        qtdeEventosForaRecorrencia = this.divisao.eventoAulas.filter(f => !seqEventosExcluir.includes(f.seq)).length;
        totalCargaLancadaSimulada = qtdeEventosForaRecorrencia + totalCargaSimulada;
        break;
    }

    const totalCargaHorariaGrade = this.divisao?.cargaHorariaGrade;
    const tipoDistribuicaoAula = this.divisao?.tipoDistribuicaoAula;

    if (totalCargaLancadaSimulada < totalCargaHorariaGrade) {
      this.mensagemAssert = 'A carga horária a ser lançada está menor que a carga horária cadastrada para a divisão do componente. Deseja continuar?';
      this.changeDetection.detectChanges();
    }

    if (totalCargaLancadaSimulada > totalCargaHorariaGrade && tipoDistribuicaoAula === 'Concentrada') {
      this.mensagemInformativa = `Para a divisão de turma configurada como 'concentrada' a carga horária total lançada
                                  deverá ser exatamente igual a carga horária da divisão do componente`;
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
    }

    if (totalCargaLancadaSimulada > totalCargaHorariaGrade && tipoDistribuicaoAula === 'Livre') {
      this.mensagemAssert = 'A carga horária total lançada está ultrapassando a carga horária da divisão do componente. Deseja continuar?';
      this.changeDetection.detectChanges();
    }
  }

  private async validarColisaoProfessores(eventosSimulados: EventoAulaModel[]) {
    const model: EventoAulaValidacaoColisaoColaboradorModel[] = [];
    eventosSimulados.forEach(evento => {
      evento.colaboradores?.forEach(colaborador => {
        model.push({
          seqColaborador: colaborador.seqColaborador,
          seqDivisaoTurma: this.divisao.seq,
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

  private validarColaboradoresDiferentes(): boolean {
    const colaboradoresSelecionados = distinctArray(
      this.recuperarEventosPeriodo().map(m => this.recupearColaboradoresSelecionados(m))
    );
    if (colaboradoresSelecionados.length > 1) {
      this.mensagemConfirmacao = this.mensagemInformativa =
        'Existem colaboradores distintos associados aos eventos no período informado. Os mesmos serão desvinculados, sendo necessário uma nova alocação após a alteração do horário.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-alerta';
      return true;
    }
    return false;
  }

  private validarLocaisDiferentes(): boolean {
    const locaisSelecionados = distinctArray(this.recuperarEventosPeriodo().map(m => m.codigoLocalSEF));
    if (locaisSelecionados.length > 1) {
      this.mensagemConfirmacao = this.mensagemInformativa =
        'Existem locais distintos associados aos eventos no período informado. Os mesmos serão desvinculados, sendo necessário uma nova alocação após a alteração do horário.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-alerta';
      return true;
    }
    return false;
  }

  private recupearColaboradoresSelecionados(evento: EventoAulaModel): string {
    const colaboradores: string[] = [];
    evento.colaboradores?.forEach(f =>
      colaboradores.push(!isNullOrEmpty(f.seqColaboradorSubstituto) ? f.seqColaboradorSubstituto : f.seqColaborador)
    );
    return colaboradores.sort().join('|');
  }

  private recuperarEventosPeriodo() {
    const periodo = this.recuperarPeriodo();
    return this.divisao.eventoAulas.filter(
      f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd && periodo.dataInicio <= f.data && f.data <= periodo.dataFim
    );
  }

  private gerarSimulacao(): void {
    this.periodoComecoTerminoSimulacao();
    const parametros: EventoAulaSimulacaoModel = {
      seqDivisaoTurma: this.formEdit.controls.seqDivisaoTurma.value,
      seqsHorarios: [this.formEdit.controls.horarios.value],
      descricaoDivisao: this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
        f => f.seq === this.formEdit.controls.seqDivisaoTurma.value
      ).descricaoDivisaoFormatada,
      idtTipoRecorrencia: isNullOrEmpty(this.formEdit.controls.idtTipoRecorrencia.value)
        ? 2 //semana
        : this.formEdit.controls.idtTipoRecorrencia.value,
      idtTipoInicioRecorrencia: isNullOrEmpty(this.formEdit.controls.idtTipoInicioRecorrencia.value)
        ? 1
        : this.formEdit.controls.idtTipoInicioRecorrencia.value,
      comeca: this.formEdit.controls.comeca.value,
      termina: this.formEdit.controls.termina.value,
      repetir: isNullOrEmpty(this.formEdit.controls.repetir.value) ? 1 : this.formEdit.controls.repetir.value,
      local: this.locaisDiferentes ? '' : this.formEdit.controls.local.value,
      codigoLocalSEF: this.locaisDiferentes ? '' : this.formEdit.controls.codigoLocalSEF.value,
      colaboradores: this.colaboradoresDiferentes
        ? []
        : this.formEdit.controls.colaboradores.value.filter(f => f.seqColaborador),
    };

    try {
      const eventos = this.service
        .gerarSimulacaoEventos(parametros)
        .map(m => ({ ...m, turno: this.formEdit.value.turno }));
      this._eventoAulaSimulacao = eventos;
    } catch {
      this._eventoAulaSimulacao = [];
    }
  }

  private periodoComecoTerminoSimulacao() {
    switch (this.formEdit.value.tipoEdicao) {
      case '1':
        this.formEdit.controls.comeca.setValue(this.formEdit.value.novaData, this._options);
        this.formEdit.controls.termina.setValue(this.formEdit.value.novaData, this._options);
        break;
      case '2':
        const primeiroEvento = this.divisao.eventoAulas.filter(
          f => f.seqHorarioAgd === this.eventoAula.seqHorarioAgd
        )[0];
        const dataInicioPeriodo = moment(primeiroEvento.data);
        dataInicioPeriodo.add(-dataInicioPeriodo.weekday(), 'd');
        this.formEdit.controls.comeca.setValue(
          //moment(this.divisao.inicioPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD'),
          dataInicioPeriodo.format('YYYY-MM-DD'),
          this._options
        );
        const ultimoEvento = this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd === this.eventoAula.seqHorarioAgd)
          .slice(-1)[0];
        let datafimPeriodo = moment(ultimoEvento.data);
        datafimPeriodo.add(6 - datafimPeriodo.weekday(), 'd');
        if (this.divisao.tipoDistribuicaoAula === 'Quinzenal') {
          datafimPeriodo.add(7, 'd');
          const dataFimDivisao = moment(this.divisao.fimPeriodoLetivo, 'DD/MM/YYYY');
          if (datafimPeriodo > dataFimDivisao) {
            datafimPeriodo = dataFimDivisao;
          }
        }
        this.formEdit.controls.termina.setValue(
          //moment(this.divisao.fimPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD'),
          datafimPeriodo.format('YYYY-MM-DD'),
          this._options
        );
        break;
      case '3':
        this.formEdit.controls.comeca.setValue(this.formEdit.value.dataInicio, this._options);
        this.formEdit.controls.termina.setValue(this.formEdit.value.dataFim, this._options);
        break;
    }
  }

  private filtrarEventosSimuladosNovos() {
    return this._eventoAulaSimulacao.filter(
      eventoSimulado =>
        !this.divisao.eventoAulas.some(
          eventoExistente =>
            eventoExistente.diaSemanaFormatada === eventoSimulado.diaSemanaFormatada &&
            eventoExistente.seqHorarioAgd == eventoSimulado.seqHorarioAgd
        )
    );
  }

  private recuperarPeriodo(): { dataInicio: Date; dataFim: Date } {
    if (this.formEdit?.controls.tipoEdicao.value == 2) {
      return {
        dataInicio: moment(this.divisao.inicioPeriodoLetivo, 'DD/MM/YYYY').toDate(),
        dataFim: moment(this.divisao.fimPeriodoLetivo, 'DD/MM/YYYY').toDate(),
      };
    } else if (this.formEdit?.controls.tipoEdicao.value == 3) {
      return {
        dataInicio: moment(this.formEdit.controls.dataInicio.value, 'YYYY-MM-DD').toDate(),
        dataFim: moment(this.formEdit.controls.dataFim.value, 'YYYY-MM-DD').toDate(),
      };
    } else {
      return { dataInicio: this.eventoAula.data, dataFim: this.eventoAula.data };
    }
  }

  private validarEdicaoEventosSelecionados(): boolean {
    const eventosSelecionados = this.filtrarEventosSelecionados();
    let falha = false;
    if (eventosSelecionados.length === 0 && this.formEdit.valid) {
      this.mensagemInformativa = 'Nenhum horário será alterado.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      falha = true;
    } else if (eventosSelecionados.length > 0 && this.formEdit.valid) {
      if (
        eventosSelecionados.some(
          s => s.situacaoApuracaoFrequencia === 'Executada' || s.situacaoApuracaoFrequencia === 'Não executada'
        )
      ) {
        this.mensagemInformativa = `Alteração ou exclusão do horário de aula não permitida. Existem aulas que estão com a
        situação de "apurada" ou "não executada" dentro do período informado.`;
        this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
        falha = true;
      }
    }
    return falha;
  }

  validarHorarioAlterado(evento: EventoAulaModel): boolean {
    if (this.formEdit.controls.tipoEdicao.value == 1) {
      return (
        this.formEdit.controls.horarios.value != evento.seqHorarioAgd ||
        moment(this.formEdit.controls.novaData.value, 'YYYY-MM-DD').format('YYYYMMDD') !==
          moment(evento.data).format('YYYYMMDD')
      );
    } else {
      return this.formEdit.controls.horarios.value != evento.seqHorarioAgd;
    }
  }

  exibirModalMensagemInformativa() {
    if (this.alunosCoincidentes.length > 0 || this.protocolosCoincidentes.length > 0) {
      this.modalMensagemInformtiva.open();
    }
  }

  private filtrarEventosSelecionados(): EventoAulaModel[] {
    const eventos: EventoAulaModel[] = this.filtrarEventosPorPeriodo(
      this.divisao,
      this.eventoAula.data,
      this.eventoAula.seqHorarioAgd
    )
      .filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
      .filter(f => this.validarHorarioAlterado(f));
    return eventos;
  }

  private filtrarEventosPorPeriodo(
    divisao: EventoAulaDivisaoTurmaModel,
    data: Date,
    seqHorarioAgd: string
  ): EventoAulaModel[] {
    let eventos: EventoAulaModel[];
    switch (this.formEdit.value.tipoEdicao) {
      case '1':
        const sData = moment(data).format('YYYYMMDD');
        eventos = divisao.eventoAulas.filter(
          f => moment(f.data).format('YYYYMMDD') === sData && f.seqHorarioAgd === seqHorarioAgd
        );
        break;
      case '2':
        eventos = divisao.eventoAulas;
        break;
      case '3':
        const mDataInicio = moment(this.formEdit.value.dataInicio, 'YYYY-MM-DD');
        const mDataFim = moment(this.formEdit.value.dataFim, 'YYYY-MM-DD');
        eventos = divisao.eventoAulas.filter(f => {
          const data = moment(f.data, 'YYYY-MM-DD');
          return data >= mDataInicio && data <= mDataFim;
        });
        break;
    }
    return eventos;
  }

  private filtrarEventosColisao(): EventoAulaModel[] {
    const [, , numeroDivisao, numeroGrupo] = this.eventoAula.grupoFormatado.split('.').map(v => +v);
    const eventos: EventoAulaModel[] = [];
    const data = moment(this.formEdit.controls.novaData.value, 'YYYY-MM-DD').toDate();
    const seqHorarioAgd = this.formEdit.controls.horarios.value as string;
    this.dataService.dadosTurma.eventoAulaDivisoesTurma.forEach(divisao => {
      if (!(divisao.numero == numeroDivisao && divisao.numeroGrupo != numeroGrupo)) {
        eventos.push(...this.filtrarEventosPorPeriodo(divisao, data, seqHorarioAgd));
      }
    });
    return eventos.filter(f => f.seqHorarioAgd === seqHorarioAgd);
  }

  private async validarColisaoAlunos(eventosSimulados: EventoAulaModel[]) {
    this.alunosCoincidentes = await this.service.validarColisaoHorarioAluno(eventosSimulados);
  }

  private async validarColisaoHorarioSolicitacaoServico(eventosSimulados: EventoAulaModel[]) {
    this.protocolosCoincidentes = await this.service.validarColisaoHorarioSolicitacaoServico(eventosSimulados);
  }

  private async validarColisoesAunosEProfessores(eventosSimulados: EventoAulaModel[]) {
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

    this.validacaoBackend$.next(false);
  }
}
