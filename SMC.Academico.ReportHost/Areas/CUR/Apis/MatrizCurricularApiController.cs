using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.CUR.Models;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.MAT.Apis
{
    public class MatrizCurricularApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private IMatrizCurricularService MatrizCurricularService => Create<IMatrizCurricularService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion [ Services ]

        [HttpPost]
        //[SMCAuthorize(UC_CUR_001_07_01.PESQUISAR_MATRIZ_CURRICULAR)]
        [SMCAllowAnonymous]
        public byte[] RelatorioMatrizCurricular(MatrizCurricularFiltroVO filtro)
        {
            var lista = MatrizCurricularService.BuscarMatrizCurricularRelatorio(filtro.SeqMatrizCurricular);

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                var subDataSource = lista.MatrizCurricularDados;

                e.DataSources.Add(new ReportDataSource("DSMatrizCurricularDados", subDataSource));

                var subDataSourceGrupos = lista.MatrizCurricularGrupos;

                e.DataSources.Add(new ReportDataSource("DSMatrizCurricularGrupos", subDataSourceGrupos));
            };

            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);
            var parameters = new List<ReportParameter>();
            //parameters.Add(new ReportParameter("MensagemRodape", UIResource.Layout_Rodape_Mensagem_SMC));
            parameters.Add(new ReportParameter("MensagemRodape", "GTI - Gerência de Tecnologia da Informação"));
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));

            return SMCGenerateReport("Areas/CUR/Reports/MatrizCurricular/MatrizCurricularRelatorio.rdlc", lista.MatrizCurricularCabecalho, "DSMatrizCurricular", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Landscape, parameters);
        }
    }
}