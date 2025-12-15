using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.ALN.Models;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.ALN.Apis
{
    public class PrevisaoConclusaoOrientacaoApiController : SMCApiControllerBase
    {
        #region [ Serviços ]

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        private IAlunoService AlunoService => Create<IAlunoService>();
        private ICursoService CursoService => this.Create<ICursoService>();

        #endregion [ Serviços ]

        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(RelatorioPrevisaoConclusaoOrientacaoFiltroVO filtro)
        {
            var parameters = new List<ReportParameter>();
            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("CicloLetivo", filtro.Descricao));

            var filtroData = filtro.Transform<RelatorioPrevisaoConclusaoOrientacaoFiltroData>();

            var dataService = AlunoService.BuscarPrevisaoConclusaoOrientacaoRelatorio(filtroData);
            var data = dataService.TransformList<RelatorioPrevisaoConclusaoOrientacaoVO>(dataService);

            var dataServiceAluno = data.SelectMany(s => s.Alunos).ToList();

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                e.DataSources.Add(new ReportDataSource("DSRelatorioPrevisaoConclusaoOrientacaoAluno", dataServiceAluno));
            };

            return SMCGenerateReport("Areas/ALN/Reports/Relatorio/RelatorioPrevisaoConclusaoOrientacao.rdlc", data, "RelatorioPrevisaoConclusaoOrientacao", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Landscape, parameters);
        }
    }
}