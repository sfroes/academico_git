using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.TUR.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.TUR.Controllers
{
    public class TurmaRelatorioController : SMCReportingControllerBase
    {
        #region [Campos privados]

        private static List<SMCDatasourceItem> _entidadesResponsaveis { get; set; }

        #endregion

        #region  [ Propriedades Relatório ]

        public override string ControllerName
        {
            get
            {
                return "Turma";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\TUR\Reports\";
            }
        }

        #endregion [ Propriedades Relatório ]

        #region [ Serviços ]

        internal ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();

        internal ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => Create<ICursoOfertaLocalidadeService>();

        internal IDivisaoTurmaColaboradorService DivisaoTurmaColaboradorService => Create<IDivisaoTurmaColaboradorService>();

        internal IEntidadeService EntidadeService => Create<IEntidadeService>();

        internal IHierarquiaEntidadeService HierarquiaEntidadeService => Create<IHierarquiaEntidadeService>();

        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        internal IInstituicaoNivelService InstituicaoNivelService => Create<IInstituicaoNivelService>();

        internal ITipoTurmaService TipoTurmaService => Create<ITipoTurmaService>();

        internal ITurmaColaboradorService TurmaColaboradorService => Create<ITurmaColaboradorService>();

        internal ITurmaService TurmaService => Create<ITurmaService>();

        internal ITurnoService TurnoService => Create<ITurnoService>();

        #endregion

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("TurmaReport");

        #endregion APIS

        [SMCAuthorize(UC_TUR_001_04_01.PESQUISAR_TURMA)]
        public ActionResult Index()
        {
            var modelo = new TurmaRelatorioFiltroViewModel()
            {
                EntidadesResponsaveis = EntidadeService.BuscarUnidadesResponsaveisGPILocalSelect(),
                NiveisEnsino = this.InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect(),
                TiposTurma = this.TipoTurmaService.BuscarTiposTurmasSelect()
            };

            _entidadesResponsaveis = modelo.EntidadesResponsaveis;

            return View(modelo);
        }

        [SMCAuthorize(UC_TUR_001_04_01.PESQUISAR_TURMA)]
        public JsonResult GerarRelatorio(TurmaRelatorioFiltroViewModel filtro)
        {
            var param = new
            {
                SeqCicloLetivo = filtro.SeqCicloLetivo?.Seq,
                filtro.SeqNivelEnsino,
                filtro.SeqsEntidadesResponsaveis,
                SeqCursoOferta = filtro.SeqCursoOferta?.Seq,
                filtro.SeqTipoTurma,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            TempData["RelatorioTurmaCiclo"] = dadosReport;

            return Json("");
        }

        [SMCAuthorize(UC_TUR_001_04_01.PESQUISAR_TURMA)]
        public FileContentResult ApresentarRelatorio()
        {
            return new FileContentResult(TempData["RelatorioTurmaCiclo"] as byte[], "application/pdf");
        }        
    }
}