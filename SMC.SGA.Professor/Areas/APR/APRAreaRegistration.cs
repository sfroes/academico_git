using SMC.Academico.UI.Mvc.Areas.APR.Controllers;
using System.Web.Mvc;

namespace SMC.SGA.Professor.Areas.APR
{
    public class APRAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "APR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {            

            //Registro da rota realizado fora do UI.Mvc por causa do Dynamic
            context.MapRoute(
                "APR_default",
                "APR/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {
                    "SMC.SGA.Professor.Areas.APR", //Ordem é importante, primeiro o sistema
                    typeof(MaterialController).Namespace, //Classe do UI.Mvc
                    "SMC.SGA.Professor.Areas.APR.Controllers"
                }
            );
        }
    }
}