using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ConvocacaoOfertaFilterSpecification : SMCSpecification<ConvocacaoOferta>
    {
        public long? Seq { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqProcessoSeletivoOferta { get; set; }

        public long[] SeqsProcessoSeletivoOferta { get; set; }

        public long? SeqConvocacao { get; set; }

        public long[] SeqsConvocacao { get; set; }

        public override Expression<Func<ConvocacaoOferta, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == Seq);
            AddExpression(this.SeqProcessoSeletivo, p => p.ProcessoSeletivoOferta.SeqProcessoSeletivo == SeqProcessoSeletivoOferta);
            AddExpression(this.SeqConvocacao, p => p.SeqConvocacao == SeqConvocacao);
            AddExpression(this.SeqsConvocacao, p => SeqsConvocacao.Contains(p.SeqConvocacao));
            AddExpression(this.SeqProcessoSeletivoOferta, p => p.SeqProcessoSeletivoOferta == SeqProcessoSeletivoOferta);
            AddExpression(this.SeqsProcessoSeletivoOferta, p => SeqsProcessoSeletivoOferta.Contains(p.SeqProcessoSeletivoOferta));

            return GetExpression();
        }
    }
}