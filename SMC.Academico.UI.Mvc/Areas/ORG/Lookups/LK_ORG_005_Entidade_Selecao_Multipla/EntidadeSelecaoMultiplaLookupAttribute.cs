using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeSelecaoMultiplaLookupAttribute : SMCLookupAttribute
    {
        public EntidadeSelecaoMultiplaLookupAttribute()
             : base("Entidade")
        {
            ModalWindowSize = SMCModalWindowSize.Large;
            AutoSearch = true;
            HideSeq = true;
            Filter = typeof(EntidadeSelecaoMultiplaLookupFiltroViewModel);
            PrepareFilter = typeof(EntidadeSelecaoMultiplaLookupPrepareFilter);
            Model = typeof(EntidadeSelecaoMultiplaLookupListaViewModel);
            Service<IEntidadeService>(nameof(IEntidadeService.BuscarEntidadesLookupSelecaoMultipla));
        }
    }
}
