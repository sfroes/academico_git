using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class TipoServicoFilterSpecification : SMCSpecification<TipoServico>
    {
        public string Token { get; set; }

        public override Expression<Func<TipoServico, bool>> SatisfiedBy()
        {
            AddExpression(Token, w => w.Token == this.Token);
          
            return GetExpression();
        }
    }
}
