using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Framework.Exceptions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
	[ServiceContract(Namespace = NAMESPACES.SERVICE)]
	public interface IHistoricoEscolarService : ISMCService
	{
		/// <summary>
		/// Busca os dados do cabeçalho do histórico escolar de um aluno
		/// </summary>
		/// <param name="seqAluno">Sequencial do aluno</param>
		/// <returns>Dados do Cabeçalho</returns>
		HistoricoEscolarCabecalhoData BuscarHistoricoEscolarCabecalho(long seqAluno);

		/// <summary>
		/// Busca o histórico escolar de um aluno
		/// </summary>
		/// <param name="filtros">Sequencial do aluno e dados de paginação</param>
		/// <returns>Dados do histórico escolar do aluno</returns>
		SMCPagerData<HistoricoEscolarListaData> BuscarHistoricosEscolares(HistoricoEscolarFiltroData filtros);

		/// <summary>
		/// Busca um item de histórico escolar
		/// </summary>
		/// <param name="filtro">Filtro a ser utilizado</param>
		/// <returns>Filtro a ser utilizado</returns>
		HistoricoEscolarCompletoData BuscarHistoricoEscolar(HistoricoEscolarFiltroData filtro);

		/// <summary>
		/// Retorna o histórico escolar de uma turma para um aluno
		/// </summary>
		/// <param name="seqTurma">Sequencial da turma</param>
		/// <param name="seqAluno">Sequencial do aluno</param>
		/// <returns>Dados do histórico escolar</returns>
		[OperationContract]
		HistoricoEscolarCompletoData BuscarHistoricoEscolarTurma(long seqTurma, long seqAluno);

		/// <summary>
		/// Busca um registro de histórico escolar que representa a associação de um componente
		/// </summary>
		/// <param name="seq">Sequencial do item de histórico escolar</param>
		/// <exception cref="SMCInvalidOperationException">Caso o histórico informado tenha origem de avaliação (significa que não foi criado nesta tela)</exception>
		/// <returns>Dados do histórico escolar</returns>
		HistoricoEscolarData BuscarHistoricoEscolarComponente(long seq);

		/// <summary>
		/// Grava uma associação de histórico escolar com componente
		/// </summary>
		/// <param name="historicoEscolar">Dados do histórico escolar</param>
		/// <returns>Sequencial do histórico escolar gravado</returns>
		long SalvarHistoricoEscolarComponente(HistoricoEscolarData historicoEscolar);

		/// <summary>
		/// Grava o lançamento de notas e frequências finais de todos os alunos de uma turma.
		/// </summary>
		/// <param name="dados">Informações de notas, faltas e conteúdo lecionado de uma turma.</param>
		/// <returns></returns>
		void SalvarLancamentoNotasFrequenciaFinal(LancamentoHistoricoEscolarData dados);

		/// <summary>
		/// Inclui o lançamento de notas e frequências finais de todos os alunos da turma.
		/// </summary>
		/// <param name="seqTurma">Sequencial da turma</param>
		/// <param name="seqsOrientadores">Sequenciais dos orientadores para filtro dos alunos</param>
		/// <returns></returns>
		LancamentoHistoricoEscolarData LancarNotasFrequenciasFinais(long seqTurma, List<long> seqsOrientadores);

		/// <summary>
		/// Calcular situação final do aluno no lançamento de nota parcial
		/// </summary>
		/// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
		/// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
		/// <param name="nota">Nota final aluno</param>
		/// <param name="seqsProfessores">Lista sequencial dos professores</param>
		/// <returns>Situação final do aluno</returns>
		SituacaoHistoricoEscolar CalcularSituacaofinalLancamentoNotaParcial(long seqOrigemAvaliacao, long seqAlunoHistorico, short? nota, List<long> seqsProfessores);

		/// <summary>
		/// Calcula a situação final do aluno segundo a regra RN_APR_007 - Cálculo da situação final de aluno
		/// </summary>
		/// <param name="historicoEscolarSituacaoData">Dados para o cálculo da situação final do aluno</param>
		/// <returns>Situação final do aluno</returns>
		SituacaoHistoricoEscolar CalcularSituacaoFinal(HistoricoEscolarSituacaoFinalData historicoEscolarSituacaoData);

		/// <summary>
		/// Apaga o registro de histórico escolar
		/// </summary>
		/// <param name="seq">Sequencial do registro</param>
		/// <exception cref="SMCInvalidOperationException">Caso o registro tenha uma origem de avaliação (Significa que não foi criado na tela de histórico escolar)</exception>
		void ExcluirHistoricoEscolarComponente(long seq);

		/// <summary>
		/// Excluir historico esclar por aluno historico
		/// </summary>
		/// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
		/// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
		void ExcluirHistoricoEscolarPorAlunoHistorico(long seqAlunoHistorico, long seqOrigemAvaliacao);

		/// <summary>
		/// Recupera a obrigatoriedade de matriz conforme o vínculo do aluno
		/// </summary>
		/// <param name="seqAluno">Sequencial do aluno</param>
		/// <returns>Obrigatoriedade da matriz conforme o vínculo do aluno</returns>
		HistoricoEscolarData BuscarConfiguracaoHistoricoEscolarComponente(long seqAluno);

		/// <summary>
		/// Dados do cabeçalho da consulda de integralização, separado quando colocado o filtro para evitar duas buscas completas
		/// </summary>
		/// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
		/// <param name="visaoAluno">Verifica o sistema que chamou a modal</param>
		/// <returns>Dados do cabeçalho da integralização</returns>
		IntegralizacaoMatrizCurricularOfertaCabecalhoData CabecalhoTelaIntegralizacaoCurricularHistorico(long seqPessoaAtuacao, bool visaoAluno);

		/// <summary>
		/// Consulta histórico escolar, matriz curricular e plano de estudo para exibir os dados da integralização curricular do aluno/ingressante
		/// </summary>
		/// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
		/// <param name="visaoAluno">Defini o projeto, se for SGA.Aluno exibe apenas nome social</param>
		/// <param name="filtro">Filtro de situação e descrição da configuração de componente</param>
		/// <returns>Objeto de retorno da consulta estruturado com matriz e componentes</returns>
		IntegralizacaoConsultaHistoricoData ConsultaIntegralizacaoCurricularHistorico(long seqPessoaAtuacao, bool visaoAluno, IntegralizacaoConsultaHistoricoFiltroData filtro);

		/// <summary>
		/// Busca os históricos escolares para modal de integralização curricular
		/// </summary>
		/// <param name="seqsHistoricoEscolar">Sequenciais concatenados por ,</param>
		/// <returns>Dados do histórico escolar do aluno</returns>
		List<HistoricoEscolarListaData> BuscarHistoricosEscolaresIntegralizacao(string seqsHistoricoEscolar);

		/// <summary>
		/// Flag para visualizar consulta de integralização, sempre para aluno e quando for ingressante somente se conceder formação.
		/// </summary>
		/// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
		/// <returns>Retorna o valor da flag de controle da visualização</returns>
		bool VisualizarConsultaConcederFormacaoIntegralizacao(long seqPessoaAtuacao);

		/// <summary>
		/// Buscar o ciclo letivo e o curso oferta localidade turno do historico escolar
		/// </summary>
		/// <param name="seq">Sequencial do historico escolar</param>
		/// <returns>Retorno o sequencial do ciclo letivo e do curso oferta localidade turno</returns>
		(long SeqCicloLetivo, long SeqCursoOfertaLocalidadeTurno) BuscarCicloLetivoLocalidadeTurnoHistoricoEscolar(long seq);

		/// <summary>
		/// Método que verifica se existe componentes ja aprovados ou dispensados na lista
		/// </summary>
		/// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
		/// <param name="componentesComAssunto">Lista de componentes com assunto</param>
		/// <returns>Retorna lista de componentes separado com , para os componentes já aprovados ou dispensados</returns>
		string VerificarHistoricoComponentesAprovadosDispensados(long seqPessoaAtuacao, List<HistoricoEscolarAprovadoFiltroData> componentesComAssunto);

		/// <summary>
		/// Método que verifica se existe componentes ja aprovados ou dispensados na lista
		/// </summary>
		/// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
		/// <param name="dadosComponentesSelecionados">Dados dos componentes selecionados</param>
		/// <param name="seqCicloLetivoProcesso">Sequencial do ciclo letivo processo</param>
		/// <returns>Retorna lista de componentes separado com , para os componentes já aprovados ou dispensados</returns>
		string VerificarHistoricoComponentesAprovadosDispensados(long seqPessoaAtuacao, List<(long? SeqConfiguracaoComponente, long? SeqDivisaoTurma)> dadosComponentesSelecionados, long? seqCicloLetivoProcesso);

		/// <summary>
		/// Retornar dados historico escolar baseado na procedure ACADEMICO.APR.st_rel_componentes_historico_escolar
		/// </summary>
		/// <param name="seqAluno">Sequencial Aluno</param>
		/// <param name="exibirReprovados">Indicador para exibir ou não componentes reprovados</param>
		/// <param name="exibeCreditoZero">Indicador para exibir ou não componentes com crédito zero</param>
		/// <param name="exibeComponenteCursadoSemHistorico">Indicador para exibir ou não componentes cursados que ainda não tem lançamento de nota no histórico</param>
		/// <param name="exibeExame">Indicador para exibir ou não componentes de exame cursados</param>
		/// <returns>Lista de componentes cursados</returns>
		[OperationContract]
		List<ComponentesCreditosData> ConsultarNotasFrequencias(long seqAluno, bool exibirReprovados, bool exibeCreditoZero, bool exibeComponenteCursadoSemHistorico, bool exibeExame);
	}
}