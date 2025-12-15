using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class RelatorioIdentidadeAcademicaController : SMCReportingControllerBase
    {
        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("IdentidadeAcademicaReport");

        #endregion APIS

        #region Serviços

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion Serviços

        public override string ControllerName => "Relatorio";

        public override string ReportBasePath => @"\Areas\PES\Reports\";

        [SMCAllowAnonymous]
        public FileContentResult ApresentarRelatorio()
        {
            return new FileContentResult(TempData["Relatorio"] as byte[], "application/pdf");
        }

        [SMCAllowAnonymous]
        public JsonResult GerarRelatorio(RelatorioIdentidadeAcademicaFiltroViewModel model)
        {
            var param = new
            {
                SeqsAlunos = model.Alunos?.Where(w => w.Aluno?.Seq != null).Select(s => s.Aluno.Seq.Value),
                SeqsColaboradores = model.Colaboradores?.Where(w => w.Colaborador?.Seq != null).Select(s => s.Colaborador.Seq.Value),
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            TempData["Relatorio"] = dadosReport;
            return Json("");
        }

        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}