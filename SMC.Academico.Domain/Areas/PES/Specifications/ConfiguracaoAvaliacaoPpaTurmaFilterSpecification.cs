using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class ConfiguracaoAvaliacaoPpaTurmaFilterSpecification : SMCSpecification<ConfiguracaoAvaliacaoPpaTurma>
    {
        public long? Seq { get; set; }
        public long? SeqConfiguracaoAvaliacaoPpa { get; set; }
        public long? SeqTurma { get; set; }
        public int? CodigoInstrumento { get; set; }
        public DateTime? DataInclusao { get; set; }

        public override Expression<Func<ConfiguracaoAvaliacaoPpaTurma, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == Seq);
            AddExpression(SeqTurma, p => p.SeqTurma == SeqTurma);
            AddExpression(SeqConfiguracaoAvaliacaoPpa, p => p.SeqConfiguracaoAvaliacaoPpa == SeqConfiguracaoAvaliacaoPpa);
            AddExpression(CodigoInstrumento, p => p.CodigoInstrumento == CodigoInstrumento);
            AddExpression(DataInclusao, a => a.DataInclusao == DataInclusao);

            return GetExpression();
        }
    }
}
