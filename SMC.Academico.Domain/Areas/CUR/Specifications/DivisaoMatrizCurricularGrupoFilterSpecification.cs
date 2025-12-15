using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class DivisaoMatrizCurricularGrupoFilterSpecification : SMCSpecification<DivisaoMatrizCurricularGrupo>
    {
        public long? Seq { get; set; }

        public long? SeqCurriculoCursoOfertaGrupo { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public long? SeqDivisaoMatrizCurricular { get; set; }

        public long? SeqGrupoCurricular { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public override Expression<Func<DivisaoMatrizCurricularGrupo, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, w => this.Seq.Value == w.Seq);
            AddExpression(this.SeqCurriculoCursoOfertaGrupo, w => this.SeqCurriculoCursoOfertaGrupo.Value == w.SeqCurriculoCursoOfertaGrupo);
            AddExpression(this.SeqMatrizCurricular, w => this.SeqMatrizCurricular == w.DivisaoMatrizCurricular.SeqMatrizCurricular);
            AddExpression(this.SeqDivisaoMatrizCurricular, w => this.SeqDivisaoMatrizCurricular.Value == w.SeqDivisaoMatrizCurricular);
            AddExpression(this.SeqGrupoCurricular, w => this.SeqGrupoCurricular == w.CurriculoCursoOfertaGrupo.SeqGrupoCurricular);
            AddExpression(this.SeqComponenteCurricular, w => w.CurriculoCursoOfertaGrupo.GrupoCurricular.ComponentesCurriculares.Any(a => a.SeqComponenteCurricular == this.SeqComponenteCurricular));

            return GetExpression();
        }
    }
}