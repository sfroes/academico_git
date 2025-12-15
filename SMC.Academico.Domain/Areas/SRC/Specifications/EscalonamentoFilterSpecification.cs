using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class EscalonamentoFilterSpecification : SMCSpecification<Escalonamento>
    {
        public long? Seq { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public long? SeqProcesso { get; set; }

        public bool? EscalonamentoVencido { get; set; }

        DateTime hoje = DateTime.Today;

        public override Expression<Func<Escalonamento, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(SeqProcessoEtapa, w => w.SeqProcessoEtapa == SeqProcessoEtapa.Value);
            AddExpression(EscalonamentoVencido, w => (EscalonamentoVencido.Value && w.DataFim < hoje) || (!EscalonamentoVencido.Value && w.DataFim > hoje));
            AddExpression(this.SeqProcesso, w => w.ProcessoEtapa.SeqProcesso == this.SeqProcesso);

            return GetExpression();
        }
    }
}
