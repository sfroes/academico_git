using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;

using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ServicoTipoDocumentoFilterSpeficication : SMCSpecification<ServicoTipoDocumento>
    {
        public long? Seq { get; set; }
        public long? SeqServico { get; set; }

        public override Expression<Func<ServicoTipoDocumento, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(SeqServico, w => w.SeqServico == this.SeqServico);

            return GetExpression();
        }

    }
}
