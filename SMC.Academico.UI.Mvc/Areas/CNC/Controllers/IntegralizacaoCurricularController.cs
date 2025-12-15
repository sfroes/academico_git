using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.UI.Mvc.Areas.CNC.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Controllers
{
    public class IntegralizacaoCurricularController : SMCControllerBase
    {
        #region [ Services ]

        private IHistoricoEscolarService HistoricoEscolarService => Create<IHistoricoEscolarService>();

        private IPlanoEstudoItemService PlanoEstudoItemService => Create<IPlanoEstudoItemService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_CNC_001_02_01.EXIBIR_INTEGRALIZACAO_CURRICULAR)]
        public ActionResult ConsultaIntegralizacaoCurricular(long seqPessoaAtuacao)
        {
            var visaoAluno = SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO;
            var model = new IntegralizacaoConsultaHistoricoViewModel();

            model.DadosCabecalho = HistoricoEscolarService.CabecalhoTelaIntegralizacaoCurricularHistorico(seqPessoaAtuacao, visaoAluno).Transform<IntegralizacaoMatrizCurricularOfertaCabecalhoViewModel>();

            var orderEnum = SMCEnumHelper.GenerateKeyValuePair<SituacaoComponenteIntegralizacao>();
            model.LegendaSituacoesComponenteIntegralizacao = orderEnum.OrderBy(o => ((SituacaoComponenteIntegralizacaoOrderBy)o.Key).SMCGetDescription()).Select(s => s.Key).ToList();
            model.LegendaTiposComponentesCurriculares = model.DadosCabecalho.TiposComponentesCurriculares.Select(s => $"<label>{s.Sigla}:</label><p>{s.Descricao}</p>").ToList();

            model.Filtros = new IntegralizacaoMatrizCurricularOfertaFiltroViewModel();
            model.Filtros.SeqPessoaAtuacao = seqPessoaAtuacao;

            var viewPrincipal = GetExternalView(AcademicoExternalViews.INTEGRALIZACAO_CURRICULAR_PRINCIPAL);
            return PartialView(viewPrincipal, model);
        }

        [SMCAuthorize(UC_CNC_001_02_01.EXIBIR_INTEGRALIZACAO_CURRICULAR)]
        public ActionResult PesquisarConsultaIntegralizacaoCurricular(IntegralizacaoMatrizCurricularOfertaFiltroViewModel filtros)
        {
            var visaoAluno = SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO;
            var model = HistoricoEscolarService.ConsultaIntegralizacaoCurricularHistorico(filtros.SeqPessoaAtuacao, visaoAluno, filtros.Transform<IntegralizacaoConsultaHistoricoFiltroData>()).Transform<IntegralizacaoConsultaHistoricoViewModel>();

            model.Filtros = filtros;
            var orderEnum = SMCEnumHelper.GenerateKeyValuePair<SituacaoComponenteIntegralizacao>();
            model.LegendaSituacoesComponenteIntegralizacao = orderEnum.OrderBy(o => ((SituacaoComponenteIntegralizacaoOrderBy)o.Key).SMCGetDescription()).Select(s => s.Key).ToList();
            model.LegendaTiposComponentesCurriculares = model.DadosCabecalho.TiposComponentesCurriculares.Select(s => $"<div><label>{s.Sigla}:</label><p>{s.Descricao}</p></div>").ToList();

            ViewBag.SeqPessoaAtuacao = filtros.SeqPessoaAtuacao;

            var viewPrincipal = GetExternalView(AcademicoExternalViews.INTEGRALIZACAO_CURRICULAR_LISTAR);
            return PartialView(viewPrincipal, model);
        }

        [SMCAuthorize(UC_CNC_001_02_01.EXIBIR_INTEGRALIZACAO_CURRICULAR)]
        [HttpGet]
        public ActionResult ConsultaHistoricosEscolarComponente(string seqsHistoricosEscolarPlano, long? SeqPlanoEstudo, long? SeqPlanoEstudoAntigo, long? SeqComponente, long? SeqConfiguracaoComponente)
        {
            var model = new IntegralizacaoMatrizHistoricoEscolarViewModel();

            List<HistoricoEscolarListaData> historicos = new List<HistoricoEscolarListaData>();

            //Consulta os históricos
            if (!string.IsNullOrEmpty(seqsHistoricosEscolarPlano))
            {
                historicos = HistoricoEscolarService.BuscarHistoricosEscolaresIntegralizacao(seqsHistoricosEscolarPlano);
                var historicoGeral = historicos.First();
                var SeqsAlunosHistoricos = historicos.Select(s => s.SeqAlunoHistorico).ToList();
                var SeqsCicloLetivos = historicos.Select(s => s.SeqCicloLetivo).ToList();

                var turmas = PlanoEstudoItemService.BuscarIntegralizacaoComponentePlanoEstudoTurma(SeqsAlunosHistoricos, SeqsCicloLetivos);

                model.CodigoConfiguracao = historicoGeral.CodigoComponenteCurricular;

                if(string.IsNullOrEmpty(historicoGeral.DescricaoAssunto))
                    model.DescricaoConfiguracao = historicoGeral.DescricaoComponenteCurricular;
                else
                    model.DescricaoConfiguracao = $"{historicoGeral.DescricaoComponenteCurricular}: {historicoGeral.DescricaoAssunto}";

                model.SiglaComponente = historicoGeral.SiglaComponente;                
                model.ExibirTurma = turmas != null && turmas.Any(a => a.SeqComponenteCurricular == historicoGeral.SeqComponenteCurricular);

                model.HistoricosNotas = historicos.Where(w => !w.SeqSolicitacaoDispensa.HasValue).Select(s => new IntegralizacaoMatrizHistoricoEscolarNotaViewModel()
                {
                    Seq = s.Seq,                    
                    DescricaoCicloLetivo = s.DescricaoCicloLetivo,
                    SituacaoHistoricoEscolar = s.SituacaoHistoricoEscolar,
                    Nota = s.Nota,
                    DescricaoConceito = s.DescricaoConceito,
                    Faltas = s.Faltas,
                    CodigoTurma = turmas.FirstOrDefault(f => f.SeqComponenteCurricular == s.SeqComponenteCurricular
                                                         && f.SeqComponenteCurricularAssunto == s.SeqComponenteCurricularAssunto)?.CodigoTurmaFormatado,
                    Professores = turmas.FirstOrDefault(f => f.SeqComponenteCurricular == s.SeqComponenteCurricular
                                                                                            && f.SeqComponenteCurricularAssunto == s.SeqComponenteCurricularAssunto)?.Professores,
                }).ToList();

                model.HistoricosDispensa = historicos.Where(w => w.SeqSolicitacaoDispensa.HasValue).GroupBy(g => g.SeqSolicitacaoDispensa).Select(s => new IntegralizacaoMatrizHistoricoEscolarDispensaViewModel()
                {
                    Seq = s.First().Seq,
                    SeqSolicitacaoDispensa = s.First().SeqSolicitacaoDispensa,
                    DescricaoCicloLetivo = s.First().DescricaoCicloLetivo,
                    SituacaoHistoricoEscolar = $"{s.First().SituacaoHistoricoEscolar.SMCGetDescription()} - {s.First().ProtocoloSolicitacaoDispensa}" ,
                    Nota = s.First().Nota,
                    DescricaoConceito = s.First().DescricaoConceito,
                    Faltas = s.First().Faltas,
                    CodigoTurma = turmas.FirstOrDefault(f => f.SeqComponenteCurricular == s.First().SeqComponenteCurricular
                                                         && f.SeqComponenteCurricularAssunto == s.First().SeqComponenteCurricularAssunto)?.CodigoTurmaFormatado,
                    Professores = turmas.FirstOrDefault(f => f.SeqComponenteCurricular == s.First().SeqComponenteCurricular
                                                                                            && f.SeqComponenteCurricularAssunto == s.First().SeqComponenteCurricularAssunto)?.Professores,
                }).ToList();
            }

            //Consulta o plano de estudo 
            if (SeqPlanoEstudo.HasValue && (SeqComponente.HasValue || SeqConfiguracaoComponente.HasValue))
            {
                var planoItem = PlanoEstudoItemService.BuscarPlanoEstudoItemIntegralizacaoConfiguracao(SeqPlanoEstudo.Value, SeqComponente, SeqConfiguracaoComponente);
                var planoGeral = planoItem.First();
                var SeqsAlunosHistoricos = planoItem.Select(s => s.SeqAlunoHistorico).ToList();
                var SeqsCicloLetivos = planoItem.Select(s => s.SeqCicloLetivo).ToList();

                var turmas = PlanoEstudoItemService.BuscarIntegralizacaoComponentePlanoEstudoTurma(SeqsAlunosHistoricos, SeqsCicloLetivos);

                model.CodigoConfiguracao = planoGeral.CodigoComponenteCurricular;
                if (string.IsNullOrEmpty(planoGeral.DescricaoAssunto))
                    model.DescricaoConfiguracao = planoGeral.DescricaoComponenteCurricular;
                else
                    model.DescricaoConfiguracao = $"{planoGeral.DescricaoComponenteCurricular}: {planoGeral.DescricaoAssunto}";
                
                model.SiglaComponente = planoGeral.SiglaComponente;
                model.ExibirTurma = turmas != null && turmas.Any(a => a.SeqComponenteCurricular == planoGeral.SeqComponenteCurricular);

                model.HistoricosEmCurso = planoItem.Where(w => !historicos
                                                   .Any(a => a.SeqComponenteCurricular == w.SeqComponenteCurricular && a.SeqComponenteCurricularAssunto == w.SeqComponenteCurricularAssunto
                                                    && (a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado || a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado)))
                                                   .Select(s => new IntegralizacaoMatrizHistoricoEmCursoViewModel()
                                                   {
                                                       Seq = s.Seq,
                                                       DescricaoCicloLetivo = s.DescricaoCicloLetivo,
                                                       SituacaoHistoricoEscolar = SituacaoComponenteIntegralizacao.EmCurso,
                                                       Nota = s.Nota,
                                                       DescricaoConceito = s.DescricaoConceito,
                                                       Faltas = s.Faltas,
                                                       CodigoTurma = turmas.FirstOrDefault(f => f.SeqComponenteCurricular == s.SeqComponenteCurricular
                                                                                            && f.SeqComponenteCurricularAssunto == s.SeqComponenteCurricularAssunto)?.CodigoTurmaFormatado,
                                                       Professores = turmas.FirstOrDefault(f => f.SeqComponenteCurricular == s.SeqComponenteCurricular
                                                                                            && f.SeqComponenteCurricularAssunto == s.SeqComponenteCurricularAssunto)?.Professores,
                                                   }).ToList();
            }

            //Consulta o plano de estudo antigo sem lançamento de notas no histórico
            if (SeqPlanoEstudoAntigo.HasValue && (SeqComponente.HasValue || SeqConfiguracaoComponente.HasValue))
            {
                var planoItem = PlanoEstudoItemService.BuscarPlanoEstudoItemIntegralizacaoConfiguracao(SeqPlanoEstudoAntigo.Value, SeqComponente, SeqConfiguracaoComponente);
                var planoGeral = planoItem.First();
                var SeqsAlunosHistoricos = planoItem.Select(s => s.SeqAlunoHistorico).ToList();
                var SeqsCicloLetivos = planoItem.Select(s => s.SeqCicloLetivo).ToList();

                var turmas = PlanoEstudoItemService.BuscarIntegralizacaoComponentePlanoEstudoTurma(SeqsAlunosHistoricos, SeqsCicloLetivos);

                model.CodigoConfiguracao = planoGeral.CodigoComponenteCurricular;
                if (string.IsNullOrEmpty(planoGeral.DescricaoAssunto))
                    model.DescricaoConfiguracao = planoGeral.DescricaoComponenteCurricular;
                else
                    model.DescricaoConfiguracao = $"{planoGeral.DescricaoComponenteCurricular}: {planoGeral.DescricaoAssunto}";

                model.SiglaComponente = planoGeral.SiglaComponente;
                model.ExibirTurma = turmas != null && turmas.Any(a => a.SeqComponenteCurricular == planoGeral.SeqComponenteCurricular);

                model.HistoricosSemApuracao = planoItem.Where(w => !historicos
                                                   .Any(a => a.SeqComponenteCurricular == w.SeqComponenteCurricular && a.SeqComponenteCurricularAssunto == w.SeqComponenteCurricularAssunto
                                                    && (a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado || a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado)))
                                                   .Select(s => new IntegralizacaoMatrizHistoricoSemApuracaoViewModel()
                                                   {
                                                       Seq = s.Seq,
                                                       DescricaoCicloLetivo = s.DescricaoCicloLetivo,
                                                       SituacaoHistoricoEscolar = SituacaoComponenteIntegralizacao.AguardandoNota,
                                                       Nota = s.Nota,
                                                       DescricaoConceito = s.DescricaoConceito,
                                                       Faltas = s.Faltas,
                                                       CodigoTurma = turmas.FirstOrDefault(f => f.SeqComponenteCurricular == s.SeqComponenteCurricular
                                                                                            && f.SeqComponenteCurricularAssunto == s.SeqComponenteCurricularAssunto)?.CodigoTurmaFormatado,
                                                       Professores = turmas.FirstOrDefault(f => f.SeqComponenteCurricular == s.SeqComponenteCurricular
                                                                                            && f.SeqComponenteCurricularAssunto == s.SeqComponenteCurricularAssunto)?.Professores,
                                                   }).ToList();
            }

            var viewModal = GetExternalView(AcademicoExternalViews.INTEGRALIZACAO_CURRICULAR_MODAL_HISTORICO);
            return PartialView(viewModal, model);
        }
    }
}
