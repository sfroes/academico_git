using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TipoTurmaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCFilter]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public long Seq { get; set; }


        [SMCMaxLength(100)]
        [SMCFilter]
        [SMCDescription]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid14_24)]
        public string Descricao { get; set; }
    }
}