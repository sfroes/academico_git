import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SafeHtml } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { PoModalComponent } from '@po-ui/ng-components';
import { SmcModalComponent } from 'projects/shared/components/smc-modal/smc-modal.component';
import { SmcLoadService } from 'projects/shared/services/load/smc-load.service';
import { SmcNotificationService } from 'projects/shared/services/notification/smc-notification.service';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { BehaviorSubject } from 'rxjs';
import { EventoAulaTurmaCabecalhoAssociarProfessorModel } from '../../moldels/evento-aula-turma-cabecalho-associar-professor.model';
import { EventoAulaDataService } from '../../services/evento-aula-data.service';
import { EventoAulaService } from '../../services/evento-aula.service';

@Component({
  selector: 'sga-evento-aula-agendamento-associar-professor',
  templateUrl: './evento-aula-agendamento-associar-professor.component.html',
  styles: [],
})
export class EventoAulaAgendamentoAssociarProfessorComponent implements AfterViewInit {
  dadosTurmaCabecalhoAssociarProfessor: EventoAulaTurmaCabecalhoAssociarProfessorModel = {} as any;
  seqTurma: string = '';
  htmlDetalhe: SafeHtml = '';
  formAssociar: FormGroup;
  validacaoAssociacaoProfessor$ = new BehaviorSubject(false);
  mensagemItensMasterDetail = 'Não existem itens cadastrados';
  @ViewChild(SmcModalComponent) modal: SmcModalComponent;

  constructor(
    private fb: FormBuilder,
    private service: EventoAulaService,
    private dataService: EventoAulaDataService,
    private changeDetection: ChangeDetectorRef,
    private router: Router,
    private smcLoadService: SmcLoadService,
    private notificationService: SmcNotificationService
  ) {}

  ngAfterViewInit(): void {
    this.abrirModal();
  }

  abrirModal() {
    if (!this.dataService.dadosCabecalhoAssociarProfessor) {
      this.fecharModal();
      return;
    }

    this.dadosTurmaCabecalhoAssociarProfessor = this.dataService.dadosCabecalhoAssociarProfessor;
    this.seqTurma = this.dataService.dadosTurma.eventoAulaTurmaCabecalho.seqTurma;

    this.formAssociar = this.inicialiazarModelo();

    this.modal.open();
    this.changeDetection.detectChanges();
  }

  fecharModal() {
    this.router.navigate([{ outlets: { modais: null } }], { queryParamsHandling: 'preserve' });
    this.modal.close();
  }

  async abrirModalVisualisarComponenteCurricular(
    seqComponenteCurricular: string,
    modalVisualizacaoCurricular: PoModalComponent
  ) {
    this.smcLoadService.startLoading();
    try {
      this.htmlDetalhe = await this.service.buscarDetalheComponenteCurricular(seqComponenteCurricular);
      modalVisualizacaoCurricular.open();
    } finally {
      this.smcLoadService.endLoading();
    }
  }

  inicialiazarModelo() {
    const modelo = this.fb.group({
      seqTurma: [this.seqTurma],
      colaborador: this.inicialTurmaColaborador(),
    });

    return modelo;
  }

  inicialTurmaColaborador() {
    const colaboradores = this.fb.array([]);
    if (this.dadosTurmaCabecalhoAssociarProfessor.colaboradoresTurma) {
      this.dadosTurmaCabecalhoAssociarProfessor.colaboradoresTurma.colaborador.forEach(prof => {
        const groupProf = this.fb.group({
          seq: [prof],
        });
        colaboradores.push(groupProf as any);
      });
    }
    return colaboradores;
  }

  addProfessor() {
    const control = this.formAssociar.get('colaborador') as FormArray;
    const groupProf = this.fb.group({
      seq: ['', Validators.required],
    });
    control.push(groupProf as any);
  }

  getProfessores() {
    const professores = this.formAssociar.controls.colaborador as FormArray;
    const professoresBanco = this.dadosTurmaCabecalhoAssociarProfessor.colaboradoresTurma.colaborador.length;
    //valida já para o submit vir desabilitado
    if (professores.length === 0 && professoresBanco === 0) {
      this.formAssociar.markAsPristine();
    } else if (professores.length === 0 && professoresBanco > 0) {
      this.mensagemItensMasterDetail = 'Todos os professores foram removidos';
    }
    return professores.controls;
  }

  removeProfessor(indexProfessor) {
    const control = this.formAssociar.get('colaborador') as FormArray;
    control.removeAt(indexProfessor);
    control.markAsDirty();
  }

  async onSubmit() {
    try {
      const daddosProfessores = {
        seqTurma: this.seqTurma,
        Colaborador:
          this.formAssociar.value.colaborador
            .map(m => ({
              seq: m.seq,
            }))
            .filter(f => !isNullOrEmpty(f.seq)) ?? [],
      };
      this.validacaoAssociacaoProfessor$.next(true);
      this.notificationService.information(`Iniciando a associação do(s) professsor(es).`);
      await this.service.AssociarProfessorResponsavel(daddosProfessores);
      await this.preencherDadosProfessoresResponsaveis();
      this.notificationService.success(`Gravação da associação do(s) professsor(es). Concluída com sucesso.`);
      this.validacaoAssociacaoProfessor$.next(false);
      this.fecharModal();
    } catch (error) {}
  }

  async preencherDadosProfessoresResponsaveis() {
    await this.service.preencherDataSourceColaboradoresTurma();
  }
}
