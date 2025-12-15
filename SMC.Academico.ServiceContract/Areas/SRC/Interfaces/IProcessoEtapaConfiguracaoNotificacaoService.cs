using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IProcessoEtapaConfiguracaoNotificacaoService : ISMCService
    {
        /// <summary>
        /// Busca a lista de escalonamentos pelo sequencial do processo etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial processo etapa</param>
        /// <returns>Select com a lista de escalonamento</returns>
        List<SMCDatasourceItem> BuscarEscalonamentoPorProcessoEtapaSelect(long seqProcessoEtapa);

        /// <summary>
        /// Buscar configurações de notificação de um processo organizados por etapas
        /// </summary>
        /// <param name="seqProcesso">Sequencial do Processo</param>
        /// <returns>Lista de configurações de notificação de um processo organizado por etapas</returns>
        SMCPagerData<ProcessoEtapaConfiguracaoNotificacaoListarData> BuscarConfiguracaoNotificacaoPorProcesso(ProcessoEtapaConfiguracaoNotificacaoFiltroData filtro);

        /// <summary>
        /// Buscar Configuração de notificação
        /// </summary>
        /// <param name="seq">Sequencial configuração notificação</param>
        /// <returns>Dados da configuração da notificação</returns>
        ProcessoEtapaConfiguracaoNotificacaoData BuscarConfiguracaoNotificacao(long seq);

        /// <summary>
        /// Salvar processo etapa configuração de notificação
        /// </summary>
        /// <param name="modelo">Dados a serem salvos</param>
        /// <returns>Sequencial do processo etapa configuração notificação</returns>
        long Salvar(ProcessoEtapaConfiguracaoNotificacaoData modelo);

        /// <summary>
        /// Excluir a configuração notificação e todos os seus parametros
        /// </summary>
        /// <param name="seq">Sequencial da configuração notificação</param>
        void Excluir(long seq);

        bool ExisteConfiguracaoNotificacao(List<long> excluidos, long seqServico);
    }
}
