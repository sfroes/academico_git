using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class TipoOfertaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter]
        [SMCSize(SMCSize.Grid16_24)]
        public string Descricao { get; set; }
    }
}