using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class SolicitacaoHistoricoSituacaoService : SMCServiceBase, ISolicitacaoHistoricoSituacaoService
    {
        private SolicitacaoHistoricoSituacaoDomainService SolicitacaoHistoricoSituacaoDomainService
        {
            get { return this.Create<SolicitacaoHistoricoSituacaoDomainService>(); }
        }

        public void AtualizarHistoricoSituacao(long seqSolicitacaoServicoEtapa, long seqSituacaoEtapaSGF)
        {
            SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(seqSolicitacaoServicoEtapa, seqSituacaoEtapaSGF);
        }
    }
}