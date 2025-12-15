using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Interfaces
{
    public interface ITurmaColaboradorService : ISMCService
    {
        /// <summary>
        /// Buscar a lista turma colaborador
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Objeto turma colaborador</returns>
        TurmaColaboradorData BuscarTurmaColaborador(long seq);

        /// <summary>
        /// Grava uma lista de turma colaboradores
        /// </summary>
        /// <param name="turmaColaborador">Dados da turma colaboradores</param>
        void SalvarTurmaColaborador(TurmaColaboradorData turmaColaborador);

        /// <summary>
        /// Buscar a lista de colaboradores de todas as turma do relatório
        /// </summary>
        /// <param name="seqsTurma">Sequenciais de turmas</param>
        /// <returns>Lista com todos colaboradores de todas as turmas</returns>
        List<TurmaColaboradorRelatorioData> BuscarColaboradoresRelatorioTurma(List<long> seqsTurma);
    }
}
