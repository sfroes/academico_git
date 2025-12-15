using SMC.Academico.Domain.Helpers;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Formularios.ServiceContract.Areas.TMP;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Services
{
    public class SGFHelperService : SMCServiceBase, ISGFHelperService
    {
        public EtapaListaData BuscarEtapa(long seqSolicitacaoServico, long seqConfiguracaoEtapa)
        {
            return SGFHelper.BuscarEtapa(seqSolicitacaoServico, seqConfiguracaoEtapa).Transform<EtapaListaData>();
        }

        public List<EtapaListaData> BuscarEtapas(long seqSolicitacaoServico)
        {
            return SGFHelper.BuscarEtapas(seqSolicitacaoServico).TransformList<EtapaListaData>();
        }

        public EtapaSimplificadaData[] BuscarEtapasSGFCache(long seqTemplateProcesso)
        {
            return SGFHelper.BuscarEtapasSGFCache(seqTemplateProcesso).TransformListToArray<EtapaSimplificadaData>();
        }
    }
}