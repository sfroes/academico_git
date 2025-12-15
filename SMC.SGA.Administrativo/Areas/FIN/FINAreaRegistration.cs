using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.FIN
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
            context.MapRoute(
                "FIN_default",
                "FIN/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}