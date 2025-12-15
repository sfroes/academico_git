using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class DocumentoAcademicoCurriculoFilterSpecification : SMCSpecification<DocumentoAcademicoCurriculo>
    {
        public long? SeqDocumentoAcademicoGAD { get; set; }
        public ClasseSituacaoDocumento? ClasseSituacaoDocumento { get; set; }

        public override Expression<Func<DocumentoAcademicoCurriculo, bool>> SatisfiedBy()
        {
            AddExpression(SeqDocumentoAcademicoGAD, x => x.SeqDocumentoGAD == SeqDocumentoAcademicoGAD);
            AddExpression(ClasseSituacaoDocumento, x => x.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento == ClasseSituacaoDocumento);

            return GetExpression();
        }
    }
}
