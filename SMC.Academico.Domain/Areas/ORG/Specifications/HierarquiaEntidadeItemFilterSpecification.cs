using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class HierarquiaEntidadeItemFilterSpecification : SMCSpecification<HierarquiaEntidadeItem>
    {
        public long? Seq { get; set; }

        public long? SeqHierarquiaEntidade { get; set; }

        public long? SeqEntidade { get; set; }

        public long? SeqEntidadeSuperior { get; set; }

        public TipoVisao? TipoVisaoHierarquia { get; set; }

        public long? SeqHierarquiaEntidadeSuperior { get; set; }

        public List<long> SeqsEntidade { get; set; }

        public bool? EntidadeAtiva { get; set; }

        public string TokenTipoEntidade { get; set; }

        public List<long> SeqsTipoHierarquiaEntidadeItem { get; set; }

        public override Expression<Func<HierarquiaEntidadeItem, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqHierarquiaEntidade, p => p.SeqHierarquiaEntidade == this.SeqHierarquiaEntidade);
            AddExpression(this.SeqEntidade, p => p.SeqEntidade == this.SeqEntidade);
            AddExpression(this.SeqEntidadeSuperior, p => p.ItemSuperior != null && p.ItemSuperior.SeqEntidade == this.SeqEntidadeSuperior);
            AddExpression(this.TipoVisaoHierarquia, p => this.TipoVisaoHierarquia == p.HierarquiaEntidade.TipoHierarquiaEntidade.TipoVisao);
            AddExpression(this.SeqHierarquiaEntidadeSuperior, p => this.SeqHierarquiaEntidadeSuperior == p.SeqItemSuperior);
            AddExpression(this.SeqsEntidade, p => this.SeqsEntidade.Contains(p.SeqEntidade));
            AddExpression(this.EntidadeAtiva, p =>
                this.EntidadeAtiva ==
                    (p.Entidade.HistoricoSituacoes
                        .FirstOrDefault(f => f.DataInicio <= DateTime.Today && (!f.DataFim.HasValue || f.DataFim >= DateTime.Today))
                        .SituacaoEntidade.CategoriaAtividade == CategoriaAtividade.Ativa));
            AddExpression(this.TokenTipoEntidade, p => p.Entidade.TipoEntidade.Token == this.TokenTipoEntidade);
            AddExpression(this.SeqsTipoHierarquiaEntidadeItem, p => this.SeqsTipoHierarquiaEntidadeItem.Contains(p.SeqTipoHierarquiaEntidadeItem));

            return GetExpression();
        }
    }
}