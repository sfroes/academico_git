using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class BeneficioHistoricoValorAuxilioFilterSpecification : SMCSpecification<BeneficioHistoricoValorAuxilio>
    {
        public long? SeqBeneficio { get; set; }

        public long? SeqInstituicaoNivelBeneficio { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public override Expression<Func<BeneficioHistoricoValorAuxilio, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqBeneficio, p => p.InstituicaoNivelBeneficio.SeqBeneficio == this.SeqBeneficio);
            AddExpression(this.SeqInstituicaoNivelBeneficio, p => p.SeqInstituicaoNivelBeneficio == this.SeqInstituicaoNivelBeneficio);
            AddExpression(this.SeqNivelEnsino, p => p.InstituicaoNivelBeneficio.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino);

            return GetExpression();


        }
    }
}
