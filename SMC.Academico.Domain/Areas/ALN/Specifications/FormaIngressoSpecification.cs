using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class FormaIngressoSpecification : SMCSpecification<FormaIngresso>
    {
        public FormaIngressoSpecification(long seqTipoVinculoAluno)
        {
            SeqTipoVinculoAluno = seqTipoVinculoAluno;
        }

        public long SeqTipoVinculoAluno { get; private set; }

        public override Expression<Func<FormaIngresso, bool>> SatisfiedBy()
        {
            AddExpression(x => x.SeqTipoVinculoAluno == SeqTipoVinculoAluno);
            return GetExpression();
        }
    }
}
