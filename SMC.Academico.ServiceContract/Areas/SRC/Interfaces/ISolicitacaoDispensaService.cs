using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ISolicitacaoDispensaService : ISMCService
    {
        SolicitacaoDispensaItensDispensadosData PrepararModeloSolicitacaoDispensaItensDispensados(long seqPessoaAtuacao, long seqSolicitacaoServico);

        void SalvarSolicitacaoDispensaItensDispensados(SolicitacaoDispensaItensDispensadosData modelo);

        SolicitacaoDispensaItensCursadosData PrepararModeloSolicitacaoDispensaItensCursados(long seqPessoaAtuacao, long seqSolicitacaoServico);

        void SalvarSolicitacaoDispensaItensCursados(SolicitacaoDispensaItensCursadosData modelo);

        /// <summary>
        /// Verifica se existe solicitação de dispensa para solicitação de serviço, se não existir cria um solicitação de dispensa apenas com o sequencial da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        void CriarSolicitacaoDispensaPorSolicitacaoServico(long seqSolicitacaoServico);

        /// <summary>
        /// Busca os itens para o datasource dos grupos de dispensa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de dispensa</param>
        /// <param name="seqGrupo">Sequencial do grupo que está sendo editado. Informar null caso seja inclusão</param>
        /// <returns>Itens de datasource</returns>
        AtendimentoDispensaAgrupamentoItensData BuscarItensAgrupamentoAtendimentoDispensa(long seqSolicitacaoServico, long? seqGrupo);

        /// <summary>
        /// Salva um grupo de dispensa
        /// </summary>
        /// <param name="dados">Grupo de dispensa a ser salvo</param>
        void SalvarAtendimentoDispensaAgrupamentoGrupo(SolicitacaoDispensaGrupoData dados);

        /// <summary>
        /// Retorna os dados de agrupamento de uma solicitação de dispensa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço de dispensa</param>
        /// <returns>Dados do agrupamento</returns>
        AtendimentoDispensaAgrupamentoData BuscarDadosAgrupamentoAtendimentoDispensa(long seqSolicitacaoServico);

        /// <summary>
        /// Busca os dados de um grupo para edição
        /// </summary>
        /// <param name="seqGrupo">Sequencial do grupo a ser buscado</param>
        /// <returns>Dados do grupo recuperado</returns>
        SolicitacaoDispensaGrupoData BuscarDadosGrupoAgrupamentoAtendimentoDispensa(long seqGrupo);

        /// <summary>
        /// Remove um grupo de um atendimento de dispensa
        /// </summary>
        /// <param name="seqGrupo"></param>
        void RemoverGrupoAgrupamentoAtendimentoDispensa(long seqGrupo);

        /// <summary>
        /// RN_SRC_089 - Solicitação - Criação automática de grupos de itens de dispensa
        /// Verifica se existe apenas 1 item cursado (interno ou externo) e também só 1 item a ser dispensado.
        /// Nesse caso, já cria o agrupamento, verificando também se esses itens formam uma equivalência.
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        void VerificarAgrupamentoAutomatico(long seqSolicitacaoServico);

        /// <summary>
        /// Faz as validações para prosseguir com a criação de grupos para o atendimento de dispensa individual
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        void SalvarAtendimentoDispensaAgrupamento(long seqSolicitacaoServico);

        /// <summary>
        /// Verifica se é uma solicitação de dispensa pelo token e se na dispensa destino possui a flag item_excluido_plano
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns></returns>
        bool VerificarReabrirDispensaItemPlano(long seqSolicitacaoServico);

        /// <summary>
        /// Verificar se existe item no plano de estudo e sem historico lançado
        /// </summary>
        /// <param name="seqSolicitacaoDispensa">Sequencial da Solicitação de Serviço de Dispensa</param>
        /// <returns>Lista de Plano de Estudo Item sem histórico lançado</returns>
        List<PlanoEstudoItemSemHistoricoData> VerificarTurmasPlanoEstudoSemHistorico(long seqSolicitacaoDispensa);
    }
}