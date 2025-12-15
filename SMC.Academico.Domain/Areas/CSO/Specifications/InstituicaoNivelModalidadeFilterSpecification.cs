using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class InstituicaoNivelModalidadeFilterSpecification : SMCSpecification<InstituicaoNivelModalidade>
    {
        public long? Seq { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqInstituicao { get; set; }

        public long? SeqModalidade { get; set; }

        public override Expression<Func<InstituicaoNivelModalidade, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq);
            AddExpression(SeqNivelEnsino, w => w.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(SeqModalidade, w => w.SeqModalidade == this.SeqModalidade);
            AddExpression(SeqInstituicaoNivel, w => w.SeqInstituicaoNivel == this.SeqInstituicaoNivel);
            AddExpression(SeqInstituicao, w => w.InstituicaoNivel.SeqInstituicaoEnsino == this.SeqInstituicao);

            return GetExpression();
        }
    }
}