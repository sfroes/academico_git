using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoCobrancaTaxaItemViewModel : SMCViewModelBase
    {
        [SMCCssClass("smc-size-md-20 smc-size-xs-20 smc-size-sm-20 smc-size-lg-20")]
        public string DescricaoTaxa { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string ValorTaxa { get; set; }
    }
}
