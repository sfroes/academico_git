using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.SRC
{
    public class SRCAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SRC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                "SRC_WebApiRoute",
                "SRC/api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            context.MapRoute(
                "SRC_default",
                "SRC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}