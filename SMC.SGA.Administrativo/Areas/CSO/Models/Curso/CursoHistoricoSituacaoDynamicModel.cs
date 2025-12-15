using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Models;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoHistoricoSituacaoDynamicModel : EntidadeHistoricoSituacaoDynamicModel
    {
        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            base.ConfigureDynamic(ref options);
            options.ButtonBackIndex("Index", "Curso");
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new CursoNavigationGroup(this);
        }
    }
}