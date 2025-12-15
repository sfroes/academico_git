using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class MatrizCurricularOfertaExcecaoLocalidadeFilterSpecification : SMCSpecification<MatrizCurricularOfertaExcecaoLocalidade>
    {
        public List<long> SeqsMatrizCurricularOferta { get; set; }

        public override Expression<Func<MatrizCurricularOfertaExcecaoLocalidade, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqsMatrizCurricularOferta, p => SeqsMatrizCurricularOferta.Contains(p.SeqMatrizCurricularOferta));

            return GetExpression();
        }
    }
}