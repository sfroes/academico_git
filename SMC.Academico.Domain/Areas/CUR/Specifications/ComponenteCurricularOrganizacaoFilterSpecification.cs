using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class ComponenteCurricularOrganizacaoFilterSpecification : SMCSpecification<ComponenteCurricularOrganizacao>
    {
        public long? SeqComponenteCurricular { get; set; }

        public bool? Ativo { get; set; }

        public override Expression<Func<ComponenteCurricularOrganizacao, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqComponenteCurricular, p => p.SeqComponenteCurricular == this.SeqComponenteCurricular);
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo);

            return GetExpression();
        }
    }
}