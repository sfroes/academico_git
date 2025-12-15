using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class EnvioNotificacaoDestinatarioService : SMCServiceBase, IEnvioNotificacaoDestinatarioService
    {
        #region DomainService

        private EnvioNotificacaoDestinatarioDomainService EnvioNotificacaoDestinatarioDomainService => Create<EnvioNotificacaoDestinatarioDomainService>();

        #endregion DomainService

        public EnvioNotificacaoDestinatarioAlunoCabecalhoData BuscaDadosEnvioNotificacaoDestinatarioAlunoCabecalho(long seqPessoaAtuacao)
        {
            return EnvioNotificacaoDestinatarioDomainService
                        .BuscaDadosEnvioNotificacaoDestinatarioAlunoCabecalho(seqPessoaAtuacao)
                        .Transform<EnvioNotificacaoDestinatarioAlunoCabecalhoData>();
        }

        public EnvioNotificacaoDestinatarioColaboradorCabecalhoData BuscaDadosEnvioNotificacaoDestinatarioColaboradorCabecalho(long seqPessoaAtuacao)
        {
            return EnvioNotificacaoDestinatarioDomainService
                        .BuscaDadosEnvioNotificacaoDestinatarioColaboradorCabecalho(seqPessoaAtuacao)
                        .Transform<EnvioNotificacaoDestinatarioColaboradorCabecalhoData>();
        }

        public SMCPagerData<EnvioNotificacaoDestinatarioListarData> BuscarNotificacoesDestinatario(EnvioNotificacaoDestinatarioFiltroData filtro)
        {
            var spec = filtro.Transform<EnvioNotificacaoDestinatarioFilterSpecification>();
            return EnvioNotificacaoDestinatarioDomainService.BuscarNotificacoesDestinatario(spec)
                                           .Transform<SMCPagerData<EnvioNotificacaoDestinatarioListarData>>();
        }
    }
}