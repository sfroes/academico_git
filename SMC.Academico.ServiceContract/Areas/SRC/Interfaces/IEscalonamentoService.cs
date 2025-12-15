using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IEscalonamentoService : ISMCService
    {
        /// <summary>
        /// Busca a lista de escalonamentos pelo sequencial do processo etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial processo etapa</param>
        /// <returns>Select com a lista de escalonamento</returns>
        List<SMCDatasourceItem> BuscarEscalonamentoPorProcessoEtapaSelect(long seqProcessoEtapa);

        /// <summary>
        /// Busca a lista de grupos escalonamento do processo etapa
        /// </summary>
        /// <param name="seq">Objeto de filtro com escalonamento</param>
        /// <returns>Objeto escalonamento com sequenciais de grupo escalonamento</returns>
        ProcessoEtapaProcessamentoListarData BuscarSeqsGrupoEscalonamentosPorProcessoEtapa(long seq);

        /// <summary>
        /// Buscar escalonamentos de um processo organizados por etapas
        /// </summary>
        /// <param name="seqProcesso">Sequencial do Processo</param>
        /// <returns>Lista de escalonamentos de um processo organizado por etapas</returns>
        SMCPagerData<EscalonamentoListarData> BuscarEscalonamentosPorProcesso(EscalonamentoFiltroData filtro);

        /// <summary>
        /// Salvar um novo escalonamento
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        long SalvarEscalonamento(EscalonamentoData modelo);

        /// <summary>
        /// Validar se a data final do escalonametno e maior que a data final do processo e menor
        /// data final periodo financeiro de um respectivo ciclo letivo de um processo, segundo RN_SRC_036 CONSISTÊNCIAS NA INCLUSÃO / ALTERAÇÃO
        /// </summary>
        /// <param name="dataFimEscalonamento">Data final do escalonamento</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa</param>       
        /// <returns>True e False</returns>
        bool ValidarDataFimEscalonamento(long seqProcessoEtapa, DateTime dataFimEscalonamento);

        /// <summary>
        /// Recupera os grupos de escalonamentos de um escalonamento
        /// </summary>
        /// <param name="seqEscalonamento">Sequencial do escalonamento</param>
        /// <returns>Grupos de escalonamentos separados por virgua</returns>
        string RecuperaGrupoEscalonamentoPorEscalonamento(long seqEscalonamento);

        /// <summary>
        /// Verifica se a data fim do escalonamento e a data data fim da parcela são iguais
        /// </summary>
        /// <param name="modelo">Modelo com os dados escalonamento</param>
        /// <returns>True se as parcelas forem iguais</returns>
        bool VerificarDataParcelasDataFimEscalonamento(EscalonamentoData modelo);

        /// <summary>
        /// Buscar escalonamento
        /// </summary>
        /// <param name="seq">Sequencial do escalonamento</param>
        /// <returns>Dados do escalonamento</returns>
        EscalonamentoData BuscarEscalonamento(long seq);

        /// <summary>
        /// Exluir escalonamento selecionado
        /// </summary>
        /// <param name="seq">Sequencial do escalonamento</param>
        void ExlcuirEscalonamento(long seq);

        /// <summary>
        /// Verificar se existem solicitações associadas a um grupo de escalonamento
        /// </summary>
        /// <param name="seqEscalonamento">Sequencial do escalonamento</param>
        /// <param name="dataFimEscalonamento">Data final do escalonamento</param>
        /// <returns>Boleano afirmando se existe solicitação de serviço</returns>
        bool VerificarExisteSolicitacaoServicoGrupoPorEscalonamento(long seqEscalonamento, DateTime dataFimEscalonamento);
    }
}
