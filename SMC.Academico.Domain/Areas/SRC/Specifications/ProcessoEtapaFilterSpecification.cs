using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ProcessoEtapaFilterSpecification : SMCSpecification<ProcessoEtapa>
    {
        public long? Seq { get; set; }

        public long? SeqEtapaSGF { get; set; }

        public long[] SeqsProcessoEtapa { get; set; }

        public long? SeqProcesso { get; set; }

        public List<long> SeqsProcessos { get; set; }

        public int? Ordem { get; set; }

        public override Expression<Func<ProcessoEtapa, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq);
            AddExpression(SeqEtapaSGF, w => w.SeqEtapaSgf == this.SeqEtapaSGF.Value);
            AddExpression(SeqsProcessoEtapa, w => SeqsProcessoEtapa.Contains(w.Seq));
            AddExpression(SeqsProcessos, w => SeqsProcessos.Contains(w.SeqProcesso));
            AddExpression(SeqProcesso, w => w.SeqProcesso == SeqProcesso.Value);
            AddExpression(Ordem, w => w.Ordem == Ordem.Value);

            return GetExpression();
        }
    }
}