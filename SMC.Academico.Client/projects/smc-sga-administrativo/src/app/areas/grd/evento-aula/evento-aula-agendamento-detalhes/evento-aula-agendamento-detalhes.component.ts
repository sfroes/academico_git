import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { EventoAulaModel } from '../../moldels/evento-aula.model';
import { SmcModalComponent } from '../../../../../../../shared/components/smc-modal/smc-modal.component';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { Router, ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { PoDialogService } from '@po-ui/ng-components';
import { EventoAulaService } from '../../services/evento-aula.service';
import { SmcKeyValueModel } from 'projects/shared/models/smc-key-value.model';

@Component({
  selector: 'sga-evento-aula-agendamento-detalhes',
  templateUrl: './evento-aula-agendamento-detalhes.component.html',
  styles: [],
  standalone: false,
})
export class EventoAulaAgendamentoDetalhesComponent implements OnInit, AfterViewInit {
  descricaoDivisaoFormatadaSeleciona: string = '';
  eventoAula: EventoAulaModel;
  eventoSemProfessor: boolean;
  // get somenteLeitura() {
  //   return this.dataService.dadosTurma?.eventoAulaTurmaCabecalho.somenteLeitura ?? false;
  // }
  get carregandoDataSource$() {
    return this.service.loadingDataSource;
  }
  get tokenSegurancaEditarHorario() {
    return this.dataService.tokensSeguranca.find(f => f.nome === 'alterarHorario')?.permitido !== true;
  }
  get tokenSegurancaEditarLocal() {
    return this.dataService.tokensSeguranca.find(f => f.nome === 'alterarLocal')?.permitido !== true;
  }
  get tokenSegurancaEditarColaborador() {
    return this.dataService.tokensSeguranca.find(f => f.nome === 'alterarColaborador')?.permitido !== true;
  }
  get tokenSegurancaExcluir() {
    return this.dataService.tokensSeguranca.find(f => f.nome === 'excluirEventoAula')?.permitido !== true;
  }
  @ViewChild(SmcModalComponent) modal: SmcModalComponent;

  constructor(
    private service: EventoAulaService,
    private dataService: EventoAulaDataService,
    private router: Router,
    private changeDetection: ChangeDetectorRef,
    private activatedRoute: ActivatedRoute,
    private dialog: PoDialogService
  ) {}

  ngAfterViewInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.abrirModalDivisaoTurma(params['seqEventoAula'] as string, params['seqDivisaoTurma'] as string);
      this.changeDetection.detectChanges();
    });
  }

  ngOnInit(): void {}

  abrirModalDivisaoTurma(seqEventoAula: string, seqDivisaoTurma: string) {
    if (!this.dataService.dadosTurma) {
      this.fecharModal();
      return;
    }

    const divisaoTurma = this.dataService.dadosTurma.eventoAulaDivisoesTurma.filter(f => f.seq == seqDivisaoTurma)[0];

    this.descricaoDivisaoFormatadaSeleciona = divisaoTurma.descricaoDivisaoFormatada;

    const eventoAula = divisaoTurma.eventoAulas.filter(f => f.seq == seqEventoAula)[0];

    this.eventoAula = { ...eventoAula };
    if (isNullOrEmpty(this.eventoAula.local)) {
      this.eventoAula.local = 'Local não informado';
    }

    this.eventoSemProfessor = this.eventoAula.colaboradores[0].descricaoFormatada === 'Professor(es) não informado(s)';

    this.modal.open();
  }

  abrirModalExcluir(seqDivisaoTurma: string, seqEventoAula: string) {
    this.router.navigate([{ outlets: { modais: ['Delete', seqEventoAula, seqDivisaoTurma] } }], {
      queryParamsHandling: 'preserve',
    });
  }

  abrirModalEditHorario(seqDivisaoTurma: string, seqEventoAula: string) {
    this.router.navigate([{ outlets: { modais: ['EditHorario', seqEventoAula, seqDivisaoTurma] } }], {
      queryParamsHandling: 'preserve',
    });
  }

  abrirModalEditColaborador(seqDivisaoTurma: string, seqEventoAula: string) {
    this.router.navigate([{ outlets: { modais: ['EditColaborador', seqEventoAula, seqDivisaoTurma] } }], {
      queryParamsHandling: 'preserve',
    });
  }

  abrirModalEditColaboradorSubstituto(seqDivisaoTurma: string, seqEventoAula: string) {
    this.router.navigate([{ outlets: { modais: ['EditColaboradorSubstituto', seqEventoAula, seqDivisaoTurma] } }], {
      queryParamsHandling: 'preserve',
    });
  }

  abrirModalEditLocal(seqDivisaoTurma: string, seqEventoAula: string) {
    this.router.navigate([{ outlets: { modais: ['EditLocal', seqEventoAula, seqDivisaoTurma] } }], {
      queryParamsHandling: 'preserve',
    });
  }

  abrirModalEdit(seqDivisaoTurma: string, seqEventoAula: string) {
    this.router.navigate([{ outlets: { modais: ['Edit', seqEventoAula, seqDivisaoTurma] } }], {
      queryParamsHandling: 'preserve',
    });
  }

  fecharModal() {
    this.router.navigate([{ outlets: { modais: null } }], { queryParamsHandling: 'preserve' });
    this.modal.close();
  }

  validarSomenteLeitura() {
    let bloquear = this.dataService.dadosTurma?.eventoAulaTurmaCabecalho.somenteLeitura;
    if (
      !bloquear &&
      this.eventoAula?.situacaoApuracaoFrequencia !== 'Não executada' &&
      this.eventoAula?.situacaoApuracaoFrequencia !== 'Executada'
    ) {
      bloquear = false;
    } else {
      bloquear = true;
    }
    return bloquear;
  }

  recuperarQuantidadeCompartilhamentos(): number {
    const temCompartilhamento = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
      f => f.seq === this.eventoAula.seqDivisaoTurma
    ).compartilhamentos;

    return temCompartilhamento.length;
  }

  validarCompartilhamento(): boolean {
    return this.recuperarQuantidadeCompartilhamentos() > 0;
  }

  recuperarCompartilhamento(): SmcKeyValueModel {
    const compartilhamentos = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
      f => f.seq === this.eventoAula.seqDivisaoTurma
    ).compartilhamentos;

    return compartilhamentos[0];
  }

  recuperarCompartilhamentosDivisao(): SmcKeyValueModel[] {
    const compartilhamentos = this.dataService.dadosTurma.eventoAulaDivisoesTurma.find(
      f => f.seq === this.eventoAula.seqDivisaoTurma
    ).compartilhamentos;

    return compartilhamentos;
  }

  navegarGradeCompartilhada(seqTurma: string): void {
    this.router.navigate(['/GRD/EventoAula/Turma/', seqTurma, { outlets: { modais: null } }]);
    this.modal.close();
  }
}
