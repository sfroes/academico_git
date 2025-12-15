using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class AlunoFormacaoFilterSpecification : SMCSpecification<AlunoFormacao>
    {
        public long? SeqAlunoHistorico { get; set; }

        public bool? Ativo { get; set; }

        public bool? AlunoHistoricoAtual { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public long? SeqTipoFormacaoEspecifica { get; set; }

        public List<long> SeqsAlunoFormacaoDiferente { get; set; }

        public override Expression<Func<AlunoFormacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqAlunoHistorico, a => a.SeqAlunoHistorico == SeqAlunoHistorico);
            AddExpression(Ativo, a => a.DataFim == null);
            AddExpression(AlunoHistoricoAtual, a => a.AlunoHistorico.Atual == AlunoHistoricoAtual);
            AddExpression(SeqPessoa, a => a.AlunoHistorico.Aluno.SeqPessoa == SeqPessoa);
            AddExpression(SeqCurso, a => a.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso == SeqCurso);
            AddExpression(SeqGrauAcademico, a => a.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.CursosFormacaoEspecifica.FirstOrDefault(f => f.SeqFormacaoEspecifica == a.SeqFormacaoEspecifica).SeqGrauAcademico == SeqGrauAcademico);
            AddExpression(SeqTipoFormacaoEspecifica, a => a.FormacaoEspecifica.SeqTipoFormacaoEspecifica == SeqTipoFormacaoEspecifica);
            AddExpression(SeqsAlunoFormacaoDiferente, p => !SeqsAlunoFormacaoDiferente.Contains(p.Seq));

            return GetExpression();
        }
    }
}
