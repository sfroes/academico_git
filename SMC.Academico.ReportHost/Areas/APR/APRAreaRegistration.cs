using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.APR
{
    public class APRAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "APR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                "APR_WebApiRoute",
                "APR/api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            context.MapRoute(
                "APR_default",
                "APR/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}