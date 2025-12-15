using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class HierarquiaEntidadeLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }
     
        [SMCHidden]
        public TipoVisao? TipoVisaoHierarquia { get; set; }

    }
}
