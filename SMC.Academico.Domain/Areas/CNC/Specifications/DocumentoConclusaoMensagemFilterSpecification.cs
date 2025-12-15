using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class DocumentoConclusaoMensagemFilterSpecification : SMCSpecification<DocumentoConclusaoMensagem>
    {
        public long? SeqDocumentoConclusao { get; set; }

        public override Expression<Func<DocumentoConclusaoMensagem, bool>> SatisfiedBy()
        {
            AddExpression(SeqDocumentoConclusao, x => x.SeqDocumentoConclusao == this.SeqDocumentoConclusao.Value);

            return GetExpression();
        }
    }
}
