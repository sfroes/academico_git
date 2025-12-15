import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl, FormArray } from '@angular/forms';
import { LancamentoNotaAvaliacaoModel } from '../models/lancamento-nota-avaliacao.model';
import { LancamentoNotaAlunoApuracaoModel } from '../models/lancamento-nota-aluno-apuracao.model';
import { LancamentoNotaModel } from '../models/lancamento-nota.model';
import { LancamentoNotaRetornoApuracaoModel } from '../models/lancamento-nota-retorno-apuracao';
import { LancamentoNotaRetornoModel } from '../models/lancamento-nota-retorno.model';
import { isNullOrEmpty } from 'projects/shared/utils/util';
import { LancamentoNotaDiario } from '../models/lancamento-nota-fechamento-diario';
import { LancamentoNotaDiarioAluno } from '../models/lancamento-nota-diario-aluno';
import { smcMax } from 'projects/shared/validators/smc-validator.directive';

@Injectable({
  providedIn: 'root',
})
export class LancamentoNotaMapService {
  constructor(private formBuilder: FormBuilder) {}

  mapearForm(model: LancamentoNotaModel): FormGroup {
    return this.formBuilder.group({
      seqOrigemAvaliacao: [model.seqOrigemAvaliacao],
      materiaLecionada: [model.materiaLecionada],
      alunos: this.mapearFormAlunos(model),
    });
  }

  mapearFormAlunos(model: LancamentoNotaModel) {
    const arrayAluno = this.formBuilder.array([]);
    model.alunos.forEach(aluno => {
      const groupAluno = this.formBuilder.group({
        seqAlunoHistorico: [aluno.seqAlunoHistorico],
        observacao: [aluno.observacao],
        situacaoFinal: [],
        apuracoes: this.mapearFormApuracoes(aluno.apuracoes, model.avaliacoes),
      });
      arrayAluno.push(groupAluno);
    });
    return arrayAluno;
  }

  mapearFormApuracoes(apuracoes: LancamentoNotaAlunoApuracaoModel[], avaliacoes: LancamentoNotaAvaliacaoModel[]) {
    const apuracoesForm = this.formBuilder.array([]);
    apuracoes.forEach(apuracao => {
      const avaliacao = avaliacoes.filter(f => f.seqAplicacaoAvaliacao === apuracao.seqAplicacaoAvaliacao)[0];
      const controle = this.formBuilder.group({
        seq: [apuracao.seq],
        seqAplicacaoAvaliacao: [apuracao.seqAplicacaoAvaliacao],
        nota: [apuracao.comparecimento ? apuracao.nota ?? '' : '*', [Validators.min(0), smcMax(avaliacao.valor)]],
        comentarioApuracao: [apuracao.comentarioApuracao ?? ''],
      });
      apuracoesForm.push(controle);
      apuracao.entregaWeb = avaliacao.entregaWeb;
    });
    return apuracoesForm;
  }

  mapearAvaliacoesOptions(avaliacoes: LancamentoNotaAvaliacaoModel[]) {
    return avaliacoes.map(m => {
      return {
        value: m.seqAplicacaoAvaliacao.toString(),
        label: m.descricaoFormatada,
      };
    });
  }

  mapearFormObservacao(
    apuracoes: LancamentoNotaAlunoApuracaoModel[] = [],
    avaliacoes: LancamentoNotaAvaliacaoModel[] = []
  ) {
    const form = this.formBuilder.group({
      apuracoes: this.mapearFormApuracoes(apuracoes, avaliacoes),
    });
    return form;
  }

  mapearRetornoModelo(
    model: LancamentoNotaModel,
    formNotas: AbstractControl,
    seqAlunoHistorico?: string
  ): LancamentoNotaRetornoModel {
    const modelo: LancamentoNotaRetornoModel = {
      seqOrigemAvaliacao: formNotas.value.seqOrigemAvaliacao,
      materiaLecionada: formNotas.value.materiaLecionada,
      apuracoes: this.mapearRetornoApuracoesAlteradas(model, formNotas, seqAlunoHistorico),
      seqsApuracaoExculida: this.mapearRetornoApuracoesExcluidos(model, formNotas, seqAlunoHistorico),
    };
    return modelo;
  }

  mapearRetornoMateriaLecionada(formNotas: AbstractControl): LancamentoNotaRetornoModel {
    const modelo: LancamentoNotaRetornoModel = {
      seqOrigemAvaliacao: formNotas.value.seqOrigemAvaliacao,
      materiaLecionada: formNotas.value.materiaLecionada,
      apuracoes: null,
      seqsApuracaoExculida: null,
    };
    return modelo;
  }

  private mapearRetornoApuracoesAlteradas(
    model: LancamentoNotaModel,
    formNotas: AbstractControl,
    seqAlunoHistorico?: string
  ): LancamentoNotaRetornoApuracaoModel[] {
    const apuracoes = [];
    for (let a = 0; a < model.alunos.length; a++) {
      const alunoModel = model.alunos[a];
      if (!isNullOrEmpty(seqAlunoHistorico) && alunoModel.seqAlunoHistorico !== seqAlunoHistorico) {
        continue;
      }
      for (let n = 0; n < alunoModel.apuracoes.length; n++) {
        const apuracaoModel = alunoModel.apuracoes[n];
        const apuracaoForm = formNotas.get(`alunos.${a}.apuracoes.${n}`);
        if (this.validarApuracaoModificadaComValor(apuracaoModel, apuracaoForm)) {
          apuracoes.push(this.mapearRetornoApuracao(apuracaoForm, alunoModel.seqAlunoHistorico));
        }
      }
    }
    return apuracoes;
  }

