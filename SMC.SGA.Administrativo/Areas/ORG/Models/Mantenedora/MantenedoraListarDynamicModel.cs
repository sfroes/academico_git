using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class MantenedoraListarDynamicModel : SMCDynamicViewModel
    {
        [SMCSize(SMCSize.Grid6_24)]
        [SMCOrder(0)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCOrder(1)]
        [SMCMaxLength(100)]
        [SMCSortable(true, true)]
        public string Nome { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCOrder(2)]
        [SMCMaxLength(15)]
        public string Sigla { get; set; }
    }
}