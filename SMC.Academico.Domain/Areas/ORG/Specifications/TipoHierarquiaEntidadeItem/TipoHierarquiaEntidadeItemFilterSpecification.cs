using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class TipoHierarquiaEntidadeItemFilterSpecification : SMCSpecification<TipoHierarquiaEntidadeItem>
    {
        public long? SeqTipoHierarquiaEntidade { get; set; }

        public long? SeqTipoEntidade { get; set; }

        /// <summary>
        /// Tipos que possam ser pai do tipo informado
        /// </summary>
        public long? SeqPaiTipoEntidade { get; set; }

        public long? SeqItemSuperior { get; set; }

        public bool? Raiz { get; set; }

        public bool? EntidadeExternada { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public TipoVisao? TipoVisao { get; set; }

        public bool? Responsavel { get; set; }

        public override Expression<Func<TipoHierarquiaEntidadeItem, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqTipoHierarquiaEntidade, p => p.SeqTipoHierarquiaEntidade == this.SeqTipoHierarquiaEntidade);
            AddExpression(this.SeqTipoEntidade, p => p.SeqTipoEntidade == this.SeqTipoEntidade);
            AddExpression(this.SeqPaiTipoEntidade, p => p.ItensFilhos.Any(a => a.SeqTipoEntidade == SeqPaiTipoEntidade));
            AddExpression(this.SeqItemSuperior, p => p.SeqItemSuperior == this.SeqItemSuperior);
            AddExpression(this.Raiz, p => (this.Raiz.Value && !p.SeqItemSuperior.HasValue));
            AddExpression(this.EntidadeExternada, p => p.TipoEntidade.EntidadeExternada == this.EntidadeExternada.Value);
            AddExpression(this.SeqInstituicaoEnsino, p => p.TipoHierarquiaEntidade.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(this.TipoVisao, p => p.TipoHierarquiaEntidade.TipoVisao == this.TipoVisao);
            AddExpression(this.Responsavel, p => p.Responsavel == this.Responsavel);
            return GetExpression();
        }
    }
}