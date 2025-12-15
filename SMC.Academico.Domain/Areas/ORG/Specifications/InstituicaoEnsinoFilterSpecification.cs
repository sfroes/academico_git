using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoEnsinoFilterSpecification: SMCSpecification<InstituicaoEnsino>
    {
        public InstituicaoEnsinoFilterSpecification()
        {
            this.SetOrderBy(x => x.Nome);
        }

        public long? Seq { get; set; }

        public string Nome { get; set; }

        public bool? Ativo { get; set; }

        public string Sigla { get; set; }

        public override System.Linq.Expressions.Expression<Func<InstituicaoEnsino, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq.Value);
            AddExpression(this.Nome, p => p.Nome.StartsWith(this.Nome));
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo);
            AddExpression(this.Sigla, p => p.Sigla == Sigla);

            return GetExpression();
        }
    }
    
}
