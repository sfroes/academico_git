using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ServicoTaxaFilterSpecification : SMCSpecification<ServicoTaxa>
    {
        public long? SeqServico { get; set; }

        public override Expression<Func<ServicoTaxa, bool>> SatisfiedBy()
        {
            AddExpression(SeqServico, w => w.SeqServico == this.SeqServico);
          
            return GetExpression();
        }
    }
}
