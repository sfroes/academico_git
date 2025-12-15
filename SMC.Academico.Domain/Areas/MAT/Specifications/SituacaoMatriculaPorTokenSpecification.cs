using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.MAT.Specifications
{
    public class SituacaoMatriculaPorTokenSpecification : SMCSpecification<SituacaoMatricula>
    {
        public string Token { get; private set; }

        public SituacaoMatriculaPorTokenSpecification(string token)
        {
            Token = token;
        }

        public override Expression<Func<SituacaoMatricula, bool>> SatisfiedBy()
        {
            AddExpression(x => x.Token == Token);
            return GetExpression();
        }
    }
}
