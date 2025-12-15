using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class InstituicaoTipoEntidadeHierarquiasClassificacaoFilterSpecification : SMCSpecification<InstituicaoTipoEntidadeHierarquiaClassificacao>
    {
        public long? SeqInstituicaoTipoEntidade { get; set; }

        public override Expression<Func<InstituicaoTipoEntidadeHierarquiaClassificacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqInstituicaoTipoEntidade, w => this.SeqInstituicaoTipoEntidade.Value == w.SeqInstituicaoTipoEntidade);

            return GetExpression();
        }
    }
}
