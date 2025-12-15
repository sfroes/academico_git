using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IOrigemAvaliacaoService : ISMCService
    {
        /// <summary>
        /// Buscar a descrição da origem de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequncial da origem de avaliação</param>
        /// <returns>Descrição da origem de avaliação</returns>
        string BuscarDescricaoOrigemAvaliacao(long seqOrigemAvaliacao);

        /// <summary>
        /// Buscar origem avaliacao
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Origem avaliação</returns>
        OrigemAvaliacaoData BuscarOrigemAvaliacao(long seqOrigemAvaliacao);

        /// <summary>
        /// Diario aberto ou fechado baseado na origem de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Boleano</returns>
        bool DiarioAbertoPorOrigemAvaliacao(long seqOrigemAvaliacao);

        /// <summary>
        /// Buscar alunos baseado na origem avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de alunos com sequencial histiorico aluno e nome</returns>
        List<SMCDatasourceItem> BuscarAlunosPorOrigemAvaliacao(long seqOrigemAvaliacao);

        /// <summary>
        /// Buscar professores de uma origem avaliacao
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de professores responsaveis da turma</returns>
        List<TurmaCabecalhoResponsavelData> BuscarProfessoresPorOrigemAvaliacao(long seqOrigemAvaliacao);

        /// <summary>
        /// Buscar origem avaliação por divisão
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequecial divisão de turma</param>
        /// <returns></returns>
        long BuscarOrigemAvaliacaoPorDivisaoTurma(long seqDivisaoTurma);

        /// <summary>
        /// Buscar descrição do ciclo letivo pela origem avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns></returns>
        string BusacarDescricaoCicloLetivoPorOrigemAvaliacao(long seqOrigemAvaliacao);
    }
}