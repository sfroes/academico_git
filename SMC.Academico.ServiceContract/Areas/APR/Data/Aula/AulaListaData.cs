using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.APR.Data.Aula
{
    public class AulaListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public DateTime DataAula { get; set; }

        public long SeqDivisaoTurma { get; set; }
    }
}