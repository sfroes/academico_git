using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.CustomFilters
{
    public class OrientacaoPessoaAtuacaoCustomFilter : SMCCustomFilter<OrientacaoPessoaAtuacao>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<OrientacaoPessoaAtuacao, bool>> SatisfiedBy()
        {
            return x => SeqsHierarquias.Contains((x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.HierarquiasEntidades.FirstOrDefault().Seq)
                     || SeqsHierarquias.Contains((x.PessoaAtuacao as Ingressante).EntidadeResponsavel.HierarquiasEntidades.FirstOrDefault().Seq);
        }
    }
}
