using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class CriterioAprovacaoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCOrder(0)]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        [SMCCssClass("smc-size-md-15 smc-size-xs-16 smc-size-sm-16 smc-size-lg-16")]
        public string Descricao { get; set; }


        [SMCOrder(1)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public bool ApuracaoNota { get; set; }

        [SMCOrder(2)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public bool ApuracaoFrequencia { get; set; }

        [SMCOrder(3)]
        [SMCCssClass("smc-size-md-3 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public TipoArredondamento TipoArredondamento { get; set; }
    }
}