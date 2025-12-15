import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PoTableColumn } from '@po-ui/ng-components';
import * as moment from 'moment';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { environment } from 'projects/smc-sga-administrativo/src/environments/environment';
import { Subscription } from 'rxjs';
import { SmcModalComponent } from '../../../../../../../shared/components/smc-modal/smc-modal.component';
import { EventoAulaAgendamentoDeleteModel } from '../../moldels/evento-aula-agendamento-delete.model';
import { EventoAulaDivisaoTurmaModel } from '../../moldels/evento-aula-divisao-turma.model';
import { EventoAulaTurmaCabecalhoModel } from '../../moldels/evento-aula-turma-cabecalho.model';
import { EventoAulaModel } from '../../moldels/evento-aula.model';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';
import { SmcButtonComponent } from './../../../../../../../shared/components/smc-button/smc-button.component';

@Component({
  selector: 'sga-evento-aula-agendamento-delete',
  templateUrl: './evento-aula-agendamento-delete.component.html',
  styles: [],
})
export class EventoAulaAgendamentoDeleteComponent implements AfterViewInit, OnDestroy {
  modeloCrud: EventoAulaAgendamentoDeleteModel;
  esconderDataPeriodo = true;
  formExclusao: FormGroup;
  dataObrigatoria: boolean = false;
  seqsEventosExcluir: string[] = [];
  mensagemAssert = '';
  mensagemInicialNotificacao = '';
  mensagemSucessoNotificacao = '';
  mensagemInformativa: string = null;
  classeMensagemInformativa:
    | 'smc-sga-mensagem-local-informa'
    | 'smc-sga-mensagem-local-alerta'
    | 'smc-sga-mensagem-local-sucesso'
    | 'smc-sga-mensagem-local-erro' = 'smc-sga-mensagem-local-informa';
  private divisao: EventoAulaDivisaoTurmaModel;
  private _subs: Subscription[] = [];
  private set subs(sub: Subscription) {
    this._subs.push(sub);
  }

  @ViewChild(SmcModalComponent) modal: SmcModalComponent;
  @ViewChild('confirmar') modalAssert: SmcModalComponent;
  @ViewChild('btNao') btNao: SmcButtonComponent;

  constructor(
    private dataService: EventoAulaDataService,
    private service: EventoAulaService,
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private changeDetection: ChangeDetectorRef,
    private notificationService: SmcNotificationService
  ) {}

  ngAfterViewInit(): void {
    if (!this.dataService.dadosTurma) {
      this.fecharModal();
      return;
    }
    this.activatedRoute.params.subscribe(params => {
      this.abrirModal(params['seqDivisaoTurma'] as string, params['seqEventoAula'] as string);
      this.changeDetection.detectChanges();
    });
  }

  ngOnDestroy(): void {
    this._subs.forEach(f => f.unsubscribe());
  }

  abrirModal(seqDivisaoTurma: string, seqEventoAula: string) {
    this.prepararModelo(seqDivisaoTurma, seqEventoAula);
    this.formExclusao = this.inicializarModelo();
    this.subs = this.formExclusao.valueChanges.subscribe(_ => this.validarMensagensInformativas());
    this.modal.open();
  }

  prepararModelo(seqDivisaoTurma: string, seqEventoAula: string) {
    this.divisao = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(f => f.seq == seqDivisaoTurma);
    const eventoAula = this.divisao.eventoAulas.find(f => f.seq == seqEventoAula);
    this.modeloCrud = {
      seqDivisaoTurma: seqDivisaoTurma,
      seqEventoAula: seqEventoAula,
      seqHorarioAgd: eventoAula.seqHorarioAgd,
      descricaoFormatada: this.divisao.descricaoDivisaoFormatada,
      fimPeriodoLetivo: this.divisao.fimPeriodoLetivo,
      inicioPeriodoLetivo: this.divisao.inicioPeriodoLetivo,
      tipoDistribuicaoAula: this.divisao.tipoDistribuicaoAula,
      tipoPulaFeriado: this.divisao.tipoPulaFeriado,
      cargaHorariaGrade: this.divisao.cargaHorariaGrade,
      cargaHorariaLancada: this.divisao.cargaHorariaLancada,
      aulaSabado: this.divisao.aulaSabado,
      dscTabelaHorario: this.dataService.tabelaHorarioAgd.descricao,
      codigoRecorrencia: eventoAula.codigoRecorrencia,
      diaSemanaFormatada: eventoAula.diaSemanaFormatada,
      diaSemanaDescricao: eventoAula.diaSemanaDescricao,
      horaInicio: eventoAula.horaInicio,
      horaFim: eventoAula.horaFim,
      codigoLocalSEF: eventoAula.codigoLocalSEF,
      local: eventoAula.local,
      professores: eventoAula.colaboradores.map(m => m.descricaoFormatada),
      contemProfessores: eventoAula.colaboradores.some(s => s.seqColaborador),
    };

    if (isNullOrEmpty(this.modeloCrud.local)) {
      this.modeloCrud.local = 'Local não informado';
    }
  }

