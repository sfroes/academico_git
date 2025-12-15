using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class PlanoEstudoFilterSpecification : SMCSpecification<PlanoEstudo>
    {
        public long? Seq { get; set; }

        public long? SeqAluno { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public bool? Atual { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqAlunoHistoricoCicloLetivo { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long[] Seqs { get; set; }

        public long[] SeqsMatrizCurricularOferta { get; set; }

        public long[] SeqsDivisaoTurma { get; set; }

		public bool? ComItensNoPlanoDeEstudo { get; set; }

        //public List<long> CicloLetivoDiferente { get; set; }

        public long? CicloLetivoDiferente { get; set; }

        public override Expression<Func<PlanoEstudo, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.Seqs, p => Seqs.Contains(p.Seq));
            AddExpression(this.SeqAluno, p => p.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno == this.SeqAluno);
            AddExpression(this.SeqSolicitacaoServico, p => p.SeqSolicitacaoServico == this.SeqSolicitacaoServico);
            AddExpression(this.Atual, p => p.Atual == this.Atual);
            AddExpression(this.SeqTurma, p => p.Itens.Any(a => a.DivisaoTurma.SeqTurma == this.SeqTurma));
            AddExpression(this.SeqCicloLetivo, p => p.AlunoHistoricoCicloLetivo.SeqCicloLetivo == this.SeqCicloLetivo);
            AddExpression(this.SeqAlunoHistoricoCicloLetivo, p => p.SeqAlunoHistoricoCicloLetivo == SeqAlunoHistoricoCicloLetivo);
            AddExpression(this.SeqDivisaoTurma, p => p.Itens.Any(a => a.SeqDivisaoTurma == SeqDivisaoTurma));
            AddExpression(this.SeqsMatrizCurricularOferta, p => SeqsMatrizCurricularOferta.Contains(p.SeqMatrizCurricularOferta.Value));
            AddExpression(this.SeqsDivisaoTurma, p => p.Itens.Any(a => SeqsDivisaoTurma.Contains(a.SeqDivisaoTurma.Value)));
			AddExpression(this.ComItensNoPlanoDeEstudo, p => p.Itens.Any() == this.ComItensNoPlanoDeEstudo.Value);
            //AddExpression(CicloLetivoDiferente, p => CicloLetivoDiferente.Contains(p.AlunoHistoricoCicloLetivo.SeqCicloLetivo));
            AddExpression(CicloLetivoDiferente, p => p.AlunoHistoricoCicloLetivo.SeqCicloLetivo != CicloLetivoDiferente);


            return GetExpression();
        }
    }
}