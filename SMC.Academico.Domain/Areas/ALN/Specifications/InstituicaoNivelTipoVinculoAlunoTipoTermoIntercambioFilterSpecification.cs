using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class InstituicaoNivelTipoVinculoAlunoTipoTermoIntercambioFilterSpecification : SMCSpecification<InstituicaoNivelTipoVinculoAluno>
    {
        public long? SeqParceriaIntercambio { get; set; }

        public override Expression<Func<InstituicaoNivelTipoVinculoAluno, bool>> SatisfiedBy()
        {
            AddExpression(SeqParceriaIntercambio, w => w.TiposTermoIntercambio.Any(t => t.TipoTermoIntercambio.ParceriasIntercambioTipoTermo.Any(p => p.SeqParceriaIntercambio == SeqParceriaIntercambio)));
            return GetExpression();
        }
    }
}