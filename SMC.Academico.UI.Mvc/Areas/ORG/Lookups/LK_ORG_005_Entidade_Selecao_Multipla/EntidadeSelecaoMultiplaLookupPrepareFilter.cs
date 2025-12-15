using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeSelecaoMultiplaLookupPrepareFilter : ISMCFilter<EntidadeSelecaoMultiplaLookupFiltroViewModel>
    {
        public EntidadeSelecaoMultiplaLookupFiltroViewModel Filter(SMCControllerBase controllerBase, EntidadeSelecaoMultiplaLookupFiltroViewModel filter)
        {
            var service = controllerBase.Create<ITipoEntidadeService>();

            // Busca os tipos de entidade que são responsáveis da visão organizacional
            filter.TiposEntidade = service.BuscarTipoEntidadeResponsavelPorVisao(filter.SeqInstituicaoEnsino.Value, TipoVisao.VisaoOrganizacional);
            filter.SeqsTipoEntidade = filter.TiposEntidade.Select(s => s.Seq).ToList();

            return filter;
        }
    }
}