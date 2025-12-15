using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class ContratoFilterSpecification : SMCSpecification<Contrato>
    {
        public string NumeroRegistro { get; set; }

        public string Descricao { get; set; }

        public long? SeqNivelEnsino { get; set; } 

        public long? SeqCurso { get; set; }

        public override Expression<Func<Contrato, bool>> SatisfiedBy()
        {
            AddExpression(NumeroRegistro, p => p.NumeroRegistro.Contains(NumeroRegistro));
            AddExpression(Descricao, p => p.Descricao.Contains(Descricao));
            AddExpression(SeqNivelEnsino, p => p.NiveisEnsino.Any(w => w.SeqNivelEnsino == SeqNivelEnsino.Value));
            AddExpression(SeqCurso, p=> p.Cursos.Any(w => w.SeqCurso == SeqCurso.Value));

            return GetExpression();
        }
    }
}

 
