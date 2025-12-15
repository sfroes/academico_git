using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class TipoEntidadeFilterSpecification : SMCSpecification<TipoEntidade>
    {
        public long? Seq { get; set; }

        public bool? EntidadeExternada { get; set; }

        public string Token { get; set; }

        public List<string> Tokens { get; set; }

        public bool? PermiteAtoNormativo { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqEntidade { get; set; }

        public override System.Linq.Expressions.Expression<Func<TipoEntidade, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.EntidadeExternada, p => p.EntidadeExternada == this.EntidadeExternada.Value);
            AddExpression(this.Token, p => p.Token == this.Token);
            AddExpression(this.Tokens, p => Tokens.Contains(p.Token));
            AddExpression(this.PermiteAtoNormativo, a => a.PermiteAtoNormativo == this.PermiteAtoNormativo);
            AddExpression(this.SeqInstituicaoEnsino, a => a.InstituicoesTipoEntidade.Select(s => s.SeqInstituicaoEnsino).Contains(this.SeqInstituicaoEnsino.Value));
            


            return GetExpression();

        }
    }
}