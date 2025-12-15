using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.TUR.Specifications
{
    public class TurmaHistoricoFechamentoDiarioFilterSpecification : SMCSpecification<TurmaHistoricoFechamentoDiario>
    {
        public long? Seq { get; set; }

        public long? SeqTurma { get; set; }

        public override Expression<Func<TurmaHistoricoFechamentoDiario, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqTurma, p => p.SeqTurma == this.SeqTurma);

            return GetExpression();
        }
    }
}
