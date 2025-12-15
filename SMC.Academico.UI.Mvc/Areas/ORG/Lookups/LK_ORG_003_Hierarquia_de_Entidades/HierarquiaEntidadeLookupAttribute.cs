using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class HierarquiaEntidadeLookupAttribute : SMCLookupAttribute
    {
        public HierarquiaEntidadeLookupAttribute()
            : base("HierarquiaEntidade", SMCDisplayModeType.TreeView)
        {
            ModalWindowSize = SMCModalWindowSize.Medium;
            HideSeq = true;
            Model = typeof(HierarquiaEntidadeLookupViewModel);
            Filter = typeof(HierarquiaEntidadeLookupFiltroViewModel);
            PrepareFilter = typeof(HierarquiaEntidadeLookupPrepareFilter);
            Transformer = typeof(HierarquiaEntidadeLookupTransformer);
            Service<IHierarquiaEntidadeItemService>(nameof(IHierarquiaEntidadeItemService.BuscarHierarquiaEntidadeItemLookup));
            SelectService<IHierarquiaEntidadeItemService>(nameof(IHierarquiaEntidadeItemService.BuscarHierarquiaEntidadeItemLookup));
        }
    }
}
