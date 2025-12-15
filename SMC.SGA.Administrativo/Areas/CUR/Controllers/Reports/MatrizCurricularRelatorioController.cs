using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class MatrizCurricularRelatorioController : SMCReportingControllerBase
    {
        public override string ControllerName => "MatrizCurricular";

        public override string ReportBasePath => @"\Areas\CUR\Reports\";

        #region [ Services ]

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("MatrizCurricularReport");

        #endregion APIS

        [SMCAuthorize(UC_CUR_001_07_01.PESQUISAR_MATRIZ_CURRICULAR)]
        public ActionResult Index(MatrizCurricularRelatorioFiltroViewModel filtros = null)
        {
            return View(filtros);
        }

        [HttpPost]
        [SMCAuthorize(UC_CUR_001_07_01.PESQUISAR_MATRIZ_CURRICULAR)]
        public FileContentResult RelatorioMatrizCurricular(MatrizCurricularRelatorioFiltroViewModel filtro)
        {
            var param = new
            {
                SeqMatrizCurricular = filtro.SeqMatrizCurricular.Seq,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq
            };

            var dadosReport = ReportAPI.Execute<byte[]>("RelatorioMatrizCurricular", param, Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }
    }
}