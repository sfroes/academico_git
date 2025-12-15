using System.ServiceModel;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IEnvioNotificacaoDestinatarioService : ISMCService
    {
        EnvioNotificacaoDestinatarioAlunoCabecalhoData BuscaDadosEnvioNotificacaoDestinatarioAlunoCabecalho(long seqPessoaAtuacao);

        EnvioNotificacaoDestinatarioColaboradorCabecalhoData BuscaDadosEnvioNotificacaoDestinatarioColaboradorCabecalho(long seqPessoaAtuacao);

        SMCPagerData<EnvioNotificacaoDestinatarioListarData> BuscarNotificacoesDestinatario(EnvioNotificacaoDestinatarioFiltroData filtro);

    }
}