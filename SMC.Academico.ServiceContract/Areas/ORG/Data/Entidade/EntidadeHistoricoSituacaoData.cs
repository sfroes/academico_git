using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeHistoricoSituacaoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqEntidade { get; set; }

        public long SeqSituacaoEntidade { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public EntidadeData Entidade { get; set; }

        public SituacaoEntidadeData SituacaoEntidade { get; set; }
    }
}
