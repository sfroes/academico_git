using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class TipoDocumentoAcademicoServicoFilterSpecification : SMCSpecification<TipoDocumentoAcademicoServico>
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqServico { get; set; }

        public override Expression<Func<TipoDocumentoAcademicoServico, bool>> SatisfiedBy()
        {         
            AddExpression(SeqInstituicaoEnsino, x => x.TipoDocumentoAcademico.SeqInstituicaoEnsino == SeqInstituicaoEnsino.Value);
            AddExpression(SeqServico, x => x.SeqServico == SeqServico.Value);

            return GetExpression();
        }
    }
}
