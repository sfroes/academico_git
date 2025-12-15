using Microsoft.Reporting.WebForms;
using SMC.Academico.ReportHost.Areas.TUR.Models;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SMC.Academico.ReportHost.Areas.TUR.Apis
{
    public class TurmaReportApiController : SMCApiControllerBase
    {
        #region [ Services ]

        private ITurmaService TurmaService => Create<ITurmaService>();

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();

        #endregion [ Services ]

        //[SMCAuthorize(UC_TUR_001_04_01.PESQUISAR_TURMA)]
        [HttpPost]
        [SMCAllowAnonymous]
        public byte[] GerarRelatorio(TurmaRelatorioFiltroVO filtro)
        {
            var filtroData = filtro.Transform<TurmaCicloLetivoRelatorioFiltroData>();

            var cicloLetivoDescricao = CicloLetivoService.BuscarDescricaoFormatadaCicloLetivo(filtroData.SeqCicloLetivo.Value);

            //Título do Relatório deve ser: "Relatório de Turmas - Ciclo: <ciclo letivo>"(ciclo letivo selecionado como filtro).
            string titulo = $"Relatório de Turmas - Ciclo Letivo: {cicloLetivoDescricao}";

            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(filtro.SeqInstituicaoEnsino);
            var parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("Titulo", titulo));

            var registros = TurmaService.BuscarRelatorioTurmasPorCicloLetivo(filtroData);

            if (registros.SMCCount() == 0)
                throw new Exception("Não existem turmas para serem exibidas a partir dos filtros aplicados.");

            return SMCGenerateReport("Areas/TUR/Reports/Relatorio/RelatorioTurmaCicloLetivoColaboradores.rdlc", registros, "DSTurmaCicloLetivo", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Portrait, parameters);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        //[SMCAuthorize(UC_APR_002_02_01.EXIBIR_RELATORIO_DIARIO_TURMA)]
        public byte[] DiarioTurmaRelatorio(long id)
        {
            var seqTurma = id;
            // Busca os dados no banco
            var cabecalho = TurmaService.BuscarDiarioTurmaCabecalho(seqTurma);
            var alunos = TurmaService.BuscarDiarioTurmaAluno(seqTurma, null, null, null);
            var professores = TurmaService.BuscarDiarioTurmaProfessor(seqTurma);
            var seqInstituicaoTurma = TurmaService.BuscarSeqInstituicaoEnsinoTurma(seqTurma);

            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(seqInstituicaoTurma);
            var parameters = new List<ReportParameter>();

            bool indicadorDiarioFechado = Convert.ToBoolean(cabecalho.FirstOrDefault()?.IndicadorDiarioFechado);

            parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));
            parameters.Add(new ReportParameter("IndicadorDiarioFechado", indicadorDiarioFechado.ToString()));

            //Ordenar e remover registro duplicado de professor da turma e da divisão
            if (professores.SMCCount() > 0)
            {
                professores = professores.Select(s => new DiarioTurmaProfessorData() { NomeProfessor = s.NomeProfessor })
                                         .SMCDistinct(d => d.NomeProfessor)
                                         .OrderBy(o => o.NomeProfessor)
                                         .ToList();
            }

            //Agrupando por disciplina e, depois, por curso, para montar o cabeçalho.
            List<DiarioTurmaCabecalhoData> listaDisciplinasCursos = new List<DiarioTurmaCabecalhoData>();
            foreach (var itemGrupo in cabecalho.GroupBy(a => a.Disciplina).ToList())
            {
                string itemListaDisciplinaCurso = itemGrupo.Key + Environment.NewLine;
                List<string> listaAdicionados = new List<string>();
                foreach (var item in itemGrupo)
                {
                    if (!listaAdicionados.Contains(item.Curso))
                    {
                        listaAdicionados.Add(item.Curso);
                        itemListaDisciplinaCurso += "\t\t\t\t- " + item.Curso + Environment.NewLine;
                    }
                }
                listaDisciplinasCursos.Add(new DiarioTurmaCabecalhoData()
                {
                    DataFechamentoDiario = itemGrupo.FirstOrDefault().DataFechamentoDiario,
                    IndicadorDiarioFechado = itemGrupo.FirstOrDefault().IndicadorDiarioFechado,
                    DescricaoCicloLetivo = itemGrupo.FirstOrDefault().DescricaoCicloLetivo,
                    TurmaCabecalhoRelatorio = itemListaDisciplinaCurso
                });
            }

            Action<object, SubreportProcessingEventArgs> reportAction = (sender, e) =>
            {
                e.DataSources.Add(new ReportDataSource("DSDiarioTurmaCabecalho", listaDisciplinasCursos));
                e.DataSources.Add(new ReportDataSource("DSDiarioTurmaProfessores", professores));
            };

            return SMCGenerateReport("Areas/TUR/Reports/DiarioTurma/DiarioTurmaRelatorio.rdlc", alunos, "DSDiarioTurma", new SMCReportViewerHelper(SMCExportTypes.PDF, reportAction), SMCOrientationReport.Portrait, parameters);
        }
    }
}