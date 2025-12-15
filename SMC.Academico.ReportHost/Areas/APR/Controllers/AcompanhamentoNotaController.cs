using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc;
using SMC.Academico.UI.Mvc.Areas.APR.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.ReportHost.Areas.APR.Apis
{
    public class AcompanhamentoNotaController : SMCControllerBase
    {
        #region [ Services ]

        private ITurmaService TurmaService => Create<ITurmaService>();
        private IOrigemAvaliacaoService OrigemAvaliacaoService => Create<IOrigemAvaliacaoService>();
        private IApuracaoAvaliacaoService ApuracaoAvaliacaoService => Create<IApuracaoAvaliacaoService>();
        private IDivisaoTurmaService DivisaoTurmaService => Create<IDivisaoTurmaService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        private IHistoricoEscolarService HistoricoEscolarService => Create<IHistoricoEscolarService>();

        #endregion [ Services ]

        /// <summary>
        /// Gera o realorio de acompanhamento de notas
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem avaliação</param>
        /// <param name="seqProfessor">Sequencial do professor caso venha do portal de professor caso venha do adminitrativo será zero</param>
        /// <param name="administrativo">Indicativo que o relaótio foi solicitado pela secretraria</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GerarRelatorio(SMCEncryptedLong seqOrigemAvaliacao, long seqProfessorLogado = 0,  bool administrativo = false)
        {
            
            RelatorioAcompanhamentoNotaViewModel retorno = PreencherModelo(seqOrigemAvaliacao, seqProfessorLogado, administrativo);
            var options = new GridOptions(orientation: PDFPageOrientation.Landscape)
            {
                HeaderHtml = RenderViewActionToString(viewName: "_CabecalhoRelatorio", action: "CabecalhoRelatorio"),
                FooterHtml = RenderViewActionToString(viewName: "_RodapeRelatorio", action: "RodapeRelatorio"),
                PageMargins = new MarginInfo() { Left = 10, Right = 10, Top = 37 }
            };
            return RenderPdfView("GerarRelatorio", model: retorno, options: options);
        }

        public ActionResult CabecalhoRelatorio()
        {
            CabecalhoNotaViewModel cabecalho = new CabecalhoNotaViewModel();
            var seqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq;
            var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(seqInstituicaoEnsino);
            string imagemBase = instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData);
            cabecalho.ImagemCabecalho = string.Format("data:image/png;base64,{0}", imagemBase);
            cabecalho.titulo = "Relatório de Acompanhamento de Nota";
            cabecalho.nomeInstituicao = instituicao.Nome;
            return PartialView("_CabecalhoRelatorio", cabecalho);
        }

        public ActionResult RodapeRelatorio()
        {
            return PartialView("_RodapeRelatorio");
        }

        private RelatorioAcompanhamentoNotaViewModel PreencherModelo(SMCEncryptedLong seqOrigemAvaliacao, long seqProfessorLogado, bool administrativo)
        {
            OrigemAvaliacaoData origemAvaliacao = OrigemAvaliacaoService.BuscarOrigemAvaliacao(seqOrigemAvaliacao);
            RelatorioAcompanhamentoNotaViewModel retorno = new RelatorioAcompanhamentoNotaViewModel();

            if (origemAvaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                retorno.Turma = new TurmaNotaViewModel();
                List<TurmaCabecalhoResponsavelData> professores = OrigemAvaliacaoService.BuscarProfessoresPorOrigemAvaliacao(seqOrigemAvaliacao);
                long seqProfessor = ValidarProfessorLogado(seqOrigemAvaliacao, professores, seqProfessorLogado);
                LancamentoAvaliacaoData result = ApuracaoAvaliacaoService.BuscarLancamentosAvaliacao(seqOrigemAvaliacao, seqProfessor, administrativo);
                List<AlunosNotaViewModel> alunos = new List<AlunosNotaViewModel>();
                long seqTurma = TurmaService.BuscarSeqTurmaPorOrigemAvaliacao(seqOrigemAvaliacao);
                List<long> seqsOrigensAvaliacaoTuma = DivisaoTurmaService.BuscarDivisaoTurmaLista(seqTurma).Select(s => s.SeqOrigemAvaliacao).ToList();
                List<DivisaoTurmaNotaViewModel> divisoesTurma = new List<DivisaoTurmaNotaViewModel>();
                foreach (var seqOrigensAvaliacaoTuma in seqsOrigensAvaliacaoTuma)
                {
                    divisoesTurma.Add(PrencherDivisao(seqOrigensAvaliacaoTuma, retorno, seqProfessorLogado, administrativo));
                }

                var avaliacoes = result.AplicacaoAvaliacoes.OrderBy(o => o.Seq)
                                           .Select(s =>
                                                    new AvaliacoesCabecalhoViewModel
                                                    {
                                                        Sigla = s.Sigla,
                                                        Descricao = s.Avaliacao.Descricao,
                                                    }).ToList();

                var alunosDivisao = divisoesTurma.SelectMany(sm => sm.Alunos).ToList();
                foreach (var aluno in result.Alunos.OrderBy(o => o.Nome))
                {
                    var seqAlunoHistoricoDivisao = alunosDivisao.Where(w => w.SeqAlunoHistorico == aluno.SeqAlunoHistorico)?.FirstOrDefault()?.SeqAlunoHistorico;
                    alunos.Add(new AlunosNotaViewModel()
                    {
                        Apuracoes = aluno.Apuracoes.OrderBy(o => o.SeqAplicacaoAvaliacao).Select(s => new ApuracoesNotaViewModel() { Nota = s.Nota }).ToList(),
                        Nome = aluno.Nome,
                        RA = aluno.NumeroRegistroAcademico.ToString(),
                        Faltas = aluno.Faltas,
                        SeqAlunoHistorico = aluno.SeqAlunoHistorico,
                        ExibirFaltas = origemAvaliacao.ApurarFrequencia.GetValueOrDefault(),
                        TotalParcial = divisoesTurma.SelectMany(s => s.Alunos).Where(w => w.SeqAlunoHistorico == seqAlunoHistoricoDivisao).SelectMany(sm => sm.Apuracoes).Sum(su => su.Nota),
                        TotalNotasAvaliacao = aluno.Apuracoes.OrderBy(o => o.SeqAplicacaoAvaliacao).Sum(su => su.Nota),
                        AptoCalcularSituacaoFinal = divisoesTurma.SelectMany(s => s.Alunos).FirstOrDefault(f => f.SeqAlunoHistorico == seqAlunoHistoricoDivisao)?.AptoCalcularSituacaoFinal ?? false,
                        SituacaoHistoricoEscolcar = aluno.DescricaoSituacaoFinal
                    });
                }

                foreach (var aluno in alunos)
                {
                    if (aluno.AptoCalcularSituacaoFinal && string.IsNullOrEmpty(aluno.SituacaoHistoricoEscolcar))
                    {
                        (string situacaoFinal, decimal? totalFinal) = CalcularSituacaoFinal(aluno, seqOrigemAvaliacao, seqProfessor);
                        aluno.Situacao = situacaoFinal;
                        aluno.TotalFinal = totalFinal;
                    }
                    else if(!string.IsNullOrEmpty(aluno.SituacaoHistoricoEscolcar))
                    {
                        aluno.Situacao = aluno.SituacaoHistoricoEscolcar;
                        aluno.TotalFinal = result.Alunos.FirstOrDefault(f => f.SeqAlunoHistorico == aluno.SeqAlunoHistorico).Nota;
                    }
                }

                retorno.Turma.TotalPontos = origemAvaliacao.NotaMaxima;
                retorno.Turma.Avaliacoes = avaliacoes;
                retorno.TemTurma = true;
                retorno.Turma.DescricaoTurma = result.DescricaoOrigemAvaliacao;
                retorno.Turma.Professores = string.Join(", ", professores.Select(s => s.NomeColaborador));
                retorno.Turma.DivisoesTuma = divisoesTurma;
                retorno.Turma.ExibirFaltas = origemAvaliacao.ApurarFrequencia.GetValueOrDefault();
                retorno.Turma.Alunos = alunos;
                retorno.Turma.DescricaoCicloLetivo = OrigemAvaliacaoService.BusacarDescricaoCicloLetivoPorOrigemAvaliacao(seqOrigemAvaliacao);
                retorno.Turma.SituacaoDiario = result.DiarioFechado ? "Fechado" : "Aberto";
            }
            else
            {
                retorno.DivisaoTurma = PrencherDivisao(seqOrigemAvaliacao, retorno,  seqProfessorLogado, administrativo);
            }

            return retorno;
        }

        private DivisaoTurmaNotaViewModel PrencherDivisao(long seqOrigemAvaliacao, RelatorioAcompanhamentoNotaViewModel retorno, long seqProfessorLogado, bool administrativo)
        {
            OrigemAvaliacaoData origemAvaliacao = OrigemAvaliacaoService.BuscarOrigemAvaliacao(seqOrigemAvaliacao);
            retorno.DivisaoTurma = new DivisaoTurmaNotaViewModel();
            var professores = OrigemAvaliacaoService.BuscarProfessoresPorOrigemAvaliacao(origemAvaliacao.Seq);
            long seqProfessor = ValidarProfessorLogado(seqOrigemAvaliacao, professores, seqProfessorLogado);
            var result = ApuracaoAvaliacaoService.BuscarLancamentosAvaliacao(origemAvaliacao.Seq, seqProfessor, administrativo);
            List<AlunosNotaViewModel> alunos = new List<AlunosNotaViewModel>();
            var totalAvliacoesTurminha = result.AplicacaoAvaliacoes.Select(s => s.Avaliacao).Sum(su => su.Valor);
            var turminhaValida = totalAvliacoesTurminha == origemAvaliacao.NotaMaxima;
            var avaliacoes = result.AplicacaoAvaliacoes.OrderBy(o => o.Seq)
                                                       .Select(s =>
                                                                new AvaliacoesCabecalhoViewModel
                                                                {
                                                                    Sigla = s.Sigla,
                                                                    Descricao = s.Avaliacao.Descricao,
                                                                }).ToList();

            foreach (var aluno in result.Alunos.OrderBy(o => o.Nome))
            {
                alunos.Add(new AlunosNotaViewModel()
                {
                    Apuracoes = aluno.Apuracoes.OrderBy(o => o.SeqAplicacaoAvaliacao).Select(s => new ApuracoesNotaViewModel() { Nota = s.Nota }).ToList(),
                    Nome = aluno.Nome,
                    RA = aluno.NumeroRegistroAcademico.ToString(),
                    TotalDT = aluno.Apuracoes.OrderBy(o => o.SeqAplicacaoAvaliacao).Sum(s => s.Nota),
                    ExibirFaltas = origemAvaliacao.ApurarFrequencia.GetValueOrDefault(),
                    Faltas = aluno.Faltas,
                    SeqAlunoHistorico = aluno.SeqAlunoHistorico,
                    FezTodasAvaliacoes = aluno.Apuracoes.OrderBy(o => o.SeqAplicacaoAvaliacao).All(a => a.Nota != null)
                });
            }

            foreach (var aluno in alunos)
            {
                aluno.AptoCalcularSituacaoFinal = aluno.FezTodasAvaliacoes == turminhaValida;
            }

            retorno.DivisaoTurma.Alunos = alunos;
            var descricao = result.DescricaoOrigemAvaliacao.Replace("<br />", "|");
            retorno.DivisaoTurma.DescricaoTurma = descricao.Split('|')[0];
            retorno.DivisaoTurma.CodificacaoDescricaoTurma = descricao.Split('|')[1];
            retorno.DivisaoTurma.Professores = string.Join(", ", professores.Select(s => s.NomeColaborador));
            retorno.DivisaoTurma.Avaliacoes = avaliacoes;
            retorno.DivisaoTurma.TotalPontos = (short)result.AplicacaoAvaliacoes.Sum(s => s.Avaliacao.Valor);
            retorno.DivisaoTurma.ExibirFaltas = origemAvaliacao.ApurarFrequencia.GetValueOrDefault();
            retorno.DivisaoTurma.DescricaoCicloLetivo = OrigemAvaliacaoService.BusacarDescricaoCicloLetivoPorOrigemAvaliacao(seqOrigemAvaliacao);

            return retorno.DivisaoTurma;
        }

        private (string, decimal?) CalcularSituacaoFinal(AlunosNotaViewModel aluno, long seqOrigemAvaliacao, long seqProfessor)
        {
            string situacaoFinal = "";

            if (aluno.TotalNotasAvaliacao == 0 && aluno.TotalParcial > 0)
            {
                aluno.TotalFinal = aluno.TotalParcial;
            }
            else if (aluno.TotalParcial == 0 && aluno.TotalNotasAvaliacao > 0)
            {
                aluno.TotalFinal = aluno.TotalNotasAvaliacao;
            }
            else if (aluno.TotalParcial > 0 && aluno.TotalNotasAvaliacao > 0)
            {
                aluno.TotalFinal = (aluno.TotalParcial.GetValueOrDefault() + aluno.TotalNotasAvaliacao.GetValueOrDefault()) / 2;
            }

            if(aluno.TotalParcial > 0 || aluno.TotalNotasAvaliacao > 0)
            {
                SituacaoHistoricoEscolar situacaoAtual = HistoricoEscolarService
                 .CalcularSituacaofinalLancamentoNotaParcial(seqOrigemAvaliacao, aluno.SeqAlunoHistorico, (short)aluno.TotalFinal, new List<long>() { seqProfessor });
                situacaoFinal = SMCEnumHelper.GetDescription(situacaoAtual);
            }
            aluno.TotalFinal = Decimal.Parse(Math.Round(aluno.TotalFinal.GetValueOrDefault(), 0, MidpointRounding.AwayFromZero).ToString("0.00"));
            return (situacaoFinal, aluno.TotalFinal);
        }

        /// <summary>
        /// Validação usada para verificação e caso seja turma orientação garante que o sequencial seja o do professor orientador
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequncial da origem de avaliação</param>
        /// <param name="professores">Lista de professores da origem avaliação</param>
        /// <param name="seqProfessorLogado">Sequencial do professor logado</param>
        /// <returns></returns>
        private long ValidarProfessorLogado(long seqOrigemAvaliacao, List<TurmaCabecalhoResponsavelData> professores, long seqProfessorLogado)
        {
            long retorno = seqProfessorLogado;
            
            if (retorno == 0)
            {
                retorno = professores.Any() ? professores.FirstOrDefault().SeqColaborador : 0;
            }          

            return retorno;
        }
    }
}