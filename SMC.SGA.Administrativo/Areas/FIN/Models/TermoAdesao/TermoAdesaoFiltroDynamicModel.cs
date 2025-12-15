using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class TermoAdesaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid17_24, SMCSize.Grid11_24)]
        [SMCDescription]
        [SMCMaxLength(100)]
        public string Titulo { get; set; }


        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        [SMCRadioButtonList]
        public bool? Ativo { get; set; }
    }
}