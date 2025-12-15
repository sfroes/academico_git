using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class DocumentoAcademicoHistoricoSituacaoFilterSpecification : SMCSpecification<DocumentoAcademicoHistoricoSituacao>
    {
        public long? SeqDocumentoAcademico { get; set; }

        public override Expression<Func<DocumentoAcademicoHistoricoSituacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqDocumentoAcademico, x => x.SeqDocumentoAcademico == SeqDocumentoAcademico);

            return GetExpression();
        }
    }
}
