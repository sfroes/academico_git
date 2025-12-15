using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Reporting;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Controllers
{
    public class ComprovanteProcessosController : SMCReportingControllerBase
    {
        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("ComprovanteProcessoReport");

        #endregion APIS

        public override string ControllerName
        {
            get
            {
                return "ComprovanteProcessos";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\SRC\Reports\";
            }
        }

        [HttpGet]
        //[SMCAuthorize(UC_MAT_003_19_02.RELATORIO_COMPROVANTE_MATRICULA)]
        [SMCAllowAnonymous]
        public FileContentResult ComprovanteProcessosRelatorio(SMCEncryptedLong seqSolicitacaoMatricula, SMCEncryptedLong seqProcessoEtapa, SMCEncryptedLong seqPessoaAtuacao)
        {
            var param = new
            {
                SeqPessoaAtuacao = seqPessoaAtuacao?.Value,
                SeqSolicitacaoMatricula = seqSolicitacaoMatricula?.Value,
                SeqProcessoEtapa = seqProcessoEtapa?.Value
            };

            var dadosReport = ReportAPI.Execute<byte[]>("ComprovanteProcessosRelatorio", param, Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }
    }
}