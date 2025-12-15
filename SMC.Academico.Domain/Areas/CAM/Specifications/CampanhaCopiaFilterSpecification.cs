using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaCopiaFilterSpecification : SMCSpecification<Campanha>
    {
        public long? SeqCampanhaOrigem { get; set; }

        public override Expression<Func<Campanha, bool>> SatisfiedBy()
        {
            AddExpression(x => x.Seq == SeqCampanhaOrigem);

            return GetExpression();
        }
    }
}