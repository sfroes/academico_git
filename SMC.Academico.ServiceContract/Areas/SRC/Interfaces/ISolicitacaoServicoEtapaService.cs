using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public interface ISolicitacaoServicoEtapaService : ISMCService
    {
        /// <summary>
        /// Este serviço tem como objetivo iniciar ou continuar uma etapa de uma solicitação de serviço.
        /// </summary>
        /// <param name="seqSolicitacaoServico"></param>
        /// <returns></returns>
        SolicitacaoServicoEtapaData ObterUltimaEtapaIniciadaMatricula(long seqSolicitacaoServico);

        /// <summary>
        /// Verifica se a etapa pode ser acessada
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviçp</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        void VerificarAcessoEtapa(long seqSolicitacaoServico, long seqConfiguracaoEtapa, bool ignoreFinalizedValidation = false);
    }
}