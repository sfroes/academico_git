using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaOfertaItemFilterSpecification : SMCSpecification<CampanhaOfertaItem>
    {
        public long? SeqCampanhaOferta { get; set; }

        public override Expression<Func<CampanhaOfertaItem, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqCampanhaOferta, p => p.SeqCampanhaOferta == this.SeqCampanhaOferta);

            return GetExpression();
        }
    }
}