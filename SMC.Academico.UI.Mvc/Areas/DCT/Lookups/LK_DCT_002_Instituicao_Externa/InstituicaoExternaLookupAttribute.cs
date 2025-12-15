using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    public class InstituicaoExternaLookupAttribute : SMCLookupAttribute
    {
        public InstituicaoExternaLookupAttribute()
            : base("EntidadeExterna")
        {
            ModalWindowSize = SMCModalWindowSize.Largest;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(InstituicaoExternaLookupViewModel);
            Filter = typeof(InstituicaoExternaLookupFiltroViewModel);
            PrepareFilter = typeof(InstituicaoExternaLookupPrepareFilter);
            Service<IInstituicaoExternaService>(nameof(IInstituicaoExternaService.BuscarInstituicoesExternas));
            SelectService<IInstituicaoExternaService>(nameof(IInstituicaoExternaService.BuscarInstituicoesExternas));
        }
    }
}