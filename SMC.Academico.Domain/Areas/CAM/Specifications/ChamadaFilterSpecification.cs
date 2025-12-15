using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ChamadaFilterSpecification : SMCSpecification<Chamada>
    {
        public long? SeqCampanha { get; set; }

        public long? SeqConvocacao { get; set; }

        public TipoChamada? TipoChamada { get; set; }

        public override Expression<Func<Chamada, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqCampanha, p => p.Convocacao.ProcessoSeletivo.SeqCampanha == this.SeqCampanha);
            AddExpression(this.SeqConvocacao, p => p.SeqConvocacao == this.SeqConvocacao);
            AddExpression(this.TipoChamada, p=> p.TipoChamada == this.TipoChamada);

            return GetExpression();

        }
    }
}