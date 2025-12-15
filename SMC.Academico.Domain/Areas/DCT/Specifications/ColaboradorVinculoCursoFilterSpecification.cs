using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.Specifications
{
    public class ColaboradorVinculoCursoFilterSpecification : SMCSpecification<ColaboradorVinculoCurso>
    {
        public long? SeqCursoOfertaLocalidade { get; set; }

        public TipoAtividadeColaborador? TipoAtividadeColaborador { get; set; }

        //public bool? TipoEntidadePermiteVinculo { get; set; }

        public long[] SeqsEntidadesVinculo { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long[] SeqsCursoOferta { get; set; }

        public long[] SeqsCursoOfertaLocalidade { get; set; }

        public long? SeqColaboradorVinculo { get; set; }

        public override Expression<Func<ColaboradorVinculoCurso, bool>> SatisfiedBy()
        {
            AddExpression(SeqCursoOfertaLocalidade, c => SeqCursoOfertaLocalidade == c.SeqCursoOfertaLocalidade);
            AddExpression(TipoAtividadeColaborador, c => c.Atividades.Any(at => at.TipoAtividadeColaborador == TipoAtividadeColaborador));
            //AddExpression(TipoEntidadePermiteVinculo, c => c.ColaboradorVinculo.EntidadeVinculo.TipoEntidade.InstituicoesTipoEntidade.Any(aa => aa.PermiteVinculoColaborador == TipoEntidadePermiteVinculo));
            AddExpression(SeqsEntidadesVinculo, c => SeqsEntidadesVinculo.Contains(c.SeqCursoOfertaLocalidade));
            AddExpression(SeqCursoOferta, c => c.CursoOfertaLocalidade.SeqCursoOferta == SeqCursoOferta);
            AddExpression(SeqsCursoOferta, c => SeqsCursoOferta.Contains(c.CursoOfertaLocalidade.SeqCursoOferta));
            AddExpression(SeqsCursoOfertaLocalidade, c => SeqsCursoOfertaLocalidade.Contains(c.SeqCursoOfertaLocalidade));
            AddExpression(SeqColaboradorVinculo, a => a.SeqColaboradorVinculo == SeqColaboradorVinculo);

            return GetExpression();
        }
    }
}