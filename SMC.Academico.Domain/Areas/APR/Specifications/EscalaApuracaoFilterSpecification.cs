using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class EscalaApuracaoFilterSpecification : SMCSpecification<EscalaApuracao>
    {
        public bool? ApuracaoFinal { get; set; }

        public bool? TipoDiferenteConceito { get; set; }

        public override Expression<Func<EscalaApuracao, bool>> SatisfiedBy()
        { 
            AddExpression(ApuracaoFinal, w => this.ApuracaoFinal == w.ApuracaoFinal);
            AddExpression(TipoDiferenteConceito, w => (w.TipoEscalaApuracao != TipoEscalaApuracao.Conceito) == this.TipoDiferenteConceito.Value);

            return GetExpression();
        }
    }
}
