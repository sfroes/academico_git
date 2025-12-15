using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConsultarTaxasPorNucleoListarData : ISMCMappable
    {
        public int SeqOrigem { get; set; }

        public string DescricaoOrigem { get; set; }

        public int SeqTaxaGra { get; set; }

        public string DescricaoTaxa { get; set; }

        public List<ConsultarTaxasPorNucleoListarItemData> TaxasPorNucleo { get; set; }
    }
}
