using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.Specifications
{
    public class ColaboradorInstituicaoExternaFilterSpecification : SMCSpecification<ColaboradorInstituicaoExterna>
    {
        public long? SeqColaborador { get; set; }

        public long[] Seqs { get; set; }

        public bool? Ativo { get; set; }

        public long[] SeqsColaboradores { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public long[] SeqsInstituicoesExternas { get; set; }

        public override Expression<Func<ColaboradorInstituicaoExterna, bool>> SatisfiedBy()
        {
            this.SetOrderBy(o => o.InstituicaoExterna.Nome);

            AddExpression(SeqColaborador, p => p.SeqColaborador == this.SeqColaborador);
            AddExpression(Seqs, p => Seqs.Contains(p.SeqInstituicaoExterna));
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo);
            AddExpression(SeqsColaboradores, p => SeqsColaboradores.Contains(p.SeqColaborador));
            AddExpression(SeqInstituicaoEnsino, p => p.InstituicaoExterna.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(SeqInstituicaoExterna, p => p.SeqInstituicaoExterna == SeqInstituicaoExterna);
            AddExpression(SeqsInstituicoesExternas, p => SeqsInstituicoesExternas.Contains(p.SeqInstituicaoExterna));

            return GetExpression();
        }
    }
}