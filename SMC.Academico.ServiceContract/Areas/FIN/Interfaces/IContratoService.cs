using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.FIN.Interfaces
{
    public interface IContratoService : ISMCService
    {
        SMCPagerData<ContratoListarData> ListarContrato(ContratoFiltroData filtro);

        ContratoData BuscarContrato(long seq);

        long SalvarContrato(ContratoData contrato);

        AdesaoContratoData BuscarAdesaoContrato(long seqSolicitacaoMatricula);
        AdesaoContratoData BuscarAdesaoContratoResidenciaMedica(long seqSolicitacaoMatricula);

        AdesaoContratoData AderirContrato(AdesaoContratoDadosData dados);

        AdesaoContratoData AderirContratoRenovacao(AdesaoContratoDadosData dados);
        AdesaoContratoData AderirContratoResidenciaMedica(AdesaoContratoDadosData dados);

        SMCUploadFile GerarTermoAdesaoContrato(long seqSolicitacaoMatricula, bool gerarTermo = false);
        SMCUploadFile GerarTermoAdesaoContratoResidenciaMedica(long seqSolicitacaoMatricula, bool gerarTermo = false);
    }
}