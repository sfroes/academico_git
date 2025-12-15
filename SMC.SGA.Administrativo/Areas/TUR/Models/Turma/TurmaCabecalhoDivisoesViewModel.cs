using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaCabecalhoDivisoesViewModel : SMCViewModelBase
    {
        public long SeqDivisao { get; set; }

        public string DivisaoComponenteDescricao { get; set; }

        public List<DivisaoTurmaColaboradorDivisoesViewModel> DetalhesDivisao { get; set; }
    }
}