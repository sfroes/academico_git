using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.PES.Apis
{
    public class IdentidadeAcademicaApiController : SMCApiControllerBase
    {
        #region Serviços

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion Serviços

        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(IdentidadeAcademicaFiltroVO model)
        {
            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(model.SeqInstituicaoEnsino);
            var parameters = new List<ReportParameter>()
            {
                new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)),
                new ReportParameter("NomeInstituicao", instituicao.Nome)
            };
            var lista = PessoaAtuacaoService.BuscarPessoaAtuacaoIdentidadeEstudantil(model.SeqsAlunos, model.SeqsColaboradores);
            return SMCGenerateReport("Areas/PES/Reports/IdentidadeAcademica/IdentidadeAcademicaRelatorio.rdlc", lista, "DSIdentidadeAcademica", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Custom, parameters);
        }
    }
}