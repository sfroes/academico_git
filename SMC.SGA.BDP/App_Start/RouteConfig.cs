
using SMC.Framework.UI.Mvc;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.BDP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.RegistrarRotaProfile();
            //routes.RegistrarRotaSGF();
            //routes.RegistrarRotaLocalidade();

            // configuração da rota do FileUpload
            routes.SMCMapUploadRoute(
                name: "FileUpload",
                url: "FileUpload",
                defaults: new { controller = "FileUpload", action = "Index", area = "" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{instituicao}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}