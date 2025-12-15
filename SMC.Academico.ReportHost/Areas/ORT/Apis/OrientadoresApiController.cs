using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.ORT.Models;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.ORT.Apis
{
    public class OrientadoresApiController : SMCApiControllerBase
    {
        #region [ Serviços ]

        private IOrientacaoService OrientacaoService => this.Create<IOrientacaoService>();

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion [ Serviços ]

        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(RelatorioOrientadoresFiltroVO filtro)
        {
            var parameters = new List<ReportParameter>();
            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));

            var filtroData = filtro.Transform<OrientacaoFiltroData>();
            var data = OrientacaoService.BuscarOrientacoesRelatorio(filtroData);

            return SMCGenerateReport("Areas/ORT/Reports/Orientadores/OrientadoresRelatorio.rdlc", data, "DSOrientadores", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Landscape, parameters);
        }
    }
}