using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.CustomFilters
{
    public class ColaboradorEntidadeResponsavelCustomFilter : SMCCustomFilter<Colaborador>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<Colaborador, bool>> SatisfiedBy()
        {
            return x => x.Vinculos.Any(a => SeqsHierarquias.Contains(a.EntidadeVinculo.HierarquiasEntidades.FirstOrDefault().Seq));
        }
    }
}