using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class GrupoConfiguracaoComponenteFilterSpecification : SMCSpecification<GrupoConfiguracaoComponente>
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public bool? Ativo { get; set; }

        public TipoGrupoConfiguracaoComponente? TipoGrupoConfiguracaoComponente { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public override Expression<Func<GrupoConfiguracaoComponente, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.Descricao, p => p.Descricao.StartsWith(this.Descricao));
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo);
            AddExpression(this.TipoGrupoConfiguracaoComponente, p => this.TipoGrupoConfiguracaoComponente.Value == p.TipoGrupoConfiguracaoComponente);
            AddExpression(this.SeqConfiguracaoComponente, p => p.Itens.Any(x => x.SeqConfiguracaoComponente == this.SeqConfiguracaoComponente.Value));

            return GetExpression();
        }
    }
}