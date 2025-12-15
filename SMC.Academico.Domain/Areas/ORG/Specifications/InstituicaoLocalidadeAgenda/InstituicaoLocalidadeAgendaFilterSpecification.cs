using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoLocalidadeAgendaFilterSpecification : SMCSpecification<InstituicaoLocalidadeAgenda>
    {
        public long? Seq { get; set; }

        public long? SeqTipoAgenda { get; set; }

        public long? SeqEntidadeLocalidade { get; set; }

        public override Expression<Func<InstituicaoLocalidadeAgenda, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.SeqTipoAgenda, p => this.SeqTipoAgenda.Value == p.SeqTipoAgenda);
            AddExpression(this.SeqEntidadeLocalidade, p => this.SeqEntidadeLocalidade.Value == p.SeqEntidadeLocalidade);

            return GetExpression();
        }
    }
}