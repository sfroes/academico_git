using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoConfiguracaoGrupoCurricularFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid9_24)]
        public string Descricao { get; set; }
    }
}