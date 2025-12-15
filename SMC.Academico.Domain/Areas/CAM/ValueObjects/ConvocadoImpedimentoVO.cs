using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class ConvocadoImpedimentoVO : ISMCMappable
    {
        public ConvocadoImpedimentoVO()
        {
            Impedimentos = new List<string>();
        }

        public long SeqIngressante { get; set; }

        public string NomeIngressante { get; set; }

        public List<string> Impedimentos { get; set; }
    }
}