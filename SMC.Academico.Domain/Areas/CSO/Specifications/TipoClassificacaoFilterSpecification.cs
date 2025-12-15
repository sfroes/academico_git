using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{

    public class TipoClassificacaoFilterSpecification : SMCSpecification<TipoClassificacao>
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public long? SeqTipoHierarquiaClassificacao { get; set; }

        public long? SeqTipoClassificacaoSuperior { get; set; }

        public bool Exclusivo { get; set; }

        public override Expression<Func<TipoClassificacao, bool>> SatisfiedBy()
        {
            return a => (!this.Seq.HasValue || this.Seq.Value == default(long) || this.Seq.Value == a.Seq) &&
                        (!this.SeqTipoHierarquiaClassificacao.HasValue || this.SeqTipoHierarquiaClassificacao.Value == default(long) || this.SeqTipoHierarquiaClassificacao.Value == a.SeqTipoHierarquiaClassificacao) &&
                        ((!this.SeqTipoClassificacaoSuperior.HasValue && (!a.SeqTipoClassificacaoSuperior.HasValue || !Exclusivo)) || this.SeqTipoClassificacaoSuperior.Value == a.SeqTipoClassificacaoSuperior) &&
                        (string.IsNullOrEmpty(this.Descricao) || a.Descricao.StartsWith(this.Descricao));
        }
    }
}
