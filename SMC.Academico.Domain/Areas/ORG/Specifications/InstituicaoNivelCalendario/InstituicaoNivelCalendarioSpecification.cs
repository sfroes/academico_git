using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoNivelCalendarioSpecification : SMCSpecification<InstituicaoNivelCalendario>
    {
        public long? SeqInstituicaoNivel { get; set; }

        public UsoCalendario? UsoCalendario { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public TipoAvaliacao? TipoAvaliacao { get; set; }

        public List<long> SeqsNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelCalendario, bool>> SatisfiedBy()
        {
            AddExpression(SeqInstituicaoNivel, i => i.SeqInstituicaoNivel == SeqInstituicaoNivel);
            AddExpression(UsoCalendario, i => i.UsoCalendario == UsoCalendario);
            AddExpression(SeqNivelEnsino, i => i.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(TipoAvaliacao, x => x.TiposEvento.Any(a => a.TipoAvaliacao == TipoAvaliacao));
            AddExpression(SeqsNivelEnsino, i => SeqsNivelEnsino.Contains(i.InstituicaoNivel.SeqNivelEnsino));

            return GetExpression();
        }
    }
}