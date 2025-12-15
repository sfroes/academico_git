using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Linq;
using SMC.Academico.Common.Areas.PES.Enums;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class InstituicaoMotivoBloqueioFilterSpecification : SMCSpecification<InstituicaoMotivoBloqueio>
    {
        public List<long> SeqTipoBloqueio { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public bool RemoverIntegracao { get; set; }

        public override Expression<Func<InstituicaoMotivoBloqueio, bool>> SatisfiedBy()
        {
            AddExpression(SeqTipoBloqueio, w => SeqTipoBloqueio.Any(x => x == w.MotivoBloqueio.SeqTipoBloqueio));
            AddExpression(SeqInstituicaoEnsino, w => w.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(() => RemoverIntegracao, w => w.MotivoBloqueio.FormaBloqueio != FormaBloqueio.Integracao);

            return GetExpression();
        }
    }
}