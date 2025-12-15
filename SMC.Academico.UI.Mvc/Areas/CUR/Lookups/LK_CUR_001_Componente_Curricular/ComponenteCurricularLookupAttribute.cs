using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ComponenteCurricularLookupAttribute : SMCLookupAttribute
    {
        public ComponenteCurricularLookupAttribute(bool customReturn = false) :
            base("ComponenteCurricular")
        {
            ModalWindowSize = SMCModalWindowSize.Largest;
            AutoSearch = false;
            HideSeq = true;
            Model = typeof(ComponenteCurricularLookupViewModel);
            Filter = typeof(ComponenteCurricularLookupFiltroViewModel);
            PrepareFilter = typeof(ComponenteCurricularLookupPrepareFilter);
            Service<IComponenteCurricularService>(nameof(IComponenteCurricularService.BuscarComponentesCurricularesLookup));
            SelectService<IComponenteCurricularService>(nameof(IComponenteCurricularService.BuscarComponentesCurricularesLookup));
            CustomView = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_001_Componente_Curricular/_CustomView";

            if (customReturn)
                CustomReturn = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_001_Componente_Curricular/_ReturnList";
        }
    }
}