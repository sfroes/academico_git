using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class SolicitacaoDeServicoLookupPrepareFilter : ISMCFilter<SolicitacaoDeServicoLookupFiltroViewModel>
    {
        public SolicitacaoDeServicoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, SolicitacaoDeServicoLookupFiltroViewModel filter)
        {
            var servicoService = controllerBase.Create<IServicoService>();

            if (filter.SeqTipoServico.HasValue)
                filter.Servicos = servicoService.BuscarServicosPorInstituicaoNivelEnsinoTipoServicoSelect(filter.SeqTipoServico.Value);
            else
                filter.Servicos = servicoService.BuscarServicosPorInstituicaoNivelEnsinoSelect();

            return filter;
        }
    }
}
