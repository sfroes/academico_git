import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import moment from 'moment';
import { SmcModalComponent } from 'projects/shared/components/smc-modal/smc-modal.component';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { Subscription } from 'rxjs';
import { EventoAulaAgendamentoEditModel } from '../../moldels/evento-aula-agendamento-edit.model';
import { EventoAulaDivisaoTurmaModel } from '../../moldels/evento-aula-divisao-turma.model';
import { EventoAulaModel } from '../../moldels/evento-aula.model';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';
import { SmcButtonComponent } from './../../../../../../../shared/components/smc-button/smc-button.component';

@Component({
  selector: 'sga-evento-aula-agendamento-edit-local',
  templateUrl: './evento-aula-agendamento-edit-local.component.html',
  styles: [],
  standalone: false,
})
export class EventoAulaAgendamentoEditLocalComponent implements AfterViewInit, OnDestroy {
  formEdit: FormGroup;
  divisao: EventoAulaDivisaoTurmaModel;
  eventoAula: EventoAulaModel;
  modeloCrud: EventoAulaAgendamentoEditModel;
  esconderDataPeriodo: boolean = true;
  dataObrigatoria: boolean = false;
  turmaIntegradaSEF: boolean = true;
  repetirDisabled = true;
  dataSourceTipoInicioOcorrencia: SmcKeyValueModel[] = [];
  mensagemInformativa: string = null;
  classeMensagemInformativa:
    | 'smc-sga-mensagem-local-informa'
    | 'smc-sga-mensagem-local-alerta'
    | 'smc-sga-mensagem-local-sucesso'
    | 'smc-sga-mensagem-local-erro' = 'smc-sga-mensagem-local-informa';
  mensagemInicialNotificacao: string = '';
  mensagemSucessoNotificacao: string = '';
  mensagemAssert: string = '';
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
    this.subs = this.formEdit.valueChanges.subscribe(value => {
      this.validarMensagensInformativas();
    });
    this.modal.open();
  }

  prepararModelo(seqDivisaoTurma: string, seqEventoAula: string) {
    this.divisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq == seqDivisaoTurma);
    this.divisao.dscTabelaHorario = this.dataService.tabelaHorarioAgd.descricao;
    this.eventoAula = this.divisao.eventoAulas.find(f => f.seq == seqEventoAula);
    this.turmaIntegradaSEF = this.dataService.turmaIntegradaSEF;
  }

  inicializarModelo() {
    const modelo = this.fb.group({
      local: [this.eventoAula.local],
      codigoLocalSEF: [this.eventoAula.codigoLocalSEF],
      codigoRecorrencia: [this.eventoAula.codigoRecorrencia],
      tipoEdicao: ['1', Validators.required],
      dataInicio: [''],
      dataFim: [''],
    });
    return modelo;
  }

  async onSubmit() {
    const eventos = this.filterEventosSelecionados();
    const seqEventos = eventos.map(m => m.seq);
    this.divisao.eventoAulas = this.divisao.eventoAulas.filter(f => !seqEventos.includes(f.seq));
    this.notificationService.information(this.mensagemInicialNotificacao);
    // Move eventos para processando
    this.service.registrarEventosProcessando(eventos);
    this.service.refresh.next();
    // Gavação alteração
    this.eventoAula = null;
    this.fecharModal();
    try {
      await this.service.editarLocalEventos(this.formEdit.value.codigoLocalSEF, this.formEdit.value.local, seqEventos);
      await this.service.atualizarEventos();
      this.notificationService.success(this.mensagemSucessoNotificacao);
    } catch {
      this.service.cancelarEventosProcessando(eventos);
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
    this.changeDetection.detectChanges();
  }

  localSelecionado(local: SmcKeyValueModel) {
    this.formEdit.get('local').setValue(local.value);
    this.formEdit.get('local').markAsDirty();
    this.formEdit.get('codigoLocalSEF').setValue(local.key);
    this.formEdit.get('codigoLocalSEF').markAsDirty();
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
        this.mensagemInicialNotificacao = `Iniciando alteração de local do evento do dia ${this.eventoAula.diaSemanaFormatada.toLowerCase()} de ${
          this.eventoAula.horaInicio
        } - ${this.eventoAula.horaFim}.`;
        this.mensagemSucessoNotificacao = `Alteração de local do evento do dia ${this.eventoAula.diaSemanaFormatada.toLowerCase()} de ${
          this.eventoAula.horaInicio
        } - ${this.eventoAula.horaFim}`;
        this.mensagemAssert = `O local do agendamento do dia ${this.eventoAula.diaSemanaFormatada.toLowerCase()} de ${
          this.eventoAula.horaInicio
        } - ${this.eventoAula.horaFim} será alterado.`;
        break;
      case '2':
        quantidadeEventos = this.divisao.eventoAulas.filter(f => f.seqHorarioAgd == this.eventoAula.seqHorarioAgd)
          .length;
        this.mensagemInicialNotificacao = `Iniciando a alteração de local dos agendamentos recorrentes de ${descricaoEvento}.`;
        this.mensagemSucessoNotificacao = `Alteração de local dos agendamentos recorrentes de ${descricaoEvento}`;
        this.mensagemAssert = `Ao salvar esta alteração, ${quantidadeEventos} agendamento(s), de ${descricaoEvento}, terão o local alterado.`;
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
        this.mensagemInicialNotificacao = `Iniciando a alteração de local dos agendamento(s) de
                                           ${descricaoEvento}, no período de
                                           ${dataInicio.format('DD/MM/YYYY')} a ${dataFim.format('DD/MM/YYYY')}.`;
        this.mensagemSucessoNotificacao = `Alteração de local do(s) agendamento(s) de ${descricaoEvento}, no período de
                                          ${dataInicio.format('DD/MM/YYYY')} a ${dataFim.format('DD/MM/YYYY')}`;
        this.mensagemAssert = `Ao salvar esta alteração, ${quantidadeEventos} agendamento(s) de ${descricaoEvento}, de ${dataInicio.format(
          'DD/MM/YYYY'
        )} a ${dataFim.format('DD/MM/YYYY')}, terão o local alterado.`;
        break;
    }
    this.mensagemSucessoNotificacao += ', realizada com sucesso.';
    this.modalAssert.open();
    this.btNao.focus();
  }

  private validarMensagensInformativas() {
    this.mensagemInformativa = null;
    this.formEdit.setErrors(null);
    if (this.formEdit.pristine || this.formEdit.invalid) {
      return;
    }
    this.mensagemInformativa || this.validarEventosSelecionados();
    this.changeDetection.detectChanges();
  }

  private validarEventosSelecionados() {
    const eventosSelecionados = this.filterEventosSelecionados();
    if (eventosSelecionados.length === 0) {
      this.mensagemInformativa = 'Nenhum local será alterado.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      this.formEdit.setErrors({ nenhumRegistro: true });
    } else if (
      eventosSelecionados.some(
        s => s.situacaoApuracaoFrequencia === 'Executada' || s.situacaoApuracaoFrequencia === 'Não executada'
      )
    ) {
      this.mensagemInformativa = `Alteração ou exclusão do local de aula não permitida. Existem aulas que estão com a
                                  situação de "apurada" ou "não executada" dentro do período informado.`;
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      this.formEdit.setErrors({ situacaoEventoInvalida: true });
    }
  }

  validarLocalAlterado(evento: EventoAulaModel): boolean {
    return (
      this.formEdit.controls.codigoLocalSEF.value != evento.codigoLocalSEF ||
      this.formEdit.controls.local.value != evento.local
    );
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
    eventos = eventos.filter(f => this.validarLocalAlterado(f));
    return eventos;
  }
}
