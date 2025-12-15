using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.TUR.Specifications
{
    public class TurmaCicloLetivoCursoOfertaLocalidadeTurnoFilterSpecification : SMCSpecification<Turma>
    {
        public long SeqCicloLetivo { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long? SeqTurno { get; set; }

        public override Expression<Func<Turma, bool>> SatisfiedBy()
        {
            AddExpression(p => p.SeqCicloLetivoInicio == this.SeqCicloLetivo);
            AddExpression(p => p.ConfiguracoesComponente.Any(c => c.RestricoesTurmaMatriz.Any(r => r.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade == this.SeqCursoOfertaLocalidade)));
            AddExpression(this.SeqTurno, p => p.ConfiguracoesComponente.Any(c => c.RestricoesTurmaMatriz.Any(r => r.CursoOfertaLocalidadeTurno.SeqTurno == this.SeqTurno)));

            return GetExpression();
        }
    }
}
