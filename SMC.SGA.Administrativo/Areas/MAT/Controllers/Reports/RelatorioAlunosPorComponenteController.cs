using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.MAT.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.MAT.Controllers
{
    public class RelatorioAlunosPorComponenteController : SMCReportingControllerBase
    {
        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("AlunosPorComponenteReport");

        #endregion APIS

        #region [ Services ]

        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        internal ITurnoService TurnoService => Create<ITurnoService>();
        internal ITurmaService TurmaService => Create<ITurmaService>();
        internal IConfiguracaoComponenteService ConfiguracaoComponenteService => Create<IConfiguracaoComponenteService>();

        #endregion

        #region [ Propriedades ]

        public override string ControllerName
        {
            get
            {
                return "RelatorioAlunosPorComponente";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\MAT\Reports\";
            }
        }

        #endregion [ Propriedades ]

        [SMCAuthorize(UC_MAT_005_07_01.PESQUISAR_ALUNOS_COMPONENTE_CURRICULAR)]
        public ActionResult Index()
        {
            var modelo = new RelatorioAlunosPorComponenteFiltroViewModel()
            {
                ExibirSolicitanteMatrículaNaoFinalizada = false
            };

            return View(modelo);
        }

        #region [ Renderizar o Relatório ]

        [SMCAuthorize(UC_MAT_005_07_01.PESQUISAR_ALUNOS_COMPONENTE_CURRICULAR)]
        public PartialViewResult Cabecalho()
        {
            return PartialView("_Cabecalho", new RelatorioAlunosPorComponenteCabecalhoViewModel());
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_005_07_01.PESQUISAR_ALUNOS_COMPONENTE_CURRICULAR)]
        public FileContentResult GerarRelatorio(RelatorioAlunosPorComponenteFiltroViewModel filtro)
        {
            var param = new
            {
                SeqCicloLetivo = filtro.SeqCicloLetivo?.Seq,
                SeqCursoOfertaLocalidade = filtro.SeqCursoOfertaLocalidade?.Seq,
                filtro.SeqTurno,
                filtro.SeqTurma,
                filtro.SeqConfiguracaoComponente,
                filtro.ExibirSolicitanteMatrículaNaoFinalizada,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }

        [SMCAuthorize(UC_MAT_005_07_01.PESQUISAR_ALUNOS_COMPONENTE_CURRICULAR)]
        public JsonResult BuscarTurnosPorCursoOfertaLocalidadeSelect(long seqCursoOfertaLocalidade)
        {
            var turnos = this.TurnoService.BuscarTurnosPorCursoOfertaLocalidadeSelect(seqCursoOfertaLocalidade);

            return Json(turnos);
        }

        [SMCAuthorize(UC_MAT_005_07_01.PESQUISAR_ALUNOS_COMPONENTE_CURRICULAR)]
        public JsonResult BuscarTurmasPorCicloLetivoCursoOfertaLocalidadeTurnoSelect(long? seqCicloLetivo, long? seqCursoOfertaLocalidade, long? seqTurno)
        {
            var turmas = this.TurmaService.BuscarTurmasPorCicloLetivoCursoOfertaLocalidadeTurnoSelect(seqCicloLetivo, seqCursoOfertaLocalidade, seqTurno);

            return Json(turmas);
        }

        [SMCAuthorize(UC_MAT_005_07_01.PESQUISAR_ALUNOS_COMPONENTE_CURRICULAR)]
        public JsonResult BuscarConfiguracoesComponentePorCicloLetivoCursoOfertaLocalidadeSelect(long? seqCicloLetivo, long? seqCursoOfertaLocalidade, long? seqTurno)
        {
            var configuracoesComponente = this.ConfiguracaoComponenteService.BuscarConfiguracoesComponentePorCicloLetivoCursoOfertaLocalidadeSelect(seqCicloLetivo, seqCursoOfertaLocalidade, seqTurno);

            return Json(configuracoesComponente);
        }

        #endregion [ Renderizar o Relatório ]

    }
}