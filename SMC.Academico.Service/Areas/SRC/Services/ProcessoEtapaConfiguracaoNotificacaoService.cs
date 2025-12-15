using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ProcessoEtapaConfiguracaoNotificacaoService : SMCServiceBase, IProcessoEtapaConfiguracaoNotificacaoService
    {
        #region [ DomainService ]

        private EscalonamentoDomainService EscalonamentoDomainService
        {
            get { return this.Create<EscalonamentoDomainService>(); }
        }

        private ProcessoEtapaConfiguracaoNotificacaoDomainService ProcessoEtapaConfiguracaoNotificacaoDomainService { get => Create<ProcessoEtapaConfiguracaoNotificacaoDomainService>(); }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca a lista de escalonamentos pelo sequencial do processo etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial processo etapa</param>
        /// <returns>Select com a lista de escalonamento</returns>
        public List<SMCDatasourceItem> BuscarEscalonamentoPorProcessoEtapaSelect(long seqProcessoEtapa)
        {
            return EscalonamentoDomainService.BuscarEscalonamentoPorProcessoEtapaSelect(seqProcessoEtapa);
        }

        /// <summary>
        /// Busca a lista de grupos escalonamento do processo etapa
        /// </summary>
        /// <param name="seq">Objeto de filtro com escalonamento</param>
        /// <returns>Objeto escalonamento com sequenciais de grupo escalonamento</returns>
        public ProcessoEtapaProcessamentoListarData BuscarSeqsGrupoEscalonamentosPorProcessoEtapa(long seq)
        {
            return EscalonamentoDomainService.BuscarSeqsGrupoEscalonamentosPorProcessoEtapa(seq).Transform<ProcessoEtapaProcessamentoListarData>();
        }

        /// <summary>
        /// Buscar configurações de notificação de um processo organizados por etapas
        /// </summary>
        /// <param name="seqProcesso">Sequencial do Processo</param>
        /// <returns>Lista de configurações de notificação de um processo organizado por etapas</returns>
        public SMCPagerData<ProcessoEtapaConfiguracaoNotificacaoListarData> BuscarConfiguracaoNotificacaoPorProcesso(ProcessoEtapaConfiguracaoNotificacaoFiltroData filtro)
        {
            return this.ProcessoEtapaConfiguracaoNotificacaoDomainService.BuscarConfiguracaoNotificacaoPorProcesso(filtro.Transform<ProcessoEtapaConfiguracaoNotificacaoFiltroVO>()).Transform<SMCPagerData<ProcessoEtapaConfiguracaoNotificacaoListarData>>();
        }

        /// <summary>
        /// Buscar Configuração de notificação
        /// </summary>
        /// <param name="seq">Sequencial configuração notificação</param>
        /// <returns>Dados da configuração da notificação</returns>
        public ProcessoEtapaConfiguracaoNotificacaoData BuscarConfiguracaoNotificacao(long seq)
        {
            return this.ProcessoEtapaConfiguracaoNotificacaoDomainService.BuscarConfiguracaoNotificacao(seq).Transform<ProcessoEtapaConfiguracaoNotificacaoData>();
        }

        /// <summary>
        /// Salvar processo etapa configuração de notificação
        /// </summary>
        /// <param name="modelo">Dados a serem salvos</param>
        /// <returns>Sequencial do processo etapa configuração notificação</returns>
        public long Salvar(ProcessoEtapaConfiguracaoNotificacaoData modelo)
        {
            return this.ProcessoEtapaConfiguracaoNotificacaoDomainService.Salvar(modelo.Transform<ProcessoEtapaConfiguracaoNotificacaoVO>());
        }

        /// <summary>
        /// Excluir a configuração notificação e todos os seus parametros
        /// </summary>
        /// <param name="seq">Sequencial da configuração notificação</param>
        public void Excluir(long seq)
        {
            this.ProcessoEtapaConfiguracaoNotificacaoDomainService.Excluir(seq);
        }

        public bool ExisteConfiguracaoNotificacao(List<long> excluidos, long seqServico)
        {
            return ProcessoEtapaConfiguracaoNotificacaoDomainService.ExisteConfiguracaoNotificacao(excluidos, seqServico);
        }
    }
}
