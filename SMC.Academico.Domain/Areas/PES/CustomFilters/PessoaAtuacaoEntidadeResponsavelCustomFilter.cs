using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.CustomFilters
{
    /// <summary>
    /// RNG_USG_005 - Filtro por Entidade Responsável
    /// </summary>
    public class PessoaAtuacaoEntidadeResponsavelCustomFilter : SMCCustomFilter<PessoaAtuacao>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<PessoaAtuacao, bool>> SatisfiedBy()
        {
            return x => SeqsHierarquias.Contains((x as Aluno).Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.HierarquiasEntidades.FirstOrDefault().Seq)
                     || SeqsHierarquias.Contains((x as Ingressante).EntidadeResponsavel.HierarquiasEntidades.FirstOrDefault().Seq)
                     || (x as Colaborador).Vinculos.Any(a => SeqsHierarquias.Contains(a.EntidadeVinculo.HierarquiasEntidades.FirstOrDefault().Seq));
        }
    }
}