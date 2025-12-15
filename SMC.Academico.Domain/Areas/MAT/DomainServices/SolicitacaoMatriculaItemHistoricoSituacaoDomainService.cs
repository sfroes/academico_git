using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Domain.Areas.MAT.Models;

namespace SMC.Academico.Domain.Areas.MAT.DomainServices
{
    public class SolicitacaoMatriculaItemHistoricoSituacaoDomainService : AcademicoContextDomain<SolicitacaoMatriculaItemHistoricoSituacao>
    {
        /// <summary>
        /// Acresenta um registro de histórico de acordo com a situação
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequencial da solicitação matrícula item</param>
        /// <param name="seqSituacao">Sequencial da situação</param>
        /// <param name="motivo">Enumerador necessário de acordo com a situação</param>
        public void AtualizarHistoricoSolicitacaoMatriculaItem(long seqSolicitacaoMatriculaItem, long seqSituacao, MotivoSituacaoMatricula? motivo)
        {
            SolicitacaoMatriculaItemHistoricoSituacao historico = new SolicitacaoMatriculaItemHistoricoSituacao();
            historico.SeqSolicitacaoMatriculaItem = seqSolicitacaoMatriculaItem;
            historico.SeqSituacaoItemMatricula = seqSituacao;

            if (motivo.HasValue)
                historico.MotivoSituacaoMatricula = motivo.Value;

            this.SaveEntity(historico);
        }
    }
}
