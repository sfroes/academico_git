using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class MatrizCurricularLookupAttribute : SMCLookupAttribute
    {
        public MatrizCurricularLookupAttribute() :
            base("MatrizCurricular")
        {
            ModalWindowSize = SMCModalWindowSize.Large;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(MatrizCurricularLookupViewModel);
            Filter = typeof(MatrizCurricularLookupFiltroViewModel);
            PrepareFilter = typeof(MatrizCurricularLookupPrepareFilter);
            Service<IMatrizCurricularService>(nameof(IMatrizCurricularService.BuscarMatrizesCurricular));
            SelectService<IMatrizCurricularService>(nameof(IMatrizCurricularService.BuscarMatrizCurricular));
            CustomView = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_003_Matriz_Curricular/_List";
        }
    }
}