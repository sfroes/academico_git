using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class EntregaOnlineParticipanteFilterSpecification : SMCSpecification<EntregaOnlineParticipante>
    {
 
        public long? SeqAplicacaoAvaliacao { get; set; }
        public long? SeqAlunoHistorico { get; set; }

        public override Expression<Func<EntregaOnlineParticipante, bool>> SatisfiedBy()
        {
            AddExpression(SeqAplicacaoAvaliacao, x => x.EntregaOnline.SeqAplicacaoAvaliacao == SeqAplicacaoAvaliacao);
            AddExpression(SeqAlunoHistorico, a => a.SeqAlunoHistorico == SeqAlunoHistorico);

            return GetExpression();
        }
    }
}