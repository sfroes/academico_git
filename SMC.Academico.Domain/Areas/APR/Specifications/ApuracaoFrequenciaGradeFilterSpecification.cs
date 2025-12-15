using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class ApuracaoFrequenciaGradeFilterSpecification : SMCSpecification<ApuracaoFrequenciaGrade>
    {
        public long? SeqDivisaoTurma { get; set; }
        public List<long> Seqs { get; set; }
        public long? SeqAluno { get; set; }
        public List<long> SeqsDivisaoTurma { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public DateTime? FimPeriodo { get; set; }
        public long? SeqTurma { get; set; }
        public long? SeqEventoAula { get; set; }

        public override Expression<Func<ApuracaoFrequenciaGrade, bool>> SatisfiedBy()
        {
            AddExpression(SeqDivisaoTurma, a => a.EventoAula.SeqDivisaoTurma == SeqDivisaoTurma);
            AddExpression(Seqs, a => Seqs.Contains(a.Seq));
            AddExpression(SeqAluno, a => a.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno == SeqAluno);
            AddExpression(InicioPeriodo, a => a.EventoAula.Data >= InicioPeriodo);
            AddExpression(FimPeriodo, a => a.EventoAula.Data <= FimPeriodo);
            AddExpression(SeqsDivisaoTurma, a => SeqsDivisaoTurma.Contains(a.EventoAula.SeqDivisaoTurma));
            AddExpression(SeqTurma, a => a.EventoAula.DivisaoTurma.SeqTurma == SeqTurma);
            AddExpression(SeqEventoAula, a => a.SeqEventoAula == SeqEventoAula);

            return GetExpression();
        }
    }
}