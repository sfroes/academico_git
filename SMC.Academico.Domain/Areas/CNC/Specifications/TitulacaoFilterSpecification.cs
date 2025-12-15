using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class TitulacaoFilterSpecification : SMCSpecification<Titulacao>
    {
        public string Descricao { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public bool? SemCurso { get; set; }

        public long? Seq { get; set; }

        public bool? GrauAcademicoCurso { get; set; }

        public List<long> SeqsGrauAcademico { get; set; }

        // NV01 UC_CSO_001_01_06 - Manter Titulação
        // Opção 2 - Seleção de Formação Específica
        // Deverá ser considerado os graus associados ao nível de ensino do respectivo curso.
        public override Expression<Func<Titulacao, bool>> SatisfiedBy()
        {
            if (SeqsGrauAcademico == null)
            {
                SeqsGrauAcademico = new List<long>();
            }

            AddExpression(Descricao, x => x.DescricaoFeminino.Contains(Descricao) || x.DescricaoMasculino.Contains(Descricao) || x.DescricaoAbreviada.Contains(Descricao));
            AddExpression(Ativo, x => x.Ativo == Ativo);
            AddExpression(SemCurso, x => x.SeqCurso.HasValue != SemCurso);
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqsGrauAcademico, w => this.SeqsGrauAcademico.Contains((long)w.SeqGrauAcademico));

            if (GrauAcademicoCurso.GetValueOrDefault())
            {
                AddExpression(x => x.GrauAcademico.NiveisEnsino.Any(n => n.Cursos.Any(c => c.Seq == SeqCurso)));
            }
            else
            {
                AddExpression(SeqCurso, x => x.SeqCurso == SeqCurso);
                AddExpression(SeqGrauAcademico, x => x.SeqGrauAcademico == SeqGrauAcademico);
            }

            return GetExpression();
        }
    }
}