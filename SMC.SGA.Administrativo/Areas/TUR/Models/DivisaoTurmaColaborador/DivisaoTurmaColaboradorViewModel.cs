using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class DivisaoTurmaColaboradorViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCParameter]
        public long SeqTurma { get; set; }

        public List<DivisaoTurmaColaboradorListaViewModel> Divisoes { get; set; } 
        
    }
}