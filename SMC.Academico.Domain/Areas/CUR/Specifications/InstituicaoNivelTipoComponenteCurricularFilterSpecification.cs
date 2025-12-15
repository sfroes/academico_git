using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class InstituicaoNivelTipoComponenteCurricularFilterSpecification : SMCSpecification<InstituicaoNivelTipoComponenteCurricular>
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoComponenteCurricular { get; set; }

        public bool? PermiteCadastroDispensa { get; set; }

        public TipoGestaoDivisaoComponente? TipoGestaoDivisaoComponente { get; set; }

        public List<long> SeqsTiposComponentesCurriculares { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelTipoComponenteCurricular, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.SeqInstituicaoNivel, p => this.SeqInstituicaoNivel.Value == p.SeqInstituicaoNivel);
            AddExpression(this.SeqNivelEnsino, p => this.SeqNivelEnsino.Value == p.InstituicaoNivel.SeqNivelEnsino);
            AddExpression(this.SeqInstituicaoEnsino, p => p.InstituicaoNivel.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(this.SeqTipoComponenteCurricular, p => this.SeqTipoComponenteCurricular.Value == p.SeqTipoComponenteCurricular);
            AddExpression(this.PermiteCadastroDispensa, p => this.PermiteCadastroDispensa.Value == p.PermiteCadastroDispensa);
            AddExpression(this.TipoGestaoDivisaoComponente, p => p.TiposDivisaoComponente.Any(t => t.TipoDivisaoComponente.TipoGestaoDivisaoComponente == this.TipoGestaoDivisaoComponente.Value));

            if (SeqsTiposComponentesCurriculares != null)
                AddExpression(p => SeqsTiposComponentesCurriculares.Contains(p.SeqTipoComponenteCurricular));

            return GetExpression();
        }
    }
}