using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoMotivoBloqueioParcelaViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.MotivosBloqueio))]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid16_24, SMCSize.Grid16_24, SMCSize.Grid16_24)]
        public long SeqMotivoBloqueio { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCSelect]
        [SMCConditionalRequired(nameof(SeqMotivoBloqueio), SMCConditionalOperation.GreaterThen, 0)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public bool? Obrigatorio { get; set; }
    }
}