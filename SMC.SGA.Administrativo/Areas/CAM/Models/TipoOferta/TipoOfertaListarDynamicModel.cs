using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class TipoOfertaListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCSortable]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public override long Seq { get; set; }

        [SMCSortable]
        [SMCDescription]
        [SMCCssClass("smc-size-md-16 smc-size-xs-16 smc-size-sm-16 smc-size-lg-16")]
        public string Descricao { get; set; }

        [SMCSortable]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public bool ExigeCursoOfertaLocalidadeTurno { get; set; }
    }
}