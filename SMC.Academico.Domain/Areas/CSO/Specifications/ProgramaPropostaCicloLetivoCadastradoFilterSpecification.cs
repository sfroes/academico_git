using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    /// <summary>
    /// Specification que valida se já existe um ciclo letivo cadastrado para uma proposta de um programa
    /// </summary>
    public class ProgramaPropostaCicloLetivoCadastradoFilterSpecification : SMCSpecification<ProgramaProposta>
    {
        public long? SeqCicloLetivo { get; set; }

        public long? SeqPrograma { get; set; }

        public long? Seq { get; set; }

        public override Expression<Func<ProgramaProposta, bool>> SatisfiedBy()
        { 
            AddExpression(Seq, w => w.Seq != Seq.Value);
            AddExpression(SeqCicloLetivo, w => w.SeqCicloLetivo == SeqCicloLetivo.Value);
            AddExpression(SeqPrograma, w => w.SeqPrograma == SeqPrograma.Value);

            return GetExpression();
        }
    }
}