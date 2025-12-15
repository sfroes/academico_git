using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.MAT
{
    public class MATAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MAT";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                "MAT_WebApiRoute",
                "MAT/api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            context.MapRoute(
                "MAT_default",
                "MAT/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}