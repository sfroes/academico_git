using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.APR.Models.EntregaOnline;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.APR.Controllers
{
    public class EntregaOnlineSharedController : SMCControllerBase
    {
        public IEntregaOnlineService EntregaOnlineService => Create<IEntregaOnlineService>();

        [SMCAuthorize(UC_APR_001_09_01.HISTORICO_SITUACAO_ENTREGA_ONLINE)]
        [SMCAllowAnonymous]
        public ActionResult HistoricoSituacaoEntrega(SMCEncryptedLong seqEntregaOnline)
        {
            if (seqEntregaOnline == 0 || seqEntregaOnline == null)
                throw new SMCApplicationException("Parâmetros Incorretos.");

            var model = EntregaOnlineService.BuscarHistoricoSituacaoEntrega(seqEntregaOnline).Transform<HistoricoSituacaoEntregaOnlineViewModel>();

            var view = GetExternalView(AcademicoExternalViews.ENTREGA_ONLINE_HISTORICO_SITUACAO);
            
            if (Request.IsAjaxRequest())
                return PartialView(view, model);

            return View(view, model);
        }
    }
}