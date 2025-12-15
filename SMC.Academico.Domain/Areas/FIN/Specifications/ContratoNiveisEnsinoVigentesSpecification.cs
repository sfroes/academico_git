using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class ContratoNiveisEnsinoVigentesSpecification : SMCSpecification<ContratoNivelEnsino>
    {
        public long SeqNivelEnsino { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        //public DateTime DataInicioValidade { get; set; } 



        public override Expression<Func<ContratoNivelEnsino, bool>> SatisfiedBy()
        {
            AddExpression(p => p.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(p => p.Contrato.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            return GetExpression();
        }
    }
} 
 