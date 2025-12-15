using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.CustomFilters
{
    public class ProcessoUnidadeResponsavelCustomFilter : SMCCustomFilter<ProcessoUnidadeResponsavel>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<ProcessoUnidadeResponsavel, bool>> SatisfiedBy()
        {
            return x => SeqsHierarquias.Contains(x.EntidadeResponsavel.HierarquiasEntidades.FirstOrDefault().Seq);
        }
    }
}
