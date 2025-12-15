using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class MantenedoraFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid13_24)]
        [SMCMaxLength(100)]
        public string Nome { get; set; }
    }
}