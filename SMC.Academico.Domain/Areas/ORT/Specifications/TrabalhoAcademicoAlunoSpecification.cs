using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class TrabalhoAcademicoAlunoSpecification : SMCSpecification<TrabalhoAcademico>
    {
        public long? SeqAluno { get; set; }
        public List<long> SeqsTrabalhoAcademico { get; set; }

        public long? SeqTipoTrabalho { get; set; }

        public bool? BancaCancelada { get; set; }

        public override Expression<Func<TrabalhoAcademico, bool>> SatisfiedBy()
        {
            AddExpression(SeqAluno, x => x.Autores.Any(a => a.SeqAluno == SeqAluno));
            AddExpression(SeqsTrabalhoAcademico, x => SeqsTrabalhoAcademico.Contains(x.Seq));
            AddExpression(SeqTipoTrabalho, x => x.SeqTipoTrabalho == SeqTipoTrabalho);

            if (BancaCancelada.HasValue)
            {
                if (!BancaCancelada.Value)
                    AddExpression(x => x.DivisoesComponente.Any(s => s.OrigemAvaliacao.AplicacoesAvaliacao.Any(a => a.DataCancelamento == null)));
                else
                    AddExpression(x => x.DivisoesComponente.Any(s => s.OrigemAvaliacao.AplicacoesAvaliacao.Any(a => a.DataCancelamento != null)));
            }
            return GetExpression();
        }
    }
}