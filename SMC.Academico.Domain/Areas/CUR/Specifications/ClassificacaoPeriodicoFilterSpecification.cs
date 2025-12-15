using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class ClassificacaoPeriodicoFilterSpecification : SMCSpecification<ClassificacaoPeriodico>
    {
        public long? Seq { get; set; }

        public bool? Atual { get; set; }

        public override Expression<Func<ClassificacaoPeriodico, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq == p.Seq);
            AddExpression(this.Atual, p => this.Atual == p.Atual);

            return GetExpression();
        }
    }
}