using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Data.Aluno;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IAlunoService : ISMCService
    {
        /// <summary>
        /// Busca os alunos com as depêndencias apresentadas na listagem do seu cadastro
        /// </summary>
        /// <param name="filtros">Filtros do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        SMCPagerData<AlunoListaData> BuscarAlunos(AlunoFiltroData filtros);

        /// <summary>
        /// Busca os alunos com as depêndencias apresentadas na consulta detalhada
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno</returns>
        AlunoListaData BuscarAlunoVisualizacaoDados(long seq);

        /// <summary>
        /// Grava alterações dos dados pessoais de um aluno
        /// </summary>
        /// <param name="aluno">Dados pessoais do aluno a ser atualizado</param>
        /// <returns>Sequencial do aluno atualizado</returns>
        /// <exception cref="AtuacaoSemTelefoneException">Caso não seja informado nenhum telefone</exception>
        long SalvarAluno(AlunoData aluno);

        /// <summary>
        /// Busca um aluno com seus dados pessoais
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno</returns>
        AlunoData BuscarAluno(long seq);

        /// <summary>
        /// Busca um aluno com seus dados pessoais para mobile
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno</returns>
        [OperationContract]
        ///[WebGet(UriTemplate = "/areas/aln/alunoservice/buscaraluno/{seq}", ResponseFormat = WebMessageFormat.Json)]
        AlunoData BuscarAlunoMobile(long seq);

        /// <summary>
        /// Retorna o nível de ensino atual do aluno.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno.</param>
        /// <returns>Nível de ensino do aluno informado.</returns>
        NivelEnsinoData BuscarNivelEnsinoAluno(long seqPessoaAtuacao);

        /// <summary>
        /// Busca os alunos para emissão de relatórios
        /// </summary>
        /// <param name="filtro">Filtro a ser considerado na pesquisa</param>
        /// <returns>Lista de alunos para emissão de relatórios</returns>
        SMCPagerData<RelatorioListarData> BuscarAlunosRelatorio(RelatorioFiltroData filtro, bool buscarDescricaoCicloLetivoIngresso = false);

        /// <summary>
        /// Busca os alunos para emissão da identidade estudantil pelos seqs informados
        /// </summary>
        /// <param name="seqsAlunos">Seqs dos alunos para pesquisa</param>
        /// <returns>Lista de alunos para emissão da identidade estudantil</returns>
        List<RelatorioIdentidadeEstudantilData> BuscarAlunosIdentidadeEstudantil(List<long> seqsAlunos);

        /// <summary>
        /// Busca aluno para emissão da identidade estudantil pelos seqs informados, sem paginação
        /// </summary>
        /// <param name="seqAluno"></param>
        /// <returns></returns>
        [OperationContract]
        IdentidadeEstudantilData BuscarIdentidadeEstudantil(long seqAluno);

        /// <summary>
        /// Buscar dados da matricula do aluno
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        [OperationContract]
        ConsultaDadosAlunoData BuscarDadosMatriculaAluno(long seq);

        /// <summary>
        /// Busca os dados relativos a matricula dos alunos passados no parametro. Obs.: utilizado para gerar
        /// relatório de declaração de matrícula
        /// </summary>
        /// <param name="seqPessoaAtuacao"></param>
        /// <returns></returns>
        List<ItemDeclaracaoMatriculaData> BuscarItemsDeclaracaoMatricula(long[] seqPessoaAtuacao, long SeqCicloLetivo);

        /// <summary>
        /// Retorna a lista de alunos como select para ser usado em combo
        /// </summary>
        /// <param name="alunoFiltroData">Filtros a serem utilizados</param>
        /// <param name="exibirVinculo">Flag para exibir ou não o vinculo do aluno</param>
        /// <param name="carregarVinculoAtivo">Inclui se o vínculo está ativo ou não no DataAttributes</param>
        /// <returns>Lista de alunos</returns>
        [OperationContract]
        List<SMCDatasourceItem> BuscarAlunosSelect(AlunoFiltroData alunoFiltroData, bool exibirVinculo = false, bool carregarVinculoAtivo = false);

        /// <summary>
        /// Recupera os dados de integração com o SGA antigo para fazer a requisição ao sistema antigo e recuperar o conteúdo html
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados de integração</returns>
        AlunoDadosIntegracaoData BuscarDadosIntegracaoSGAAntigo(long seq);

        /// <summary>
        /// Retorna os dados do ingressante que deu origem ao aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Dados do ingressante</returns>
        AlunoDadosIngressanteData BuscarDadosIngressanteAluno(long seqAluno);

        /// <summary>
        /// Buscar os dados das parcelas em aberto do aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <returns>Dados de parcela em aberto</returns>
        List<EmitirBoletoAbertoData> BuscarParcelasPagamentoEmAberto(long seqPessoaAtuacao);

        /// <summary>
        /// Gerar o boleto de acordo com o título e o serviço no portal do aluno
        /// </summary>
        /// <param name="seqTitulo">Sequencial do titulo do boleto</param>
        /// <param name="seqServico">Sequencial do serviço</param>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno solicitante</param>
        /// <returns></returns>
        AlunoEmitirBoletoData GerarBoletoAluno(long seqTitulo, long seqServico, long seqPessoaAtuacao);

        /// <summary>
        /// Busca as disciplinas cursadas para cada aluno passado como parametro
        /// </summary>
        /// <param name="filtro">Filtro com dos dados para busca</param>
        /// <returns>Disciplinas cursadas dos alunos informados</returns>
        List<RelatorioDisciplinasCursadasData> RelatorioDisciplinasCursadas(RelatorioDisciplinasCursadasFiltroData filtro);

        /// <summary>
        /// Emisão de relatório de histórico escolar.
        /// </summary>
        /// <param name="seqsAlunos">Lista de sequenciais dos alunos (PessoaAtuacao).</param>
        /// <returns>Data com dados dos alunos informados para montar os relatórios de cada aluno.</returns>
        List<RelatorioHistoricoEscolarData> RelatorioHistoricoEscolar(List<long> seqsAlunos, bool? compCurriculaSemCreditos, bool? exibirMediaNotas);

        /// <summary>
        /// Emisão de relatório de histórico escolar interno.
        /// </summary>
        /// <param name="seqsAlunos">Lista de sequenciais dos alunos (PessoaAtuacao).</param>
        /// <returns>Data com dados dos alunos informados para montar os relatórios de cada aluno.</returns>
        List<RelatorioHistoricoEscolarData> RelatorioHistoricoEscolarInterno(List<long> seqsAlunos);

        /// <summary>
        /// Busca o registro acadêmico para comprovante de processo
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <returns>Número do registro acadêmico</returns>
        long BuscarRegistroAcademicoAluno(long seqPessoaAtuacao);

        /// <summary>
        /// Busca os dados do aluno para o header BI_ALN_001 - Aluno - Cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno para o header</returns>
        AlunoCabecalhoData BuscarAlunoCabecalho(long seq);

        /// <summary>
        /// Busca o cliclo letivo atual de um aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Ciclo letivo atual</returns>
        long BuscarCicloLetivoAtual(long seqAluno);

        /// <summary>
        /// Busca o cliclos letivos do aluno histórico de um aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Ciclos letivos encontrados</returns>
        List<SMCDatasourceItem> BuscarCiclosLetivosAlunoHistoricoSelect(long seqAluno);

        /// <summary>
        /// Busca o código de migração do aluno no GRA
        /// </summary>
        /// <param name="seqPessoaAtuacao"></param>
        /// <returns></returns>
        [OperationContract]
        long BuscarCodigoMigracaoAluno(long seqPessoaAtuacao);

        /// <summary>
        /// Realizar o filtro para relatório de alunos por situação de matrícula.
        /// </summary>
        /// <param name="filtros">Filtros da pesquisa</param>
        /// <returns>Lista de alunos para o relatório</returns>
        List<RelatorioAlunosPorSituacaoData> BuscarDadosRelatorioAlunosPorSituacao(RelatorioAlunosPorSituacaoFiltroData filtros);

        /// <summary>
        /// Realizar o filtro para relatório de alunos por componente.
        /// </summary>
        /// <param name="filtros">Filtros da pesquisa</param>
        /// <returns>Lista de alunos para o relatório</returns>
        List<RelatorioAlunosPorComponenteListaData> BuscarDadosRelatorioAlunosPorComponente(RelatorioAlunosPorComponenteFiltroData filtros);

        /// <summary>
        /// Cancela uma matrícula.
        /// </summary>
        string CancelarMatricula(CancelarMatriculaAlunoData data);

        /// <summary>
        /// Verifica nada consta financeiro
        /// </summary>
        /// <param name="codigoAlunoMigracao">Código do aluno de migração</param>
        /// <param name="dataVerificacao">Data da verificação</param>
        /// <returns></returns>
        bool ValidarNadaConstaFinanceiro(int codigoAlunoMigracao, DateTime dataVerificacao);

        SituacaoAlunoData BuscarSituacaoAtual(long seqAluno, bool desativarFiltroDados);

        /// <summary>
        /// Buscar os dados do aluno para abertura de chamado de suporte técnico
        /// </summary>
        /// <param name="seqAluno">Sequencial da pessoa atuação do aluno</param>
        /// <returns>Dados para abertura de chamados de suporte tecnico</returns>
        AlunoDadosSuporteTecnicoData BuscarDadosSuporteTecnico(long seqAluno);

        /// <summary>
        /// Recupera o código de estabelecimento de um aluno 
        /// </summary>
        /// <param name="seqAluno">Sequencial da pessoa atuação do aluno</param>
        /// <returns>Recupera o código de estabelecimento de um aluno</returns>
        [OperationContract]
        string BuscarCodigoEstabelecimentoAluno(long seqAluno);

        /// <summary>
        /// Chama a procedure ALN.st_carga_sincronismo_aluno_graduacao para sincronizar os dados do aluno
        /// </summary>
        /// <param name="codigoAlunoMigracao">Código do aluno migração a ser sincronizado</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuacao</param>
        /// <returns>Sequencial da pessoa atuação que foi sincronizada</returns>
        long SincronizarDadosAluno(long codigoAlunoMigracao, long seqPessoaAtuacao);

        List<PrevisaoConclusaoOrientacaoRelatorioData> BuscarPrevisaoConclusaoOrientacaoRelatorio(RelatorioPrevisaoConclusaoOrientacaoFiltroData filtro);

        /// <summary>
        /// Metodo que gera os dados para buscar os dados do arquivo de texto com o codigo dos alunos.
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        List<int> BuscarDadosArquivoSMDAlunosPorSituacao(RelatorioAlunosPorSituacaoFiltroData filtros);

        /// <summary>
        /// Sincroniza alunos do legado com o academico somente suando o codigo de migração
        /// </summary>
        /// <param name="codigoAlunoMigracao"></param>
        /// <returns></returns>
        long SincronizarDadosALunoSomenteComCodigoMigracao(int codigoAlunoMigracao);

        /// <summary>
        /// Encaminha documentos para a assinatura no GAD
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        void EnviarParaAssinatura(ConfigurarRelatorioDeclaracaoGenericaData dados);

        ConfigurarRelatorioDeclaracaoGenericaData ConfigurarRelatorioDeclaracaoGenerica(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico, SMCLanguage idiomaDocumentoAcademico, long seqAluno);

        byte[] BuscarRelatorioGenerico(ConfigurarRelatorioDeclaracaoGenericaData dados);

    }
}