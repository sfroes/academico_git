using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class ExigirVigenciasTermoIntercambioFilterSpecification : SMCSpecification<InstituicaoNivelTipoTermoIntercambio>
    {
        public long SeqTipoTermoIntercambio { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelTipoTermoIntercambio, bool>> SatisfiedBy()
        {
            AddExpression(w => w.SeqTipoTermoIntercambio == SeqTipoTermoIntercambio);
            AddExpression(w => w.ExigePeriodoIntercambioTermo);
            AddExpression(w => w.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(w => w.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);

            return GetExpression();
        }
    }
}
