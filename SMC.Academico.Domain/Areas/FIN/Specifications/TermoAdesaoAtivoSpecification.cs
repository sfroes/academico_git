using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class TermoAdesaoAtivoSpecification : SMCSpecification<TermoAdesao>
    { 
        public long SeqContrato { get; set; }

        public long SeqServico { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public override Expression<Func<TermoAdesao, bool>> SatisfiedBy()
        {
            //return prop => prop.SeqContrato == SeqContrato 
            //               && prop.Ativo
            //               && prop.SeqServico == SeqServico
            //               && prop.SeqTipoVinculoAluno == SeqTipoVinculoAluno;

            AddExpression(p => p.SeqContrato == this.SeqContrato);
            AddExpression(p => p.Ativo);
            AddExpression(p => p.SeqServico == this.SeqServico);
            AddExpression(p => p.SeqTipoVinculoAluno == this.SeqTipoVinculoAluno);

            return GetExpression();
        }
    }
}
 