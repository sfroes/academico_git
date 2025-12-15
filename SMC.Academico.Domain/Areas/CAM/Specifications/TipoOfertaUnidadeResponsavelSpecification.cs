using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class TipoOfertaUnidadeResponsavelSpecification : SMCSpecification<TipoOfertaUnidadeResponsavel>
    {
        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqTipoOferta { get; set; }

        public override Expression<Func<TipoOfertaUnidadeResponsavel, bool>> SatisfiedBy()
        {
            AddExpression(SeqEntidadeResponsavel, x => x.SeqEntidadeResponsavel == SeqEntidadeResponsavel);
            AddExpression(SeqTipoOferta, x => x.SeqTipoOferta == SeqTipoOferta);
            return GetExpression();
        }
    }
}