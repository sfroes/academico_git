using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class LancamentoNotaFechamentoDiarioViewModel : SMCViewModelBase
    {
        public long SeqOrigemAvaliacao { get; set; }

        public string MateriaLecionada { get; set; }

        public bool FechamentoIndividual { get; set; }

        public List<LancamentoNotaFechamentoDiarioAlunoViewModel> Alunos { get; set; }
    }
}