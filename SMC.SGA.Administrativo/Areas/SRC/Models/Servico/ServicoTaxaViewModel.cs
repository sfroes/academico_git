using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoTaxaViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.TaxasGRA))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public int SeqTaxaGra { get; set; }
    }
}