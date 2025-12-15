using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class HierarquiaEntidadeLookupPrepareFilter : ISMCFilter<HierarquiaEntidadeLookupFiltroViewModel>
    {
        public HierarquiaEntidadeLookupFiltroViewModel Filter(SMCControllerBase controllerBase, HierarquiaEntidadeLookupFiltroViewModel filter)
        {
            return filter;
        }
    }
}
