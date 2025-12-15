using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.CustomFilters
{
    /// <summary>
    /// RNG_USG_004 - Filtro por Nível de Ensino
    /// </summary>
    public class PessoaAtuacaoNivelEnsinoCustomFilter : SMCCustomFilter<PessoaAtuacao>
    {
        [SMCFilterParameter(FILTER.NIVEL_ENSINO, true)]
        public long[] SeqsNiveisEnsino { get; set; }

        public override Expression<Func<PessoaAtuacao, bool>> SatisfiedBy()
        {
            return x => SeqsNiveisEnsino.Contains((x as Aluno).Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino)
                     || SeqsNiveisEnsino.Contains((x as Ingressante).SeqNivelEnsino);
        }
    }
}