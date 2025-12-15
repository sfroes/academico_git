using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
	public class SolicitacaoDadoFormularioCampoFilterSpecification : SMCSpecification<SolicitacaoDadoFormularioCampo>
	{
        public long? SeqSolicitacaoServico { get; set; }

        public string NumeroProtocolo { get; set; }

		public string TokenElemento { get; set; }
		public Guid? IdCorrelacao { get; set; }

		public override Expression<Func<SolicitacaoDadoFormularioCampo, bool>> SatisfiedBy()
		{
			AddExpression(SeqSolicitacaoServico, x => x.DadoFormulario.SeqSolicitacaoServico == SeqSolicitacaoServico);
			AddExpression(NumeroProtocolo, x => x.DadoFormulario.SolicitacaoServico.NumeroProtocolo == NumeroProtocolo);
			AddExpression(TokenElemento, x => x.TokenElemento == TokenElemento);
			AddExpression(IdCorrelacao, x => x.IdCorrelacao == IdCorrelacao);

			return GetExpression();
		}
	}
}