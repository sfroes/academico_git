using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    public class ColaboradorLookupAttribute : SMCLookupAttribute
    {
        public ColaboradorLookupAttribute()
            : base("Colaborador")
        {
            ModalWindowSize = SMCModalWindowSize.Largest;            
            HideSeq = true;
            Model = typeof(ColaboradorLookupViewModel);
            Filter = typeof(ColaboradorLookupFiltroViewModel);
            PrepareFilter = typeof(ColaboradorLookupPrepareFilter);
            Service<IColaboradorService>(nameof(IColaboradorService.BuscarColaboradores));
            SelectService<IColaboradorService>(nameof(IColaboradorService.BuscarColaboradorLookup));
        }
    }
}
