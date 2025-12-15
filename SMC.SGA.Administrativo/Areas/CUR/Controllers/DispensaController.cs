using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class DispensaController : SMCDynamicControllerBase
    {
        [SMCAuthorize(UC_CUR_003_02_01.PESQUISAR_DISPENSA)]
        public ActionResult CabecalhoLista(DispensaDynamicModel model)
        {
            return PartialView("_CabecalhoLista", model);
        }
    }
}