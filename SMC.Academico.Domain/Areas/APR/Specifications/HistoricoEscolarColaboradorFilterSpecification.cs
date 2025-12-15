using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class HistoricoEscolarColaboradorFilterSpecification : SMCSpecification<HistoricoEscolarColaborador>
    {
        public long? Seq { get; set; }

        public long? SeqHistoricoEscolar { get; set; }

        public long? SeqColaborador { get; set; }

        public bool? ComColaborador { get; set; }

        public override Expression<Func<HistoricoEscolarColaborador, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(SeqHistoricoEscolar, w => w.SeqHistoricoEscolar == SeqHistoricoEscolar.Value);
            AddExpression(SeqColaborador, w => w.SeqColaborador == SeqColaborador.Value);
            AddExpression(ComColaborador, w => w.SeqColaborador.HasValue == ComColaborador);

            return GetExpression();
        }
    }
}