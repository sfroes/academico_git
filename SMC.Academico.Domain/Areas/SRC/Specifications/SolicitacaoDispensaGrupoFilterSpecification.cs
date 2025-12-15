using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class SolicitacaoDispensaGrupoFilterSpecification : SMCSpecification<SolicitacaoDispensaGrupo>
    {
        public long? Seq { get; set; }

        public long? SeqDiferente { get; set; }

        public long? SeqDispensa { get; set; }

        public long? SeqDispensaDiferente { get; set; }

        public long? SeqSolicitacaoDispensa { get; set; }

        public List<long> SeqsOrigensInternas { get; set; }

        public List<long> SeqsOrigensExternas { get; set; }

        public List<long> SeqsOrigens { get; set; }

        public List<long> SeqsDestinos { get; set; }

        public long? SeqSolicitacaoDispensaDestino { get; set; }

        public override Expression<Func<SolicitacaoDispensaGrupo, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqDiferente, x => x.Seq != SeqDiferente);
            AddExpression(SeqDispensa, x => x.SeqDispensa == SeqDispensa);
            AddExpression(SeqSolicitacaoDispensa, w => w.SeqSolicitacaoDispensa == this.SeqSolicitacaoDispensa.Value);
            AddExpression(SeqsOrigensInternas, x => x.OrigensInternas.Any(o => SeqsOrigensInternas.Contains(o.SeqSolicitacaoDispensaOrigemInterna)));
            AddExpression(SeqsOrigensExternas, x => x.OrigensExternas.Any(o => SeqsOrigensExternas.Contains(o.SeqSolicitacaoDispensaOrigemExterna)));
            AddExpression(SeqsOrigens, x => x.OrigensExternas.Any(o => SeqsOrigens.Contains(o.SeqSolicitacaoDispensaOrigemExterna)) || x.OrigensInternas.Any(o => SeqsOrigens.Contains(o.SeqSolicitacaoDispensaOrigemInterna)));
            AddExpression(SeqsDestinos, x => x.Destinos.Any(o => SeqsDestinos.Contains(o.SeqSolicitacaoDispensaDestino)));
            AddExpression(SeqSolicitacaoDispensaDestino, x => x.Destinos.Any(a=>a.SeqSolicitacaoDispensaDestino == SeqSolicitacaoDispensaDestino));

            return GetExpression();
        }
    }
}