using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ConfiguracaoProcessoFilterSpecification : SMCSpecification<ConfiguracaoProcesso>
    {
        public ConfiguracaoProcessoFilterSpecification(long seqVinculo, long seqEntidadeResponsavel)
        {
            SeqTipoVinculoAluno = seqVinculo;
            SeqEntidadeResponsavel = seqEntidadeResponsavel;
        }

        public ConfiguracaoProcessoFilterSpecification()
        {

        }

        public long? Seq { get; set; }
        public long? SeqTipoVinculoAluno { get; set; }

        public long[] SeqsVinculo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long[] SeqsNivelEnsino { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public long[] SeqsCursoOfertaLocalidadeTurno { get; set; }

        public long? SeqProcesso { get; set; }

        public override Expression<Func<ConfiguracaoProcesso, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq.Value);
            AddExpression(SeqTipoVinculoAluno, x => x.TiposVinculoAluno.Any(f => f.SeqTipoVinculoAluno == SeqTipoVinculoAluno));
            AddExpression(SeqsVinculo, w => w.TiposVinculoAluno.Any(x => SeqsVinculo.Contains(x.SeqTipoVinculoAluno)));            
            AddExpression(SeqEntidadeResponsavel, x => x.Processo.UnidadesResponsaveis.Any(f => f.SeqEntidadeResponsavel == SeqEntidadeResponsavel));
            AddExpression(SeqProcesso, x => x.SeqProcesso == SeqProcesso.Value);
            AddExpression(SeqNivelEnsino, x => x.NiveisEnsino.Any(f => f.SeqNivelEnsino == SeqNivelEnsino));
            AddExpression(SeqsNivelEnsino, w => w.NiveisEnsino.Any(x => SeqsNivelEnsino.Contains(x.SeqNivelEnsino)));
            AddExpression(SeqCursoOfertaLocalidadeTurno, x => x.Cursos.Any(g => g.SeqCursoOfertaLocalidadeTurno == SeqCursoOfertaLocalidadeTurno.Value));
            AddExpression(SeqsCursoOfertaLocalidadeTurno, w => w.Cursos.Any(x => SeqsCursoOfertaLocalidadeTurno.Contains(x.SeqCursoOfertaLocalidadeTurno)));

            return GetExpression();
        }
    }
}