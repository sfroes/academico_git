using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoOfertaLocalidadeLookupAttribute : SMCLookupAttribute
    {
        public CursoOfertaLocalidadeLookupAttribute(bool customReturn = false) :
            base("CursoOfertaLocalidade")
        {
            ModalWindowSize = SMCModalWindowSize.Largest;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(CursoOfertaLocalidadeLookupViewModel);
            Filter = typeof(CursoOfertaLocalidadeLookupFiltroViewModel);
            PrepareFilter = typeof(CursoOfertaLocalidadeLookupPrepareFilter);
            Service<ICursoOfertaLocalidadeService>(nameof(ICursoOfertaLocalidadeService.BuscarCursoOfertasLocalidade));
            SelectService<ICursoOfertaLocalidadeService>(nameof(ICursoOfertaLocalidadeService.BuscarCursoOfertasLocalidadeGridLookup));
            
            //if (customReturn)
            //    CustomReturn = @"Areas\CSO\Lookups\LK_CSO_005_Oferta_de_Curso_por_Localidade\_ReturnList";
        }
    }
}