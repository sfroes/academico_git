using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaAtuacaoCondicaoObrigatoriedadeFilterSpecification : SMCSpecification<PessoaAtuacaoCondicaoObrigatoriedade>
    {
        public long SeqPessoaAtuacao { get; set; }

        public bool? Ativo { get; set; }

        public override Expression<Func<PessoaAtuacaoCondicaoObrigatoriedade, bool>> SatisfiedBy()
        { 
            AddExpression(w => w.SeqPessoaAtuacao == this.SeqPessoaAtuacao);
            AddExpression(Ativo, w => w.Ativo == this.Ativo);

            return GetExpression();
        }
    }
}