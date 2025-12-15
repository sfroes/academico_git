using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.ORT
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
            context.MapRoute(
                "ORT_default",
                "ORT/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}