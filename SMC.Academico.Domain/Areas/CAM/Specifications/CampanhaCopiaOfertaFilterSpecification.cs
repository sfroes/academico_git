using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaCopiaOfertaFilterSpecification : SMCSpecification<CampanhaOferta>
    {
        public long? SeqCampanhaOrigem { get; set; }
        public List<string> DesconsiderarTokensTipoOferta { get; set; }

        public override Expression<Func<CampanhaOferta, bool>> SatisfiedBy()
        {
            AddExpression(SeqCampanhaOrigem, x => x.SeqCampanha == SeqCampanhaOrigem);
            AddExpression(DesconsiderarTokensTipoOferta, x => !DesconsiderarTokensTipoOferta.Contains(x.TipoOferta.Token));

            return GetExpression();
        }
    }
}