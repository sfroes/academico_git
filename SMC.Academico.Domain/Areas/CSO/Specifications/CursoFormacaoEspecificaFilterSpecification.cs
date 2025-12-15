using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoFormacaoEspecificaFilterSpecification : SMCSpecification<CursoFormacaoEspecifica>
    {
        public long? SeqCurso { get; set; }

        public List<long> SeqFormacaoEspecificaSuperior { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public bool? Ativo { get; set; }

        public List<long> SeqsFormacaoEspecifica { get; set; }

        public override Expression<Func<CursoFormacaoEspecifica, bool>> SatisfiedBy()
        {
            var agora = DateTime.Today;

            AddExpression(SeqCurso, w => w.SeqCurso == this.SeqCurso);
            AddExpression(SeqFormacaoEspecificaSuperior, w => this.SeqFormacaoEspecificaSuperior.Contains(w.FormacaoEspecifica.SeqFormacaoEspecificaSuperior.Value));
            AddExpression(SeqFormacaoEspecifica, w => w.SeqFormacaoEspecifica == this.SeqFormacaoEspecifica);
            AddExpression(this.SeqsFormacaoEspecifica, p => SeqsFormacaoEspecifica.Contains(p.SeqFormacaoEspecifica));
            AddExpression(Ativo, w => this.Ativo == (agora >= w.DataInicioVigencia && (!w.DataFimVigencia.HasValue || agora <= w.DataFimVigencia)));

            return GetExpression();
        }
    }
}