export interface LancamentoNotaAlunoTotaisTurmaModel {
  seqAlunoHistorico: string;
  totalParcial: string;
  totalFinal?: number;
  situacaoFinal?: number;
  descricaoSituacaoFinal?: string;
  todasApuracoesDivisaoLancadas?: boolean;
  todasApuracoesVazias?: boolean;
}
