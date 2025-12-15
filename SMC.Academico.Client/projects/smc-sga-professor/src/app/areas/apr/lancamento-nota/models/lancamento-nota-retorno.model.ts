import { LancamentoNotaRetornoApuracaoModel } from './lancamento-nota-retorno-apuracao';

export interface LancamentoNotaRetornoModel {
  seqOrigemAvaliacao: string;
  materiaLecionada: string;
  apuracoes: LancamentoNotaRetornoApuracaoModel[];
  seqsApuracaoExculida: string[];

  totalParcial?: number;
}
