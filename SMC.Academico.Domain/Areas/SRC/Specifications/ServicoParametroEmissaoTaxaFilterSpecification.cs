using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ServicoParametroEmissaoTaxaFilterSpecification : SMCSpecification<ServicoParametroEmissaoTaxa>
    {
        public long? SeqServico { get; set; }

        public TipoEmissaoTaxa? TipoEmissaoTaxa { get; set; }

        public override Expression<Func<ServicoParametroEmissaoTaxa, bool>> SatisfiedBy()
        {
            AddExpression(SeqServico, a => a.SeqServico == SeqServico.Value);
            AddExpression(TipoEmissaoTaxa, a => a.TipoEmissaoTaxa == TipoEmissaoTaxa.Value);

            return GetExpression();
        }
    }
}
