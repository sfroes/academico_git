using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class IngressanteHistoricoSituacaoFilterSpecification : SMCSpecification<IngressanteHistoricoSituacao>
    {
        public long? Seq { get; set; }

        public long? SeqIngressante { get; set; }

        public override Expression<Func<IngressanteHistoricoSituacao, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqIngressante, w => w.SeqIngressante == this.SeqIngressante);
            AddExpression(this.Seq, a => a.Seq == this.Seq);

            return GetExpression();
        }
    }
}