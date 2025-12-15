using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;
using System.Linq;
using SMC.Academico.Common.Areas.ORT.Enums;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification : SMCSpecification<InstituicaoNivelTipoOrientacaoParticipacao>
    {
        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public bool? ObrigatorioOrientacao { get; set; }

        public long? SeqTipoIntercambio { get; set; }

        public long? SeqTipoVinculo { get; set; }

        public long[] Seqs { get; set; }

        public TipoParticipacaoOrientacao? TipoParticipacaoOrientacao { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public bool? PossuiTipoIntercambio { get; set; }

        public override Expression<Func<InstituicaoNivelTipoOrientacaoParticipacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqNivelEnsino, w => w.InstituicaoNivelTipoOrientacao.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino == this.SeqNivelEnsino.Value);
            AddExpression(SeqTipoOrientacao, w => w.InstituicaoNivelTipoOrientacao.SeqTipoOrientacao == this.SeqTipoOrientacao.Value);
            AddExpression(ObrigatorioOrientacao, w => w.ObrigatorioOrientacao == this.ObrigatorioOrientacao.Value);
            AddExpression(SeqTipoVinculo, w => w.InstituicaoNivelTipoOrientacao.InstituicaoNivelTipoVinculoAluno.SeqTipoVinculoAluno == this.SeqTipoVinculo.Value);
            AddExpression(SeqTipoIntercambio, w => w.InstituicaoNivelTipoOrientacao.InstituicaoNivelTipoTermoIntercambio.SeqTipoTermoIntercambio == this.SeqTipoIntercambio.Value);
            AddExpression(Seqs, w => this.Seqs.Contains(w.Seq));
            AddExpression(TipoParticipacaoOrientacao, w => w.TipoParticipacaoOrientacao == this.TipoParticipacaoOrientacao.Value);
            AddExpression(PossuiTipoIntercambio, w => w.InstituicaoNivelTipoOrientacao.SeqInstituicaoNivelTipoTermoIntercambio.HasValue == PossuiTipoIntercambio);

            return GetExpression();
        }
    }
}