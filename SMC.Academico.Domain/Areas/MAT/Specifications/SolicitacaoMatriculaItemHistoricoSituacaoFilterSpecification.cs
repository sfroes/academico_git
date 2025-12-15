using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.MAT.Specifications
{
    public class SolicitacaoMatriculaItemHistoricoSituacaoFilterSpecification : SMCSpecification<SolicitacaoMatriculaItemHistoricoSituacao>
    {
        public long? SeqItem { get; set; }

        public long? SeqSituacaoItemMatricula { get; set; }

        public override Expression<Func<SolicitacaoMatriculaItemHistoricoSituacao, bool>> SatisfiedBy()
        { 
            AddExpression(SeqItem, w => w.SeqSolicitacaoMatriculaItem == SeqItem.Value);
            AddExpression(SeqSituacaoItemMatricula, w => w.SeqSituacaoItemMatricula == SeqSituacaoItemMatricula.Value);

            return GetExpression();
        }
    }
}