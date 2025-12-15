using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class InstituicaoNivelEscalaApuracaoFilterSpecification : SMCSpecification<InstituicaoNivelEscalaApuracao>
    {
        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public List<long> SeqsNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelEscalaApuracao, bool>> SatisfiedBy()
        { 
            AddExpression(SeqNivelEnsino, w => w.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(SeqInstituicaoNivel, w => w.SeqInstituicaoNivel == this.SeqInstituicaoNivel);
            AddExpression(SeqsNivelEnsino, p => SeqsNivelEnsino.Contains(p.InstituicaoNivel.SeqNivelEnsino));

            return GetExpression();
        }
    }
}
