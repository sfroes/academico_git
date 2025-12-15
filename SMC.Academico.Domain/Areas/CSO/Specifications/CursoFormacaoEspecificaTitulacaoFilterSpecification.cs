using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoFormacaoEspecificaTitulacaoFilterSpecification : SMCSpecification<CursoFormacaoEspecificaTitulacao>
    {
        public long? SeqCurso { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public List<long> SeqsFormacoesEspecificas { get; set; }

        public long? SeqCursoFormacaoEspecifica { get; set; }

        public long? SeqTitulacao { get; set; }

        public override Expression<Func<CursoFormacaoEspecificaTitulacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqCursoFormacaoEspecifica, w => w.SeqCursoFormacaoEspecifica == this.SeqCursoFormacaoEspecifica);
            AddExpression(SeqCurso, w => w.CursoFormacaoEspecifica.SeqCurso == this.SeqCurso);
            AddExpression(SeqFormacaoEspecifica, w => w.CursoFormacaoEspecifica.SeqFormacaoEspecifica == this.SeqFormacaoEspecifica);
            AddExpression(SeqsFormacoesEspecificas, w => SeqsFormacoesEspecificas.Contains(w.CursoFormacaoEspecifica.SeqFormacaoEspecifica));
            AddExpression(SeqTitulacao, w => w.SeqTitulacao == this.SeqTitulacao); 

            return GetExpression();
        }
    }
}