using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class TesteSpecification : SMCSpecification<InstituicaoEnsino>
    {
       
        public override Expression<Func<InstituicaoEnsino, bool>> SatisfiedBy()
        {
            return x => x.Seq == 1;
        }
    }
}