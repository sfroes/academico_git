import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PoCheckboxGroupOption, PoTableComponent } from '@po-ui/ng-components';
import moment from 'moment';
import { SmcModalComponent } from 'projects/shared/components/smc-modal/smc-modal.component';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { isNullOrEmpty } from 'projects/shared/utils/util';
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
import { SmcButtonComponent } from './../../../../../../../shared/components/smc-button/smc-button.component';

@Component({
  selector: 'sga-evento-aula-agendamento-edit-colaborador',
  templateUrl: './evento-aula-agendamento-edit-colaborador.component.html',
  styles: [],
  standalone: false,
})
export class EventoAulaAgendamentoEditColaboradorComponent implements AfterViewInit, OnDestroy {
  formEdit: FormGroup;
  divisao: EventoAulaDivisaoTurmaModel;
  eventoAula: EventoAulaModel;
  dataSourceColaboradores: SmcKeyValueModel[] = [];
  modeloCrud: EventoAulaAgendamentoEditModel;
  esconderDataPeriodo: boolean = true;
  dataObrigatoria: boolean = false;

  dataSourceTipoInicioOcorrencia: SmcKeyValueModel[] = [];

  mensagemInicialNotificacao: string = null;
  mensagemSucessoNotificacao: string = null;
  mensagemAssert: string = null;
  mensagemInformativa: string = null;
  classeMensagemInformativa:
    | 'smc-sga-mensagem-local-informa'
    | 'smc-sga-mensagem-local-alerta'
    | 'smc-sga-mensagem-local-sucesso'
    | 'smc-sga-mensagem-local-erro' = 'smc-sga-mensagem-local-informa';
  validandoProfessor$ = new BehaviorSubject(false);
  private _eventoAulaSimulacao: EventoAulaModel[] = [];
  private _subs: Subscription[] = [];
  private set subs(sub: Subscription) {
    this._subs.push(sub);
  }

  @ViewChild(SmcModalComponent) modal: SmcModalComponent;
  @ViewChild('confirmar') modalAssert: SmcModalComponent;
  @ViewChild('btNao') btNao: SmcButtonComponent;

