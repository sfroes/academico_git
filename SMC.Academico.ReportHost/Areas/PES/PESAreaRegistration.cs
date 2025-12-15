using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.PES
{
    public class PESAreaRegistration : AreaRegistration
    {
        public override string AreaName => "PES";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                "PES_WebApiRoute",
                "PES/api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            context.MapRoute(
                "PES_default",
                "PES/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}