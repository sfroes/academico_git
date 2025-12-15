using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CampanhaLookupAttribute : SMCLookupAttribute
    {
        public CampanhaLookupAttribute()
            : base("Campanha")
        {
            AutoSearch = true;
            HideSeq = true;
            ModalWindowSize = SMCModalWindowSize.Large;
            Filter = typeof(CampanhaLookupFiltroViewModel);
            Model = typeof(CampanhaLookupViewModel);
            PrepareFilter = typeof(CampanhaLookupPrepareFilter);
            Service<ICampanhaService>(nameof(ICampanhaService.BuscarCampanhasLookup));
            //SelectService<ICampanhaService>(nameof(ICampanhaService.BuscarCampanha));
        }
    }
}