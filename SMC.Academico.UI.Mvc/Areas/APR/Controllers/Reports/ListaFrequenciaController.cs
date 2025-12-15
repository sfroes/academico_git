using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.APR.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.APR.Controllers
{
    public class ListaFrequenciaController : SMCReportingControllerBase
    {
        /*
         1) Para fazer funcionar tem que mudar a rota na area no sistema. Procura TODO: DynamicUIMVC
         2) Coloque a View como EmbeddedResource
         3) Crie um PATH (nome do projeto mais o caminha ate chegar na View) e concatene com a view
        */

        #region [ Propriedades ]

        private DivisaoTurmaDetalhesData _divisaoTurmaDetalhes;
        private long _seqDivisaoTurma;

        public override string ControllerName
        {
            get
            {
                return "ListaFrequencia";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\APR\Reports\";
            }
        }

        #endregion [ Propriedades ]

        #region [ Services ]

        internal ITurmaService TurmaService => this.Create<ITurmaService>();

        internal IInstituicaoEnsinoService InstituicaoEnsinoService
        {
            get { return this.Create<IInstituicaoEnsinoService>(); }
        }

        private IDivisaoTurmaService DivisaoTurmaService
        {
            get { return Create<IDivisaoTurmaService>(); }
        }

        private IEventoAulaService EventoAulaService => Create<IEventoAulaService>();

        private IInstituicaoNivelService InstituicaoNivelService
        {
            get { return Create<IInstituicaoNivelService>(); }
        }

        #endregion [ Services ]

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("ListaFrequenciaReport");

        #endregion APIS

        #region [ Actions ]

        [HttpGet]
        [SMCAuthorize(UC_APR_006_03_01.PARAMETROS_LISTA_FREQUENCIA)]
        public ActionResult Index(SMCEncryptedLong seqDivisaoTurma, bool? selecionarProfessor)
        {
            try
            {
                ValidarDivisaoTurma(seqDivisaoTurma);

                var filtro = MontarFiltro(selecionarProfessor);

                var view = GetExternalView(AcademicoExternalViews.LISTA_FREQUENCIA_PATH + "Index");

                return PartialView(view, filtro);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                return ThrowOpenModalException(ex.Message);
            }
        }

        [HttpPost]
        [SMCAuthorize(UC_APR_006_03_02.EXIBIR_LISTA_FREQUENCIA)]
        public ActionResult GerarRelatorio(ListaFrequenciaFiltroViewModel filtro, List<long> colaboradores)
        {
            try
            {
                string QuantidadeAula = string.Empty;
                if (filtro.SeqGradeHoraria.HasValue)
                {
                    List<ListaFrequenciaGradeTurnoViewModel> grades = (List<ListaFrequenciaGradeTurnoViewModel>)TempData["FREQUENCIA_GRADE_HORARIO"];
                    var gradeSelecionada = grades.Where(w => w.Seq == filtro.SeqGradeHoraria.Value).FirstOrDefault();

                    if (gradeSelecionada != null)
                    {
                        var minutosTotalInicio = (((int)(gradeSelecionada.HoraInicio / 100)) * 60) + (gradeSelecionada.HoraInicio % 100);
                        var minutosTotalFim = (((int)(gradeSelecionada.HoraFim / 100)) * 60) + (gradeSelecionada.HoraFim % 100);
                        var dataInicio = gradeSelecionada.Data.Value.AddMinutes(minutosTotalInicio);
                        var dataFim = gradeSelecionada.Data.Value.AddMinutes(minutosTotalFim);
                        filtro.DiaEmissao = gradeSelecionada.Data;
                        filtro.HoraInicio = dataInicio;
                        filtro.HoraFim = dataFim;
                        QuantidadeAula = gradeSelecionada.QuantidadeAulas.ToString();
                    }
                    TempData["FREQUENCIA_GRADE_HORARIO"] = grades;
                }
                else
                {
                    if(filtro.DiaEmissao.HasValue)
                    {
                        // Faz a comparação desconsiderando a hora da aula, pois o período letivo da turma não tem hora informada
                        if (filtro.DiaEmissao < filtro.DataInicioPeriodoLetivo || filtro.DiaEmissao > filtro.DataFimPeriodoLetivo)
                        {
                            throw new Exception($"A data informada deverá estar entre a {filtro.DataInicioPeriodoLetivo.Value} e a {filtro.DataFimPeriodoLetivo.Value} !");
                        }
                    }
                }

                var param = new
                {
                    filtro.TurmaDescricao,
                    filtro.DescricaoOfertaTurno,
                    filtro.DescricoesCursoOfertaLocalidadeTurno,
                    filtro.DiaEmissao,
                    filtro.HoraInicio,
                    filtro.HoraFim,
                    Colaboradores = colaboradores,
                    filtro.SeqNivelEnsino,
                    filtro.SeqDivisaoTurma,
                    QuantidadeAula
                };

                var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
                return new FileContentResult(dadosReport, "application/pdf");
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.SMCLastInnerException().Message, target: SMCMessagePlaceholders.Centro);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        #endregion [ Actions ]

        #region [ Métodos Privados ]

        private ListaFrequenciaFiltroViewModel MontarFiltro(bool? selecionarProfessor)
        {
            var divisaoTurmaCabecalho = DivisaoTurmaService.BuscarDivisaoTurmaCabecalho(_seqDivisaoTurma);
            _divisaoTurmaDetalhes = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(_seqDivisaoTurma, preservarColaboradores: true).FirstOrDefault(d => d.Seq == _seqDivisaoTurma);

            var filtro = new ListaFrequenciaFiltroViewModel()
            {
                TurmaDescricao = divisaoTurmaCabecalho.TurmaDescricaoFormatado,
                Colaboradores = new SMCPagerModel<ListaFrequenciaColaboradorViewModel>(
                         _divisaoTurmaDetalhes.Colaboradores.Select(c =>
                         new ListaFrequenciaColaboradorViewModel
                         {
                             SeqColaborador = c.SeqColaborador,
                             NomeColaborador = c.NomeFormatado
                         }))
            };

            if (selecionarProfessor.GetValueOrDefault())
            {
                filtro.Colaboradores.SelectedValues = new List<object>()
                    {
                        HttpContext.GetEntityLogOn(FILTER.PROFESSOR).Value
                    };
            }

            filtro.SeqDivisaoTurma = _seqDivisaoTurma;
            filtro.SeqNivelEnsino = _divisaoTurmaDetalhes.SeqNivelEnsino;
            filtro.DescricaoOfertaTurno = $"{_divisaoTurmaDetalhes.NomeCursoOfertaLocalidade} - {_divisaoTurmaDetalhes.DescricaoTurno}";
            filtro.DescricoesCursoOfertaLocalidadeTurno = _divisaoTurmaDetalhes.DescricoesCursoOfertaLocalidadeTurno;
            filtro.DataInicioPeriodoLetivo = _divisaoTurmaDetalhes.DataInicioPeriodoLetivo;
            filtro.DataFimPeriodoLetivo = _divisaoTurmaDetalhes.DataFimPeriodoLetivo;

            var divisaoEvento = this.EventoAulaService.BuscarEventosOrigemAvaliacao(filtro.SeqDivisaoTurma, _divisaoTurmaDetalhes.SeqOrigemAvaliacao);
            if (divisaoEvento == null || divisaoEvento.EventoAulas == null|| divisaoEvento.EventoAulas.Count() == 0)
            {
                filtro.DisponibilizarGrade = false;
            }
            else
            {
                filtro.DisponibilizarGrade = true;
                var filtroGrade = divisaoEvento.EventoAulas.OrderBy(o => o.Data).ThenBy(t=> t.HoraInicio).GroupBy(g => new { g.Data, g.Turno })
                    .Select(s => new ListaFrequenciaGradeTurnoViewModel()
                    {
                        Seq = s.First().Seq,
                        Descricao = $"{s.First().Data.ToShortDateString()} {s.First().HoraInicio} - {s.Last().HoraFim} ({s.Count()} aulas)",
                        Data = s.First().Data,
                        HoraInicio = Convert.ToInt64(s.First().HoraInicio.Remove(2, 1)),
                        HoraFim = Convert.ToInt64(s.Last().HoraFim.Remove(2, 1)),                        
                        QuantidadeAulas = s.Count()
                    }).ToList();

                TempData["FREQUENCIA_GRADE_HORARIO"] = filtroGrade;
                filtro.GradeHoraria = filtroGrade.Select(s => new SMCDatasourceItem()
                {
                    Seq = s.Seq,
                    Descricao = s.Descricao
                }).ToList();
            }
            return filtro;
        }

        private void ValidarDivisaoTurma(long? seqDivisaoTurma)
        {
            _seqDivisaoTurma = seqDivisaoTurma.Value;

            if (_seqDivisaoTurma <= 0) { throw new Exception("Divisão de turma inválida!"); }
        }

        #endregion [ Métodos Privados ]
    }
}