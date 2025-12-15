using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class IngressanteFormacaoEspecificaFilterSpecification : SMCSpecification<IngressanteFormacaoEspecifica>
    {
        public long? SeqIngressante { get; set; }

        public override Expression<Func<IngressanteFormacaoEspecifica, bool>> SatisfiedBy()
        {
            AddExpression(SeqIngressante, a => a.SeqIngressante == SeqIngressante);
           
            return GetExpression();
        }
    }
}
