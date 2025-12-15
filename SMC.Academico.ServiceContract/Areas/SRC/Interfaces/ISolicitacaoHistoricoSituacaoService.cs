using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ISolicitacaoHistoricoSituacaoService : ISMCService
    {
        void AtualizarHistoricoSituacao(long seqSolicitacaoServicoEtapa, long seqSituacaoEtapaSGF);
    }
}