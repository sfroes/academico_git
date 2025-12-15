using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.SRC
{
    public class SRCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SRC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SRC_default",
                "SRC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}