using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class GrupoEscalonamentoFilterSpecification : SMCSpecification<GrupoEscalonamento>
    {
        public long? Seq { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqProcesso { get; set; }

        public List<long> SeqsProcessos { get; set; }

        public string Descricao { get; set; }

        public bool? EscalonamentoAtivo { get; set; }

        public long? SeqEscalonamento { get; set; }

        public override Expression<Func<GrupoEscalonamento, bool>> SatisfiedBy()
        {
            if (Seq == 0) Seq = null;

            AddExpression(Seq, x => x.Seq == Seq.Value);
            AddExpression(Ativo, x => x.Ativo == this.Ativo);
            AddExpression(EscalonamentoAtivo, x => (EscalonamentoAtivo.Value && x.Itens.Any(g => g.Escalonamento.DataInicio <= DateTime.Now && g.Escalonamento.DataFim >= DateTime.Now)) ||
                                                   (!EscalonamentoAtivo.Value && x.Itens.Any(g => g.Escalonamento.DataInicio > DateTime.Now && g.Escalonamento.DataFim < DateTime.Now)));
            AddExpression(SeqProcesso, x => x.SeqProcesso == SeqProcesso.Value);
            AddExpression(SeqsProcessos, x => SeqsProcessos.Contains(x.SeqProcesso));
            AddExpression(Descricao, x => x.Descricao.Contains(Descricao));

            AddExpression(SeqEscalonamento, a => a.Itens.Any(b => b.SeqEscalonamento == SeqEscalonamento.Value));

            return GetExpression();
        }
    }
}