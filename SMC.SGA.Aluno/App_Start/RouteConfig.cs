
using SMC.Academico.UI.Mvc;
using SMC.Financeiro.UI.Mvc.Util;
using SMC.Formularios.UI.Mvc;
using SMC.Framework.UI.Mvc;
using SMC.Localidades.UI.Mvc;
using SMC.Seguranca.UI.Mvc.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Aluno
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // configuração de rota para uso de formulários do sgf
            routes.RegistrarRotaSGF();

            routes.RegistrarRotaLocalidade();

            // Configura as rotas para os lookups do acadêmico
            routes.RegistrarRotaProfile();
            routes.RegistrarRotaAcademico();

            // configuração da rota do Financeiro
            routes.RegistraRotaFinanceiro();

            // configuração da rota do FileUpload
            routes.SMCMapUploadRoute(
                name: "FileUpload",
                url: "FileUpload",
                defaults: new { controller = "FileUpload", action = "Index", area = "" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SMC.SGA.Aluno.Controllers" }
            );
        }
    }
}