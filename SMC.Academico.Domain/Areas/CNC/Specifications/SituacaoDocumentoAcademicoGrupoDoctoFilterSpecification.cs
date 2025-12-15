using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class SituacaoDocumentoAcademicoGrupoDoctoFilterSpecification : SMCSpecification<SituacaoDocumentoAcademicoGrupoDocto>
    {
        public long? SeqSituacaoDocumento { get; set; }


        public override Expression<Func<SituacaoDocumentoAcademicoGrupoDocto, bool>> SatisfiedBy()
        {
            AddExpression(SeqSituacaoDocumento, x => x.SeqSituacaoDocumentoAcademico == SeqSituacaoDocumento);
            

            return GetExpression();
        }
    }
}