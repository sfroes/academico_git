using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class LancamentoAvaliacaoAlunoApuracaoVO : ISMCMappable
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
