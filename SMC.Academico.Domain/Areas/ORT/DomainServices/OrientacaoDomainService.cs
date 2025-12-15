using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.Common.Areas.ORT.Exceptions.Orientacao;
using SMC.Academico.Common.Areas.ORT.Includes;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Resources;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORT.DomainServices
{
    public class OrientacaoDomainService : AcademicoContextDomain<Orientacao>
    {
        #region [ DomainService ]

        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();

        private ColaboradorVinculoDomainService ColaboradorVinculoDomainService => Create<ColaboradorVinculoDomainService>();

        private AlunoDomainService AlunoDomainService { get => Create<AlunoDomainService>(); }

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService { get => Create<DivisaoTurmaDomainService>(); }

        private InstituicaoExternaDomainService InstituicaoExternaDomainService { get => Create<InstituicaoExternaDomainService>(); }

        private InstituicaoNivelTipoOrientacaoDomainService InstituicaoNivelTipoOrientacaoDomainService { get => Create<InstituicaoNivelTipoOrientacaoDomainService>(); }

        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService { get => Create<InstituicaoNivelTipoTermoIntercambioDomainService>(); }

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService { get => Create<InstituicaoNivelTipoVinculoAlunoDomainService>(); }

        private MotivoBloqueioDomainService MotivoBloqueioDomainService { get => Create<MotivoBloqueioDomainService>(); }

        private OrientacaoColaboradorDomainService OrientacaoColaboradorDomainService { get => Create<OrientacaoColaboradorDomainService>(); }

        private OrientacaoPessoaAtuacaoDomainService OrientacaoPessoaAtuacaoDomainService { get => Create<OrientacaoPessoaAtuacaoDomainService>(); }

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService { get => Create<PlanoEstudoItemDomainService>(); }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService { get => Create<PessoaAtuacaoDomainService>(); }

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService { get => Create<PessoaAtuacaoBloqueioDomainService>(); }

        private PessoaAtuacaoBloqueioItemDomainService PessoaAtuacaoBloqueioItemDomainService { get => Create<PessoaAtuacaoBloqueioItemDomainService>(); }

        private PessoaAtuacaoTermoIntercambioDomainService PessoaAtuacaoTermoIntercambioDomainService { get => Create<PessoaAtuacaoTermoIntercambioDomainService>(); }

        private CicloLetivoDomainService CicloLetivoDomainService { get => Create<CicloLetivoDomainService>(); }

        private TipoOrientacaoDomainService TipoOrientacaoDomainService { get => Create<TipoOrientacaoDomainService>(); }

        private TipoTermoIntercambioDomainService TipoTermoIntercambioDomainService { get => Create<TipoTermoIntercambioDomainService>(); }

        private TurmaDomainService TurmaDomainService { get => Create<TurmaDomainService>(); }

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService { get => Create<ConfiguracaoEventoLetivoDomainService>(); }

        private DivisaoTurmaColaboradorDomainService DivisaoTurmaColaboradorDomainService { get => Create<DivisaoTurmaColaboradorDomainService>(); }

        private HistoricoEscolarDomainService HistoricoEscolarDomainService { get => Create<HistoricoEscolarDomainService>(); }

        private HistoricoEscolarColaboradorDomainService HistoricoEscolarColaboradorDomainService { get => Create<HistoricoEscolarColaboradorDomainService>(); }

        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();

        #endregion [ DomainService ]

        #region Constantes

        private const string INCLUSAO = "Inclusão";
        private const string ALTERAR = "Alteração";

        #endregion Constantes

        #region [Querys]

        private string QUERY_RELATORIO_ORIENTACAO1 =
             @"
                select
                    oc.seq_atuacao_colaborador as SeqColaborador,
	                e.seq_entidade as SeqEntidade,
	                e.nom_entidade as NomeEntidade, 
	                pdpo.nom_pessoa as NomeOrientador,
                    ne.seq_nivel_ensino as SeqNivelEnsino,
	                ne.dsc_nivel_ensino as NivelEnsino,
	                pa.seq_pessoa_atuacao as RAAluno,
	                pdp.nom_pessoa as NomeAluno, 
	                cl.dsc_ciclo_letivo as CicloLetivo, 
	                sm.dsc_situacao_matricula as SituacaoMatricula,
	                oc.dat_inicio_orientacao as InicioOrientacao,
	                oc.dat_fim_orientacao as FimOrientacao
                from	ALN.aluno a
                join	PES.pessoa_atuacao pa
		                on a.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                join	PES.pessoa_dados_pessoais pdp
		                on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
                join	ALN.aluno_historico ah
		                on a.seq_pessoa_atuacao = ah.seq_atuacao_aluno
		                and	ah.ind_atual = 1
		                and ah.seq_entidade_vinculo in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_ENTIDADE_RESPONSAVEL, ','))
                join	ORG.entidade e
		                on ah.seq_entidade_vinculo = e.seq_entidade
                join	ORT.orientacao_pessoa_atuacao opa
		                on pa.seq_pessoa_atuacao = opa.seq_pessoa_atuacao
                join	ORT.orientacao o
		                on opa.seq_orientacao = o.seq_orientacao
                join	ORT.tipo_orientacao tor
		                on o.seq_tipo_orientacao = tor.seq_tipo_orientacao
		                and tor.ind_trabalho_conclusao_curso = 1
                join	ORT.orientacao_colaborador oc
		                on o.seq_orientacao = oc.seq_orientacao
		                and (( oc.dat_inicio_orientacao <= GETDATE()
		                and isnull(oc.dat_fim_orientacao, getdate()+1) >= GETDATE() )
		                or @IND_EXIBE_FINALIZADAS = 1)
		                and isnull(@SEQ_PESSOA_ATUACAO_COLABORADOR, oc.seq_atuacao_colaborador) = oc.seq_atuacao_colaborador
                join	PES.pessoa_atuacao pao
		                on oc.seq_atuacao_colaborador = pao.seq_pessoa_atuacao
                join	PES.pessoa_dados_pessoais pdpo
		                on pao.seq_pessoa_dados_pessoais = pdpo.seq_pessoa_dados_pessoais
                join	ORG.nivel_ensino ne
		                on ah.seq_nivel_ensino = ne.seq_nivel_ensino
                join	ALN.aluno_historico_ciclo_letivo ahcl
		                on ah.seq_aluno_historico = ahcl.seq_aluno_historico
		                and ahcl.seq_ciclo_letivo = @SEQ_CICLO_LETIVO
                join	ALN.aluno_historico_situacao ahs
		                on ahcl.seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
		                and ahs.dat_exclusao is null
		                and ahs.dat_inicio_situacao = (	select	MAX(dat_inicio_situacao)
										                from	ALN.aluno_historico_situacao ahs1
										                where	ahs1.seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
										                and		ahs1.dat_exclusao is null)
                join	MAT.situacao_matricula sm
		                on ahs.seq_situacao_matricula = sm.seq_situacao_matricula
		                and sm.seq_tipo_situacao_matricula in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_TIPO_SITUACAO_MATRICULA, ','))
                join	cam.ciclo_letivo cl
		                on ahcl.seq_ciclo_letivo = cl.seq_ciclo_letivo
                order by 
	                e.nom_entidade, 
	                pdpo.nom_pessoa,
	                ne.dsc_nivel_ensino,
	                pdp.nom_pessoa";

        private string QUERY_RELATORIO_ORIENTACAO =
        @"      declare
	                @LISTA_ENTIDADE_RESPONSAVEL varchar(255) = {0},
	                @SEQ_PESSOA_ATUACAO_COLABORADOR bigint = {1},
	                @SEQ_CICLO_LETIVO bigint = {2},
	                @LISTA_TIPO_SITUACAO_MATRICULA varchar(255) = {3},
	                @IND_EXIBE_FINALIZADAS bit = {4}

                 select
                    oc.seq_atuacao_colaborador as SeqColaborador,
	                e.seq_entidade as SeqEntidade,
	                e.nom_entidade as NomeEntidade, 
	                pdpo.nom_pessoa as NomeOrientador,
                    ne.seq_nivel_ensino as SeqNivelEnsino,
	                ne.dsc_nivel_ensino as NivelEnsino,
	                pa.seq_pessoa_atuacao as RAAluno,
	                pdp.nom_pessoa as NomeAluno, 
	                cl.dsc_ciclo_letivo as CicloLetivo, 
	                sm.dsc_situacao_matricula as SituacaoMatricula,
	                oc.dat_inicio_orientacao as InicioOrientacao,
	                oc.dat_fim_orientacao as FimOrientacao
                from	ALN.aluno a
                join	PES.pessoa_atuacao pa
		                on a.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                join	PES.pessoa_dados_pessoais pdp
		                on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
                join	ALN.aluno_historico ah
		                on a.seq_pessoa_atuacao = ah.seq_atuacao_aluno
		                and	ah.ind_atual = 1
		                and ah.seq_entidade_vinculo in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_ENTIDADE_RESPONSAVEL, ','))
                join	ORG.entidade e
		                on ah.seq_entidade_vinculo = e.seq_entidade
                join	ORT.orientacao_pessoa_atuacao opa
		                on pa.seq_pessoa_atuacao = opa.seq_pessoa_atuacao
                join	ORT.orientacao o
		                on opa.seq_orientacao = o.seq_orientacao
                join	ORT.tipo_orientacao tor
		                on o.seq_tipo_orientacao = tor.seq_tipo_orientacao
		                and tor.ind_trabalho_conclusao_curso = 1
                join	ORT.orientacao_colaborador oc
		                on o.seq_orientacao = oc.seq_orientacao
		                and (( oc.dat_inicio_orientacao <= GETDATE()
		                and isnull(oc.dat_fim_orientacao, getdate()+1) >= GETDATE() )
		                or @IND_EXIBE_FINALIZADAS = 1)
		                and isnull(@SEQ_PESSOA_ATUACAO_COLABORADOR, oc.seq_atuacao_colaborador) = oc.seq_atuacao_colaborador
                        and idt_dom_tipo_participacao_orientacao  = 1
                join	PES.pessoa_atuacao pao
		                on oc.seq_atuacao_colaborador = pao.seq_pessoa_atuacao
                join	PES.pessoa_dados_pessoais pdpo
		                on pao.seq_pessoa_dados_pessoais = pdpo.seq_pessoa_dados_pessoais
                join	ORG.nivel_ensino ne
		                on ah.seq_nivel_ensino = ne.seq_nivel_ensino
                join	ALN.aluno_historico_ciclo_letivo ahcl
		                on ah.seq_aluno_historico = ahcl.seq_aluno_historico
		                and ahcl.seq_ciclo_letivo = @SEQ_CICLO_LETIVO
                join	ALN.aluno_historico_situacao ahs
		                on ahcl.seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
		                and ahs.dat_exclusao is null
		                and ahs.dat_inicio_situacao = (	select	MAX(dat_inicio_situacao)
										                from	ALN.aluno_historico_situacao ahs1
										                where	ahs1.seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
										                and		ahs1.dat_exclusao is null 
                                                        and     ahs1.dat_inicio_situacao <= GETDATE())
                join	MAT.situacao_matricula sm
		                on ahs.seq_situacao_matricula = sm.seq_situacao_matricula
		                and sm.seq_tipo_situacao_matricula in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_TIPO_SITUACAO_MATRICULA, ','))
                join	cam.ciclo_letivo cl
		                on ahcl.seq_ciclo_letivo = cl.seq_ciclo_letivo
                order by 
	                e.nom_entidade, 
	                pdpo.nom_pessoa,
	                ne.dsc_nivel_ensino,
	                pdp.nom_pessoa";

        #endregion

        public SMCPagerData<OrientacaoVO> BuscarOrientacoes(OrientacaoFilterSpecification filtro)
        {
            var total = 0;

            var includes = IncludesOrientacao.OrientacoesColaborador
                           | IncludesOrientacao.OrientacoesPessoaAtuacao
                           | IncludesOrientacao.OrientacoesPessoaAtuacao_PessoaAtuacao
                           | IncludesOrientacao.OrientacoesPessoaAtuacao_PessoaAtuacao_DadosPessoais
                           | IncludesOrientacao.NivelEnsino
                           | IncludesOrientacao.InstituicaoEnsino
                           | IncludesOrientacao.TipoOrientacao
                           | IncludesOrientacao.OrientacoesColaborador_Colaborador
                           | IncludesOrientacao.OrientacoesColaborador_Colaborador_DadosPessoais;

            filtro.TipoAtuacao = TipoAtuacao.Aluno;

            ///Ordenação de ser por tipo de orientação e por data de inicio da orientação
            filtro.SetOrderBy(o => o.TipoOrientacao.Descricao);

            var result = this.SearchBySpecification(filtro, out total, includes).ToList();

            ///Conversão em VO para que fosse possivel trazer o campo formatado
            var resultVO = result.TransformList<OrientacaoVO>();

            ///Nomes para a mensagem de exclusao
            string NomesAlunosExclucao;

            foreach (var item in resultVO)
            {
                ///Os alunos e os professores deverão ser ordenados em ordem alfabética
                item.OrientacoesPessoaAtuacao = item.OrientacoesPessoaAtuacao.OrderBy(o => o.PessoaAtuacao.DadosPessoais.Nome).ToList();
                item.OrientacoesColaborador = item.OrientacoesColaborador.OrderByDescending(o => o.DataInicioOrientacao).ToList();

                NomesAlunosExclucao = string.Empty;
                foreach (var orientacaoPessoaAtuacao in item.OrientacoesPessoaAtuacao)
                {
                    var aluno = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(orientacaoPessoaAtuacao.SeqPessoaAtuacao), s => new
                    {
                        ra = s.NumeroRegistroAcademico,
                        curso = s.Historicos.FirstOrDefault(h => h.SeqNivelEnsino == item.SeqNivelEnsino).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                        turno = s.Historicos.FirstOrDefault(h => h.SeqNivelEnsino == item.SeqNivelEnsino).CursoOfertaLocalidadeTurno.Turno.Descricao,
                        ciclo = s.Historicos.FirstOrDefault(h => h.SeqNivelEnsino == item.SeqNivelEnsino).HistoricosCicloLetivo.OrderByDescending(c => c.Seq).FirstOrDefault(c => !c.DataExclusao.HasValue).CicloLetivo.Descricao,
                        tipoSituacao = s.Historicos.FirstOrDefault(h => h.SeqNivelEnsino == item.SeqNivelEnsino)
                                        .HistoricosCicloLetivo.OrderByDescending(c => c.Seq).FirstOrDefault(c => !c.DataExclusao.HasValue)
                                        .AlunoHistoricoSituacao.OrderByDescending(h => h.DataInicioSituacao).FirstOrDefault(h => h.DataInicioSituacao <= DateTime.Today && !h.DataExclusao.HasValue)
                                        .SituacaoMatricula.TipoSituacaoMatricula.Descricao
                    });

                    orientacaoPessoaAtuacao.RA = aluno.ra.ToString();
                    orientacaoPessoaAtuacao.DescricaoOfertaCursoLocalidade = aluno.curso;
                    orientacaoPessoaAtuacao.Turno = aluno.turno;

                    if (string.IsNullOrEmpty(orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.NomeSocial))
                    {
                        orientacaoPessoaAtuacao.Nome = orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.Nome;
                        ///Label mensagem de exclusao
                        NomesAlunosExclucao += string.IsNullOrEmpty(NomesAlunosExclucao) ? orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.Nome : " e " + orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.Nome;
                    }
                    else
                    {
                        orientacaoPessoaAtuacao.Nome = orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.NomeSocial + " (" + orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.Nome + ")";
                        ///Label mensagem de exclusao
                        NomesAlunosExclucao += string.IsNullOrEmpty(NomesAlunosExclucao) ? orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.NomeSocial + " (" + orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.Nome + ")" : " e " + orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.NomeSocial + " (" + orientacaoPessoaAtuacao.PessoaAtuacao.DadosPessoais.Nome + ")";
                    }

                    ///Na função que retorna a situação traz o seq do ciclo atual.
                    var situacaoCiclo = this.AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(orientacaoPessoaAtuacao.SeqPessoaAtuacao);
                    orientacaoPessoaAtuacao.DadosCicloCompleto = $"{situacaoCiclo.DescricaoCicloLetivo} - {situacaoCiclo.Descricao}";
                }

                item.NomesAlunosExclucao = NomesAlunosExclucao;

                foreach (var orientacaoColaborador in item.OrientacoesColaborador)
                {
                    orientacaoColaborador.Nome = (orientacaoColaborador.Transform<OrientacaoColaborador>().Colaborador as PessoaAtuacao).DadosPessoais.Nome;

                    if (orientacaoColaborador.DataFimOrientacao == null || orientacaoColaborador.DataFimOrientacao == new DateTime())
                    {
                        orientacaoColaborador.DataFormatada = $"Inicio: {orientacaoColaborador.DataInicioOrientacao?.ToShortDateString()}";
                    }
                    else
                    {
                        orientacaoColaborador.DataFormatada = $"De: {orientacaoColaborador.DataInicioOrientacao?.ToShortDateString()}  à  {orientacaoColaborador.DataFimOrientacao?.ToShortDateString()}";
                    }
                }
            }

            return new SMCPagerData<OrientacaoVO>(resultVO, total);
        }

        /// <summary>
        /// Buscar todas as orientação de uma determinda divisao de turma
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial de uma divisão de turma</param>
        /// <returns>Todas as orientações de uma divisão de turma</returns>
        public SMCPagerData<OrientacaoTurmaVO> BuscarOrientacoesPorDivisaoTurma(OrientacaoTurmaFiltroVO filtro)
        {
            var total = 0;

            var spec = filtro.Transform<PlanoEstudoItemFilterSpecification>();
            spec.SeqAluno = filtro.SeqPessoaAtuacao;
            spec.PlanoEstudoAtual = true;

            //Converte a string de tokens caso exista, para uma lista
            if (!string.IsNullOrEmpty(filtro.TokensTipoSituacaoMaticula))
            {
                spec.ListaTokensTipoSituacaoMaticula = filtro.TokensTipoSituacaoMaticula.Split(',').ToList();
            }

            spec.SetOrderBy(o => o.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.Nome);

            var retorno = this.PlanoEstudoItemDomainService.SearchProjectionBySpecification(spec, p => new OrientacaoTurmaVO
            {
                Seq = p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.Seq,
                SeqNivelEnsino = p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqNivelEnsino,
                SeqOrientacao = p.SeqOrientacao,
                SeqPessoaAtuacao = p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.Seq,
                NomeSocial = p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.NomeSocial,
                Nome = p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.Nome,
                RA = p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.NumeroRegistroAcademico,
                DescricaoSituacaoMatricula = p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Now && !s.DataExclusao.HasValue).SituacaoMatricula.Descricao,
                TokenTipoSituacaoMatricula = p.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Now && !s.DataExclusao.HasValue).SituacaoMatricula.TipoSituacaoMatricula.Token,
                DiarioFechado = p.DivisaoTurma.Turma.HistoricosFechamentoDiario.Count > 0 ? p.DivisaoTurma.Turma.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
                Colaboradores = p.Orientacao.OrientacoesColaborador.Select(s => new OrientacaoColaboradorVO()
                {
                    Nome = s.Colaborador.DadosPessoais.Nome,
                    NomeSocial = s.Colaborador.DadosPessoais.NomeSocial,
                    TipoParticipacaoOrientacao = s.TipoParticipacaoOrientacao,
                    SeqColaborador = s.SeqColaborador,
                    DataInicioOrientacao = s.DataInicioOrientacao,
                    DataFimOrientacao = s.DataFimOrientacao,
                    VinculoAtivo = !s.Colaborador.Professores.Any() || s.Colaborador.Professores.Any(a => a.SituacaoProfessor == SituacaoProfessor.Normal),
                }).ToList(),
                SeqDivisaoTurma = (long)filtro.SeqDivisaoTurma,///Voltar para a lista o sequencial da divisao de turma
                SeqTurma = (long)filtro.SeqTurma///Voltar para a lista o sequencial turma
            }, out total).ToList();

            ///Efeturar todas as validações que faltaram
            ///- Nome social tanto do Aluno quanto do Colaborador
            ///- Criar lista de longs do Seq do aluno para o lookup funcionar de forma correta trazendo o aluno selecionado.
            foreach (var item in retorno)
            {
                if (!string.IsNullOrEmpty(item.NomeSocial))
                {
                    item.Nome = $"{item.NomeSocial} ({item.Nome})";
                }

                item.SeqsAlunos = new long[] { item.Seq };

                item.RaNome = $"{item.RA} - {(string.IsNullOrEmpty(item.NomeSocial) ? item.Nome : $"{ item.NomeSocial } ({ item.Nome })")}";

                foreach (var colaborador in item.Colaboradores)
                {
                    colaborador.DadosColaboradorCompleto = string.IsNullOrEmpty(colaborador.NomeSocial) ? colaborador.Nome : $"{colaborador.NomeSocial} ({colaborador.Nome})";
                }
            }

            return new SMCPagerData<OrientacaoTurmaVO>(retorno, total);
        }

        /// <summary>
        /// Buscar orientação de uma determinado filtro
        /// </summary>
        /// <param name="filtro">Parametros para filtrar a orientação da turma</param>
        /// <returns>Orientação de uma divisão de turma</returns>
        public OrientacaoTurmaVO BuscarOrientacaoPorDivisaoTurma(OrientacaoTurmaFiltroVO filtro)
        {
            ///Buscar os plano de estudo itens da divisao de turma
            var planoEstudoItem = this.PlanoEstudoItemDomainService.BuscarAlunosPlanoEstudoItemPorDivisaoTurma(new PlanoEstudoItemFilterSpecification()
            {
                SeqDivisaoTurma = filtro.SeqDivisaoTurma,
                SeqAluno = filtro.Seq,
                PlanoEstudoAtual = true
            }).FirstOrDefault();

            OrientacaoTurmaVO result = new OrientacaoTurmaVO();

            result.Seq = planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.Seq;
            result.SeqNivelEnsino = planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqNivelEnsino;
            result.SeqOrientacao = planoEstudoItem.SeqOrientacao;
            result.SeqsAlunos = new long[] { planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.Seq };
            result.SeqPessoaAtuacao = planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.Seq;
            ///Buscar o tipo de participação parametrizado
            result.TipoParticipacaoOrientacao = (TipoParticipacaoOrientacao)this.DivisaoTurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>((long)planoEstudoItem.SeqDivisaoTurma),
                                                p => p.DivisaoComponente.TipoDivisaoComponente.TipoParticipacaoOrientacao);

            result.Nome = string.IsNullOrEmpty(planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.NomeSocial) ? planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.Nome : $"{planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.NomeSocial} ({planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.Nome})";
            result.RaNome = $"{planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.NumeroRegistroAcademico} - {result.Nome}";
            result.Colaboradores = new List<OrientacaoColaboradorVO>();

            //Validar se historico escolar existe
            var specHistoricoEscolar = new HistoricoEscolarFilterSpecification()
            {
                SeqOrigemAvaliacao = planoEstudoItem.DivisaoTurma.Turma.SeqOrigemAvaliacao,
                SeqAlunoHistorico = planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqAlunoHistorico
            };
            result.ExisteHistoricoEscolarAluno = this.HistoricoEscolarDomainService.SearchProjectionBySpecification(specHistoricoEscolar, p => p.Seq).SMCAny();

            foreach (var colaboradores in planoEstudoItem.Orientacao.OrientacoesColaborador)
            {
                OrientacaoColaboradorVO colaborador = new OrientacaoColaboradorVO();

                colaborador.Colaborador = new Colaborador();

                colaborador.DadosColaboradorCompleto = string.IsNullOrEmpty(colaboradores.Colaborador.DadosPessoais.NomeSocial) ? colaboradores.Colaborador.DadosPessoais.Nome : $"{colaboradores.Colaborador.DadosPessoais.NomeSocial} ({colaboradores.Colaborador.DadosPessoais.Nome})";
                colaborador.SeqColaborador = colaboradores.SeqColaborador;
                colaborador.TipoParticipacaoOrientacao = colaboradores.TipoParticipacaoOrientacao;
                colaborador.ColaboradorNameDescriptionField = colaborador.DadosColaboradorCompleto;
                result.Colaboradores.Add(colaborador);
            }

            return result;
        }

        /// <summary>
        /// Salva uma orientação
        /// </summary>
        /// <param name="dadosOrientacao">Dados da orientação a ser salva</param>
        /// <returns>Sequencial da orientação salva</returns>
        public long SalvarOrientacao(OrientacaoVO dadosOrientacao)
        {
            var orientacao = dadosOrientacao.Transform<Orientacao>();

            // Verifica se as participações da orientação são válidas
            string orientacaoValida = string.Empty;
            orientacaoValida = ValidarParticipacaoOrientacao(dadosOrientacao).OrientacoesFaltantes;
            if (!string.IsNullOrEmpty(orientacaoValida))
                throw new OrientacaoObrigatoriaException((dadosOrientacao.Seq == 0 ? INCLUSAO : ALTERAR), orientacaoValida);

            var orientacoesSemVinculosAtivos = this.ValidarVinculosAtivosOrientacoes(dadosOrientacao);
            if (!string.IsNullOrEmpty(orientacoesSemVinculosAtivos))
                throw new ProfessorNaoTemVinculoValidoException(orientacoesSemVinculosAtivos);

            /// Não é possível associar o mesmo colaborador com o mesmo período de datas ou com períodos sobrepostos.
            /// Caso ocorra, abortar a operação e exibir e seguinte mensagem de erro:
            /// Professor 1 – De 01/01/2018 a 31/12/2018  -- Professor 1 – Início 03/05/2018
            var colaboradoresMesmos = orientacao.OrientacoesColaborador.GroupBy(g => g.SeqColaborador).Where(w => w.Count() > 1).SelectMany(s => s).OrderBy(o => o.DataInicioOrientacao).ToList();

            if (colaboradoresMesmos.SMCAny())
            {
                for (int i = 0; i < colaboradoresMesmos.Count(); i++)
                {
                    if (i + 1 < colaboradoresMesmos.Count())///Verificar se existe mais itens na lista para serem comparados e não estourar a lista
                    {
                        if (colaboradoresMesmos[i + 1].DataInicioOrientacao >= colaboradoresMesmos[i].DataInicioOrientacao
                           && (colaboradoresMesmos[i + 1].DataInicioOrientacao <= colaboradoresMesmos[i].DataFimOrientacao || colaboradoresMesmos[i].DataFimOrientacao == null))
                        {
                            throw new OrientacaoComColaboradoresPeriodoSobrepostoException(orientacao.Seq == 0 ? INCLUSAO : ALTERAR);
                        }
                    }
                }
            }

            var colaboradoresOrdenados = orientacao.OrientacoesColaborador.OrderBy(o => o.DataInicioOrientacao).ToList();

            if (colaboradoresOrdenados.SMCAny())
            {
                int colaboradoresCoutuela = 0;

                for (int i = 0; i < colaboradoresOrdenados.Count(); i++)
                {
                    if (i + 1 < colaboradoresOrdenados.Count())///Verificar se existe mais itens na lista para serem comparados e não estourar a lista
                    {
                        if (colaboradoresOrdenados[i + 1].DataInicioOrientacao >= colaboradoresOrdenados[i].DataInicioOrientacao
                           && (colaboradoresOrdenados[i + 1].DataInicioOrientacao <= colaboradoresOrdenados[i].DataFimOrientacao || colaboradoresOrdenados[i].DataFimOrientacao == null))
                        {
                            // Para orientações do tipo de TCC:
                            if (this.TipoOrientacaoDomainService.ValidarTipoOrientacaoConclucaoCurso(orientacao.SeqTipoOrientacao))
                            {
                                // Se o tipo de termo de intercâmbio não tiver sido informado, só poderá existir um colaborador com o
                                // tipo de participação “Orientador” no período. Ou seja, não permitir o registro de mais que um
                                // “Orientador” em períodos coincidentes.
                                if (orientacao.SeqTipoTermoIntercambio == null)
                                {
                                    if (colaboradoresOrdenados[i].TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador
                                        && colaboradoresOrdenados[i + 1].TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador)
                                    {
                                        throw new OrientacaoColaboradorMesmoTipoParticipacaoException(orientacao.Seq == 0 ? INCLUSAO : ALTERAR);
                                    }
                                }
                                else
                                {
                                    // Se o tipo de termo de intercâmbio for igual a “Cotutela, só poderá existir dois colaboradores com o tipo de
                                    // participação “Orientador” no período.Ou seja, não permitir o registro de mais que dois “Orientadores” em períodos coincidentes.
                                    if (this.TipoTermoIntercambioDomainService.ValidarTipoTermoIntercambioCoutela((long)orientacao.SeqTipoTermoIntercambio))
                                    {
                                        if (colaboradoresOrdenados[i].TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador
                                        && colaboradoresOrdenados[i + 1].TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador)
                                        {
                                            colaboradoresCoutuela++;
                                        }
                                    }

                                    if (colaboradoresCoutuela > 1)
                                    {
                                        throw new OrientacaoColaboradorMesmoTipoParticipacaoCotutelaException(orientacao.Seq == 0 ? INCLUSAO : ALTERAR);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                // Validar alunos em questão de bloqueios
                foreach (var pessoaAtuacao in orientacao.OrientacoesPessoaAtuacao)
                {
                    var specListaBloqueio = new PessoaAtuacaoBloqueioFilterSpecification() { SeqPessoaAtuacao = pessoaAtuacao.SeqPessoaAtuacao };
                    var includesPessoaAtuacaoBloqueio = IncludesPessoaAtuacaoBloqueio.MotivoBloqueio
                                                        | IncludesPessoaAtuacaoBloqueio.MotivoBloqueio_TipoBloqueio
                                                        | IncludesPessoaAtuacaoBloqueio.Itens
                                                        | IncludesPessoaAtuacaoBloqueio.PessoaAtuacao
                                                        | IncludesPessoaAtuacaoBloqueio.PessoaAtuacao_Pessoa;

                    var listaBloqueio = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(specListaBloqueio, includesPessoaAtuacaoBloqueio).ToList();

                    // RN_ALN_053 - Desbloqueio orientação
                    // Verificar a pessoa - atuação possui o bloqueio acadêmico
                    // "ORIENTACAO_PENDENTE", com situação "Bloqueado", cujo item é o tipo de orientação que está sendo cadastrado.
                    foreach (var bloqueio in listaBloqueio)
                    {
                        if (bloqueio.MotivoBloqueio.Token == TOKEN_MOTIVO_BLOQUEIO.ORIENTACAO_PENDENTE
                               && bloqueio.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)
                        {
                            foreach (var item in bloqueio.Itens)
                            {
                                // Se possuir e a situação do item for "Bloqueado", atualizar o item do bloqueio
                                if (Convert.ToInt64(item.CodigoIntegracaoSistemaOrigem) == orientacao.SeqTipoOrientacao
                                   && item.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)
                                {
                                    var bloqueioItem = this.PessoaAtuacaoBloqueioItemDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBloqueioItem>(item.Seq));

                                    bloqueioItem.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                    bloqueioItem.DataDesbloqueio = DateTime.Now;
                                    bloqueioItem.UsuarioDesbloqueio = SMCContext.User.Identity.Name;

                                    this.PessoaAtuacaoBloqueioItemDomainService.SaveEntity(bloqueioItem);
                                }
                            }

                            // Verificar se todos os itens do bloqueio estão com a situação "Desbloqueado".Se estiverem, atualizar o bloqueio conforme:
                            if (bloqueio.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)
                            {
                                var bloqueioBD = this.PessoaAtuacaoBloqueioDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBloqueio>(bloqueio.Seq), IncludesPessoaAtuacaoBloqueio.Itens);

                                if (bloqueioBD.Itens.Any(a => Convert.ToInt64(a.CodigoIntegracaoSistemaOrigem) == orientacao.SeqTipoOrientacao))
                                {
                                    if (!bloqueioBD.Itens.Any(a => a.SituacaoBloqueio == SituacaoBloqueio.Bloqueado))
                                    {
                                        bloqueioBD.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                        bloqueioBD.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                                        bloqueioBD.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;
                                        bloqueioBD.DataDesbloqueioEfetivo = DateTime.Now;
                                        bloqueioBD.JustificativaDesbloqueio = "Cadastro de orientação realizado.";

                                        this.PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueioBD);
                                    }
                                }
                            }
                        }
                    }
                }
                this.SaveEntity(orientacao);

                unitOfWork.Commit();
            }

            return orientacao.Seq;
        }

        /// <summary>
        /// Buscar orientação de uma determinado filtro
        /// </summary>
        /// <param name="filtro">Parametros para filtrar a orientação da turma</param>
        /// <returns>Orientação de uma divisão de turma</returns>
        public long SalvarOrientacaoTurma(OrientacaoTurmaVO dadosOrietacao)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                var orientacao = this.AlterarOrientacao((long)dadosOrietacao.SeqOrientacao).Transform<Orientacao>();

                // Task 36089: Permitir salvar sem orientador
                //if (!dadosOrietacao.Colaboradores.SMCAny())
                //{
                //    throw new OrientacaoTurmaColaboradorObrigatorioException();
                //}

                var turma = this.TurmaDomainService.SearchProjectionByKey(dadosOrietacao.SeqTurma, p => new
                {
                    SeqCicloLetivo = p.CicloLetivoInicio.Seq,
                    SeqCursoOfertaLocalidadeTurno = p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqCursoOfertaLocalidadeTurno
                });

                var dataPeriodoLetivo = this.ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(turma.SeqCicloLetivo, turma.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                ///Faz a exclussão de todos os oritentadores
                if (orientacao.OrientacoesColaborador.Count > 0)
                {
                    foreach (var item in orientacao.OrientacoesColaborador)
                    {
                        this.OrientacaoColaboradorDomainService.DeleteEntity(item);
                    }
                }

                ///Faz a inclusão de todos os orientadores para aquela orientação
                foreach (var item in dadosOrietacao.Colaboradores)
                {
                    OrientacaoColaborador colaborador = new OrientacaoColaborador();
                    colaborador.SeqColaborador = item.SeqColaborador;
                    colaborador.SeqOrientacao = orientacao.Seq;
                    colaborador.SeqInstituicaoExterna = InstituicaoExternaDomainService.SearchProjectionBySpecification(new InstituicaoExternaFilterSpecification() { SeqInstituicaoEnsino = GetDataFilter(FILTER.INSTITUICAO_ENSINO).FirstOrDefault() },
                                                                                                                        p => p.Seq).FirstOrDefault();
                    colaborador.DataInicioOrientacao = dataPeriodoLetivo.DataInicio;
                    colaborador.DataFimOrientacao = dataPeriodoLetivo.DataFim;
                    colaborador.TipoParticipacaoOrientacao = item.TipoParticipacaoOrientacao;
                    this.OrientacaoColaboradorDomainService.SaveEntity(colaborador);
                }

                ///Regras de validação dos colaboradores
                this.ValidarColaboradores(dadosOrietacao.SeqDivisaoTurma, dadosOrietacao.SeqTurma, dadosOrietacao.Colaboradores, orientacao.OrientacoesColaborador.TransformList<OrientacaoColaboradorVO>());

                /* 4.Ao associar ou retirar o colaborador, se existir registro no histórico - escolar para o aluno para esta turma, exibir mensagem:
                MENSAGEM: "Existe um registro do histórico escolar para este aluno, esta alteração vai atualizar os dados de professor. Deseja continuar?"[Sim / Não]
                Se sim, atualiza o colaborador no histórico
                Se não, cancela operação */
                if (dadosOrietacao.ExisteHistoricoEscolarAluno)
                {
                    ///Buscar os plano de estudo itens da divisao de turma
                    var planoEstudoItem = this.PlanoEstudoItemDomainService.SearchProjectionBySpecification(new PlanoEstudoItemFilterSpecification()
                    {
                        SeqDivisaoTurma = dadosOrietacao.SeqDivisaoTurma,
                        SeqAluno = orientacao.OrientacoesPessoaAtuacao.FirstOrDefault().SeqPessoaAtuacao,
                        PlanoEstudoAtual = true
                    },
                    p => new
                    {
                        p.DivisaoTurma.Turma.SeqOrigemAvaliacao,
                        p.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqAlunoHistorico
                    }).FirstOrDefault();

                    var specHistoricoEscolar = new HistoricoEscolarFilterSpecification()
                    {
                        SeqOrigemAvaliacao = planoEstudoItem.SeqOrigemAvaliacao,
                        SeqAlunoHistorico = planoEstudoItem.SeqAlunoHistorico
                    };

                    var historicoEscolar = HistoricoEscolarDomainService.SearchProjectionBySpecification(specHistoricoEscolar, p => new
                    {
                        SeqHistoricoEscolar = p.Seq,
                        Colaboradores = p.Colaboradores.Select(s => s.Seq).ToList()
                    }).FirstOrDefault();

                    //Limpar os colaboradores
                    foreach (var item in historicoEscolar.Colaboradores)
                    {
                        this.HistoricoEscolarColaboradorDomainService.DeleteEntity(item);
                    }

                    //Atualiza com os colaboradores
                    foreach (var item in dadosOrietacao.Colaboradores)
                    {
                        var historicoEscolarColaborador = new HistoricoEscolarColaborador();
                        historicoEscolarColaborador.SeqHistoricoEscolar = historicoEscolar.SeqHistoricoEscolar;
                        historicoEscolarColaborador.SeqColaborador = item.SeqColaborador;
                        historicoEscolarColaborador.NomeColaborador = item.ColaboradorNameDescriptionField;
                        this.HistoricoEscolarColaboradorDomainService.SaveEntity(historicoEscolarColaborador);
                    }
                }

                unitOfWork.Commit();
            }
            return (long)dadosOrietacao.SeqOrientacao;
        }

        /// <summary>
        /// Se o colaborador que estiver sendo associado não estiver associado a nenhuma outra
        /// orientação vigente da divisão de turma em questão, associá-lo a divisão de turma (tabela divisao_turma_colaborador).
        ///
        /// Se o colaborador que estiver sendo desassociado não estiver associado a nenhuma outra orientação vigente da divisão
        /// de turma em questão, desassociá-lo da divisão de turma (tabela divisao_turma_colaborador).
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial divisao turma</param>
        /// <param name="colaboradoresOrientacaoTela">Lista colaboradores retornados da tela</param>
        /// <param name="colaboradoresOrientacaoBD">Lista de colaboradores retronados do banco de dados</param>
        private void ValidarColaboradores(long seqDivisaoTurma, long seqTurma, List<OrientacaoColaboradorVO> colaboradoresOrientacaoTela, List<OrientacaoColaboradorVO> colaboradoresOrientacaoBD)
        {
            var colaboradoresExcluir = colaboradoresOrientacaoBD.Select(s => s.SeqColaborador).Except(colaboradoresOrientacaoTela.Select(st => st.SeqColaborador)).ToList();
            var colaboradoresInserir = colaboradoresOrientacaoTela.Select(s => s.SeqColaborador).Except(colaboradoresOrientacaoBD.Select(sb => sb.SeqColaborador)).ToList();

            var orientacoes = this.BuscarOrientacoesPorDivisaoTurma(new OrientacaoTurmaFiltroVO() { SeqDivisaoTurma = seqDivisaoTurma, SeqTurma = seqTurma });

            if (colaboradoresExcluir.SMCAny())
            {
                foreach (var item in colaboradoresExcluir)
                {
                    var dadosDivisao = this.DivisaoTurmaColaboradorDomainService.SearchBySpecification(new DivisaoTurmaColaboradorFilterSpecification() { SeqColaborador = item, SeqDivisaoTurma = seqDivisaoTurma }).FirstOrDefault();

                    if (dadosDivisao != null && !orientacoes.SelectMany(s => s.Colaboradores.Where(w => w.SeqColaborador == item)).SMCAny())
                    {
                        this.DivisaoTurmaColaboradorDomainService.DeleteEntity(dadosDivisao);
                    }
                }
            }

            if (colaboradoresInserir.SMCAny())
            {
                foreach (var item in colaboradoresInserir)
                {
                    var dadosDivisao = this.DivisaoTurmaColaboradorDomainService.SearchBySpecification(new DivisaoTurmaColaboradorFilterSpecification() { SeqColaborador = item, SeqDivisaoTurma = seqDivisaoTurma }).FirstOrDefault();

                    if (dadosDivisao == null && orientacoes.SelectMany(s => s.Colaboradores.Where(w => w.SeqColaborador == item)).SMCAny())
                    {
                        DivisaoTurmaColaborador dadosDivisaoColaborador = new DivisaoTurmaColaborador();

                        dadosDivisaoColaborador.SeqColaborador = item;
                        dadosDivisaoColaborador.SeqDivisaoTurma = seqDivisaoTurma;
                        dadosDivisaoColaborador.QuantidadeCargaHoraria = 0;

                        this.DivisaoTurmaColaboradorDomainService.SaveEntity(dadosDivisaoColaborador);
                    }
                }
            }
        }

        public OrientacaoVO AlterarOrientacao(long seq)
        {
            var include = IncludesOrientacao.InstituicaoEnsino
                        | IncludesOrientacao.NivelEnsino
                        | IncludesOrientacao.OrientacoesColaborador
                        | IncludesOrientacao.OrientacoesColaborador_Colaborador
                        | IncludesOrientacao.OrientacoesColaborador_Colaborador_DadosPessoais
                        | IncludesOrientacao.OrientacoesPessoaAtuacao
                        | IncludesOrientacao.OrientacoesPessoaAtuacao_PessoaAtuacao
                        | IncludesOrientacao.OrientacoesPessoaAtuacao_PessoaAtuacao_DadosPessoais
                        | IncludesOrientacao.TipoOrientacao;

            var result = this.SearchByKey(new SMCSeqSpecification<Orientacao>(seq), include).Transform<OrientacaoVO>();

            if (result.SeqTipoTermoIntercambio.HasValue)
            {
                result.TipoTermoIntercambioNameDescriptionField = this.TipoTermoIntercambioDomainService.SearchProjectionByKey((long)result.SeqTipoTermoIntercambio, p => p.Descricao);
            }

            foreach (var item in result.OrientacoesColaborador)
            {
                item.InstituicoesExternas = InstituicaoExternaDomainService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroVO { SeqColaborador = item.SeqColaborador });
                item.InstituicaoExternaNameDescriptionField = InstituicaoExternaDomainService.SearchProjectionByKey(item.SeqInstituicaoExterna, p => p.Nome);
            }

            ///Preencher mensagem informativa
            string tiposParticipacao = string.Empty;

            var filtroOrientacoes = new InstituicaoNivelTipoOrientacaoFilterSpecification
            {
                SeqNivelEnsino = result.SeqNivelEnsino,
                SeqTipoIntercambio = result.SeqTipoTermoIntercambio,
                SeqTipoOrientacao = result.SeqTipoOrientacao,
                SeqTipoVinculoAluno = result.SeqTipoVinculoAluno,
            };

            if (!result.SeqTipoTermoIntercambio.HasValue)
            {
                filtroOrientacoes.ExcetoParceriaIntercambio = true;
            }

            var listaOrietnacoes = InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOritencoes(filtroOrientacoes);

            foreach (var item in listaOrietnacoes.SelectMany(x => x.TiposParticipacao.Where(w => w.ObrigatorioOrientacao == true).ToList()).ToList())
            {
                tiposParticipacao += "<br />" + item.TipoParticipacaoOrientacao.ToString() + " com a Instituição sendo " + SMCEnumHelper.GetDescription(item.OrigemColaborador);
            }

            result.MensagemIformativa = string.Format(MessagesResource.MSG_MensagemIformativa, tiposParticipacao);

            return result;
        }

        /// <summary>
        /// Verificar os tipos de participação obrigatórios em uma orientação parametrizado por instituição
        /// logada, nível de ensino, tipo de vínculo e tipo de termo(quando este for informado) e tipo de
        /// orientação selecionados. Caso exista algum tipo de participação que seja obrigatório e não tenha
        /// um professor com este tipo, exibir a mensagem de erro abaixo e abortar a operação.
        ///
        /// Mensagem:
        /// Inclusão/Alteração não permitida. Para o tipo de orientação informado é obrigatório que exista
        /// professor com o(s) tipo(s) de participação X, Y e Z.", onde X,Y e Z são os tipos de participação
        /// obrigatórios.
        /// </summary>
        /// <param name="dadosOrientacao">Dados da orientação a ser validada</param>
        /// <returns>Mensagem de erro da validação</returns>
        public (string OrientacoesFaltantes, string OrientacoesNaoParametrizadas) ValidarParticipacaoOrientacao(OrientacaoVO dadosOrientacao)
        {
            var orientacao = dadosOrientacao.Transform<Orientacao>();

            var filtroOrientacoes = new InstituicaoNivelTipoOrientacaoFilterSpecification
            {
                SeqNivelEnsino = orientacao.SeqNivelEnsino,
                SeqTipoIntercambio = orientacao.SeqTipoTermoIntercambio,
                SeqTipoOrientacao = orientacao.SeqTipoOrientacao,
                SeqTipoVinculoAluno = orientacao.SeqTipoVinculoAluno,
            };

            if (!orientacao.SeqTipoTermoIntercambio.HasValue)
                filtroOrientacoes.ExcetoParceriaIntercambio = true;

            // Recupera as configurações da orientação
            var orientacaoParticipacoes = InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOritencoes(filtroOrientacoes);

            string participacoesObrigatorias = string.Empty;
            string participacoesSobrando = string.Empty;

            // Caso tenha alguma orientação
            if (orientacaoParticipacoes != null)
            {
                // Separa as orientações em obrigatoria ou não obrigatória
                var listaOrientacoesTipoParticipacaoObrigatorias = orientacaoParticipacoes.SelectMany(x => x.TiposParticipacao.Where(w => w.ObrigatorioOrientacao).ToList()).ToList();
                var listaOrientacoesTipoParticipacaoNaoObrigatorias = orientacaoParticipacoes.SelectMany(x => x.TiposParticipacao.Where(w => !w.ObrigatorioOrientacao).ToList()).ToList();

                // Armazena os colaboradores que passaram na validação e são válidos
                var listaOrientacoesValidas = new List<OrientacaoColaboradorVO>();

                // Recupera se um colaborador é interno ou externo
                Dictionary<OrientacaoColaboradorVO, bool> origemColaboradores = new Dictionary<OrientacaoColaboradorVO, bool>();
                if (dadosOrientacao.OrientacoesColaborador != null)
                {
                    foreach (var colaborador in dadosOrientacao.OrientacoesColaborador)
                    {
                        if (!origemColaboradores.ContainsKey(colaborador))
                        {
                            var origemColaborador = this.InstituicaoExternaDomainService.ValidarInstituicaoInternaOuExterna(colaborador.SeqInstituicaoExterna);
                            origemColaboradores.Add(colaborador, origemColaborador);
                        }
                    }
                }

                // Para cada obrigatória, verifica se o aluno possui a orientação
                foreach (var item in listaOrientacoesTipoParticipacaoObrigatorias)
                {
                    var orientacoesAdicionar = dadosOrientacao.OrientacoesColaborador?.Where(a => a.TipoParticipacaoOrientacao == item.TipoParticipacaoOrientacao).ToList() ?? new List<OrientacaoColaboradorVO>();
                    if (item.OrigemColaborador == OrigemColaborador.Interno)
                        orientacoesAdicionar = orientacoesAdicionar.Where(a => origemColaboradores[a]).ToList();
                    else if (item.OrigemColaborador == OrigemColaborador.Externo)
                        orientacoesAdicionar = orientacoesAdicionar.Where(a => !origemColaboradores[a]).ToList();

                    if (!orientacoesAdicionar.Any())
                        participacoesObrigatorias += "<br />" + item.TipoParticipacaoOrientacao.ToString() + " com a Instituição sendo " + SMCEnumHelper.GetDescription(item.OrigemColaborador);
                    else
                        listaOrientacoesValidas.AddRange(orientacoesAdicionar);
                }

                // Para cada obrigatória, verifica se o aluno possui a orientação
                foreach (var item in listaOrientacoesTipoParticipacaoNaoObrigatorias)
                {
                    var orientacoesAdicionar = dadosOrientacao.OrientacoesColaborador?.Where(a => a.TipoParticipacaoOrientacao == item.TipoParticipacaoOrientacao).ToList() ?? new List<OrientacaoColaboradorVO>();
                    if (item.OrigemColaborador == OrigemColaborador.Interno)
                        orientacoesAdicionar = orientacoesAdicionar.Where(a => origemColaboradores[a]).ToList();
                    else if (item.OrigemColaborador == OrigemColaborador.Externo)
                        orientacoesAdicionar = orientacoesAdicionar.Where(a => !origemColaboradores[a]).ToList();

                    if (orientacoesAdicionar.Any())
                        listaOrientacoesValidas.AddRange(orientacoesAdicionar);
                }

                // Verifica se existe algum item que é de um tipo não informado na orientação
                if (dadosOrientacao.OrientacoesColaborador != null)
                {
                    foreach (var item in dadosOrientacao.OrientacoesColaborador.Where(o => !listaOrientacoesValidas.Contains(o)))
                        participacoesSobrando += "<br />" + item.TipoParticipacaoOrientacao + " com a Instituição sendo " + (origemColaboradores[item] ? "interna" : "externa");
                }
            }

            return (participacoesObrigatorias, participacoesSobrando);
        }

        /// <summary>
        /// Excluir orientação
        /// </summary>
        /// <param name="seq">Sequencial da orientação</param>
        public void ExcluirOrientacao(long seq)
        {
            var includes = IncludesOrientacao.OrientacoesColaborador
                          | IncludesOrientacao.OrientacoesPessoaAtuacao
                          | IncludesOrientacao.OrientacoesPessoaAtuacao_PessoaAtuacao
                          | IncludesOrientacao.OrientacoesPessoaAtuacao_PessoaAtuacao_DadosPessoais
                          | IncludesOrientacao.NivelEnsino
                          | IncludesOrientacao.InstituicaoEnsino
                          | IncludesOrientacao.TipoOrientacao
                          | IncludesOrientacao.OrientacoesColaborador_Colaborador
                          | IncludesOrientacao.OrientacoesColaborador_Colaborador_DadosPessoais;

            var orientacao = this.SearchByKey(new SMCSeqSpecification<Orientacao>(seq), includes);

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                ///Validar alunos em questão de bloqueios
                foreach (var pessoaAtuacao in orientacao.OrientacoesPessoaAtuacao)
                {
                    var specListaBloqueio = new PessoaAtuacaoBloqueioFilterSpecification() { SeqPessoaAtuacao = pessoaAtuacao.SeqPessoaAtuacao };
                    var includesPessoaAtuacaoBloqueio = IncludesPessoaAtuacaoBloqueio.MotivoBloqueio
                                                        | IncludesPessoaAtuacaoBloqueio.MotivoBloqueio_TipoBloqueio
                                                        | IncludesPessoaAtuacaoBloqueio.Itens
                                                        | IncludesPessoaAtuacaoBloqueio.PessoaAtuacao
                                                        | IncludesPessoaAtuacaoBloqueio.PessoaAtuacao_Pessoa;

                    var listaBloqueio = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(specListaBloqueio, includesPessoaAtuacaoBloqueio).ToList();

                    ///RN_ALN_054 - Incluir bloqueio orientação - exclusão
                    ///1.Verificar se o tipo de orientação em questão está
                    ///parametrizado, por instituição - nível - vínculo - tipo orientação, para ser exigido para aluno(caso a
                    ///pessoa - atuação possua um termo de intercâmbio, verificar se o tipo de orientação está parametrizado
                    ///como exigido para o tipo de termo associado.Se não estiver, verificar a parametrização
                    ///desconsiderando o tipo de termo; se estiver, considerar o tipo de termo na verificação).
                    var dadosAluno = this.AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(pessoaAtuacao.SeqPessoaAtuacao),
                                                                                   p => new
                                                                                   {
                                                                                       SeqTipoVinculo = p.SeqTipoVinculoAluno,
                                                                                       SeqNivelEnsino = p.Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino
                                                                                   });
                    var instituicaoNivelTipoVinculo = this.InstituicaoNivelTipoVinculoAlunoDomainService.SearchBySpecification(new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
                    {
                        SeqNivelEnsino = dadosAluno.SeqNivelEnsino,
                        SeqTipoVinculoAluno = dadosAluno.SeqTipoVinculo
                    }).ToList();

                    var seqInstituicaoNivelTipoVinculo = instituicaoNivelTipoVinculo.FirstOrDefault().Seq;

                    var listaSeqTipoTermoIntercambio = this.PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(new PessoaAtuacaoTermoIntercambioFilterSpecification() { SeqPessoaAtuacao = pessoaAtuacao.SeqPessoaAtuacao }, p => p.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio).ToList();

                    List<long> listaSeqInstituicaoNivelTipoTermoIntercambio = new List<long>();

                    foreach (var item in listaSeqTipoTermoIntercambio)
                    {
                        listaSeqInstituicaoNivelTipoTermoIntercambio.Add(this.InstituicaoNivelTipoTermoIntercambioDomainService.SearchBySpecification(new InstituicaoNivelTipoTermoIntercambioFilterSpecification()
                        {
                            SeqTipoTermoIntercambio = item,
                            SeqInstituicaoNivelTipoVinculoAluno = seqInstituicaoNivelTipoVinculo
                        }).FirstOrDefault().Seq);
                    }

                    bool exigeCadastroAluno = false;

                    ///Verificação se os tipos de termos achados estão parametrizados para exigir o cadastro orientação para o aluno
                    ///validando se o termo com seqInstituicaoNivelTipoVinculo, seqTipoOrientacao e seq
                    if (listaSeqInstituicaoNivelTipoTermoIntercambio.Count > 0)
                    {
                        foreach (var item in listaSeqInstituicaoNivelTipoTermoIntercambio)
                        {
                            var validarTermo = this.InstituicaoNivelTipoOrientacaoDomainService.SearchBySpecification(new InstituicaoNivelTipoOrientacaoFilterSpecification() { SeqInstituicaoNivelTipoVinculoAluno = seqInstituicaoNivelTipoVinculo, SeqTipoOrientacao = orientacao.SeqTipoOrientacao, SeqInstituicaoNivelTipoTermoIntercambio = item }).ToList();

                            if (validarTermo.Count > 0)
                            {
                                exigeCadastroAluno = (validarTermo.FirstOrDefault().CadastroOrientacaoAluno == CadastroOrientacao.Exige ? true : false);
                            }
                        }
                    }

                    ///Caso a não exista um tipo temo intercambio ou existindo com o cadastro orientação aluno não sendo do tipo exige
                    ///validaremos somente o tipo de orientação
                    if (!exigeCadastroAluno)
                    {
                        var listaTipoOrientacoes = this.InstituicaoNivelTipoOrientacaoDomainService.SearchBySpecification(new InstituicaoNivelTipoOrientacaoFilterSpecification() { SeqInstituicaoNivelTipoVinculoAluno = seqInstituicaoNivelTipoVinculo, SeqTipoOrientacao = orientacao.SeqTipoOrientacao }).ToList();

                        var cadastroOrientacaoAluno = listaTipoOrientacoes.FirstOrDefault(f => f.SeqInstituicaoNivelTipoTermoIntercambio == null).CadastroOrientacaoAluno;

                        exigeCadastroAluno = (cadastroOrientacaoAluno == CadastroOrientacao.Exige ? true : false);
                    }

                    ///2.Se o tipo não estiver parametrizado como exigido para o aluno, não realizar nenhuma ação.

                    if (exigeCadastroAluno)
                    {
                        ///1.1 Se estiver, verificar se a pessoa - atuação possui outra orientação cadastrada com este tipo sem data
                        ///fim(além da que está sendo encerrada).
                        var includesOrientacaoPessoaAtuacao = IncludesOrientacaoPessoaAtuacao.Orientacao | IncludesOrientacaoPessoaAtuacao.Orientacao_TipoOrientacao;
                        var orientacoesPessoaAtuacao = this.OrientacaoPessoaAtuacaoDomainService.SearchBySpecification(
                                                       new OrientacaoPessoaAtuacaoFilterSpecification()
                                                       {
                                                           SeqPessoaAtuacao = pessoaAtuacao.SeqPessoaAtuacao,
                                                           SeqsTipoOrientacao = new long[] { orientacao.SeqTipoOrientacao }
                                                       }, includesOrientacaoPessoaAtuacao).ToList();

                        bool possiuOutraOrientação = false;

                        foreach (var item in orientacoesPessoaAtuacao)
                        {
                            if (item.Orientacao.TipoOrientacao.Seq == orientacao.SeqTipoOrientacao
                                && item.Orientacao.Seq != orientacao.Seq)
                            {
                                possiuOutraOrientação = true;
                            }
                        }

                        ///1.2 Se ela possuir outra orientação, não realizar nenhuma ação.

                        if (!possiuOutraOrientação)
                        {
                            ///1.1.1 Se não possuir, verificar se a pessoa-atuação possui um bloqueio com o token
                            ///ORIENTACAO_PENDENTE com a situação não "Bloqueado".
                            foreach (var bloqueio in listaBloqueio)
                            {
                                if (bloqueio.MotivoBloqueio.Token == TOKEN_MOTIVO_BLOQUEIO.ORIENTACAO_PENDENTE
                                   && bloqueio.SituacaoBloqueio != SituacaoBloqueio.Bloqueado)
                                {
                                    ///1.1.1.1 Se possuir, verificar se para este bloqueio existe um item, cujo sequencial da integração é igual ao tipo de
                                    ///orientação da orientação que está sendo encerrada, com a situação "Bloqueado".
                                    foreach (var item in bloqueio.Itens)
                                    {
                                        ///1.1.1.1.2 Se não, verificar se para este bloqueio existe um item, cujo sequencial da integração é igual
                                        ///ao tipo de orientação da orientação que está sendo encerrada, com a situação "Desbloqueado".
                                        if (Convert.ToInt64(item.CodigoIntegracaoSistemaOrigem) == orientacao.SeqTipoOrientacao
                                            && item.SituacaoBloqueio == SituacaoBloqueio.Desbloqueado)
                                        {
                                            ///1.1.1.1.2.1 Se existir, alterar a situação do item correspondente ao tipo de orientação em questão para "Bloqueado"
                                            item.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                                            item.DataDesbloqueio = null;
                                            item.UsuarioDesbloqueio = null;

                                            this.PessoaAtuacaoBloqueioItemDomainService.SaveEntity(item);
                                        }
                                    }

                                    ///1.1.1.1.2.2 Se o bloqueio não possuir o tipo de orientação como item, incluir um item no bloqueio, conforme:
                                    if (!bloqueio.Itens.Any(a => Convert.ToInt64(a.CodigoIntegracaoSistemaOrigem) == orientacao.SeqTipoOrientacao))
                                    {
                                        PessoaAtuacaoBloqueioItem pessoaAtuacaoBloqueioItem = new PessoaAtuacaoBloqueioItem();
                                        pessoaAtuacaoBloqueioItem.Descricao = orientacao.TipoOrientacao.Descricao;
                                        pessoaAtuacaoBloqueioItem.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                                        pessoaAtuacaoBloqueioItem.CodigoIntegracaoSistemaOrigem = orientacao.SeqTipoOrientacao.ToString();
                                        pessoaAtuacaoBloqueioItem.DataDesbloqueio = null;

                                        bloqueio.Itens.Add(pessoaAtuacaoBloqueioItem);
                                        this.PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueio);
                                    }

                                    ///Atualiza o bloqueio
                                    bloqueio.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                                    bloqueio.DataBloqueio = DateTime.Now;
                                    bloqueio.TipoDesbloqueio = null;
                                    bloqueio.DataDesbloqueioEfetivo = null;
                                    bloqueio.UsuarioDesbloqueioEfetivo = null;
                                    bloqueio.DataDesbloqueioTemporario = null;
                                    bloqueio.UsuarioDesbloqueioTemporario = null;
                                    bloqueio.JustificativaDesbloqueio = null;

                                    this.PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueio);
                                }
                            }

                            var specListaAtulazicada = new PessoaAtuacaoBloqueioFilterSpecification() { SeqPessoaAtuacao = pessoaAtuacao.SeqPessoaAtuacao };
                            var includesListaAtualizada = IncludesPessoaAtuacaoBloqueio.MotivoBloqueio
                                                                | IncludesPessoaAtuacaoBloqueio.MotivoBloqueio_TipoBloqueio
                                                                | IncludesPessoaAtuacaoBloqueio.Itens
                                                                | IncludesPessoaAtuacaoBloqueio.PessoaAtuacao
                                                                | IncludesPessoaAtuacaoBloqueio.PessoaAtuacao_Pessoa;

                            ///Atualiza a lista de bloqueio caso ela tenha sofrido alguma interação anterior
                            var listaBloqueioAtualizada = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(specListaAtulazicada, includesListaAtualizada).ToList();

                            ///1.1.1.2 Se o bloqueio existir, com a situação "Desbloqueado" ou se a pessoa-atuação não possuir o
                            ///bloqueio ORIENTACAO_PENDENTE associado, incluir um bloqueio para ela, conforme:
                            if (!listaBloqueioAtualizada.Any(a => a.MotivoBloqueio.Token == TOKEN_MOTIVO_BLOQUEIO.ORIENTACAO_PENDENTE
                                                  && a.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)
                                                  || !listaBloqueioAtualizada.Any(a => a.MotivoBloqueio.Token == TOKEN_MOTIVO_BLOQUEIO.ORIENTACAO_PENDENTE))
                            {
                                PessoaAtuacaoBloqueio pessoaAtuacaoBloqueio = new PessoaAtuacaoBloqueio();
                                pessoaAtuacaoBloqueio.CadastroIntegracao = true;
                                pessoaAtuacaoBloqueio.DataBloqueio = DateTime.Now;
                                pessoaAtuacaoBloqueio.Descricao = "Cadastro de orientação pendente";
                                pessoaAtuacaoBloqueio.DescricaoReferenciaAtuacao = this.PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(pessoaAtuacao.SeqPessoaAtuacao), p => p.Descricao);
                                pessoaAtuacaoBloqueio.SeqMotivoBloqueio = this.MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.ORIENTACAO_PENDENTE);
                                pessoaAtuacaoBloqueio.SeqPessoaAtuacao = pessoaAtuacao.SeqPessoaAtuacao;
                                pessoaAtuacaoBloqueio.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;

                                PessoaAtuacaoBloqueioItem pessoaAtuacaoBloqueioItem = new PessoaAtuacaoBloqueioItem();
                                pessoaAtuacaoBloqueioItem.Descricao = orientacao.TipoOrientacao.Descricao;
                                pessoaAtuacaoBloqueioItem.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                                pessoaAtuacaoBloqueioItem.CodigoIntegracaoSistemaOrigem = orientacao.SeqTipoOrientacao.ToString();

                                pessoaAtuacaoBloqueio.Itens = new List<PessoaAtuacaoBloqueioItem>();

                                pessoaAtuacaoBloqueio.Itens.Add(pessoaAtuacaoBloqueioItem);

                                this.PessoaAtuacaoBloqueioDomainService.SaveEntity(pessoaAtuacaoBloqueio);
                            }
                        }
                    }
                }

                this.DeleteEntity(orientacao);

                ///Roolback caso ocorra algum erro
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Buscar orientacoes do aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Lista das orientações do aluno</returns>
        public List<OrientacaoVO> BuscarOrientacoesPorAluno(long seqPessoaAtuacao)
        {
            var spec = new OrientacaoFilterSpecification() { SeqPessoaAtuacao = seqPessoaAtuacao };

            var retorno = this.SearchProjectionBySpecification(spec, p => new OrientacaoVO()
            {
                Seq = p.Seq,
                SeqTipoOrientacao = p.SeqTipoOrientacao,
                SeqNivelEnsino = p.SeqNivelEnsino,
                SeqEntidadeInstituicao = p.SeqEntidadeInstituicao,
                SeqTipoTermoIntercambio = p.SeqTipoTermoIntercambio,
                SeqTipoVinculoAluno = (long)p.SeqTipoVinculoAluno
            }).ToList();

            return retorno;
        }

        /// <summary>
        /// Preenche o modelo de orientação para realização da éfetivação da matrícula e renovação do aluno
        /// </summary>
        /// <param name="geraOrientacao">flag de orientação</param>
        /// <param name="seqTipoOrientacao">tipo da orientação</param>
        /// <param name="tipoParticipacaoOrientacao">tipo da participação da orientação</param>
        /// <param name="orientacaoVO">objeto VO com dados básicos da orientação</param>
        /// <returns>Modelo da orientação para gravar na tabela PlanoEstudoItem</returns>
        public Orientacao CriarOrientacaoPlanoEstudoItem(bool geraOrientacao, long seqTipoOrientacao, TipoParticipacaoOrientacao tipoParticipacaoOrientacao, OrientacaoVO orientacaoVO)
        {
            if (geraOrientacao)
            {
                return new Orientacao
                {
                    SeqEntidadeInstituicao = orientacaoVO.SeqEntidadeInstituicao,
                    SeqNivelEnsino = orientacaoVO.SeqNivelEnsino,
                    SeqTipoOrientacao = seqTipoOrientacao,
                    SeqTipoVinculoAluno = orientacaoVO.SeqTipoVinculoAluno,
                    OrientacoesPessoaAtuacao = orientacaoVO.OrientacoesPessoaAtuacao?.Select(s => new OrientacaoPessoaAtuacao { SeqPessoaAtuacao = s.SeqPessoaAtuacao }).ToList(),
                    OrientacoesColaborador = orientacaoVO.OrientacoesColaborador?.Where(oc => oc.TipoParticipacaoOrientacao == tipoParticipacaoOrientacao).Select(oc => new OrientacaoColaborador
                    {
                        DataFimOrientacao = oc.DataFimOrientacao,
                        DataInicioOrientacao = oc.DataInicioOrientacao,
                        SeqColaborador = oc.SeqColaborador,
                        SeqInstituicaoExterna = oc.SeqInstituicaoExterna,
                        TipoParticipacaoOrientacao = oc.TipoParticipacaoOrientacao,
                    })?.ToList(),
                };
            }
            return null;
        }

        public string ValidarVinculosAtivosOrientacoes(OrientacaoVO dadosOrientacaoVO)
        {
            string ret = string.Empty;
            /*
               2. Ao salvar, verificar se o período de orientação informado para todos os colaboradores existentes na orientação em questão estão compreendidos entre os períodos de vínculo
               destes colaboradores.
               Considerar neste caso os vínculos registrados conforme regra:*/

            /*Se houver algum período informado na orientação que não atenda a regra acima, emitir a mensagem de erro abaixo e abortar operação:
               "Inclusão/Alteração não permitida. Para o(s) Professor(es)/Pesquisador(es) listado(s) abaixo, não existe vínculo válido no período de orientação informado.
               Nome1
               Nome2
               ...
               NomeN"
               */

            // Recupera os dados do primeiro aluno para buscar o seqcursoofertalocalidade
            var dadosAlunoOrientacao = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosOrientacaoVO.OrientacoesPessoaAtuacao.FirstOrDefault().SeqPessoaAtuacao);
            var tokenNivelEnsino = NivelEnsinoDomainService.SearchProjectionByKey(dadosOrientacaoVO.SeqNivelEnsino, x => x.Token);

            foreach (var orientacao in dadosOrientacaoVO.OrientacoesColaborador)
            {
                /* Se o nível de ensino selecionado for “MESTRADO_ACADEMICO”, “MESTRADO_PROFISSIONAL”, “DOUTORADO_ACADEMICO” ou “DOUTORADO_PROFISSIONAL”, o período dos vínculos a serem verificados
               são os em entidades do tipo de entidade “GRUPO_PROGRAMA” e que tenham o tipo de atividade “Orientação” associado ao curso oferta localidade de todos os alunos selecionados na
               orientação em questão.*/

                if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO ||
                    tokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL ||
                    tokenNivelEnsino == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO ||
                    tokenNivelEnsino == TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL)
                {
                    var dadosColaborador = ColaboradorVinculoDomainService.SearchProjectionBySpecification(new ColaboradorVinculoFilterSpecification
                    {
                        SeqColaborador = orientacao.SeqColaborador,
                        SeqCursoOfertaLocalidade = dadosAlunoOrientacao.SeqCursoOfertaLocalidade,
                        TipoAtividade = TipoAtividadeColaborador.Orientacao,
                        TipoEntidadeToken = TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA
                    }, x => new PeriodoDatasVO
                    {
                        DataInicio = x.DataInicio,
                        DataFim = x.DataFim
                    }).ToList();

                    // Faz a validação de períodos consecutivos
                    ObterDatasConsecutivas(ref dadosColaborador);

                    if (dadosColaborador == null || !dadosColaborador.Any(d => d.DataInicio <= orientacao.DataInicioOrientacao && ((!d.DataFim.HasValue && !orientacao.DataFimOrientacao.HasValue) || !d.DataFim.HasValue || d.DataFim >= orientacao.DataFimOrientacao)))
                        ret += (ret != string.Empty ? "<br />" : string.Empty) + orientacao.ColaboradorNameDescriptionField;
                }
                /* Caso contrário, o período dos vínculos a serem verificados são os em entidades responsáveis por cursos  e que tenham o tipo de atividade “Orientação” associado ao curso oferta
                   localidade de todos os alunos selecionados na orientação em questão"
                */
                else
                {
                    var dadosColaborador = ColaboradorVinculoDomainService.SearchProjectionBySpecification(new ColaboradorVinculoFilterSpecification
                    {
                        SeqColaborador = orientacao.SeqColaborador,
                        SeqCursoOfertaLocalidade = dadosAlunoOrientacao.SeqCursoOfertaLocalidade,
                        TipoAtividade = TipoAtividadeColaborador.Orientacao,
                        TiposEntidadesTokens = new string[] { TOKEN_TIPO_ENTIDADE.DEPARTAMENTO, TOKEN_TIPO_ENTIDADE.PROGRAMA }
                    }, x => new PeriodoDatasVO
                    {
                        DataInicio = x.DataInicio,
                        DataFim = x.DataFim
                    }).ToList();

                    // Faz a validação de períodos consecutivos
                    ObterDatasConsecutivas(ref dadosColaborador);

                    if (dadosColaborador == null || !dadosColaborador.Any(d => d.DataInicio <= orientacao.DataInicioOrientacao && ((!orientacao.DataFimOrientacao.HasValue && !orientacao.DataFimOrientacao.HasValue) || !d.DataFim.HasValue || d.DataFim >= orientacao.DataFimOrientacao)))
                        ret += (ret != string.Empty ? "<br />" : string.Empty) + orientacao.ColaboradorNameDescriptionField;
                }
            }

            return ret;
        }

        /// <summary>
        /// Baseado na origem de avaliação, valida se os colaboradores informados devem ser utilizados para filtrar orientandos
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem avaliação da turma ou divisão em questão</param>
        /// <param name="seqsColaboradores">Sequenciais dos colaboradores que podem ser considerados para o filtro de orientados</param>
        /// <returns>Colaboradores informados caso deva filtrar os orientandos ou null caso deva recuperar todos alunos</returns>
        public List<long> ValidarOrientadores(long seqOrigemAvaliacao, params long[] seqsColaboradores)
        {
            var dadosOrientacao = OrigemAvaliacaoDomainService.BuscarDadosOrientacao(seqOrigemAvaliacao);

            return !seqsColaboradores.SMCAny(a => dadosOrientacao.seqsColaboradoresResposavelTurma.Contains(a))
                   && dadosOrientacao.orientacao
                   && dadosOrientacao.tipoOrigemAvaliacao != TipoOrigemAvaliacao.Turma
                   ? seqsColaboradores.ToList() : null;
        }

        //private void ObterDatasConsecutivas(List<(DateTime DataInicio, DateTime? DataFim)> dadosColaborador)
        private void ObterDatasConsecutivas(ref List<PeriodoDatasVO> dadosColaborador)
        {
            // Verifica se os períodos do colaborador são consecutivos. Caso sim, cria um período único
            if (dadosColaborador != null)
            {
                dadosColaborador = dadosColaborador.OrderBy(d => d.DataInicio).ToList();
                if (dadosColaborador.Count > 1)
                {
                    // Armazena o indice maximo para o for, pois vou adicionando itens novos na lista que não precisam ser validados
                    var indiceMaximo = dadosColaborador.Count;
                    for (int i = 0; i < indiceMaximo; i++)
                    {
                        var itemAtual = dadosColaborador[i];
                        DateTime dataInicio = itemAtual.DataInicio;
                        DateTime? dataFim = itemAtual.DataFim;
                        if (itemAtual.DataFim.HasValue)
                        {
                            var proximoIndice = i + 1;
                            var validaProximo = true;
                            bool periodoConsecutivo = false;
                            while (validaProximo && proximoIndice < indiceMaximo)
                            {
                                var proximoItem = dadosColaborador[proximoIndice];
                                // Caso as datas sejam iguais, ou a diferença seja menos de um dia, considera que são consecutivas.
                                var dateDiff = (proximoItem.DataInicio - dataFim);
                                if ((proximoItem.DataInicio == dataFim) || (dateDiff.Value.TotalMinutes >= 0 && dateDiff <= TimeSpan.FromDays(1)))
                                {
                                    // Datas consecutivas. Usa a data fim deste item
                                    dataFim = proximoItem.DataFim;

                                    // Atribui flags para continuar no while
                                    periodoConsecutivo = true;
                                    validaProximo = true;
                                    proximoIndice++;

                                    // Soma 1 ao i para não validar o próximo item, uma vez que já foi validado acima como consecutivo.
                                    i++;
                                }
                                else
                                {
                                    validaProximo = false;
                                }
                            }

                            // Caso tenha achado períodos consecutivos, adiciona na lista
                            if (periodoConsecutivo)
                                dadosColaborador.Add(new PeriodoDatasVO { DataInicio = dataInicio, DataFim = dataFim });
                        }
                    }
                }
            }
        }

        public List<OrientadoresRelatorioVO> BuscarOrientacoesRelatorio(OrientacaoFilterSpecification filtro)
        {
            // ajustar parametros do filtro para adicionar valores nulos ou default
            string pessoaAtuacao = filtro.SeqPessoaAtuacao == null ? "null" : filtro.SeqPessoaAtuacao.Value.ToString();
            string exebieFinalizadas = filtro.ExibirOrientacoesFinalizadas == true ? "1" :"0";

            var query = string.Format(QUERY_RELATORIO_ORIENTACAO,
                $"'{string.Join(",", filtro.SeqsEntidadesResponsaveisHierarquiaItem)}'",
                pessoaAtuacao,
                filtro.SeqCicloLetivo,
                $"'{string.Join(",", filtro.SeqsTiposSituacoesMatriculas)}'",
                exebieFinalizadas);

            // Consulta os componentes dos alunos selecionados
            var retorno = this.RawQuery<OrientadoresRelatorioVO>(query);

            // O retorno da rawQuery vem o datetime com horas, essa função converte a data
            retorno = retorno.ConvertAll(x =>
            {
                x.InicioOrientacao = x.InicioOrientacao != null ? DateTime.Parse(x.InicioOrientacao).ToString("dd/MM/yyyy") : "";
                x.FimOrientacao = x.FimOrientacao != null ? DateTime.Parse(x.FimOrientacao).ToString("dd/MM/yyyy") : "";
                return x;
            });

            return retorno;
        }
    }
}