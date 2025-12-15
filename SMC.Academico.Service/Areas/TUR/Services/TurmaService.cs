using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.TUR.Services
{
    public class TurmaService : SMCServiceBase, ITurmaService
    {
        #region [ DomainService ]

        private TurmaDomainService TurmaDomainService
        {
            get { return this.Create<TurmaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// O campo turma é composto do "Código da turma" + "." + "Número da turma".
        /// Ao criar/copiar uma nova turma:
        /// - O campo código da turma deverá ser um campo sequencial
        /// - O campo número da turma deverá ser um sequencial começando sempre de 1 por código da turma.
        /// </summary>
        /// <returns>"Código da turma" + "." + "Número da turma"</returns>
        public string GerarCodigoNumeroTurma()
        {
            return this.TurmaDomainService.GerarCodigoNumeroTurma();
        }

        /// <summary>
        /// Buscar a turma
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="buscarDivisoesComponenteComDivisoesTurmaAssociadas"></param>
        /// <returns>Objeto turma com seus itens</returns>
        public TurmaData BuscarTurma(long seq, bool buscarDivisoesComponenteComDivisoesTurmaAssociadas = false)
        {
            return this.TurmaDomainService.BuscarTurma(seq, buscarDivisoesComponenteComDivisoesTurmaAssociadas).Transform<TurmaData>();
        }

        /// <summary>
        /// Buscar cabeçalho com os dados de turma
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Objeto com dados da turma</returns>
        public TurmaCabecalhoData BuscarTurmaCabecalho(long seq)
        {
            return this.TurmaDomainService.BuscarTurmaCabecalho(seq).Transform<TurmaCabecalhoData>();
        }

        /// <summary>
        /// Busca as turmas de acordo com os filtros
        /// </summary>
        /// <param name="filtros">Objeto turma filtro</param>
        /// <returns>SMCPagerData com a lista de turmas detalhadas</returns>
        public SMCPagerData<TurmaListarData> BuscarTurmas(TurmaFiltroData filtros)
        {
            ////Evita realizar a consulta inicial sem o campo obrigatório
            //if (filtros.SeqCicloLetivoInicio == 0)
            //    return new SMCPagerData<TurmaListarData>();

            var turmas = this.TurmaDomainService.BuscarTurmas(filtros.Transform<TurmaFiltroVO>());

            return turmas.Transform<SMCPagerData<TurmaListarData>>();
        }

        /// <summary>
        /// Busca a turma para o retorno do lookup
        /// </summary>
        /// <param name="seq">Sequencial da turma selecionada</param>
        /// <returns>Dados da turma</returns>
        public TurmaListarData BuscarTurmaLookup(long seq)
        {
            return TurmaDomainService.BuscarTurmaLookup(seq).Transform<TurmaListarData>();
        }

        /// <summary>
        /// Grava uma turma com seus respectivos itens
        /// </summary>
        /// <param name="turma">Dados da turma a ser gravado</param>
        /// <returns>Sequencial da turma gravado</returns>
        public long SalvarTurma(TurmaData turma)
        {
            return this.TurmaDomainService.SalvarTurma(turma.Transform<TurmaVO>());
        }

        /// <summary>
        /// Rotina para realizar a carga de agendas de turma
        /// </summary>
        /// <param name="seqCicloLetivo">Ciclo letivo a ser realizada a carga</param>
        public void CargaAgendasTurma(long seqCicloLetivo)
        {
            this.TurmaDomainService.CargaAgendasTurma(seqCicloLetivo);
        }

        /// <summary>
        /// Busca as turmas de acordo com a pessoa atuação processo de matrícula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <param name="seqProcesso">Sequencial processo</param>
        /// <param name="seqGrupoPrograma">Sequencial do grupo de programa para disciplina eletiva</param>
        /// <param name="tokenServico">Token do Serviço</param>
        /// <returns>Lista de turmas detalhadas</returns>
        public SMCPagerData<TurmaMatriculaListarData> BuscarTurmasPessoaAtuacao(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long? seqProcesso, long? seqGrupoPrograma, string tokenServico)
        {
            var turmas = this.TurmaDomainService.BuscarTurmasPessoaAtuacaoEntidade(seqSolicitacaoMatricula, seqPessoaAtuacao, seqProcesso, seqGrupoPrograma, tokenServico);

            return turmas.Transform<SMCPagerData<TurmaMatriculaListarData>>();
        }

        /// <summary>
        /// Busca as turmas de acordo com o professor responsável ou professor da divisão
        /// </summary>
        /// <param name="seqProfessor">Sequencial do professor</param>
        /// <returns>Lista de turmas detalhadas</returns>
        public List<TurmaListarGrupoCursoData> BuscarTurmasProfessor(long seqProfessor, bool? turmaSituacaoCancelada = null)
        {
            var turmas = TurmaDomainService.BuscarTurmasProfessor(seqProfessor, turmaSituacaoCancelada);

            return turmas.TransformList<TurmaListarGrupoCursoData>();
        }

        /// <summary>
        /// Busca as turmas de acordo com o aluno logado
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo caso queira filtrar</param>
        /// <returns>Lista de turmas detalhadas</returns>
        public List<TurmaListarGrupoCursoData> BuscarTurmasAluno(long seq, long? seqCicloLetivo, bool? apenasMatriculado = true)
        {
            var turmas = this.TurmaDomainService.BuscarTurmasAluno(seq, seqCicloLetivo, apenasMatriculado);

            return turmas.TransformList<TurmaListarGrupoCursoData>();
        }

        /// <summary>
        /// Busca as turmas de acordo com o professor responsável ou professor da divisão para preencher as seleções de filtro
        /// </summary>
        /// <param name="seqProfessor">Sequencial do professor</param>
        /// <returns>Lista de turmas com datasources de filtro</returns>
        public List<TurmaListarData> BuscarTurmasProfessorFiltros(long seqProfessor)
        {
            var turmas = this.TurmaDomainService.BuscarTurmasProfessorFiltros(seqProfessor);

            return turmas.TransformList<TurmaListarData>();
        }

        /// <summary>
        /// Busca dados de cabeçalho para relatorio de diario de turma.
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns></returns>
        public List<DiarioTurmaCabecalhoData> BuscarDiarioTurmaCabecalho(long seqTurma)
        {
            return TurmaDomainService.BuscarDiarioTurmaCabecalho(seqTurma).TransformList<DiarioTurmaCabecalhoData>();
        }

        /// <summary>
        /// Busca dados de professores para relatório de diario de turma
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns></returns>
        public List<DiarioTurmaProfessorData> BuscarDiarioTurmaProfessor(long seqTurma)
        {
            return TurmaDomainService.BuscarDiarioTurmaProfessor(seqTurma).TransformList<DiarioTurmaProfessorData>();
        }

        /// <summary>
        /// Busca dados de aluno para relatório de diario de turma
        /// </summary>
        /// <param name="seqTurma">Turma para recuperar os alunos</param>
        /// <param name="seqDivisaoTurma">Divisão da turma a ser recuperada os alunos</param>
        /// <param name="seqsOrientadores">Filtrar apenas alunos dos orientadores para o caso de turmas de orientação</param>
        /// <returns>Lista de alunos de uma turma/divisão</returns>
        public List<DiarioTurmaAlunoData> BuscarDiarioTurmaAluno(long seqTurma, long? seqDivisaoTurma, long? seqAluno, List<long> seqsOrientadores)
        {
            return TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, seqDivisaoTurma, seqAluno, seqsOrientadores).TransformList<DiarioTurmaAlunoData>();
        }

        /// <summary>
        /// Verifica se o diário de uma turma está fechado.
        /// </summary>
        /// <param name="filtro">Filtro da turma.</param>
        /// <returns>true, se estiver fechado ou false, se estiver aberto.</returns>
        public bool? DiarioTurmaFechado(TurmaFiltroData filtro)
        {
            return TurmaDomainService.DiarioTurmaFechado(filtro.Transform<TurmaFilterSpecification>());
        }

        /// <summary>
        /// Fecha o diáro de uma turma.
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma.</param>
        public void FecharDiarioTurma(long seqTurma, long seqColaborador)
        {
            TurmaDomainService.FecharDiarioTurma(seqTurma, seqColaborador);
        }

        /// <summary>
        /// Faz a validação para saber se pode ou não fechar um diário de turma
        /// </summary>
        public void VerificaFecharDiarioAlunoSemNota(long seqTurma, long seqColaborador, bool lancamentoNotaParcial = false)
        {
            TurmaDomainService.VerificaFecharDiarioAlunoSemNota(seqTurma, seqColaborador, lancamentoNotaParcial);
        }

        /// <summary>
        /// Realiza a consulta para o relatório de turmas por ciclo letivo de acordo com filtros da tela
        /// </summary>
        /// <param name="filtro">Objeto com filtros da tela</param>
        /// <returns>Lista de turmas para o relatório</returns>
        public List<TurmaCicloLetivoRelatorioData> BuscarRelatorioTurmasPorCicloLetivo(TurmaCicloLetivoRelatorioFiltroData filtro)
        {
            var turmas = this.TurmaDomainService.BuscarRelatorioTurmasPorCicloLetivo(filtro.Transform<TurmaCicloLetivoRelatorioFiltroVO>());

            return turmas.TransformList<TurmaCicloLetivoRelatorioData>();
        }

        /// <summary>
        /// Retorna a quantidade de vagas ocupadas de uma divisão da turma ou o somatório do seus grupos de divisão
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma.</param>
        public long? BuscarQuantidadeVagasOcupadasTurma(long seqTurma)
        {
            return TurmaDomainService.BuscarQuantidadeVagasOcupadasTurma(seqTurma);
        }

        /// <summary>
        /// Desdobrar uma turma com seus relacionamentos criando novos registro zerando os sequenciais possíveis
        /// Mesmo código da turma atualizando apenas o número sequencial
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Sequencial da turma desdobrada</returns>
        public long DesdobrarTurma(long seq)
        {
            return TurmaDomainService.DesdobrarTurma(seq);
        }

        /// <summary>
        /// Copiar uma turma com seus relacionamentos criando novos registro zerando os sequenciais possíveis
        /// Criar um novo código para turma
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Sequencial da turma copiar</returns>
        public long CopiarTurma(long seq)
        {
            return TurmaDomainService.CopiarTurma(seq);
        }

        /// <summary>
        /// Retorna a descrição da turma com a seguinte concatenação
        ///   [Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma].
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns>[Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma]</returns>
        public string BuscarDescricaoTurmaConcatenado(long seqTurma)
        {
            return TurmaDomainService.BuscarDescricaoTurmaConcatenado(seqTurma);
        }

        /// <summary>
        /// Busca o sequencial da instituição de ensino da turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Sequencial da Instituição de ensino</returns>
        public long BuscarSeqInstituicaoEnsinoTurma(long seqTurma)
        {
            return TurmaDomainService.BuscarSeqInstituicaoEnsinoTurma(seqTurma);
        }

        /// <summary>
        /// Método que retorna uma nova turma, para ser Desmembrada, Copiada ou Criada.
        /// </summary>
        /// <returns>Objeto turma com seus itens</returns>
        public TurmaData ExecutarOperacaoTurma(TurmaData turma)
        {
            return TurmaDomainService.ExecutarOperacaoTurma(turma.Transform<TurmaVO>()).Transform<TurmaData>();
        }

        /// <summary>
        /// Valida se a turma pode ser cancelada
        /// </summary>
        /// <param name="turma"></param>
        /// <returns>True ->Emitir mensagem e Cancelar; False -> Prosseguir sem cancelar</returns>
        public void ValidarCancelarTurma(TurmaData turma)
        {
            TurmaDomainService.ValidarCancelarTurma(turma.Transform<TurmaVO>());
        }

        /// <summary>
        /// Efetua o cancelamento da turma
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="situacaoJustificativa"></param>
        public void CancelarTurma(long seq, string situacaoJustificativa)
        {
            TurmaDomainService.CancelarTurma(seq, situacaoJustificativa);
        }

        /// <summary>
        /// Reofertar a turma
        /// </summary>
        /// <param name="seq"></param>
        public void OfertarTurma(long seq)
        {
            TurmaDomainService.OfertarTurma(seq);
        }

        public List<SMCDatasourceItem> BuscarTurmasPorCicloLetivoCursoOfertaLocalidadeTurnoSelect(long? seqCicloLetivo, long? seqCursoOfertaLocalidade, long? seqTurno)
        {
            return TurmaDomainService.BuscarTurmasPorCicloLetivoCursoOfertaLocalidadeTurnoSelect(seqCicloLetivo, seqCursoOfertaLocalidade, seqTurno);
        }

        /// <summary>
        /// Retorna os sequenciais de turmas canceladas da lista de sequenciais passados
        /// </summary>
        /// <param name="seqsTurmas">Sequenciais das turmas</param>
        /// <returns>Sequenciais cancelados</returns>
        public IEnumerable<long> ValidarTurmasCanceladas(IEnumerable<long> seqsTurmas)
        {
            if (seqsTurmas == null || !seqsTurmas.Any())
                return new List<long>();

            return TurmaDomainService.ValidarTurmasCanceladas(seqsTurmas);
        }

        public void ValidarConfiguracaoCompartilhdaRemovida(long seqTurma, List<long> seqsConfiguracoesCompartilhadas)
        {
            TurmaDomainService.ValidarConfiguracaoCompartilhdaRemovida(seqTurma, seqsConfiguracoesCompartilhadas);
        }

        /// <summary>
        /// Buscar sequencial turma baseado em uma origem avaliação turma ou divisão de turma
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Sequencial da turma</returns>
        public long BuscarSeqTurmaPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            return TurmaDomainService.BuscarSeqTurmaPorOrigemAvaliacao(seqOrigemAvaliacao);
        }

        /// <summary>
        /// Excluir turma
        /// </summary>
        /// <param name="seq">Sequencial da turma a ser excluido</param>
        public void ExcluirTurma(long seq)
        {
            this.TurmaDomainService.ExcluirTurma(seq);
        }

        /// <summary>
        /// Validar datas do período letivo da turma 
        /// </summary>
        /// <param name="turma"></param>
        public void ValidarDatasPeriodoLetivo(TurmaData turma)
        {
            this.TurmaDomainService.ValidarDatasPeriodoLetivo(turma.Transform<TurmaVO>());
        }

        /// <summary>
        /// Validação da exclusão dos grupos/divisões
        /// </summary>
        /// <param name="turma"></param>
        public void ValidarExclusaoDivisaoGrupos(TurmaData turma)
        {
            this.TurmaDomainService.ValidarExclusaoDivisaoGrupos(turma.Transform<TurmaVO>());
        }

        /// <summary>
        /// Validação de quantidade de vagas ocupadas das divisões
        /// </summary>
        /// <param name="turma"></param>
        public void ValidarQuantidadeVagasOcupadasDivisoes(TurmaData turma)
        {
            this.TurmaDomainService.ValidarQuantidadeVagasOcupadasDivisoes(turma.Transform<TurmaVO>());
        }

        /// <summary>
        /// Validação de quantidade de grupos
        /// </summary>
        /// <param name="turma"></param>
        public void ValidarQuantidadeGrupos(TurmaData turma)
        {
            this.TurmaDomainService.ValidarQuantidadeGrupos(turma.Transform<TurmaVO>());
        }

        /// <summary>
        /// Buscar o período letivo da turma baseado em uma origem avaliação, para validar o cadastro de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Período letivo da turma</returns>
        public (DateTime PeriodoInicio, DateTime PeriodoFinal) BuscarPeriodoLetivoTurmaPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            return this.TurmaDomainService.BuscarPeriodoLetivoTurmaPorOrigemAvaliacao(seqOrigemAvaliacao);
        }

        public ConfiguracaoTurmaData ConfiguracaoTurma(long seq)
        {
            return this.TurmaDomainService.ConfiguracaoTurma(seq).Transform<ConfiguracaoTurmaData>();
        }

        public void SalvarConfiguracaoTurma(ConfiguracaoTurmaData configuracaoTurma)
        {
            this.TurmaDomainService.SalvarConfiguracaoTurma(configuracaoTurma.Transform<ConfiguracaoTurmaVO>());
        }

        public void SalvarConfiguracaoTurmaComComponenteMatriz(ConfiguracaoTurmaData configuracaoTurma)
        {
            this.TurmaDomainService.SalvarConfiguracaoTurmaComComponenteMatriz(configuracaoTurma.Transform<ConfiguracaoTurmaVO>());
        }
    }
}