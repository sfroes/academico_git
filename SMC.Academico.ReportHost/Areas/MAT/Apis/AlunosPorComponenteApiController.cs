using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.MAT.Models;
using SMC.Academico.ReportHost.Areas.MAT.Views.RelatorioAlunosPorComponente.App_LocalResources;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.MAT.Apis
{
    public class AlunosPorComponenteApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private IAlunoService AlunoService => Create<IAlunoService>();
        private ITurmaService TurmaService => Create<ITurmaService>();
        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();
        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => Create<ICursoOfertaLocalidadeService>();
        private IConfiguracaoComponenteService ConfiguracaoComponenteService => Create<IConfiguracaoComponenteService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion

        [HttpPost]
        //[SMCAuthorize(UC_MAT_005_07_01.PESQUISAR_ALUNOS_COMPONENTE_CURRICULAR)]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(AlunosPorComponenteFiltroVO filtro)
        {
            var lista = AlunoService.BuscarDadosRelatorioAlunosPorComponente(filtro.Transform<RelatorioAlunosPorComponenteFiltroData>()).TransformList<AlunosPorComponenteVO>();
            return RelatorioAlunosPorComponente(lista, filtro);
        }

        public byte[] RelatorioAlunosPorComponente(List<AlunosPorComponenteVO> lista, AlunosPorComponenteFiltroVO filtro)
        {
            var cicloLetivo = this.CicloLetivoService.BuscarDescricaoFormatadaCicloLetivo(filtro.SeqCicloLetivo);

            var cursoOfertaLocalidade = this.CursoOfertaLocalidadeService.BuscarCursoOfertaLocalidade(filtro.SeqCursoOfertaLocalidade, true);

            var turmaAtividadeAcademica = string.Empty;

            if (filtro.SeqTurma.HasValue)
                turmaAtividadeAcademica = this.TurmaService.BuscarDescricaoTurmaConcatenado(filtro.SeqTurma.GetValueOrDefault());
            else
                turmaAtividadeAcademica = this.ConfiguracaoComponenteService.BuscarDescricaoConfiguracaoComponente(filtro.SeqConfiguracaoComponente.GetValueOrDefault());

            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);

            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("Titulo", UIResource.TituloRelatorio));
            parameters.Add(new ReportParameter("CicloLetivo", cicloLetivo));
            parameters.Add(new ReportParameter("CursoOfertaLocalidade", $"{cursoOfertaLocalidade.DescricaoCursoOferta} - {cursoOfertaLocalidade.NomeLocalidade}"));
            parameters.Add(new ReportParameter("ETurma", filtro.SeqTurma.HasValue ? "True" : "False"));
            parameters.Add(new ReportParameter("TurmaAtividadeAcademica", turmaAtividadeAcademica));
            parameters.Add(new ReportParameter("ExibirSolicitanteMatrículaNaoFinalizada", filtro.ExibirSolicitanteMatrículaNaoFinalizada.HasValue && filtro.ExibirSolicitanteMatrículaNaoFinalizada.Value ? "True" : "False"));

            return SMCGenerateReport("Areas/MAT/Reports/RelatorioAlunosPorComponente/RelatorioAlunosPorComponente.rdlc", lista, "DSAlunosPorComponente", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Landscape, parameters);
        }
    }
}