using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class NivelEnsinoLookupAttribute : SMCLookupAttribute
    {
        public NivelEnsinoLookupAttribute()
            : base ("NivelEnsino", SMCDisplayModeType.TreeView)
        {
            ModalWindowSize = SMCModalWindowSize.Medium;
            HideSeq = true;
            Model = typeof(NivelEnsinoLookupViewModel);
            Service<INivelEnsinoService>("BuscarNiveisEnsino");
            Transformer = typeof(NivelEnsinoLookupTransformer);
        }
    }
}
