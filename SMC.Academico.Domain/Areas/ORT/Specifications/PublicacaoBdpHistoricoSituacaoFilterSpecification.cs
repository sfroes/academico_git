using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class PublicacaoBdpHistoricoSituacaoFilterSpecification : SMCSpecification<PublicacaoBdpHistoricoSituacao>
    {
        public long? Seq { get; set; }

        public long? SeqPublicacaoBdp { get; set; }

        public override Expression<Func<PublicacaoBdpHistoricoSituacao, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, a => a.Seq == this.Seq);
            AddExpression(this.SeqPublicacaoBdp, a => a.SeqPublicacaoBdp == this.SeqPublicacaoBdp);

            return GetExpression();
        }

    }
}
