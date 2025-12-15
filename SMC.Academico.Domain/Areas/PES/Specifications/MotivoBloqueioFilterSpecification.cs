using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class MotivoBloqueioFilterSpecification : SMCSpecification<MotivoBloqueio>
    {
        public List<long> SeqTipoBloqueio { get; set; }

        public string Token { get; set; }

        public MotivoBloqueioFilterSpecification()
        {
            SetOrderBy("Descricao");
        }

        public override Expression<Func<MotivoBloqueio, bool>> SatisfiedBy()
        {
            AddExpression(SeqTipoBloqueio, w => SeqTipoBloqueio.Any(x => x == w.SeqTipoBloqueio));
            AddExpression(Token, w => w.Token == this.Token);

            return GetExpression();
        }
    }
}