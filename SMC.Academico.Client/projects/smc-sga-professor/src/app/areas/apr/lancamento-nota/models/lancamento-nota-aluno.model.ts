import { LancamentoNotaAlunoApuracaoModel } from './lancamento-nota-aluno-apuracao.model';

export interface LancamentoNotaAlunoModel {
  seqAlunoHistorico: string;
  numeroRegistroAcademico: string;
  formado: boolean;
  alunoAprovado: boolean;
  alunoDispensado: boolean;
  nome: string;
  totalParcial: string;
  apuracoes: LancamentoNotaAlunoApuracaoModel[];
  faltas: number;
  total: number;
  situacaoFinal: number;
  descricaoSituacaoFinal: string;
  observacao: string;
  temHistorico: boolean;
  situacaoHistorico: number;
  descricaoSituacaoHistorico: string;
  totalHistorico: number;
  mostrar: boolean;
  totalDivisaoTurmaCalculado: boolean;
  totalCalculado: boolean;
  situacaoFinalCalculada: boolean;
  processandoAtualizacaoHistorico: boolean;
  todasApuracoesDivisaoLancadas?: boolean;
  todasApuracoesVazias?: boolean;
}
