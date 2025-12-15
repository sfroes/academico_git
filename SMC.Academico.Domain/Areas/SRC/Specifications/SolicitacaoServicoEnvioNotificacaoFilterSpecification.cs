using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class SolicitacaoServicoEnvioNotificacaoFilterSpecification : SMCSpecification<SolicitacaoServicoEnvioNotificacao>
    {
        public long? Seq { get; set; }

        public long? SeqParametroEnvioNotificacao { get; set; }

        public List<long> SeqsParametrosEnvioNotificacao { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public long? SeqNotificacaoEmailDestinatario { get; set; }

        public override Expression<Func<SolicitacaoServicoEnvioNotificacao, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(SeqParametroEnvioNotificacao, w => w.SeqParametroEnvioNotificacao.HasValue && w.SeqParametroEnvioNotificacao == SeqParametroEnvioNotificacao);
            AddExpression(SeqsParametrosEnvioNotificacao, w => w.SeqParametroEnvioNotificacao.HasValue && SeqsParametrosEnvioNotificacao.Contains(w.SeqParametroEnvioNotificacao.Value));
            AddExpression(SeqSolicitacaoServico, w => w.SeqSolicitacaoServico == this.SeqSolicitacaoServico);
            AddExpression(SeqNotificacaoEmailDestinatario, w => w.SeqNotificacaoEmailDestinatario == this.SeqNotificacaoEmailDestinatario);

            return GetExpression();
        }
    }
}