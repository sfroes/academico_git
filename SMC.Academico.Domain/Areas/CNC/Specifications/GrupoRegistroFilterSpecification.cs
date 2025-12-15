using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class GrupoRegistroFilterSpecification : SMCSpecification<GrupoRegistro>
    {
        public long? Seq { get; set; }
        public string Descricao { get; set; }

      public override Expression<Func<GrupoRegistro, bool>> SatisfiedBy()
        {
            AddExpression(Seq, a => a.Seq == Seq);
            AddExpression(Descricao, a => a.Descricao.Contains(Descricao));
  
            return GetExpression();
        }
    }
}