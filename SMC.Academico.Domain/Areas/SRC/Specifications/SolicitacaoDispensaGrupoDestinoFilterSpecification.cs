using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class SolicitacaoDispensaGrupoDestinoFilterSpecification : SMCSpecification<SolicitacaoDispensaGrupoDestino>
    {
        public long? SeqSolicitacaoDispensaDestino { get; set; }

        public override Expression<Func<SolicitacaoDispensaGrupoDestino, bool>> SatisfiedBy()
        {
            AddExpression(SeqSolicitacaoDispensaDestino, x => x.SeqSolicitacaoDispensaDestino == SeqSolicitacaoDispensaDestino);

            return GetExpression();
        }
    }
}
