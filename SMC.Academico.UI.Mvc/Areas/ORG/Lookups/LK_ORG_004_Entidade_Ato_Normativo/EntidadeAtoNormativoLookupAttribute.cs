using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeAtoNormativoLookupAttribute : SMCLookupAttribute
    {
        public EntidadeAtoNormativoLookupAttribute()
            : base("Entidade")
        {
            ModalWindowSize = SMCModalWindowSize.Large;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(EntidadeAtoNormativoLookupListaViewModel);
            Filter = typeof(EntidadeAtoNormativoLookupFiltroViewModel);
            PrepareFilter = typeof(EntidadeAtoNormativoLookupPrepareFilter);
            Service<IEntidadeService>(nameof(IEntidadeService.BuscarEntidades));
            //SelectService<IEntidadeService>("BuscarEntidadesLookup");
           
        }
    }
}
