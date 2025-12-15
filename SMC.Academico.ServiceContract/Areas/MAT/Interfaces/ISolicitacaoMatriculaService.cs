using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Data.Efetivacao;
using SMC.Academico.ServiceContract.Areas.MAT.Data.SolicitacaoMatricula;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Jobs;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.MAT.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ISolicitacaoMatriculaService : ISMCService
    {
        SolicitacaoMatriculaData BuscarSolicitacaoMatricula(long seqSolicitacaoMatricula);

        CabecalhoMenuMatriculaData BuscarCabecalhoMenu(long seqSolicitacaoMatricula);

        void SalvarCondicaoPagamento(long seqSolicitacaoMatricula, int? seqCondicaoPagamento);

        CondicaoPagamentoSolicitacaoMatriculaData BuscarCondicaoPagamentoSolicitacaoMatricula(long seqSolicitacaoMatricula, bool considerarApenasCondicaoPagamentoSelecionada = false);

        /// <summary>
        /// Buscar as solicitações de matricula que precisam ser chancelada de acordo com o token do serviço
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de matricula</param>
        /// <param name="tokenEtapa">token definido do processo da etapa</param>
        /// <param name="orientacao">identifica se possui orientador</param>
        /// <param name="desativarFiltroDados">Desconsidera o filtro de dados</param>
        /// <returns></returns>
        ChancelaData BuscarSolicitacaoMatriculaChancela(long seq, string tokenEtapa, bool? orientacao, bool desabilitarFiltro = false);

        /// <summary>
        /// Cria as solicitações de renovação para os alunos do processo
        /// </summary>
        /// <param name="rematriculaJOBData">Parâmetros para criação das solicitações de renovação</param>
        void CriarSolicitacoesRematricula(RematriculaJOBData rematriculaJOBData);

        ParcelasPagamentoSolicitacaoMatriculaData BuscarParcelasPagamentoSolicitacaoMatricula(long seqSolicitacaoMatricula);

        SMCUploadFile InserirArquivoTermoAdesao(long seqSolicitacaoMatricula, byte[] arquivoConvertidoPdf);

        BoletoMatriculaData GerarBoletoMatricula(long seqTitulo, long seqServico, long seqSolicitacaoMatricula);

        SMCPagerData<ChancelaItemListaData> BuscarChancelas(ChancelaFiltroData filtro);

        /// <summary>
        /// Busca todos os processos das solicitações que estão em grupos de escalonamentos ativos,
        /// com alguma situação da etapa de chancela ou a situação inicial da etapa posterior para filtro da chancela do orientador
        /// </summary>
        /// <param name="apenasProcessoVigente">Filtro por data vigente no grupo de escalonamento do processo</param>
        /// <returns>Lista de processos para utilizar no filtro da chancela</returns>
        List<SMCDatasourceItem> BuscarProcessosFiltroChancela(bool apenasProcessoVigente);

        /// <summary>
        /// Chancela a solicitação de serviço e dependendo a etapa cria o plano de estudo
        /// </summary>
        /// <param name="chancela">Objeto para ser chancelado</param>
        /// <param name="tokenEtapa">token definido do processo da etapa</param>
        /// <param name="desabilitarFiltro">Desabilita o filtro de HIERARQUIA_ENTIDADE_ORGANIZACIONAL</param>
        void SalvarChancelaMatricula(ChancelaData chancelaData, string token, bool desabilitarFiltro = false);

        List<SMCDatasourceItem> BuscarSituacoesItensChancela(long seqSolicitacao);

        /// <summary>
        /// Realiza o processo de reabrir a chancela voltando as situações dos itens no histórico
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de serviço</param>
        /// <returns>Sequencial da configuração etapa de chancela que reabriu</returns>
        long ReabrirChancelaMatricula(long seq);

        void EfetivarMatricula(EfetivacaoMatriculaData model);

        /// <summary>
        /// Realizar a efetivação da rematricula atualizando o histórico do aluno e os dados do SGP
        /// </summary>
        /// <param name="model">Dados da solicitação para efetivação</param>
        void EfetivarRenovacaoMatricula(EfetivacaoMatriculaData model);

        List<SolicitacaoMatriculaListaData> BuscarSolicitacoesMatriculaLista(SolicitacaoMatriculaFiltroData solicitacaoMatriculaFiltroData);

        /// <summary>
        /// Verifica se existe solicitação de matrícula para solicitação de serviço, se não existir cria um solicitação de matrícula apenas com o sequencial da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        void CriarSolicitacaoMatriculaPorSolicitacaoServico(long seqSolicitacaoServico);

        /// <summary>
        /// Busca o sequencial do processo etapa de acordo com a solicitação de matrícula e a configuração etapa
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial solicitação matrícula</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial configuração etapa</param>
        /// <returns>Retorna o sequencial do processo etapa</returns>
        long BuscarProcessoEtapaPorSolicitacaoMatricula(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa);

        /// <summary>
        /// Buscar ciclo letivo do processor por solicitação matricula 
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Sequencial do ciclo letivo</returns>
        long? BuscarCicloLetivoProcessoSolicitacaoMatricula(long seqSolicitacaoMatricula);

        /// <summary>
        /// RN_MAT_066 - Procedimentos ao finalizar etapa
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração etapa</param>
        /// <param name="classificacaoSituacaoFinal">Classificação da situação final da etapa</param>
        void ProcedimentosFinalizarEtapa(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, ClassificacaoSituacaoFinal classificacaoSituacaoFinal);

        SolicitacaoMatriculaCabecalhoData BuscarCabecalhoMatricula(long seqSolicitacaoServico);

        /// <summary>
        /// Retorna os dados de renovação
        /// </summary>
        /// <param name="filtro">Filtro com o sequencial do aluno logado</param>
        /// <returns>Dados de renovação</returns>
        DadosSolicitacaoMatriculaRenovacaoData BuscarDadosRematricula(DadosSolicitacaoMatriculaRenovacaoFiltroData filtro);

        /// <summary>
        /// Efetivar a matricula de forma automatica
        /// </summary>
        /// <param name="filtro">Fitlro com o sequencial do historico de agendamento para o feedack</param>
        void EfetivarRenovacaoMatriculaAutomatica(EfetivarRenovacaoMatriculaAutomaticaSATData filtro);

        /// <summary>
        /// Verifica se alguma atividade acadêmica da solicitação que esteja com situação finalizada com sucesso e pertença ao plano (ou seja, solicitação de exclusão), possui histórico escolar
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de serviço</param>
        /// <param name="desativarFiltroDados">Desativar filtro de dados ou não</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="seqsSolicitacaoMatriculaItem">Sequencial dos itens da solicitação de matrícula e suas situações selecionadas na tela</param>
        /// <returns>Lista de descrição dos componentes afetados</returns>
        List<string> VerificarChancelaExclusaoAtividadesAcademicasComHistoricoEscolar(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, List<(long, long)> seqsSolicitacaoMatriculaItem, bool desativarFiltroDados);

        /// <summary>
        /// Verifica se alguma atividade acadêmica da solicitação que esteja com situação finalizada com sucesso e possui histórico escolar
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de serviço</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="seqsSolicitacaoMatriculaItem">Sequencial dos itens da solicitação de matrícula e suas situações selecionadas na tela</param>
        /// <returns>Lista de descrição dos componentes afetados</returns>
        List<string> VerificarEfetivacaoAtividadesAcademicasComHistoricoEscolar(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, List<long> seqsSolicitacaoMatriculaItem);

        /// <summary>
        /// Verifica se alguma turma da solicitação que esteja com situação finalizada com sucesso e pertença ao plano (ou seja, solicitação de exclusão), possui histórico escolar
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de serviço</param>
        /// <param name="desativarFiltroDados">Desativar filtro de dados ou não</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="seqsSolicitacaoMatriculaItem">Sequencial dos itens da solicitação de matrícula e suas situações selecionadas na tela</param>
        /// <returns>Lista de descrição dos componentes afetados</returns>
        List<string> VerificarChancelaExclusaoTurmasComHistoricoEscolar(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, List<(long, long)> seqsSolicitacaoMatriculaItem, bool desativarFiltroDados);

        /// <summary>
        /// Verifica se solicitação tem codigo de adesão
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matrícula</param>
        /// <returns></returns>
        bool TemCodigoAdesao(long seqSolicitacaoMatricula);


        /// <summary>
        /// REtorna a lista de turmas selecionadas na graduação
        /// </summary>
        List<TurmaOfertadaData> BuscarTurmasGraduacaoSelecionadas(long seqSolicitacaoMatricula);

        /// <summary>
        /// Exclui uma turma do aluno graduação
        /// </summary>
        void SelecaoPlanoEstudoExcluirTurma(long seqSolicitacaoMatriculaItem, long seqSolicitacaoMatricula);


        /// <summary>
        /// Verifica se é possível prosseguir para da seleção de plano ded estudos para a próxima etapa
        /// </summary>
        void PlanoEstudosConsistirProsseguirEtapa(long seqSolicitacaoMatricula);

        /// <summary>
        /// Retorna a lista de Turmas disponíveis para o Aluno
        /// </summary>
        List<TurmaOfertadaData> RetornaListaTurmasOfertadas(long seqSolicitacaoMatricula, string descricaoTurma = null);

        /// <summary>
        /// Salva as turams selecionadas na modal de seleção de turma no Plano de Estudo
        /// </summary>
        void PlanoEstudoSalvarTurmasSelecionadas(long seqSolicitacaoMatricula, List<long?> seqTurmas);
    }
}