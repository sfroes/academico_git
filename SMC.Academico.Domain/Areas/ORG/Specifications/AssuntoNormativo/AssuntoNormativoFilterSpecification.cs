using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class AssuntoNormativoFilterSpecification : SMCSpecification<AssuntoNormativo>
    {
        public bool? Ativo { get; set; }

        public override System.Linq.Expressions.Expression<Func<AssuntoNormativo, bool>> SatisfiedBy()
        {
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo);

            return GetExpression();

        }
    }
}