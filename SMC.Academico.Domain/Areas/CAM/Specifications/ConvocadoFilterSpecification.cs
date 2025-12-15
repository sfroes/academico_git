using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ConvocadoFilterSpecification : SMCSpecification<Convocado>
    {
        public long? SeqInscricaoGPI { get; set; }

        public long? SeqChamada { get; set; }

        public override Expression<Func<Convocado, bool>> SatisfiedBy()
        {
            AddExpression(SeqInscricaoGPI, x => x.SeqInscricaoGpi == this.SeqInscricaoGPI);
            AddExpression(SeqChamada, x => x.SeqChamada == this.SeqChamada);
            return GetExpression();
        }
    }
}