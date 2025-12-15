using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class InstituicaoNivelCriterioAprovacaoFilterSpecification : SMCSpecification<InstituicaoNivelCriterioAprovacao>
    {
        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public override Expression<Func<InstituicaoNivelCriterioAprovacao, bool>> SatisfiedBy()
        { 
            AddExpression(SeqNivelEnsino, w => w.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(SeqInstituicaoNivel, w => w.SeqInstituicaoNivel == this.SeqInstituicaoNivel);

            return GetExpression();
        }
    }
}
