using SMC.Academico.UI.Mvc;
using SMC.Framework.UI.Mvc;
using SMC.Seguranca.UI.Mvc.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Mobile
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
                     
            // Configura as rotas para os lookups do acadêmico
            routes.RegistrarRotaProfile();

            routes.RegistrarRotaAcademico();

            // configuração da rota do FileUpload
            routes.SMCMapUploadRoute(
                name: "FileUpload",
                url: "FileUpload",
                defaults: new { controller = "FileUpload", action = "Index", area = "" }
            );

            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
        }
    }
}