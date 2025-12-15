using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class TipoTrabalhoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCOrder(0)]
        [SMCFilter]
        [SMCSize(Framework.SMCSize.Grid3_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCOrder(1)]
        [SMCFilter]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid20_24, Framework.SMCSize.Grid13_24)]
        public string Descricao { get; set; }
    }
}