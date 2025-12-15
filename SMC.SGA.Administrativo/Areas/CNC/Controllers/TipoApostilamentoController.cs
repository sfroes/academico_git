using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Extensions;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CNC.Controllers
{
    public class TipoApostilamentoController : SMCDynamicControllerBase
    {
        [SMCAuthorize(UC_CNC_004_03_01.MANTER_TIPO_APOSTILAMENTO)]
        public JsonResult BuscarInstituicaoLogada(bool? retornarInstituicaoEnsinoLogada)
        {
            return Json(HttpContext.GetInstituicaoEnsinoLogada().Seq);
        }
    }
}