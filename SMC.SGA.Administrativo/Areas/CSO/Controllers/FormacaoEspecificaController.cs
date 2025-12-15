using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class FormacaoEspecificaController : SMCDynamicControllerBase
    {
        #region [ Servico ]

        private IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        #endregion [ Servico ]

        public ActionResult CabecalhoFormacaoEspecifica(SMCEncryptedLong seqEntidadeResponsavel)
        {
            var modelCabecalho = ExecuteService<EntidadeCabecalhoData, FormacaoEspecificaCabecalhoViewModel>(EntidadeService.BuscarEntidadeCabecalho, seqEntidadeResponsavel);
            return PartialView("_Cabecalho", modelCabecalho);
        }

        public ActionResult MensagemFormacaoEspecifica()
        {
            return PartialView("_MensagemTreeView");
        }
    }
}