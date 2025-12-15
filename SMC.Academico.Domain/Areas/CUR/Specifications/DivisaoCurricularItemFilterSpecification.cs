using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class DivisaoCurricularItemFilterSpecification : SMCSpecification<DivisaoCurricularItem>
    {
        public long SeqDivisaoCurricular { get; set; }

        public override Expression<Func<DivisaoCurricularItem, bool>> SatisfiedBy()
        {
            AddExpression(p => p.SeqDivisaoCurricular == this.SeqDivisaoCurricular);

            return GetExpression();
        }
    }
}
