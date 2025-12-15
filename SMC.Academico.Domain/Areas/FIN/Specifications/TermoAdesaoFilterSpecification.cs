using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class TermoAdesaoFilterSpecification : SMCSpecification<TermoAdesao>
    { 
        public long? SeqContrato { get; set; }

        public string Titulo { get; set; } 

        public bool? Ativo { get; set; }

        public long? SeqServico { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public override Expression<Func<TermoAdesao, bool>> SatisfiedBy()
        {
            AddExpression(this.Titulo, p => p.Titulo.Contains(Titulo));
            AddExpression(() => SeqContrato.HasValue && this.SeqContrato != 0 , p => p.SeqContrato == this.SeqContrato);
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo);
            AddExpression(this.SeqServico, p => p.SeqServico == SeqServico.Value);
            AddExpression(this.SeqTipoVinculoAluno, p => p.SeqTipoVinculoAluno == this.SeqTipoVinculoAluno);

            return GetExpression();

        }
    }
}
 