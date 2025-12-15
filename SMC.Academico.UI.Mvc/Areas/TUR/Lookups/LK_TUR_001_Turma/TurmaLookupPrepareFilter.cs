using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Lookups
{
    public class TurmaLookupPrepareFilter : ISMCFilter<TurmaLookupFiltroViewModel>
    {
        public TurmaLookupFiltroViewModel Filter(SMCControllerBase controllerBase, TurmaLookupFiltroViewModel filter)
        {
            var entidadesResponsaveis = controllerBase.Create<IEntidadeService>();
            filter.DataSourceEntidadeResponsavel = entidadesResponsaveis.BuscarUnidadesResponsaveisGPILocalSelect();

            filter.EntidadeResponsaveisReadOnly = filter.SeqsEntidadesResponsaveis != null && filter.SeqsEntidadesResponsaveis.Any(a => a !=0);
            if (filter.SeqCicloLetivo.HasValue)
            {
                filter.SeqCicloLetivoInicio = new CicloLetivoLookupViewModel() { Seq = filter.SeqCicloLetivo };
                filter.CicloLetivoReadOnly = true;
            }

            return filter;
        }
    }
}