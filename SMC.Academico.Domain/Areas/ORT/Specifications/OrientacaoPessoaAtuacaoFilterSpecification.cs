using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class OrientacaoPessoaAtuacaoFilterSpecification : SMCSpecification<OrientacaoPessoaAtuacao>
    {
        public long? Seq { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public bool? OrientacaoTurma { get; set; }

        public long[] SeqsTipoOrientacao { get; set; }

        public override Expression<Func<OrientacaoPessoaAtuacao, bool>> SatisfiedBy()
        { 
            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(SeqPessoaAtuacao, w => w.SeqPessoaAtuacao == SeqPessoaAtuacao.Value);
            AddExpression(OrientacaoTurma, w => w.Orientacao.TipoOrientacao.OrientacaoTurma == OrientacaoTurma.Value);
            AddExpression(SeqsTipoOrientacao, w => SeqsTipoOrientacao.Contains(w.Orientacao.SeqTipoOrientacao));

            return GetExpression();
        }
    }
}