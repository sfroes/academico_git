using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class HierarquiaEntidadeFilterSpecification : SMCSpecification<HierarquiaEntidade>
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public long? SeqTipoHierarquiaEntidade { get; set; }

        public TipoVisao? Visao { get; set; }

        public DateTime? DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public override System.Linq.Expressions.Expression<Func<HierarquiaEntidade, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq == 0 || p.Seq == this.Seq.Value);
            AddExpression(this.Descricao, p => p.Descricao.Contains(this.Descricao));
            AddExpression(this.SeqTipoHierarquiaEntidade, p => p.SeqTipoHierarquiaEntidade == this.SeqTipoHierarquiaEntidade);
            AddExpression(this.Visao, p => p.TipoHierarquiaEntidade.TipoVisao == this.Visao);
            AddExpression(this.DataInicioVigencia, p => p.DataInicioVigencia >= this.DataInicioVigencia.Value);
            AddExpression(this.DataFimVigencia, p => p.DataFimVigencia <= this.DataFimVigencia.Value);

            return GetExpression();
        }
    }
}