using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ProcessoSeletivoOfertaFilterSpecification : SMCSpecification<ProcessoSeletivoOferta>
    {
        public long? Seq { get; set; }

        public long? SeqTipoOferta { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqCampanhaOferta { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long[] Seqs { get; set; }

        public override Expression<Func<ProcessoSeletivoOferta, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.Seqs, p => this.Seqs.Contains(p.Seq));
            AddExpression(this.SeqTipoOferta, p => p.SeqHierarquiaOfertaGpi.HasValue && p.CampanhaOferta.SeqTipoOferta == this.SeqTipoOferta);
            AddExpression(this.SeqProcessoSeletivo, p => p.SeqProcessoSeletivo == this.SeqProcessoSeletivo);
            AddExpression(this.SeqCampanhaOferta, p => p.SeqCampanhaOferta == this.SeqCampanhaOferta);
            AddExpression(this.SeqDivisaoTurma, p => p.CampanhaOferta.Itens.Any(a => a.Turma.DivisoesTurma.Any(d => d.Seq == this.SeqDivisaoTurma)));

            return GetExpression();
        }
    }
}