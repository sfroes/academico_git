using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class EntidadeConfiguracaoNotificacaoFilterSpecification : SMCSpecification<EntidadeConfiguracaoNotificacao>
    {
        public long? SeqEntidade { get; set; }

        public long? SeqTipoNotificacao { get; set; }

        public string TokenTipoNotificacao { get; set; }

        public bool? Vigente { get; set; }

        public override Expression<Func<EntidadeConfiguracaoNotificacao, bool>> SatisfiedBy()
        {
            var dataAtual = DateTime.Now;

            AddExpression(SeqEntidade, p => p.SeqEntidade == SeqEntidade);
            AddExpression(SeqTipoNotificacao, p => p.SeqTipoNotificacao == SeqTipoNotificacao);
            AddExpression(TokenTipoNotificacao, p => p.TokenTipoNotificacao == TokenTipoNotificacao);
            AddExpression(Vigente, p => dataAtual >= p.DataInicioValidade && (!p.DataFimValidade.HasValue || (p.DataFimValidade.HasValue && dataAtual <= p.DataFimValidade.Value)));

            return GetExpression();
        }
    }
}
