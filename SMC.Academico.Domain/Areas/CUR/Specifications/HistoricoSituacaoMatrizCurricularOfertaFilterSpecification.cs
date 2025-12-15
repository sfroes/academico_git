using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class HistoricoSituacaoMatrizCurricularOfertaFilterSpecification : SMCSpecification<HistoricoSituacaoMatrizCurricularOferta>
    {
        public long? Seq { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public override Expression<Func<HistoricoSituacaoMatrizCurricularOferta, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqMatrizCurricularOferta, p => p.SeqMatrizCurricularOferta == this.SeqMatrizCurricularOferta);

            return GetExpression();
        }
    }
}
