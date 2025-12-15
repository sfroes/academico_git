using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoHierarquiaEntidadeFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        public long Seq { get; set; }

        [SMCOrder(1)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }
    }
}