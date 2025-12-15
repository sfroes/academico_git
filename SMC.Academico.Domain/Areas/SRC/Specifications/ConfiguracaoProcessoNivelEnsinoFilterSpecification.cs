using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ConfiguracaoProcessoNivelEnsinoFilterSpecification : SMCSpecification<ConfiguracaoProcessoNivelEnsino>
    {
        public long[] SeqsNivelEnsino { get; set; }

        public long? SeqProcesso { get; set; }

        public override Expression<Func<ConfiguracaoProcessoNivelEnsino, bool>> SatisfiedBy()
        {
            AddExpression(SeqsNivelEnsino, a => SeqsNivelEnsino.Contains(a.SeqNivelEnsino));
            AddExpression(SeqProcesso, x => x.ConfiguracaoProcesso.SeqProcesso == SeqProcesso.Value);

            return GetExpression();
        }
    }
}
