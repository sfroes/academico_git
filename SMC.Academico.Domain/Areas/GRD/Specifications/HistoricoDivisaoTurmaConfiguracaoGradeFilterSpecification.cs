using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.GRD.Specifications
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeFilterSpecification : SMCSpecification<HistoricoDivisaoTurmaConfiguracaoGrade>
    {
        public long? SeqDivisaoTurma { get; set; }

        public DateTime? DataInicio { get; set; }

        public override Expression<Func<HistoricoDivisaoTurmaConfiguracaoGrade, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqDivisaoTurma, a => a.SeqDivisaoTurma == this.SeqDivisaoTurma);
            AddExpression(this.DataInicio, a => a.DataInicio == this.DataInicio);

            return GetExpression();
        }
    }
}
