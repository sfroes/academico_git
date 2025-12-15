using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class AulaFilterSpecification : SMCSpecification<Aula>
    {
        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public override Expression<Func<Aula, bool>> SatisfiedBy()
        {
            AddExpression(SeqDivisaoTurma, x => x.SeqDivisaoTurma == SeqDivisaoTurma);
            if (DataInicio.HasValue && DataFim.HasValue)
                AddExpression(x => x.DataAula >= DataInicio && x.DataAula <= DataFim);
            else if (DataInicio.HasValue)
                AddExpression(x => x.DataAula == DataInicio);

            return GetExpression();
        }
    }
}