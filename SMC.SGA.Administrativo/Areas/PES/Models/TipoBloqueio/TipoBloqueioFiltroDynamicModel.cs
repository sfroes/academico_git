using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TipoBloqueioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCSize(Framework.SMCSize.Grid3_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid3_24)]
        [SMCFilter(true, true)]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid9_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid14_24, Framework.SMCSize.Grid10_24)]
        [SMCFilter(true, true)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCSize(Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid6_24, Framework.SMCSize.Grid4_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCFilter(true, true)]
        public string Token { get; set; }

    }
}