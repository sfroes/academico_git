using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class InstituicaoNivelTipoDivisaoComponenteFilterSpecification : SMCSpecification<InstituicaoNivelTipoDivisaoComponente>
    {
        public long? Seq { get; set; }

        public long? SeqTipoTrabalho { get; set; }

        public long? SeqTipoDivisaoComponente { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoComponenteCurricular { get; set; }

        public long? SeqInstituicaoNivelTipoComponenteCurricular{ get; set; }

        public override Expression<Func<InstituicaoNivelTipoDivisaoComponente, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, i => i.Seq == this.Seq.Value);
            AddExpression(this.SeqTipoTrabalho, i => i.SeqTipoTrabalho == this.SeqTipoTrabalho.Value);
            AddExpression(this.SeqTipoDivisaoComponente, i => i.SeqTipoDivisaoComponente == this.SeqTipoDivisaoComponente.Value);
            AddExpression(this.SeqNivelEnsino, i => i.InstituicaoNivelTipoComponenteCurricular.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino.Value);
            AddExpression(this.SeqInstituicaoEnsino, i => i.InstituicaoNivelTipoComponenteCurricular.InstituicaoNivel.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino.Value);
            AddExpression(this.SeqTipoComponenteCurricular, i => i.InstituicaoNivelTipoComponenteCurricular.SeqTipoComponenteCurricular == this.SeqTipoComponenteCurricular.Value);
            AddExpression(this.SeqInstituicaoNivelTipoComponenteCurricular, i => i.SeqInstituicaoNivelTipoComponenteCurricular == this.SeqInstituicaoNivelTipoComponenteCurricular.Value);

            return GetExpression();
        }
    }
}