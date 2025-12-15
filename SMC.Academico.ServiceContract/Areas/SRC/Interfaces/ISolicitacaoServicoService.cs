using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data.SolicitacaoReabertura;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ISolicitacaoServicoService : ISMCService
    {
        SMCPagerData<SolicitacaoServicoListarData> ListarSolicitacoesServico(SolicitacaoServicoFiltroData filtro);

        /// <summary>
        /// Busca os motivos dos bloqueios configurados para um processo
        /// </summary>
        /// <param name="SeqsProcessos">Sequenciais dos processos para o filtro</param>
        /// <returns>Lista de motivos de bloqueios encontrados</returns>
        List<SMCDatasourceItem> BuscarBloqueiosDoProcessoSelect(List<long> seqsProcessos);

        /// <summary>
        /// Busca os dados do cabeçalho de atendimento de uma solicitação padrão
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados do cabeçalho</returns>
        CabecalhoAtendimentoPadraoData BuscarDadosCabecalhoAtendimentoPadrao(long seqSolicitacaoServico);

        /// <summary>
        /// Realizar o processamento de plano de estudo das solicitações de serviços listada
        /// </summary>
        /// <param name="processamento">Objeto de processamento utilizado via WebApi</param>
        void ProcessamentoPlanoEstudoServicoMatricula(ProcessamentoPlanoEstudoSATData processamento);

        /// <summary>
        /// Buscar os usuários que possuem solicitações de serviços atribuidas
        /// </summary>
        /// <returns>Lista de usuários</returns>

        List<SMCDatasourceItem> BuscarUsuariosSolicitacoesAtribuidasSelect();

        /// <summary>
        /// Busca as situações de matricula de acordo com o tipo de atuação informado
        /// </summary>
        /// <param name="tipoAtuacao">Tipo de atuação informado</param>
        /// <returns>Lista de situações de matrícula encontradas</returns>
        List<SMCDatasourceItem> BuscarSituacoesMatriculaPorTipoAtuacaoSelect(TipoAtuacao tipoAtuacao);

        /// <summary>
        /// Busca as situações de documentação pelo sequencial do processo
        /// </summary>
        /// <param name="seqsProcessos">Sequenciais dos processos</param>
        /// <returns>Lista com as situações de documentação</returns>
        List<SMCDatasourceItem> BuscarSituacoesDocumentacaoDoProcessoSelect(List<long> seqsProcessos);

        /// <summary>
        /// Atribui o usuário atual como responsável pelo atendimento desta solicitação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Solicitação a ser atribuída</param>
        void AtualizarUsuarioResponsavelAtendimento(long seqSolicitacaoServico);

        /// <summary>
        /// Busca os dados se identificação do solicitante de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados de identificação do solicitante</returns>
        DadosSolicitacaoData BuscarDadosIdentificacaoSolicitante(long seqSolicitacaoServico);

        /// <summary>
        /// Cancela uma solicitação (pelo próprio solicitante)
        /// </summary>
        /// <param name="modelo">Modelo contendo os dados de cancelamento da solicitação</param>
        void SalvarCancelamentoSolicitacao(CancelamentoSolicitacaoData modelo);

        /// <summary>
        /// Busca os historicos de uma solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Históricos de uma solicitação de serviços</returns>
        DadosSolicitacaoData BuscarHistoricosSolicitacao(long seqSolicitacaoServico, long seqPessoaAtuacao);

        /// <summary>
        /// Busca as notificações de uma solicitação de serviços
        /// </summary>
        /// <param name="filtro">filtro a ser usado na consulta</param>
        /// <returns>Notificações de uma solicitação de serviços</returns>
        DadosSolicitacaoData BuscarNotificacoesSolicitacao(NotificacaoSolicitacaoFiltroData filtro);

        /// <summary>
        /// Busca os documentos de uma solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Documentos de uma solicitação de serviços</returns>
        DadosSolicitacaoData BuscarDocumentosSolicitacao(long seqSolicitacaoServico, long seqPessoaAtuacao);

        /// <summary>
        /// Cria uma nova solicitação para a pessoa atuação em questão, referente ao serviço informado
        /// </summary>
        /// <param name="model">Dados para criar nova solicitação</param>
        /// <returns>Dados da solicitação criada</returns>
        DadosNovaSolicitacaoServicoData CriarSolicitacaoServico(CriarSolicitacaoData model);

        /// <summary>
        /// Cria as solicitações de prorrogação manualmente
        /// </summary>
        /// <param name="codigosMigracao">Codigos de migração dos alunos
        /// </param>
        List<long> CriarSolicitacoesProrrogacaoManual(List<long> codigosMigracao);

        /// <summary>
        /// Busca uma solicitação de serviço pelo sequencial
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de serviço</param>
        /// <returns>Solicitação de serviço</returns>
        SolicitacaoServicoData BuscarSolicitacaoServico(long seq);

        /// <summary>
        /// Busca o totalizador de solicitações de serviço de um determinado
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação relacionada</param>
        /// <returns>Totalizador de solicitações da pessoa atuação</returns>
        TotalizadorSolicitacaoServicoData BuscarTotalizadorSolicitacoesServico(long seqPessoaAtuacao);

        /// <summary>
        ///
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        SMCPagerData<SolicitacaoServicoPessoaAtuacaoListaData> BuscarSolicitacoesPessoaAtuacao(SolicitacaoServicoPessoaAtuacaoFiltroData filtro);

        /// <summary>
        /// Busca os dados do solicitante de uma solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitante">Sequencial do solicitante</param>
        /// <returns>Dados do solicitante da solicitação de serviços</returns>
        DadosSolicitanteData BuscarDadosSolicitante(long seqPessoaAtuacao);

        /// <summary>
        /// Busca os historicos de navegação de uma solicitacao serviço / etapa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitacao de serviço</param>
        /// <param name="seqSolicitacaoServicoEtapa">Sequencial da etapa da solicitacao de serviço</param>
        /// <returns>Historicos de navegação da solicitacao de servico / etapa informada</returns>
        HistoricoNavegacaoData BuscarHistoricosNavegacao(long seqSolicitacaoServico, long seqSolicitacaoServicoEtapa);

        /// <summary>
        /// Busca os dados para tela de confirmação de solicitação padrão
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados da solicitação</returns>
        DadosConfirmacaoSolicitacaoPadraoData BuscarDadosConfirmacaoSolicitacaoPadrao(long seqSolicitacaoServico);

        /// <summary>
        /// Busca os bloqueios de uma solicitacao serviço / etapa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitacao de serviço</param>
        /// /// <param name="seqSolicitacaoServicoEtapa">Sequencial da solicitacao de serviço / etapa</param>
        /// <returns>Bloqueios da solicitacao de servico / etapa informada</returns>
        BloqueioEtapaSolicitacaoData BuscarBloqueiosEtapaSolicitacao(long seqSolicitacaoServico, long seqSolicitacaoServicoEtapa);

        /// <summary>
        /// Busca os detalhes de uma notificação
        /// </summary>
        /// <param name="seqNotificacaoEmail">Sequencial da notificação</param>
        /// <param name="seqTipoNotificacao">Sequencial do tipo de notificação</param>
        /// <returns>Detalhes da notificação</returns>
        DetalheNotificacaoSolicitacaoData BuscarDetalheNotificacaoSolicitacao(long seqNotificacaoEmail, long? seqTipoNotificacao = null, long? seqSolicitacaoServico = null);

        /// <summary>
        /// Envia notificação de um solicitação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação serviço</param>
        /// <param name="tokenTipoNotificacao">Token do tipo de notificação</param>
        /// <returns></returns>
        void EnviarNotificacaoSolicitacao(long seqSolicitacaoServico, string tokenTipoNotificacao = null);

        /// <summary>
        /// Envia notificação de um solicitação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação serviço</param>
        /// <param name="seqNotificacaoEmailDestinatario">Sequencial da notificação email destinatário</param>
        /// <returns></returns>
        void ReenviarNotificacaoSolicitacao(long seqSolicitacaoServico, long seqNotificacaoEmailDestinatario, PermiteReenvio permiteReenvio);

        /// <summary>
        /// Prepara o modelo para as operações com a solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviços</param>
        /// <returns>Modelo com as validações realizadas para realização das operações com a solicitação de servciço</returns>
        DadosSolicitacaoData PrepararModeloSolicitacaoServico(long seqSolicitacaoServico);

        /// <summary>
        /// Recupera os dados da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados da solicitação</returns>
        DadosSolicitacaoPadraoData BuscarDadosSolicitacaoPadrao(long seqSolicitacaoServico);

        /// <summary>
        /// Busca os dados da solicitação para exibir durante o parecer do atendimento
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados da solicitação</returns>
        DadosParecerData BuscarDadosParecer(long seqSolicitacaoServico);

        /// <summary>
        /// Verifica se solicitante possui alguma pendencia financeira
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço a ser verificada</param>
        /// <returns>TRUE caso exista pendencia financeira e FALSE caso contrário</returns>
        bool ValidarNadaConstaFinanceiro(long seqSolicitacaoServico);

        /// <summary>
        /// Realiza um atendimento
        /// </summary>
        /// <param name="seqSolicitacaoServico">Solicitação de serviço a ser realizada</param>
        /// <param name="situacao">Situação</param>
        /// <param name="parecer">Parecer do atendimento</param>
        void RealizarAtendimento(long seqSolicitacaoServico, bool? situacao, string parecer);

        /// <summary>
        /// Salva a justificativa de uma solicitação de serviço
        /// </summary>
        /// <param name="dadosSolicitacaoPadraoData">Dados a serem salvos</param>
        void SalvarJustificativaSolicitacao(DadosSolicitacaoPadraoData dadosSolicitacaoPadraoData);

        /// <summary>
        /// Busca os dados de cabecalho para cancelamento da solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados do cabeçalho para cancelamento da solicitação de serviços</returns>
        CabecalhoCancelamentoSolicitacaoData BuscarDadosCabecalhoCancelamentoSolicitacao(long seqSolicitacaoServico);

        /// <summary>
        /// Busca os dados de cabecalho para reabertura da solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados do cabeçalho para reabertura da solicitação de serviços</returns>
        CabecalhoReaberturaSolicitacaoData BuscarDadosCabecalhoReaberturaSolicitacao(long seqSolicitacaoServico);

        /// <summary>
        /// Buscar as situações de cancelamento de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Lista com as situações de cancelamento</returns>
        List<SMCDatasourceItem> BuscarSituacoesCancelamentoSolicitacaoSelect(long seqSolicitacaoServico);

        List<SMCDatasourceItem> BuscarMotivosCancelamentoSolicitacaoSelect(long seqSolicitacaoServico);

        bool ValidarMotivoCancelamentoSolicitacao(long seqSolicitacaoServico);

        bool ValidarTipoCancelamentoSolicitacao(long seqSolicitacaoServico);

        DadosFormularioSolicitacaoPadraoData BuscarDadosFormularioSolicitacaoPadrao(long seqSolicitacaoServico, long seqConfiguracaoEtapaPagina);

        DadosFinaisSolicitacaoPadraoData BuscarDadosFinaisSolicitacaoPadrao(long seqSolicitacaoServico, long? seqConfiguracaoEtapa = null);

        void SalvarDadosFormularioSolicitacaoPadrao(DadosFormularioSolicitacaoPadraoData dados);

        /// <summary>
        /// Realiza a reabertura de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço a ser reaberta</param>
        /// <param name="observacao">Observação da reabertura</param>
        void ReabrirSolicitacao(long seqSolicitacaoServico, string observacao);

        /// <summary>
        /// Efetua a finalização da etapa de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa-atuação</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração de etapa</param>
        void SalvarConfirmacaoSolicitacaoPadrao(long seqSolicitacaoServico, long seqPessoaAtuacao, long seqConfiguracaoEtapa);

        /// <summary>
        /// Buscar as solicitações de serviço da pessoa atuação e tipo de documento requerido
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqTipoDocumento">Sequencial do tipo de documento</param>
        /// <returns>Lista das solicitações de serviço encontradas</returns>
        List<SMCDatasourceItem> BuscarSolicitacoesPessoaAtuacaoTipoDocumentoSelect(long seqPessoaAtuacao, long seqTipoDocumento);

        /// <summary>
        /// Busca os dados para realizar o atendimento de reabertura de matrícula
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados complementares</returns>
        AtendimentoReaberturaData BuscarDadosAtendimentoReabertura(long seqSolicitacaoServico);

        /// <summary>
        /// Busca os dados para realizar o atendimento de uma solicitação de intercâmbio
        /// </summary>
        AtendimentoIntercambioData BuscarDadosIniciaisAtendimentoIntercambio(long seqSolicitacaoServico);

        /// <summary>
        /// Busca os dados para realizar o atendimento de uma solicitação de intercâmbio
        /// </summary>
        AtendimentoIntercambioData BuscarDadosTermoAtendimentoIntercambio(long seqSolicitacaoServico, long seqTipoTermoIntercambio);

        /// <summary>
        /// Busca os dados de orientadores de um atendimento de intercâmbio
        /// </summary>
        AtendimentoIntercambioData BuscarDadosOrientacaoTermoAtendimentoIntercambio(long seqSolicitacaoServico, long seqTipoOrientacao, long seqTipoTermoIntercambio, long seqTermoIntercambio);

        /// <summary>
        /// Verifica se a solicitação possui todos os documentos obrigatórios.
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação.</param>
        /// <param name="seqConfiguracaoEtapa">Configuração da etapa</param>
        void SalvarDocumentosSolicitacao(long seqSolicitacaoServico, long seqConfiguracaoEtapa);

        /// <summary>
        /// Busca a solicitação de atividade complementar.
        /// </summary>
        SolicitacaoAtividadeComplementarPaginaData BuscarSolicitacaoAtividadeComplementar(long seqSolicitacao);

        /// <summary>
        /// Salva a solicitação de atividade complementar.
        /// </summary>
        void SalvarSolicitacaoAtividadeComplementar(SolicitacaoAtividadeComplementarPaginaData model);

        /// <summary>
        /// Atualiza o campo entidade compartilhada da solicitação de serviço, utilizado para disciplina eletiva com o valor do grupo de programa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqEntidadeCompartilhada">Sequencial da entidade complartilhada (grupo de programa)</param>
        void AtualizarSolicitacaoServicoEntidadeCompartilhada(long seqSolicitacaoServico, long seqEntidadeCompartilhada);

        /// <summary>
        /// Salva os dados informados no atendimento de uma solicitação de reabertura
        /// </summary>
        /// <param name="atendimentoReaberturaData">Dados</param>
        void SalvarDadosAtendimentoReabertura(AtendimentoReaberturaData atendimentoReaberturaData);

        /// <summary>
        /// Salva os dados da realização do atendimento de solicitação de intercâmbio
        /// </summary>
        /// <param name="atendimentoIntercambioData">Dados do atendimento</param>
        void SalvarDadosAtendimentoIntercambio(AtendimentoIntercambioData atendimentoIntercambioData);

        /// <summary>
        /// Verifica se existe algum serviço em aberto que conflita com o serviço passado como parâmetro
        /// </summary>
        /// <param name="seqServico">Sequencial do serviço a ser validado</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Lista com as restrições encontradas</returns>
        List<SolicitacaoDispensaRestricaoSolicitacaoSimultaneaData> BuscarRestricoesSolicitacaoSimultanea(long seqServico, long seqPessoaAtuacao);

        /// <summary>
        /// Busca as situações futuras do aluno, considerando uma data de referencia.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="dataReferencia">Data de referencia para verificar situações futuras</param>
        /// <returns>Lista com as situações futuras encontradas</returns>
        List<SolicitacaoDispensaSituacaoFuturaAlunoData> BuscarSituacoesFuturasAluno(long seqPessoaAtuacao, DateTime dataReferencia);

        /// <summary>
        /// Retorna a etapa atual de uma solicitação de serviço para a etapa anterior
        /// </summary>
        /// <param name="seqSolicitacaoServico"></param>
        /// <returns>sequencial da solicitação de serviço</returns>
        long RetornarSolicitacaoParaEtapaAnterior(long seqSolicitacaoServico);

        /// <summary>
        /// Busca os dados da solicitação de serviço para detalhes da modal de acordo com sequencial ou protocolo
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="protocolo">Número do protocolo</param>
        /// <returns>Objeto de solicitacao de serviço simplificado para modal</returns>
        DadosModalSolicitacaoServicoData BuscarDadosModalSolicitacaoServico(long? seqSolicitacaoServico, string protocolo);

        /// <summary>
        /// Busca os sequenciais das etapas de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação</param>
        /// <returns>Dados da solicitação</returns>
        List<long> BuscarSeqsConfiguracoesEtapaSolicitacao(long seqSolicitacaoServico);

        /// <summary>
        /// Busca o campo justificativa complementar da solicitação de serviço
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de serviço</param>
        /// <returns>Justificativa complementar</returns>
        string BuscarSolicitacaoServicoJustificativaComplementar(long seq);

        /// <summary>
        /// Atualiza o campo justificativa complementar da solicitação de serviço, utilizado para disciplina eletiva com o valor do grupo de programa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="justificativa">Justificativa complementar da solicitação de serviço</param>
        void AtualizarSolicitacaoServicoJustificativaComplementar(long seqSolicitacaoServico, string justificativa);

        BotoesSolicitacaoData GerarBotoesSolicitacao(long seqSolicitacaoServico);

        bool VerificarConfiguracaoPossuiSolicitacaoServicoEmAberto(long seqConfiguracaoProcesso);

        SolicitacaoCobrancaTaxaData PrepararModeloSolicitacaoCobrancaTaxa(long seqSolicitacaoServico);

        void AtualizarSolicitacaoServicoTipoEmissaoTaxa(long seqSolicitacaoServico, TipoEmissaoTaxa tipoEmissaoTaxa);

        long ProcedimentosReemissaoTitulo(long seqTitulo, long seqTaxa, long seqServico, long seqSolicitacaoServico);

        bool VerificarConfiguracaoPossuiSolicitacaoServico(long seqConfiguracaoProcesso);

        bool VerificarProcessoPossuiSolicitacaoServico(long seqProcesso);

        List<DadosRelatorioSolicitacoesBloqueioData> BuscarDadosRelatorioSolicitacoesBloqueio(RelatorioSolicitacoesBloqueioFiltroData filtro);

        string BuscarTokenEtapaAtualSolicitacao(long seqSolicitacaoServico);

        /// <summary>
        /// Buscar o usuário SAS da pessoa atuação da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacao">Sequencial da solicitação</param>
        /// <returns>Sequencial do usuário sAS da pessoa atuação da solicitaçãod e serviço</returns>
        long BuscarSeqUsuarioSASSolicitacaoServico(long seqSolicitacao);

        /// <summary>
        /// Atualizar o termo de entrega da documentação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequecial da solicitação de serviços</param>
        /// <param name="compromissoEntregaDocumentacao">Falg de compormisso de entrega da documentação</param>
        void AtualizarTermoEntregaDocumentacao(long seqSolicitacaoServico, bool? compromissoEntregaDocumentacao);

        /// <summary>
        /// Buscar se o termo de entraga de documentação foi aceito
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitacao serviço</param>
        /// <returns></returns>
        bool BuscarTermoEntregaDocumentacaoFoiAceito(long seqSolicitacaoServico);

        string BuscarDescricaoTipoDocumento(long seqTipoDocumento);
        void SalvarSelecaoComponenteCurricular(long seqSolicitacaoServico, long seqDivisaoComponente);

    }
}