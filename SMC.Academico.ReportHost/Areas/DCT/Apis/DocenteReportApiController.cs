using Microsoft.Reporting.WebForms;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ReportHost.App_GlobalResources;
using SMC.Academico.ReportHost.Areas.DCT.Models;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.DCT.Apis
{
    public class DocenteReportApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private ILogAtualizacaoColaboradorService LogAtualizacaoColaboradorService => Create<ILogAtualizacaoColaboradorService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(DocenteRelatorioFiltroVO model)
        {
            var parameters = new List<ReportParameter>();
            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(model.SeqInstituicaoEnsino);
            parameters.Add(new ReportParameter("MensagemRodape", UIResource.Layout_Rodape_Mensagem_SMC));
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));

            switch (model.TipoRelatorio)
            {
                case TipoRelatorio.LogAtualizacaoColaborador:
                    return LogAtualizacaoColaborador(model, parameters);
            }

            throw new Exception("Relatório não encontrado");
        }

        private byte[] LogAtualizacaoColaborador(DocenteRelatorioFiltroVO model, List<ReportParameter> parameters)
        {
            parameters.Add(new ReportParameter("DataInicio", model.DataInicioReferencia.ToString("dd/MM/yyyy")));
            parameters.Add(new ReportParameter("DataFim", model.DataFimReferencia.ToString("dd/MM/yyyy")));

            var lista = LogAtualizacaoColaboradorService.BuscarLogsAtualizacoesColaboradoresRelatorio(model.Transform<RelatorioLogAtualizacaoColaboradorFiltroData>());
            
            return SMCGenerateReport("Areas/DCT/Reports/LogAtualizacaoColaborador/LogAtualizacaoColaborador.rdlc", lista, "DSLogAtualizacaoColaborador", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Landscape, parameters);
        }
    }
}