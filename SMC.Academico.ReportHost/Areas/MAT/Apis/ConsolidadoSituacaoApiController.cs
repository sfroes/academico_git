using Microsoft.Reporting.WebForms;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Academico.ReportHost.Areas.MAT.Views.RelatorioConsolidadoSituacao.App_LocalResources;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.MAT.Apis
{
    public class ConsolidadoSituacaoApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private IAlunoHistoricoCicloLetivoService AlunoHistoricoCicloLetivoService => Create<IAlunoHistoricoCicloLetivoService>();

        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();
        
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(ConsolidadoSituacaoFiltroVO filtro)
        {
            var lista = AlunoHistoricoCicloLetivoService.BuscarDadosRelatorioConsolidadoSituacao(filtro.Transform<RelatorioConsolidadoSituacaoFiltroData>()).TransformList<ConsolidadoSituacaoVO>();

            return MontarRelatorio(lista, filtro);
        }

        public byte[] MontarRelatorio(List<ConsolidadoSituacaoVO> lista, ConsolidadoSituacaoFiltroVO filtro)
        {
            var parameters = MontarParametros(filtro.SeqCicloLetivo, filtro.SeqInstituicaoEnsino);

            return SMCGenerateReport($"Areas/MAT/Reports/RelatorioConsolidadoSituacao/{BuscarEstiloColunaRelatorio(filtro)}", lista, "DSConsolidadoSituacao", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Landscape, parameters);
        }

        /// <summary>
        /// Método que Retorna o estilo do relatório
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Estilo de relatório com suas colunas</returns>
        private string BuscarEstiloColunaRelatorio(ConsolidadoSituacaoFiltroVO filtro)
        {
            string estiloColunaRelatorio = "RelatorioConsolidadoSituacao.rdlc";

            if (filtro.TipoAtuacoes.Contains(TipoAtuacao.Aluno) && filtro.TipoAtuacoes.Contains(TipoAtuacao.Ingressante))
            {
                //Relatório completo (Aluno e Ingressante)
                estiloColunaRelatorio = "RelatorioConsolidadoSituacao.rdlc";
            }
            else if (filtro.TipoAtuacoes.Contains(TipoAtuacao.Aluno) && !filtro.TipoAtuacoes.Contains(TipoAtuacao.Ingressante))
            {
                //Aluno
                estiloColunaRelatorio = "RelatorioConsolidadoSituacaoAluno.rdlc";
            }
            else if (!filtro.TipoAtuacoes.Contains(TipoAtuacao.Aluno) && filtro.TipoAtuacoes.Contains(TipoAtuacao.Ingressante))
            {
                //Ingressante
                estiloColunaRelatorio = "RelatorioConsolidadoSituacaoIngressante.rdlc";
            }

            return estiloColunaRelatorio;
        }

        private List<ReportParameter> MontarParametros(long seqCicloLetivo, long seqInstituicaoEnsino)
        {
            /*NV01 De acordo com o ciclo letivo selecionado na pesquisa, o título do relatório deverá ser:
             * "Posição Consolidada Situação por Tipo de Atuação" + "-" + "<Ciclo letivo>". */
            var cicloLetivo = CicloLetivoService.BuscarCicloLetivo(seqCicloLetivo);

            string descCicloLetivo = $"{cicloLetivo?.Numero}º/{cicloLetivo?.Ano}";

            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(seqInstituicaoEnsino);
            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("Titulo", string.Format(UIResource.TituloRelatorio, $"- {descCicloLetivo}")));
            parameters.Add(new ReportParameter("MSGTextoInformativo", UIResource.MSGTextoInformativo));

            return parameters;
        }
    }
}