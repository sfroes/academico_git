using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Lookups
{
    public class TermoIntercambioLookupAttribute : SMCLookupAttribute
    {
        public TermoIntercambioLookupAttribute()
            : base("TermoIntercambio")
        {
            //AutoSearch = true;
            HideSeq = true;
            ModalWindowSize = SMCModalWindowSize.Largest;
            Filter = typeof(TermoIntercambioLookupFiltroViewModel);
            Model = typeof(TermoIntercambioLookupViewModel);
            PrepareFilter = typeof(TermoIntercambioLookupPrepareFilter);
            Service<ITermoIntercambioService>(nameof(ITermoIntercambioService.ListarTermoIntercambio));
            CustomView = "SMC.Academico.UI.Mvc.dll/Areas/ALN/Lookups/LK_ALN_002_Termo_Intercambio/_List";
        }
    }
}
