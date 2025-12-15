using SMC.Framework;
using SMC.Framework.UI.Mvc;
using System.Web.Mvc;

namespace SMC.SGA.Mobile.Controllers
{
    public class HomeController : SMCControllerBase
    {      
        
        [SMCAllowAnonymous]
        public ActionResult Index()
        {           
            return View();
        }
    }
}