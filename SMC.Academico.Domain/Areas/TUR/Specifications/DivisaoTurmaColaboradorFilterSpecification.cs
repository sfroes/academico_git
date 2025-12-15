using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.TUR.Specifications
{
    public class DivisaoTurmaColaboradorFilterSpecification : SMCSpecification<DivisaoTurmaColaborador>
    {
        public long? Seq { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long? SeqColaborador { get; set; }

        public List<long> SeqsColaboradorDiferente { get; set; }

        public override Expression<Func<DivisaoTurmaColaborador, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqDivisaoTurma, p => p.SeqDivisaoTurma == this.SeqDivisaoTurma);
            AddExpression(this.SeqColaborador, p => p.SeqColaborador == this.SeqColaborador);
            AddExpression(this.SeqsColaboradorDiferente, p => !SeqsColaboradorDiferente.Contains(p.SeqColaborador));
            return GetExpression();
        }
    }
}
