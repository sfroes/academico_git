using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.CustomFilters
{
    public class CicloLetivoCustomFilter : SMCCustomFilter<CicloLetivo>
    {
        [SMCFilterParameter(FILTER.NIVEL_ENSINO, true)]
        public long[] SeqsNiveisEnsino { get; set; }

        public override Expression<Func<CicloLetivo, bool>> SatisfiedBy()
        {
            return x => x.NiveisEnsino.Any(y => SeqsNiveisEnsino.Contains(y.Seq));
        }
    }
}