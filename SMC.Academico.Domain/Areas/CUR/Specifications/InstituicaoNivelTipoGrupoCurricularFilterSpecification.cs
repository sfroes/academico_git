using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class InstituicaoNivelTipoGrupoCurricularFilterSpecification : SMCSpecification<InstituicaoNivelTipoGrupoCurricular>
    {
        public long SeqNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelTipoGrupoCurricular, bool>> SatisfiedBy()
        {
            AddExpression(p => p.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);

            return GetExpression();
        }
    }
}
