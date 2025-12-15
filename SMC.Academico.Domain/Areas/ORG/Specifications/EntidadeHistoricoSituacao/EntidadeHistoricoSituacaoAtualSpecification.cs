using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class EntidadeHistoricoSituacaoAtualSpecification : SMCSpecification<EntidadeHistoricoSituacao>
    {
        public override Expression<Func<EntidadeHistoricoSituacao, bool>> SatisfiedBy()
        {
            DateTime agora = DateTime.Today;
            AddExpression(p => agora >= p.DataInicio && (!p.DataFim.HasValue || agora <= p.DataFim.Value));

            return GetExpression();
        }
    }
}