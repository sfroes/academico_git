using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class InstituicaoNivelTipoTermoIntercambioFilterSpecification : SMCSpecification<InstituicaoNivelTipoTermoIntercambio>
    {
        public long? SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public long? SeqInstituicaoNivelTipoVinculoAlunoDiferente { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public bool? ConcedeFormacao { get; set; }

        public bool? ConcedeFormacaoDiferente { get; set; }

        public long? Seq { get; set; }

        public override Expression<Func<InstituicaoNivelTipoTermoIntercambio, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqInstituicaoNivelTipoVinculoAluno, w => w.SeqInstituicaoNivelTipoVinculoAluno == SeqInstituicaoNivelTipoVinculoAluno);
            AddExpression(SeqInstituicaoNivelTipoVinculoAlunoDiferente, w => w.SeqInstituicaoNivelTipoVinculoAluno != SeqInstituicaoNivelTipoVinculoAlunoDiferente);
            AddExpression(SeqTipoTermoIntercambio, w => w.SeqTipoTermoIntercambio == SeqTipoTermoIntercambio);
            AddExpression(SeqInstituicaoNivel, w => w.InstituicaoNivelTipoVinculoAluno.SeqInstituicaoNivel == SeqInstituicaoNivel);
            AddExpression(ConcedeFormacaoDiferente, w => w.ConcedeFormacao != ConcedeFormacaoDiferente);
            
            return GetExpression();
        }
    }
}
