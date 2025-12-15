using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConsultarTaxasPorNucleoListarVO : ISMCMappable
    {
        public int SeqOrigem { get; set; }

        public string DescricaoOrigem { get; set; }

        public int SeqTaxaGra { get; set; }

        public string DescricaoTaxa { get; set; }

        public List<ConsultarTaxasPorNucleoListarItemVO> TaxasPorNucleo { get; set; }
    }
}
