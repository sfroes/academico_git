using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class OrientacaoColaboradorFilterSpecification : SMCSpecification<OrientacaoColaborador>
    {
        public long? Seq { get; set; }

        public long? SeqOrientacao { get; set; }

        public override Expression<Func<OrientacaoColaborador, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, a => a.Seq == this.Seq.Value);
            AddExpression(this.SeqOrientacao, a => a.SeqOrientacao == this.SeqOrientacao);

            return GetExpression();
        }
    }
}
