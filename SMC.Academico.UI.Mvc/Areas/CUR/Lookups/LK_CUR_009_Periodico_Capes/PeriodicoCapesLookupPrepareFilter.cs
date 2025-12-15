using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class PeriodicoCapesLookupPrepareFilter : ISMCFilter<PeriodicoCapesLookupFiltroViewModel>
    {
        public PeriodicoCapesLookupFiltroViewModel Filter(SMCControllerBase controllerBase, PeriodicoCapesLookupFiltroViewModel filter)
        {
            filter.ClassificacaoPeriodicoAtual = true;
            return filter;
        }
    }
}
