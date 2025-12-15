using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class TipoDocumentoAcademicoTagFilterSpecification : SMCSpecification<TipoDocumentoAcademicoTag>
    {
        public long? SeqTag { get; set; }

        public override Expression<Func<TipoDocumentoAcademicoTag, bool>> SatisfiedBy()
        {
            AddExpression(SeqTag, x => x.SeqTag == SeqTag);

            return GetExpression();
        }
    }
}
