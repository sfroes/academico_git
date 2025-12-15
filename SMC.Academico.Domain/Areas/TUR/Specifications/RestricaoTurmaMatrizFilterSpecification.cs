using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.TUR.Specifications
{
    public class RestricaoTurmaMatrizFilterSpecification : SMCSpecification<RestricaoTurmaMatriz>
    {
        public long? Seq { get; set; }

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public List<long> SeqsMatrizCurricularOferta { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public override Expression<Func<RestricaoTurmaMatriz, bool>> SatisfiedBy()
        {
            AddExpression(Seq, p => p.Seq == Seq);
            AddExpression(SeqTurmaConfiguracaoComponente, p => p.SeqTurmaConfiguracaoComponente == SeqTurmaConfiguracaoComponente);
            AddExpression(SeqMatrizCurricularOferta, p => p.SeqMatrizCurricularOferta == SeqMatrizCurricularOferta);
            AddExpression(SeqsMatrizCurricularOferta, p => p.SeqMatrizCurricularOferta.HasValue && SeqsMatrizCurricularOferta.Contains(p.SeqMatrizCurricularOferta.Value));
            AddExpression(SeqTurma, p => p.TurmaConfiguracaoComponente.SeqTurma == SeqTurma.Value);
            AddExpression(SeqComponenteCurricularAssunto, p => p.SeqComponenteCurricularAssunto == SeqComponenteCurricularAssunto);

            return GetExpression();
        }
    }
}
