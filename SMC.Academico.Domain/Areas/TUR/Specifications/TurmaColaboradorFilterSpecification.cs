using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.TUR.Specifications
{
    public class TurmaColaboradorFilterSpecification : SMCSpecification<TurmaColaborador>
    {
        public long? Seq { get; set; }

        public long? SeqTurma { get; set; }
        
        public override Expression<Func<TurmaColaborador, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqTurma, p => p.SeqTurma == this.SeqTurma);
           
            return GetExpression();
        }
    }
}
