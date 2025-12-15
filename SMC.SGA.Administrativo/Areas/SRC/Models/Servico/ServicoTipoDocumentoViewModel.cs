using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoTipoDocumentoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.TiposDocumentoSelect))]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid16_24, SMCSize.Grid16_24, SMCSize.Grid16_24)]
        public long? SeqTipoDocumento { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCMaxLength(255)]
        public string DescricaoXSD { get; set; }
    }
}