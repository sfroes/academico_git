using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class DivisaoCurricularFilterSpecification : SMCSpecification<DivisaoCurricular>
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqNivelEnsino { get; set; }
        
        public override Expression<Func<DivisaoCurricular, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.SeqInstituicaoEnsino, p => this.SeqInstituicaoEnsino.Value == p.SeqInstituicaoEnsino);
            AddExpression(this.SeqNivelEnsino, p => this.SeqNivelEnsino.Value == p.SeqNivelEnsino);

            return GetExpression();
        }
    }
}
