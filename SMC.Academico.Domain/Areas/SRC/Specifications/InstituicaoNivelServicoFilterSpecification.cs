using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class InstituicaoNivelServicoFilterSpecification : SMCSpecification<InstituicaoNivelServico>
    {
        public long? Seq { get; set; }

        public long? SeqServico { get; set; }

        public long? SeqTipoServico { get; set; }

        public override Expression<Func<InstituicaoNivelServico, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq.Value);
            AddExpression(SeqServico, w => w.SeqServico == SeqServico.Value);
            AddExpression(SeqTipoServico, w => w.Servico.SeqTipoServico == SeqTipoServico.Value);

            return GetExpression();
        }
    }
}