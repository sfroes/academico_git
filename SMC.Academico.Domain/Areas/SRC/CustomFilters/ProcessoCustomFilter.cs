using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.CustomFilters
{
    public class ProcessoCustomFilter : SMCCustomFilter<Processo>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<Processo, bool>> SatisfiedBy()
        {
            return x => x.UnidadesResponsaveis.Any(u => SeqsHierarquias.Contains(u.EntidadeResponsavel.HierarquiasEntidades.FirstOrDefault().Seq) &&
                                                        u.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel);
        }
    }
}