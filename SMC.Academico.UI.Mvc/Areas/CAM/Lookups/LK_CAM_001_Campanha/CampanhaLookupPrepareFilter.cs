using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CampanhaLookupPrepareFilter : ISMCFilter<CampanhaLookupFiltroViewModel>
    {
        public CampanhaLookupFiltroViewModel Filter(SMCControllerBase controllerBase, CampanhaLookupFiltroViewModel filter)
        {
            var service = controllerBase.Create<IEntidadeService>();
            filter.UnidadesResponsaveis = service.BuscarUnidadesResponsaveisGPILocalSelect();

            return filter;
        }
    }
}