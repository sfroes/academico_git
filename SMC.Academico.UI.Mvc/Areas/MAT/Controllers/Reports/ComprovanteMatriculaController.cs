using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Controllers
{
    public class ComprovanteMatriculaController : SMCReportingControllerBase
    {
        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("ComprovanteMatriculaReport");

        #endregion APIS

        public override string ControllerName
        {
            get
            {
                return "Efetivacao";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\MAT\Reports\";
            }
        }

        [HttpGet]
        [SMCAuthorize(UC_MAT_003_19_02.RELATORIO_COMPROVANTE_MATRICULA)]
        [SMCAllowAnonymous]
        public FileContentResult ComprovanteMatriculaRelatorio(SMCEncryptedLong seqIngressante, SMCEncryptedLong seqSolicitacaoMatricula, SMCEncryptedLong seqProcessoEtapa)
        {
            var param = new
            {
                SeqIngressante = seqIngressante?.Value,
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula?.Value,
                SeqProcessoEtapa = seqProcessoEtapa?.Value
            };

            var dadosReport = ReportAPI.Execute<byte[]>("ComprovanteMatriculaRelatorio", param, Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }
    }
}