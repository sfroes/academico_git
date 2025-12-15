using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CicloLetivoLookupPrepareFilter : ISMCFilter<CicloLetivoLookupFiltroViewModel>
    {
        public CicloLetivoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, CicloLetivoLookupFiltroViewModel filter)
        {
            var service = controllerBase.Create<IRegimeLetivoService>();
            filter.RegimesLetivos = service.BuscarRegimesLetivosInstituicaoSelect();

            return filter;
        }
    }
}
