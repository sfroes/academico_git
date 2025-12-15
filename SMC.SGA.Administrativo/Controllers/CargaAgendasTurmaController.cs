using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.SGA.Administrativo.Models;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Views.CargaAgendasTurma.App_LocalResources;
using SMC.Framework.UI.Mvc.Html;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;

namespace SMC.SGA.Administrativo.Controllers
{
    public class CargaAgendasTurmaController : SMCControllerBase
    {
        #region [ Services ]   

        private ITurmaService TurmaService
        {
            get { return this.Create<ITurmaService>(); }
        }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [SMCAllowAnonymous]
        public ActionResult ExecutarCargaAgendasTurma(CargaAgendasTurmaViewModel modelo)
        {
            this.TurmaService.CargaAgendasTurma(modelo.SeqCicloLetivo.Seq.Value);
            
            SetSuccessMessage(UIResource.Mensagem_Sucesso_Carga, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Index));
        }
    }
}