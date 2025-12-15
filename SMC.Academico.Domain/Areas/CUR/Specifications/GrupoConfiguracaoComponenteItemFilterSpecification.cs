using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class GrupoConfiguracaoComponenteItemFilterSpecification : SMCSpecification<GrupoConfiguracaoComponenteItem>
    {
        public long? Seq { get; set; }
        public long? SeqGrupoConfiguracaoComponente { get; set; }

        public override Expression<Func<GrupoConfiguracaoComponenteItem, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqGrupoConfiguracaoComponente, p => p.SeqGrupoConfiguracaoComponente == this.SeqGrupoConfiguracaoComponente.Value);

            return GetExpression();
        }
    }
}