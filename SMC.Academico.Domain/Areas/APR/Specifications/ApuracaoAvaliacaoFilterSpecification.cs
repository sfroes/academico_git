using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    internal class ApuracaoAvaliacaoFilterSpecification : SMCSpecification<ApuracaoAvaliacao>
    {
        public long? SeqAlunoHistorico { get; set; }

        public long? SeqAplicacaoAvaliacao { get; set; }

        public long? Seq { get; set; }

        public List<long> SeqsAplicacaoAvaliacao { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public List<long> SeqsOrigemAvaliacao { get; set; }

        public override Expression<Func<ApuracaoAvaliacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqAlunoHistorico, x => x.SeqAlunoHistorico == SeqAlunoHistorico);
            AddExpression(SeqAplicacaoAvaliacao, x => x.SeqAplicacaoAvaliacao == SeqAplicacaoAvaliacao);
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqsAplicacaoAvaliacao, x => SeqsAplicacaoAvaliacao.Contains(x.SeqAplicacaoAvaliacao));
            AddExpression(SeqOrigemAvaliacao, x => x.AplicacaoAvaliacao.SeqOrigemAvaliacao == SeqOrigemAvaliacao);
            AddExpression(SeqsOrigemAvaliacao, x => SeqsOrigemAvaliacao.Contains(x.AplicacaoAvaliacao.SeqOrigemAvaliacao));

            return GetExpression();
        }
    }
}