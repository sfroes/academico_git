using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class InstituicaoNivelBeneficioFilterSpecification : SMCSpecification<InstituicaoNivelBeneficio>
    {
        public long? Seq { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqBeneficio { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public List<long> SeqsBeneficio { get; set; }

        public override Expression<Func<InstituicaoNivelBeneficio, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqNivelEnsino, p => p.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(this.SeqInstituicaoEnsino, p => p.InstituicaoNivel.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(this.SeqBeneficio, p => p.SeqBeneficio == this.SeqBeneficio);
            AddExpression(this.SeqInstituicaoNivel, p => p.SeqInstituicaoNivel == this.SeqInstituicaoNivel);
            AddExpression(this.SeqsBeneficio, p => this.SeqsBeneficio.Contains(p.SeqBeneficio));

            return GetExpression();
        }
    }
}