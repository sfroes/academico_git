using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.CUR
{
    public class CURAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CUR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                "CUR_WebApiRoute",
                "CUR/api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            context.MapRoute(
                "CUR_default",
                "CUR/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}