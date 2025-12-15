using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class SolicitacaoServicoLookupAttribute : SMCLookupAttribute
    {
        public SolicitacaoServicoLookupAttribute()
            : base("SolicitacaoServico")
        {
            AutoSearch = true;
            HideSeq = true;
            ModalWindowSize = SMCModalWindowSize.Largest;
            Filter = typeof(SolicitacaoServicoLookupFiltroViewModel);
            Model = typeof(SolicitacaoServicoLookupListaViewModel);
            PrepareFilter = typeof(SoliciacaoServicoLookupPrepareFilter);
            Service<ISolicitacaoServicoService>(nameof(ISolicitacaoServicoService.ListarSolicitacoesServico));
        }
    }
}