using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class QualisPeriodicoFilterSpecification : SMCSpecification<QualisPeriodico>
    {
        public long? SeqClassificacaoPeriodico { get; set; }

        public string CodigoISSN { get; set; }

        public string Descricao { get; set; }

        public string DescAreaAvaliacao { get; set; }

        public QualisCapes? QualisCapes { get; set; }

        public bool? ClassificacaoPeriodicoAtual { get; set; }

        public override Expression<Func<QualisPeriodico, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqClassificacaoPeriodico, p => p.Periodico.SeqClassificacaoPeriodico == this.SeqClassificacaoPeriodico);
            AddExpression(CodigoISSN, p => p.CodigoISSN == CodigoISSN);
            AddExpression(Descricao, p => p.Periodico.Descricao.ToLower().Contains(Descricao.ToLower()));
            AddExpression(DescAreaAvaliacao, p => p.DescricaoAreaAvaliacao.ToLower().Contains(DescAreaAvaliacao.ToLower()));
            AddExpression(QualisCapes, p => p.QualisCapes == QualisCapes);
            AddExpression(ClassificacaoPeriodicoAtual, p => p.Periodico.ClassificacaoPeriodico.Atual == ClassificacaoPeriodicoAtual);

            return GetExpression();
        }
    }
}
