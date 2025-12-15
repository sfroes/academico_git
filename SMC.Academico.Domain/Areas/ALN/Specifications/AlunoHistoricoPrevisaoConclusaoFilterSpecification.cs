using System;
using System.Linq.Expressions;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class AlunoHistoricoPrevisaoConclusaoFilterSpecification : SMCSpecification<AlunoHistoricoPrevisaoConclusao>
    {
        public long SeqPessoaAtuacao { get; set; }

        public bool? AlunoHistoricoAtual { get; set; }

        public override Expression<Func<AlunoHistoricoPrevisaoConclusao, bool>> SatisfiedBy()
        {
            AddExpression(x => x.AlunoHistorico.SeqAluno == this.SeqPessoaAtuacao);
            if (AlunoHistoricoAtual.GetValueOrDefault())
                AddExpression(x => x.AlunoHistorico.Atual);

            return GetExpression();
        }
    }
}