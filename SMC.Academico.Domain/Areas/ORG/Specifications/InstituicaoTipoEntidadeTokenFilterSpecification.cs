using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoTipoEntidadeTokenFilterSpecification : SMCSpecification<InstituicaoTipoEntidade>
    {
        public InstituicaoTipoEntidadeTokenFilterSpecification()
        {
            this.SetOrderBy(x => x.TipoEntidade.Descricao);
        }

        public string[] Tokens;

        public override Expression<Func<InstituicaoTipoEntidade, bool>> SatisfiedBy()
        {
            AddExpression(p => !this.Tokens.Contains(p.TipoEntidade.Token));

            return GetExpression();
        }
    }
}