using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class CriterioAprovacaoCursoFilterSpecification : SMCSpecification<CriterioAprovacaoCurso>
    {
        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public long[] SeqsCursoOfertaLocalidadeTurno { get; set; }

        public override Expression<Func<CriterioAprovacaoCurso, bool>> SatisfiedBy()
        {
            AddExpression(SeqCursoOfertaLocalidadeTurno, w => w.SeqCursoOfertaLocalidadeTurno == SeqCursoOfertaLocalidadeTurno);
            AddExpression(SeqsCursoOfertaLocalidadeTurno, w => SeqsCursoOfertaLocalidadeTurno.Contains(w.SeqCursoOfertaLocalidadeTurno));

            return GetExpression();
        }
    }
}