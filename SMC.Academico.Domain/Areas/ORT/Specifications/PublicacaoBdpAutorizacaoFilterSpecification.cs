using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class PublicacaoBdpAutorizacaoFilterSpecification : SMCSpecification<PublicacaoBdpAutorizacao>
    {
        public long? SeqPublicacaoBdp { get; set; }

        public override Expression<Func<PublicacaoBdpAutorizacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqPublicacaoBdp, a => a.SeqPublicacaoBdp == this.SeqPublicacaoBdp);
            return GetExpression();
        }
    }
}