using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoOfertaLookupAttribute : SMCLookupAttribute
    {
        public CursoOfertaLookupAttribute() :
            base("CursoOferta")
        {
            ModalWindowSize = SMCModalWindowSize.Largest;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(CursoOfertaLookupViewModel);
            Filter = typeof(CursoOfertaLookupFiltroViewModel);
            PrepareFilter = typeof(CursoOfertaLookupPrepareFilter);
            Service<ICursoOfertaService>(nameof(ICursoOfertaService.BuscarCursoOfertasLookup));
            SelectService<ICursoOfertaService>(nameof(ICursoOfertaService.BuscarCursosOferta));
        }
    }
}