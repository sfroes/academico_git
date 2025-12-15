using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class TipoOfertaFilterSpecification : SMCSpecification<TipoOferta>
    {
        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public override Expression<Func<TipoOferta, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(Seqs, x => Seqs.Contains(x.Seq));
            AddExpression(SeqTipoProcessoSeletivo, x => x.TiposProcessosSeletivos.Any(f => f.SeqTipoProcessoSeletivo == SeqTipoProcessoSeletivo));

            return GetExpression();
        }
    }
}