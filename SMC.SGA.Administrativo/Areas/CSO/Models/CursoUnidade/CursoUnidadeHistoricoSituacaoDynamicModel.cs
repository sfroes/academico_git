using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Models;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoUnidadeHistoricoSituacaoDynamicModel : EntidadeHistoricoSituacaoViewModel
    {
        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            base.ConfigureDynamic(ref options);
            options.ButtonBackIndex("Index", "CursoUnidade");
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new CursoUnidadeNavigationGroup(this);
        }
    }
}