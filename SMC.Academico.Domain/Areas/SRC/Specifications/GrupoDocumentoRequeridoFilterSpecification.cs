using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class GrupoDocumentoRequeridoFilterSpecification : SMCSpecification<GrupoDocumentoRequerido>
    {
        public long? SeqConfiguracaoEtapa { get; set; }
        
        public long[] SeqsGrupoDocumento { get; set; }


        public override Expression<Func<GrupoDocumentoRequerido, bool>> SatisfiedBy()
        {                     
            AddExpression(SeqConfiguracaoEtapa, w => w.SeqConfiguracaoEtapa == this.SeqConfiguracaoEtapa.Value);
            AddExpression(SeqsGrupoDocumento, w => this.SeqsGrupoDocumento.Contains(w.Seq));

            return GetExpression();
        }
    }
}