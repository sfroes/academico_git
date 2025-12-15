using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        [SMCSortable(true, false)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCSortable(true, false)]
        [SMCCssClass("smc-size-md-10 smc-size-xs-10 smc-size-sm-10 smc-size-lg-10")]
        public string Descricao { get; set; }

        [SMCIgnoreProp]
        public List<CampanhaCicloLetivoViewModel> CiclosLetivos { get; set; }

        [SMCCssClass("smc-size-md-10 smc-size-xs-10 smc-size-sm-10 smc-size-lg-10")]
        public string DisplayCiclosLetivos
        {
            get
            {
                return string.Join(", ", CiclosLetivos.Select(f => f.Descricao));
            }
        }
    }
}