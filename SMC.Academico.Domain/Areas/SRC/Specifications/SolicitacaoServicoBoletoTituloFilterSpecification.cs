using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
	public class SolicitacaoServicoBoletoTituloFilterSpecification : SMCSpecification<SolicitacaoServicoBoletoTitulo>
	{
		public long? SeqSolicitacaoServico { get; set; }

		public int? SeqTituloGra { get; set; }

		public override Expression<Func<SolicitacaoServicoBoletoTitulo, bool>> SatisfiedBy()
		{
			AddExpression(SeqSolicitacaoServico, x => x.SolicitacaoServicoBoleto.SeqSolicitacaoServico == SeqSolicitacaoServico.Value);
			AddExpression(SeqTituloGra, x => x.SeqTituloGra == SeqTituloGra.Value);

			return GetExpression();
		}
	}
}