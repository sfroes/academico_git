using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data.Aula
{
    public class AulaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDivisaoTurma { get; set; }

        public DateTime DataAula { get; set; }

        public List<AulaOfertaData> Ofertas { get; set; }
    }
}