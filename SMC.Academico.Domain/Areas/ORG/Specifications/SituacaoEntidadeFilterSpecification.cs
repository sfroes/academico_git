using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class SituacaoEntidadeFilterSpecification : SMCSpecification<SituacaoEntidade>
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public override Expression<Func<SituacaoEntidade, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq.Value);
            AddExpression(this.Descricao, p => p.Descricao.Equals(this.Descricao));

            return GetExpression();
        }
    }
}