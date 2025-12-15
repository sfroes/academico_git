using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Aluno.Areas.FIN.Models;
using SMC.SGA.Aluno.Extensions;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.FIN.Controllers
{
    public class EmitirBoletoAbertoController : SMCControllerBase
    {
        #region [ Services ]

        private IAlunoService AlunoService
        {
            get { return this.Create<IAlunoService>(); }
        }

        #endregion [ Services ]

        #region Apis

        public SMCApiClient ReportApi => SMCApiClient.Create("AlunoReport");

        #endregion Apis

        [SMCAuthorize(UC_FIN_004_01_01.EMITIR_BOLETO_ABERTO)]
        public ActionResult Index()
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            var cursos = AlunoService.BuscarParcelasPagamentoEmAberto(alunoLogado.Seq);

            var model = new EmitirBoletoAbertoViewModel();
            model.Cursos = cursos.TransformList<EmitirBoletoAbertoCursoViewModel>();

            return View("Index", model);
        }

        [SMCAuthorize(UC_FIN_004_01_01.EMITIR_BOLETO_ABERTO)]
        public ActionResult EmissaoBoletoGerar(SMCEncryptedLong seqTitulo, SMCEncryptedLong seqServico)
        {
            try
            {
                var alunoLogado = this.GetAlunoLogado();
                if (alunoLogado == null || alunoLogado.Seq == 0)
                    throw new AlunoLogadoSemVinculoException();

                var urlApi = $"{ConfigurationManager.AppSettings[WEB_API_REST.BASE_URL_KEY]}{WEB_API_REST.EMITIR_BOLETO_ALUNO}";
                var cancelationTimer = int.Parse(ConfigurationManager.AppSettings[WEB_API_REST.CANCELLATION_TIME_KEY]);
                var token = SMCDESCrypto.Encrypt(ConfigurationManager.AppSettings[WEB_API_REST.TOKEN_BOLETO_KEY]);
                var filtro = new { SeqTitulo = seqTitulo.Value, SeqServico = seqServico.Value, Token = token };
                var boleto = SMCRest.PostJson(urlApi, filtro, cancellationTimer: cancelationTimer);
                return File(Convert.FromBase64String(boleto), "application/pdf");
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "EmitirBoletoAberto");
            }
        }
    }
}