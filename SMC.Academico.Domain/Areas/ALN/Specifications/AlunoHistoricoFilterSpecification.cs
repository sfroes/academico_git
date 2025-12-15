using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class AlunoHistoricoFilterSpecification : SMCSpecification<AlunoHistorico>
    {
        public long? SeqAluno { get; set; }

        public bool? Atual { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long? SeqTurma { get; set; }

        public bool? PlanoEstudoAtual { get; set; }

        public long? SeqIngressante { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public List<long> SeqsPessoaAtuacao { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqCurso { get; set; }

        public TipoVinculoAlunoFinanceiro? TipoVinculoAlunoFinanceiro { get; set; }

        public List<long> Seqs { get; set; }

        public override Expression<Func<AlunoHistorico, bool>> SatisfiedBy()
        {
            AddExpression(SeqAluno, a => a.SeqAluno == SeqAluno);
            AddExpression(Atual, a => a.Atual == Atual.Value);
            AddExpression(SeqDivisaoTurma, a => a.HistoricosCicloLetivo.Any(b => b.PlanosEstudo.Any(c => c.Itens.Any(d => d.SeqDivisaoTurma == SeqDivisaoTurma.Value))));
            AddExpression(SeqTurma, a => a.HistoricosCicloLetivo.Any(b => b.PlanosEstudo.Any(c => c.Itens.Any(d => d.DivisaoTurma.SeqTurma == SeqTurma.Value))));
            AddExpression(PlanoEstudoAtual, a => a.HistoricosCicloLetivo.Any(b => b.PlanosEstudo.Any(c => c.Atual == PlanoEstudoAtual.Value)));
            AddExpression(SeqIngressante, a => a.SeqIngressante == SeqIngressante);
            AddExpression(SeqSolicitacaoServico, a => a.SeqSolicitacaoServico == SeqSolicitacaoServico);
            AddExpression(SeqCicloLetivo, a => a.HistoricosCicloLetivo.Any(h => h.SeqCicloLetivo == SeqCicloLetivo));
            AddExpression(SeqsPessoaAtuacao, a => SeqsPessoaAtuacao.Contains(a.SeqAluno));
            AddExpression(SeqCursoOfertaLocalidade, a => a.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade == SeqCursoOfertaLocalidade);
            AddExpression(SeqCursoOferta, a => a.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.SeqCursoOferta == SeqCursoOferta);
            AddExpression(SeqCurso, a => a.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso == SeqCurso);
            AddExpression(TipoVinculoAlunoFinanceiro, a => a.Aluno.TipoVinculoAluno.TipoVinculoAlunoFinanceiro == TipoVinculoAlunoFinanceiro);
            AddExpression(Seqs, a => Seqs.Contains(a.Seq));

            return GetExpression();
        }
    }
}