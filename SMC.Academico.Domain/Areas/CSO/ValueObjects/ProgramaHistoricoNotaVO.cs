using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ProgramaHistoricoNotaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public short ValorNota { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
    }
}
