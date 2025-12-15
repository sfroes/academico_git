import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PoCheckboxGroupOption, PoSelectOption } from '@po-ui/ng-components';
import { CalendarEvent } from 'angular-calendar';
import * as moment from 'moment';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { SmcModalComponent } from '../../../../../../../shared/components/smc-modal/smc-modal.component';
import { distinctArray, isNullOrEmpty } from '../../../../../../../shared/utils/util';
import { EventoAulaSimulacaoModel } from '../../moldels/evento-aula-simulacao.model';
import { EventoAulaModel } from '../../moldels/evento-aula.model';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';
import { EventoAulaAgendamentoSimulacaoComponent } from '../evento-aula-agendamento-simulacao/evento-aula-agendamento-simulacao.component';
import { EventoAulaDivisaoTurmaModel } from './../../moldels/evento-aula-divisao-turma.model';
import { EventoAulaValidacaoColisaoColaboradorModel } from '../../moldels/evento-aula-validacao-colisao-colaborador.model';
import { BehaviorSubject, Subscription } from 'rxjs';
import { EventoAulaBiTreeLocalComponent } from '../evento-aula-bi-tree-local/evento-aula-bi-tree-local.component';
import { SmcButtonComponent } from './../../../../../../../shared/components/smc-button/smc-button.component';

@Component({
  selector: 'sga-evento-aula-agendamento-add',
  templateUrl: './evento-aula-agendamento-add.component.html',
  styles: [],
})
export class EventoAulaAgendamentoAddComponent implements OnInit, OnDestroy, AfterViewInit {
  formAgendamento: FormGroup;
  dataSourceTabelaHorario: PoSelectOption[] = [];
  dataSourceDivisoes: SmcKeyValueModel[] = [];
  dataSourceTurno: SmcKeyValueModel[] = [];
  dataSourceDiasSemana: PoCheckboxGroupOption[] = [];
  dataSourceTipoOcorrencia: SmcKeyValueModel[] = [];
  dataSourceTipoInicioOcorrencia: SmcKeyValueModel[] = [];
  dataSourceColaboradores: SmcKeyValueModel[] = [];
  tipoRecorrenciaMensal = false;
  divisaoSelecionada: EventoAulaDivisaoTurmaModel;
  inicioPeriodoLetivo: string;
  fimPeriodoLetivo: string;
  msgErroDataTermino: string;
  camposDisabled = true;
  turmaIntegradaSEF = true;
  alunosCoincidentes: string[] = [];
  protocolosCoincidentes: string[] = [];
  formValido = false;
  tipoRecorrenciaDisabled = true;
  repetirDisabled = true;
  datasComecoTerminoDisabled = false;
  telaCarregada = false;
  validacao = false;
  mensagemInformativa: string;
  mensagemAssert = '';
  horarioDisabled = true;
  classeMensagemInformativa:
    | 'smc-sga-mensagem-local-informa'
    | 'smc-sga-mensagem-local-alerta'
    | 'smc-sga-mensagem-local-sucesso'
    | 'smc-sga-mensagem-local-erro' = 'smc-sga-mensagem-local-informa';
  validacaoBackend$ = new BehaviorSubject(false);
  _subs: Subscription[] = [];
  set subs(value: Subscription) {
    this._subs.push(value);
  }
  //get formValido() {
  //  return (this.formAgendamento?.valid ?? false) && this.classeMensagemInformativa !== 'smc-sga-mensagem-local-erro';
  //}
  @Output() novoEvento = new EventEmitter<CalendarEvent>();
  @ViewChild(SmcModalComponent) modal: SmcModalComponent;
  @ViewChild(EventoAulaAgendamentoSimulacaoComponent) modalSimulacao: EventoAulaAgendamentoSimulacaoComponent;
  @ViewChild(EventoAulaBiTreeLocalComponent) treeView: EventoAulaBiTreeLocalComponent;
  @ViewChild('btNao') btNao: SmcButtonComponent;
  @ViewChild('btNaoNew') btNaoNew: SmcButtonComponent;
  @ViewChild('confirmar') modalAssertConfirmar: SmcModalComponent;
  @ViewChild('confirmarNew') modalAssertConfirmarNew: SmcModalComponent;
  @ViewChild('modalMensagemInformativa') modalMensagemInformtiva: SmcModalComponent;

