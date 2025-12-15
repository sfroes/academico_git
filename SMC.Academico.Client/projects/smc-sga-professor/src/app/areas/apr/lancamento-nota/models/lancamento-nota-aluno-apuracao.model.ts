export interface LancamentoNotaAlunoApuracaoModel {
  seq: string;
  seqAplicacaoAvaliacao: string;
  nota?: string;
  comparecimento?: boolean;
  comentarioApuracao: string;
  alunoComComponenteOutroHistorico: boolean;
  permitirAlunoEntregarOnlinePosPrazo: boolean;

  seqAlunoHistorico?: string;
  entregaWeb?: boolean;
}
