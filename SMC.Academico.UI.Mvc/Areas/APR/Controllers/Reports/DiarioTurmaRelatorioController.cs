using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.APR.Controllers
{
    public class DiarioTurmaRelatorioController : SMCReportingControllerBase
    {
        #region [ Services ]

        internal ITurmaService TurmaService
        {
            get { return this.Create<ITurmaService>(); }
        }

        internal IInstituicaoEnsinoService InstituicaoEnsinoService
        {
            get { return this.Create<IInstituicaoEnsinoService>(); }
        }

        #endregion [ Services ]

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("TurmaReport");

        #endregion APIS


        public override string ControllerName
        {
            get
            {
                return "DiarioTurma";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\APR\Reports\";
            }
        }

        [HttpGet]
        [SMCAuthorize(UC_APR_002_02_01.EXIBIR_RELATORIO_DIARIO_TURMA)]
        public FileContentResult DiarioTurmaRelatorio(SMCEncryptedLong seqTurma)
        {
            var dadosReport = ReportAPI.Execute<byte[]>($"DiarioTurmaRelatorio/{seqTurma.Value}", Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }
    }
}