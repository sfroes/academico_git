using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class CurriculoCursoOfertaGrupoFilterSpecification : SMCSpecification<CurriculoCursoOfertaGrupo>
    {
        public long? Seq { get; set; }

        public long? SeqCurriculoCursoOferta { get; set; }

        public long? SeqGrupoCurricular { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public override Expression<Func<CurriculoCursoOfertaGrupo, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.SeqCurriculoCursoOferta, p => this.SeqCurriculoCursoOferta.Value == p.SeqCurriculoCursoOferta);
            AddExpression(this.SeqGrupoCurricular, p => this.SeqGrupoCurricular.Value == p.SeqGrupoCurricular);
            AddExpression(this.SeqComponenteCurricular, p => p.GrupoCurricular.ComponentesCurriculares.Any(a => a.SeqComponenteCurricular == this.SeqComponenteCurricular));

            return GetExpression();
        }
    }
}