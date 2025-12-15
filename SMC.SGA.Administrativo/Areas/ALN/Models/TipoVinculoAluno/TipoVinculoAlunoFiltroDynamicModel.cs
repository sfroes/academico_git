using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TipoVinculoAlunoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter]
        [SMCKey]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]        
        public long Seq { get; set; }

        [SMCFilter]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid18_24, SMCSize.Grid18_24, SMCSize.Grid13_24)]
        public string Descricao { get; set; }
    }
}