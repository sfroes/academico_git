using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class SolicitacaoDeServicoLookupAttribute : SMCLookupAttribute
    {
        public SolicitacaoDeServicoLookupAttribute(bool customReturn = false) :
          base("SolicitacaoServico")
        {
            ModalWindowSize = SMCModalWindowSize.Largest;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(SolicitacaoDeServicoLookupListaViewModel);
            Filter = typeof(SolicitacaoDeServicoLookupFiltroViewModel);
            PrepareFilter = typeof(SolicitacaoDeServicoLookupPrepareFilter);
            Service<ISolicitacaoServicoService>(nameof(ISolicitacaoServicoService.ListarSolicitacoesServico));
        }
    }
}
