using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class DocumentoConclusaoApostilamentoFilterSpecification : SMCSpecification<DocumentoConclusaoApostilamento>
    {
        public long? SeqDocumentoConclusao { get; set; }

        public override Expression<Func<DocumentoConclusaoApostilamento, bool>> SatisfiedBy()
        {
            AddExpression(SeqDocumentoConclusao, x => x.SeqDocumentoConclusao == this.SeqDocumentoConclusao.Value);

            return GetExpression();
        }
    }
}
