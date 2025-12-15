using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.Specifications
{
    public class InstituicaoNivelTipoAtividadeColaboradorFilterSpecification : SMCSpecification<InstituicaoNivelTipoAtividadeColaborador>
    {
        public long? SeqNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelTipoAtividadeColaborador, bool>> SatisfiedBy()
        {
            AddExpression(SeqNivelEnsino, x => x.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);

            return GetExpression();
        }
    }
}