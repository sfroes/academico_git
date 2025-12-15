using Microsoft.Reporting.WebForms;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.ReportHost.Areas.TUR.Models;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.APR.Apis
{
    public class BancasAgendadasApiController : SMCApiControllerBase
    {
        #region [ Serviços ]

        private IAplicacaoAvaliacaoService AplicacaoAvaliacaoService => Create<IAplicacaoAvaliacaoService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion [ Serviços ]

        [HttpPost]
        [SMCAllowAnonymous]
        //[SMCAuthorize(UC_APR_001_11_02.EXIBIR_RELATORIO_BANCAS_AGENDADAS_PERIODO)]
        public byte[] GerarRelatorio(BancasAgendadasFiltroVO filtro)
        {
            var dtInicio = filtro.DataInicio.SMCDataAbreviada();
            var dtFim = filtro.DataFim.SMCDataAbreviada();
            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);

            // Task 38162
            // Caso seja marcado para listar bancas sem anexo, o parâmetro listar com bancas apuradas tem que ser sempre true.
            if (filtro.SituacaoBanca == Common.Areas.APR.Enums.SituacaoBanca.SemAtaAnexada)
                filtro.ExibirBancasComNota = true;

            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("ExibirComNota", (filtro.ExibirBancasComNota ?? true).ToString()));
            parameters.Add(new ReportParameter("DataInicio", dtInicio));
            parameters.Add(new ReportParameter("DataFim", dtFim));

            /*Consistência para permitir pesquisar quando se passa como filtro a data fim igual a data início
            ou seja, permitir listar bancas agendadas em um dia específico*/
            if (filtro.DataFim.HasValue && filtro.DataInicio == filtro.DataFim.Value)
            {
                filtro.DataInicio = filtro.DataInicio.AddHours(00).AddMinutes(00).AddSeconds(00);
                filtro.DataFim = filtro.DataFim.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            var filtroData = filtro.Transform<BancasAgendadasFiltroData>();
            var dataService = AplicacaoAvaliacaoService.BuscarBancasAgendadasPorPeriodo(filtroData);
            var data = dataService.TransformList<BancaAgendadaVO>(dataService);

            return SMCGenerateReport("Areas/APR/Reports/BancasAgendadas/RelatorioBancasAgendadas.rdlc", data, "DSRelatorioBancasAgendadas", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Landscape, parameters);
        }
    }
}