using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class TipoFormacaoEspecificaFilterSpecification : SMCSpecification<TipoFormacaoEspecifica>
    {
        public bool? Ativo { get; set; } = true;

        public ClasseTipoFormacao ClasseTipoFormacao { get; set; }

        public TipoCurso? TipoCurso { get; set; }

        public string Token { get; set; }

        public override Expression<Func<TipoFormacaoEspecifica, bool>> SatisfiedBy()
        {
            AddExpression(Ativo, w => w.Ativo == Ativo.Value);
            AddExpression(TipoCurso, w => w.TiposCurso.Any(t => t.TipoCurso == TipoCurso.Value));
            AddExpression(w => ClasseTipoFormacao == ClasseTipoFormacao.Nenhum || w.ClasseTipoFormacao == ClasseTipoFormacao);
            AddExpression(Token, w => w.Token == Token);

            return GetExpression();
        }
    }
}