using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class TipoHierarquiaClassificacaoController : SMCDynamicControllerBase
    {
        /// <summary>
        /// Chama a montagem da hierarquia. Se não tiver, dispara erro
        /// </summary>
        /// <param name="seqTipoHierarquiaClassificacao">Sequencial do tipo de hierarquia de classificação</param>
        [SMCAuthorize(UC_CSO_001_06_03.MONTAR_HIERARQUIA_TIPO_CLASSIFICACAO)]
        public ActionResult VerificaConfiguracaoTipoClassificacao(SMCEncryptedLong seqTipoHierarquiaClassificacao)
        {
            return RedirectToAction("Index", "TipoClassificacao", new { SeqTipoHierarquiaClassificacao = seqTipoHierarquiaClassificacao });
        }
    }
}