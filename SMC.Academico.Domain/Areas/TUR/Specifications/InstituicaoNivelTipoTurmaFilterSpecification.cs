using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.TUR.Specifications
{
    public class InstituicaoNivelTipoTurmaFilterSpecification : SMCSpecification<InstituicaoNivelTipoTurma>
    {
        public long? SeqTipoTurma { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long[] SeqsNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelTipoTurma, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqTipoTurma, p => p.SeqTipoTurma == this.SeqTipoTurma);
            AddExpression(this.SeqInstituicaoNivel, p => p.SeqInstituicaoNivel == this.SeqInstituicaoNivel);
            AddExpression(this.SeqInstituicaoEnsino, p => p.InstituicaoNivel.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(this.SeqNivelEnsino, p => p.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(this.SeqsNivelEnsino, p => SeqsNivelEnsino.Contains(p.InstituicaoNivel.SeqNivelEnsino));

            return GetExpression();
        }
    }
}
