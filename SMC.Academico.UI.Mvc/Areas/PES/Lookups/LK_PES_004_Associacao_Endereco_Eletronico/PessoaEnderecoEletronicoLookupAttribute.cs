using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaEnderecoEletronicoLookupAttribute : SMCLookupAttribute
    {
        public PessoaEnderecoEletronicoLookupAttribute()
            : base("PessoaEnderecoEletronico")
        {
            Filter = typeof(PessoaEnderecoEletronicoLookupFiltroViewModel);
            Model = typeof(PessoaEnderecoEletronicoLookupViewModel);
            Service<IPessoaEnderecoEletronicoService>(nameof(IPessoaEnderecoEletronicoService.BuscarPessoaEnderecoEletronicosLookup));
            SelectService<IPessoaEnderecoEletronicoService>(nameof(IPessoaEnderecoEletronicoService.BuscarPessoaEnderecoEletronicosLookup));
            CustomFilter = "SMC.Academico.UI.Mvc.dll/Areas/PES/Lookups/LK_PES_004_Associacao_Endereco_Eletronico/Views/CustomFilter";
            CustomView = "SMC.Academico.UI.Mvc.dll/Areas/PES/Lookups/LK_PES_004_Associacao_Endereco_Eletronico/Views/CustomView";
            ModalWindowSize = SMCModalWindowSize.Medium;
            AutoSearch = true;
        }
    }
}