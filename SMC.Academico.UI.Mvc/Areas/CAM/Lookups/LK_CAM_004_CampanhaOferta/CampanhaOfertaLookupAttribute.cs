using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CampanhaOfertaLookupAttribute : SMCLookupAttribute
    {
        public CampanhaOfertaLookupAttribute(bool useCustomFilter = false)
            : base("CampanhaOferta")
        {
            AutoSearch = true;
            HideSeq = true;
            ModalWindowSize = SMCModalWindowSize.Largest;
            Filter = typeof(CampanhaOfertaLookupFiltroViewModel);
            Model = typeof(CampanhaOfertaLookupViewModel);
            PrepareFilter = typeof(CampanhaOfertaLookupPrepareFilter);
            Service<ICampanhaOfertaService>(nameof(ICampanhaOfertaService.BuscarCampanhasOfertaLookup));
            //SelectService<ICampanhaOfertaService>(nameof(ICampanhaOfertaService.BuscarCampanhaOferta));
            if (useCustomFilter)
                CustomFilter = "SMC.Academico.UI.Mvc.dll/Areas/CAM/Lookups/LK_CAM_004_CampanhaOferta/Views/CustomFilter";
        }
    }
}