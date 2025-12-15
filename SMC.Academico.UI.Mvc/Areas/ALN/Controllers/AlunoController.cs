using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ALN.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Controllers
{
    public class AlunoController : SMCControllerBase
    {
        #region Services

        private IPeriodoIntercambioService PeriodoIntercambioService => Create<IPeriodoIntercambioService>();

        #endregion Services

        [SMCAllowAnonymous]
        public ActionResult VisualizarDadosIntercambioAluno(SMCEncryptedLong seqPeriodoIntercambio)
        {
            var model = PeriodoIntercambioService.BuscarDadosIntercambioPorAluno(seqPeriodoIntercambio).Transform<ConsultaDadosAlunoTermoIntercambioViewModel>();

            var view = GetExternalView(AcademicoExternalViews.VISUALIZAR_DADOS_INTERCAMBIO_ALUNO);
            return PartialView(view, model);
        }
    }
}