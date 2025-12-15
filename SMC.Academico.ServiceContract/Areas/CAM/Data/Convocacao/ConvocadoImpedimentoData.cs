using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ConvocadoImpedimentoData : ISMCMappable
    {
        public long SeqIngressante { get; set; }

        public string NomeIngressante { get; set; }

        public List<string> Impedimentos { get; set; }
    }
}