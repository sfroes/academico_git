using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaCicloLetivoFilterSpecification : SMCSpecification<CampanhaCicloLetivo>
    {
        public long? Seq { get; set; }

        public long? SeqCampanha { get; set; }

        public long? SeqCicloLeitivo { get; set; }

        public override Expression<Func<CampanhaCicloLetivo, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqCampanha, p => p.SeqCampanha == this.SeqCampanha);
            AddExpression(this.SeqCicloLeitivo, p => p.SeqCicloLetivo == this.SeqCicloLeitivo);

            return GetExpression();
        }
    }
}