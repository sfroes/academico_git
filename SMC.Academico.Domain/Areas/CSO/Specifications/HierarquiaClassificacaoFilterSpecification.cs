using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class HierarquiaClassificacaoFilterSpecification : SMCSpecification<HierarquiaClassificacao>
    {
        public long? SeqTipoHierarquiaClassificacao { get; set; }

        public string TokenTipoHierarquiaClassificacao { get; set; }

        public string Descricao { get; set; }

        public override Expression<Func<HierarquiaClassificacao, bool>> SatisfiedBy()
        {
            AddExpression(TokenTipoHierarquiaClassificacao, w => w.TipoHierarquiaClassificacao.Token == this.TokenTipoHierarquiaClassificacao);
            AddExpression(this.SeqTipoHierarquiaClassificacao, w => w.SeqTipoHierarquiaClassificacao == this.SeqTipoHierarquiaClassificacao);
            AddExpression(this.Descricao, w => w.Descricao == this.Descricao);

            return GetExpression();
        }
    }
}