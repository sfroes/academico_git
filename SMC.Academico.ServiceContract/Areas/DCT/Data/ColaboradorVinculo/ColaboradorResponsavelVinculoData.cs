using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorResponsavelVinculoData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqColaboradorResponsavel { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
    }
}