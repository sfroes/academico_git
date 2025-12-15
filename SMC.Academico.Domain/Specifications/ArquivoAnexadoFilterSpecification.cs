using SMC.Academico.Domain.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Specifications
{
    public class ArquivoAnexadoFilterSpecification : SMCSpecification<ArquivoAnexado>
    {
        public Guid? UidArquivo { get; set; }

        public override Expression<Func<ArquivoAnexado, bool>> SatisfiedBy()
        {
            AddExpression(UidArquivo, w => w.UidArquivo == this.UidArquivo);

            return GetExpression();
        }
    }
}
