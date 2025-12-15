using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoNivelSistemaOrigemFilterSpecification : SMCSpecification<InstituicaoNivelSistemaOrigem>
    {
        public long? SeqInstituicaoNivel { get; set; }

        public string TokenSistemaOrigemGAD { get; set; }

        public long? NivelEnsino { get; set; }

        public UsoSistemaOrigem? UsoSistemaOrigem { get; set; }

        public override Expression<Func<InstituicaoNivelSistemaOrigem, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqInstituicaoNivel, a => a.SeqInstituicaoNivel == this.SeqInstituicaoNivel);
            AddExpression(this.TokenSistemaOrigemGAD, a => a.TokenSistemaOrigemGAD == this.TokenSistemaOrigemGAD);
            AddExpression(this.UsoSistemaOrigem, a => a.UsoSistemaOrigem == this.UsoSistemaOrigem);            

            return GetExpression();
        }
    }
}