using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Aluno.Areas.FIN.Models;
using SMC.SGA.Aluno.Areas.FIN.Views.DeclaracaoPagamento.App_LocalResources;
using SMC.SGA.Aluno.Extensions;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.FIN.Controllers
{
    public class DeclaracaoPagamentoController : SMCReportingControllerBase
    {
        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("DeclaracaoPagamentoReport");

        #endregion APIS

        #region [ Propriedades ]

        public override string ControllerName
        {
            get
            {
                return "DeclaracaoPagamento";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\FIN\Reports\";
            }
        }

        #endregion [ Propriedades ]

        #region [ Actions ]

        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            var modelo = new DeclaracaoPagamentoFiltroViewModel()
            { };

            return View(modelo);
        }

        #endregion [ Actions ]

        #region [ Renderizar o Relatório ]

        [HttpPost]
        [SMCAuthorize(UC_FIN_004_04_02.EXIBIR_DECLARACAO_PAGAMENTO)]
        public ActionResult GerarRelatorio(DeclaracaoPagamentoFiltroViewModel filtros)
        {
            var alunoLogado = this.HttpContext.GetAlunoLogado();
            var param = new
            {
                filtros.DataFim,
                filtros.DataInicio,
                SeqAlunoLogado = alunoLogado.Seq
            };
            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            if (!dadosReport.SMCAny())
            {
                return MensagemPadraoSemRegistro();
            }
            return new FileContentResult(dadosReport, "application/pdf");
        }

        private ActionResult MensagemPadraoSemRegistro()
        {
            SetErrorMessage(UIResource.MSG_Nao_Existe_Registro, target: SMCMessagePlaceholders.Centro);
            return SMCRedirectToUrl(Request.UrlReferrer.ToString());
        }

        #endregion [ Renderizar o Relatório ]
    }
}