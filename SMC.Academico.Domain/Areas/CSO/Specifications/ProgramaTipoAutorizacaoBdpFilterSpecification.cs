using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class ProgramaTipoAutorizacaoBdpFilterSpecification : SMCSpecification<ProgramaTipoAutorizacaoBdp>
    {
        public long? SeqPrograma { get; set; }

        public TipoAutorizacao? TipoAutorizacao { get; set; }

        public override Expression<Func<ProgramaTipoAutorizacaoBdp, bool>> SatisfiedBy()
        {
            AddExpression(SeqPrograma, w => w.SeqPrograma == this.SeqPrograma);
            AddExpression(TipoAutorizacao, w => w.TipoAutorizacao == this.TipoAutorizacao);
            
            return GetExpression();
        }
    }
}
