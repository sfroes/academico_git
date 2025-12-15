using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class ConfiguracaoNumeracaoTrabalhoFilterSpecification : SMCSpecification<ConfiguracaoNumeracaoTrabalho>
    {
        public long? Seq { get; set; }
        public long? SeqEntidadeResponsavel { get; set; }
        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        public override Expression<Func<ConfiguracaoNumeracaoTrabalho, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, a => a.Seq == this.Seq.Value);
            AddExpression(this.SeqEntidadeResponsavel, a => a.SeqEntidadeResponsavel == this.SeqEntidadeResponsavel);
            AddExpression(this.SeqsEntidadesResponsaveis, a =>  this.SeqsEntidadesResponsaveis.Contains(a.SeqEntidadeResponsavel));

            return GetExpression();
        }
    }
}
