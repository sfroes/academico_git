using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class GrupoDocumentoRequeridoItemFilterSpecification : SMCSpecification<GrupoDocumentoRequeridoItem>
    {
        public long? SeqDocumentoRequerido { get; set; }

        public override Expression<Func<GrupoDocumentoRequeridoItem, bool>> SatisfiedBy()
        {                     
            AddExpression(SeqDocumentoRequerido, w => w.SeqDocumentoRequerido == this.SeqDocumentoRequerido.Value);            

            return GetExpression();
        }
    }
}
