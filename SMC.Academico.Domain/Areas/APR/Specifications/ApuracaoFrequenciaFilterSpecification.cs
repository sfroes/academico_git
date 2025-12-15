using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class ApuracaoFrequenciaFilterSpecification : SMCSpecification<ApuracaoFrequencia>
    {
        public long? SeqDivisaoTurma { get; set; }

        public long? SeqAlunoHistoricoCicloLetivo { get; set; }

        public long? SeqApuracaoFrequenciaDiferente { get; set; }

        public long? SeqAula { get; set; }

        public override Expression<Func<ApuracaoFrequencia, bool>> SatisfiedBy()
        {
            AddExpression(SeqDivisaoTurma, x => x.Aula.SeqDivisaoTurma == SeqDivisaoTurma);
            AddExpression(SeqAlunoHistoricoCicloLetivo, x => x.SeqAlunoHistoricoCicloLetivo == SeqAlunoHistoricoCicloLetivo);
            AddExpression(SeqApuracaoFrequenciaDiferente, x => x.Seq != SeqApuracaoFrequenciaDiferente);
            AddExpression(SeqAula, x => x.SeqAula == SeqAula);

            return GetExpression();
        }
    }
}