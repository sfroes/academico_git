using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class ApuracaoFormacaoFilterSpecification : SMCSpecification<ApuracaoFormacao>
    {
        public long? SeqAlunoFormacao { get; set; }

        public override Expression<Func<ApuracaoFormacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqAlunoFormacao, x => x.SeqAlunoFormacao == this.SeqAlunoFormacao.Value);
         
            return GetExpression();
        }
    }
}
