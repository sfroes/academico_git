using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaLookupAttribute : SMCLookupAttribute
    {
        public PessoaLookupAttribute()
            : base("Pessoa")
        {
            HideSeq = true;
            Filter = typeof(PessoaLookupFiltroViewModel);
            Model = typeof(PessoaLookupViewModel);
            ModalWindowSize = Framework.SMCModalWindowSize.Largest;
            Service<IPessoaService>(nameof(IPessoaService.BuscarPessoasLookup));
            SelectService<IPessoaService>(nameof(IPessoaService.BuscarPessoaLookup));
        }
    }
}