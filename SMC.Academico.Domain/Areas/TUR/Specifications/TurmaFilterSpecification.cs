using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.Specifications
{
    public class TurmaFilterSpecification : SMCSpecification<Turma>
    {
        public long? Seq { get; set; }
        public long? SeqCicloLetivoInicio { get; set; }
        public long? SeqCicloLetivoFim { get; set; }
        public string CodigoFormatado { get; set; }
        public int? Codigo { get; set; }
        public short? Numero { get; set;}
        public string DescricaoConfiguracao { get; set; }
        public long? SeqMatrizCurricularOferta { get; set; }
        public List<long> SeqsConfiguracoesComponentes { get; set; }
        public SituacaoTurma? SituacaoTurmaAtual { get; set; }
        public SituacaoTurmaDiario? SituacaoTurmaDiario { get; set; }
        public bool? SituacaoTurmaDiarioFlag { get; set; }
        public List<long> SeqsTurma { get; set; }
        public List<long> SeqsDivisoesTurma { get; set; }
        public long? SeqDivisaoTurma { get; set; }
        public long? SeqColaborador { get; set; }
        public bool? TurmaComOrientacao { get; set; }
        public long? SeqCicloLetivo { get; set; }
        public long? SeqNivelEnsino { get; set; }
        public List<long> SeqsEntidadeResponsavel { get; set; }
        public List<long> SeqsEntidadeResponsavelPrograma { get; set; }
        public long? SeqLocalidade { get; set; }
        public long? SeqCursoOferta { get; set; }
        public long? SeqTurno { get; set; }
        public long? SeqTipoTurma { get; set; }
        public long? SeqEntidadeGrupoPrograma { get; set; }
        public short? NumeroCicloLetivo { get; set; }
        public short? AnoCicloLetivo { get; set; }
        public short? NumeroFimCiclo { get; set; }
        public short? AnoInicioCiclo { get; set; }
        public List<long> SeqsMatrizCurricularOferta { get; set; }
        public long? SeqComponenteCurricularAssunto { get; set; }
        public long? SeqComponenteCurricular { get; set; }
        /// <summary>
        /// Todas as Turmas que não estão canceladas
        /// </summary>
        public bool? TurmaSituacaoNaoCancelada { get; set; }
        public long? SeqOrigemAvaliacao { get; set; }
        public bool? TurmasPeriodoEncerrado { get; set; }

        public override Expression<Func<Turma, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqCicloLetivoInicio, p => p.SeqCicloLetivoInicio == this.SeqCicloLetivoInicio);
            AddExpression(this.SeqCicloLetivoFim, p => p.SeqCicloLetivoFim == this.SeqCicloLetivoFim);
            AddExpression(this.Codigo, p => p.Codigo == this.Codigo);
            AddExpression(this.Numero, p => p.Numero == this.Numero);
            AddExpression(this.DescricaoConfiguracao, p => p.ConfiguracoesComponente.Where(w => w.Descricao.Contains(DescricaoConfiguracao)).Count() > 0);
            AddExpression(this.SeqMatrizCurricularOferta, p => p.ConfiguracoesComponente.Any(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == this.SeqMatrizCurricularOferta)));
            AddExpression(this.SeqsConfiguracoesComponentes, p => p.ConfiguracoesComponente.Any(w => this.SeqsConfiguracoesComponentes.Contains(w.SeqConfiguracaoComponente)));
            AddExpression(this.SituacaoTurmaAtual, p => p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma == this.SituacaoTurmaAtual);
            AddExpression(this.SeqsTurma, p => this.SeqsTurma.Contains(p.Seq));
            AddExpression(this.SeqColaborador, p => p.Colaboradores.Any(a => a.SeqColaborador == this.SeqColaborador) || p.DivisoesTurma.Any(a => a.Colaboradores.Any(c => c.SeqColaborador == SeqColaborador)));
            AddExpression(this.SeqDivisaoTurma, p => p.DivisoesTurma.Any(d => d.Seq == SeqDivisaoTurma));
            AddExpression(this.SeqsDivisoesTurma, p => p.DivisoesTurma.Any(d => SeqsDivisoesTurma.Contains(d.Seq)));
            AddExpression(this.TurmaComOrientacao, p => p.ConfiguracoesComponente.Any(a => a.ConfiguracaoComponente.DivisoesComponente.Any(ad => ad.TipoDivisaoComponente.GeraOrientacao == this.TurmaComOrientacao)));
            AddExpression(this.SituacaoTurmaDiarioFlag, p => p.HistoricosFechamentoDiario.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DiarioFechado == this.SituacaoTurmaDiarioFlag);

            AddExpression(this.SeqCicloLetivo, p => p.SeqCicloLetivoInicio == this.SeqCicloLetivo || p.SeqCicloLetivoFim == this.SeqCicloLetivo);
            AddExpression(this.SeqCursoOferta, p => p.ConfiguracoesComponente.Any(a => a.RestricoesTurmaMatriz.Any(r => r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.SeqCursoOferta == this.SeqCursoOferta)));
            AddExpression(this.SeqNivelEnsino, p => p.ConfiguracoesComponente.FirstOrDefault(a => a.Principal).RestricoesTurmaMatriz.Any(r => r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino == this.SeqNivelEnsino));
            AddExpression(this.SeqLocalidade, p => p.ConfiguracoesComponente.FirstOrDefault(c => c.Principal).RestricoesTurmaMatriz.Select(r => r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades).FirstOrDefault().Any(g => g.ItemSuperior.Entidade.Seq == this.SeqLocalidade));

            AddExpression(this.SeqsEntidadeResponsavel, p => p.ConfiguracoesComponente.Any(a => a.RestricoesTurmaMatriz.Any(r => SeqsEntidadeResponsavel.Contains(r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.InstituicaoEnsino.Seq))));

            AddExpression(this.SeqsEntidadeResponsavelPrograma, p => p.ConfiguracoesComponente.Any(a => a.RestricoesTurmaMatriz.Any(r => SeqsEntidadeResponsavelPrograma.Contains(r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.SeqEntidade))));

            AddExpression(this.SeqTurno, p => p.ConfiguracoesComponente.Any(w => w.RestricoesTurmaMatriz.Any(a => a.CursoOfertaLocalidadeTurno.SeqTurno == this.SeqTurno)));
            AddExpression(this.SeqTipoTurma, p => p.ConfiguracoesComponente.Any(w => w.Turma.SeqTipoTurma == this.SeqTipoTurma));
            AddExpression(this.SeqEntidadeGrupoPrograma, p => p.ConfiguracoesComponente.FirstOrDefault(c => c.Principal).RestricoesTurmaMatriz.Select(r => r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades).FirstOrDefault().Any(g => g.ItemSuperior.ItemSuperior.SeqEntidade == this.SeqEntidadeGrupoPrograma));

            //Validação Período Ciclo Letivo Turma           
            AddExpression(this.NumeroCicloLetivo, p => p.CicloLetivoInicio.Numero >= NumeroCicloLetivo && p.CicloLetivoFim.Numero <= NumeroCicloLetivo);
            AddExpression(this.AnoCicloLetivo, p => p.CicloLetivoInicio.Ano >= AnoCicloLetivo && p.CicloLetivoFim.Ano <= AnoCicloLetivo);

            AddExpression(this.TurmaSituacaoNaoCancelada, p => p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma != SituacaoTurma.Cancelada);
            AddExpression(this.SeqsMatrizCurricularOferta, p => p.ConfiguracoesComponente.Any(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta.HasValue && SeqsMatrizCurricularOferta.Contains(r.SeqMatrizCurricularOferta.Value))));
            AddExpression(this.SeqComponenteCurricularAssunto, p => p.ConfiguracoesComponente.Any(w => w.RestricoesTurmaMatriz.Any(r => r.SeqComponenteCurricularAssunto == SeqComponenteCurricularAssunto)));
            AddExpression(this.SeqComponenteCurricular, p => p.ConfiguracoesComponente.Any(w => w.ConfiguracaoComponente.SeqComponenteCurricular == SeqComponenteCurricular));
            AddExpression(this.SeqOrigemAvaliacao, p => p.SeqOrigemAvaliacao == this.SeqOrigemAvaliacao);
            AddExpression(TurmasPeriodoEncerrado, p => (p.DataFimPeriodoLetivo < DateTime.Now) == TurmasPeriodoEncerrado);

            return GetExpression();
        }
    }
}
