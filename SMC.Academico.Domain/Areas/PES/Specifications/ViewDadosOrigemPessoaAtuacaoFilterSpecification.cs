using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
	public class ViewDadosOrigemPessoaAtuacaoFilterSpecification : SMCSpecification<ViewDadosOrigemPessoaAtuacao>
	{
		public long? SeqPessoaAtuacao { get; set; }

		public override Expression<Func<ViewDadosOrigemPessoaAtuacao, bool>> SatisfiedBy()
		{
			AddExpression(SeqPessoaAtuacao, x => x.SeqPessoaAtuacao == SeqPessoaAtuacao);
			return GetExpression();
		}
	}
}