using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ConfiguracaoProcessoCursoFilterSpecification : SMCSpecification<ConfiguracaoProcessoCurso>
    {      
        public long[] SeqsCursoOfertaLocalidadeTurno { get; set; }

        public long? SeqProcesso { get; set; }

        public override Expression<Func<ConfiguracaoProcessoCurso, bool>> SatisfiedBy()
        {
            AddExpression(SeqsCursoOfertaLocalidadeTurno, a => SeqsCursoOfertaLocalidadeTurno.Contains(a.SeqCursoOfertaLocalidadeTurno));        
            AddExpression(SeqProcesso, x => x.ConfiguracaoProcesso.SeqProcesso == SeqProcesso.Value);

            return GetExpression();
        }
    }
}
