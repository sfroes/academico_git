using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ProgramaHistoricoNotaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public short ValorNota { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
    }
}
