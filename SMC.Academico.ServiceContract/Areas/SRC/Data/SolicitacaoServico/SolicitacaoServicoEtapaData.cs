using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoServicoEtapaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public List<SolicitacaoHistoricoNavegacaoData> HistoricosNavegacao { get; set; }

        public List<SolicitacaoHistoricoSituacaoData> HistoricosSituacao { get; set; }

        /*
        public virtual ConfiguracaoEtapaData ConfiguracaoEtapa { get; set; }

        public virtual SolicitacaoServicoData SolicitacaoServico { get; set; }*/
    }
}