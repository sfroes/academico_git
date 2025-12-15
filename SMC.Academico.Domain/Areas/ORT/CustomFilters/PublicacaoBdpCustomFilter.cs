using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.CustomFilters
{
    public class PublicacaoBdpCustomFilter : SMCCustomFilter<PublicacaoBdp>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<PublicacaoBdp, bool>> SatisfiedBy()
        {
            return x => x.TrabalhoAcademico.Autores.Any(a => SeqsHierarquias.Contains(a.Aluno.Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.HierarquiasEntidades.FirstOrDefault().Seq));
        }
    }
}