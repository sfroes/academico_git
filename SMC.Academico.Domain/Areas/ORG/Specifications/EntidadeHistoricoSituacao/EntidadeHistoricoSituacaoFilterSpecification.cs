using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class EntidadeHistoricoSituacaoFilterSpecification : SMCSpecification<EntidadeHistoricoSituacao>
    {
        public long? Seq { get; set; }

        public long? SeqEntidade { get; set; }

        public override Expression<Func<EntidadeHistoricoSituacao, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq == p.Seq);
            AddExpression(this.SeqEntidade, p => this.SeqEntidade == p.SeqEntidade);

            return GetExpression();
        }
    }
}
