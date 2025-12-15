using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.GRD
{
    public class GRDAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GRD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GRD_default",
                "GRD/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}