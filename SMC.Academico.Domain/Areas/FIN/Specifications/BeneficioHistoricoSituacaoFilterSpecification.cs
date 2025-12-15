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
    public class BeneficioHistoricoSituacaoFilterSpecification : SMCSpecification<BeneficioHistoricoSituacao>
    {
        public long? SeqPessoaAtuacaoBeneficio { get; set; }

        public bool? Atual { get; set; }

        public override Expression<Func<BeneficioHistoricoSituacao, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqPessoaAtuacaoBeneficio, p => p.SeqPessoaAtuacaoBeneficio == this.SeqPessoaAtuacaoBeneficio);
            AddExpression(this.Atual, p => p.Atual == this.Atual);

            return GetExpression();
        }
    }
}
