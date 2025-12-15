using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class TipoHierarquiaEntidadeItemVerificarFilhosSpecification : SMCSpecification<TipoHierarquiaEntidadeItem>
    {
        /// <summary>
        /// Seq da árvore pai
        /// </summary>
        public long SeqTipoHierarquiaEntidade { get; set; }

        public TipoHierarquiaEntidadeItemVerificarFilhosSpecification(long SeqTipoHierarquiaEntidade)
        {
            this.SeqTipoHierarquiaEntidade = SeqTipoHierarquiaEntidade;
        }

        /// <summary>
        /// Busca todos os itens vinculados à uma árvore, através do SeqTipoHierarquiaEntidade
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TipoHierarquiaEntidadeItem, bool>> SatisfiedBy()
        {
            AddExpression(p => p.SeqTipoHierarquiaEntidade == this.SeqTipoHierarquiaEntidade);

            return GetExpression();
        }
    }
}
