using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class LancamentoNotaApuracaoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public decimal? Nota { get; set; }

        public bool Comparecimento { get; set; }

        public string ComentarioApuracao { get; set; }
    }
}