using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class MembroBancaExaminadoraFilterSpecification : SMCSpecification<MembroBancaExaminadora>
    {

        public long? Seq { get; set; }
        public long? SeqAplicacaoAvaliacao { get; set; }

        public override Expression<Func<MembroBancaExaminadora, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq);
            AddExpression(SeqAplicacaoAvaliacao, w => w.SeqAplicacaoAvaliacao == SeqAplicacaoAvaliacao);

            return GetExpression();
        }
    }
}