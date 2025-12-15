using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Academico.ReportHost.Areas.MAT.Views.RelatorioAlunosPorSituacao.App_LocalResources;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
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
    public class AlunosPorSituacaoApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private IAlunoService AlunoService => Create<IAlunoService>();
        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();
        private IEntidadeService EntidadeService => Create<IEntidadeService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion

        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(AlunosPorSituacaoFiltroVO filtro)
        {
            var lista = AlunoService.BuscarDadosRelatorioAlunosPorSituacao(filtro.Transform<RelatorioAlunosPorSituacaoFiltroData>()).TransformList<AlunosPorSituacaoVO>();
            MontarDescricaoEntidadeResponsavel(filtro, lista);
            return RelatorioAlunosPorSituacao(lista, filtro);
        }

        public byte[] RelatorioAlunosPorSituacao(List<AlunosPorSituacaoVO> lista, AlunosPorSituacaoFiltroVO filtro)
        {
            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                var dataSourceAlunosPorSituacao = lista.ToList();
                e.DataSources.Add(new ReportDataSource("DSAlunosPorSituacao", dataSourceAlunosPorSituacao));
            };

            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);
            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("Titulo", UIResource.TituloRelatorio));
            parameters.Add(new ReportParameter("MSGTextoInformativo", UIResource.MSGTextoInformativo));

            return SMCGenerateReport("Areas/MAT/Reports/RelatorioAlunosPorSituacao/RelatorioAlunosPorSituacao.rdlc", lista, "DSAlunosPorSituacao", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Landscape, parameters);
        }

        /// <summary>
        /// NV02 Exibir o valor da entidade responsável e do ciclo letivo selecionados 
        /// na pesquisa no seguinte formato: 
        /// "Enidade responsável" + "-" + "Ciclo letivo". 
        /// </summary>
        /// <param name="seqEntidadeResponsavel"></param>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Sub título do relatório</returns>
        private void MontarDescricaoEntidadeResponsavel(AlunosPorSituacaoFiltroVO filtro, List<AlunosPorSituacaoVO> lista)
        {
            var entidades = EntidadeService.BuscarEntidadesNomes(filtro.SeqsEntidadesResponsaveis);

            var cicloLetivo = CicloLetivoService.BuscarCicloLetivo(filtro.SeqCicloLetivo);

            foreach (var item in lista)
            {
                item.NomeEntidadeResponsavel = $"{entidades[item.SeqEntidadeResponsavel]} - {cicloLetivo?.Numero}º/{cicloLetivo?.Ano}";
            }

        }
    }
}