using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class TrabalhoAcademicoAutoriaSpecification : SMCSpecification<TrabalhoAcademicoAutoria>
    {
        public long? SeqAluno { get; set; }


        public override Expression<Func<TrabalhoAcademicoAutoria, bool>> SatisfiedBy()
        {
            AddExpression(SeqAluno, x => x.SeqAluno == SeqAluno);
           
            return GetExpression();
        }
    }
}