  inicializarModelo() {
    const modelo = this.fb.group({
      seqDivisaoTurma: [this.modeloCrud.seqDivisaoTurma],
      seqEventoAula: [this.modeloCrud.seqEventoAula],
      codigoRecorrencia: [this.modeloCrud.codigoRecorrencia],
      tipoExclusao: [environment.production ? '1' : '2', Validators.required],
      dataInicio: [''],
      dataFim: [''],
    });
    return modelo;
  }

  changeTipoExclusao(tipoExclusao) {
    if (tipoExclusao < 3) {
      this.esconderDataPeriodo = true;
      this.formExclusao.get('dataInicio').setValue('');
      this.formExclusao.get('dataFim').setValue('');
      this.dataObrigatoria = false;
    } else {
      this.esconderDataPeriodo = false;
      this.dataObrigatoria = true;
      this.formExclusao
        .get('dataFim')
        .setValue(moment(this.modeloCrud.fimPeriodoLetivo, 'DD/MM/YYYY').format('YYYY-MM-DD'));
    }
    this.changeDetection.detectChanges();
  }

  async onSubmit() {
    const divisaoTurma = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
      f => f.seq === this.modeloCrud.seqDivisaoTurma
    );

    divisaoTurma.eventoAulas = divisaoTurma.eventoAulas.filter(f => !this.seqsEventosExcluir.includes(f.seq));
    this.service.refresh.next();

