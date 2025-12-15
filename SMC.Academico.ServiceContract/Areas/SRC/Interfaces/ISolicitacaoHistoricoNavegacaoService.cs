using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ISolicitacaoHistoricoNavegacaoService : ISMCService
    {
        SolicitacaoHistoricoNavegacaoData BuscarSolicitacaoHistoricoNavegacao(long seqSolicitacaoServicoEtapa, long seqConfiguracaoEtapaPagina, bool inserir);

        void AtualizarDataSaidaSolicitacaoHistoricoNavegacao(long seqSolicitacaoHistoricoNavegacao);
    }
}