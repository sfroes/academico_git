using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoLookupAttribute : SMCLookupAttribute
    {
        public CursoLookupAttribute() :
            base("Curso")
        {
            ModalWindowSize = SMCModalWindowSize.Large;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(CursoLookupViewModel);
            Filter = typeof(CursoLookupFiltroViewModel);
            PrepareFilter = typeof(CursoLookupPrepareFilter);
            Service<ICursoService>(nameof(ICursoService.BuscarCursos));
            SelectService<ICursoService>(nameof(ICursoService.BuscarCursosLookupSelect));
        }
    }
}
