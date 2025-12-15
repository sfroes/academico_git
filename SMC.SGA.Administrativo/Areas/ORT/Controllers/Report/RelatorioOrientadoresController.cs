using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ORT.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORT.Controllers
{
    public class RelatorioOrientadoresController : SMCReportingControllerBase
    {
        public override string ControllerName => "RelatorioOrientadores";

        #region [ Serviços ]

        private ICursoService CursoService => this.Create<ICursoService>();
        private IEntidadeService EntidadeService => this.Create<IEntidadeService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        private ITipoSituacaoMatriculaService TipoSituacaoMatriculaService => Create<ITipoSituacaoMatriculaService>();

        #endregion [ Serviços ]

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("OrientadoresReport");

        #endregion

        [SMCAuthorize(UC_ORT_001_03_01.PESQUISAR_ORIENTADORES)]
        public ActionResult Index()
        {
            //var model = new RelatorioOrientadoresFiltroViewModel { EntidadesResponsaveis = CursoService.BuscarHierarquiaSuperiorCursoSelect() };
            var model = new RelatorioOrientadoresFiltroViewModel {
                EntidadesResponsaveis = EntidadeService.BuscarUnidadesResponsaveisGPILocalSelect(),
                TiposSituacaoMatricula = TipoSituacaoMatriculaService.BuscarTiposSituacoesMatriculasTokenMatriculadoSelect()
            };
            return View(model);
        }

        [SMCAuthorize(UC_ORT_001_03_02.EXIBIR_RELATÓRIO_ORIENTADORES)]
        public FileContentResult GerarRelatorio(RelatorioOrientadoresFiltroViewModel filtro)
        {
            var param = new
            {
                filtro.SeqEntidadeResponsavel,
                SeqPessoaAtuacao = filtro.SeqColaborador.Seq,
                filtro.ExibirOrientacoesFinalizadas,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq,
                filtro.SeqsTiposSituacoesMatriculas,
                SeqCicloLetivo = filtro.SeqCicloLetivo.Seq
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }
    }
}