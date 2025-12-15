using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class DivisaoComponenteFilterSpecification : SMCSpecification<DivisaoComponente>
    {
        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public override Expression<Func<DivisaoComponente, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.Seqs, p => Seqs.Contains(p.Seq));
            AddExpression(this.SeqConfiguracaoComponente, p => p.SeqConfiguracaoComponente == this.SeqConfiguracaoComponente);

            return GetExpression();
        }
    }
}