using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class CicloLetivoController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        private IInstituicaoNivelRegimeLetivoService InstituicaoNivelRegimeLetivoService
        {
            get { return this.Create<IInstituicaoNivelRegimeLetivoService>(); }
        }

        #endregion [ Serviços ]

        [SMCAuthorize(UC_CAM_002_01_01.PESQUISAR_CICLO_LETIVO)]
        public JsonResult BuscarNiveisEnsinoDoRegimeSelect(long seqRegimeLetivo)
        {
            List<SMCDatasourceItem> niveisEnsino = this.InstituicaoNivelRegimeLetivoService.BuscarNiveisEnsinoDoRegimeSelect(seqRegimeLetivo);
            return Json(niveisEnsino);
        }

        [SMCAuthorize(UC_CAM_002_01_05.COPIAR_CICLO_LETIVO)]
        public ActionResult CopiaCicloLetivo()
        {
            return PartialView("_CopiaCicloLetivo");
        }
    }
}