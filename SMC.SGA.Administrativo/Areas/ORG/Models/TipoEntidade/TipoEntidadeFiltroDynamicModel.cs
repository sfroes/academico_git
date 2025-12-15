using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoEntidadeFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        [SMCFilter(true, true)]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid13_24)]
        [SMCFilter(true, true)]
        public string Descricao { get; set; }
    }
}