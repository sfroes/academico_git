using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class PosicaoConsolidadaController : SMCDynamicControllerBase
    {
        [SMCAuthorize(UC_SRC_005_01_01.CONSULTAR_POSICAO_CONSOLIDADA)]
        public ActionResult CabecalhoProcesso(SMCEncryptedLong seqProcesso)
        {
            return new ProcessoController().CabecalhoProcesso(seqProcesso, true);
        }
    }
}