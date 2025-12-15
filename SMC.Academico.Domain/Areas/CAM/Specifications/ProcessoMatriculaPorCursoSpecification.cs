using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ProcessoMatriculaPorCursoSpecification : SMCSpecification<Processo>
    {
        public ProcessoMatriculaPorCursoSpecification(long seqProcesso, long seqCursoOfertaLocalidadeTurno, IEnumerable<long> seqVinculo)
        {
            SeqProcesso = seqProcesso;
            SeqCursoOfertaLocalidadeTurno = seqCursoOfertaLocalidadeTurno;
            SeqVinculo = seqVinculo;
        }

        public long SeqProcesso { get; set; }

        public IEnumerable<long> SeqVinculo { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public override Expression<Func<Processo, bool>> SatisfiedBy()
        {
            AddExpression(x => x.Seq == SeqProcesso);

            AddExpression(x => x.Configuracoes.Any(c => c.Cursos.Any(f => f.SeqCursoOfertaLocalidadeTurno == SeqCursoOfertaLocalidadeTurno)));

            AddExpression(x => SeqVinculo.All(n => x.Configuracoes.Any(c => c.TiposVinculoAluno.Any(f => f.SeqTipoVinculoAluno == n))));

            return GetExpression();
        }
    }
}
