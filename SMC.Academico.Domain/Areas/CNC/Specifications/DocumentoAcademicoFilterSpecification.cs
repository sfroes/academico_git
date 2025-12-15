using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class DocumentoAcademicoFilterSpecification : SMCSpecification<DocumentoAcademico>
    {
        public long? SeqDocumentoGAD { get; set; }

        public override Expression<Func<DocumentoAcademico, bool>> SatisfiedBy()
        {
            AddExpression(SeqDocumentoGAD, x => x.SeqDocumentoGAD == SeqDocumentoGAD);

            return GetExpression();
        }
    }
}