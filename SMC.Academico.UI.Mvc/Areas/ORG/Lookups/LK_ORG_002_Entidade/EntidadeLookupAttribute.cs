using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeLookupAttribute: SMCLookupAttribute
    {
        public EntidadeLookupAttribute()
            : base("Entidade")
        {
            ModalWindowSize = SMCModalWindowSize.Large;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(EntidadeLookupViewModel);
            Filter = typeof(EntidadeLookupFiltroViewModel);
            PrepareFilter = typeof(EntidadeLookupPrepareFilter);
            Service<IEntidadeService>(nameof(IEntidadeService.BuscarEntidades));     
        }
    }
}
