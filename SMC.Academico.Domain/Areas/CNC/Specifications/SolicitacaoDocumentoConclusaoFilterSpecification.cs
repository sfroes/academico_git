using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class SolicitacaoDocumentoConclusaoFilterSpecification : SMCSpecification<SolicitacaoDocumentoConclusao>
    {
        public long? SeqDocumentoConclusao { get; set; }

        public override Expression<Func<SolicitacaoDocumentoConclusao, bool>> SatisfiedBy()
        {
            AddExpression(SeqDocumentoConclusao, x => x.DocumentosAcademicos.Any(d => d.Seq == SeqDocumentoConclusao));
            return GetExpression();
        }
    }
}