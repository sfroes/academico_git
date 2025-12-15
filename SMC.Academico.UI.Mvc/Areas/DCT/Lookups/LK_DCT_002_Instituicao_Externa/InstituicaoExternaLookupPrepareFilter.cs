using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    public class InstituicaoExternaLookupPrepareFilter : ISMCFilter<InstituicaoExternaLookupFiltroViewModel>
    {
        public InstituicaoExternaLookupFiltroViewModel Filter(SMCControllerBase controllerBase, InstituicaoExternaLookupFiltroViewModel filter)
        {
            filter.CategoriasInstituicaoEnsino = controllerBase.Create<ICategoriaInstituicaoEnsinoService>().BuscarCategoriasInstituicaoEnsinoSelect();
            filter.Paises = controllerBase.Create<ILocalidadeService>().BuscarPaisesValidosCorreios().Select(s => new SMCDatasourceItem(s.Codigo, s.Nome)).ToList();
            if (!filter.RetornarInstituicaoEnsinoLogada)
            {
                filter.SeqInstituicaoEnsino = controllerBase.HttpContext.GetEntityLogOn(FILTER.INSTITUICAO_ENSINO).Value;
            }
            return filter;
        }
    }
}