export interface LancamentoNotaAvaliacaoModel {
  seqAplicacaoAvaliacao: string;
  sigla: string;
  descricao: string;
  valor: number;
  entregaWeb: boolean;
  mostrar: boolean;
  descricaoFormatada: string;
  alunoComComponenteOutroHistorico: boolean;
  permitirAlunoEntregarOnlinePosPrazo: boolean;
}
