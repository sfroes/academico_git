using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.TUR
{
    public class TURAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TUR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                "TUR_WebApiRoute",
                "TUR/api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            context.MapRoute(
                "TUR_default",
                "TUR/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}