using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class HistoricoEscolarFilterSpecification : SMCSpecification<HistoricoEscolar>
    {
        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public long[] SeqsExcluidos { get; set; }

        public List<long> SeqsOrigemAvaliacoes { get; set; }

        public long? SeqAluno { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public List<long> SeqsAlunoHistorico { get; set; }

        public long? SeqAlunoHistorico { get; set; }

        public SituacaoHistoricoEscolar[] SituacoesHistoricoEscolar { get; set; }

        public List<long> SeqsCurriculo { get; set; }

        public TipoGestaoDivisaoComponente? TipoGestaoDivisaoComponente { get; set; }

        public List<long> SeqsComponenteCurricular { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public bool? DisciplinaIsolada { get; set; }

        public long? SeqSoliciacaoServico { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqCicloLetivoDiferente { get; set; }

        public bool? AlunoHistoricoAtual { get; set; }

        public override Expression<Func<HistoricoEscolar, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq);
            AddExpression(Seqs, w => Seqs.Contains(w.Seq));
            AddExpression(SeqsExcluidos, w => !SeqsExcluidos.Contains(w.Seq));
            AddExpression(SeqAluno, w => w.AlunoHistorico.SeqAluno == SeqAluno);
            AddExpression(SeqPessoa, w => w.AlunoHistorico.Aluno.SeqPessoa == SeqPessoa);
            AddExpression(SeqOrigemAvaliacao, w => w.SeqOrigemAvaliacao == SeqOrigemAvaliacao);
            AddExpression(SeqsOrigemAvaliacoes, w => SeqsOrigemAvaliacoes.Contains(w.SeqOrigemAvaliacao.Value));
            AddExpression(SeqComponenteCurricular, w => w.SeqComponenteCurricular == SeqComponenteCurricular);
            AddExpression(SeqComponenteCurricularAssunto, w => w.SeqComponenteCurricularAssunto == SeqComponenteCurricularAssunto);
            AddExpression(SeqOfertaCurso, w => w.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.SeqCursoOferta == SeqOfertaCurso);
            AddExpression(SeqCursoOfertaLocalidadeTurno, w => w.AlunoHistorico.SeqCursoOfertaLocalidadeTurno == SeqCursoOfertaLocalidadeTurno);
            AddExpression(SituacaoHistoricoEscolar, w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar);
            AddExpression(() => SeqsAlunoHistorico != null, w => SeqsAlunoHistorico.Contains(w.SeqAlunoHistorico));
            AddExpression(SeqAlunoHistorico, w => w.SeqAlunoHistorico == SeqAlunoHistorico);
            AddExpression(SeqsComponenteCurricular, w => SeqsComponenteCurricular.Contains(w.SeqComponenteCurricular.Value));
            AddExpression(SituacoesHistoricoEscolar, w => SituacoesHistoricoEscolar.Contains(w.SituacaoHistoricoEscolar));
            AddExpression(SeqsCurriculo, w => w.ComponenteCurricular.Configuracoes.Any(a => a.DivisoesMatrizCurricularComponente.Any(d => SeqsCurriculo.Contains(d.DivisaoMatrizCurricular.MatrizCurricular.CurriculoCursoOferta.SeqCurriculo))));
            AddExpression(TipoGestaoDivisaoComponente, w => w.ComponenteCurricular.TipoComponente.TiposDivisao.All(t => t.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente));
            AddExpression(SeqSoliciacaoServico, w => w.SeqSolicitacaoServico == SeqSoliciacaoServico);
            AddExpression(SeqCicloLetivo, w => w.SeqCicloLetivo == SeqCicloLetivo);
            AddExpression(SeqCicloLetivoDiferente, w => w.SeqCicloLetivo != SeqCicloLetivoDiferente);
            AddExpression(AlunoHistoricoAtual, w => w.AlunoHistorico.Atual == AlunoHistoricoAtual);

            //Quando é disciplina isolada o histórico do aluno não tem oferta de curso
            AddExpression(DisciplinaIsolada, w => w.AlunoHistorico.SeqCursoOfertaLocalidadeTurno.HasValue != DisciplinaIsolada);

            return GetExpression();
        }
    }
}