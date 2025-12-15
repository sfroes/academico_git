using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class TipoHierarquiaEntidadeItemController : SMCDynamicControllerBase
    {
        #region Serviços

        internal ITipoHierarquiaEntidadeService TipoHierarquiaEntidadeService
        {
            get { return this.Create<ITipoHierarquiaEntidadeService>(); }
        }

        #endregion Serviços

        public ActionResult CabecalhoTipoHierarquiaEntidade(SMCEncryptedLong SeqTipoHierarquiaEntidade)
        {
            TipoHierarquiaEntidadeCabecalhoViewModel model = ExecuteService<TipoHierarquiaEntidadeData, TipoHierarquiaEntidadeCabecalhoViewModel>(TipoHierarquiaEntidadeService.BuscarTipoHierarquiaEntidade, SeqTipoHierarquiaEntidade);
            return PartialView("_Cabecalho", model);
        }
    }
}