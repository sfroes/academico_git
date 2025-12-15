using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.CustomFilters
{
    public class MatrizCurricularOfertaCustomFilter : SMCCustomFilter<MatrizCurricularOferta>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<MatrizCurricularOferta, bool>> SatisfiedBy()
        {
            return x => x.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.Any(y => SeqsHierarquias.Contains(y.Seq));
        }
    }
}