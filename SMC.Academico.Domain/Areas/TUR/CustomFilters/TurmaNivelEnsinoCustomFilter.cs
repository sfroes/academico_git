using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.TUR.CustomFilters
{
    public class TurmaNivelEnsinoCustomFilter : SMCCustomFilter<Turma>
    {
        [SMCFilterParameter(FILTER.NIVEL_ENSINO, true)]
        public long[] SeqsNiveisEnsino { get; set; }

        public override Expression<Func<Turma, bool>> SatisfiedBy()
        {           

            return p => p.ConfiguracoesComponente.Any(c => SeqsNiveisEnsino.Contains(c.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.SingleOrDefault(s => s.Responsavel).SeqNivelEnsino)); 
        }
    }
}
