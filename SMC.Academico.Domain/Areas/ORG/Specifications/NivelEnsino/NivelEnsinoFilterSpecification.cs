using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class NivelEnsinoFilterSpecification : SMCSpecification<NivelEnsino>
    {
        public string Token { get; set; }

        public override Expression<Func<NivelEnsino, bool>> SatisfiedBy()
        {
            AddExpression(this.Token, p => p.Token.Equals(this.Token));

            return GetExpression();
        }
    }
}
