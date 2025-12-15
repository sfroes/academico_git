using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CicloLetivoLookupAttribute : SMCLookupAttribute
    {
        public CicloLetivoLookupAttribute()
            : base("CicloLetivo")
        {
            HideSeq = true;
            ModalWindowSize = SMCModalWindowSize.Medium;
            Filter = typeof(CicloLetivoLookupFiltroViewModel);
            Model = typeof(CicloLetivoLookupListaViewModel);
            PrepareFilter = typeof(CicloLetivoLookupPrepareFilter);
            Service<ICicloLetivoService>(nameof(ICicloLetivoService.BuscarCiclosLetivosLookup));
            SelectService<ICicloLetivoService>(nameof(ICicloLetivoService.BuscarCiclosLetivosLookupSelect));
        }
    }
}
