using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class EntregaOnlineFilterSpecification : SMCSpecification<EntregaOnline>
    {
 
        public long? SeqAplicacaoAvaliacao { get; set; }

        public override Expression<Func<EntregaOnline, bool>> SatisfiedBy()
        {
            AddExpression(SeqAplicacaoAvaliacao, x => x.SeqAplicacaoAvaliacao == SeqAplicacaoAvaliacao);

            return GetExpression();
        }
    }
}