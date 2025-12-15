using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class ParceriaIntercambioInstituicaoExternaSpecification : SMCSpecification<ParceriaIntercambioInstituicaoExterna>
    {
        public long? SeqParceriaIntercambio { get; set; }

        public bool? Ativo { get; set; }

        public override Expression<Func<ParceriaIntercambioInstituicaoExterna, bool>> SatisfiedBy()
        { 
            AddExpression(SeqParceriaIntercambio, w => w.SeqParceriaIntercambio == this.SeqParceriaIntercambio);
            AddExpression(this.Ativo, a => a.Ativo == this.Ativo);

            return GetExpression();
        }
    }
}

 