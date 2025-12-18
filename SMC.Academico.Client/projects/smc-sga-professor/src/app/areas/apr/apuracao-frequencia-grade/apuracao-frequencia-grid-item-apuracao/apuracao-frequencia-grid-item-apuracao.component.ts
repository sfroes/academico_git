import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import moment from 'moment';
import { SmcModalComponent } from 'projects/shared/components/smc-modal/smc-modal.component';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { Subscription } from 'rxjs';
import { ApuracaoFrequenciaApuracaoModel } from '../models/apuracao-frequencia-apuracao.model';
import { Frequencia, OcorrenciaFrequencia } from '../models/apuracao-frequencia-types.model';
import { ApuracaoFrequenciaDataService } from '../services/apuracao-frequencia-data.service';
import { ApuracaoFrequenciaService } from '../services/apuracao-frequencia.service';
import { ApuracaoFrequenciaAulaModel } from './../models/apuracao-frequencia-aula.model';
import { SmcButtonComponent } from 'projects/shared/components/smc-button/smc-button.component';

@Component({
  selector: 'sga-apuracao-frequencia-grid-item-apuracao',
  templateUrl: './apuracao-frequencia-grid-item-apuracao.component.html',
  standalone: false,
})
export class ApuracaoFrequenciaGridItemApuracaoComponent implements OnInit, OnDestroy {
  classe:
    | 'smc-btn-frequencia-branco'
    | 'smc-btn-frequencia-falta'
    | 'smc-btn-frequencia-presente'
    | 'smc-btn-frequencia-sancao'
    | 'smc-btn-frequencia-abono';
  aula: ApuracaoFrequenciaAulaModel;
  @Input() formApuracao: FormGroup;
  private presente: Frequencia = 'Presente';
  private ausente: Frequencia = 'Ausente';
  private abono: OcorrenciaFrequencia = 'Abono/Retificação';
  private sancao: OcorrenciaFrequencia = 'Sanção';
  private _subs: Subscription[] = [];
  private set subs(value: Subscription) {
    this._subs.push(value);
  }
  get frequencia() {
    return this.formApuracao.controls.frequencia;
  }
  get frequenciaValue() {
    return this.formApuracao.controls.frequencia.value;
  }
  get ocorrenciaFrequenciaValue() {
    return this.formApuracao.controls.ocorrenciaFrequencia.value;
  }
  get desativarButton() {
    return this.validarDesativarBotao();
  }
  get processando$() {
    return this.dataService.processando$;
  }
  get observacaoValue() {
    return this.formApuracao.controls.observacao.value;
  }
  get dataObservacaoValue() {
    return this.formApuracao.controls.dataObservacao.value;
  }
  get descricaoTipoMensagem() {
    return this.formApuracao.controls.descricaoTipoMensagem.value;
  }
  get tooltip(): string {
    return this.validarTooltipBotao();
  }
  get seqAlunoHistoricoCicloLetivo() {
    return this.formApuracao.value.seqAlunoHistoricoCicloLetivo;
  }
  get alunoFormado() {
    return this.dataService.model$.value.alunos.find(
      f => f.seqAlunoHistoricoCicloLetivo === this.seqAlunoHistoricoCicloLetivo
    ).alunoFormado;
  }
  get alunoHistoricoEscolar() {
    return this.dataService.model$.value.alunos.find(
      f => f.seqAlunoHistoricoCicloLetivo === this.seqAlunoHistoricoCicloLetivo
    ).alunoHistoricoEscolar;
  }
  @ViewChild('observacao') modalObservacao: SmcModalComponent;
  @ViewChild('modalAlunoHistorico') modalAlunoHistorico: SmcModalComponent;
  @ViewChild('btnAssertSalvarNao') btnAssertSalvarNao: SmcButtonComponent;

  constructor(private service: ApuracaoFrequenciaService, private dataService: ApuracaoFrequenciaDataService) {}

  ngOnInit(): void {
    const seqEventoAula = this.formApuracao.controls.seqEventoAula.value;
    this.aula = this.dataService.modelSnapshot.aulas.find(f => f.seqEventoAula === seqEventoAula);
    this.subs = this.frequencia.valueChanges.subscribe(v => {
      this.alterarClasse();
    });
    this.alterarClasse();
  }

  ngOnDestroy(): void {
    this._subs.forEach(f => f.unsubscribe());
  }

  /**
   * Altera a frequencia do aluno, caso tenha aluno tenha historico escolar
   * @param alunoHistoricoValiado Reposta do modal de confirmação
   * @returns
   */
  alterarFrequencia(alunoHistoricoValiado: boolean = false) {
    if (this.alunoHistoricoEscolar && !this.alunoFormado && !alunoHistoricoValiado) {
      this.modalAlunoHistorico.open();
      this.btnAssertSalvarNao.focus();
      return;
    } else if (alunoHistoricoValiado) {
      this.modalAlunoHistorico.close();
    }

    if (this.aula.situacaoApuracaoFrequencia === 'Não executada') {
      return;
    }

    if (this.ocorrenciaFrequenciaValue === this.sancao || this.ocorrenciaFrequenciaValue === this.abono) {
      this.modalObservacao.open();
      return;
    }

    if (!this.frequenciaValue || this.frequenciaValue === this.presente) {
      this.frequencia.setValue(this.ausente);
      this.frequencia.markAsDirty();
    } else if (this.frequenciaValue === this.ausente) {
      this.frequencia.setValue(this.presente);
      this.frequencia.markAsDirty();
    }

    this.service.atualizarSituacaoFrequenciaAula(this.aula, this.frequenciaValue);
  }

  private validarDesativarBotao(): boolean {
    if (this.ocorrenciaFrequenciaValue === this.sancao || this.ocorrenciaFrequenciaValue === this.abono) {
      return false;
    }

    if (this.service.validarColisaoProfessor(this.aula) || this.alunoFormado) {
      return true;
    }

    return !this.service.validarDataLimiteApuracao(this.aula);
  }

  private validarTooltipBotao() {
    if (this.ocorrenciaFrequenciaValue === this.sancao) {
      return 'Aluno sancionado';
    } else if (this.ocorrenciaFrequenciaValue === this.abono) {
      return 'Falta abonada/retificada';
    } else if (this.alunoFormado) {
      return 'Aluno Formado';
    }

    if (this.service.validarColisaoProfessor(this.aula)) {
      return 'Apurada por outro professor';
    } else if (this.aula.situacaoApuracaoFrequencia === 'Não executada') {
      return 'Aula não executada';
    } else if (this.desativarButton) {
      return 'Prazo de alteração expirado';
    } else if (isNullOrEmpty(this.frequenciaValue)) {
      return 'Não apurado';
    } else {
      return this.frequenciaValue;
    }
  }

  private alterarClasse() {
    if (this.frequenciaValue === this.ausente) {
      this.classe = 'smc-btn-frequencia-falta';
    } else if (this.frequenciaValue === this.presente) {
      this.classe = 'smc-btn-frequencia-presente';
    } else {
      this.classe = 'smc-btn-frequencia-branco';
    }

    if (this.ocorrenciaFrequenciaValue === this.sancao) {
      this.classe = 'smc-btn-frequencia-sancao';
    } else if (this.ocorrenciaFrequenciaValue === this.abono) {
      this.classe = 'smc-btn-frequencia-abono';
    }
  }
}
