using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ProgramaTipoAutorizacaoBdpVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPrograma { get; set; }

        public TipoAutorizacao TipoAutorizacao { get; set; }

        public short? NumeroDiasDuracaoAutorizacao { get; set; }

    }
}
