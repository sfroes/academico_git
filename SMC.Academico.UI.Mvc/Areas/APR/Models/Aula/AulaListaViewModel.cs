using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class AulaListaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        public DateTime DataAula { get; set; }
    }
}