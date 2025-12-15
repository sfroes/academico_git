using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Extensions;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class ParceriaIntercambioController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IEntidadeService EntidadeService
        {
            get { return Create<IEntidadeService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_ALN_004_01_01.PESQUISAR_PARCERIA_INTERCAMBIO)]
        public JsonResult BuscarInstituicaoLogadaSelect(long seqInstituicaoEnsino)
        {
            return Json(EntidadeService.BuscarEntidadeCabecalho(this.GetInstituicaoEnsinoLogada().Seq).Nome);
        }
    }
}
