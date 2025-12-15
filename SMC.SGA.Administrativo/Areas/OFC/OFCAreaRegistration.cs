using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC
{
    public class OFCAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "OFC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "OFC_default",
                "OFC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
