using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class TipoApostilamentoFilterSpecification : SMCSpecification<TipoApostilamento>
    {
        public string TokenDiferente { get; set; }

        public bool? Ativo { get; set; }

        public override Expression<Func<TipoApostilamento, bool>> SatisfiedBy()
        {
            AddExpression(TokenDiferente, x => x.Token != this.TokenDiferente);
            AddExpression(Ativo, x => x.Ativo == this.Ativo);

            return GetExpression();
        }
    }
}
