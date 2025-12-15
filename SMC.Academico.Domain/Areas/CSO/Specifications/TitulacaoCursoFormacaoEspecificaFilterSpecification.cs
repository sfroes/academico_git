using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class TitulacaoCursoFormacaoEspecificaFilterSpecification : SMCSpecification<CursoFormacaoEspecificaTitulacao>
    {
        public long SeqCursoFormacaoEspecifica { get; set; }

        public override Expression<Func<CursoFormacaoEspecificaTitulacao, bool>> SatisfiedBy()
        {
            AddExpression(w => w.SeqCursoFormacaoEspecifica == SeqCursoFormacaoEspecifica); 

            return GetExpression();
        }
    }
}
