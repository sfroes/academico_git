using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class UnidadeResponsavelGPISpecification : SMCSpecification<Entidade>
    {
        public override Expression<Func<Entidade, bool>> SatisfiedBy()
        {
            AddExpression(a => (a.SeqUnidadeResponsavelGpi.HasValue));

            return GetExpression();
        }
    }
}