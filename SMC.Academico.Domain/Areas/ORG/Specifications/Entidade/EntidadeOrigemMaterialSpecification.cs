using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class EntidadeOrigemMaterialSpecification : SMCSpecification<Entidade>
    {
        public List<long> SeqsEntidades { get; set; }

        public override System.Linq.Expressions.Expression<Func<Entidade, bool>> SatisfiedBy()
        {
            AddExpression(x => SeqsEntidades.Contains(x.Seq));
            AddExpression(x => x.SeqOrigemMaterial != null);

            return GetExpression();
        }
    }
}
