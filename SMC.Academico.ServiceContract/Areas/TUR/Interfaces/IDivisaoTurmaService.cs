using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Interfaces
{
    public interface IDivisaoTurmaService : ISMCService
    {
        /// <summary>
        /// Buscar a divisão turma com detalhes para cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial da divisão turma</param>
        /// <param name="telaDetalhes">Quando true, quer dizer que é da tela de detalhes da divisão de turma, e o cabeçalho é montado de maneira diferente</param>
        /// <returns>Objeto divisão turma com detalhes para o cabeçalho</returns>
        DivisaoTurmaDetalhesData BuscarDivisaoTurmaCabecalho(long seq, bool telaDetalhes = false);

        /// <summary>
        /// Buscar a divisão turma com detalhes para tela de visualização
        /// </summary>
        /// <param name="seq">Sequencial da divisão turma</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="preservarColaboradores">Quando true, mesmo que não seja infromada uma pessoa atuação e a turma seja de orientação os colaboradores serão retornados</param>
        /// <param name="telaDetalhes">Quando true, quer dizer que é da tela de detalhes da divisão de turma, e o cabeçalho é montado de maneira diferente</param>
        /// <returns>Objeto divisão turma com detalhes e colaboradores</returns>
        List<DivisaoTurmaDetalhesData> BuscarDivisaoTurmaDetalhes(long seq, long? seqPessoaAtuacao = null, bool preservarColaboradores = false, bool telaDetalhes = false);

        /// <summary>
        /// Buscar a divisão turma com detalhes e colaboradores para tela de visualização
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Lista divisão turma com detalhes e colaboradores</returns>
        List<DivisaoTurmaDetalhesData> BuscarDivisaoTurmaLista(long seqTurma);

        /// <summary>
        /// Lista de alunos por uma divisão de turma. Modelo de VO para relatório de Emissão de Lista de Frequencia
        /// </summary>
        /// <param name="seqDivisaoTurma"></param>
        /// <returns></returns>
        List<DivisaoTurmaRelatorioAlunoData> BuscarAlunosDivisaoTurma(long seqDivisaoTurma);

        /// <summary>
        /// Lista de cabecalho por uma divisão de turma. Informações da turma. Modelo de VO para relatório de Emissão de Lista de Frequencia
        /// </summary>
        /// <param name="seqDivisaoTurma"></param>
        /// <returns></returns>
        List<DivisaoTurmaRelatorioCabecalhoData> BuscarDivisaoTurmaRelatorioCabecalho(long seqDivisaoTurma);

        /// <summary>
        /// Lista de colaboradores por uma divisão de turma. 
        /// Modelo de VO para relatório de Emissão de Lista de Frequencia
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão de turma</param>
        /// <param name="colaboradores">Lista de colaboradores selecionados</param>
		/// <param name="dataVinculo">Data para validação do vínculo</param>
        /// <returns>Lista de colaboradores da divisão de turma</returns>
        List<DivisaoTurmaRelatorioColaboradorData> BuscarDivisaoTurmaRelatorioColaborador(long seqDivisaoTurma, List<long> colaboradores, DateTime? dataVinculo);

        /// <summary>
        /// Busca a data de inicio e fim de um evento letivo de uma turma, baseado na divisão da turma.
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão da turma</param>
        /// <returns>Datas de início e fim do evento letivo.</returns>
        (DateTime dataInicio, DateTime dataFim) BuscarDatasEventoLetivoTurma(long seqDivisaoTurma);

        /// <summary>
        /// Montar descrição divisao de turma - RN_TUR_026 - Exibição Descrição - Divisão Turma
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial divisão de turma</param>
        /// <returns>Descrição divisão de turma</returns>
        string ObterDescricaoDivisaoTurma(long seqDivisaoTurma, long seqAluno = 0);

        void ValidarOfertaMatrizExcluidas(long seqTurma, List<long> seqsMatrizesCurricularesOfertasExcluidas);

        DetalheDivisaoTurmaGradeData BuscarDetalhesDivisaoTurmaGrade(long seqDivisaoTurma);

        /// <summary>
        ///  Buscar a divisões de turma para a grade horaria compartilhada
        /// </summary>
        /// <param name="seqTurma">Sequencial de turma</param>
        /// <returns>Divisões filtradas para turma</returns>
        List<SMCDatasourceItem> BuscarDivisõesPorTurmaParaGradeCompartilhadaSelect(long seqTurma);

        /// <summary>
        /// Buscar se pelo menos uma divisão tem obrigatoriedade de matéria lecionada obrigatória
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns></returns>
        bool MateriaLecionadaObrigatoria(long seqTurma);
    }
}