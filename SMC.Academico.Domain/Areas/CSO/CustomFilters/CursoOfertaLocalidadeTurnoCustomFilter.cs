using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.CustomFilters
{
    public class CursoOfertaLocalidadeTurnoCustomFilter : SMCCustomFilter<CursoOfertaLocalidadeTurno>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<CursoOfertaLocalidadeTurno, bool>> SatisfiedBy()
        {
            return x => x.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.Any(y => SeqsHierarquias.Contains(y.Seq));
        }
    }
}