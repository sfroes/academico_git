using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IGrupoEscalonamentoService : ISMCService
    {
        SMCPagerData<GrupoEscalonamentoListaData> BuscarGruposEscalonamento(GrupoEscalonamentoFiltroData filtro);

        List<SMCDatasourceItem> BuscarGruposEscalonamentoSelect(GrupoEscalonamentoFiltroData filtro);

        List<SMCDatasourceItem> BuscarGruposEscalonamentoPorProcessoSelect(long seqProcesso);

        GrupoEscalonamentoData BuscarGrupoEscalonamento(long seqGrupoEscalonamento);

        GrupoEscalonamentoData ValidarGrupoEscalonamento(long seqProcessoEtapa);

        GrupoEscalonamentoData DesativarGrupoEscalonamento(long seqProcessoEtapa);

        GrupoEscalonamentoCabecalhoData BuscarCabecalhoGrupoEscalonamento(long seqGrupoEscalonamento);

        GrupoEscalonamentoData BuscarConfiguracaoEscalonamento(long seqProcesso);

        long SalvarGrupoEscalonamento(GrupoEscalonamentoData modelo);

        void ValidarModelo(GrupoEscalonamentoData modelo);

        bool ValidarAssertSalvar(GrupoEscalonamentoData modelo);

        void ExcluirGrupoEscalonamento(long seqGrupoExcalonamento);

        bool ExistemGruposEscalonamentoPorProcesso(List<long> seqsProcessos);

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
        void CopiarGrupoEscalonamento(long seqGrupoEscalonamentoOrigem, string descricao);

        /// <summary>
        /// Copiar um grupo de escalonamento de um processo
        /// </summary>
        /// <param name="modelo">Dados do modelo</param>
        void SalvarAssociarSolicitacaoGrupoEscalonamento(GrupoEscalonamentoData modelo);

        /// <summary>
        /// Efetuar a verificação dos casos impeditivos em uma associação de nova solicitação
        /// </summary>
        /// <param name="modelo">Modelo de dados</param>
        void ValidarAssociacaoSolicitacao(GrupoEscalonamentoData modelo);

        /// <summary>
        /// Listar todas as vagas caso existam com respectivas descrições
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial solicitação</param>
        /// <returns>Lista da descrição de vagas, se nenhum dos itens tem vaga, se todos tem vaga e se somente algum tem vaga </returns>
        (List<string> dscVagas, bool nenhumaTemVagas, bool todasTemVagas, bool algumasTemVagas) ValidacaoQuantidadeVagasPelaSolicitacao(long seqSolicitacaoServico);

        /// <summary>
        /// Recupera os grupos de escalonamento para o retorno do lookup
        /// </summary>
        /// <param name="seqs">Sequenciais dos grupos de escalonamento selecionados</param>
        /// <returns>Dados dos escalonamentos</returns>
        List<GrupoEscalonamentoListaData> BuscarGruposEscalonamentoGridLookup(GrupoEscalonamentoLookupSelectData filtro);

        /// <summary>
        /// Verifica se todos os escalonamentos dos itens do grupo possui data final maior que a data atual para continuar o processo
        /// </summary>
        /// <param name="seq">Sequencial do grupo de escalonamento</param>
        /// <returns>Retorna true para válido</returns>
        bool ValidarDataFimEscalonamentoPorGrupoEscalonamento(long seq);

        /// <summary>
        /// Enviar notificações referentes aos prazos de vigencia das etapas do grupo de escalonamento
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>        
        void EnviarNotificacaoPrazoVigencia(long seqGrupoEscalonamento);

        /// <summary>
        /// Validar se a solicitação é uma disciplina isolada e irá chamar validação de vagas e consequentemente o assert.
        /// </summary>
        /// <param name="seqSoliciatacaoServico">Seq solicitação de serviço</param>
        /// <returns></returns>
        bool ValidarAssertAssociacaoSolicitacaoGrupoEscalonamento(long seqSoliciatacaoServico);
    }
}