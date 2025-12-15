using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class EntregaOnlineHistoricoSituacaoFilterSpecification : SMCSpecification<EntregaOnlineHistoricoSituacao>
    {
 
        public long? SeqEntregaOnline { get; set; }

        public override Expression<Func<EntregaOnlineHistoricoSituacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqEntregaOnline, x => x.SeqEntregaOnline == SeqEntregaOnline);

            return GetExpression();
        }
    }
}