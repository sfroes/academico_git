using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorVinculoFormacaoEspecificaData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqFormacaoEspecifica { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
    }
}