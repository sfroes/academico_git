using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.CustomFilters
{
    public class CursoOfertaLocalidadeCustomFilter : SMCCustomFilter<CursoOfertaLocalidade>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<CursoOfertaLocalidade, bool>> SatisfiedBy()
        {
            return x => x.HierarquiasEntidades.Any(y => SeqsHierarquias.Contains(y.Seq));
        }
    }
}
