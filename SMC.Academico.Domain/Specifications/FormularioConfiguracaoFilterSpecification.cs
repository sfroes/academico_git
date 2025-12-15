using SMC.Academico.Domain.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Specifications
{
    public class FormularioConfiguracaoFilterSpecification : SMCSpecification<FormularioConfiguracao>
    {
        public string Token { get; set; }

        public override Expression<Func<FormularioConfiguracao, bool>> SatisfiedBy()
        {
            return p => p.Token == this.Token;
        }
    }
}
