using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class InstituicaoNivelTipoOfertaCursoFilterSpecification : SMCSpecification<InstituicaoNivelTipoOfertaCurso>
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoNivel { get; set; }              

        public long? SeqNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelTipoOfertaCurso, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(SeqInstituicaoNivel, w => this.SeqInstituicaoNivel.Value == w.SeqInstituicaoNivel);
            AddExpression(SeqNivelEnsino, w => this.SeqNivelEnsino.Value == w.InstituicaoNivel.SeqNivelEnsino); 

            return GetExpression();
        }

    }
}
