using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class TipoConfiguracaoGrupoCurricularFilterSpecification : SMCSpecification<TipoConfiguracaoGrupoCurricular>
    {
        public bool? Raiz { get; set; }

        public long? SeqTipoConfiguracaoGrupoCurricularSuperior { get; set; }

        public override Expression<Func<TipoConfiguracaoGrupoCurricular, bool>> SatisfiedBy()
        {
            AddExpression(this.Raiz, p => p.Raiz == this.Raiz);
            AddExpression(this.SeqTipoConfiguracaoGrupoCurricularSuperior, p =>
                            p.TiposConfiguracoesGrupoCurricularSuperiores.Any(a => a.Seq == this.SeqTipoConfiguracaoGrupoCurricularSuperior));

            return GetExpression();
        }
    }
}
