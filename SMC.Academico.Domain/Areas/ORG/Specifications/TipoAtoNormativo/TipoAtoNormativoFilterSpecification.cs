using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class TipoAtoNormativoFilterSpecification : SMCSpecification<TipoAtoNormativo>
    {

        public bool? Ativo { get; set; }

        public override System.Linq.Expressions.Expression<Func<TipoAtoNormativo, bool>> SatisfiedBy()
        {
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo);

            return GetExpression();

        }
    }
}