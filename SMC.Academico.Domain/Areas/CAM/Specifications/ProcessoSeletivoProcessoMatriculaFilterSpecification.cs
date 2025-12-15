using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ProcessoSeletivoProcessoMatriculaFilterSpecification : SMCSpecification<ProcessoSeletivoProcessoMatricula>
    {
        public long? SeqCampanhaCicloLetivo { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public override Expression<Func<ProcessoSeletivoProcessoMatricula, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqCampanhaCicloLetivo, p => p.SeqCampanhaCicloLetivo == this.SeqCampanhaCicloLetivo);
            AddExpression(this.SeqProcessoSeletivo, p => p.SeqProcessoSeletivo == this.SeqProcessoSeletivo);

            return GetExpression();
        }
    }
}