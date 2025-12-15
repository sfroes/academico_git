using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class EscalaApuracaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid17_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCMaxLength(100)]
        public string Descricao { get; set; }

        [SMCFilter]
        [SMCOrder(1)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        public bool? ApuracaoFinal { get; set; }

        [SMCFilter]
        [SMCOrder(2)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public bool? ApuracaoAvaliacao { get; set; }

        [SMCFilter]
        [SMCOrder(3)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }
    }
}