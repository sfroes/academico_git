using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class PlanoEstudoItemFilterSpecification : SMCSpecification<PlanoEstudoItem>
    {
        public long? Seq { get; set; }

        public List<long> SeqsPlanoEstudoItem { get; set; }

        public long? SeqPlanoEstudo { get; set; }

        public long? SeqPlanoEstudoDiferente { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public List<long?> SeqsComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqAluno { get; set; }

        public long? SeqAlunoHistorico { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public List<long> SeqsDivisoesTurma { get; set; }

        public bool? SomenteTurma { get; set; }

        public bool? PlanoEstudoAtual { get; set; }

        public long? SeqColaborador { get; set; }

        public bool? ApenasMatriculado { get; set; }

        public bool? OrientacaoTurma { get; set; }

        public List<long> SeqsAlunosHistoricos { get; set; }

        public List<long> SeqsCicloLetivos { get; set; }

        public long? SeqTurma { get; set; }

        public List<string> ListaTokensTipoSituacaoMaticula { get; set; }

        public bool? VinculoAtivo { get; set; }

        public long? SeqOrigemAvaliacaoTurma { get; set; }

        public long? SeqOrigemAvaliacaoDivisaoTurma { get; set; }

        public override Expression<Func<PlanoEstudoItem, bool>> SatisfiedBy()
        {
            var tokensMatriculado = new string[] { TOKENS_SITUACAO_MATRICULA.MATRICULADO, TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE, TOKENS_SITUACAO_MATRICULA.FORMADO };

            var hoje = DateTime.Now.Date;

            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqPlanoEstudo, p => p.SeqPlanoEstudo == this.SeqPlanoEstudo.Value);
            AddExpression(this.SeqPlanoEstudoDiferente, p => p.SeqPlanoEstudo != this.SeqPlanoEstudoDiferente.Value);
            AddExpression(this.SeqAluno, p => p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno == this.SeqAluno);
            AddExpression(this.SomenteTurma, p => p.SeqDivisaoTurma.HasValue == this.SomenteTurma);
            AddExpression(this.SeqDivisaoTurma, p => p.SeqDivisaoTurma == this.SeqDivisaoTurma);
            AddExpression(this.SeqsDivisoesTurma, p => p.SeqDivisaoTurma.HasValue && SeqsDivisoesTurma.Contains(p.SeqDivisaoTurma.Value));
            AddExpression(this.PlanoEstudoAtual, p => p.PlanoEstudo.Atual == this.PlanoEstudoAtual);
            AddExpression(this.SeqColaborador, p => p.Orientacao.OrientacoesColaborador.Any(a => a.SeqColaborador == SeqColaborador));
            AddExpression(this.OrientacaoTurma, p => p.Orientacao.TipoOrientacao.OrientacaoTurma == OrientacaoTurma);
            AddExpression(this.SeqCicloLetivo, p => p.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqCicloLetivo == this.SeqCicloLetivo);
            AddExpression(this.SeqAlunoHistorico, p => p.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqAlunoHistorico == this.SeqAlunoHistorico);
            AddExpression(this.ApenasMatriculado, p => tokensMatriculado.Contains(p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistoricoSituacao.OrderByDescending(a => a.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Now && !s.DataExclusao.HasValue).SituacaoMatricula.Token));
            AddExpression(this.SeqsCicloLetivos, p => this.SeqsCicloLetivos.Contains(p.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqCicloLetivo));
            AddExpression(this.SeqsAlunosHistoricos, p => this.SeqsAlunosHistoricos.Contains(p.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqAlunoHistorico));
            AddExpression(this.SeqConfiguracaoComponente, p => p.SeqConfiguracaoComponente == this.SeqConfiguracaoComponente);
            AddExpression(this.SeqComponenteCurricular, p => p.ConfiguracaoComponente.SeqComponenteCurricular == this.SeqComponenteCurricular);
            AddExpression(this.SeqTurma, p => p.DivisaoTurma.SeqTurma == this.SeqTurma);
            AddExpression(this.ListaTokensTipoSituacaoMaticula, p => ListaTokensTipoSituacaoMaticula.Contains(p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistoricoSituacao.OrderByDescending(a => a.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Now && !s.DataExclusao.HasValue).SituacaoMatricula.TipoSituacaoMatricula.Token));
            AddExpression(this.SeqsPlanoEstudoItem, p => this.SeqsPlanoEstudoItem.Contains(p.Seq));
            AddExpression(this.SeqsComponenteCurricular, p => this.SeqsComponenteCurricular.Contains(p.ConfiguracaoComponente.SeqComponenteCurricular));
            AddExpression(this.SeqOrigemAvaliacaoTurma, p => this.SeqOrigemAvaliacaoTurma == p.DivisaoTurma.Turma.SeqOrigemAvaliacao);
            AddExpression(this.SeqOrigemAvaliacaoDivisaoTurma, p => this.SeqOrigemAvaliacaoDivisaoTurma == p.DivisaoTurma.SeqOrigemAvaliacao);
            AddExpression(this.SeqComponenteCurricularAssunto, p => p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqComponenteCurricularAssunto == this.SeqComponenteCurricularAssunto);

            //Esta deficinição para professor com vinculo ativo na instituição
            if (this.VinculoAtivo.HasValue && this.VinculoAtivo.Value)
            {
                AddExpression(this.VinculoAtivo, p => p.Orientacao.OrientacoesColaborador.Any() &&
                             (p.Orientacao.OrientacoesColaborador.Any(s => !s.Colaborador.Professores.Any()) || p.Orientacao.OrientacoesColaborador.Any(s => s.Colaborador.Professores.Any(w => w.SituacaoProfessor == SituacaoProfessor.Normal))));

            }
            else if (this.VinculoAtivo.HasValue && !this.VinculoAtivo.Value)
            {
                AddExpression(this.VinculoAtivo, p => p.Orientacao.OrientacoesColaborador.SelectMany(s => s.Colaborador.Professores).Any(w => w.SituacaoProfessor != SituacaoProfessor.Normal)
                                                      && p.Orientacao.OrientacoesColaborador.SelectMany(s => s.Colaborador.Professores).Any());
            }

            return GetExpression();
        }
    }
}