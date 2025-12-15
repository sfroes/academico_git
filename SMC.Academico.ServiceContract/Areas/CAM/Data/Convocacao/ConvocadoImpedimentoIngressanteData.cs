using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ConvocadoImpedimentoIngressanteData : ISMCMappable
    {
        public long SeqIngressante { get; set; }

        public string NomeIngressante { get; set; }

        public string Impedimento { get; set; }
    }
}
