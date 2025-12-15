using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.FIN.Views.DeclaracaoPagamento.App_LocalResources;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.FIN.Apis
{
    public class DeclaracaoPagamentoApiController : SMCApiControllerBase
    {
        #region [ Services ]

        internal IAlunoService IAlunoService => Create<IAlunoService>();

        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        internal IIntegracaoFinanceiroService IntegracaoFinanceiroService => Create<IIntegracaoFinanceiroService>();

        #endregion [ Services ]

        [HttpPost]
        //[SMCAuthorize(UC_FIN_004_04_02.EXIBIR_DECLARACAO_PAGAMENTO)]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(DeclaracaoPagamentoFiltroVO filtros)
        {
            var filtroData = GetDeclaracaoPagamentoFiltroData(filtros);

            var lista = IntegracaoFinanceiroService.BuscarDeclaracaoPagamentoAluno(filtroData).TransformList<DeclaracaoPagamentoVO>();

            if (!lista.SMCAny())
                return null;

            return GerarDeclaracaoPagamento(lista);
        }

        /// <summary>
        /// NV06 Cálculo do Valor Total:
        /// Soma dos valores pagos(detalhe) - Valor OP - Valor Cheque.
        /// </summary>
        private decimal CalcularValorTotal(List<DeclaracaoPagamentoVO> lista)
        {
            var valorTotalPagamento = lista != null && lista.SMCAny() ? lista.Sum(x => x.ValorPagamento) : 0;

            //  ValorPagamento - ValorOP - ValorChequeDevolvido;
            return valorTotalPagamento - lista.FirstOrDefault().ValorOP - lista.FirstOrDefault().ValorChequeDevolvido;
        }

        private byte[] GerarDeclaracaoPagamento(List<DeclaracaoPagamentoVO> lista)
        {
            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                var dataSourceBolsistas = lista.ToList();
                e.DataSources.Add(new ReportDataSource("DSDeclaracaoPagamento", dataSourceBolsistas));
            };

            List<ReportParameter> parameters = MontarParametros(lista);

            return SMCGenerateReport("Areas/FIN/Reports/DeclaracaoPagamento/DeclaracaoPagamento.rdlc", lista, "DSDeclaracaoPagamento", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Portrait, parameters);
        }

        private DeclaracaoPagamentoFiltroData GetDeclaracaoPagamentoFiltroData(DeclaracaoPagamentoFiltroVO filtros)
        {
            var filtroData = filtros.Transform<DeclaracaoPagamentoFiltroData>();
            filtroData.CodigoAluno = int.Parse(IAlunoService.BuscarCodigoMigracaoAluno(filtros.SeqAlunoLogado).ToString());
            filtroData.SeqOrigem = 1;

            return filtroData;
        }

        private string MontarMensagemDeclaracao(DeclaracaoPagamentoVO decAluno)
        {
            if (decAluno == null) { return string.Empty; }

            string msg = $"Declaramos para os devidos fins, que o(a) aluno(a) {decAluno.NomeAluno?.Trim()}, código {decAluno.CodigoAlunoMatriculado}, " +
                   $"inscrito(a) no CPF nº {decAluno.CpfAluno}, residente à {decAluno.Endereco?.Trim()} - {decAluno.Bairro?.Trim()}, CEP: {decAluno.Cep}, " +
                   $"{decAluno.Cidade?.Trim()} - {decAluno.Estado?.Trim()}, do curso {decAluno.DescricaoServicoMatriculado?.Trim()}, do(a) {decAluno.DescricaoEmpresa?.Trim()}, " +
                   $"efetuou o pagamento de mensalidade(s) no período de {decAluno.DataPagamentoMinimo.ToShortDateString()} a {decAluno.DataPagamentoMaxima.ToShortDateString()}, conforme " +
                   $"discriminado a seguir:";

            string tag = string.Format("<p align='justify'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}</p>", msg);

            return tag;
        }

        private List<ReportParameter> MontarParametros(List<DeclaracaoPagamentoVO> lista)
        {
            var instituicaoLogada = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado();
            var valorTotal = CalcularValorTotal(lista);
            var valorTotalExtenso = valorTotal != 0 ? SMCExtenseHelper.ToExtense(Math.Abs(valorTotal)) : string.Empty;
            var msgDeclaracao = MontarMensagemDeclaracao(lista.First());

            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicaoLogada.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicaoLogada.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicaoLogada.Nome));
            parameters.Add(new ReportParameter("Titulo", UIResource.Titulo_Relatorio));
            parameters.Add(new ReportParameter("MsgDeclaracao", msgDeclaracao));
            parameters.Add(new ReportParameter("ValorTotal", valorTotal.ToString()));
            parameters.Add(new ReportParameter("ValorTotalExtenso", valorTotalExtenso));

            return parameters;
        }
    }
}