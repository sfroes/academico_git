using SMC.Framework.Entity.Util;
using SMC.Framework.Fake;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Resources;
using SMC.SGA.Professor.Controllers;
using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Professor
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : SMCHttpApplication
    {
        private static readonly CultureInfo _culture = new System.Globalization.CultureInfo("pt-BR");

        protected override void OnStart()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Estragégias do fake
            FakeConfig.RegisterStrategies(SMCFakeStrategyConfiguration.Strategies);

            ControllerBuilder.Current.SMCRegisterFactory("SMC.Academico");
            //ControllerBuilder.Current.SetControllerFactory(typeof(SMCBaseControllerFactory));
            SMCEmbeddedVirtualPathProvider.RegisterAllAssemblies();
            SMCMiniProfilerConfigHelper.InitProfilerEF6Settings();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = _culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = _culture;
        }
    }
}