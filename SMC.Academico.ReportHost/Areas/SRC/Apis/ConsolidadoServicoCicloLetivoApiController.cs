using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Academico.ReportHost.Areas.SRC.Views.RelatorioConsolidadoServicoCicloLetivo.App_LocalResources;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.SRC.Apis
{
    public class ConsolidadoServicoCicloLetivoApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        private IServicoService ServicoService => Create<IServicoService>();

        #endregion [ Services ]

        #region [ Renderizar o Relatório ]

        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(ConsolidadoServicoCicloLetivoFiltroVO filtro)
        {
            var filtroData = filtro.Transform<RelatorioServicoCicloLetivoFiltroData>();

            var lista = ServicoService.BuscarDadosRelatorioServicoCicloLetivo(filtroData).TransformList<ConsolidadoServicoCicloLetivoVO>();

            return MontarRelatorio(lista, filtro);
        }

        public byte[] MontarRelatorio(List<ConsolidadoServicoCicloLetivoVO> lista, ConsolidadoServicoCicloLetivoFiltroVO filtro)
        {
            var reportAction = MontarDataSourceTotalizador(lista);

            var parameters = MontarParametros(filtro);

            return SMCGenerateReport("Areas/SRC/Reports/Relatorio/RelatorioConsolidadoServicoCicloLetivoAgrupado.rdlc", lista, "DSServicoCicloLetivo", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Landscape, parameters);
        }

        private Action<object, SubreportProcessingEventArgs> MontarDataSourceTotalizador(List<ConsolidadoServicoCicloLetivoVO> lista)
        {
            var listTotais = TotalizarTodasEtapas(lista);

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                e.DataSources.Add(new ReportDataSource("DSServicoCicloLetivoTotalizador", listTotais));
            };

            return reportAction;
        }

        private List<ReportParameter> MontarParametros(ConsolidadoServicoCicloLetivoFiltroVO filtro)
        {
            var instituicaoLogada = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado();
            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicaoLogada.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicaoLogada.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicaoLogada.Nome));
            parameters.Add(new ReportParameter("Titulo", MontarTitulo(filtro)));
            parameters.Add(new ReportParameter("MSGTextoInformativo", UIResource.MSGTextoInformativo));

            return parameters;
        }

        /// <summary>
        /// Método que faz a totalização geral de cada Etapa do relatório,
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        private List<ConsolidadoServicoCicloLetivoTotalizadorVO> TotalizarTodasEtapas(List<ConsolidadoServicoCicloLetivoVO> lista)
        {
            var listTotais = new List<ConsolidadoServicoCicloLetivoTotalizadorVO>();

            //Busco os tipos de etapas agrupadas (Sem duplicidade)
            var listEtapa = lista.SMCDistinct(e => e.DescricaoEtapa);

            foreach (var etapa in listEtapa)
            {
                var totallizador = new ConsolidadoServicoCicloLetivoTotalizadorVO();

                totallizador = etapa.Transform<ConsolidadoServicoCicloLetivoTotalizadorVO>();
                var etapas = lista.Where(e => etapa.DescricaoEtapa.ToLower().Equals(e.DescricaoEtapa.ToLower()));

                totallizador.QuantidadeNaoIniciada = etapas.Sum(s => s.QuantidadeNaoIniciada);
                totallizador.QuantidadeEmAndamento = etapas.Sum(s => s.QuantidadeEmAndamento);
                totallizador.QuantidadeFimComSucesso = etapas.Sum(s => s.QuantidadeFimComSucesso);
                totallizador.QuantidadeFimSemSucesso = etapas.Sum(s => s.QuantidadeFimSemSucesso);
                totallizador.QuantidadeCancelada = etapas.Sum(s => s.QuantidadeCancelada);

                listTotais.Add(totallizador);
            }
            return listTotais;
        }

        #endregion [ Renderizar o Relatório ]

        #region [ Métodos Privados ]

        /// <summary>
        /// NV01 O título do relatório deve ser:
        /// "Posição Consolidada: " + <descrição_do_serviço> + "-" + "<numero_do_ciclo_letivo>" + "º" + "/" + <ano_do_ciclo_letivo>".
        /// </summary>
        /// <returns>Título formatado</returns>
        private string MontarTitulo(ConsolidadoServicoCicloLetivoFiltroVO filtro)
        {
            var servico = ServicoService.BuscarServico(filtro.SeqServico);

            var cicloLetivo = CicloLetivoService.BuscarCicloLetivo(filtro.SeqCicloLetivo);

            return $"Posição Consolidada: {servico.Descricao} - {cicloLetivo.Numero}º/{cicloLetivo.Ano}";
        }

        #endregion [ Métodos Privados ]
    }
}