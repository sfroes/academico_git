using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoTipoEntidadeFilterSpecification : SMCSpecification<InstituicaoTipoEntidade>
    {
        public InstituicaoTipoEntidadeFilterSpecification()
        {
            this.SetOrderBy(x => x.TipoEntidade.Descricao);
        }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoEntidade { get; set; }
        public List<long> SeqsTipoEntidade { get; set; }

        public bool? EntidadeExternada { get; set; }

        public string DescricaoTipoEntidade { get; set; }

        public string Token { get; set; }

        public override Expression<Func<InstituicaoTipoEntidade, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqInstituicaoEnsino, p => p.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino.Value);
            AddExpression(this.SeqTipoEntidade, p => p.SeqTipoEntidade == this.SeqTipoEntidade.Value);
            AddExpression(this.EntidadeExternada, p => p.TipoEntidade.EntidadeExternada == this.EntidadeExternada.Value);
            AddExpression(this.DescricaoTipoEntidade, p => p.TipoEntidade.Descricao.StartsWith(this.DescricaoTipoEntidade));
            AddExpression(this.Token, p => p.TipoEntidade.Token == this.Token);
            AddExpression(this.SeqsTipoEntidade, p => SeqsTipoEntidade.Contains(p.SeqTipoEntidade));

            return GetExpression();
        }
    }
}