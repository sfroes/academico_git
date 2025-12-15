using SMC.Academico.Domain.Areas.PES.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class ReferenciaFamiliarFilterSpecification : SMCSpecification<ReferenciaFamiliar>
    {
        public long? Seq { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public TipoParentesco? TipoParentesco { get; set; }

        public string NomeParente { get; set; }

        public override Expression<Func<ReferenciaFamiliar, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(SeqPessoaAtuacao, w => w.SeqPessoaAtuacao == SeqPessoaAtuacao.Value);
            AddExpression(TipoParentesco, w => w.TipoParentesco == TipoParentesco.Value);
            AddExpression(NomeParente, w => w.NomeParente.Contains(this.NomeParente));

            return GetExpression();
        }
    }
}