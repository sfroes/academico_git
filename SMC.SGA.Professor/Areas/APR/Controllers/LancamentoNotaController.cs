using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Rest;
using SMC.Framework.Security.Util;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Professor.Areas.APR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.SessionState;

namespace SMC.SGA.Professor.Areas.APR.Controllers
{
#if !DEBUG
    [SessionState(SessionStateBehavior.ReadOnly)]
#endif
    public class LancamentoNotaController : SMCControllerBase
    {
        #region [Servicos]

        private IApuracaoAvaliacaoService ApuracaoAvaliacaoService => Create<IApuracaoAvaliacaoService>();
        
        private ITurmaService TurmaService => Create<ITurmaService>();

        private IHistoricoEscolarService HistoricoEscolarService => Create<IHistoricoEscolarService>();

        private IAlunoHistoricoService AlunoHistoricoService => Create<IAlunoHistoricoService>();

        private IAplicacaoAvaliacaoService AplicacaoAvaliacaoService => Create<IAplicacaoAvaliacaoService>();

        #endregion

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("TurmaReport");

        #endregion APIS

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult BuscarLancamentosAvaliacao(SMCEncryptedLong seqOrigemAvaliacao)
        {
            long? seqProfessor = HttpContext.GetEntityLogOn(FILTER.PROFESSOR)?.Value;
            var result = ApuracaoAvaliacaoService.BuscarLancamentosAvaliacao(seqOrigemAvaliacao, seqProfessor.GetValueOrDefault());
            var data = new
            {
                SeqOrigemAvaliacao = new SMCEncryptedLong(result.SeqOrigemAvaliacao).ToString(),
                result.ApuracaoFrequencia,
                result.ApuracaoNota,
                result.OrigemAvaliacaoTurma,
                result.ResponsavelTurma,
                result.DiarioFechado,
                result.DescricaoOrigemAvaliacao,
                result.MateriaLecionada,
                result.PermiteAlunoSemNota,
                result.PermiteMateriaLecionada,
                result.MateriaLecionadaObrigatoria,
                result.MateriaLecionadaCadastrada,
                result.DescricoesDivisaoTurma,
                Avaliacoes = result.AplicacaoAvaliacoes.Select(s => new {
                    SeqAplicacaoAvaliacao = new SMCEncryptedLong(s.Seq).ToString(),
                    s.Sigla,
                    s.Avaliacao.Descricao,
                    s.Avaliacao.Valor,
                    s.EntregaWeb,
                }),
                Alunos = result.Alunos.Select(s => new {
                    SeqAlunoHistorico = new SMCEncryptedLong(s.SeqAlunoHistorico).ToString(),
                    NumeroRegistroAcademico = s.NumeroRegistroAcademico.ToString(),
                    s.Formado,
                    s.Nome,
                    s.AlunoAprovado,
                    s.AlunoDispensado,
                    Apuracoes = s.Apuracoes.Select(sa => new {
                        Seq = new SMCEncryptedLong(sa.Seq).ToString(),
                        SeqAlunoHistorico = new SMCEncryptedLong(sa.SeqAlunoHistorico).ToString(),
                        SeqAplicacaoAvaliacao = new SMCEncryptedLong(sa.SeqAplicacaoAvaliacao).ToString(),
                        Nota = sa.Nota?.ToString(),
                        sa.Comparecimento,
                        sa.ComentarioApuracao,
                        sa.AlunoComComponenteOutroHistorico,
                        sa.PermitirAlunoEntregarOnlinePosPrazo
                    }),
                    s.Faltas,
                    Total = s.Nota,
                    s.SituacaoFinal,
                    s.DescricaoSituacaoFinal,
                    TemHistorico = s.SituacaoFinal != SituacaoHistoricoEscolar.Nenhum,
                })
            };
            return JsonResultAngular(data);
        }

