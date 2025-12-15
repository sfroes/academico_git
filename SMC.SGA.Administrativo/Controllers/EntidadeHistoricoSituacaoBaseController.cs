using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Controllers
{
    public abstract class EntidadeHistoricoSituacaoBaseController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        #endregion [ Services ]

        public ActionResult CabecalhoEntidadeHistoricoSituacao(SMCEncryptedLong seqEntidade)
        {
            var modelCabecalho =
                ExecuteService<EntidadeCabecalhoData, EntidadeHistoricoSituacaoCabecalhoViewModel>(EntidadeService.BuscarEntidadeCabecalho, seqEntidade);
            return PartialView("_CabecalhoEntidade", modelCabecalho);
        }
    }
}