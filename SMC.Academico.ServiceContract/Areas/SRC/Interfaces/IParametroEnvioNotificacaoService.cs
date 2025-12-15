using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IParametroEnvioNotificacaoService : ISMCService
    {
        /// <summary>
        /// Buscar todos os parametros de envio da notificacao para processo etapa configuração notificação
        /// </summary>
        /// <param name="seqProcessoEtapaConfiguracaoNotificacao">Sequencial processo etapa configuração notificação</param>
        /// <returns>Lista de parametros envio notificação</returns>
        ParametroEnvioNotificacaoData BuscarParametroEnvioNotificacaoPorConfiguracaoNotificacao(long seqProcessoEtapaConfiguracaoNotificacao);

        /// <summary>
        /// Salvar os parametros de notificação
        /// </summary>
        /// <param name="modelo">Dados a serem salvos</param>
        /// <returns>Modelo de parametros</returns>
        ParametroEnvioNotificacaoData SalvarParametros(ParametroEnvioNotificacaoData modelo);
    }
}