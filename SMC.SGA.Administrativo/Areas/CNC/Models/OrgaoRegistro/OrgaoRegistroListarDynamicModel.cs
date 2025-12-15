using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class OrgaoRegistroListarDynamicModel : SMCDynamicViewModel
    {
        [SMCSortable(true, true)]
        public override long Seq { get; set; }
       
        [SMCSortable(true)]
        public string Descricao { get; set; }

        [SMCSortable(true)]
        public string Sigla { get; set; }
    }
}