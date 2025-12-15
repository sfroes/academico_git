using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class EventoAulaColaboradorFilterSpecification : SMCSpecification<EventoAulaColaborador>
    {
        public List<long> Seqs { get; set; }

        public List<DateTime> Datas { get; set; }

        public long? SeqEventoAula { get; set; }

        public override Expression<Func<EventoAulaColaborador, bool>> SatisfiedBy()
        {
            AddExpression(this.Seqs, a => this.Seqs.Contains(a.SeqColaboradorSubstituto.Value) 
                                       || (a.SeqColaboradorSubstituto == null && this.Seqs.Contains(a.SeqColaborador)));
            AddExpression(this.Datas, a => this.Datas.Contains(a.EventoAula.Data));
            AddExpression(SeqEventoAula, a => a.SeqEventoAula == SeqEventoAula);

            return GetExpression();
        }
    }
}