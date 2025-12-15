using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoAvaliacaoAlunoApuracaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public decimal? Nota { get; set; }

        public bool Comparecimento { get; set; }

        public string ComentarioApuracao { get; set; }
        public bool AlunoComComponenteOutroHistorico { get; set; }
        public bool PermitirAlunoEntregarOnlinePosPrazo { get; set; }
    }
}
