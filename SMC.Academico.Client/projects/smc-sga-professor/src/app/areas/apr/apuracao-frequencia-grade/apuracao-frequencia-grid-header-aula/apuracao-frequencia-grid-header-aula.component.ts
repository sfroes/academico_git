import { AfterViewChecked, ChangeDetectorRef, Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { ApuracaoFrequenciaAulaModel } from '../models/apuracao-frequencia-aula.model';
import { Frequencia } from '../models/apuracao-frequencia-types.model';
import { ApuracaoFrequenciaDataService } from '../services/apuracao-frequencia-data.service';
import { ApuracaoFrequenciaService } from '../services/apuracao-frequencia.service';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { SmcModalComponent } from 'projects/shared/components/smc-modal/smc-modal.component';
import { SmcButtonComponent } from 'projects/shared/components/smc-button/smc-button.component';

@Component({
  selector: 'sga-apuracao-frequencia-grid-header-aula',
  templateUrl: './apuracao-frequencia-grid-header-aula.component.html',
  standalone: false,
})
export class ApuracaoFrequenciaGridHeaderAulaComponent implements AfterViewChecked, OnInit {
  toggleMenu = false;
  ausente: Frequencia = 'Ausente';
  presente: Frequencia = 'Presente';
  frequenciaSelecionadaAlunoHistorico: Frequencia;
  hint: string = '';
  @Input() aula: ApuracaoFrequenciaAulaModel;
  get aulaAtiva(): boolean {
    return this.service.validarDataLimiteApuracao(this.aula);
  }
  private get form() {
    return this.dataService.form;
  }
  get aulaEmCancelamento(): boolean {
    return this.aula.situacaoApuracaoFrequencia === 'Em cancelamento';
  }
  get alunoFormado() {
    const qtdAlunosFormados = this.dataService.model$.value.alunos.filter(f => f.alunoFormado);
    return qtdAlunosFormados.length > 0;
  }
  get alunoHistoricoEscolar() {
    const qtdAlunoHistoricoEscolar = this.dataService.model$.value.alunos.filter(f => f.alunoHistoricoEscolar);
    return qtdAlunoHistoricoEscolar.length > 0;
  }
  @ViewChild('modalAlunoHistorico') modalAlunoHistorico: SmcModalComponent;
  @ViewChild('btnAssertSalvarNao') btnAssertSalvarNao: SmcButtonComponent;

  constructor(
    private service: ApuracaoFrequenciaService,
    private dataService: ApuracaoFrequenciaDataService,
    private changeDetection: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.hint = this.aula.descricaoFormatada;
  }

  ngAfterViewChecked(): void {
    this.changeDetection.detectChanges();
  }

  alterarFrequencia(frequencia?: Frequencia, alunoHistoricoValiado: boolean = false) {
    if (this.alunoHistoricoEscolar && !this.alunoFormado && !alunoHistoricoValiado && frequencia !== null) {
      this.frequenciaSelecionadaAlunoHistorico = frequencia;
      this.modalAlunoHistorico.open();
      this.btnAssertSalvarNao.focus();
      return;
    } else if (alunoHistoricoValiado) {
      this.modalAlunoHistorico.close();
    }

    if (this.aula.situacaoApuracaoFrequencia === 'Não executada') {
      return;
    }
    this.service.atualizarSituacaoFrequenciaAula(this.aula, frequencia);
    const alunos = this.form.controls.alunos as FormArray;
    alunos.controls.forEach(f => {
      const aluno = f as FormGroup;
      const alunoFormado = this.dataService.model$.value.alunos.find(
        f => f.seqAlunoHistoricoCicloLetivo === aluno.value.seqAlunoHistoricoCicloLetivo
      ).alunoFormado;
      const apuracoes = aluno.controls.apuracoes as FormArray;
      apuracoes.controls.forEach(fa => {
        const apuracao = fa as FormGroup;
        if (
          this.aula.seqEventoAula == apuracao.controls.seqEventoAula.value &&
          isNullOrEmpty(apuracao.controls.ocorrenciaFrequencia.value) &&
          !alunoFormado
        ) {
          apuracao.controls.frequencia.setValue(frequencia);
          apuracao.controls.frequencia.markAsDirty();
        }
      });
    });
  }

  validarLimparTodos(aula: ApuracaoFrequenciaAulaModel): boolean {
    let retorno = false;
    if (
      aula.situacaoApuracaoFrequenciaOriginal !== 'Executada' &&
      aula.situacaoApuracaoFrequencia !== 'Não executada'
    ) {
      retorno = true;
    }
    return retorno;
  }

  colisaoProfessor() {
    let retorno = true;
    if (this.service.validarColisaoProfessor(this.aula)) {
      retorno = false;
      this.hint = 'Apurada por outro professor';
    }
    return retorno;
  }
}