        //TODO: Verificar token manter
        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult SalvarLancamentosAvaliacao(LancamentoNotaViewModel model)
        {
            try
            {
                var data = model.Transform<LancamentoAvaliacaoData>();
                ApuracaoAvaliacaoService.SalvarLancamentosAvaliacao(data);
                if (!data.Apuracoes.SMCAny() && !data.SeqsApuracaoExculida.SMCAny())
                {
                    return SMCJsonResultAngular("");
                }
                return BuscarLancamentosAvaliacao(new SMCEncryptedLong(model.SeqOrigemAvaliacao));
            }
            catch (Exception ex)
            {
                return HandleErrorAngular(ex);
            }
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult FecharDiario(LancamentoNotaFechamentoDiarioViewModel model)
        {
            try
            {
                long? seqProfessor = HttpContext.GetEntityLogOn(FILTER.PROFESSOR)?.Value;
                var data = model.Transform<LancamentoNotaFechamentoDiarioData>();
                data.SeqProfessor = seqProfessor.GetValueOrDefault();
                ApuracaoAvaliacaoService.FecharLancamentoDiario(data);
                return BuscarLancamentosAvaliacao(new SMCEncryptedLong(model.SeqOrigemAvaliacao));
            }
            catch (Exception ex)
            {
                return HandleErrorAngular(ex);
            }
        }

        [HttpGet]
        [SMCAuthorize(UC_APR_002_02_01.EXIBIR_RELATORIO_DIARIO_TURMA)]
        public FileContentResult DiarioTurmaRelatorio(SMCEncryptedLong seqOrigemAvaliacao)
        {
            var seqTurma = TurmaService.BuscarSeqTurmaPorOrigemAvaliacao(seqOrigemAvaliacao);
            var dadosReport = ReportAPI.Execute<byte[]>($"DiarioTurmaRelatorio/{seqTurma}", Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult CalcularSituacaoFinal(long seqOrigemAvaliacao, long seqAlunoHistorico, short? notaTotal)
        {
            SituacaoHistoricoEscolar retorno = CalcularSituacaoFinalAluno(seqOrigemAvaliacao, seqAlunoHistorico, notaTotal);

            return JsonResultAngular(new
            {
                valor = retorno,
                descricao = retorno.SMCGetDescription()
            });
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult CalcularTotalTurma(LancamentoNotaViewModel model)
        {
            return JsonResultAngular(CalcularTotalTurmaAluno(model));
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult CalcularTotalDivisaoTurma(LancamentoNotaViewModel model)
        {
            decimal? retorno = SomarApuracoes(model);

            return JsonResultAngular(retorno);
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult CalcularTotalParcial(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            var total = CalcularTotalParcialAluno(seqAlunoHistorico, seqOrigemAvaliacao);

            return JsonResultAngular(total);
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult CalcularTotaisTurma(long seqOrigemAvaliacao, long[] seqsAlunoHistorico, bool permiteAlunoSemNota)
        {
            var result = ApuracaoAvaliacaoService.CalcularTotaisTurma(seqOrigemAvaliacao, seqsAlunoHistorico, permiteAlunoSemNota)
                .Select(s => new LancamentoNotaAlunoTotaisTurmaModel()
                {
                    SeqAlunoHistorico = new SMCEncryptedLong(s.SeqAlunoHistorico).ToString(),
                    TotalParcial = s.TotalParcial,
                    TotalFinal = s.TotalFinal == null ? null : (short?)Convert.ToInt16(Math.Round((s.TotalFinal ?? 0), 0, MidpointRounding.AwayFromZero)),
                    SituacaoFinal = s.SituacaoHistoricoEscolar.GetValueOrDefault(),
                    DescricaoSituacaoFinal = s.SituacaoHistoricoEscolar?.SMCGetDescription(),
                    TodasApuracoesDivisaoLancadas = s.TodasApuracoesDivisaoLancadas,
                    TodasApuracoesVazias = s.TodasApuracoesVazias
                }).ToList();
            return JsonResultAngular(result);
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult CalcularTotaisDivisaoTurma(long seqOrigemAvaliacao, long[] seqsAlunoHistorico)
        {
            if (!seqsAlunoHistorico.SMCAny())
            {
                return null;
            }
            var result = new List<LancamentoNotaAlunoTotaisDivisaoTurmaViewModel>();
            foreach (var seqAlunoHistorico in seqsAlunoHistorico)
            {
                var retorno = new LancamentoNotaAlunoTotaisDivisaoTurmaViewModel();
                retorno.SeqAlunoHistorico = new SMCEncryptedLong(seqAlunoHistorico).ToString();
                var lancamento = new LancamentoNotaViewModel();
                lancamento.Apuracoes = ApuracaoAvaliacaoService
                    .BuscarApuracoesPorAlunoOrigemAvaliacao(seqAlunoHistorico, seqOrigemAvaliacao)
                    .TransformList<LancamentoNotaApuracaoViewModel>();
                 retorno.Total = SomarApuracoes(lancamento);
                result.Add(retorno);
            }
            return JsonResultAngular(result);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarTokensSeguranca()
        {
            var retorno = new[]
            {
                new {nome = "comentarioApuracao", permitido = SMCSecurityHelper.Authorize(UC_APR_001_05_02.COMENTARIO_APURACAO)},
            };

            return SMCJsonResultAngular(retorno);
        }

        [SMCAuthorize(UC_APR_001_01_01.PESQUISAR_AVALIACAO_TURMA)]
        public ContentResult ExcluirHistoricoEscolar(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            try
            {
                HistoricoEscolarService.ExcluirHistoricoEscolarPorAlunoHistorico(seqAlunoHistorico, seqOrigemAvaliacao);
                return BuscarLancamentosAvaliacao(new SMCEncryptedLong(seqOrigemAvaliacao));
            }
            catch (Exception ex)
            {
                return HandleErrorAngular(ex);
            }
        }

        private string CalcularTotalParcialAluno(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            List<ApuracaoAvaliacaoData> apuracoes = ApuracaoAvaliacaoService.BuscarApuracoesDasDivisioesPorAlunoTurma(seqAlunoHistorico, seqOrigemAvaliacao);

            LancamentoNotaViewModel lancamentoNotaViewModel = new LancamentoNotaViewModel();
            lancamentoNotaViewModel.SeqOrigemAvaliacao = seqOrigemAvaliacao;
            lancamentoNotaViewModel.Apuracoes = new List<LancamentoNotaApuracaoViewModel>();

            foreach (var item in apuracoes)
            {
                LancamentoNotaApuracaoViewModel apuracao = new LancamentoNotaApuracaoViewModel();
                apuracao.Nota = item.Nota;
                apuracao.SeqAlunoHistorico = seqAlunoHistorico;
                apuracao.Comparecimento = item.Comparecimento;
                lancamentoNotaViewModel.Apuracoes.Add(apuracao);
            }

            if (lancamentoNotaViewModel.Apuracoes.SMCAny()
            && lancamentoNotaViewModel.Apuracoes.All(a => !a.Comparecimento)
            && lancamentoNotaViewModel.Apuracoes.Count == AplicacaoAvaliacaoService.BuscarQuantidadeAvaliacoesAlunoPorOrigemTurma(seqAlunoHistorico, lancamentoNotaViewModel.SeqOrigemAvaliacao))
            {
                return "*";
            }

            return SomarApuracoes(lancamentoNotaViewModel).ToString();
        }

        private short? CalcularTotalTurmaAluno(LancamentoNotaViewModel model)
        {
            // RN_APR_044 Gravação de histórico escolar para turmas com avaliação parcial
            // Nota final: interpretar fórmula associada a turma, por enquanto utilizar a seguinte fórmula:
            /*
             * - se aluno com nota de realiavação:    
             *   ((soma de notas das avaliações de todas a divisões que o aluno estiver matriculado) + (soma de avaliação golbal - se houver) + nota de reavaliação )/2
             * - se aluno sem nota de realiavação:
             *   (soma de notas das avaliações de todas a divisões que o aluno estiver matriculado) + (soma de avaliação golbal - se houver)
             * 
             * A nota final deve ser arredondada para valor inteiro
             * 
             * */
            var somaReavaliacaoComGlobais = SomarApuracoes(model);
            if (somaReavaliacaoComGlobais.HasValue)
            {
                decimal calculos = (somaReavaliacaoComGlobais.GetValueOrDefault() + model.TotalParcial.GetValueOrDefault()) / 2;
                return Convert.ToInt16(Math.Round(calculos, 0, MidpointRounding.AwayFromZero));
            }
            else
            {
                return model.TotalParcial.HasValue ? (short?)Convert.ToInt16(Math.Round(model.TotalParcial.Value, 0, MidpointRounding.AwayFromZero)) : null;
            }
        }

        private SituacaoHistoricoEscolar CalcularSituacaoFinalAluno(long seqOrigemAvaliacao, long seqAlunoHistorico, short? notaTotal)
        {
            long? seqProfessor = HttpContext.GetEntityLogOn(FILTER.PROFESSOR)?.Value;
            SituacaoHistoricoEscolar retorno = HistoricoEscolarService.CalcularSituacaofinalLancamentoNotaParcial(seqOrigemAvaliacao, seqAlunoHistorico, notaTotal, new List<long>() { seqProfessor.GetValueOrDefault() });
            return retorno;
        }

        private ContentResult HandleErrorAngular(Exception ex)
        {
            var result = new
            {
                ex.Message,
                ErrorStack = ex.ToString()
            };
            return JsonResultAngular(result, HttpStatusCode.InternalServerError);
        }

        private ContentResult JsonResultAngular(object data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
            var json = JsonConvert.SerializeObject(data, settings);
            Response.StatusCode = (int)statusCode;
            return Content(json, "application/json");
        }

        private static decimal? SomarApuracoes(LancamentoNotaViewModel model)
        {
            decimal? retorno = null;

            foreach (var item in model.Apuracoes)
            {
                if (item.Nota != null || !item.Comparecimento)
                {
                    retorno = (retorno ?? 0) + (item.Nota ?? 0);
                }
            }

            return retorno;
        }
    }
}