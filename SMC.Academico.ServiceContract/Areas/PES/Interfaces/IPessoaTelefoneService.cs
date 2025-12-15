using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IPessoaTelefoneService : ISMCService
    {
        SMCPagerData<PessoaTelefoneLookupData> BuscarPessoaTelefonesLookup(PessoaTelefoneFiltroLookupData filtro);

        long SalvarTelefonePessoa(PessoaTelefoneData telefoneData);

        PessoaTelefoneData BuscarPessoaTelefone(long seq);
    }
}