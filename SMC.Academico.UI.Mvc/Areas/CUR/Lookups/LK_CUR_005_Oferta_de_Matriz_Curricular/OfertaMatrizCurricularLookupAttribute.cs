using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class OfertaMatrizCurricularLookupAttribute : SMCLookupAttribute
    {
        public OfertaMatrizCurricularLookupAttribute()
            : base("OfertaMatrizCurricular")
        {
            ModalWindowSize = SMCModalWindowSize.Largest;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(OfertaMatrizCurricularLookupViewModel);
            Filter = typeof(OfertaMatrizCurricularLookupFiltroViewModel);
            PrepareFilter = typeof(OfertaMatrizCurricularLookupPrepareFilter);
            Service<IMatrizCurricularOfertaService>(nameof(IMatrizCurricularOfertaService.BuscarMatrizesCurricularLookupOferta));
            SelectService<IMatrizCurricularOfertaService>(nameof(IMatrizCurricularOfertaService.BuscarMatrizesCurricularLookupOfertaSelecionado));
            CustomView = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_005_Oferta_de_Matriz_Curricular/_List";
        }
    }
}