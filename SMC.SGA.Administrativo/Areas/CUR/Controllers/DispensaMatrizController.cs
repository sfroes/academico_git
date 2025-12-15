using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class DispensaMatrizController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IDispensaService DispensaService
        {
            get { return this.Create<IDispensaService>(); }
        }

        #endregion [ Services ]

        public ActionResult CabecalhoDispensa(SMCEncryptedLong seq)
        {
            var model = ExecuteService<DispensaData, DispensaCabecalhoViewModel>(DispensaService.BuscarDispensa, seq);
            return PartialView("_Cabecalho", model);
        }
    }
}