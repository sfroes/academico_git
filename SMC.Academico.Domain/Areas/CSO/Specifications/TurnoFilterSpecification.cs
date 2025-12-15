using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class TurnoFilterSpecification : SMCSpecification<Turno>
    {
        public long? SeqCursoOferta { get; set; }

        public long? SeqLocalidade { get; set; }

        public override Expression<Func<Turno, bool>> SatisfiedBy()
        {
            AddExpression(SeqCursoOferta, w => w.CursoOfertaLocalidadesTurnos.Any(a => a.CursoOfertaLocalidade.SeqCursoOferta == SeqCursoOferta));
            AddExpression(SeqLocalidade, w => w.CursoOfertaLocalidadesTurnos.Any(a => a.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade == SeqLocalidade));

            return GetExpression();
        }
    }
}