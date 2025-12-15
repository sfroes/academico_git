using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class DispensaComponenteCurricularLookupAttribute : SMCLookupAttribute
    {
        public DispensaComponenteCurricularLookupAttribute() :
            base("DispensaComponenteCurricular")
        {
            ModalWindowSize = SMCModalWindowSize.Large;
            HideSeq = true;
            Filter = typeof(DispensaComponenteCurricularLookupFiltroViewModel);
            Model = typeof(DispensaComponenteCurricularLookupViewModel);
            PrepareFilter = typeof(DispensaComponenteCurricularLookupPrepareFilter);
            Service<IComponenteCurricularService>(nameof(IComponenteCurricularService.BuscarComponentesCurricularesDispensaLookup));
            SelectService<IComponenteCurricularService>(nameof(IComponenteCurricularService.BuscarComponentesCurricularesDispensa));
        }
    }    
}
