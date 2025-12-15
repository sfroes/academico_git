using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using SMC.SGA.Administrativo.Areas.ORG.Views.HierarquiaEntidadeItem.App_LocalResources;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class HierarquiaEntidadeItemController : SMCDynamicControllerBase
    {
        #region Services

        private IHierarquiaEntidadeService HierarquiaEntidadeService
        {
            get { return this.Create<IHierarquiaEntidadeService>(); }
        }

        private IHierarquiaEntidadeItemService HierarquiaEntidadeItemService
        {
            get { return this.Create<IHierarquiaEntidadeItemService>(); }
        }

        #endregion Services

        public ActionResult CabecalhoHierarquiaEntidadeItem(SMCEncryptedLong SeqHierarquiaEntidade)
        {
            HierarquiaEntidadeItemCabecalhoViewModel model = ExecuteService<HierarquiaEntidadeData, HierarquiaEntidadeItemCabecalhoViewModel>(HierarquiaEntidadeService.BuscarHierarquiaEntidade, SeqHierarquiaEntidade);
            return PartialView("_Cabecalho", model);
        }

        public ActionResult MensagemHierarquiaEntidadeItem()
        {
            return PartialView("_MensagemTreeView");
        }
    }
}