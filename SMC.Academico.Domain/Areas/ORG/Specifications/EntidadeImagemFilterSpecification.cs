using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class EntidadeImagemFilterSpecification : SMCSpecification<EntidadeImagem>
    {
        public long? Seq { get; set; }
        public long? SeqEntidade { get; set; }
        public TipoImagem? TipoImagem { get; set; }


        public override Expression<Func<EntidadeImagem, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, e => e.Seq == this.Seq);
            AddExpression(this.SeqEntidade, e => e.SeqEntidade == this.SeqEntidade);
            AddExpression(this.TipoImagem, e => e.TipoImagem == this.TipoImagem);

            return GetExpression();
        }
    }
}
