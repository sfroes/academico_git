using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.CustomFilters
{
    public class CampanhaOfertaCustomFilter : SMCCustomFilter<CampanhaOferta>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<CampanhaOferta, bool>> SatisfiedBy()
        {
            return x => x.Campanha.EntidadeResponsavel.HierarquiasEntidades.Any(y => SeqsHierarquias.Contains(y.Seq));
        }
    }
}