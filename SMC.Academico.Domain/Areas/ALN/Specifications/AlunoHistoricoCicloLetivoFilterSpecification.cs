using System;
using System.Linq.Expressions;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class AlunoHistoricoCicloLetivoFilterSpecification : SMCSpecification<AlunoHistoricoCicloLetivo>
    {
        public long? SeqPessoaAtuacaoAluno { get; set; }

        public long? SeqAlunoHistorico { get; set; }

        public long? SeqCicloLetivo { get; set; }
        public bool? Atual { get; set; }

        public override Expression<Func<AlunoHistoricoCicloLetivo, bool>> SatisfiedBy()
        {
            AddExpression(SeqPessoaAtuacaoAluno, h => h.AlunoHistorico.SeqAluno == this.SeqPessoaAtuacaoAluno);
            AddExpression(SeqAlunoHistorico, h => h.SeqAlunoHistorico == this.SeqAlunoHistorico);
            AddExpression(SeqCicloLetivo, h => h.SeqCicloLetivo == this.SeqCicloLetivo);
            AddExpression(Atual, x => x.AlunoHistorico.Atual == this.Atual);
            return GetExpression();
        }
    }
}