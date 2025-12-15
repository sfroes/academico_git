using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class SelecaoOfertaCampanhaLookupAttribute : SMCLookupAttribute
    {
        public SelecaoOfertaCampanhaLookupAttribute()
            : base("OfertaCampanha")
        {
            Filter = typeof(SelecaoOfertaCampanhaLookupFiltroViewModel);
            Model = typeof(SelecaoOfertaCampanhaLookupListaViewModel);
            PrepareFilter = typeof(SelecaoOfertaCampanhaLookupPrepareFilter);
            Service<ICampanhaOfertaService>(nameof(ICampanhaOfertaService.BuscarCampanhasOfertaSelecaoLookup));
            SelectService<ICampanhaOfertaService>(nameof(ICampanhaOfertaService.BuscarCampanhasOfertaSelecaoLookup));
            CustomView = @"SMC.Academico.UI.Mvc.dll/Areas/CAM/Lookups/LK_CAM_003_SelecaoOfertaCampanha/Views/CustomOfertaCampanhaListaLookup";
            CustomFilter = @"SMC.Academico.UI.Mvc.dll/Areas/CAM/Lookups/LK_CAM_003_SelecaoOfertaCampanha/Views/CustomOfertaCampanhaFiltroLookup";
        }
    }
}
