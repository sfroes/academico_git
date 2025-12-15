using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class InstituicaoNivelTipoMembroBancaFilterSpecification : SMCSpecification<InstituicaoNivelTipoMembroBanca>
    {
        public long? SeqNivelEnsino { get; set; }

        public long[] SeqsCursoOfertaLocalidadeTurno { get; set; }

        public override Expression<Func<InstituicaoNivelTipoMembroBanca, bool>> SatisfiedBy()
        {
            AddExpression(SeqNivelEnsino, w => w.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);

            return GetExpression();
        }
    }
}