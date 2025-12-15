using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoEnsinoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCMaxLength(100)]
        public string Nome { get; set; }
    }
}