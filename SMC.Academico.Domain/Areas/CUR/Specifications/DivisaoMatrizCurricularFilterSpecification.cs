using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class DivisaoMatrizCurricularFilterSpecification : SMCSpecification<DivisaoMatrizCurricular>
    {
        public long? Seq { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public short? NumeroPreRequisito { get; set; }

        public short? NumeroCoRequisito { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public override Expression<Func<DivisaoMatrizCurricular, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqMatrizCurricular, p => p.SeqMatrizCurricular == this.SeqMatrizCurricular);
            AddExpression(this.NumeroPreRequisito, p => p.DivisaoCurricularItem.Numero < this.NumeroPreRequisito);
            AddExpression(this.NumeroCoRequisito, p => p.DivisaoCurricularItem.Numero == this.NumeroCoRequisito);
            AddExpression(this.SeqComponenteCurricular, p => p.ConfiguracoesComponentes.Any(a => a.GrupoCurricularComponente.SeqComponenteCurricular == this.SeqComponenteCurricular));

            return GetExpression();
        }
    }
}