  validarApuracaoModificadaComValor(apuracaoModel: LancamentoNotaAlunoApuracaoModel, apuracaoForm: AbstractControl) {
    if (apuracaoForm.pristine) {
      return false;
    }
    const nota = apuracaoForm.value.nota;
    if (isNullOrEmpty(nota)) {
      return false;
    }

    const comentarioApuracao = apuracaoForm.value.comentarioApuracao;
    return apuracaoModel.nota !== nota || apuracaoModel.comentarioApuracao !== comentarioApuracao;
  }

  private mapearRetornoApuracoesExcluidos(
    model: LancamentoNotaModel,
    formNotas: AbstractControl,
    seqAlunoHistorico?: string
  ): string[] {
    const apuracoes: string[] = [];
    for (let a = 0; a < model.alunos.length; a++) {
      const alunoModel = model.alunos[a];
      if (!isNullOrEmpty(seqAlunoHistorico) && alunoModel.seqAlunoHistorico !== seqAlunoHistorico) {
        continue;
      }
      for (let n = 0; n < alunoModel.apuracoes.length; n++) {
        const apuracaoForm = formNotas.get(`alunos.${a}.apuracoes.${n}`);
        if (!isNullOrEmpty(alunoModel.apuracoes[n].nota) && this.validarApuracaoModificadaSemValor(apuracaoForm)) {
          apuracoes.push(apuracaoForm.get('seq').value);
        }
      }
    }
    return apuracoes;
  }

  validarApuracaoModificadaSemValor(apuracaoForm: AbstractControl) {
    if (apuracaoForm.pristine) {
      return false;
    }
    const nota = apuracaoForm.value.nota;
    const seq = apuracaoForm.value.seq;
    return seq !== 0 && nota === '';
  }

  mapearRetornoApuracao(apuracaoForm: AbstractControl, seqAlunoHistorico: string): LancamentoNotaRetornoApuracaoModel {
    const fromValue = apuracaoForm.value;
    return {
      seq: fromValue.seq,
      seqAlunoHistorico,
      seqAplicacaoAvaliacao: fromValue.seqAplicacaoAvaliacao,
      nota: fromValue.nota === '*' ? null : fromValue.nota,
      comparecimento: fromValue.nota !== '*',
      comentarioApuracao: fromValue.comentarioApuracao,
    };
  }

  mapearRetornoAtualizacaoHistorico(model: LancamentoNotaModel, formNotas: AbstractControl, indexAluno: number) {
    const modelo = this.mapearRetornoFechamentoDiario(model, formNotas);
    modelo.fechamentoIndividual = true;
    modelo.alunos = [modelo.alunos[indexAluno]];
    return modelo;
  }

  mapearModeloCalculoTotal(
    seqOrigemAvaliacao: string,
    seqAlunoHistorico: string,
    totalParcial: number,
    formApuacoes: AbstractControl
  ): LancamentoNotaRetornoModel {
    const modelo: LancamentoNotaRetornoModel = {
      seqOrigemAvaliacao,
      materiaLecionada: '',
      apuracoes: this.mapearModeloApuracoesCalculoTotal(formApuacoes as FormArray, seqAlunoHistorico),
      totalParcial,
      seqsApuracaoExculida: [],
    };
    return modelo;
  }

  mapearModeloApuracoesCalculoTotal(
    formApuacoes: FormArray,
    seqAlunoHistorico: string
  ): LancamentoNotaRetornoApuracaoModel[] {
    const apuracoes = [];
    for (let n = 0; n < formApuacoes.length; n++) {
      apuracoes.push(this.mapearApuracaoCalculoTotal(formApuacoes.controls[n], seqAlunoHistorico));
    }
    return apuracoes;
  }

  mapearApuracaoCalculoTotal(apuracaoForm: AbstractControl, seqAlunoHistorico: string): any {
    const formValue = apuracaoForm.value;
    return {
      seq: formValue.seq,
      seqAlunoHistorico,
      seqAplicacaoAvaliacao: formValue.seqAplicacaoAvaliacao,
      nota: formValue.nota === '*' ? null : formValue.nota,
      comparecimento: formValue.nota !== '*',
      comentarioApuracao: formValue.comentarioApuracao,
    };
  }

  mapearRetornoFechamentoDiario(model: LancamentoNotaModel, formNotas: AbstractControl) {
    const modelo: LancamentoNotaDiario = {
      seqOrigemAvaliacao: formNotas.value.seqOrigemAvaliacao,
      materiaLecionada: formNotas.value.materiaLecionada,
      alunos: this.mapearRetornoFechamentoDiarioAluno(model, formNotas),
      fechamentoIndividual: false,
    };
    return modelo;
  }

  mapearRetornoFechamentoDiarioAluno(model: LancamentoNotaModel, formNotas: AbstractControl) {
    const modelo: LancamentoNotaDiarioAluno[] = [];
    const formAlunos = formNotas.value.alunos;
    formAlunos.forEach((formAluno, index) => {
      modelo.push({
        seqAlunoHistorico: formAluno.seqAlunoHistorico,
        nota: model.alunos[index].total,
        faltas: model.alunos[index].faltas,
        situacaoHistoricoEscolar: model.alunos[index].situacaoFinal,
      });
    });
    return modelo;
  }
}
