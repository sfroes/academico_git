using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class TipoAgendaFilterSpecification : SMCSpecification<TipoAgenda>
    {
        public TipoAgendaFilterSpecification()
        {
            this.SetOrderBy(x => x.Descricao);
        }

        public long? Seq { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public string Descricao { get; set; }

        public bool? EventoLetivo { get; set; }

        public bool? DiaUtil { get; set; }

        public bool? ReplicarLancamentoPorLocalidade { get; set; }

        public override System.Linq.Expressions.Expression<Func<TipoAgenda, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p =>  this.Seq.Value == 0 || p.Seq == this.Seq.Value);
            AddExpression(this.SeqInstituicaoEnsino, p => p.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino.Value);
            AddExpression(this.Descricao, p => p.Descricao.Contains(this.Descricao));
            AddExpression(this.EventoLetivo, p => p.EventoLetivo == this.EventoLetivo.Value);
            AddExpression(this.DiaUtil, p => p.DiaUtil == this.DiaUtil.Value);
            AddExpression(this.ReplicarLancamentoPorLocalidade, p => p.ReplicarLancamentoPorLocalidade == this.ReplicarLancamentoPorLocalidade.Value);

            return GetExpression();
        }
    }
}