using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class SolicitacaoHistoricoNavegacaoService : SMCServiceBase, ISolicitacaoHistoricoNavegacaoService
    {
        private SolicitacaoHistoricoNavegacaoDomainService SolicitacaoHistoricoNavegacaoDomainService
        {
            get { return this.Create<SolicitacaoHistoricoNavegacaoDomainService>(); }
        }

        public void AtualizarDataSaidaSolicitacaoHistoricoNavegacao(long seqSolicitacaoHistoricoNavegacao)
        {
            SolicitacaoHistoricoNavegacaoDomainService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(seqSolicitacaoHistoricoNavegacao);
        }

        public SolicitacaoHistoricoNavegacaoData BuscarSolicitacaoHistoricoNavegacao(long seqSolicitacaoServicoEtapa, long seqConfiguracaoEtapaPagina, bool inserir)
        {
            return SolicitacaoHistoricoNavegacaoDomainService.BuscarSolicitacaoHistoricoNavegacao(seqSolicitacaoServicoEtapa, seqConfiguracaoEtapaPagina, inserir).Transform<SolicitacaoHistoricoNavegacaoData>();
        }
    }
}