using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class CriterioAprovacaoFilterSpecification : SMCSpecification<CriterioAprovacao>
    {
        public long? SeqEscalaApuracao { get; set; }

        public bool? SemEscalaApuracao { get; set; }

        public override Expression<Func<CriterioAprovacao, bool>> SatisfiedBy()
        { 
            AddExpression(SeqEscalaApuracao, w => w.SeqEscalaApuracao == this.SeqEscalaApuracao);
            AddExpression(SemEscalaApuracao, w => w.SeqEscalaApuracao.HasValue == !this.SemEscalaApuracao);

            return GetExpression();
        }
    }
}
