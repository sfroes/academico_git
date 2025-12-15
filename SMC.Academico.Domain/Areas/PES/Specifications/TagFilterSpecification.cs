using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class TagFilterSpecification : SMCSpecification<Tag>
    {
        public string Descricao { get; set; }
        public TipoTag? TipoTag { get; set; }
        public override Expression<Func<Tag, bool>> SatisfiedBy()
        {
            AddExpression(Descricao, w => w.Descricao.Contains(Descricao));
            AddExpression(TipoTag, w => w.TipoTag == TipoTag);

            return GetExpression();
        }
    }
}
