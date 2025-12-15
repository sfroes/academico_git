using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.TUR.Specifications
{
    public class DivisaoTurmaFilterSpecification : SMCSpecification<DivisaoTurma>
    {
        public long? Seq { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqOrigemMaterial { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }
        
        public long? SeqOrigemAvaliacaoTurma { get; set; }

        public long? SeqDivisaoComponente { get; set; }
        public List<long> Seqs { get; set; }

        public override Expression<Func<DivisaoTurma, bool>> SatisfiedBy()
        {
            AddExpression(Seq, p => p.Seq == Seq);
            AddExpression(SeqTurma, p => p.SeqTurma == SeqTurma);
            AddExpression(SeqOrigemMaterial, p => p.SeqOrigemMaterial == SeqOrigemMaterial.Value);
            AddExpression(SeqOrigemAvaliacao, p => p.SeqOrigemAvaliacao == SeqOrigemAvaliacao);
            AddExpression(SeqOrigemAvaliacaoTurma, p => p.Turma.SeqOrigemAvaliacao == SeqOrigemAvaliacaoTurma);
            AddExpression(SeqDivisaoComponente, p => p.SeqDivisaoComponente == SeqDivisaoComponente);
            AddExpression(Seqs, p => Seqs.Contains(p.Seq));

            return GetExpression();
        }
    }
}
