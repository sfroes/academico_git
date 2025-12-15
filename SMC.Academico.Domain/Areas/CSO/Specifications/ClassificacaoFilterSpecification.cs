using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class ClassificacaoFilterSpecification : SMCSpecification<Classificacao>
    {
        public long? SeqHierarquiaClassificacao { get; set; }

        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public override Expression<Func<Classificacao, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqHierarquiaClassificacao, p => p.SeqHierarquiaClassificacao == this.SeqHierarquiaClassificacao);
            AddExpression(this.Seqs, p => Seqs.Contains(p.Seq));

            // Ao adicionar a propriedade Seq no FiltroData, a mesma chega com valor 0 neste ponto devido à tela ser um Dynamic.
            // O Dynamic mapeia automaticamente as propriedades de mesmo nome da entidade principal para o filtro
            // Na entidade principal da classificação, existe o SEQ apenas pelo fato de ser dynamic, não sendo necessário preenchimento.
            if (this.Seq != 0)
                AddExpression(this.Seq, p => p.Seq == this.Seq);

            return GetExpression();
        }
    }
}