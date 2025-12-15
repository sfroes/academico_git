using SMC.Framework.Service;
using System;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    public interface IAlunoHistoricoService : ISMCService
    {
        /// <summary>
        /// Busca de acordo com o tipo de pessoa atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matrícula</param>
        /// <returns>Registro acadêmico</returns>
        long BuscarCodigoAlunoMigracao(long seqPessoaAtuacao, long seqSolicitacaoMatricula);

        /// <summary>
        /// Busca o sequencial da entidade de vínculo atual do aluno e a instituição de ensino
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Sequencial da entidade de vínculo atual do aluno e Sequencial da instituição de ensino</returns>
        Tuple<long, long?> BuscarEntidadeVinculoAluno(long seq);

        /// <summary>
        /// Buscar o sequencial do aluno historico atual
        /// </summary>
        /// <param name="seqAluno">Sequencial do Aluno</param>
        /// <returns>Sequencial do historico aluno</returns>
        long BuscarSequencialAlunoHistoricoAtual(long seqAluno);

        /// <summary>
        ///  Buscar sequencial do aluno pelo aluno historico
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequacial do aluno historico</param>
        /// <returns>Sequencial do aluno</returns>
        long BuscarSeqAlunoPorAlunoHistorico(long seqAlunoHistorico);
    }
}
