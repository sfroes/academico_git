using System;
using System.Linq.Expressions;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class RestricaoSolicitacaoSimultaneaFilterSpecification : SMCSpecification<RestricaoSolicitacaoSimultanea>
    {
        public long? SeqServico { get; set; }

        public override Expression<Func<RestricaoSolicitacaoSimultanea, bool>> SatisfiedBy()
        {
            AddExpression(SeqServico, x => x.SeqServico == SeqServico.Value);

            return GetExpression();
        }
    }
}