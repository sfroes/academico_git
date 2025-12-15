using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class NivelEnsinoFolhaSpecification : SMCSpecification<NivelEnsino>
    {
        public override Expression<Func<NivelEnsino, bool>> SatisfiedBy()
        {
            AddExpression(p => p.NiveisEnsinoFilhos == null || p.NiveisEnsinoFilhos.Count == 0);

            return GetExpression();
        }
    }
}