  constructor(
    private fb: FormBuilder,
    private service: EventoAulaService,
    private dataService: EventoAulaDataService,
    private changeDetection: ChangeDetectorRef,
    private router: Router,
    private notificationService: SmcNotificationService
  ) { }

  ngAfterViewInit(): void {
    this.abrirModal();
    this.changeDetection.detectChanges();
  }

  ngOnInit(): void { }

  ngOnDestroy(): void {
    this._subs.forEach(f => f.unsubscribe());
  }

  abrirModal() {
    if (!this.dataService.dadosTurma) {
      this.fecharModal();
      return;
    }
    this.modal.open();
    this.turmaIntegradaSEF = this.dataService.turmaIntegradaSEF;
    this.formAgendamento = this.inicializarModelo();
    this.preenchimentoDataSources();
    this.subs = this.formAgendamento.valueChanges.subscribe(result => {
      this.formValido = this.formAgendamento.valid;
      if (!isNullOrEmpty(result.termina)) {
        this.validarDataTermino(result.termina);
        //Ajusta a mensagem de assert conforme muda a data de começo e fim
        const mensagemAssertPadrao =
          'Confirma a inclusão do(s) agendamento(s), no período de {dti} a {dtf}, conforme os parâmetros informados?';
        this.mensagemAssert = mensagemAssertPadrao
          .replace('{dti}', moment(result.comeca).format('DD/MM/YYYY'))
          .replace('{dtf}', moment(result.termina).format('DD/MM/YYYY'));
      }
      const professoresSelecionados: string[] = result.colaboradores
        .filter(f => !isNullOrEmpty(f.seqColaborador))
        .map(m => m.seqColaborador);
      this.dataSourceColaboradores = this.dataService.dataSourceColaboradores.filter(
        f => !professoresSelecionados.includes(f.key)
      );
      this.validarMensagensInformativas();
      this.habilitarHorarios();
    });
    this.subs = this.formAgendamento.controls.turno.valueChanges.subscribe(_ => {
      this.formAgendamento.controls.horarios.setValue('');
    });
    this.subs = this.formAgendamento.controls.seqDivisaoTurma.valueChanges.subscribe((seqDivisaoTurma: string) => {
      this.dependecyDivisaoTurma(seqDivisaoTurma);
    });
    this.changeDetection.detectChanges();
  }

