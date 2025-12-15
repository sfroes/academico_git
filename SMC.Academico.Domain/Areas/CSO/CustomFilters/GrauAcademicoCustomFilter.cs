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
    public class GrauAcademicoCustomFilter : SMCCustomFilter<GrauAcademico>
    {
        [SMCFilterParameter(FILTER.NIVEL_ENSINO, true)]
        public long[] SeqsNiveisEnsino { get; set; }

        public override Expression<Func<GrauAcademico, bool>> SatisfiedBy()
        {
            return x => x.NiveisEnsino.Any(y => SeqsNiveisEnsino.Contains(y.Seq));
        }
    }
}