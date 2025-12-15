using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ProgramaTipoAutorizacaoBdpData : ISMCMappable, ISMCSeq​​
    {

        public long Seq { get; set; }

        public long SeqPrograma { get; set; }

        public TipoAutorizacao TipoAutorizacao { get; set; }

        public short? NumeroDiasDuracaoAutorizacao { get; set; }
    }
}
