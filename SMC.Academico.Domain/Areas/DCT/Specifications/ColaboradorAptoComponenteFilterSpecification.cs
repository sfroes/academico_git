using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.Specifications
{
    public class ColaboradorAptoComponenteFilterSpecification : SMCSpecification<ColaboradorAptoComponente>
    {
        public long? Seq { get; set; }

        public long? SeqAtuacaoColaborador { get; set; }

        public override Expression<Func<ColaboradorAptoComponente, bool>> SatisfiedBy()
        {
            AddExpression(Seq, c => c.Seq == this.Seq);
            AddExpression(SeqAtuacaoColaborador, c => c.SeqAtuacaoColaborador == this.SeqAtuacaoColaborador);
            
            return GetExpression();
        }
    }
}