using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class InstituicaoNivelFormaIngressoFilterSpecification : SMCSpecification<InstituicaoNivelFormaIngresso>
    {
        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public override Expression<Func<InstituicaoNivelFormaIngresso, bool>> SatisfiedBy()
        {
            AddExpression(SeqNivelEnsino, p => p.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(SeqsNivelEnsino, p => SeqsNivelEnsino.Contains(p.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino));
            AddExpression(SeqTipoVinculoAluno, p => p.InstituicaoNivelTipoVinculoAluno.SeqTipoVinculoAluno == SeqTipoVinculoAluno);
            AddExpression(SeqTipoProcessoSeletivo, p => p.TiposProcessoSeletivo.Any(a => a.SeqTipoProcessoSeletivo == SeqTipoProcessoSeletivo));

            return GetExpression();
        }
    }
}