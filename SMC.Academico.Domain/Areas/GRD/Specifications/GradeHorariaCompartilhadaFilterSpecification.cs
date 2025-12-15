using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.GRD.Specifications
{
    public class GradeHorariaCompartilhadaFilterSpecification : SMCSpecification<GradeHorariaCompartilhada>
    {
        public long? Seq { get; set; }
        public long? SeqTurma { get; set; }
        public List<long> SeqsEntidadesResponsaveis { get; set; }
        public long? SeqCicloLetivo { get; set; }
        public long? SeqDivisaoTurma { get; set; }
        public string Descricao { get; set; }

        public override Expression<Func<GradeHorariaCompartilhada, bool>> SatisfiedBy()
        {
            AddExpression(Seq, a => a.Seq == Seq);
            AddExpression(SeqTurma, a => a.Itens.Any(aa => aa.DivisaoTurma.SeqTurma == SeqTurma));
            AddExpression(SeqsEntidadesResponsaveis, a => SeqsEntidadesResponsaveis.Contains(a.SeqEntidadeResponsavel));
            AddExpression(SeqCicloLetivo, a => a.SeqCicloLetivo == SeqCicloLetivo);
            AddExpression(SeqDivisaoTurma, a => a.Itens.Any(ai => ai.SeqDivisaoTurma == SeqDivisaoTurma));
            AddExpression(Descricao, a => a.Descricao.Contains(Descricao));

            return GetExpression();
        }
    }
}