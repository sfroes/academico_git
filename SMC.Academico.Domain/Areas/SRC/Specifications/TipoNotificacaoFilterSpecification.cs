using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class TipoNotificacaoFilterSpecification : SMCSpecification<TipoNotificacao>
    {
        public long? Seq { get; set; }        

        public bool? PermiteAgendamento { get; set; }

        public string Token { get; set; }

        public override Expression<Func<TipoNotificacao, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(PermiteAgendamento, w => w.PermiteAgendamento == this.PermiteAgendamento);
            AddExpression(Token, w => w.Token == this.Token);

            return GetExpression();
        }
    }
}
