using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class OrgaoRegistroFilterSpecification : SMCSpecification<OrgaoRegistro>
    {
        public string Sigla { get; set; }

        public override Expression<Func<OrgaoRegistro, bool>> SatisfiedBy()
        {
            AddExpression(Sigla, x => x.Sigla == this.Sigla);

            return GetExpression();
        }
    }
}
