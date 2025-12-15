using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class InstituicaoNivelTipoVinculoAlunoFilterSpecification : SMCSpecification<InstituicaoNivelTipoVinculoAluno>
    {
        public long? Seq { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqInstituicao { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public TipoVinculoAlunoFinanceiro[] TiposVinculoAlunoFinanceiro { get; set; }

        public InstituicaoNivelTipoVinculoAlunoFilterSpecification()
        {
            this.SetOrderBy(w => w.InstituicaoNivel.NivelEnsino.Descricao);
            this.SetOrderBy(w => w.TipoVinculoAluno.Descricao);
        }

        public override Expression<Func<InstituicaoNivelTipoVinculoAluno, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(SeqInstituicaoNivel, w => w.SeqInstituicaoNivel == SeqInstituicaoNivel.Value);
            AddExpression(SeqInstituicao, w => w.InstituicaoNivel.SeqInstituicaoEnsino == SeqInstituicao.Value);
            AddExpression(SeqNivelEnsino, w => w.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino.Value);
            AddExpression(SeqTipoVinculoAluno, w => w.SeqTipoVinculoAluno == SeqTipoVinculoAluno.Value);
            AddExpression(SeqTipoTermoIntercambio, w => w.TiposTermoIntercambio.Any(x => x.SeqTipoTermoIntercambio == SeqTipoTermoIntercambio.Value));
            AddExpression(SeqTipoOrientacao, w => w.TiposOrientacao.Any(x => x.SeqTipoOrientacao == SeqTipoOrientacao.Value));
            AddExpression(SeqTipoProcessoSeletivo, w => w.FormasIngresso.Any(fi => fi.TiposProcessoSeletivo.Any(tps => tps.SeqTipoProcessoSeletivo == SeqTipoProcessoSeletivo)));
            AddExpression(TiposVinculoAlunoFinanceiro, w => TiposVinculoAlunoFinanceiro.Contains(w.TipoVinculoAluno.TipoVinculoAlunoFinanceiro));
            return GetExpression();
        }
    }
}