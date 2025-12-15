using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaDadosPessoaisFilterSpecification : SMCSpecification<PessoaDadosPessoais>
    {
        public List<long> Seqs { get; set; }

        public override Expression<Func<PessoaDadosPessoais, bool>> SatisfiedBy()
        {
            AddExpression(Seqs, p => Seqs.Any(x => x == p.Seq));

            return GetExpression();
        }
    }
}
