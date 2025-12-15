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
    public class IngressanteConvocadoSpecification : SMCSpecification<Ingressante>
    {
        public long SeqConvocado { get; set; }

        public override Expression<Func<Ingressante, bool>> SatisfiedBy()
        {
            AddExpression(x => x.SeqConvocado == SeqConvocado);
            return GetExpression();
        }
    }
}
