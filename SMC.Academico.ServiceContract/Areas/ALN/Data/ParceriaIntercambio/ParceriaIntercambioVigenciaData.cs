using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public  class ParceriaIntercambioVigenciaData : ISMCMappable
    { 
        public long Seq { get; set; }

        public DateTime DataInicio { get; set; }
         
        public DateTime? DataFim { get; set; }

        public string Justificativa { get; set; }
    }
}
