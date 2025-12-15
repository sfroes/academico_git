using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class AplicacaoAvaliacaoFilterSpecification : SMCSpecification<AplicacaoAvaliacao>
    {

        public long? Seq { get; set; }

        public long? SeqAvaliacao { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public List<long> SeqsOrigemAvaliacao { get; set; }

        public TipoAvaliacao? TipoAvaliacao { get; set; }

        public override Expression<Func<AplicacaoAvaliacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqOrigemAvaliacao, a => a.SeqOrigemAvaliacao == SeqOrigemAvaliacao);
            AddExpression(SeqsOrigemAvaliacao, a => SeqsOrigemAvaliacao.Contains(a.SeqOrigemAvaliacao));
            AddExpression(SeqAvaliacao, a => a.SeqAvaliacao == SeqAvaliacao);
            AddExpression(TipoAvaliacao, a => a.Avaliacao.TipoAvaliacao == TipoAvaliacao);

            return GetExpression();
        }
    }
}