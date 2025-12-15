using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConsultarTaxasPorNucleoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        public int SeqOrigem { get; set; }

        public string DescricaoOrigem { get; set; }

        public string DescricaoOrigemCompleta
        {
            get
            {
                return $"{SeqOrigem.ToString().PadLeft(2, '0')} - {DescricaoOrigem}";
            }
        }

        public int SeqTaxaGra { get; set; }

        public string DescricaoTaxa { get; set; }

        public List<ConsultarTaxasPorNucleoListarItemViewModel> TaxasPorNucleo { get; set; }
    }
}