  constructor(
    private fb: FormBuilder,
    private service: EventoAulaService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private changeDetection: ChangeDetectorRef,
    private dataService: EventoAulaDataService,
    private notificationService: SmcNotificationService
  ) {}

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
    this.subs = this.formEdit.controls.colaboradores.valueChanges.subscribe(novosColaboradores =>
      this.validarColaboradorDuplicado(novosColaboradores)
    );
    this.subs = this.formEdit.valueChanges.subscribe(_ => {
      this.mensagemInformativa = null;
      if (this.formEdit.valid) {
        this.validarMensagensInformativas();
        this.validarColisaoProfessores();
      }
      this.changeDetection.detectChanges();
    });
    this.modal.open();
  }

  prepararModelo(seqDivisaoTurma: string, seqEventoAula: string) {
    this.divisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq == seqDivisaoTurma);
    this.eventoAula = this.divisao.eventoAulas.find(f => f.seq == seqEventoAula);
  }

  inicializarModelo() {
    const modelo = this.fb.group({
      seq: [this.eventoAula.seq],
      seqDivisaoTurma: [this.eventoAula.seqDivisaoTurma],
      seqEventoAulaAgd: [this.eventoAula.seqEventoAgd],
      data: [this.eventoAula.data],
      codigoRecorrencia: [this.eventoAula.codigoRecorrencia],
      colaboradores: this.inicialProfessor(this.eventoAula.colaboradores),
      repetir: [''],
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
      if (prof.seqColaboradorSubstituto === null) {
        const groupProf = this.fb.group({
          seqColaborador: [prof.seqColaborador],
          seqColaboradorBanco: [prof.seqColaborador],
          seqColaboradorSubstituto: [prof.seqColaboradorSubstituto]
        });
        colaboradores.push(groupProf as any);
      } else {
        const indexColaborador = this.dataSourceColaboradores.findIndex(f => f.key === prof.seqColaboradorSubstituto);
        this.dataSourceColaboradores.slice(indexColaborador, 1);
      }
    });
    return colaboradores;
  }

  addProfessor() {
    const control = this.formEdit.get('colaboradores') as FormArray;
    const groupProf = this.fb.group({
      seqColaborador: [''],
      seqColaboradorBanco: [''],
      seqColaboradorSubstituto: [''],
    });
    control.push(groupProf as any);
  }

  getProfessores() {
    const professores = this.formEdit.controls.colaboradores as FormArray;
    if (professores.length === 0) {
      const groupProf = this.fb.group({
        seqColaborador: [''],
        seqColaboradorBanco: [''],
        seqColaboradorSubstituto: [''],
      });
      professores.push(groupProf as any);
    }
    return professores.controls;
  }

  removeProfessor(indexProfessor) {
    const control = this.formEdit.get('colaboradores') as FormArray;
    control.removeAt(indexProfessor);
    control.markAsDirty();
  }

  async onSubmit() {
    let seqEventos: string[] = [];
    const eventos = this.filterEventosSelecionados();

    if (eventos.length === 0) {
      this.notificationService.warning('Nenhum evento será alterado.');
      return;
    }

    seqEventos = eventos.map(m => m.seq);
    this.divisao.eventoAulas = this.divisao.eventoAulas.filter(f => !seqEventos.includes(f.seq));

    this.notificationService.information(this.mensagemInicialNotificacao);
    // Move eventos para processando
    this.service.registrarEventosProcessando(eventos);
    this.service.refresh.next();
    // Gravação alteração
    this.eventoAula = null;
    this.fecharModal();
    try {
      const colaboradores: EventoAulaAgendamentoProfessorModel[] =
        this.formEdit.value.colaboradores
          ?.map(m => ({
            seqColaborador: m.seqColaborador,
            seqColaboradorSubstituto: m.seqColaboradorSubstituto,
            colaboradorResponsavel: m.colaboradorResponsavel
          }))
          .filter(f => !isNullOrEmpty(f.seqColaborador)) ?? [];
      const seqEventoTemplate = this.formEdit.value.seq;
      await this.service.editarColaboradoresEventos(colaboradores, seqEventos, seqEventoTemplate, true);
      await this.service.atualizarEventos();
      this.notificationService.success(this.mensagemSucessoNotificacao);
    } catch {
      this.service.cancelarEventosProcessando(eventos);
    }
  }

  dependencyProfessor(professor: AbstractControl) {
    if (!professor.value.seqColaborador) {
      professor.get('seqColaboradorSubstituto').setValue('');
    }
  }

  async preenchimentoDataSources() {
    this.dataSourceColaboradores = [...this.dataService.dataSourceColaboradores];
    this.eventoAula.colaboradores.forEach(prof => {
      if (prof.seqColaboradorSubstituto) {
        const indexProfessorSubstituto = this.dataSourceColaboradores.findIndex(
          f => f.key === prof.seqColaboradorSubstituto
        );
        this.dataSourceColaboradores.splice(indexProfessorSubstituto, 1);
        const indexProfessor = this.dataSourceColaboradores.findIndex(f => f.key === prof.seqColaborador);
        this.dataSourceColaboradores.splice(indexProfessor, 1);
      }
    });
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
    const diaSemana = this.eventoAula.diaSemanaDescricao.toLowerCase();
    const intervalo = `${this.eventoAula.horaInicio} - ${this.eventoAula.horaFim}`;
    const descricaoEvento = `${diaSemana}, ${intervalo}`;
    switch (this.formEdit.value.tipoEdicao) {
      case '1':
        quantidadeEventos = 1;
        const data = moment(this.eventoAula.data).format('DD/MM/YYYY');
        this.mensagemInicialNotificacao = `Iniciando alocação de professor(es) na(o) ${diaSemana} do dia ${data} de ${intervalo}.`;
        this.mensagemSucessoNotificacao = `Alocação de professor(es) na(o) ${diaSemana} do dia ${data} de ${intervalo}`;
        this.mensagemAssert = `O agendamento de ${diaSemana} do dia ${data} de ${intervalo}
        terá o(s) professor(es) alterado(s) de acordo com o vínculo ativo de cada um.`;
        break;
      case '2':
        quantidadeEventos = this.divisao.eventoAulas.filter(
          f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd
        ).length;
        this.mensagemInicialNotificacao = `Iniciando a alocação de professor(es) dos agendamentos recorrentes de ${descricaoEvento}.`;
        this.mensagemSucessoNotificacao = `Alocação de professor(es) dos agendamentos recorrentes de ${descricaoEvento}`;
        this.mensagemAssert = `O(s) ${quantidadeEventos} agendamento(s) de ${descricaoEvento}
        terá(ão) o(s) professor(es) alterado(s) de acordo com o vínculo ativo de cada um.`;
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
        this.mensagemInicialNotificacao = `Iniciando a alocação de professor(es) dos agendamento(s) de
                                           ${descricaoEvento}, no período de
                                           ${dataInicio.format('DD/MM/YYYY')} a ${dataFim.format('DD/MM/YYYY')}.`;
        this.mensagemSucessoNotificacao = `Alocação de professor(es) do(s) agendamento(s) de ${descricaoEvento}, no período de
                                          ${dataInicio.format('DD/MM/YYYY')} a ${dataFim.format('DD/MM/YYYY')}`;
        this.mensagemAssert = `O(s) ${quantidadeEventos} agendamento(s) de ${diaSemana}, ${intervalo},
        no período de ${dataInicio.format('DD/MM/YYYY')} a ${dataFim.format(
          'DD/MM/YYYY'
        )}, terá(ão) o(s) professor(es) alterado(s) de acordo com o vínculo ativo de cada um.`;
        break;
    }
    this.mensagemSucessoNotificacao += ', realizada com sucesso.';
    this.modalAssert.open();
    this.btNao.focus();
  }

  private filterEventosSelecionados() {
    let eventos: EventoAulaModel[];
    switch (this.formEdit.value.tipoEdicao) {
      case '1':
        eventos = [this.eventoAula];
        break;
      case '2':
        eventos = this.divisao.eventoAulas.filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd);
        break;
      case '3':
        const dataInicio = moment(this.formEdit.value.dataInicio, 'YYYY-MM-DD');
        const dataFim = moment(this.formEdit.value.dataFim, 'YYYY-MM-DD');
        eventos = this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
          .filter(f => {
            const data = moment(f.data, 'YYYY-MM-DD');
            return data >= dataInicio && data <= dataFim;
          });
        break;
    }
    eventos = eventos.filter(f => this.validarColaboradorAlterado(f));
    return eventos;
  }

  private validarMensagensInformativas() {
    this.mensagemInformativa = null;
    this.formEdit.setErrors(null);
    if (this.formEdit.pristine) {
      return;
    }
    this.mensagemInformativa || this.validarColaboradoresSelecionados();
    this.changeDetection.detectChanges();
  }

  private validarColaboradoresSelecionados() {
    const eventosSelecionados = this.filterEventosSelecionados();
    if (eventosSelecionados.length === 0) {
      this.mensagemInformativa = 'Nenhum colaborador será alterado.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      this.formEdit.setErrors({ nenhumRegistro: true });
    } else if (
      eventosSelecionados.some(
        s => s.situacaoApuracaoFrequencia === 'Executada' || s.situacaoApuracaoFrequencia === 'Não executada'
      )
    ) {
      this.mensagemInformativa = `Inclusão, alteração ou exclusão  do(a) professor(a)  na aula não permitida.
                                  Existem aulas que estão com a situação de "apurada" ou "não executada" dentro do período informado.`;
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      this.formEdit.setErrors({ situacaoEventoInvalida: true });
    }
  }

  private async validarColisaoProfessores() {
    this.validandoProfessor$.next(true);
    const model: EventoAulaValidacaoColisaoColaboradorModel[] = [];
    const seqEventosSelecionados = this.recuperarSeqEventoSelecionados();
    const seqColaboradoresOriginais = this.recuperarColaboradoresEvento(this.eventoAula);
    const seqColaboradorSelecionados = this.recuperarColaboradoresForm().filter(
      f => !seqColaboradoresOriginais.includes(f)
    );
    this.divisao.eventoAulas
      .filter(f => seqEventosSelecionados.includes(f.seq))
      .filter(f => this.validarColaboradorAlterado(f))
      .forEach(evento =>
        seqColaboradorSelecionados.forEach(seqColaborador =>
          model.push({
            seqColaborador: seqColaborador,
            seqDivisaoTurma: this.divisao.seq,
            codigoLocalSEF: evento.codigoLocalSEF,
            dataAula: evento.data,
            horaInicio: evento.horaInicio,
            horaFim: evento.horaFim,
          })
        )
      );
    if (model.length) {
      const validacao = await this.service.validarColisao(model);
      if (isNullOrEmpty(validacao)) {
      } else {
        this.mensagemInformativa = validacao;
        this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
        this.formEdit.setErrors({ colisaoProfessor: true });
      }
    }
    this.validandoProfessor$.next(false);
  }

  private recuperarSeqEventoSelecionados(): string[] {
    switch (this.formEdit.value.tipoEdicao) {
      case '1':
        return [this.eventoAula.seq];
      case '2':
        return this.divisao.eventoAulas.filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd).map(m => m.seq);
      case '3':
        const dataInicio = moment(this.formEdit.value.dataInicio, 'YYYY-MM-DD');
        const dataFim = moment(this.formEdit.value.dataFim, 'YYYY-MM-DD');
        return this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
          .filter(f => {
            const data = moment(f.data, 'YYYY-MM-DD');
            return data >= dataInicio && data <= dataFim;
          })
          .map(m => m.seq);
      default:
        return [];
    }
  }

  private recuperarPeriodo(): { dataInicio: Date; dataFim: Date } {
    if (this.formEdit?.controls.tipoEdicao.value == 2) {
      return {
        dataInicio: moment(this.divisao.inicioPeriodoLetivo, 'DD/MM/YYYY').toDate(),
        dataFim: moment(this.divisao.fimPeriodoLetivo, 'DD/MM/YYYY').toDate(),
      };
    } else if (this.formEdit?.controls.tipoEdicao.value == 3) {
      return {
        dataInicio: moment(this.formEdit.controls.comeca.value, 'YYYY-MM-DD').toDate(),
        dataFim: moment(this.formEdit.controls.termina.value, 'YYYY-MM-DD').toDate(),
      };
    } else {
      return { dataInicio: this.eventoAula.data, dataFim: this.eventoAula.data };
    }
  }

  private recuperarColaboradoresForm(): string[] {
    return this.formEdit.value.colaboradores
      .map(m => (m.seqColaboradorSubstituto ? m.seqColaboradorSubstituto : m.seqColaborador))
      .filter(f => !isNullOrEmpty(f));
  }

  private recuperarColaboradoresEvento(evento: EventoAulaModel): string[] {
    return (
      evento.colaboradores
        ?.map(m => (m.seqColaboradorSubstituto ? m.seqColaboradorSubstituto : m.seqColaborador))
        .filter(f => !isNullOrEmpty(f)) ?? []
    );
  }

  private validarColaboradorAlterado(evento: EventoAulaModel): boolean {
    const colaboradoresSelecionados = this.recuperarColaboradoresForm().sort();
    const colaboradoresEvento = this.recuperarColaboradoresEvento(evento).sort();
    return (
      colaboradoresSelecionados.length !== colaboradoresEvento.length ||
      colaboradoresEvento.some(e => !colaboradoresSelecionados.includes(e))
    );
  }
}
