using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class RegimeLetivoFilterSpecification : SMCSpecification<RegimeLetivo>
    {
        public long? Seq { get; set; }

        public override Expression<Func<RegimeLetivo, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);

            return GetExpression();
        }
    }
}
