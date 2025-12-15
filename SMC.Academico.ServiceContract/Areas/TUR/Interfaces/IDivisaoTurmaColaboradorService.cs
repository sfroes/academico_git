using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Interfaces
{
    public interface IDivisaoTurmaColaboradorService : ISMCService
    {
        /// <summary>
        /// Buscar o tipo de organização da divisão turma para associar professor
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisao turma</param>
        /// <returns>Texto com o tipo organização definido</returns>
        string BuscarTipoComponenteDivisaoTurma(long seqDivisaoTurma);

        /// <summary>
        /// Buscar o tipo de organização e dados iniciais da divisão de turma para associar professor
        /// </summary>
        /// <param name="seqDivisao">Sequencial da divisão turma</param>
        /// <returns>Objeto com dados iniciais da divisão da turma</returns>
        DivisaoTurmaColaboradorData BuscarConfiguracaoDivisaoTurmaColaborador(long seqDivisao);

        /// <summary>
        /// Buscar a associação de colaborador com a divisão de turma
        /// </summary>
        /// <param name="seq">Sequencial da associacao de divisao com colaborador</param>
        /// <returns>Objeto que associa o colaborador a divisão com suas organizações</returns>
        DivisaoTurmaColaboradorData BuscarDivisaoTurmaColaborador(long seq);

        /// <summary>
        /// Remover a associação do colaborador com a divisão de turma
        /// </summary>
        /// <param name="seq">Sequencial da associacao de divisao com colaborador</param>
        void ExcluirDivisaoTurmaColaborador(long seq);

        /// <summary>
        /// Grava uma colaborador associado a divisão com suas respectivas organizações
        /// </summary>
        /// <param name="divisaoColaborador">Dados da divisão turma colaboradores</param>
        void SalvarDivisaoTurmaColaborador(DivisaoTurmaColaboradorData divisaoColaborador);

        /// <summary>
        /// Buscar a lista de colaboradores de todas as divisões de turma do relatório
        /// </summary>
        /// <param name="seqsDivisaoTurma">Sequenciais de divisões de turmas</param>
        /// <returns>Lista com todos colaboradores de todas as divisões de turmas</returns>
        List<DivisaoTurmaRelatorioColaboradorData> BuscarColaboradoresDivisaoRelatorioTurma(List<long> seqsDivisaoTurma);
    }
}
