using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.CustomFilters
{
    public class TrabalhoAcademicoCustomFilter : SMCCustomFilter<TrabalhoAcademico>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<TrabalhoAcademico, bool>> SatisfiedBy()
        {
            return x => x.Autores.Any(a => SeqsHierarquias.Contains(a.Aluno.Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.HierarquiasEntidades.FirstOrDefault().Seq));
        }
    }
}