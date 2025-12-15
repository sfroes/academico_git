using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class EscalaApuracaoItemFilterSpecification : SMCSpecification<EscalaApuracaoItem>
    {
        public long? Seq { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        public short? Percentual { get; set; }

        public override Expression<Func<EscalaApuracaoItem, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(SeqEscalaApuracao, w => w.SeqEscalaApuracao == SeqEscalaApuracao);
            AddExpression(Percentual, w => Percentual >= w.PercentualMinimo && Percentual <= w.PercentualMaximo);

            return GetExpression();
        }
    }
}