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
    public class ContratoVigenteSpecification : SMCSpecification<Contrato>
    {
        public long? SeqCurso { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public override Expression<Func<Contrato, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqCurso, p => p.Cursos.Any(f => f.SeqCurso == SeqCurso.Value));
            AddExpression(SeqInstituicaoEnsino, p => p.SeqInstituicaoEnsino == SeqInstituicaoEnsino.Value);
            AddExpression(SeqNivelEnsino, p => p.NiveisEnsino.Any(f => f.SeqNivelEnsino == SeqNivelEnsino.Value));
            AddExpression(p => p.DataInicioValidade <= DateTime.Today && (!p.DataFimValidade.HasValue || p.DataFimValidade.Value >= DateTime.Today));

            return GetExpression();
        }
    }
}