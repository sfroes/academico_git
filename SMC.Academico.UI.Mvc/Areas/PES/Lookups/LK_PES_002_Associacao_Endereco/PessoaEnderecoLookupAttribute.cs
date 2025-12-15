using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaEnderecoLookupAttribute : SMCLookupAttribute
    {
        public PessoaEnderecoLookupAttribute()
            : base("PessoaEndereco")
        {
            Filter = typeof(PessoaEnderecoLookupFiltroViewModel);
            Model = typeof(PessoaEnderecoLookupViewModel);
            PrepareFilter = typeof(PessoaEnderecoLookupPrepareFilter);
            Service<IPessoaEnderecoService>(nameof(IPessoaEnderecoService.BuscarPessoaEnderecosLookup));
            SelectService<IPessoaAtuacaoEnderecoService>(nameof(IPessoaAtuacaoEnderecoService.BuscarPessoaAtuacaoEnderecosLookup));
            //Nas Views adicionar RazorGenerator
            CustomFilter = "SMC.Academico.UI.Mvc.dll/Areas/PES/Lookups/LK_PES_002_Associacao_Endereco/Views/CustomFilter";
            CustomReturn = "SMC.Academico.UI.Mvc.dll/Areas/PES/Lookups/LK_PES_002_Associacao_Endereco/Views/CustomReturn";
            CustomView = "SMC.Academico.UI.Mvc.dll/Areas/PES/Lookups/LK_PES_002_Associacao_Endereco/Views/CustomView";
            //----------------------------------------------------------------------------------------------
            AutoSearch = true;
        }
    }
}