using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaAtuacaoLookupAttribute : SMCLookupAttribute
    {
        public PessoaAtuacaoLookupAttribute()
            : base("PessoaAtuacao")
        {
            Filter = typeof(PessoaAtuacaoLookupFiltroViewModel);
            Model = typeof(PessoaAtuacaoLookupViewModel);
            ModalWindowSize = Framework.SMCModalWindowSize.Largest;
            PrepareFilter = typeof(PessoaAtuacaoLookupPrepareFilter);
            Service<IPessoaAtuacaoService>(nameof(IPessoaAtuacaoService.BuscarPessoaAtuacoes));
            SelectService<IPessoaAtuacaoService>(nameof(IPessoaAtuacaoService.BuscarPessoaAtuacao));
        }
    }
}