using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Filters;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.SGA.Professor
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SMCHandleErrorAttribute());

            // Filtro para monitorar a navegação pelo site
            filters.Add(new SMCAccessMonitoringAttribute());

            // Filtro para o botão voltar
            filters.Add(new SMCBackButtonFilterAttribute());
        }
    }
}