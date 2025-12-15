using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class IngressanteOfertaFilterSpecification : SMCSpecification<IngressanteOferta>
    {
        public long? SeqIngressante { get; set; }

        public List<long> Seqs { get; set; }

        public override Expression<Func<IngressanteOferta, bool>> SatisfiedBy()
        { 
            AddExpression(SeqIngressante, w => SeqIngressante.Value == w.SeqIngressante);
            AddExpression(Seqs, w => this.Seqs.Contains(w.Seq));

            return GetExpression();
        }
    }
}