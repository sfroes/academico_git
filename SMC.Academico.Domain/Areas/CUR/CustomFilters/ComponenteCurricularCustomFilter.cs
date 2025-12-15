using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.CustomFilters
{
    public class ComponenteCurricularCustomFilter : SMCCustomFilter<ComponenteCurricular>
    {
        [SMCFilterParameter(FILTER.NIVEL_ENSINO, true)]
        public long[] SeqsNiveisEnsino { get; set; }

        public override Expression<Func<ComponenteCurricular, bool>> SatisfiedBy()
        {
            return x => x.NiveisEnsino.Any(y => SeqsNiveisEnsino.Contains(y.SeqNivelEnsino));
        }
    }
}