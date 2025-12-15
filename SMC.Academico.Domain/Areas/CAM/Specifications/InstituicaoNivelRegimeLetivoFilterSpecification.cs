using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class InstituicaoNivelRegimeLetivoFilterSpecification : SMCSpecification<InstituicaoNivelRegimeLetivo>
    {
        public long? SeqRegimeLetivo { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelRegimeLetivo, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqRegimeLetivo, p => p.SeqRegimeLetivo == this.SeqRegimeLetivo.Value);
            AddExpression(this.SeqInstituicaoNivel, p => p.SeqInstituicaoNivel == this.SeqInstituicaoNivel.Value);
            AddExpression(this.SeqNivelEnsino, p => p.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino.Value);

            return GetExpression();
        }
    }
}
