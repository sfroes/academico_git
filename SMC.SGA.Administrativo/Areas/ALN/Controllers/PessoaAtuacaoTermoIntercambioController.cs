using SMC.Framework.UI.Mvc.Dynamic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class PessoaAtuacaoTermoIntercambioController : SMCDynamicControllerBase
    {
        public ActionResult Legenda()
        {
            return PartialView("_Legenda");
        }
    }
}