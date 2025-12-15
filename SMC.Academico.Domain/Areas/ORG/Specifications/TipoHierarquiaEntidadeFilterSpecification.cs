using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class TipoHierarquiaEntidadeFilterSpecification : SMCSpecification<TipoHierarquiaEntidade>
    {
        public TipoVisao TipoVisao { get; set; }

        public override Expression<Func<TipoHierarquiaEntidade, bool>> SatisfiedBy()
        {
            AddExpression(p => this.TipoVisao == p.TipoVisao);

            return GetExpression();
        }
    }
}