using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class ContratoCursosVigentesSpecification : SMCSpecification<ContratoCurso>
    {
        public long SeqCurso { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        //public DateTime DataInicioValidade { get; set; }

        public override Expression<Func<ContratoCurso, bool>> SatisfiedBy()
        {
            AddExpression(p => p.SeqCurso == SeqCurso);
            AddExpression(p => p.Contrato.SeqInstituicaoEnsino == SeqInstituicaoEnsino);

            return GetExpression();
        }
    }
}

 
