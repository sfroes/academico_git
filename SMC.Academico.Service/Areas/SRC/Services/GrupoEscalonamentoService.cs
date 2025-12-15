using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class GrupoEscalonamentoService : SMCServiceBase, IGrupoEscalonamentoService
    {
        #region [ DomainService ]

        private GrupoEscalonamentoDomainService GrupoEscalonamentoDomainService
        {
            get { return this.Create<GrupoEscalonamentoDomainService>(); }
        }

        #endregion [ DomainService ]

        public SMCPagerData<GrupoEscalonamentoListaData> BuscarGruposEscalonamento(GrupoEscalonamentoFiltroData filtro)
        {
            var result = this.GrupoEscalonamentoDomainService.BuscarGruposEscalonamento(filtro.Transform<GrupoEscalonamentoFiltroVO>()).TransformList<GrupoEscalonamentoListaData>();

            return new SMCPagerData<GrupoEscalonamentoListaData>(result, result.Count);
        }

        public List<SMCDatasourceItem> BuscarGruposEscalonamentoSelect(GrupoEscalonamentoFiltroData filtro)
        {
            return GrupoEscalonamentoDomainService.BuscarGruposEscalonamentoSelect(filtro.Transform<GrupoEscalonamentoFiltroVO>());
        }

        public List<SMCDatasourceItem> BuscarGruposEscalonamentoPorProcessoSelect(long seqProcesso)
        {
            return GrupoEscalonamentoDomainService.BuscarGruposEscalonamentoPorProcessoSelect(seqProcesso);
        }

        public bool ExistemGruposEscalonamentoPorProcesso(List<long> seqsProcessos)
        {
            return GrupoEscalonamentoDomainService.ExistemGruposEscalonamentoPorProcesso(seqsProcessos);
        }

        public GrupoEscalonamentoData BuscarGrupoEscalonamento(long seqGrupoEscalonamento)
        {
            return this.GrupoEscalonamentoDomainService.BuscarGrupoEscalonamento(seqGrupoEscalonamento).Transform<GrupoEscalonamentoData>();
        }

        public GrupoEscalonamentoData ValidarGrupoEscalonamento(long seqProcessoEtapa)
        {
            return this.GrupoEscalonamentoDomainService.ValidarGrupoEscalonamento(seqProcessoEtapa).Transform<GrupoEscalonamentoData>();
        }

        public GrupoEscalonamentoData DesativarGrupoEscalonamento(long seqProcessoEtapa)
        {
            return this.GrupoEscalonamentoDomainService.DesativarGrupoEscalonamento(seqProcessoEtapa).Transform<GrupoEscalonamentoData>();
        }

        public GrupoEscalonamentoCabecalhoData BuscarCabecalhoGrupoEscalonamento(long seqGrupoEscalonamento)
        {
            return this.GrupoEscalonamentoDomainService.BuscarCabecalhoGrupoEscalonamento(seqGrupoEscalonamento).Transform<GrupoEscalonamentoCabecalhoData>();
        }

        public GrupoEscalonamentoData BuscarConfiguracaoEscalonamento(long seqProcesso)
        {
            return this.GrupoEscalonamentoDomainService.BuscarConfiguracaoEscalonamento(seqProcesso).Transform<GrupoEscalonamentoData>();
        }

        public long SalvarGrupoEscalonamento(GrupoEscalonamentoData modelo)
        {
            return this.GrupoEscalonamentoDomainService.SalvarGrupoEscalonamento(modelo.Transform<GrupoEscalonamentoVO>());
        }

        public void ValidarModelo(GrupoEscalonamentoData modelo)
        {
            this.GrupoEscalonamentoDomainService.ValidarModelo(modelo.Transform<GrupoEscalonamentoVO>());
        }

        public bool ValidarAssertSalvar(GrupoEscalonamentoData modelo)
        {
            return this.GrupoEscalonamentoDomainService.ValidarAssertSalvar(modelo.Transform<GrupoEscalonamentoVO>());
        }

        public void ExcluirGrupoEscalonamento(long seqGrupoEscalonamento)
        {
            this.GrupoEscalonamentoDomainService.ExcluirGrupoEscalonamento(seqGrupoEscalonamento);
        }

        /// <summary>
        /// Realiza a cópia de um grupo de escalonamento.
        /// 1. Criar o registro do novo grupo de escalonamento com a nova descrição, para o processo em questão, e copiar o número de parcelas do grupo de origem.
        /// 2. Criar o registro para os itens do grupo de escalonamento, associando-os aos mesmos escalonamentos do grupo de origem, para cada etapa do processo em questão.
        /// 3. Criar um registro para cada item do grupo de escalonamento e copiar as configurações de parcela do grupo de origem:
        ///     o número de parcelas, a data de vencimento da parcela,
        ///     percentual da parcela,
        ///     motivo do bloqueio e
        ///     descrição da parcela.
        /// 4. Criar um registro de parâmetro de envio de notificação para cada parâmetro do grupo de origem e copiar todos os dados.
        /// </summary>
        /// <param name="seqGrupoEscalonamentoOrigem">Sequencial do Grupo de escalonamento que será copiado.</param>
        /// <param name="descricao">Descrição do novo grupo de escalonamento.</param>
        public void CopiarGrupoEscalonamento(long seqGrupoEscalonamentoOrigem, string descricao)
        {
            GrupoEscalonamentoDomainService.CopiarGrupoEscalonamento(seqGrupoEscalonamentoOrigem, descricao);
        }

        /// <summary>
        /// Copiar um grupo de escalonamento de um processo
        /// </summary>
        /// <param name="modelo">Dados do modelo</param>
        public void SalvarAssociarSolicitacaoGrupoEscalonamento(GrupoEscalonamentoData modelo)
        {
            this.GrupoEscalonamentoDomainService.SalvarAssociarSolicitacaoGrupoEscalonamento(modelo.Transform<GrupoEscalonamentoVO>());
        }

        /// <summary>
        /// Efetuar a verificação dos casos impeditivos em uma associação de nova solicitação
        /// </summary>
        /// <param name="modelo">Modelo de dados</param>
        public void ValidarAssociacaoSolicitacao(GrupoEscalonamentoData modelo)
        {
            this.GrupoEscalonamentoDomainService.ValidarAssociacaoSolicitacao(modelo.Transform<GrupoEscalonamentoVO>());
        }

        /// <summary>
        /// Listar todas as vagas caso existam com respectivas descrições
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial solicitação</param>
        /// <returns>Lista da descrição de vagas, se nenhum dos itens tem vaga, se todos tem vaga e se somente algum tem vaga </returns>
        public (List<string> dscVagas, bool nenhumaTemVagas, bool todasTemVagas, bool algumasTemVagas) ValidacaoQuantidadeVagasPelaSolicitacao(long seqSolicitacaoServico)
        {
            return this.GrupoEscalonamentoDomainService.ValidacaoQuantidadeVagasPelaSolicitacao(seqSolicitacaoServico);
        }

        /// <summary>
        /// Recupera os grupos de escalonamento para o retorno do lookup
        /// </summary>
        /// <param name="seqs">Sequenciais dos grupos de escalonamento selecionados</param>
        /// <returns>Dados dos escalonamentos</returns>
        public List<GrupoEscalonamentoListaData> BuscarGruposEscalonamentoGridLookup(GrupoEscalonamentoLookupSelectData filtro)
        {
            var seqs = filtro.Seqs ?? (filtro.Seq.HasValue ? new long[] { filtro.Seq.Value } : null);
            return this.GrupoEscalonamentoDomainService.BuscarGruposEscalonamentoGridLookup(seqs, filtro.DisparaExcecao).TransformList<GrupoEscalonamentoListaData>();
        }

        /// <summary>
        /// Verifica se todos os escalonamentos dos itens do grupo possui data final maior que a data atual para continuar o processo
        /// </summary>
        /// <param name="seq">Sequencial do grupo de escalonamento</param>
        /// <returns>Retorna true para válido</returns>
        public bool ValidarDataFimEscalonamentoPorGrupoEscalonamento(long seq)
        {
            return GrupoEscalonamentoDomainService.ValidarDataFimEscalonamentoPorGrupoEscalonamento(seq);
        }

        /// <summary>
        /// Enviar notificações referentes aos prazos de vigencia das etapas do grupo de escalonamento
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>        
        public void EnviarNotificacaoPrazoVigencia(long seqGrupoEscalonamento)
        {
            this.GrupoEscalonamentoDomainService.EnviarNotificacaoPrazoVigencia(seqGrupoEscalonamento);
        }

        /// <summary>
        /// Validar se a solicitação é uma disciplina isolada e irá chamar validação de vagas e consequentemente o assert.
        /// </summary>
        /// <param name="seqSoliciatacaoServico">Seq solicitação de serviço</param>
        /// <returns></returns>
        public bool ValidarAssertAssociacaoSolicitacaoGrupoEscalonamento(long seqSoliciatacaoServico)
        {
            return this.GrupoEscalonamentoDomainService.ValidarAssertAssociacaoSolicitacaoGrupoEscalonamento(seqSoliciatacaoServico);
        }
    }
}