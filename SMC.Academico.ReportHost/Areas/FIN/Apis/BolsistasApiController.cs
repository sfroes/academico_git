using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.FIN.Views.Bolsistas.App_LocalResources;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.SpreadSheet;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.FIN.Apis
{
    public class BolsistasApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private IBeneficioService BeneficioService => Create<IBeneficioService>();

        private IPessoaAtuacaoBeneficio PessoaAtuacaoBeneficio => Create<IPessoaAtuacaoBeneficio>();

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion [ Services ]

        [System.Web.Http.HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(BolsistasFiltroVO filtro)
        {
            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);
            var filtroData = filtro.Transform<RelatorioBolsistasFiltroData>();
            filtroData.SeqInstituicaoLogada = filtro.SeqInstituicaoEnsino;

            var lista = PessoaAtuacaoBeneficio.BuscarDadosRelatorioBolsistas(filtroData).TransformList<BolsistasVO>();

            return RelatorioBolsistas(lista, filtro, instituicao);
        }

        private byte[] RelatorioBolsistas(List<BolsistasVO> lista, BolsistasFiltroVO filtro, InstituicaoEnsinoData instituicao)
        {
            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                var dataSourceBolsistas = lista.ToList();
                e.DataSources.Add(new ReportDataSource("DSBolsistas", dataSourceBolsistas));
            };

            string titulo = (filtro.DataInicioReferencia.HasValue && filtro.DataFimReferencia.HasValue ? $"{UIResource.TituloRelatorio} de {filtro.DataInicioReferencia.Value.ToShortDateString()} a {filtro.DataFimReferencia.Value.ToShortDateString()} " : UIResource.TituloRelatorio);

            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("Titulo", titulo));
            parameters.Add(new ReportParameter("ExibirParcelasEmAberto", filtro.ExibirParcelasEmAberto ? "true" : "false"));
            parameters.Add(new ReportParameter("ExibirReferenciaContrato", filtro.ExibirReferenciaContrato ? "true" : "false"));

            return SMCGenerateReport("Areas/FIN/Reports/RelatorioBolsistas/RelatorioBolsistas.rdlc", lista, "DSBolsistas", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Landscape, parameters);
        }

        [System.Web.Http.HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorioExcel(BolsistasFiltroVO filtro)
        {
            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);
            var filtroData = filtro.Transform<RelatorioBolsistasFiltroData>();
            filtroData.SeqInstituicaoLogada = filtro.SeqInstituicaoEnsino;

            var lista = PessoaAtuacaoBeneficio.BuscarDadosRelatorioBolsistas(filtroData).TransformList<BolsistasVO>();

            return RelatorioBolsistasExcel(lista, filtro, instituicao);
        }

        public byte[] RelatorioBolsistasExcel(List<BolsistasVO> lista, BolsistasFiltroVO filtro, InstituicaoEnsinoData instituicao)
        {
            Workbook workbook = new Workbook();
            Worksheet worksheet = null;
            worksheet = new Worksheet("RelatorioBolsistas");

            ushort columnWidth = 9000;
            ushort indexColuna = 0;
            ushort indexLinha = 1;

            worksheet.Cells[0, 0] = new Cell("Nome");
            worksheet.Cells.ColumnWidth[0, 0] = columnWidth;

            worksheet.Cells[0, 1] = new Cell("Programa - Nivel");
            worksheet.Cells.ColumnWidth[0, 1] = columnWidth;

            worksheet.Cells[0, 2] = new Cell("Atuação - ID (Cód. Migração)");
            worksheet.Cells.ColumnWidth[0, 2] = columnWidth;

            worksheet.Cells[0, 3] = new Cell("Benefício");
            worksheet.Cells.ColumnWidth[0, 3] = columnWidth;

            worksheet.Cells[0, 4] = new Cell("Início benefício");
            worksheet.Cells.ColumnWidth[0, 4] = columnWidth;

            worksheet.Cells[0, 5] = new Cell("Fim benefício");
            worksheet.Cells.ColumnWidth[0, 5] = columnWidth;

            worksheet.Cells[0, 6] = new Cell("Situação benefício");
            worksheet.Cells.ColumnWidth[0, 6] = columnWidth;

            if (filtro.ExibirReferenciaContrato)
            {
                worksheet.Cells[0, 7] = new Cell("Referência financeira");
                worksheet.Cells.ColumnWidth[0, 7] = columnWidth;
            }

            if (filtro.ExibirParcelasEmAberto && !filtro.ExibirReferenciaContrato)
            {
                worksheet.Cells[0, 7] = new Cell("Parcelas em aberto");
                worksheet.Cells.ColumnWidth[0, 7] = columnWidth;
            }

            if(filtro.ExibirParcelasEmAberto && filtro.ExibirReferenciaContrato)
            {
                worksheet.Cells[0, 8] = new Cell("Parcelas em aberto");
                worksheet.Cells.ColumnWidth[0, 8] = columnWidth;
            }

            foreach (var item in lista)
            {
                indexColuna = 0; 

                worksheet.Cells[indexLinha, indexColuna] = new Cell(item.Nome);
                worksheet.Cells.ColumnWidth[indexLinha, indexColuna] = columnWidth;
                indexColuna++;

                worksheet.Cells[indexLinha, indexColuna] = new Cell(item.SiglaEntidadeResponsavel + " - " + item.DescricaoNivelEnsino);
                worksheet.Cells.ColumnWidth[indexLinha, indexColuna] = columnWidth;
                indexColuna++;

                worksheet.Cells[indexLinha, indexColuna] = new Cell(item.TipoAtuacao + " - " + item.SeqPessoaAtuacao + "(" + item.CodigoAlunoMigracao + ")");
                worksheet.Cells.ColumnWidth[indexLinha, indexColuna] = columnWidth;
                indexColuna++;

                worksheet.Cells[indexLinha, indexColuna] = new Cell(item.DescricaoBeneficio);
                worksheet.Cells.ColumnWidth[indexLinha, indexColuna] = columnWidth;
                indexColuna++;

                worksheet.Cells[indexLinha, indexColuna] = new Cell(item.DataInicioVigencia.ToString());
                worksheet.Cells.ColumnWidth[indexLinha, indexColuna] = columnWidth;
                indexColuna++;

                worksheet.Cells[indexLinha, indexColuna] = new Cell(item.DataFimVigencia.ToString());
                worksheet.Cells.ColumnWidth[indexLinha, indexColuna] = columnWidth;
                indexColuna++;

                worksheet.Cells[indexLinha, indexColuna] = new Cell(item.SituacaoChancelaBeneficio);
                worksheet.Cells.ColumnWidth[indexLinha, indexColuna] = columnWidth;
                indexColuna++;

                if (filtro.ExibirReferenciaContrato)
                {
                    worksheet.Cells[indexLinha, indexColuna] = new Cell(item.ReferenciaFinanceira);
                    worksheet.Cells.ColumnWidth[indexLinha, indexColuna] = columnWidth;
                    indexColuna++;
                }
                if (filtro.ExibirParcelasEmAberto)
                {
                    worksheet.Cells[indexLinha, indexColuna] = new Cell(item.ParcelasAbertas);
                    worksheet.Cells.ColumnWidth[indexLinha, indexColuna] = columnWidth;
                    indexColuna++;
                }

                indexLinha++;
            }
            workbook.Worksheets.Add(worksheet);

            return ConverteParaBytes(workbook);

        }

        public static byte[] ConverteParaBytes(Workbook workbook)
        {
            MemoryStream memoryStream = new MemoryStream();
            workbook.Save(memoryStream);

            return memoryStream.ToArray();
        }
    }
}