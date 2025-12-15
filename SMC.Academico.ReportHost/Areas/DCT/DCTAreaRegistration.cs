using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.DCT
{
    public class DCTAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DCT";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                "DCT_WebApiRoute",
                "DCT/api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            context.MapRoute(
                "DCT_default",
                "DCT/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}