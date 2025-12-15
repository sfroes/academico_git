using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class InstituicaoNivelCondicaoObrigatoriedadeFilterSpecification : SMCSpecification<InstituicaoNivelCondicaoObrigatoriedade>
    {
        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public override Expression<Func<InstituicaoNivelCondicaoObrigatoriedade, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqNivelEnsino, p => p.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(this.SeqInstituicaoNivel, p => p.SeqInstituicaoNivel == this.SeqInstituicaoNivel);

            return GetExpression();
        }
    }
}