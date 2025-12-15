using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class MotivoAlteracaoBeneficioFilterSpecification : SMCSpecification<MotivoAlteracaoBeneficio>
    {
        public long? Seq { get; set; }

        public long? SeqIntitiuicaoEnsino { get; set; }

        public string Token { get; set; }

        public override Expression<Func<MotivoAlteracaoBeneficio, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, a => a.Seq == this.Seq);
            AddExpression(this.SeqIntitiuicaoEnsino, a => a.SeqInstituicaoEnsino == this.SeqIntitiuicaoEnsino);
            AddExpression(this.Token, a => a.Token == this.Token);

            return GetExpression();
        }
    }
}
