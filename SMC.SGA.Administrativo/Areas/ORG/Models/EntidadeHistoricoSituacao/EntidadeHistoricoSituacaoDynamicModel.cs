using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Models;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeHistoricoSituacaoDynamicModel : EntidadeHistoricoSituacaoViewModel
    {
        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            base.ConfigureDynamic(ref options);
            options.ButtonBackIndex("Index", "Entidade");
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new EntidadeHistoricoSituacaoNavigationGroup(this);
        }
    }
}