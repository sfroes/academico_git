using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class CriterioAprovacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid7_24)]
        [SMCMaxLength(255)]
        public string Descricao { get; set; }


        [SMCFilter]
        [SMCOrder(1)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        public bool? ApuracaoNota { get; set; }

        [SMCFilter]
        [SMCOrder(2)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public bool? ApuracaoFrequencia { get; set; }

        [SMCFilter]
        [SMCOrder(3)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid6_24)]
        public TipoArredondamento? TipoArredondamento { get; set; }
    }
}