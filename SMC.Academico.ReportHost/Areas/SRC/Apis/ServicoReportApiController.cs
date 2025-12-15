using Microsoft.Reporting.WebForms;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ReportHost.Areas.SRC.Models;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SMC.Academico.ReportHost.Areas.SRC.Views.ServicoReport.App_LocalResources;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;

namespace SMC.Academico.ReportHost.Areas.SRC.Apis
{
    public class ServicoReportApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private IServicoService ServicoService => Create<IServicoService>();

        private ISolicitacaoServicoService SolicitacaoServicoService => Create<ISolicitacaoServicoService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(ServicoRelatorioFiltroVO filtro)
        {
            switch (filtro.TipoRelatorioServico)
            {
                case TipoRelatorioServico.PosicaoConsolidadaServicoCicloLetivo:
                    return PosicaoConsolidadaServicoCicloLetivo(filtro);

                case TipoRelatorioServico.SolicitacoesBloqueio:
                    return RelatorioSolicitacoesBloqueio(filtro);
            }

            throw new Exception("Relatório não encontrado");
        }

        private byte[] PosicaoConsolidadaServicoCicloLetivo(ServicoRelatorioFiltroVO filtro)
        {
            var filtroData = filtro.Transform<RelatorioServicoCicloLetivoFiltroData>();

            var lista = ServicoService.BuscarDadosRelatorioServicoCicloLetivo(filtroData).TransformList<ConsolidadoServicoCicloLetivoVO>();

            var reportAction = MontarDataSourceTotalizadorPosicaoConsolidada(lista);

            var instituicaoLogada = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);

            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicaoLogada.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicaoLogada.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicaoLogada.Nome));
            parameters.Add(new ReportParameter("Titulo", MontarTitulo(filtro)));
            parameters.Add(new ReportParameter("MSGTextoInformativo", UIResource.MSGTextoInformativo));

            return SMCGenerateReport("Areas/SRC/Reports/Relatorio/RelatorioConsolidadoServicoCicloLetivoAgrupado.rdlc", lista, "DSServicoCicloLetivo", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Landscape, parameters);
        }

        private byte[] RelatorioSolicitacoesBloqueio(ServicoRelatorioFiltroVO filtro)
        {
            var filtroData = filtro.Transform<RelatorioSolicitacoesBloqueioFiltroData>();

            var lista = SolicitacaoServicoService.BuscarDadosRelatorioSolicitacoesBloqueio(filtroData);

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                var subDataSourceBloqueios = lista.SelectMany(a => a.Bloqueios).ToList();
                e.DataSources.Add(new ReportDataSource("DSBloqueios", subDataSourceBloqueios));
            };

            var instituicaoLogada = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);

            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicaoLogada.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicaoLogada.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicaoLogada.Nome));

            return SMCGenerateReport("Areas/SRC/Reports/Relatorio/RelatorioSolicitacoesComBloqueio.rdlc", lista, "DSSolicitacoesComBloqueio", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Landscape, parameters);
        }

        #region [ Métodos Privados ]

        private Action<object, SubreportProcessingEventArgs> MontarDataSourceTotalizadorPosicaoConsolidada(List<ConsolidadoServicoCicloLetivoVO> lista)
        {
            var listTotais = TotalizarTodasEtapasPosicaoConsolidada(lista);

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                e.DataSources.Add(new ReportDataSource("DSServicoCicloLetivoTotalizador", listTotais));
            };

            return reportAction;
        }

        /// <summary>
        /// Método que faz a totalização geral de cada Etapa do relatório,
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        private List<ConsolidadoServicoCicloLetivoTotalizadorVO> TotalizarTodasEtapasPosicaoConsolidada(List<ConsolidadoServicoCicloLetivoVO> lista)
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

        /// <summary>
        /// NV01 O título do relatório deve ser:
        /// "Posição Consolidada: " + <descrição_do_serviço> + "-" + "<numero_do_ciclo_letivo>" + "º" + "/" + <ano_do_ciclo_letivo>".
        /// </summary>
        /// <returns>Título formatado</returns>
        private string MontarTitulo(ServicoRelatorioFiltroVO filtro)
        {
            var servico = ServicoService.BuscarServico(filtro.SeqServico.Value);

            var cicloLetivo = CicloLetivoService.BuscarCicloLetivo(filtro.SeqCicloLetivo.Value);

            return $"Posição Consolidada: {servico.Descricao} - {cicloLetivo.Numero}º/{cicloLetivo.Ano}";
        }

        #endregion [ Métodos Privados ]
    }
}
