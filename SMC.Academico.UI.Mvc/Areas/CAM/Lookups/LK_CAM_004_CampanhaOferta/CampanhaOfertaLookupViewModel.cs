using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CampanhaOfertaLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCCssClass("smc-size-md-23 smc-size-xs-23 smc-size-sm-23 smc-size-lg-23")]
        public string Descricao { get; set; }
    }
}