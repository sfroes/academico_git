using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class ComponenteCurricularEmentaFilterSpecification : SMCSpecification<ComponenteCurricularEmenta>
    {
        public long? SeqComponenteCurricular { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public override Expression<Func<ComponenteCurricularEmenta, bool>> SatisfiedBy()
        {
            AddExpression(SeqComponenteCurricular, w => SeqComponenteCurricular.Value == w.SeqComponenteCurricular);
            AddExpression(DataInicio, w => DataInicio.Value >= w.DataInicio);
            AddExpression(DataFim, w => !w.DataFim.HasValue || DataFim.Value <= w.DataFim);

            return GetExpression();
        }
    }
}
