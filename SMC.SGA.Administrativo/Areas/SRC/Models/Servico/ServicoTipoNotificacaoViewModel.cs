using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoTipoNotificacaoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.EtapasTemplateProcessoSgf))]
        [SMCSize(SMCSize.Grid7_24)]
        public long? SeqEtapaSgf { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.TiposNotificacaoSGA))]
        [SMCSize(SMCSize.Grid10_24)]
        public long? SeqTipoNotificacao { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid5_24)]
        public bool? Obrigatorio { get; set; }
    }
}