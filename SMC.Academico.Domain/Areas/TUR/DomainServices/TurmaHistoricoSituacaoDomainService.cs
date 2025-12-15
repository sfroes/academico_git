using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Domain.Areas.TUR.Models;

namespace SMC.Academico.Domain.Areas.TUR.DomainServices
{
    public class TurmaHistoricoSituacaoDomainService : AcademicoContextDomain<TurmaHistoricoSituacao>
    {
        public void AlterarSituacaoTurma (long seqTurma, SituacaoTurma situacao, string justificativa)
        {
            // Cria a nova situação da turma
            var novaSituacao = new TurmaHistoricoSituacao()
            {
                SeqTurma = seqTurma,
                SituacaoTurma = situacao,
                Justificativa = justificativa
            };
            // Salva a nova situação no histórico
            this.SaveEntity(novaSituacao);
        }

    }
}