using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class SolicitacaoServicoEtapaFilterSpecification : SMCSpecification<SolicitacaoServicoEtapa>
    {
        public long? SeqSolicitacaoServico { get; set; }

        public long? Seq { get; set; }

        public long? SeqConfiguracaoEtapa { get; set; }

        public string Token { get; set; }

        public override Expression<Func<SolicitacaoServicoEtapa, bool>> SatisfiedBy()
        { 
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(Token, w => w.ConfiguracaoEtapa.ProcessoEtapa.Token == Token);
            AddExpression(SeqSolicitacaoServico, w => w.SeqSolicitacaoServico == SeqSolicitacaoServico.Value);
            AddExpression(SeqConfiguracaoEtapa, w => w.SeqConfiguracaoEtapa == SeqConfiguracaoEtapa.Value);

            return GetExpression();
        }
    }
}