using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class SelecaoOfertaCampanhaLookupPrepareFilter : ISMCFilter<SelecaoOfertaCampanhaLookupFiltroViewModel>
    {
        public SelecaoOfertaCampanhaLookupFiltroViewModel Filter(SMCControllerBase controllerBase, SelecaoOfertaCampanhaLookupFiltroViewModel filter)
        {
            var instituicaoService = controllerBase.Create<IInstituicaoNivelService>();
            filter.NiveisEnsino = instituicaoService.BuscarNiveisEnsinoReconhecidoLDBSelect().TransformList<SMCSelectListItem>();

            var entidadeService = controllerBase.Create<IHierarquiaEntidadeService>();
            filter.EntidadesResponsaveis = entidadeService.BuscarHierarquiaOrganizacionalSuperiorProcessoSelect(true, true).TransformList<SMCSelectListItem>();

            var localidadeService = controllerBase.Create<ICursoOfertaLocalidadeService>();
            filter.Localidades = localidadeService.BuscarEntidadesSuperioresSelect().TransformList<SMCSelectListItem>();

            var tipoOfertaService = controllerBase.Create<ITipoOfertaService>();
            filter.TiposOferta = tipoOfertaService.BuscarTiposOfertaDaCampanhaSelect(filter.SeqCampanha).TransformList<SMCSelectListItem>();
                     

            return filter;
        }
    }
}
