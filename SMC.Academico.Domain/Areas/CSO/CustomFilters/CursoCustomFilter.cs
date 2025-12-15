using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CSO.CustomFilters
{
    public class CursoCustomFilter : SMCCustomFilter<Curso>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<Curso, bool>> SatisfiedBy()
        {
            return x => x.HierarquiasEntidades.Any(y => SeqsHierarquias.Contains(y.Seq));
        }
    }
}
