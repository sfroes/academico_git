using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class TrabalhoAcademicoDivisaoComponenteSpecification : SMCSpecification<TrabalhoAcademicoDivisaoComponente>
    {
        public long? SeqTrabalhoAcademico { get; set; }


        public override Expression<Func<TrabalhoAcademicoDivisaoComponente, bool>> SatisfiedBy()
        {
            AddExpression(SeqTrabalhoAcademico, x => x.SeqTrabalhoAcademico == SeqTrabalhoAcademico);
           
            return GetExpression();
        }
    }
}