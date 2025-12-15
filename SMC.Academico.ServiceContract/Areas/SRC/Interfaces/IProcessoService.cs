using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IProcessoService : ISMCService
    {
        SMCPagerData<ProcessoListaData> BuscarProcessos(ProcessoFiltroData filtros);

        ProcessoData BuscarProcessoInserir();

        ProcessoData BuscarProcessoEditar(long seqProcesso);

        CopiarProcessoData BuscarProcessoCopiar(long seqProcesso);

        long SalvarProcesso(ProcessoData modelo);

        List<SMCDatasourceItem> BuscarEtapasDoProcessoSelect(long seqProcesso);

        List<SMCDatasourceItem> BuscarSituacoesEtapasComCategoriaSelect(long? seqProcessoEtapa, List<long> seqsProcessos);

        List<SMCDatasourceItem> BuscarSituacoesEtapasSgfSelect(long? seqProcessoEtapaSgf, long? seqServico);

        bool ValidarBloqueioSituacaoDocumentacao(List<long> seqsProcessos);

        /// <summary>
        /// Busca a descrição do ciclo letivo do processo
        /// </summary>
        /// <param name="seq">Sequencial do processo</param>
        /// <returns>Descrição do ciclo letivo</returns>
        string BuscarDescricaoCicloLetivoProcesso(long seq);

        /// <summary>
        /// Busca uma lista contendo todos os processos
        /// </summary>
        /// <param name="filtros">Filtros e ordenação</param>
        /// <returns>Dados dos processos filtrados e ordenados</returns>
        List<SMCDatasourceItem> BuscarProcessosSelect(ProcessoFiltroData filtros);

        /// <summary>
        /// Busca um processo por dados do processo ou do seu relacionamento com processo seletivo
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Dados do processo</returns>
        ProcessoData BuscarProcesso(ProcessoFiltroData filtros);

        /// <summary>
        /// Buscar dados do processo para montar o cabeçalho das ações relativas ao mesmo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <param name="quantidadeSolicitacoes">Exibir a quantidade de solicitações do processo</param>
        /// <returns></returns>
        ProcessoCabecalhoData BuscarCabecalhoProcesso(long seqProcesso, bool quantidadeSolicitacoes);

        /// <summary>
        /// Buscar dados do processo e do grupo de escalonamento para montar o cabeçalho das ações relativas ao mesmo
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>
        /// <returns></returns>
        GrupoEscalonamentoItemCabecalhoData BuscarCabecalhoGrupoEscalonamentoItem(long seqGrupoEscalonamentoItem);

        /// <summary>
        /// Busca os processos de matrícula associados à um ciclo letivo.
        /// </summary>
        List<SMCDatasourceItem> BuscarProcessosMatriculaPorCicloLetivoSelect(long seqCicloLetivo, long seqCampanha);

        /// <summary>
        /// Busca os dados do agendamento do SAT de um processo.
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo.</param>
        /// <returns>O sequencial do agendamento se existir. Caso contrário retorna null.</returns>
        (long? SeqAgendamento, SituacaoAgendamento? Processando) BuscarAgendamentoSAT(long seqProcesso);

        /// <summary>
        /// Atualiza o sequencial do agendamento do SAT de um processo.
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo.</param>
        /// <param name="seqAgendamento">Sequencial do agendamento.</param>
        void CriarAgendamentoPreparacaoRematricula(long seqProcesso, long seqAgendamento);

        /// <summary>
        /// Busca os processos de acordo com os serviços que o aluno pode abrir solicitação
        /// </summary>
        /// <param name="filtro">Filtros para pesquisa</param>
        /// <returns>Lista de processos</returns>
        List<SMCDatasourceItem> BuscarProcessosPorAlunoSelect(ServicoPorAlunoFiltroData filtro);

        /// <summary>
        /// Busca os processos de acordo com um servico informado
        /// </summary>
        /// <param name="seqServico">Sequencial do serviço</param>
        /// <returns>Lista de processos encontrados</returns>
        List<SMCDatasourceItem> BuscarProcessosPorServicoSelect(long seqServico);

        /// <summary>
        /// Busca informacoes do processo pelo sequencial informado
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <returns>Informações do processo encontradas</returns>
        InformacaoProcessoListaData BuscarDadosProcesso(long seqProcesso, long? seqGrupoEscalonamento);

        /// <summary>
        /// Buscar dados do processo e do grupo de escalonamento para montar o cabeçalho das informações relativas ao mesmo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do process</param>
        /// <returns>Informações do processo e do grupo de escalonamento</returns>
        CabecalhoInformacaoProcessoData BuscarCabecalhoDadosProcesso(long seqProcesso, long? seqGrupoEscalonamento);

        ConsultaPosicaoGeralData BuscarPosicaoConsolidadaGeral(ProcessoFiltroData filtros);

        /// <summary>
        /// Executa procedimentos para encerramento do processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo a ser encerrado</param>
        void EncerrarProcesso(long seqProcesso);

        /// <summary>
        /// Executa procedimentos para copiar o processo
        /// </summary>
        /// <param name="modelo">Modelo do processo a ser copiad</param>
        long SalvarCopiaProcesso(CopiarProcessoData modelo);

        List<ProcessoEtapaSGFData> BuscarEtapasSGFPorServico(long seqServico);

        bool VerificarValidadeTokenManutencaoProcesso(long seqProcesso);
        void ReabrirProcesso(long seqProcesso);
    }
}