  inicializarModelo() {
    const modelo = this.fb.group({
      seqDivisaoTurma: ['', Validators.required],
      idtTipoRecorrencia: [''],
      idtTipoInicioRecorrencia: [''],
      repetir: [''],
      diasSemana: ['', Validators.required],
      turno: [''],
      comeca: [''],
      termina: [''],
      local: [''],
      codigoLocalSEF: [''],
      horarios: [''],
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

  onSubmit() {
    this.gravarEventos();
    this.fecharModal();
  }

  salvarNovo() {
    this.modalAssertConfirmarNew.open();
    this.btNaoNew.focus();
  }

  onSubmitNew(assert: SmcModalComponent) {
    assert.close();
    this.gravarEventos();
    const seqDivisaoTurma = this.divisaoSelecionada.seq;
    this.resetCampos();
    this.formAgendamento.controls.seqDivisaoTurma.setValue(seqDivisaoTurma);
  }

  dependecyDivisaoTurma(seqDivisaoTurma: string) {
    this.mensagemInformativa = null;
    this.divisaoSelecionada = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq === seqDivisaoTurma);
    this.inicioPeriodoLetivo = moment(this.divisaoSelecionada?.inicioPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD');
    this.fimPeriodoLetivo = moment(this.divisaoSelecionada?.fimPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD');

    //Se porventura tiver divisao habilita todos os campos...
    if (this.divisaoSelecionada) {
      this.camposDisabled = false;
      this.divisaoSelecionada.dscTabelaHorario = this.dataService.tabelaHorarioAgd.descricao;
    } else {
      this.camposDisabled = true;
      this.resetCampos();
      return;
    }

    //Se por ventura tiver aula aos sabados habilita o sabado
    if (this.divisaoSelecionada) {
      this.dataSourceDiasSemana.forEach(f => {
        if (f.label === 'Sábado') {
          f.disabled = !this.divisaoSelecionada.aulaSabado;
        }
      });
      this.formAgendamento.controls.diasSemana.reset();
      this.formAgendamento.controls.horarios.reset();
      this.dataSourceDiasSemana = [...this.dataSourceDiasSemana];
    }

    //NV09 Trazer como default para estes campos a data inicio e data fim do periodo letivo da turma.
    this.formAgendamento
      .get('comeca')
      .setValue(moment(this.divisaoSelecionada?.inicioPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD'));
    this.formAgendamento
      .get('termina')
      .setValue(moment(this.divisaoSelecionada?.fimPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD'));

    //Caso o tipo de tipoDistribuicaoAula - SMC.Calendarios.Common.Areas.CLD.Enums
    if (
      this.divisaoSelecionada?.tipoDistribuicaoAula === 'Semanal' ||
      this.divisaoSelecionada?.tipoDistribuicaoAula === 'Quinzenal'
    ) {
      //NV09 Para a divisão de turma, onde a distribuição de aula cadastrada,  seja 'Semanal' ou 'Quinzenal' somente poderão
      //alterar a data inicio e data fim o usuário que possuir permissão no token Permite Alterar Data Agendamento Aula.
      //Para os outros tipos de distribuição de aula estes dois campos estarão sempre liberados independente de permissão.
      this.datasComecoTerminoDisabled = this.dataService.dadosTurma.permiteAlterarDataAgendamentoAula ? false : true;
      //NV's tipo de recorrencia
      this.formAgendamento.get('idtTipoRecorrencia').setValue(2); //Semanal
      this.tipoRecorrenciaDisabled = true;
      this.tipoRecorrenciaMensal = false;
    } else {
      this.formAgendamento.get('idtTipoRecorrencia').setValue('');
      this.tipoRecorrenciaDisabled = false;
      this.tipoRecorrenciaMensal = true;
    }
    //NV12 Se a distribuicao de aula configurada for quinzenal , este campo deverá vir preenchido com o valor 2 e o usuário não poderá alterá-lo
    if (this.divisaoSelecionada?.tipoDistribuicaoAula === 'Semanal') {
      this.formAgendamento.get('repetir').setValue(1);
      this.repetirDisabled = true;
    } else if (this.divisaoSelecionada?.tipoDistribuicaoAula === 'Quinzenal') {
      this.formAgendamento.get('repetir').setValue(2);
      this.repetirDisabled = true;
    } else {
      this.formAgendamento.get('repetir').setValue('');
      this.repetirDisabled = false;
    }
  }

  async preenchimentoDataSources() {
    //NV01 - A  divisão do componente associada a divisão da turma possuir  carga horária de grade maior que zero e
    const dsDivisoes: SmcKeyValueModel[] = [];
    this.dataService.dadosTurma.eventoAulaDivisoesTurma.forEach(divisao => {
      if (divisao.cargaHorariaLancada < divisao.cargaHorariaGrade) {
        const option: SmcKeyValueModel = { key: divisao.seq, value: divisao.descricaoDivisaoFormatada };
        dsDivisoes.push(option);
      }
    });
    this.dataSourceDivisoes = dsDivisoes;
    this.dataSourceTipoOcorrencia = this.dataService.dataSourceTipoOcorrencia;
    this.dataSourceDiasSemana = this.dataService.dataSourceDiasSemana;
    this.dataSourceTurno = this.dataService.dataSourceTurno;
    this.dataSourceTipoInicioOcorrencia = this.dataService.dataSourceTipoInicioOcorrencia;
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
      const horariosSelecionados = Array.from(this.formAgendamento?.controls.horarios.value ?? []).filter(f =>
        itensTemporarios.some(s => s.value == f)
      );
      this.formAgendamento.controls.horarios.setValue(horariosSelecionados);
      this.dataSourceTabelaHorario = itensTemporarios;
    } else {
      this.dataSourceTabelaHorario = [];
      this.formAgendamento.controls.horarios.setValue('');
    }

    if (isNullOrEmpty(seqturno)) {
      const options = { onlySelf: true, emitEvent: false };
      this.formAgendamento.get('horarios').setValue('', options);
    }
  }

  fecharModal() {
    this.router.navigate([{ outlets: { modais: null } }], { queryParamsHandling: 'preserve' });
    this.formAgendamento = undefined;
    this.dataSourceTabelaHorario = [];
    this.modal.close();
  }

  abrirSimulacao() {
    this.modalSimulacao.gerarSimulacao(this.formAgendamento);
  }

  onChangeTipoRecorrencia(tipoRecorrencia: number) {
    const idtTipoInicioRecorrencia = this.formAgendamento.get('idtTipoInicioRecorrencia');
    if (tipoRecorrencia !== 2) {
      this.tipoRecorrenciaMensal = true;
      idtTipoInicioRecorrencia.setValidators([Validators.required]);
    } else {
      this.tipoRecorrenciaMensal = false;
      idtTipoInicioRecorrencia.clearValidators();
    }
    idtTipoInicioRecorrencia.updateValueAndValidity();
  }

  validarDataTermino(data: string) {
    if (moment(data) > moment(this.fimPeriodoLetivo)) {
      this.msgErroDataTermino = 'A data informada deverá estar dentro do periodo letivo da turma.';
    } else {
      this.msgErroDataTermino = 'A data fim deverá ser maior ou igual à data início.';
    }
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

  private validarMensagensInformativas() {
    if (this.validacao) {
      return;
    }
    this.mensagemInformativa = null;
    this.classeMensagemInformativa = 'smc-sga-mensagem-local-informa';

    this.validacao = true;

    this.mensagemInformativa || this.validarNumeroAulas();
    this.mensagemInformativa || this.validarAtualizacaoHistorico();
    if (this.formValido) {
      const eventosSimulados = this.executarSimulacao();
      this.mensagemInformativa || this.validarColisaoHorarios(eventosSimulados);
      this.mensagemInformativa || this.ValidarQuantidadeEventos(eventosSimulados.length);
      this.mensagemInformativa || this.validarCargaHorariaTotal(eventosSimulados.length);
      this.validarColisoesAunosEProfessores(eventosSimulados);
    }

    this.changeDetection.detectChanges();

    this.validacao = false;
  }

  private executarSimulacao() {
    const parametros: EventoAulaSimulacaoModel = {
      seqDivisaoTurma: this.formAgendamento.value.seqDivisaoTurma,
      seqsHorarios: Array.from(this.formAgendamento.value.horarios),
      descricaoDivisao: this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
        f => f.seq === this.formAgendamento.value.seqDivisaoTurma
      ).descricaoDivisaoFormatada,
      idtTipoRecorrencia: this.formAgendamento.value.idtTipoRecorrencia,
      idtTipoInicioRecorrencia: this.formAgendamento.value.idtTipoInicioRecorrencia,
      comeca: this.formAgendamento.value.comeca,
      termina: this.formAgendamento.value.termina,
      repetir: this.formAgendamento.value.repetir,
      local: this.formAgendamento.value.local,
      codigoLocalSEF: this.formAgendamento.value.codigoLocalSEF,
      colaboradores: this.formAgendamento.value.colaboradores.filter(f => f.seqColaborador),
    };
    const eventosSimulados = this.service.gerarSimulacaoEventos(parametros);
    return eventosSimulados;
  }

  private validarNumeroAulas() {
    const horarios = (this.formAgendamento.get('horarios').value as []) ?? [];
    const seqDivisaoTurma = this.formAgendamento.get('seqDivisaoTurma').value;
    const divisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq === seqDivisaoTurma);
    if (!divisao || divisao.tipoDistribuicaoAula === 'Concentrada' || divisao.tipoDistribuicaoAula === 'Livre') {
      return;
    }
    const eventosLancados =
      divisao.eventoAulas
        .filter(f => f.codigoRecorrencia)
        .map(me => `${me.diaSemana}|${me.horaInicio}|${me.horaFim}`) ?? [];
    const horariosLancados = distinctArray(eventosLancados).length;
    const numAulasInformada = +horarios.length + horariosLancados;
    let numAulasSemanaisPemitidas = divisao.cargaHorariaGrade / divisao.quantidadeSemanas;

    if (divisao.tipoDistribuicaoAula === 'Quinzenal') {
      numAulasSemanaisPemitidas = numAulasSemanaisPemitidas * 2;
    }

    if (numAulasInformada > numAulasSemanaisPemitidas) {
      this.mensagemInformativa = `A quantidade de aulas semanais informada ${numAulasInformada}
                                  não pode ser maior que a quantidade de aulas
                                  semanais permitida para a carga horária do componente ${numAulasSemanaisPemitidas}`;
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      this.formAgendamento
        .get('horarios')
        .setValidators([Validators.maxLength(numAulasSemanaisPemitidas - horariosLancados)]);
    } else {
      this.mensagemInformativa = null;
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-informa';
      this.formAgendamento.get('horarios').setValidators([Validators.required]);
    }
    this.formAgendamento.get('horarios').updateValueAndValidity();
  }

  private async gravarEventos() {
    const parametros: EventoAulaSimulacaoModel = {
      seqDivisaoTurma: this.formAgendamento.value.seqDivisaoTurma,
      seqsHorarios: Array.from(this.formAgendamento.value.horarios),
      descricaoDivisao: this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
        f => f.seq === this.formAgendamento.value.seqDivisaoTurma
      ).descricaoDivisaoFormatada,
      idtTipoRecorrencia: this.formAgendamento.value.idtTipoRecorrencia,
      idtTipoInicioRecorrencia: this.formAgendamento.value.idtTipoInicioRecorrencia,
      comeca: this.formAgendamento.value.comeca,
      termina: this.formAgendamento.value.termina,
      repetir: this.formAgendamento.value.repetir,
      local: this.formAgendamento.value.local,
      codigoLocalSEF: this.formAgendamento.value.codigoLocalSEF,
      colaboradores: this.formAgendamento.value.colaboradores.filter(f => f.seqColaborador),
    };
    const eventos = this.service
      .gerarSimulacaoEventos(parametros)
      .map(m => ({ ...m, turno: this.formAgendamento.value.turno }));
    const recorrenciaComeca = moment(parametros.comeca).format('DD/MM/YYYY');
    const recorrenciaTermina = moment(parametros.termina).format('DD/MM/YYYY');
    if (eventos.length === 0) {
      this.notificationService.warning('Conforme regras não existe eventos a serem criados!');
      return;
    }
    this.notificationService.information(
      `Iniciando a gravação dos agendamentos, de ${recorrenciaComeca} a ${recorrenciaTermina}, e todas as suas recorrências.`
    );
    this.service.registrarEventosProcessando(eventos);
    try {
      await this.service.salvarEventos(eventos);
      await this.service.atualizarEventos();
      this.notificationService.success(
        `Gravação dos agendamentos de ${recorrenciaComeca} a ${recorrenciaTermina}, e todas as suas recorrências. Concluída com sucesso.`
      );
      if (this.dataService.dadosTurma.eventoAulaTurmaCabecalho.cargaHorariaCompleta) {
        this.fecharModal();
      } else if (this.formAgendamento) {
        this.preenchimentoDataSources();
        this.dependecyDivisaoTurma(this.formAgendamento.value.seqDivisaoTurma);
      }
    } catch {
      this.service.cancelarEventosProcessando(eventos);
    }
  }

  private validarCargaHorariaTotal(totalCargaSimulada: number) {
    this.formValido = false;
    const divisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
      f => f.seq === this.formAgendamento.value.seqDivisaoTurma
    );

    const totalCargaLancadaSimulada = totalCargaSimulada + divisao.cargaHorariaLancada;
    const totalCargaHorariaGrade = divisao.cargaHorariaGrade;
    const tipoDistribuicaoAula = divisao.tipoDistribuicaoAula;

    if (totalCargaLancadaSimulada < totalCargaHorariaGrade) {
      this.mensagemAssert = 'A carga horária a ser lançada está menor que a carga horária cadastrada para a divisão do componente. Deseja continuar?';
    }

    if (totalCargaLancadaSimulada > totalCargaHorariaGrade && tipoDistribuicaoAula === 'Concentrada') {
      this.mensagemInformativa = `Para a divisão de turma configurada como 'concentrada' a carga horária total lançada
                                  deverá ser exatamente igual a carga horária da divisão do componente`;
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      this.formValido = false;
    }
    else {
      this.formValido = this.formAgendamento.valid;
    }

    if (totalCargaLancadaSimulada > totalCargaHorariaGrade && tipoDistribuicaoAula === 'Livre') {
      this.mensagemAssert = 'A carga horária total lançada está ultrapassando a carga horária da divisão do componente. Deseja continuar?';
    }
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
    }
    else {
      this.formValido = this.formAgendamento.valid;
    }

    this.validacaoBackend$.next(false);
  }

  private validarAtualizacaoHistorico() {
    //RN_TUR_022 - Valida aluno com histórico
    const seqDivisao = this.formAgendamento.value.seqDivisaoTurma as string;
    if (isNullOrEmpty(seqDivisao)) {
      return;
    }
    const temHistorico = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq === seqDivisao)
      .temHistoricoEscolar;
    const temProfessores = this.formAgendamento.value.colaboradores.some(f => !isNullOrEmpty(f.seqColaborador));
    if (temProfessores && temHistorico) {
      this.mensagemInformativa =
        'Existe aluno com registro do histórico escolar para esta divisão de turma, esta operação vai atualizar os dados de professor.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-alerta';
    }
  }

  private ValidarQuantidadeEventos(totalEventos: number) {
    if (totalEventos === 0 && this.formAgendamento.value.horarios.length > 0) {
      this.mensagemInformativa = `O período informado para geração da aula não possui nenhum dos dias da semana selecionado. Favor rever os parâmetros para
      geração dos eventos.`;
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
    }
  }

  private habilitarHorarios() {
    this.horarioDisabled = true;
    const turno = this.formAgendamento.value.turno;
    const diasSemana = this.formAgendamento.value.diasSemana as [];
    if (!isNullOrEmpty(turno) && diasSemana?.length > 0) {
      this.horarioDisabled = false;
    }
  }

  private resetCampos() {
    this.formAgendamento.controls.turno.setValue('');
    this.formAgendamento.controls.horarios.setValue('');
    this.formAgendamento.controls.local.setValue('');
    this.formAgendamento.controls.codigoLocalSEF.setValue('');
    this.formAgendamento.controls.idtTipoRecorrencia.setValue('');
    this.formAgendamento.controls.idtTipoInicioRecorrencia.setValue('');
    this.formAgendamento.controls.repetir.setValue('');
    this.formAgendamento.controls.diasSemana.setValue('');
    this.formAgendamento.controls.comeca.setValue('');
    this.formAgendamento.controls.termina.setValue('');
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