    this.notificationService.information(this.mensagemInicialNotificacao);
    this.mensagemSucessoNotificacao += ', realizada com sucesso';
    this.fecharModal();
    try {
      await this.service.excluirEventos(this.seqsEventosExcluir);
      this.notificationService.success(this.mensagemSucessoNotificacao);
    } finally {
      this.service.atualizarEventos();
    }
  }

  formatarMensagemEventosExcluir() {
    this.seqsEventosExcluir = [];
    this.mensagemInicialNotificacao = `Iniciando exclusão do evento ${this.modeloCrud.diaSemanaFormatada} de ${this.modeloCrud.horaInicio} - ${this.modeloCrud.horaFim}`;
    this.mensagemSucessoNotificacao = `Exclusão do evento ${this.modeloCrud.diaSemanaFormatada} de ${this.modeloCrud.horaInicio} - ${this.modeloCrud.horaFim}`;
    const divisaoTurma = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
      f => f.seq === this.modeloCrud.seqDivisaoTurma
    );
    switch (this.formExclusao.value.tipoExclusao) {
      case '1':
        this.seqsEventosExcluir.push(this.formExclusao.value.seqEventoAula);
        this.mensagemAssert = `O agendamento de ${this.modeloCrud.diaSemanaFormatada} de ${this.modeloCrud.horaInicio} -
                                                ${this.modeloCrud.horaFim} será excluído.`;
        break;
      case '2':
        this.seqsEventosExcluir = divisaoTurma.eventoAulas
          .filter(f => f.seqHorarioAgd == this.modeloCrud.seqHorarioAgd)
          .map(m => m.seq);
        this.mensagemInicialNotificacao = `Iniciando a exclusão dos agendamentos recorrentes de ${this.modeloCrud.diaSemanaDescricao} de
                      ${this.modeloCrud.horaInicio} - ${this.modeloCrud.horaFim}`;
        this.mensagemSucessoNotificacao = `Exclusão dos agendamentos recorrentes de ${this.modeloCrud.diaSemanaDescricao} de
                            ${this.modeloCrud.horaInicio} - ${this.modeloCrud.horaFim}`;
        this.mensagemAssert = `Ao excluir todas as ocorrências, será(ão) removido(s) ${this.seqsEventosExcluir.length} agendamento(s),
                              correspondente(s) a ${this.modeloCrud.diaSemanaDescricao} de
                              ${this.modeloCrud.horaInicio} - ${this.modeloCrud.horaFim}.`;
        break;
      case '3':
        const dataInicio = moment(this.formExclusao.value.dataInicio, 'YYYY-MM-DD');
        const dataFim = moment(this.formExclusao.value.dataFim, 'YYYY-MM-DD');
        this.seqsEventosExcluir = divisaoTurma.eventoAulas
          .filter(f => f.seqHorarioAgd == this.modeloCrud.seqHorarioAgd)
          .filter(f => {
            const data = moment(f.data, 'YYYY-MM-DD');
            return data >= dataInicio && data <= dataFim;
          })
          .map(m => m.seq);
        this.mensagemInicialNotificacao = `Iniciando a exclusão do(s) agendamento(s) de
                                           ${this.modeloCrud.diaSemanaDescricao} de
                                           ${this.modeloCrud.horaInicio} - ${this.modeloCrud.horaFim}, no período de
                                           ${dataInicio.format('DD/MM/YYYY')} a ${dataFim.format('DD/MM/YYYY')}.`;
        this.mensagemSucessoNotificacao = `Exclusão do(s) agendamento(s) de ${this.modeloCrud.diaSemanaDescricao} de
                                          ${this.modeloCrud.horaInicio} - ${this.modeloCrud.horaFim}, no período de
                                          ${dataInicio.format('DD/MM/YYYY')} a ${dataFim.format('DD/MM/YYYY')}`;
        this.mensagemAssert = `Ao excluir a(s) ocorrência(s), de ${dataInicio.format('DD/MM/YYYY')} a
                              ${dataFim.format('DD/MM/YYYY')}, será(ão) excluído(s) ${this.seqsEventosExcluir.length}
                              agendamento(s), correspondente(s) a ${this.modeloCrud.diaSemanaDescricao}
                              de ${this.modeloCrud.horaInicio} - ${this.modeloCrud.horaFim}.`;
        break;
    }
    this.modalAssert.open();
    this.btNao.focus();
  }

  fecharModal() {
    this.router.navigate([{ outlets: { modais: null } }], { queryParamsHandling: 'preserve' });
    this.modal.close();
  }

  voltar() {
    this.modal.close();
    this.router.navigate(
      [{ outlets: { modais: ['Detalhes', this.modeloCrud.seqEventoAula, this.modeloCrud.seqDivisaoTurma] } }],
      { queryParamsHandling: 'preserve' }
    );
  }

  private validarMensagensInformativas() {
    this.mensagemInformativa = null;
    this.formExclusao.setErrors(null);
    if (this.formExclusao.pristine || this.formExclusao.invalid) {
      return;
    }
    if (this.recuperarQuantidadeEventosSelecionados() === 0) {
      this.mensagemInformativa = 'Nenhum evento será excluído.';
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      this.formExclusao.setErrors({ nenhumRegistro: true });
    } else if (this.validarSituacaoEventoAula()) {
      this.mensagemInformativa = `Alteração ou exclusão do horário de aula não permitida. Existem aulas que estão com a 
        situação de "apurada" ou "não executada" dentro do período informado.`;
      this.classeMensagemInformativa = 'smc-sga-mensagem-local-erro';
      this.formExclusao.setErrors({ situacaoAulaInvalida: true });
    }
    this.changeDetection.detectChanges();
  }

  private recuperarQuantidadeEventosSelecionados(): number {
    switch (this.formExclusao.controls.tipoExclusao.value) {
      case '1':
        return 1;
      case '2':
        return this.divisao.eventoAulas.filter(f => f.seqHorarioAgd == this.modeloCrud.seqHorarioAgd).length;
      case '3':
        const dataInicio = moment(this.formExclusao.value.dataInicio, 'YYYY-MM-DD');
        const dataFim = moment(this.formExclusao.value.dataFim, 'YYYY-MM-DD');
        return this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.modeloCrud.seqHorarioAgd)
          .filter(f => {
            const data = moment(f.data, 'YYYY-MM-DD');
            return data >= dataInicio && data <= dataFim;
          }).length;
    }
  }

  private validarSituacaoEventoAula(): boolean {
    let bloquear = false;
    switch (this.formExclusao.controls.tipoExclusao.value) {
      case '1':
        bloquear = false;
        break;
      case '2':
        bloquear = this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.modeloCrud.seqHorarioAgd)
          .some(s => s.situacaoApuracaoFrequencia === 'Executada' || s.situacaoApuracaoFrequencia === 'Não executada');
        break;
      case '3':
        const dataInicio = moment(this.formExclusao.value.dataInicio, 'YYYY-MM-DD');
        const dataFim = moment(this.formExclusao.value.dataFim, 'YYYY-MM-DD');
        bloquear = this.divisao.eventoAulas
          .filter(f => f.seqHorarioAgd == this.modeloCrud.seqHorarioAgd)
          .filter(f => {
            const data = moment(f.data, 'YYYY-MM-DD');
            return data >= dataInicio && data <= dataFim;
          })
          .some(s => s.situacaoApuracaoFrequencia === 'Executada' || s.situacaoApuracaoFrequencia === 'Não executada');
        break;
    }

    return bloquear;
  }
}
