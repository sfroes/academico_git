using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class TipoProcessoSeletivoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCSortable]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCSortable(true, true)]
        [SMCCssClass("smc-size-md-10 smc-size-xs-10 smc-size-sm-10 smc-size-lg-10")]
        public string Descricao { get; set; }

        [SMCSortable]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public bool IngressoDireto { get; set; }

        [SMCSortable]
        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        public TipoCalculoDataAdmissao TipoCalculoDataAdmissao { get; set; }
    }
}