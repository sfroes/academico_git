using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoOfertaLocalidadeFormacaoFilterSpecification : SMCSpecification<CursoOfertaLocalidadeFormacao>
    {
        public long? SeqCursoLocalidade { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqCurso { get; set; }

        public override Expression<Func<CursoOfertaLocalidadeFormacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqCursoLocalidade, w => w.SeqCursoOfertaLocalidade == this.SeqCursoLocalidade);
            AddExpression(SeqFormacaoEspecifica, w => w.SeqFormacaoEspecifica == this.SeqFormacaoEspecifica);
            AddExpression(SeqCurso, w => w.CursoOfertaLocalidade.CursoOferta.SeqCurso == this.SeqCurso);

            return GetExpression();
        }
    }
}
