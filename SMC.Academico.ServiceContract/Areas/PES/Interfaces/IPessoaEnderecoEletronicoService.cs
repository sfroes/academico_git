using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IPessoaEnderecoEletronicoService : ISMCService
    {
        SMCPagerData<PessoaEnderecoEletronicoLookupData> BuscarPessoaEnderecoEletronicosLookup(PessoaEnderecoEletronicoFiltroLookupData filtro);

        long SalvarEnderecoEletronicoPessoa(PessoaEnderecoEletronicoData enderecoEletronicoData);

        PessoaEnderecoEletronicoData BuscarPessoaEnderecoEletronico(long seq);
    }
}