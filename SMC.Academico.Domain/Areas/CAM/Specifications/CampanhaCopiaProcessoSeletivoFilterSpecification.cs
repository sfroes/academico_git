using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaCopiaProcessoSeletivoFilterSpecification : SMCSpecification<ProcessoSeletivo>
    {
        public long? SeqCampanhaOrigem { get; set; }

        public long[] SeqsProcessosSeletivos { get; set; }

        public override Expression<Func<ProcessoSeletivo, bool>> SatisfiedBy()
        {
            AddExpression(SeqCampanhaOrigem, x => x.SeqCampanha == SeqCampanhaOrigem);
            AddExpression(SeqsProcessosSeletivos, x => SeqsProcessosSeletivos.Contains(x.Seq));

            return GetExpression();
        }
    }
}