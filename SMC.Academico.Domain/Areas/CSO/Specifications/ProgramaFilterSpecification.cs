using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class ProgramaFilterSpecification : SMCSpecification<Programa>
    {
        public long? Seq { get; set; }

        public string Nome { get; set; }

        public TipoPrograma? TipoPrograma { get; set; }

        public long? SeqSituacaoAtual { get; set; }

        public override Expression<Func<Programa, bool>> SatisfiedBy()
        {
            var agora = DateTime.Now;

            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(Nome, w => w.Nome.Contains(this.Nome));
            AddExpression(TipoPrograma, w => w.TipoPrograma == this.TipoPrograma);
            AddExpression(SeqSituacaoAtual, w => w.HistoricoSituacoes.FirstOrDefault(h => agora >= h.DataInicio && (!h.DataFim.HasValue || agora <= h.DataFim.Value)).SituacaoEntidade.Seq == this.SeqSituacaoAtual);

            return GetExpression();
        }
    }
}
