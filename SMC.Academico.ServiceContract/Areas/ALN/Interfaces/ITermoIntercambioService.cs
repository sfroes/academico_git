using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ITermoIntercambioService : ISMCService
    {
        TermoIntercambioCabecalhoData BuscarCabecalhoTermoIntercambio(long seqParceriaIntercambio);

        SMCPagerData<TermoIntercambioListarData> ListarTermoIntercambio(TermoIntercambioFiltroData filtro);

        TermoIntercambioData PreencherModeloTermoIntercambio(long seq);

        long SalvarTermoIntercambio(TermoIntercambioData modelo);

        bool TermoIntercambioPossuiPessoaAtuacao(long seqTermoIntercambio);

        void ExcluirTermoIntercambio(long seq);

        DadosSimplificadoTermoIntercambioData BuscarDadosTermoIntercambio(long seqTermoIntercambio);
    }
}