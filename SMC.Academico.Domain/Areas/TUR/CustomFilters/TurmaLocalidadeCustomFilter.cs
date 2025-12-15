using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.TUR.CustomFilters
{
    public class TurmaLocalidadeCustomFilter : SMCCustomFilter<Turma>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<Turma, bool>> SatisfiedBy()
        {
            //Task 28881 Bug 28945 quando não tiver matriz tem que exibir
            return x => x.ConfiguracoesComponente.Any(
                        c => !c.RestricoesTurmaMatriz.Any() || c.RestricoesTurmaMatriz.Any(
                            a => !a.SeqMatrizCurricularOferta.HasValue || a.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.Any(
                                y => SeqsHierarquias.Contains(y.Seq))));
        }
    }   
}
