using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class ConfiguracaoBeneficioFilterSpecification : SMCSpecification<ConfiguracaoBeneficio>
    {
        public long? SeqBeneficio { get; set; }

        public long? SeqInstituicaoNivelBeneficio { get; set; }
        
        public override Expression<Func<ConfiguracaoBeneficio, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqBeneficio, p => p.InstituicaoNivelBeneficio.SeqBeneficio == this.SeqBeneficio);
            AddExpression(this.SeqInstituicaoNivelBeneficio, p => p.SeqInstituicaoNivelBeneficio == this.SeqInstituicaoNivelBeneficio);

            return GetExpression();
        }
    }
}
