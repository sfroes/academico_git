using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Formularios.ServiceContract.Areas.TMP;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Interfaces
{
    public interface ISGFHelperService : ISMCService
    {
        /// <summary>
        /// Busca as etapas de um template de processo no SGF, considerando cache
        /// </summary>
        /// <param name="seqTemplateProcesso"></param>
        /// <returns></returns>
        EtapaSimplificadaData[] BuscarEtapasSGFCache(long seqTemplateProcesso);

        /// <summary>
        /// Busca as etapas do SGF de uma determinada solicitação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Seq da solicitação de serviço</param>
        /// <returns>Etapas</returns>
        List<EtapaListaData> BuscarEtapas(long seqSolicitacaoServico);

        /// <summary>
        /// Busca a etapa de acordo com ingressante, solicitação de serviço e configuração da etapa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração etapa</param>
        /// <returns>Objeto da etapa atual</returns>
        EtapaListaData BuscarEtapa(long seqSolicitacaoServico, long seqConfiguracaoEtapa);
    }
}