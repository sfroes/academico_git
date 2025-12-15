using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Models;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ProgramaHistoricoSituacaoDynamicModel : EntidadeHistoricoSituacaoDynamicModel
    {
        [SMCHidden]
        [SMCParameter]
        public long SeqHierarquiaEntidadeItem { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            base.ConfigureDynamic(ref options);
            options.ButtonBackIndex("Index", "Programa");
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new ProgramaNavigationGroup(this);
        }
    }
}