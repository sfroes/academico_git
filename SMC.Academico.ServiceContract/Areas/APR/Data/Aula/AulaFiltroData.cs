using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.APR.Data.Aula
{
    public class AulaFiltroData : ISMCMappable
    {
        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long SeqDivisaoTurma { get; set; }
    }
}