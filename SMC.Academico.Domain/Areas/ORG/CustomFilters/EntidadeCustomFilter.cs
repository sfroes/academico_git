using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.CustomFilters
{
    public class EntidadeCustomFilter : SMCCustomFilter<Entidade>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<Entidade, bool>> SatisfiedBy()
        {
            return x => x.HierarquiasEntidades.Any(y => SeqsHierarquias.Contains(y.Seq));
        }
    }
}
