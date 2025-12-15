using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class InstituicaoNivelTurnoFilterSpecification : SMCSpecification<InstituicaoNivelTurno>
    {
        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicao { get; set; }

        public override Expression<Func<InstituicaoNivelTurno, bool>> SatisfiedBy()
        {
            AddExpression(SeqInstituicaoNivel, w => w.SeqInstituicaoNivel == SeqInstituicaoNivel);
            AddExpression(SeqNivelEnsino, w => this.SeqNivelEnsino == w.InstituicaoNivel.SeqNivelEnsino);
            AddExpression(SeqInstituicao, w => this.SeqInstituicao == w.InstituicaoNivel.SeqInstituicaoEnsino);

            return GetExpression();
        }
    }
}
