using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class InstituicaoNivelTipoDocumentoModeloRelatorioFilterSpecification : SMCSpecification<InstituicaoNivelTipoDocumentoModeloRelatorio>
    {
        public long? SeqInstituicaoNivelTipoDocumentoAcademico { get; set; }

        public override Expression<Func<InstituicaoNivelTipoDocumentoModeloRelatorio, bool>> SatisfiedBy()
        {
            AddExpression(SeqInstituicaoNivelTipoDocumentoAcademico, x => x.SeqInstituicaoNivelTipoDocumentoAcademico == this.SeqInstituicaoNivelTipoDocumentoAcademico.Value);

            return GetExpression();
        }
    }
}
