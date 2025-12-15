using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class TitulacaoDocumentoComprobatorioFilterSpecification : SMCSpecification<TitulacaoDocumentoComprobatorio>
    {
        public long? SeqTitulacao { get; set; }

        public override Expression<Func<TitulacaoDocumentoComprobatorio, bool>> SatisfiedBy()
        {
            AddExpression(SeqTitulacao, p => p.SeqTitulacao == SeqTitulacao);

            return GetExpression();
        }
    }
}