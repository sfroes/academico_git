using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class GrupoEscalonamentoItemFilterSpecification : SMCSpecification<GrupoEscalonamentoItem>
    {
        public long? Seq { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public long? SeqEscalonamento { get; set; }
                
        public bool? EscalonamentoVencido { get; set; }

        private DateTime DataAtual = DateTime.Today;

        public override Expression<Func<GrupoEscalonamentoItem, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(SeqGrupoEscalonamento, w => w.SeqGrupoEscalonamento == SeqGrupoEscalonamento.Value);
            AddExpression(SeqEscalonamento, w => w.SeqEscalonamento == SeqEscalonamento.Value);
            AddExpression(EscalonamentoVencido, w => (EscalonamentoVencido.Value && w.Escalonamento.DataFim < DataAtual) || (!EscalonamentoVencido.Value && w.Escalonamento.DataFim > DataAtual));
           
            return GetExpression();
        }
    }
}