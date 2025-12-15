using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.APR.Controllers
{
    public class InstituicaoNivelCriterioAprovacaoController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        private IInstituicaoNivelCriterioAprovacaoService InstituicaoNivelCriterioAprovacaoService
        {
            get { return this.Create<IInstituicaoNivelCriterioAprovacaoService>(); }
        }

        #endregion [ Serviços ]

        [SMCAuthorize(UC_APR_003_02_01.MANTER_CRITERIO_APROVACAO_INSTITUICAO_NIVEL)]
        public ActionResult BuscarCriteriosAprovacaoNivelEnsinoSelect(SMCEncryptedLong seqInstituicaoNivel)
        {
            var criteriosAprovacao = this.InstituicaoNivelCriterioAprovacaoService.BuscarCriteriosAprovacaoDaInstituicaoNivelSelect(seqInstituicaoNivel);
            return Json(criteriosAprovacao);
        }
    }
}