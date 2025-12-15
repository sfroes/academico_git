using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Service;
using System;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class AlunoHistoricoService : SMCServiceBase, IAlunoHistoricoService
    {
        #region [ DomainService ]

        private AlunoHistoricoDomainService AlunoHistoricoDomainService { get => Create<AlunoHistoricoDomainService>(); }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca de acordo com o tipo de pessoa atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matrícula</param>
        /// <returns>Registro acadêmico</returns>
        public long BuscarCodigoAlunoMigracao(long seqPessoaAtuacao, long seqSolicitacaoMatricula)
        {
            return AlunoHistoricoDomainService.BuscarCodigoAlunoMigracao(seqPessoaAtuacao, seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Busca o sequencial da entidade de vínculo atual do aluno e a instituição de ensino
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Sequencial da entidade de vínculo atual do aluno e Sequencial da instituição de ensino</returns>
        public Tuple<long, long?> BuscarEntidadeVinculoAluno(long seq)
        {
            return AlunoHistoricoDomainService.BuscarEntidadeVinculoAluno(seq);
        }

        /// <summary>
        /// Buscar o sequencial do aluno historico atual
        /// </summary>
        /// <param name="seqAluno">Sequencial do Aluno</param>
        /// <returns>Sequencial do historico aluno</returns>
        public long BuscarSequencialAlunoHistoricoAtual(long seqAluno)
        {
            return AlunoHistoricoDomainService.BuscarSequencialAlunoHistoricoAtual(seqAluno);
        }

        /// <summary>
        ///  Buscar sequencial do aluno pelo aluno historico
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequacial do aluno historico</param>
        /// <returns>Sequencial do aluno</returns>
        public long BuscarSeqAlunoPorAlunoHistorico(long seqAlunoHistorico)
        {
            return AlunoHistoricoDomainService.BuscarSeqAlunoPorAlunoHistorico(seqAlunoHistorico);
        }
    }
}
