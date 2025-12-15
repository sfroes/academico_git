using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class JustificativaSolicitacaoServicoFilterSpecification : SMCSpecification<JustificativaSolicitacaoServico>
    {
        public long? SeqServico { get; set; }

        public bool? Ativo { get; set; }

        public string Descricao { get; set; }

        public override Expression<Func<JustificativaSolicitacaoServico, bool>> SatisfiedBy()
        {
            AddExpression(SeqServico, x => x.SeqServico == SeqServico);
            AddExpression(Ativo, x => x.Ativo == Ativo);
            AddExpression(Descricao, x => x.Descricao == Descricao);

            return GetExpression();
        }
    }
}