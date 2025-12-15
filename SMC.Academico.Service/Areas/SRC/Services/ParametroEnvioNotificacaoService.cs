using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ParametroEnvioNotificacaoService : SMCServiceBase, IParametroEnvioNotificacaoService
    {
        #region [ DomainService ]

        private ParametroEnvioNotificacaoDomainService ParametroEnvioNotificacaoDomainService { get => Create<ParametroEnvioNotificacaoDomainService>(); }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar todos os parametros de envio da notificacao para processo etapa configuração notificação
        /// </summary>
        /// <param name="seqProcessoEtapaConfiguracaoNotificacao">Sequencial processo etapa configuração notificação</param>
        /// <returns>Lista de parametros envio notificação</returns>
        public ParametroEnvioNotificacaoData BuscarParametroEnvioNotificacaoPorConfiguracaoNotificacao(long seqProcessoEtapaConfiguracaoNotificacao)
        {
            return this.ParametroEnvioNotificacaoDomainService.BuscarParametroEnvioNotificacaoPorConfiguracaoNotificacao(seqProcessoEtapaConfiguracaoNotificacao).Transform<ParametroEnvioNotificacaoData>();
        }

        /// <summary>
        /// Salvar os parametros de notificação
        /// </summary>
        /// <param name="modelo">Dados a serem salvos</param>
        /// <returns>Modelo de parametros</returns>
        public ParametroEnvioNotificacaoData SalvarParametros(ParametroEnvioNotificacaoData modelo)
        {
            return this.ParametroEnvioNotificacaoDomainService.SalvarParametros(modelo.Transform<ParametroEnvioNotificacaoVO>()).Transform<ParametroEnvioNotificacaoData>();
        }
    }
}