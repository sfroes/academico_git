using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class InstituicaoNivelHierarquiasClassificacaoFilterSpecification : SMCSpecification<InstituicaoNivelHierarquiaClassificacao>
    {
        public long SeqNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelHierarquiaClassificacao, bool>> SatisfiedBy()
        { 
            AddExpression(w => w.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);

            return GetExpression();
        }
    }
}