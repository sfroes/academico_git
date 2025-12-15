using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.ORT
{
    public class ORTAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ORT";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                "ORT_WebApiRoute",
                "ORT/api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            context.MapRoute(
               "ORT_default",
               "ORT/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}