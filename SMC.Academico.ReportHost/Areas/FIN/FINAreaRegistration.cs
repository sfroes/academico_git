using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.FIN
{
    public class FINAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FIN";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                "FIN_WebApiRoute",
                "FIN/api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });
            
            context.MapRoute(
                "FIN_default",
                "FIN/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}