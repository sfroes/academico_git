using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
	public class SolicitacaoServicoBoletoFilterSpecification : SMCSpecification<SolicitacaoServicoBoleto>
	{
		public long? SeqSolicitacaoServico { get; set; }

		public override Expression<Func<SolicitacaoServicoBoleto, bool>> SatisfiedBy()
		{
			AddExpression(SeqSolicitacaoServico, x => x.SeqSolicitacaoServico == SeqSolicitacaoServico.Value);
		
			return GetExpression();
		}
	}
}