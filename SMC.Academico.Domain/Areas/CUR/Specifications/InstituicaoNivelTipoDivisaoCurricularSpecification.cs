using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class InstituicaoNivelTipoDivisaoCurricularSpecification : SMCSpecification<InstituicaoNivelTipoDivisaoCurricular>
    {
        public long? seqInstituicaoNivel { get; set; }

        public long? seqTipoDivisaoCurricular { get; set; }

        public long? seqNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelTipoDivisaoCurricular, bool>> SatisfiedBy()
        {
            AddExpression(this.seqInstituicaoNivel, p => p.SeqInstituicaoNivel == this.seqInstituicaoNivel.Value);
            AddExpression(this.seqTipoDivisaoCurricular, p => p.SeqTipoDivisaoCurricular == this.seqTipoDivisaoCurricular.Value);
            AddExpression(this.seqNivelEnsino, p => p.InstituicaoNivel.SeqNivelEnsino == this.seqNivelEnsino.Value);

            return GetExpression();
        }
    }
}
