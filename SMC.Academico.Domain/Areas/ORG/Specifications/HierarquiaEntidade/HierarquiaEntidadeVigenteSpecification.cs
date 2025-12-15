using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class HierarquiaEntidadeVigenteSpecification : SMCSpecification<HierarquiaEntidade>
    {
        public override Expression<Func<HierarquiaEntidade, bool>> SatisfiedBy()
        {
            //Obtendo a data "sem a hora"
            var today = DateTime.Today;

            AddExpression(p => today >= p.DataInicioVigencia);
            AddExpression(p => !p.DataFimVigencia.HasValue || today <= p.DataFimVigencia.Value);

            return GetExpression();
        }
    }
}