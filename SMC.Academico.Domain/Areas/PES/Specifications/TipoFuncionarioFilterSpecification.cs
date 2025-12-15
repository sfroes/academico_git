using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class TipoFuncionarioFilterSpecification : SMCSpecification<TipoFuncionario>
    {
        public string Token { get; set; }

        public List<string> Tokens { get; set; }


        public override Expression<Func<TipoFuncionario, bool>> SatisfiedBy()
        {
            AddExpression(Token, a => a.Token == Token);
            AddExpression(Tokens, a => Tokens.Contains(a.Token));

            return GetExpression();
        }
    }
}
