using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.CustomFilters
{
    public class DeclaracaoGenericaCustomFilter : SMCCustomFilter<DeclaracaoGenerica>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<DeclaracaoGenerica, bool>> SatisfiedBy()
        {
            return x => SeqsHierarquias.Contains((x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.HierarquiasEntidades.FirstOrDefault().Seq);
        }
    }
}
