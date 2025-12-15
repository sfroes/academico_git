using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class PeriodicoFilterSpecification : SMCSpecification<Periodico>
    {
        public long? Seq { get; set; }       

        public long? SeqClassificacaoPeriodico { get; set; }

        public string Descricao { get; set; }

        public string CodigoISSN { get; set; }

        public string DescAreaAvaliacao { get; set; }

        public QualisCapes? QualisCapes { get; set; }

        public override Expression<Func<Periodico, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqClassificacaoPeriodico, p => this.SeqClassificacaoPeriodico == p.SeqClassificacaoPeriodico);
            AddExpression(this.Descricao, p => p.Descricao.Contains(this.Descricao));
            AddExpression(this.CodigoISSN, p => p.QualisPeriodico.Count(y => y.CodigoISSN.Contains(this.CodigoISSN)) > 0);
            AddExpression(this.DescAreaAvaliacao, p => p.QualisPeriodico.Any(y => y.DescricaoAreaAvaliacao.Contains(this.DescAreaAvaliacao)));
            AddExpression(this.QualisCapes, p => p.QualisPeriodico.Count(y => y.QualisCapes == this.QualisCapes) > 0);

            return GetExpression();
        }
            
    }
}
