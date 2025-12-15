using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class TipoOrientacaoFilterSpecification : SMCSpecification<TipoOrientacao>
    {
        public long? Seq { get; set; }

        public bool? TrabalhoConclusaoCurso { get; set; }

        public bool? PermiteManutencaoManual { get; set; }

        public override Expression<Func<TipoOrientacao, bool>> SatisfiedBy()
        { 
            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(TrabalhoConclusaoCurso, w => w.TrabalhoConclusaoCurso == TrabalhoConclusaoCurso.Value);
            AddExpression(PermiteManutencaoManual, w => w.PermiteManutencaoManual == PermiteManutencaoManual.Value);

            return GetExpression();
        }
    }
}
