using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Mapper;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class AlunoFilterSpecification : SMCSpecification<Aluno>, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public long? SeqSituacaoMatricula { get; set; }

        public List<long> SeqEntidadesResponsaveis { get; set; }

        /// <summary>
        /// Sequencial do item de hierarquia que representa a localidade
        /// </summary>
        public long? SeqLocalidade { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqCicloLetivoHistoricoAtual { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqCurso { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public bool? VinculoAlunoAtivo { get; set; }

        public long? SeqFormaIngresso { get; set; }

        public long? CodigoAlunoMigracao { get; set; }

        public long? SeqCicloLetivoIngresso { get; set; }

        public List<long> SeqsTipoVinculoAluno { get; set; }

        public List<long> SeqsTipoSituacaoMatricula { get; set; }

        public List<long> SeqsSituacaoMatricula { get; set; }

        public List<TipoAtuacao> TiposAtuacao { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqCicloLetivoSituacaoMatricula { get; set; }

        public string[] TokensTipoSituacaoMatricula { get; set; }

        /// <summary>
        /// Lista de RAs dos alunos
        /// </summary>
        public List<long> NumerosRegistrosAcademicos { get; set; }

        public bool? ConcessaoAteFinalCurso { get; set; }

        public bool? CodigoAlunoMigracaoPreenchido { get; set; }

        public long[] CodigosAlunoMigracao { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqUsuarioSAS { get; set; }

        public List<long> SeqsSituacaoMatriculaCicloLetivo { get; set; }

        public string TokenTipoOrientacao { get; set; }

        public List<long?> SeqsEntidadesResponsaveisHierarquiaItem { get; set; }

        public DateTime? PrazoEncerrado { get; set; }
        public List<long> SeqsDivisoesTurma { get; set; }

        public override Expression<Func<Aluno, bool>> SatisfiedBy()
        {
            //if (SeqCicloLetivoSituacaoMatricula.HasValue ^ (TokensTipoSituacaoMatricula.SMCAny() || SeqsSituacaoMatriculaCicloLetivo.SMCAny()))
            //{
            //    throw new SMCApplicationException("SeqCicloLetivoSituacaoMatricula e TokensTipoSituacaoMatricula ou SeqsSitucaoMatricula devem ser informados juntos");
            //}

            Cpf = Cpf?.SMCRemoveNonDigits();

            AddExpression(Seq, p => p.Seq == Seq);
            AddExpression(NumeroRegistroAcademico, p => p.NumeroRegistroAcademico == NumeroRegistroAcademico);
            AddExpression(Nome, p => p.DadosPessoais.Nome.Contains(Nome) || p.DadosPessoais.NomeSocial.Contains(Nome));
            AddExpression(SeqSituacaoMatricula, p => p.Historicos.FirstOrDefault(h => h.Atual)
                          .HistoricosCicloLetivo.OrderByDescending(h => h.CicloLetivo.Ano).ThenByDescending(h => h.CicloLetivo.Numero).FirstOrDefault(h => !h.DataExclusao.HasValue)
                          .AlunoHistoricoSituacao.OrderByDescending(s => s.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Today && !s.DataExclusao.HasValue).SeqSituacaoMatricula == SeqSituacaoMatricula);
            AddExpression(SeqEntidadesResponsaveis, p => p.Historicos.Any(a => SeqEntidadesResponsaveis.Contains(a.SeqEntidadeVinculo)));
            AddExpression(SeqLocalidade, p => p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade == SeqLocalidade);
            AddExpression(SeqNivelEnsino, p => p.Historicos.Any(a => a.SeqNivelEnsino == SeqNivelEnsino));
            AddExpression(SeqCursoOferta, p => p.Historicos.Any(a => a.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.SeqCursoOferta == SeqCursoOferta));
            AddExpression(SeqFormacaoEspecifica, p => p.Historicos.Any(h =>
                          h.Formacoes.Any(af =>
                          af.SeqFormacaoEspecifica == SeqFormacaoEspecifica)));
            AddExpression(SeqTurno, p => p.Historicos.Any(a => a.CursoOfertaLocalidadeTurno.SeqTurno == SeqTurno));
            AddExpression(SeqTipoVinculoAluno, p => p.SeqTipoVinculoAluno == SeqTipoVinculoAluno);
            AddExpression(Seqs, p => Seqs.Contains(p.Seq));
            AddExpression(SeqPessoa, p => p.SeqPessoa == SeqPessoa);
            AddExpression(SeqCurso, p => p.Historicos.Any(h => h.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso == SeqCurso));
            AddExpression(SeqCicloLetivoHistoricoAtual, p => p.Historicos.FirstOrDefault(h => h.Atual).SeqCicloLetivo == SeqCicloLetivoHistoricoAtual);
            AddExpression(SeqCicloLetivo, p => p.Historicos.Any(h => h.SeqCicloLetivo == SeqCicloLetivo));
            AddExpression(Cpf, p => p.Pessoa.Cpf == Cpf);
            AddExpression(NumeroPassaporte, p => p.DadosPessoais.Pessoa.NumeroPassaporte == NumeroPassaporte);
            AddExpression(VinculoAlunoAtivo, p => p
                          .Historicos.FirstOrDefault(f => f.Atual)
                          .HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(f => !f.DataExclusao.HasValue)
                          .AlunoHistoricoSituacao.OrderByDescending(s => s.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Today && !s.DataExclusao.HasValue)
                          .SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo == VinculoAlunoAtivo);
            AddExpression(SeqFormaIngresso, p => p.Historicos.FirstOrDefault(f => f.Atual).SeqFormaIngresso == SeqFormaIngresso);
            AddExpression(CodigoAlunoMigracao, p => p.CodigoAlunoMigracao == CodigoAlunoMigracao);
            AddExpression(SeqCicloLetivoIngresso, p => p.Historicos.OrderBy(o => o.DataInclusao).FirstOrDefault(f => !f.DataExclusao.HasValue)
                          .HistoricosCicloLetivo.OrderBy(o => o.CicloLetivo.Ano).ThenBy(o => o.CicloLetivo.Numero).FirstOrDefault(f => !f.DataExclusao.HasValue)
                          .SeqCicloLetivo == SeqCicloLetivoIngresso);
            AddExpression(SeqsTipoVinculoAluno, p => SeqsTipoVinculoAluno.Contains(p.SeqTipoVinculoAluno));
            AddExpression(SeqsTipoSituacaoMatricula, p => SeqsTipoSituacaoMatricula.Contains(p.Historicos.FirstOrDefault(h => h.Atual)
                          .HistoricosCicloLetivo.OrderByDescending(h => h.CicloLetivo.Ano).ThenByDescending(h => h.CicloLetivo.Numero).FirstOrDefault(h => !h.DataExclusao.HasValue)
                          .AlunoHistoricoSituacao.OrderByDescending(s => s.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Today && !s.DataExclusao.HasValue).SituacaoMatricula.SeqTipoSituacaoMatricula));
            AddExpression(SeqsSituacaoMatricula, p => SeqsSituacaoMatricula.Contains(p.Historicos.FirstOrDefault(h => h.Atual)
                          .HistoricosCicloLetivo.OrderByDescending(h => h.CicloLetivo.Ano).ThenByDescending(h => h.CicloLetivo.Numero).FirstOrDefault(h => !h.DataExclusao.HasValue)
                          .AlunoHistoricoSituacao.OrderByDescending(s => s.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Today && !s.DataExclusao.HasValue).SeqSituacaoMatricula));
            AddExpression(TiposAtuacao, p => TiposAtuacao.Contains(p.TipoAtuacao));
            AddExpression(SeqCursoOfertaLocalidade, p => p.Historicos.Any(a => a.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade == SeqCursoOfertaLocalidade));
            AddExpression(SeqEntidadeResponsavel, p => p.Historicos.Any(a => a.SeqEntidadeVinculo == SeqEntidadeResponsavel));
            AddExpression(NumerosRegistrosAcademicos, p => NumerosRegistrosAcademicos.Contains(p.NumeroRegistroAcademico));

            if (SeqCicloLetivoSituacaoMatricula.HasValue)
            {
                AddExpression(TokensTipoSituacaoMatricula, p => TokensTipoSituacaoMatricula.Contains(
                              p.Historicos.FirstOrDefault(f => f.Atual)
                              .HistoricosCicloLetivo.FirstOrDefault(f => f.SeqCicloLetivo == SeqCicloLetivoSituacaoMatricula && !f.DataExclusao.HasValue)
                              .AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault(f => !f.DataExclusao.HasValue && f.DataInicioSituacao <= DateTime.Now)
                              .SituacaoMatricula.TipoSituacaoMatricula.Token));
                AddExpression(SeqsSituacaoMatriculaCicloLetivo, p => SeqsSituacaoMatriculaCicloLetivo.Contains(
                              p.Historicos.FirstOrDefault(f => f.Atual)
                              .HistoricosCicloLetivo.FirstOrDefault(f => f.SeqCicloLetivo == SeqCicloLetivoSituacaoMatricula && !f.DataExclusao.HasValue)
                              .AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault(f => !f.DataExclusao.HasValue && f.DataInicioSituacao <= DateTime.Now)
                              .SeqSituacaoMatricula));
            }

            AddExpression(ConcessaoAteFinalCurso, p => p.Beneficios.Any(a => a.Beneficio.ConcessaoAteFinalCurso == ConcessaoAteFinalCurso));
            AddExpression(CodigoAlunoMigracaoPreenchido, p => p.CodigoAlunoMigracao.HasValue == CodigoAlunoMigracaoPreenchido);
            AddExpression(CodigosAlunoMigracao, p => CodigosAlunoMigracao.Contains((long)p.CodigoAlunoMigracao));
            AddExpression(SeqInstituicaoEnsino, p => p.Pessoa.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(SeqUsuarioSAS, p => p.Pessoa.SeqUsuarioSAS == SeqUsuarioSAS);

            if (PrazoEncerrado.HasValue)
                AddExpression(x => x.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderBy(o => o.DataInclusao).FirstOrDefault().DataPrevisaoConclusao == PrazoEncerrado.Value);

            //AddExpression(this.SeqsEntidadesResponsaveisHierarquiaItem, a => this.SeqsEntidadesResponsaveisHierarquiaItem.Any(se => a.OrientacoesPessoaAtuacao
            //              .Select(s => (s.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(f => f.Atual)
            //              .CursoOfertaLocalidadeTurno
            //              .CursoOfertaLocalidade
            //              .CursoOferta
            //              .Curso
            //              .HierarquiasEntidades.FirstOrDefault()
            //              .SeqItemSuperior).Any(al => al == se)));

            AddExpression(this.SeqsEntidadesResponsaveisHierarquiaItem, a => this.SeqsEntidadesResponsaveisHierarquiaItem.Contains(a.Historicos.FirstOrDefault(f => f.Atual)
                          .CursoOfertaLocalidadeTurno
                          .CursoOfertaLocalidade
                          .CursoOferta
                          .Curso.HierarquiasEntidades.FirstOrDefault().SeqItemSuperior));
            AddExpression(SeqsDivisoesTurma, p => p.Historicos.Any(h => h.Atual && h.HistoricosCicloLetivo.Any(hc => hc.PlanosEstudo.Any(pe => pe.Atual && pe.Itens.Any(pei => SeqsDivisoesTurma.Contains(pei.SeqDivisaoTurma.Value))))));

            return GetExpression();
        }
    }
}