using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.CustomFilters
{
    /// <summary>
    /// RNG_USG_005 - Filtro por Nível de Ensino
    /// </summary>
    public class IngressanteEntidadeResponsavelCustomFilter : SMCCustomFilter<Ingressante>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<Ingressante, bool>> SatisfiedBy()
        {
            return x => SeqsHierarquias.Contains(x.EntidadeResponsavel.HierarquiasEntidades.FirstOrDefault().Seq);
        }
    }
}