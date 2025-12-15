using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class HistoricoSolicitacaoData : ISMCMappable
    {
        public List<HistoricoSolicitacaoEtapaData> Etapas { get; set; }

        public List<HistoricoSolicitacaoEtapaItemData> Historicos { get; set; }
    }
}