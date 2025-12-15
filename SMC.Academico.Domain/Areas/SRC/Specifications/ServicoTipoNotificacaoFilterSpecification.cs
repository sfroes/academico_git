using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ServicoTipoNotificacaoFilterSpecification : SMCSpecification<ServicoTipoNotificacao>
    {
        public long? SeqServico { get; set; }

        public long? SeqEtapaSgf { get; set; }

        public long? SeqTipoNotificacao { get; set; }

        public override Expression<Func<ServicoTipoNotificacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqServico, w => w.SeqServico == this.SeqServico);
            AddExpression(SeqEtapaSgf, w => w.SeqEtapaSgf == this.SeqEtapaSgf);
            AddExpression(SeqTipoNotificacao, w => w.SeqTipoNotificacao == this.SeqTipoNotificacao);
 
            return GetExpression();
        }
    }
}
