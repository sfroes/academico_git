using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ConvocadoOfertaFilterSpecification : SMCSpecification<ConvocadoOferta>
    {
        public long? SeqInscricaoOfertaGpi { get; set; }

        public override Expression<Func<ConvocadoOferta, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqInscricaoOfertaGpi, p => p.SeqInscricaoOfertaGpi == SeqInscricaoOfertaGpi.Value);

            return GetExpression();
        }
    }
}