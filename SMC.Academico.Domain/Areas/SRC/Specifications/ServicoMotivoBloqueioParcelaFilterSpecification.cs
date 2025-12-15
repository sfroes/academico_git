using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ServicoMotivoBloqueioParcelaFilterSpecification : SMCSpecification<ServicoMotivoBloqueioParcela>
    {
        public long? SeqServico { get; set; }

        public bool? Obrigatorio { get; set; }

        public override Expression<Func<ServicoMotivoBloqueioParcela, bool>> SatisfiedBy()
        {
            AddExpression(SeqServico, w => w.SeqServico == this.SeqServico);
            AddExpression(Obrigatorio, w => w.Obrigatorio == this.Obrigatorio);
          
            return GetExpression();
        }
    }
}
