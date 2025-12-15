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
    public class BeneficioEnvioNotificacaoFilterSpecification : SMCSpecification<BeneficioEnvioNotificacao>
    {
        public long? SeqPessoaAtuacaoBeneficio { get; set; }

        public override Expression<Func<BeneficioEnvioNotificacao, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqPessoaAtuacaoBeneficio, p => p.SeqPessoaAtuacaoBeneficio == this.SeqPessoaAtuacaoBeneficio);

            return GetExpression();
        }
    }
}
