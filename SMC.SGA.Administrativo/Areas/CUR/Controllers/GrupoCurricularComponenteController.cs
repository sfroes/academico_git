using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class GrupoCurricularComponenteController : SMCDynamicControllerBase
    {
        private IGrupoCurricularComponenteService GrupoCurricularComponenteService => Create<IGrupoCurricularComponenteService>();

        [SMCAuthorize(UC_CUR_001_01_07.ASSOCIAR_COMPONENTE_CURRICULAR_GRUPO)]
        public ActionResult ExcluirGrupoCurricularComponente(SMCEncryptedLong seq, SMCEncryptedLong seqCurriculo)
        {
            try
            {
                this.GrupoCurricularComponenteService.Excluir(seq);
                SetSuccessMessage(Views.GrupoCurricularComponente.App_LocalResources.UIResource.Mensagem_Sucesso_Exclusao_Grupo_Curricular_Componente, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction("Index", "GrupoCurricular", routeValues: new { seqCurriculo = new SMCEncryptedLong(seqCurriculo) });
        }
    }
}