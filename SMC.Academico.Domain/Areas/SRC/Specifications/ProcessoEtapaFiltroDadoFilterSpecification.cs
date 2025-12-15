using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ProcessoEtapaFiltroDadoFilterSpecification : SMCSpecification<ProcessoEtapaFiltroDado>
    {
        public long? SeqProcessoEtapa { get; set; }

        public override Expression<Func<ProcessoEtapaFiltroDado, bool>> SatisfiedBy()
        {
            AddExpression(SeqProcessoEtapa, w => w.SeqProcessoEtapa == this.SeqProcessoEtapa.Value);

            return GetExpression();
        }
    }
}