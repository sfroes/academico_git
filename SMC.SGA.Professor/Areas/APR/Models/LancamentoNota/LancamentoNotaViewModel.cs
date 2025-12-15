using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class LancamentoNotaViewModel : SMCViewModelBase
    {
        public long SeqOrigemAvaliacao { get; set; }

        public string MateriaLecionada { get; set; }

        public decimal? TotalParcial { get; set; }

        public List<LancamentoNotaApuracaoViewModel> Apuracoes { get; set; }

        public List<long> SeqsApuracaoExculida { get; set; }
    }
}