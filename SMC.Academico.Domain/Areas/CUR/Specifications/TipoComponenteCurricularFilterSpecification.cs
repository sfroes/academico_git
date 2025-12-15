using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class TipoComponenteCurricularFilterSpecification : SMCSpecification<TipoComponenteCurricular>
    {
        public long? Seq { get; set; }

        public override Expression<Func<TipoComponenteCurricular, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);

            return GetExpression();
        }
    }
}