using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ConfiguracaoProcessoTipoVinculoAlunoFilterSpecification : SMCSpecification<ConfiguracaoProcessoTipoVinculoAluno>
    {
        public long[] SeqsTipoVinculoAluno { get; set; }

        public long? SeqProcesso { get; set; }

        public override Expression<Func<ConfiguracaoProcessoTipoVinculoAluno, bool>> SatisfiedBy()
        {
            AddExpression(SeqsTipoVinculoAluno, a => SeqsTipoVinculoAluno.Contains(a.SeqTipoVinculoAluno));
            AddExpression(SeqProcesso, x => x.ConfiguracaoProcesso.SeqProcesso == SeqProcesso.Value);

            return GetExpression();
        }
    }
}
