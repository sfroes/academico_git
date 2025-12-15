using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
	public class SolicitacaoServicoBoletoTaxaFilterSpecification : SMCSpecification<SolicitacaoServicoBoletoTaxa>
	{
		public long? SeqSolicitacaoServico { get; set; }

		public override Expression<Func<SolicitacaoServicoBoletoTaxa, bool>> SatisfiedBy()
		{
			AddExpression(SeqSolicitacaoServico, x => x.SolicitacaoServicoBoleto.SeqSolicitacaoServico == SeqSolicitacaoServico.Value);

			return GetExpression();
		}
	}
}