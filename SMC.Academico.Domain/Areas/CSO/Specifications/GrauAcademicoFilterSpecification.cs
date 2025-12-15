using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class GrauAcademicoFilterSpecification : SMCSpecification<GrauAcademico>
    {
        public long? Seq { get; set; }

        public List<long> SeqNivelEnsino { get; set; }

        public bool? Ativo { get; set; }

        public bool? GrauAcademicoAtivo { get; set; }

        public List<long?> SeqsGrauAcademico { get; set; }

        public string Descricao { get; set; }

        public override Expression<Func<GrauAcademico, bool>> SatisfiedBy()
        {
            SetOrderBy("Descricao");
            var ativoFinal = Ativo.GetValueOrDefault() || GrauAcademicoAtivo.GetValueOrDefault();

            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(this.SeqsGrauAcademico, p => SeqsGrauAcademico.Contains(p.Seq));
            AddExpression(w => (!Ativo.HasValue && !GrauAcademicoAtivo.HasValue) || w.Ativo == ativoFinal);
            AddExpression(SeqNivelEnsino, w => w.NiveisEnsino.Any(c => this.SeqNivelEnsino.Contains(c.Seq)));
            AddExpression(Descricao, w => w.Descricao == this.Descricao);

            return GetExpression();
        }
    }
}