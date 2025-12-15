using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.APR.Models;
using SMC.Academico.UI.Mvc.Areas.APR.Views.LancamentoHistoricoEscolar.App_LocalResources;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.APR.Controllers
{
    public class LancamentoHistoricoEscolarController : SMCControllerBase
    {
        #region [ Serviços ]

        private ITurmaService TurmaService { get => Create<ITurmaService>(); }

        private IHistoricoEscolarService HistoricoEscolarService { get => Create<IHistoricoEscolarService>(); }

        private IEscalaApuracaoItemService EscalaApuracaoItemService { get => Create<IEscalaApuracaoItemService>(); }

        private IDivisaoTurmaService DivisaoTurmaService { get => Create<IDivisaoTurmaService>(); }

        #endregion [ Serviços ]

        #region [ Get ]

        [SMCAuthorize(UC_APR_001_15_01.LANCAMENTO_NOTA_FREQUENCIA_FINAL)]
        public ActionResult Index(SMCEncryptedLong seqTurma, string backUrl = null)
        {
            try
            {
                var view = GetExternalView(AcademicoExternalViews.LANCAMENTO_HISTORICOESCOLAR_PATH + "Index");

                LancamentoHistoricoEscolarViewModel model = DadosInciais(seqTurma, true);
                model.BackUrl = backUrl;

                return View(view, model);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return RedirectToAction("Index", "Turma", new { area = "TUR" });
            }
        }

        [SMCAuthorize(UC_APR_001_15_01.LANCAMENTO_NOTA_FREQUENCIA_FINAL)]
        public ActionResult BuscarLancamentoHistoricoEscolarCabecalho(long seqTurma)
        {
            LancamentoHistoricoEscolarCabecalhoViewModel modelo = new LancamentoHistoricoEscolarCabecalhoViewModel();

            // Monta a lista de turma para o cabeçalho
            modelo.TurmaCabecalho = TurmaService.BuscarDiarioTurmaCabecalho(seqTurma).TransformList<LancamentoHistoricoEscolarCabecalhoTurmaItemViewModel>();

            var view = GetExternalView(AcademicoExternalViews.LANCAMENTO_HISTORICOESCOLAR_PATH + "_Cabecalho");
            return PartialView(view, modelo);
        }

        [SMCAllowAnonymous]
        public ActionResult DiarioTurmaFechado(long? seqTurma)
        {
            return Json(TurmaService.DiarioTurmaFechado(new TurmaFiltroData { Seq = seqTurma.GetValueOrDefault() }));
        }

        #endregion [ Get ]

        #region [ Post ]

        [HttpPost]
        [SMCAuthorize(UC_APR_001_15_01.LANCAMENTO_NOTA_FREQUENCIA_FINAL)]
        public ActionResult Salvar(LancamentoHistoricoEscolarViewModel modelo)
        {
            SalvarLancamento(modelo);

            return RedirectToAction("Index", new { seqTurma = new SMCEncryptedLong(modelo.SeqTurma), backUrl = modelo.BackUrl });
        }

        [SMCAuthorize(UC_APR_001_15_01.LANCAMENTO_NOTA_FREQUENCIA_FINAL)]
        public ActionResult FecharDiarioTurma(LancamentoHistoricoEscolarViewModel modelo)
        {
            try
            {
                long? seqColaborador = HttpContext.GetEntityLogOn(FILTER.PROFESSOR)?.Value;

                //Conferindo novamente se a tela não foi chamada equivocadamente.
                LancamentoHistoricoEscolarViewModel modeloOriginal = DadosInciais(modelo.SeqTurma);

                if (modeloOriginal.HabilitaFecharDiario && modeloOriginal.HabilitaSalvar)
                {
                    if (SalvarLancamento(modelo))
                    {
                        // Valida se pode ou não fechar...
                        TurmaService.VerificaFecharDiarioAlunoSemNota(modelo.SeqTurma, seqColaborador.GetValueOrDefault());

                        // Manda o assert
                        modelo = DadosInciais(modelo.SeqTurma); // Carrega novamente para atualizar o AlunosPendentes
                        Assert(modelo, modelo.AlunosPendentes ? UIResource.MSG_FecharDiarioPendentes : UIResource.MSG_FecharDiario, () => true);

                        //Fechar diário.
                        TurmaService.FecharDiarioTurma(modelo.SeqTurma, seqColaborador.GetValueOrDefault());
                        SetSuccessMessage(UIResource.MensagemDiarioFechadoSucesso, target: SMCMessagePlaceholders.Centro);
                    }
                }
                else
                {
                    //Não pode fechar diário.
                    if (!modelo.HabilitaSalvar)
                    {
                        //Erro: não é professor da turma
                        SetErrorMessage(UIResource.ERR_NaoProfessor, target: SMCMessagePlaceholders.Centro);
                    }
                    else
                    {
                        //Erro: diário já fechado.
                        SetErrorMessage(UIResource.ERR_DiarioJaFechado, target: SMCMessagePlaceholders.Centro);
                    }
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction("Index", "LancamentoHistoricoEscolar", new { seqTurma = modelo.SeqTurma, backUrl = modelo.BackUrl });
        }

        [HttpPost]
        [SMCAuthorize(UC_APR_001_15_01.LANCAMENTO_NOTA_FREQUENCIA_FINAL)]
        public JsonResult CalcularSituacaoFinalAluno(HistoricoEscolarNotaViewModel model)
        {
            var data = model.Transform<HistoricoEscolarSituacaoFinalData>();

            return Json(HistoricoEscolarService.CalcularSituacaoFinal(data).SMCGetDescription());
        }

        #endregion [ Post ]

        #region [ Métodos auxiliares ]

        private LancamentoHistoricoEscolarViewModel TratarVisibilidadeAlunos(LancamentoHistoricoEscolarData data, LancamentoHistoricoEscolarViewModel modelo)
        {
            var aluno = modelo.Lancamentos.FirstOrDefault();

            modelo.MostrarLacamentosSemNota = aluno?.MostrarLacamentosSemNota ?? false;
            modelo.MostrarLancamentosNota = aluno?.MostrarLancamentosNota ?? false;
            modelo.MostrarLancamentosNotaDescricao = aluno?.MostrarLancamentosNotaDescricao ?? false;
            modelo.MostrarLancamentosSeqEscalaApuracaoItem = aluno?.MostrarLancamentosSeqEscalaApuracaoItem ?? false;

            if (modelo.MostrarLancamentosSeqEscalaApuracaoItem)
            {
                modelo.ListaEscalaApuracaoItens = EscalaApuracaoItemService.BuscarEscalaApuracaoItensSelect(new EscalaApuracaoItemFiltroData() { SeqEscalaApuracao = aluno.SeqEscalaApuracao.GetValueOrDefault() });
            }
            else if (aluno != null && aluno.SeqEscalaApuracao.HasValue)
            {
                var escalaApuracao = EscalaApuracaoItemService.BuscarEscalaApuracaoItens(aluno.SeqEscalaApuracao.Value);
                modelo.EscalaApuracao = $"[{string.Join(",", escalaApuracao.Select(f => $"{{\"desc\":\"{f.Descricao}\",\"min\":\"{f.PercentualMinimo}\", \"max\":\"{f.PercentualMaximo}\"}}"))}]";
            }

            var situacoesFinais = SMCEnumHelper.GenerateKeyValuePair<SituacaoHistoricoEscolar>().Select(f =>
            {
                return $"\"{f.Key.ToString()}\":\"{SMCEnumHelper.GetDescription(typeof(SituacaoHistoricoEscolar), f.Value)}\"";
            });
            modelo.SituacoesFinais = $"{{{string.Join(",", situacoesFinais)}}}";

            return modelo;
        }

        private LancamentoHistoricoEscolarViewModel DadosInciais(long seqTurma, bool montarMesteDetalheAlunos = false)
        {
            bool professorDaTurma = true;
            bool professorDaTurmaOuDivisao = true;
            long? seqColaborador = HttpContext.GetEntityLogOn(FILTER.PROFESSOR)?.Value;
            LancamentoHistoricoEscolarData data = null;

            if (SMCContext.ApplicationId != SIGLA_APLICACAO.SGA_ADMINISTRATIVO)
            {
                // Obtendo o professor logado e consultando se ele é professor da turma passada no parâmetro.
                List<DiarioTurmaProfessorData> professores = TurmaService.BuscarDiarioTurmaProfessor(seqTurma);
                professorDaTurma = professores.Any(a => a.SeqColaborador == seqColaborador && a.ProfessorTurma);
                professorDaTurmaOuDivisao = professores.Any(a => a.SeqColaborador == seqColaborador);
            }

            // Caso tenha colaborador, cria a lista para enviar de filtro
            List<long> seqsColaboradores = null;
            if (!professorDaTurma && seqColaborador.HasValue)
                seqsColaboradores = new List<long> { seqColaborador.Value };

            data = HistoricoEscolarService.LancarNotasFrequenciasFinais(seqTurma, seqsColaboradores);

            data.MateriaLecionadaObrigatoria = DivisaoTurmaService.MateriaLecionadaObrigatoria(seqTurma);

            // Buscando os alunos da turma e formando a tela.
            var modelo = data.Transform<LancamentoHistoricoEscolarViewModel>();

            // Verificando se deve ou não habilitar o botão de fechamento de diário.
            bool? diarioFechado = TurmaService.DiarioTurmaFechado(new TurmaFiltroData { Seq = seqTurma });

            if (!diarioFechado.HasValue)
                throw new LancamentoHistoricoEscolarTurmaSemDiarioException();

            modelo.HabilitaFecharDiario = professorDaTurma && !diarioFechado.Value;
            modelo.HabilitaSalvar = professorDaTurmaOuDivisao && !diarioFechado.Value;
            if (!modelo.HabilitaSalvar)
            {
                this.SetViewMode(SMCViewMode.ReadOnly);
            }

            if (data.Lancamentos != null)
            {
                modelo.AlunosPendentes = data.Lancamentos.Any(a => a.Seq == default(long));
                if (montarMesteDetalheAlunos)
                {
                    return TratarVisibilidadeAlunos(data, modelo);
                }
            }
            else
            {
                modelo.TurmaSemAluno = true;
            }

            return modelo;
        }

        private bool SalvarLancamento(LancamentoHistoricoEscolarViewModel modelo)
        {
            try
            {
                LancamentoHistoricoEscolarData data = modelo.Transform<LancamentoHistoricoEscolarData>();
                if (data.Lancamentos == null)
                {
                    throw new Exception(UIResource.LancamentosNaoEncontrados);
                }

                HistoricoEscolarService.SalvarLancamentoNotasFrequenciaFinal(data);
                SetSuccessMessage(UIResource.MensagemSucesso, target: SMCMessagePlaceholders.Centro);
                return true;
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return false;
            }
        }

        #endregion [ Métodos auxiliares ]
    }
}