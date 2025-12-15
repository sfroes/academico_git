using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class ClassificacaoBDPSpecification : SMCSpecification<Classificacao>
    {
        public override Expression<Func<Classificacao, bool>> SatisfiedBy()
        {
            AddExpression(x => x.HierarquiaClassificacao.TipoHierarquiaClassificacao.Token == "HIERARQUIA_CAPES");
            AddExpression(x => x.TipoClassificacao.Token == "GRANDE_AREA");

            return GetExpression();
        }
    }
}
