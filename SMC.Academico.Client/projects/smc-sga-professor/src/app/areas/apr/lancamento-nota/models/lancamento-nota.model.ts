import { LancamentoNotaAlunoModel } from './lancamento-nota-aluno.model';
import { LancamentoNotaAvaliacaoModel } from './lancamento-nota-avaliacao.model';

export interface LancamentoNotaModel {
  seqOrigemAvaliacao: string;
  apuracaoFrequencia: boolean;
  apuracaoNota: boolean;
  origemAvaliacaoTurma: boolean;
  responsavelTurma: boolean;
  diarioFechado: boolean;
  descricaoOrigemAvaliacao: string;
  materiaLecionada: string;
  permiteAlunoSemNota: boolean;
  permiteMateriaLecionada: boolean;
  materiaLecionadaObrigatoria: boolean;
  alunos: LancamentoNotaAlunoModel[];
  avaliacoes: LancamentoNotaAvaliacaoModel[];
  materiaLecionadaCadastrada: boolean;
  descricoesDivisaoTurma: string[];
}
