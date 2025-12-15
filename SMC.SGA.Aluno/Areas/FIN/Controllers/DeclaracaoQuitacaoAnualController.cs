using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Aluno.Areas.FIN.Models;
using SMC.SGA.Aluno.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.FIN.Controllers
{
    public class DeclaracaoQuitacaoAnualController : SMCControllerBase
    {
        private IIntegracaoFinanceiroService IntegracaoFinanceiroService => this.Create<IIntegracaoFinanceiroService>();
        private IAlunoService AlunoService => Create<IAlunoService>();

        [SMCAuthorize(UC_FIN_004_05_01.PESQUISAR_DECLARACAO_QUITACAO_ANUAL)]
        public ActionResult Index()
        {
            var model = new DeclaracaoQuitacaoAnualViewModel
            {
                AnoReferencia = IntegracaoFinanceiroService.BuscarAnoReferenciaQuitacaoAnualDebitos(),
                ListaCpfPagante = new List<SMCDatasourceItem<string>>()
            };

            ViewBag.Title = $"Declaração de Quitação Anual de Débitos - Referência: {model.AnoReferencia}";

            var alunoLogado = this.GetAlunoLogado();
            var aluno = AlunoService.BuscarAluno(alunoLogado.Seq);
            var codigoAlunoMigracao = aluno.CodigoAlunoMigracao.HasValue ? (int)aluno.CodigoAlunoMigracao : 0;
            var seqOrigem = 1; //retorna origem = 1 sendo aluno

            model.ListaCpfPagante = IntegracaoFinanceiroService.BuscarCpfPaganteQuitacaoAnualDebitos(codigoAlunoMigracao, seqOrigem);


            return View(model);
        }

        [SMCAuthorize(UC_FIN_004_01_01.EMITIR_BOLETO_ABERTO)]
        public async Task<ActionResult> EmitirDeclaracao(DeclaracaoQuitacaoAnualViewModel model)
        {
            byte[] pdfBytes = null;
            string type = "application/pdf";
            string nomeRelatorio = ConfigurationManager.AppSettings["DeclaracaoQuitacaoAnualNomeRelatorio"];
            var urlCrystalLegado = ConfigurationManager.AppSettings["UrlCrystalLegado"];

            try
            {
                if (model == null || (string.IsNullOrWhiteSpace(model.Cpf) && model.AnoReferencia == 0))
                    throw new Exception("Dados inválidos.");

                var retorno = TransformarBody(model.AnoReferencia, model.Cpf);

                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(retorno);

                    var response = await client.PostAsync(urlCrystalLegado, content);

                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Erro ao gerar o relatório: {response.StatusCode}");

                    pdfBytes = await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, "", SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", "DeclaracaoQuitacaoAnual", new { area = "FIN" });
            }

            Response.AddHeader("Content-Disposition", $"inline; filename=\"{nomeRelatorio}\"");

            return File(pdfBytes, type);
        }

        public async Task<ActionResult> BaixarDeclaracao(DeclaracaoQuitacaoAnualViewModel model)
        {
            byte[] pdfBytes = null;
            string type = "application/pdf";
            string nomeRelatorio = ConfigurationManager.AppSettings["DeclaracaoQuitacaoAnualNomeRelatorio"];
            var urlCrystalLegado = ConfigurationManager.AppSettings["UrlCrystalLegado"];

            try
            {
                if (model == null || (string.IsNullOrWhiteSpace(model.Cpf) && model.AnoReferencia == 0))
                    throw new Exception("Dados inválidos.");

                var retorno = TransformarBody(model.AnoReferencia, model.Cpf);

                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(retorno);

                    var response = await client.PostAsync(urlCrystalLegado, content);

                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Erro ao gerar o relatório: {response.StatusCode}");

                    pdfBytes = await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, "", SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", "DeclaracaoQuitacaoAnual", new { area = "FIN" });
            }

            return File(pdfBytes, type, nomeRelatorio);
        }

        private Dictionary<string, string> TransformarBody(int anoReferencia, string cpf)
        {
            var tipRelatorio = ConfigurationManager.AppSettings["DeclaracaoQuitacaoAnualTipRelatorio"];
            var tipDeclaracao = ConfigurationManager.AppSettings["DeclaracaoQuitacaoAnualTipDeclaracao"];
            var indCpf = ConfigurationManager.AppSettings["DeclaracaoQuitacaoAnualIndCpf"];
            var nomeRpt = ConfigurationManager.AppSettings["DeclaracaoQuitacaoAnualNomeRPT"];
            var nomeUsuaio = ConfigurationManager.AppSettings["DeclaracaoQuitacaoAnualNomeUsuario"];
            var nomeBanco = ConfigurationManager.AppSettings["DeclaracaoQuitacaoAnualNomeBanco"];

            var retorno = new Dictionary<string, string>
            {
                { "pNomeRPT", nomeRpt },
                { "pUsuario", nomeUsuaio },
                { "pBanco", nomeBanco },
                { "pQtdParam", "11" },
                { "pTipo1", "Integer" },
                { "pParam1", tipRelatorio },
                { "pTipo2", "Integer" },
                { "pParam2", tipDeclaracao },
                { "pTipo3", "Integer" },
                { "pParam3", anoReferencia.ToString() },
                { "pTipo4", "Integer" },
                { "pParam4", anoReferencia.ToString() },
                { "pTipo5", "Integer" },
                { "pParam5", indCpf },
                { "pTipo6", "String" },
                { "pParam6", cpf },
                { "pTipo7", "null" },
                { "pTipo8", "null" },
                { "pTipo9", "null" },
                { "pTipo10", "null" },
                { "pTipo11", "null" }
            };

            return retorno;
        }
    }
}