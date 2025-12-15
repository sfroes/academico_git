using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.CustomFilters
{
    public class CursoUnidadeCustomFilter : SMCCustomFilter<CursoUnidade>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<CursoUnidade, bool>> SatisfiedBy()
        {
            return x => x.HierarquiasEntidades.Any(y => SeqsHierarquias.Contains(y.Seq));
        }
    }
}
