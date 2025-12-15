using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class MatrizCurricularDivisaoComponenteFilterSpecification : SMCSpecification<MatrizCurricularDivisaoComponente>, ISMCMappable
    {
        public long? SeqDivisaoComponente { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public override Expression<Func<MatrizCurricularDivisaoComponente, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqDivisaoComponente, p => p.SeqDivisaoComponente == this.SeqDivisaoComponente); 
            AddExpression(this.SeqMatrizCurricularOferta, p => p.DivisaoMatrizCurricularComponente.MatrizCurricular.Ofertas.Any(w => w.Seq == this.SeqMatrizCurricularOferta));
           
            return GetExpression();
        }
    }
}