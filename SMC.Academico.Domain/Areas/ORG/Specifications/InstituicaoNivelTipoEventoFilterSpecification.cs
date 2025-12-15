using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoNivelTipoEventoFilterSpecification : SMCSpecification<InstituicaoNivelTipoEvento>
    {
        public long? Seq { get; set; }
        public long? SeqNivelEnsino { get; set; }
        public string TokenTipoEventoAgd { get; set; }
        public long? SeqInstituicaoEnsino { get; set; }

        public TipoAvaliacao? TipoAvaliacao { get; set; }

        public override Expression<Func<InstituicaoNivelTipoEvento, bool>> SatisfiedBy()
        {
            AddExpression(Seq, p => p.Seq == Seq);
            AddExpression(SeqNivelEnsino, p => p.InstituicaoNivelCalendario.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(TokenTipoEventoAgd, p => p.TokenTipoEventoAgd == TokenTipoEventoAgd);
            AddExpression(TipoAvaliacao, x => x.TipoAvaliacao == TipoAvaliacao);
            AddExpression(SeqInstituicaoEnsino, x => x.InstituicaoNivelCalendario.InstituicaoNivel.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            return GetExpression();
        }
    }
}