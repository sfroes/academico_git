import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { PoModalAction, PoModalComponent } from '@po-ui/ng-components';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { LancamentoNotaService } from '../../services/lancamento-nota.service';
import { LancamentoNotaAlunoApuracaoModel } from '../models/lancamento-nota-aluno-apuracao.model';
import { LancamentoNotaAvaliacaoModel } from '../models/lancamento-nota-avaliacao.model';
import { LancamentoNotaMapService } from '../services/lancamento-nota-map.service';

@Component({
  selector: 'app-lancamento-nota-observacao',
  templateUrl: './lancamento-nota-observacao.component.html',
  standalone: false,
})
export class LancamentoNotaObservacaoComponent implements OnInit {
  @Input() diarioFechado: boolean;
  @Input() descricaoOrigemAvaliacao: string;
  @Input() avaliacoes: LancamentoNotaAvaliacaoModel[];
  @Output() alunoAlterado = new EventEmitter<LancamentoNotaAlunoApuracaoModel[]>();
  @ViewChild(PoModalComponent, { static: true }) modalAlunoObservacao: PoModalComponent;

  nomeAluno: string;
  formado: boolean;
  apuracaoes: LancamentoNotaAlunoApuracaoModel[];
  formObservacao: FormGroup;
  seqAlunoHistorico: string;

  constructor(
    private lancamentoNotaMapService: LancamentoNotaMapService,
    private lancamentoNotaService: LancamentoNotaService
  ) { }

  acaoFechar: PoModalAction = {
    action: () => {
      this.fechar();
    },
    label: 'Cancelar',
    danger: true,
  };

  acaoSalvar: PoModalAction = {
    action: () => {
      this.salvar();
    },
    label: 'Ok',
    disabled: true,
  };

  ngOnInit(): void {
    this.formObservacao = this.lancamentoNotaMapService.mapearFormObservacao();
  }

  abrirModal(
    seqAlunoHistorico: string,
    nomeAluno: string,
    formado: boolean,
    apuracoes: LancamentoNotaAlunoApuracaoModel[]
  ) {
    this.nomeAluno = nomeAluno;
    this.formado = formado;
    this.seqAlunoHistorico = seqAlunoHistorico;
    this.apuracaoes = apuracoes;
    this.formObservacao = this.createForm();
    this.modalAlunoObservacao.open();
  }

  validarNotaObrigatoria(indexApuracao: number) {
    const comentario = this.formObservacao.value.apuracoes[indexApuracao].comentarioApuracao;
    return !isNullOrEmpty(comentario);
  }

  salvar() {
    const apuracoesModificadas: LancamentoNotaAlunoApuracaoModel[] = [];
    const apuracoesForm = this.formObservacao.controls.apuracoes as FormArray;
    apuracoesForm.controls.forEach((apuracaoForm, indexApuracao) => {
      if (this.lancamentoNotaService.validarApuracaoModificada(this.apuracaoes[indexApuracao], apuracaoForm)) {
        const model = apuracaoForm.value as LancamentoNotaAlunoApuracaoModel;
        model.seqAlunoHistorico = this.seqAlunoHistorico;
        apuracoesModificadas.push(model);
      }
    });
    this.alunoAlterado.emit(apuracoesModificadas);
    this.fechar();
  }

  fechar() {
    this.formObservacao.reset();
    this.modalAlunoObservacao.close();
  }

  private createForm() {
    const form = this.lancamentoNotaMapService.mapearFormObservacao(this.apuracaoes, this.avaliacoes);
    form.statusChanges.subscribe(() => {
      this.acaoSalvar.disabled =
        this.formObservacao.pristine ||
        this.formObservacao.invalid ||
        this.formado ||
        this.diarioFechado;
    });
    this.ajusteNotaObrigatoria(form);
    this.acaoSalvar.disabled = true;
    return form;
  }

  private ajusteNotaObrigatoria(form: AbstractControl) {
    // tslint:disable-next-line: forin
    for (const indexApuracao in this.apuracaoes) {
      const comentario = form.get(`apuracoes.${indexApuracao}.comentarioApuracao`);
      comentario.valueChanges.subscribe(() => {
        const nota = form.get(`apuracoes.${indexApuracao}.nota`);
        if (!nota.value) {
          nota.markAsDirty();
        }
      });
    }

  }
}
