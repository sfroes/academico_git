using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.SGA.Administrativo.Areas.TUR.Data;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.TUR.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ITurmaService : ISMCService
    {
        /// <summary>
        /// O campo turma é composto do "Código da turma" + "." + "Número da turma".
        /// O campo código da turma deverá ser um campo sequencial;
        /// O campo número da turma deverá ser um sequencial começando sempre de 1 por código da turma.
        /// </summary>
        /// <returns>"Código da turma" + "." + "Número da turma"</returns>
        string GerarCodigoNumeroTurma();

        /// <summary>
        /// Buscar a turma
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="buscarDivisoesComponenteComDivisoesTurmaAssociadas"></param>
        /// <returns>Objeto turma com seus itens</returns>
        TurmaData BuscarTurma(long seq, bool buscarDivisoesComponenteComDivisoesTurmaAssociadas = false);

        /// <summary>
        /// Buscar cabeçalho com os dados de turma
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Objeto com dados da turma</returns>
        TurmaCabecalhoData BuscarTurmaCabecalho(long seq);

        /// <summary>
        /// Busca as turmas de acordo com os filtros
        /// </summary>
        /// <param name="filtros">Objeto turma filtro</param>
        /// <returns>SMCPagerData com a lista de turmas detalhadas</returns>
        SMCPagerData<TurmaListarData> BuscarTurmas(TurmaFiltroData filtros);

        /// <summary>
        /// Busca a turma para o retorno do lookup
        /// </summary>
        /// <param name="seq">Sequencial da turma selecionada</param>
        /// <returns>Dados da turma</returns>
        TurmaListarData BuscarTurmaLookup(long seq);

        /// <summary>
        /// Grava uma turma com seus respectivos itens
        /// </summary>
        /// <param name="turma">Dados da turma a ser gravado</param>
        /// <returns>Sequencial da turma gravado</returns>
        long SalvarTurma(TurmaData turma);

        /// <summary>
        /// Rotina para realizar a carga de agendas de turma
        /// </summary>
        /// <param name="seqCicloLetivo">Ciclo letivo a ser realizada a carga</param>
        void CargaAgendasTurma(long seqCicloLetivo);

        /// <summary>
        /// Busca o sequencial da instituição de ensino da turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Sequencial da Instituição de ensino</returns>
        long BuscarSeqInstituicaoEnsinoTurma(long seqTurma);

        /// <summary>
        /// Busca as turmas de acordo com a pessoa atuação processo de matrícula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <param name="seqProcesso">Sequencial processo</param>
        /// <param name="seqGrupoPrograma">Sequencial do grupo de programa para disciplina eletiva</param>
        /// <param name="tokenServico">Token do Serviço</param>
        /// <returns>Lista de turmas detalhadas</returns>
        SMCPagerData<TurmaMatriculaListarData> BuscarTurmasPessoaAtuacao(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long? seqProcesso, long? seqGrupoPrograma, string tokenServico);

        /// <summary>
        /// Busca as turmas de acordo com o aluno logado
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo caso queira filtrar</param>
        /// <param name="apenasMatriculado">Recuperar apenas as turmas em que o aluno está com o status de matriculado</param>
        /// <returns>Lista de turmas detalhadas</returns>
        [OperationContract]
        List<TurmaListarGrupoCursoData> BuscarTurmasAluno(long seq, long? seqCicloLetivo, bool? apenasMatriculado);

        /// <summary>
        /// Busca as turmas de acordo com o professor responsável ou professor da divisão
        /// </summary>
        /// <param name="seqProfessor">Sequencial do professor</param>
        /// <returns>Lista de turmas detalhadas</returns>
        List<TurmaListarGrupoCursoData> BuscarTurmasProfessor(long seqProfessor, bool? turmaSituacaoCancelada = null);

        /// <summary>
        /// Busca as turmas de acordo com o professor responsável ou professor da divisão para preencher as seleções de filtro
        /// </summary>
        /// <param name="seqProfessor">Sequencial do professor</param>
        /// <returns>Lista de turmas com datasources de filtro</returns>
        List<TurmaListarData> BuscarTurmasProfessorFiltros(long seqProfessor);

        /// <summary>
        /// Busca dados de cabeçalho para relatorio de diario de turma.
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns></returns>
        List<DiarioTurmaCabecalhoData> BuscarDiarioTurmaCabecalho(long seqTurma);

        /// <summary>
        /// Busca dados de professores para relatório de diario de turma
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns></returns>
        List<DiarioTurmaProfessorData> BuscarDiarioTurmaProfessor(long seqTurma);

        /// <summary>
        /// Busca dados de aluno para relatório de diario de turma
        /// </summary>
        /// <param name="seqTurma">Turma para recuperar os alunos</param>
        /// <param name="seqDivisaoTurma">Divisão da turma a ser recuperada os alunos</param>
        /// <param name="seqAluno">Sequencial do aluno caso queira apenas os dados de um determinado aluno</param>
        /// <param name="seqsOrientadores">Filtrar apenas alunos dos orientadores para o caso de turmas de orientação</param>
        /// <returns>Lista de alunos de uma turma/divisão</returns>
        List<DiarioTurmaAlunoData> BuscarDiarioTurmaAluno(long seqTurma, long? seqDivisaoTurma, long? seqAluno, List<long> seqsOrientadores);

        /// <summary>
        /// Verifica se o diário de uma turma está fechado.
        /// </summary>
        /// <param name="filtro">Filtro da turma.</param>
        /// <returns>true, se estiver fechado ou false, se estiver aberto.</returns>
        bool? DiarioTurmaFechado(TurmaFiltroData filtro);

        /// <summary>
        /// Retorna os sequenciais de turmas canceladas da lista de sequenciais passados
        /// </summary>
        /// <param name="seqsTurmas">Sequenciais das turmas</param>
        /// <returns>Sequenciais cancelados</returns>
        IEnumerable<long> ValidarTurmasCanceladas(IEnumerable<long> seqsTurmas);

        /// <summary>
        /// Fecha o diáro de uma turma.
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma.</param>
        /// <param name="seqColaborador">Seq do colaborador que está fechando a turma</param>
        void FecharDiarioTurma(long seqTurma, long seqColaborador);

        /// <summary>
        /// Faz a validação para saber se pode ou não fechar um diário de turma
        /// </summary>
        void VerificaFecharDiarioAlunoSemNota(long seqTurma, long seqColaborador, bool lancamentoNotaParcial = false);

        /// <summary>
        /// Realiza a consulta para o relatório de turmas por ciclo letivo de acordo com filtros da tela
        /// </summary>
        /// <param name="filtro">Objeto com filtros da tela</param>
        /// <returns>Lista de turmas para o relatório</returns>
        List<TurmaCicloLetivoRelatorioData> BuscarRelatorioTurmasPorCicloLetivo(TurmaCicloLetivoRelatorioFiltroData filtro);

        /// <summary>
        /// Retorna a quantidade de vagas ocupadas de uma divisão da turma ou o somatório do seus grupos de divisão
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma.</param>
        long? BuscarQuantidadeVagasOcupadasTurma(long seqTurma);

        /// <summary>
        /// Desdobrar uma turma com seus relacionamentos criando novos registro zerando os sequenciais possíveis
        /// Mesmo código da turma atualizando apenas o número sequencial
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Sequencial da turma desdobrada</returns>
        long DesdobrarTurma(long seq);

        /// <summary>
        /// Copiar uma turma com seus relacionamentos criando novos registro zerando os sequenciais possíveis
        /// Criar um novo código para turma
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Sequencial da turma copiada</returns>
        long CopiarTurma(long seq);

        /// <summary>
        /// Retorna a descrição da turma com a seguinte concatenação
        ///   [Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma].
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns>[Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma]</returns>
        string BuscarDescricaoTurmaConcatenado(long seqTurma);


        /// <summary>
        /// Método que retorna uma nova turma, para ser Desmembrada, Copiada ou Criada.
        /// </summary>
        /// <returns>Objeto turma com seus itens</returns>
        TurmaData ExecutarOperacaoTurma(TurmaData turma);

        /// <summary>
        /// Valida se a turma pode ser cancelada
        /// </summary>
        /// <param name="turma"></param>
        void ValidarCancelarTurma(TurmaData turma);

        /// <summary>
        /// Efetua o cancelamento da turma
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="situacaoJustificativa"></param>
        void CancelarTurma(long seq, string situacaoJustificativa);

        /// <summary>
        /// Reofertar a turma
        /// </summary>
        /// <param name="seq"></param>
        void OfertarTurma(long seq);

        /// <summary>
        /// Buscar as turmas por ciclo letivo, curso oferta localidade e turno
        /// </summary>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <param name="seqTurno">Sequencial do turno</param>
        /// <returns>Turmas encontradas</returns>
        List<SMCDatasourceItem> BuscarTurmasPorCicloLetivoCursoOfertaLocalidadeTurnoSelect(long? seqCicloLetivo, long? seqCursoOfertaLocalidade, long? seqTurno);

        void ValidarConfiguracaoCompartilhdaRemovida(long seqTurma, List<long> seqsConfiguracoesCompartilhadas);

        /// <summary>
        /// Buscar sequencial turma baseado em uma origem avaliação turma ou divisão de turma
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Sequencial da turma</returns>
        long BuscarSeqTurmaPorOrigemAvaliacao(long seqOrigemAvaliacao);

        /// <summary>
        /// Excluir turma
        /// </summary>
        /// <param name="seq">Sequencial da turma a ser excluido</param>
        void ExcluirTurma(long seq);

        /// <summary>
        /// Validar datas do período letivo da turma 
        /// </summary>
        /// <param name="turma"></param>
        void ValidarDatasPeriodoLetivo(TurmaData turma);

        /// <summary>
        /// Validação da exclusão dos grupos/divisões
        /// </summary>
        /// <param name="turma"></param>
        void ValidarExclusaoDivisaoGrupos(TurmaData turma);

        /// <summary>
        /// Validação de quantidade de vagas ocupadas das divisões
        /// </summary>
        /// <param name="turma"></param>
        void ValidarQuantidadeVagasOcupadasDivisoes(TurmaData turma);

        /// <summary>
        /// Validação de quantidade de grupos
        /// </summary>
        /// <param name="turma"></param>
        void ValidarQuantidadeGrupos(TurmaData turma);

        /// <summary>
        /// Buscar o período letivo da turma baseado em uma origem avaliação, para validar o cadastro de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Período letivo da turma</returns>
        (DateTime PeriodoInicio, DateTime PeriodoFinal) BuscarPeriodoLetivoTurmaPorOrigemAvaliacao(long seqOrigemAvaliacao);

        ConfiguracaoTurmaData ConfiguracaoTurma(long seq);

        void SalvarConfiguracaoTurma(ConfiguracaoTurmaData configuracaoTurma);

        void SalvarConfiguracaoTurmaComComponenteMatriz(ConfiguracaoTurmaData configuracaoTurma);
    }
}
