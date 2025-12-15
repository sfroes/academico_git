using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class DocumentoConclusaoFormacaoFilterSpecification : SMCSpecification<DocumentoConclusaoFormacao>
    {
        public long? SeqDocumentoConclusao { get; set; }

        public long? SeqDocumentoConclusaoApostilamento { get; set; }

        public override Expression<Func<DocumentoConclusaoFormacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqDocumentoConclusao, x => x.SeqDocumentoConclusao == this.SeqDocumentoConclusao.Value);
            AddExpression(SeqDocumentoConclusaoApostilamento, x => x.SeqDocumentoConclusaoApostilamento == this.SeqDocumentoConclusaoApostilamento.Value);

            return GetExpression();
        }
    }
}
