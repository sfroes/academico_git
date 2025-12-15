using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.DCT.Exceptions;
using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Areas.TUR.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Resources;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.IntegracaoAcademico.ServiceContract.Areas.ACA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.TUR.DomainServices
{
    public class DivisaoTurmaDomainService : AcademicoContextDomain<DivisaoTurma>
    {
        #region [ Services ]

        private IAcademicoService AcademicoService => Create<IAcademicoService>();
        private ITabelaHorarioService TabelaHorarioService => Create<ITabelaHorarioService>();

        #endregion Services

        #region [ DomainService ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();
        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();
        private ColaboradorVinculoDomainService ColaboradorVinculoDomainService => Create<ColaboradorVinculoDomainService>();
        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();
        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();
        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();
        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();
        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();
        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();
        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();
        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService => Create<ProcessoSeletivoOfertaDomainService>();
        private RestricaoTurmaMatrizDomainService RestricaoTurmaMatrizDomainService => Create<RestricaoTurmaMatrizDomainService>();
        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();

        #endregion [ DomainService ]

        #region [ Queries ]

        //private string _buscarAlunosDivisaoTurmaQuery = @"
        //                    declare @SEQ_DIVISAO_TURMA bigint
        //                    set @SEQ_DIVISAO_TURMA = {0}

        //                    select
        //                     dt.seq_divisao_turma as SeqDivisaoTurma,
        //                     a.num_registro_academico as NumeroRegistroAcademico,
        //                     isnull(pdp.nom_social, pdp.nom_pessoa) as NomeAluno
        //                    from	TUR.divisao_turma dt
        //                    join	ALN.plano_estudo_item pei
        //                      on dt.seq_divisao_turma = pei.seq_divisao_turma
        //                    join	ALN.plano_estudo pe
        //                      on pei.seq_plano_estudo = pe.seq_plano_estudo
        //                      and pe.ind_atual = 1
        //                    join	ALN.aluno_historico_ciclo_letivo ahcl
        //                      on pe.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
        //                      and ahcl.dat_exclusao is null
        //                    join	ALN.aluno_historico ah
        //                      on ahcl.seq_aluno_historico = ah.seq_aluno_historico
        //                      and ah.ind_atual = 1
        //                    join	ALN.aluno a
        //                      on ah.seq_atuacao_aluno = a.seq_pessoa_atuacao
        //                    join	PES.pessoa_atuacao pa
        //                      on a.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
        //                    join	PES.pessoa_dados_pessoais pdp
        //                      on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
        //                    join	ALN.aluno_historico_situacao hs
        //                      on ahcl.seq_aluno_historico_ciclo_letivo = hs.seq_aluno_historico_ciclo_letivo
        //                      and hs.dat_exclusao is null
        //                      and hs.dat_inicio_situacao = (	select	MAX(dat_inicio_situacao)
        //		                            from	ALN.aluno_historico_situacao
        //		                            where	seq_aluno_historico_ciclo_letivo = hs.seq_aluno_historico_ciclo_letivo
        //		                            and		dat_exclusao is null)
        //                    join	MAT.situacao_matricula sm
        //                      on hs.seq_situacao_matricula = sm.seq_situacao_matricula
        //                      and	sm.dsc_token in ('MATRICULADO', 'PROVAVEL_FORMANDO')
        //                    where	dt.seq_divisao_turma = @SEQ_DIVISAO_TURMA
        //                    order by ISNULL(pdp.nom_social, pdp.nom_pessoa)";

        private string _buscarAlunosDivisaoTurmaQuery = @"
                            declare @SEQ_DIVISAO_TURMA bigint
                            set @SEQ_DIVISAO_TURMA = {0}

                            select
	                            dt.seq_divisao_turma as SeqDivisaoTurma,
                                a.seq_pessoa_atuacao as SeqPessoaAtuacao,                                
	                            a.num_registro_academico as NumeroRegistroAcademico,
				                case
					                when pdp.nom_social is not null then rtrim(pdp.nom_social) + ' (' + rtrim(pdp.nom_pessoa) + ')'
					                else rtrim(pdp.nom_pessoa)
				                end as NomeAluno
                            from	TUR.divisao_turma dt
                            join	ALN.plano_estudo_item pei
		                            on dt.seq_divisao_turma = pei.seq_divisao_turma
                            join	ALN.plano_estudo pe
		                            on pei.seq_plano_estudo = pe.seq_plano_estudo
		                            and pe.ind_atual = 1
                            join	ALN.aluno_historico_ciclo_letivo ahcl
		                            on pe.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
		                            and ahcl.dat_exclusao is null
                            join	ALN.aluno_historico ah
		                            on ahcl.seq_aluno_historico = ah.seq_aluno_historico
		                            and ah.ind_atual = 1
                            join	ALN.aluno a
		                            on ah.seq_atuacao_aluno = a.seq_pessoa_atuacao
                            join	PES.pessoa_atuacao pa
		                            on a.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                            join	PES.pessoa_dados_pessoais pdp
		                            on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
                            join	ALN.aluno_historico_situacao hs
		                            on ahcl.seq_aluno_historico_ciclo_letivo = hs.seq_aluno_historico_ciclo_letivo
		                            and hs.dat_exclusao is null
		                            and hs.dat_inicio_situacao = (	select	MAX(dat_inicio_situacao)
										                            from	ALN.aluno_historico_situacao
										                            where	seq_aluno_historico_ciclo_letivo = hs.seq_aluno_historico_ciclo_letivo
										                            and		dat_exclusao is null)
                            join	MAT.situacao_matricula sm
		                            on hs.seq_situacao_matricula = sm.seq_situacao_matricula
		                            and	sm.dsc_token in ('MATRICULADO', 'MATRICULADO_MOBILIDADE', 'PROVAVEL_FORMANDO')
                            where	dt.seq_divisao_turma = @SEQ_DIVISAO_TURMA
                            order by ISNULL(pdp.nom_social, pdp.nom_pessoa)";

        private string _buscarDivisaoTurmaRelatorioCabecalhoQuery = @"
                            declare @SEQ_DIVISAO_TURMA bigint
                            set @SEQ_DIVISAO_TURMA = {0}

                            select
	                            dt.seq_divisao_turma as SeqDivisaoTurma,
	                            ecol.nom_entidade as NomeCursoOfertaLocalidade,
	                            tu.dsc_turno as DescricaoTurno,
	                            cco.dsc_configuracao_componente as DescricaoConfiguracaoComponente,
	                            cc_assunto.dsc_componente_curricular as DescricaoComponenteCurricularAssunto, 
	                            cc.qtd_carga_horaria as CargaHorariaComponenteCurricular,
	                            cc.qtd_credito as CreditoComponenteCurricular,
	                            cast(t.cod_turma as varchar) + '.' +
	                            cast(t.num_turma as varchar) + '.' +
	                            cast(dc.num_divisao_componente as varchar) + '.' +
	                            REPLICATE('0', 3 - LEN(cast(dt.num_grupo as varchar))) + cast(dt.num_grupo as varchar) as CodigoDivisaoFormatado,
	                            cc_organizacao.dsc_componente_curricular_organizacao as DescricaoComponenteCurricularOrganizacao,
	                            tdc.dsc_tipo_divisao_componente as TipoDivisaoDescricao,
	                            dc.qtd_carga_horaria as CargaHoraria, 
	                            intcc.idt_dom_formato_carga_horaria as FormatoCargaHoraria
                            from	TUR.divisao_turma dt
                            join	TUR.turma t
		                            on dt.seq_turma = t.seq_turma
                            join	TUR.turma_configuracao_componente tcc
		                            on t.seq_turma = tcc.seq_turma
                            join    CUR.configuracao_componente cco
		                            on cco.seq_configuracao_componente = tcc.seq_configuracao_componente
                            join    CUR.componente_curricular cc
		                            on cc.seq_componente_curricular = cco.seq_componente_curricular
                            left join CUR.componente_curricular_nivel_ensino ccne
		                            on ccne.seq_componente_curricular = cc.seq_componente_curricular and ccne.ind_responsavel = 1
                            left join ORG.instituicao_nivel inn
		                            on inn.seq_nivel_ensino = ccne.seq_nivel_ensino
                            left join CUR.instituicao_nivel_tipo_componente_curricular intcc
		                            on intcc.seq_tipo_componente_curricular = cc.seq_tipo_componente_curricular and intcc.seq_instituicao_nivel = inn.seq_instituicao_nivel
                            join	CUR.divisao_componente dc
		                            on dt.seq_divisao_componente = dc.seq_divisao_componente
                            left join CUR.componente_curricular_organizacao cc_organizacao 
		                            on cc_organizacao.seq_componente_curricular_organizacao = dc.seq_componente_curricular_organizacao
                            join    CUR.tipo_divisao_componente tdc 
		                            on tdc.seq_tipo_divisao_componente = dc.seq_tipo_divisao_componente
                            join	TUR.restricao_turma_matriz rtm
		                            on tcc.seq_turma_configuracao_componente = rtm.seq_turma_configuracao_componente
                            left join CUR.componente_curricular cc_assunto
		                            on cc_assunto.seq_componente_curricular = rtm.seq_componente_curricular_assunto
                            join	CSO.curso_oferta_localidade_turno colt
		                            on rtm.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
                            join	ORG.entidade ecol
		                            on colt.seq_entidade_curso_oferta_localidade = ecol.seq_entidade
                            join	CSO.turno tu
		                            on colt.seq_turno = tu.seq_turno
                            where	dt.seq_divisao_turma = @SEQ_DIVISAO_TURMA";

        private string _buscarDivisaoTurmaColaboradorQuery = @"
                            declare
	                            @SEQ_DIVISAO_TURMA bigint = {0},
	                            @LISTA_PROFESSORES varchar(255) = {1}
                            select
                                pa.seq_pessoa_atuacao as SeqPessoaAtuacaoColaborador,
	                            isnull(pdp.nom_social, pdp.nom_pessoa) as NomeColaborador
                            from	TUR.divisao_turma_colaborador dtc
                            join	DCT.colaborador c
		                            on dtc.seq_atuacao_colaborador = c.seq_pessoa_atuacao
                            join	PES.pessoa_atuacao pa
		                            on c.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                            join	PES.pessoa_dados_pessoais pdp
		                            on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
                            where	dtc.seq_divisao_turma = @SEQ_DIVISAO_TURMA
                            and	(	@LISTA_PROFESSORES is null
                            or		pa.seq_pessoa_atuacao in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_PROFESSORES, ',')))
                            order by isnull(pdp.nom_social, pdp.nom_pessoa)";

        #endregion [ Queries ]

        /// <summary>
        /// Buscar a divisão turma com detalhes para cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial da divisão turma</param>
        /// <returns>Objeto divisão turma com detalhes para o cabeçalho</returns>
        public DivisaoTurmaDetalhesVO BuscarDivisaoTurmaCabecalho(long seq, bool telaDetalhes = false, bool quebraLinha = true)
        {
            var divisaoTurma = this.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seq),
                                                   p => new DivisaoTurmaDetalhesVO()
                                                   {
                                                       Seq = p.Seq,
                                                       TurmaCodigo = p.Turma.Codigo,
                                                       TurmaNumero = p.Turma.Numero,
                                                       SeqTurma = p.Turma.Seq,
                                                       DescricaoTurmaConfiguracaoComponente = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().Descricao,
                                                       DescricaoConfiguracaoComponente = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.Descricao,
                                                       SeqNivelEnsino = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino,
                                                       SeqTipoComponenteCurricular = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                                                       SeqComponenteCurricular = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.SeqComponenteCurricular,
                                                       TipoDivisaoDescricao = p.DivisaoComponente.TipoDivisaoComponente.Descricao,
                                                       Numero = p.DivisaoComponente.Numero,
                                                       CargaHoraria = p.DivisaoComponente.CargaHoraria,
                                                       Credito = p.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito,
                                                       CargaHorariaComponenteCurricular = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                                                       CreditoComponenteCurricular = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.Credito,
                                                       NumeroGrupo = p.NumeroGrupo,
                                                       DescricaoLocalidade = p.Localidade.Nome,
                                                       InformacoesAdicionais = p.InformacoesAdicionais,
                                                       QuantidadeVagas = p.QuantidadeVagas,
                                                       DescricaoComponenteCurricularOrganizacao = p.DivisaoComponente.ComponenteCurricularOrganizacao.Descricao,
                                                       DescricaoComponenteCurricularAssunto = p.Turma.ConfiguracoesComponente.FirstOrDefault().RestricoesTurmaMatriz.FirstOrDefault().ComponenteCurricularAssunto.Descricao
                                                   });

            divisaoTurma.FormatoCargaHoraria = ConfiguracaoComponenteDomainService.BuscarFormatoCargaHoraria(divisaoTurma.SeqNivelEnsino, divisaoTurma.SeqTipoComponenteCurricular);

            MontarTurmaDescricaoFormatado(ref divisaoTurma, telaDetalhes, quebraLinha);

            return divisaoTurma;
        }

        public void MontarTurmaDescricaoFormatado(ref DivisaoTurmaDetalhesVO divisaoTurma, bool telaDetalhes, bool quebraLinha = true)
        {
            if (!telaDetalhes)
            {
                divisaoTurma.TurmaDescricaoFormatado = $"{divisaoTurma.DescricaoConfiguracaoComponente}";

                if (!string.IsNullOrEmpty(divisaoTurma.DescricaoComponenteCurricularAssunto))
                {
                    divisaoTurma.TurmaDescricaoFormatado += $": {divisaoTurma.DescricaoComponenteCurricularAssunto}";
                }

                if (divisaoTurma.CargaHorariaComponenteCurricular.GetValueOrDefault() > 0)
                {
                    string labelHoraAula = null;

                    if (divisaoTurma.FormatoCargaHoraria == FormatoCargaHoraria.Hora)
                    {
                        labelHoraAula = divisaoTurma.CargaHorariaComponenteCurricular == 1 ? MessagesResource.Label_Hora : MessagesResource.Label_Horas;
                    }
                    else
                    {
                        labelHoraAula = divisaoTurma.CargaHorariaComponenteCurricular == 1 ? MessagesResource.Label_HoraAula : MessagesResource.Label_HorasAula;
                    }

                    divisaoTurma.TurmaDescricaoFormatado += $" - {divisaoTurma.CargaHorariaComponenteCurricular} {labelHoraAula}";
                }

                if (divisaoTurma.CreditoComponenteCurricular.GetValueOrDefault() > 0)
                {
                    string labelCredito = divisaoTurma.CreditoComponenteCurricular == 1 ? MessagesResource.Label_Credito : MessagesResource.Label_Creditos;

                    divisaoTurma.TurmaDescricaoFormatado += $" - {divisaoTurma.CreditoComponenteCurricular} {labelCredito}";
                }

                if (quebraLinha)
                {
                    divisaoTurma.TurmaDescricaoFormatado += "<br />";
                    divisaoTurma.TurmaDescricaoFormatado += $"{divisaoTurma.TurmaCodigo}.{divisaoTurma.TurmaNumero}.{divisaoTurma.Numero}.{divisaoTurma.NumeroGrupo:d3}";

                    if (!string.IsNullOrEmpty(divisaoTurma.DescricaoComponenteCurricularOrganizacao))
                    {
                        divisaoTurma.TurmaDescricaoFormatado += $" - {divisaoTurma.DescricaoComponenteCurricularOrganizacao}";
                    }

                    divisaoTurma.TurmaDescricaoFormatado += $" - {divisaoTurma.TipoDivisaoDescricao}";

                    if (divisaoTurma.CargaHoraria.GetValueOrDefault() > 0)
                    {
                        string labelHoraAula = null;

                        if (divisaoTurma.FormatoCargaHoraria == FormatoCargaHoraria.Hora)
                        {
                            labelHoraAula = divisaoTurma.CargaHoraria == 1 ? MessagesResource.Label_Hora : MessagesResource.Label_Horas;
                        }
                        else
                        {
                            labelHoraAula = divisaoTurma.CargaHoraria == 1 ? MessagesResource.Label_HoraAula : MessagesResource.Label_HorasAula;
                        }

                        divisaoTurma.TurmaDescricaoFormatado += $" - {divisaoTurma.CargaHoraria} {labelHoraAula}";
                    }
                }

                //if (divisaoTurma.Credito.GetValueOrDefault() > 0)
                //{
                //    string labelCredito = divisaoTurma.Credito == 1 ? MessagesResource.Label_Credito : MessagesResource.Label_Creditos;

                //    divisaoTurma.TurmaDescricaoFormatado += $" - {divisaoTurma.Credito} {labelCredito}";
                //}
            }
            else
            {
                divisaoTurma.TurmaDescricaoFormatado = $"{divisaoTurma.TurmaCodigo}.{divisaoTurma.TurmaNumero} - {divisaoTurma.DescricaoTurmaConfiguracaoComponente}";
            }
        }

        /// <summary>
        /// Buscar o tipo de organização da divisão turma para associar professor
        /// </summary>
        /// <param name="seq">Sequencial da divisao turma</param>
        /// <returns>Modelo com o tipo organização definido</returns>
        public DivisaoTurmaDetalhesVO BuscarTipoComponenteDivisaoTurma(long seq)
        {
            var divisaoTurma = this.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seq),
                                                   p => new DivisaoTurmaDetalhesVO()
                                                   {
                                                       Seq = p.Seq,
                                                       TipoOrganizacao = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.TipoOrganizacao
                                                   });
            return divisaoTurma;
        }

        /// <summary>
        /// Buscar as divisões de turma que compartilham a mesma divisão de componente para tela de visualização
        /// </summary>
        /// <param name="seq">Sequencial da divisão turma</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="preservarColaboradores">Quando true, mesmo que não seja infromada uma pessoa atuação e a turma seja de orientação os colaboradores serão retornados</param>
        /// <returns>Objeto divisão turma com detalhes e colaboradores</returns>
        public List<DivisaoTurmaDetalhesVO> BuscarDivisaoTurmaDetalhes(long seq, long? seqPessoaAtuacao = null, bool preservarColaboradores = false, bool telaDetalhes = false)
        {
            this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            MatrizCurricularOfertaDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            PlanoEstudoItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            PessoaAtuacaoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            long seqMatrizCurricularOferta = 0;
            string descricaoCursoOfertaLocalidade = null;
            string descricaoTurno = null;
            if (seqPessoaAtuacao.HasValue && seqPessoaAtuacao.Value > 0)
            {
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao.GetValueOrDefault());
                seqMatrizCurricularOferta = dadosOrigem.SeqMatrizCurricularOferta;

                if (seqMatrizCurricularOferta != 0)
                {
                    // Recupera a descrição do curso oferta localidade turno
                    var dadosDescricao = MatrizCurricularOfertaDomainService.SearchProjectionByKey(seqMatrizCurricularOferta, x => new
                    {
                        DescricaoCursoOfertaLocalidade = x.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                        DescricaoTurno = x.CursoOfertaLocalidadeTurno.Turno.Descricao,
                    });
                    descricaoCursoOfertaLocalidade = dadosDescricao.DescricaoCursoOfertaLocalidade;
                    descricaoTurno = dadosDescricao.DescricaoTurno;
                }
            }

            // Recupera qual é o divisao componente da turma
            var dadosDivisao = this.SearchProjectionByKey(seq, x => new
            {
                x.SeqDivisaoComponente,
                x.SeqTurma
            });

            // Nova regra NV02/03
            // Recupera todas as divisões de turma que possuem o mesmo divisão de componente da turma enviada como parâmetro

            var divisoesTurma = this.SearchProjectionBySpecification(new DivisaoTurmaFilterSpecification { SeqDivisaoComponente = dadosDivisao.SeqDivisaoComponente, SeqTurma = dadosDivisao.SeqTurma }, p => new
            {
                Seq = p.Seq,
                TurmaCodigo = p.Turma.Codigo,
                TurmaNumero = p.Turma.Numero,
                SeqTurma = p.Turma.Seq,
                SeqOrigemAvaliacao = p.SeqOrigemAvaliacao,
                GeraOrientacao = p.DivisaoComponente.TipoDivisaoComponente.GeraOrientacao,
                DescricaoConfiguracaoComponente = p.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == seqMatrizCurricularOferta)).Descricao,
                DescricaoConfiguracaoComponentePrincipal = p.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).Descricao,
                TipoDivisaoDescricao = p.DivisaoComponente.TipoDivisaoComponente.Descricao,
                Numero = p.DivisaoComponente.Numero,
                CargaHoraria = p.DivisaoComponente.CargaHoraria,
                DescricaoComponenteCurricularOrganizacao = p.DivisaoComponente.ComponenteCurricularOrganizacao.Descricao,
                SeqInstituicaoEnsino = (long?)p.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.ComponenteCurricular.SeqInstituicaoEnsino,
                SeqNivelEnsino = (long?)p.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == seqMatrizCurricularOferta)).ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.FirstOrDefault(w => w.Responsavel).SeqNivelEnsino,
                SeqNivelEnsinoPrincipal = (long?)p.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.FirstOrDefault(w => w.Responsavel).SeqNivelEnsino,
                SeqTipoComponenteCurricular = (long?)p.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                SeqComponenteCurricular = (long?)p.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.SeqComponenteCurricular,
                DescricaoCursoOfertaLocalidadePrincipal = p.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                DescricoesCursoOfertaLocalidadeTurno = p.Turma.ConfiguracoesComponente.SelectMany(w => w.RestricoesTurmaMatriz.Select(r => r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome + " - " + r.CursoOfertaLocalidadeTurno.Turno.Descricao)).Distinct().OrderBy(d => d),
                DescricaoTurnoPrincipal = p.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).CursoOfertaLocalidadeTurno.Turno.Descricao,
                NumeroGrupo = p.NumeroGrupo,
                DescricaoLocalidade = p.Localidade.Nome,
                InformacoesAdicionais = p.InformacoesAdicionais,
                Colaboradores = p.Colaboradores.OrderBy(c => c.Colaborador.DadosPessoais.Nome).Select(s => new DivisaoTurmaDetalhesColaboradorVO()
                {
                    SeqColaborador = s.Colaborador.Seq,
                    Nome = s.Colaborador.DadosPessoais.Nome,
                    NomeSocial = s.Colaborador.DadosPessoais.NomeSocial,
                }).ToList(),
                DataInicioPeriodoLetivo = p.Turma.DataInicioPeriodoLetivo,
                DataFimPeriodoLetivo = p.Turma.DataFimPeriodoLetivo,
            });

            List<DivisaoTurmaDetalhesVO> ret = new List<DivisaoTurmaDetalhesVO>();

            divisoesTurma.SMCForEach(divisaoTurmaDados =>
            {
                // Faz a conversão do objeto considerando os dados da configuração da matriz ou a principal
                DivisaoTurmaDetalhesVO divisaoTurma = SMCMapperHelper.Create<DivisaoTurmaDetalhesVO>(divisaoTurmaDados);
                divisaoTurma.DescricaoConfiguracaoComponente = divisaoTurma.DescricaoConfiguracaoComponente ?? divisaoTurmaDados.DescricaoConfiguracaoComponentePrincipal;
                divisaoTurma.TurmaDescricaoFormatado = this.ObterDescricaoDivisaoTurma(divisaoTurmaDados.Seq, seqPessoaAtuacao ?? 0, telaDetalhes);
                divisaoTurma.SeqNivelEnsino = divisaoTurmaDados.SeqNivelEnsino ?? divisaoTurmaDados.SeqNivelEnsinoPrincipal ?? 0;
                divisaoTurma.NomeCursoOfertaLocalidade = descricaoCursoOfertaLocalidade ?? divisaoTurmaDados.DescricaoCursoOfertaLocalidadePrincipal;
                divisaoTurma.DescricaoTurno = descricaoTurno ?? divisaoTurmaDados.DescricaoTurnoPrincipal;
                divisaoTurma.FormatoCargaHoraria = ConfiguracaoComponenteDomainService.BuscarFormatoCargaHoraria(divisaoTurma.SeqNivelEnsino, divisaoTurma.SeqTipoComponenteCurricular);
                divisaoTurma.DataInicioPeriodoLetivo = divisaoTurmaDados.DataInicioPeriodoLetivo;
                divisaoTurma.DataFimPeriodoLetivo = divisaoTurmaDados.DataFimPeriodoLetivo;
                divisaoTurma.SeqOrigemAvaliacao = divisaoTurmaDados.SeqOrigemAvaliacao;

                // Marca esta como destacada para exibição no grid
                divisaoTurma.Destaque = divisaoTurma.Seq == seq;

                ///Se o tipo da divisão da configuração da divisão da turma em questão estiver marcada para gerar orientação, verificar se a pessoa - atuação foi passada como parâmetro.
                if (divisaoTurma.GeraOrientacao)
                {
                    if (seqPessoaAtuacao.HasValue && seqPessoaAtuacao.Value > 0)
                    {
                        var tipoAtuacao = this.PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao.Value), x => x.TipoAtuacao);

                        ///Se o tipo for "Ingressante", não exibir o agrupamento "Colaborador"
                        if (tipoAtuacao == TipoAtuacao.Ingressante)
                        {
                            divisaoTurma.Colaboradores = null;
                        }
                        else if (tipoAtuacao == TipoAtuacao.Aluno)
                        {
                            var alunoTemOrientacao = this.PlanoEstudoItemDomainService.SearchProjectionBySpecification(new PlanoEstudoItemFilterSpecification() { SeqAluno = seqPessoaAtuacao, SeqDivisaoTurma = seq },
                                                                                                                       p => p.SeqOrientacao).FirstOrDefault().HasValue;

                            ///Caso não exista orientação ou não exista item do plano de estudos para a divisão, não exibir colaborador.
                            if (alunoTemOrientacao)
                            {
                                var planoEstudoItem = this.PlanoEstudoItemDomainService.BuscarAlunosPlanoEstudoItemPorDivisaoTurma(new PlanoEstudoItemFilterSpecification()
                                {
                                    SeqDivisaoTurma = seq,
                                    SeqAluno = seqPessoaAtuacao,
                                    PlanoEstudoAtual = true
                                }).FirstOrDefault();

                                if (planoEstudoItem != null)
                                {
                                    var seqsColaboradores = planoEstudoItem.Orientacao.OrientacoesColaborador.Select(s => s.SeqColaborador);

                                    divisaoTurma.Colaboradores = divisaoTurma.Colaboradores.Where(w => seqsColaboradores.Contains(w.SeqColaborador)).ToList();
                                }
                                else
                                {
                                    divisaoTurma.Colaboradores = null;
                                }
                            }
                            else
                            {
                                divisaoTurma.Colaboradores = null;
                            }
                        }
                    }
                    else if (!preservarColaboradores)
                    {
                        ///Se não for passado parâmetro, não exibir o o agrupamento "Colaborador".
                        divisaoTurma.Colaboradores = null;
                    }
                }
                ret.Add(divisaoTurma);
            });

            this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            MatrizCurricularOfertaDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            PlanoEstudoItemDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            PessoaAtuacaoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            ///Se não gerar orientação, retornar todos os colaboradores associados à divisão da turma em questão.
            return ret.OrderBy(o => o.GrupoFormatado).ToList();
        }

        /// <summary>
        /// Buscar a divisão turma com detalhes e colaboradores para tela de visualização
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Lista divisão turma com detalhes e colaboradores</returns>
        public List<DivisaoTurmaDetalhesVO> BuscarDivisaoTurmaLista(long seqTurma)
        {
            var filter = new DivisaoTurmaFilterSpecification() { SeqTurma = seqTurma };
            filter.MaxResults = int.MaxValue;

            filter.SetOrderBy(o => o.Turma.Codigo);
            filter.SetOrderBy(o => o.Turma.Numero);
            filter.SetOrderBy(o => o.DivisaoComponente.Numero);
            filter.SetOrderBy(o => o.NumeroGrupo);
            var divisaoTurma = this.SearchProjectionBySpecification(filter,
                                                   p => new DivisaoTurmaDetalhesVO()
                                                   {
                                                       Seq = p.Seq,
                                                       SeqOrigemAvaliacao = p.SeqOrigemAvaliacao,
                                                       TurmaCodigo = p.Turma.Codigo,
                                                       TurmaNumero = p.Turma.Numero,
                                                       SeqTurma = p.Turma.Seq,
                                                       DescricaoConfiguracaoComponente = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().Descricao,
                                                       SeqNivelEnsino = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino,
                                                       SeqTipoComponenteCurricular = p.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                                                       TipoDivisaoDescricao = p.DivisaoComponente.TipoDivisaoComponente.Descricao,
                                                       Numero = p.DivisaoComponente.Numero,
                                                       CargaHoraria = p.DivisaoComponente.CargaHoraria,
                                                       CargaHorariaGrade = p.DivisaoComponente.CargaHorariaGrade,
                                                       NumeroGrupo = p.NumeroGrupo,
                                                       DescricaoLocalidade = p.Localidade.Nome,
                                                       InformacoesAdicionais = p.InformacoesAdicionais,
                                                       QuantidadeVagas = p.QuantidadeVagas,
                                                       GeraOrientacao = p.DivisaoComponente.TipoDivisaoComponente.GeraOrientacao,
                                                       TurmaPossuiAgenda = p.Turma.SeqAgendaTurma.HasValue,
                                                       DescricaoComponenteCurricularOrganizacao = p.DivisaoComponente.ComponenteCurricularOrganizacao.Descricao,
                                                       DiarioFechado = p.Turma.HistoricosFechamentoDiario.Count > 0 ? p.Turma.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
                                                       Colaboradores = p.Colaboradores.Select(s => new DivisaoTurmaDetalhesColaboradorVO()
                                                       {
                                                           Seq = s.Seq, //Associacao Divisao Colaborador
                                                           SeqColaborador = s.SeqColaborador,
                                                           Nome = s.Colaborador.DadosPessoais.Nome,
                                                           NomeSocial = s.Colaborador.DadosPessoais.NomeSocial,
                                                           QuantidadeCargaHoraria = s.QuantidadeCargaHoraria,
                                                       }).ToList()
                                                   }).ToList();

            if (divisaoTurma != null && divisaoTurma.Count > 0)
            {
                var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(divisaoTurma.First().SeqNivelEnsino, divisaoTurma.First().SeqTipoComponenteCurricular);
                if (tiposComponenteNivel != null)
                    divisaoTurma.ForEach(f => f.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria);
            }
            return divisaoTurma;
        }

        /// <summary>
        /// Buscar a divisão turma para verificação de quantidades
        /// </summary>
        /// <param name="seq">Sequencial da divisão turma</param>
        /// <returns>Objeto divisão turma com as quantidades</returns>
        public DivisaoTurmaVO BuscarDivisaoTurmaQuantidades(long seq)
        {
            var divisaoTurma = this.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seq),
                                                   p => new DivisaoTurmaVO()
                                                   {
                                                       Seq = p.Seq,
                                                       QuantidadeVagas = p.QuantidadeVagas,
                                                       QuantidadeVagasOcupadas = p.QuantidadeVagasOcupadas,
                                                       QuantidadeVagasReservadas = p.QuantidadeVagasReservadas
                                                   });

            return divisaoTurma;
        }

        /// <summary>
        /// Atualizar os dados quantidade de vagas ocupadas
        /// </summary>
        /// <param name="seq">Sequencial da divisão turma</param>
        /// <param name="quantidadeVagasOcupadas">Quantidade de vagas ocupadas</param>
        public void AtualizarQuantidadeVagasOcupadas(long seq, int quantidadeVagasOcupadas)
        {
            var divisaoTurma = new DivisaoTurma()
            {
                Seq = seq,
                QuantidadeVagasOcupadas = (short)quantidadeVagasOcupadas
            };

            UpdateFields(divisaoTurma, f => f.QuantidadeVagasOcupadas);
        }

        /// <summary>
        /// Lista dos alunos de uma divisão de turma.
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão de turma.</param>
        /// <returns>Lista de alunos desta divisão de turma</returns>
        public List<Aluno> ListarAlunosPorDivisaoTurma(long seqDivisaoTurma)
        {
            List<Aluno> listaAluno = new List<Aluno>();
            AlunoHistoricoFilterSpecification ahfs = new AlunoHistoricoFilterSpecification() { Atual = true, SeqDivisaoTurma = seqDivisaoTurma, PlanoEstudoAtual = true };

            var listaAH = AlunoHistoricoDomainService.SearchBySpecification(ahfs, a => a.Aluno).ToList();

            foreach (var alunoHistorico in listaAH)
            {
                listaAluno.Add(alunoHistorico.Aluno);
            }

            return listaAluno;
        }

        /// <summary>
        /// Lista de alunos por uma divisão de turma. Modelo de VO para relatório de Emissão de Lista de Frequencia
        /// </summary>
        public virtual List<DivisaoTurmaRelatorioAlunoVO> BuscarAlunosDivisaoTurma(long seqDivisaoTurma)
        {
            return this.RawQuery<DivisaoTurmaRelatorioAlunoVO>(string.Format(_buscarAlunosDivisaoTurmaQuery, seqDivisaoTurma));
        }

        /// <summary>
        /// Modelo Cabeçalho de VO para relatório de Emissão de Lista de Frequencia
        /// </summary>
        public List<DivisaoTurmaRelatorioCabecalhoVO> BuscarDivisaoTurmaRelatorioCabecalho(long seqDivisaoTurma)
        {
            return this.RawQuery<DivisaoTurmaRelatorioCabecalhoVO>(string.Format(_buscarDivisaoTurmaRelatorioCabecalhoQuery, seqDivisaoTurma));
        }

        /// <summary>
        /// Busca lista de colaboradores. Modelo de VO para relatório de Emissão de Lista de Frequencia
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão de turma</param>
        /// <param name="colaboradores">Lista de colaboradores selecionados (opcional)</param>
        /// <returns>Lista de colaboradores da turma</returns>
        public List<DivisaoTurmaRelatorioColaboradorVO> BuscarDivisaoTurmaRelatorioColaborador(long seqDivisaoTurma, List<long> colaboradores, DateTime? dataVinculo)
        {
            string paramColab = colaboradores.SMCCount() > 0 ? $"'{string.Join(",", colaboradores)}'" : "null";
            string query = string.Format(_buscarDivisaoTurmaColaboradorQuery, seqDivisaoTurma, paramColab);
            var colaboradoresRet = RawQuery<DivisaoTurmaRelatorioColaboradorVO>(query);

            // Percorre os colaboradores e verifica se possuem vínculo ativo com a instituição na data informada
            if (dataVinculo.HasValue)
            {
                // Recupera a entidade
                var seqsCursoOfertaLocalidade = this.SearchProjectionByKey(seqDivisaoTurma, x => x.Turma.ConfiguracoesComponente.SelectMany(c => c.RestricoesTurmaMatriz.Select(r => r.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade))).ToArray();

                List<string> colaboradoresSemVinculo = new List<string>();
                foreach (var item in colaboradoresRet)
                {
                    var vinculoAtivo = ColaboradorVinculoDomainService.VerificarVinculosAtivosColaborador(item.SeqPessoaAtuacaoColaborador, seqsCursoOfertaLocalidade, dataVinculo.Value);
                    if (!vinculoAtivo)
                        colaboradoresSemVinculo.Add(item.NomeSocialColaborador ?? item.NomeColaborador);
                }

                if (colaboradoresSemVinculo.Any())
                    throw new ColaboradorSemVinculoAtivoException(colaboradoresSemVinculo);
            }

            return colaboradoresRet;
        }

        /// <summary>
        /// Verifica se existe alguma solicitação de matricula ou algum plano de estudo atual para esta divisão de turma
        /// </summary>
        /// <param name="seq">Sequencial da divisão de turma</param>
        /// <param name="apenasAtivaEAtual">Verifica se deverá pesquisar apenas por solicitação ativa e apenas plano de estudo atual,
        /// valor false: qualquer Solicitação ou plano de estudo</param>
        /// <returns>Verdadeiro se existir solicitação de matrícula ou plano de estudo</returns>
        public bool VerificarDivisaoTurmaMatriculaPlano(long seq, bool apenasAtivaEAtual = true)
        {
            //Se existir solicitação de matricula para a turma já retorna true
            if (VerificarSolicitacaoServicoAtiva(seq, apenasAtivaEAtual))
                return true;

            //Se existir plano de estudo para a turma já retorna true
            return VerificarExistePlanoEstudoAtual(seq, apenasAtivaEAtual);
        }

        /// <summary>
        /// Busca a data de inicio e fim de um evento letivo de uma turma, baseado na divisão da turma.
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão da turma</param>
        /// <returns>Datas de início e fim do evento letivo.</returns>
        public (DateTime dataInicio, DateTime dataFim) BuscarDatasEventoLetivoTurma(long seqDivisaoTurma)
        {
            // Busca as informações da divisão de turma
            var spec = new SMCSeqSpecification<DivisaoTurma>(seqDivisaoTurma);
            var dadosDivisaoTurma = this.SearchProjectionByKey(spec, d => new
            {
                SeqCicloLetivoInicio = d.Turma.SeqCicloLetivoInicio,
                SeqCicloLetivoFim = d.Turma.SeqCicloLetivoFim,
                SeqCursoOfertaLocalidadeTurno = (long?)d.Turma.ConfiguracoesComponente.FirstOrDefault(c => c.RestricoesTurmaMatriz.Any(r => r.OfertaMatrizPrincipal)).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).SeqCursoOfertaLocalidadeTurno
            });

            if (!dadosDivisaoTurma.SeqCursoOfertaLocalidadeTurno.HasValue)
                throw new TurmaSemMatrizPrincipalException("de turma");

            if (dadosDivisaoTurma.SeqCicloLetivoInicio == dadosDivisaoTurma.SeqCicloLetivoFim)
            {
                var datas = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosDivisaoTurma.SeqCicloLetivoInicio, dadosDivisaoTurma.SeqCursoOfertaLocalidadeTurno.Value, TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                return (datas.DataInicio, datas.DataFim);
            }
            else
            {
                DateTime dataInicio = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosDivisaoTurma.SeqCicloLetivoInicio, dadosDivisaoTurma.SeqCursoOfertaLocalidadeTurno.Value, TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_LETIVO).DataInicio;
                DateTime dataFim = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosDivisaoTurma.SeqCicloLetivoFim, dadosDivisaoTurma.SeqCursoOfertaLocalidadeTurno.Value, TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_LETIVO).DataFim;

                return (dataInicio, dataFim);
            }
        }

        /// <summary>
        /// Buscar o sequencial do curso oferta localidade turno partindo da divisão de turma, turma, turma configuração e matriz de restrição
        /// </summary>
        /// <param name="seq">Sequencial da divisão turma</param>
        /// <returns>Sequencial do curso oferta localidade turno</returns>
        public long BuscarCursoOfertaLocalidadeTurnoPorDivisaoTurmaQuantidades(long seq)
        {
            var seqCursoOfertaLocalidadeTurno = this.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seq),
                                                   p => p.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal)
                                                               .RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal)
                                                               .SeqCursoOfertaLocalidadeTurno);

            return seqCursoOfertaLocalidadeTurno;
        }

        /// <summary>
        /// Verifica se existe alguma solicitação de serviço ativa, que possue a turma como item
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão de turma</param>
        /// <returns>Verdadeiro se existir solicitação de matrícula</returns>
        public bool VerificarSolicitacaoServicoAtiva(long seqDivisaoTurma, bool apenasAtiva = true)
        {
            //Se existir solicitação de matricula para a turma já retorna true
            var filtroSolicitacaoMatricula = new SolicitacaoMatriculaItemFilterSpecification() { SeqDivisaoTurma = seqDivisaoTurma };
            filtroSolicitacaoMatricula.ClassificacaoSituacaoFinal = (apenasAtiva) ? ClassificacaoSituacaoFinal.FinalizadoComSucesso : (ClassificacaoSituacaoFinal?)null;

            return SolicitacaoMatriculaItemDomainService.Count(filtroSolicitacaoMatricula) > 0;
        }

        /// <summary>
        /// Verificar se existe plano de estudo para a turma
        /// </summary>
        /// <param name="seqDivisaoTurma"></param>
        /// <returns></returns>
        public bool VerificarExistePlanoEstudoAtual(long seqDivisaoTurma, bool apenasAtual = true)
        {
            //Se existir plano de estudo para a turma já retorna true
            var filtroPlanoEstudo = new PlanoEstudoItemFilterSpecification() { SeqDivisaoTurma = seqDivisaoTurma };
            filtroPlanoEstudo.PlanoEstudoAtual = apenasAtual ? true : (bool?)null;

            return PlanoEstudoItemDomainService.Count(filtroPlanoEstudo) > 0;
        }

        /// <summary>
        /// Verifica se existe vaga livre para uma determinada turma
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão de turma</param>
        /// <returns>Vagas livres ou não</returns>
        public DivisaoTurmaVagasVO VerificarVagaLivreTurma(long seqDivisaoTurma)
        {
            var divisaoAtual = BuscarDivisaoTurmaQuantidades(seqDivisaoTurma);
            short quantidadeVagas = divisaoAtual.QuantidadeVagas;
            short quantidadeVagasOcupadas = divisaoAtual.QuantidadeVagasOcupadas.GetValueOrDefault();
            short quantidadeVagasReservadas = divisaoAtual.QuantidadeVagasReservadas.GetValueOrDefault();
            bool vagasLivres = ((quantidadeVagas - (quantidadeVagasReservadas + quantidadeVagasOcupadas)) > 0);

            var ret = new DivisaoTurmaVagasVO
            {
                QuantidadeVagas = quantidadeVagas,
                QuantidadeVagasOcupadas = quantidadeVagasOcupadas,
                QuantidadeVagasReservadas = quantidadeVagasReservadas,
                VagasLivres = vagasLivres,
            };
            if (!vagasLivres)
                ret.MotivoSemVaga = MotivoSituacaoMatricula.VagasExcedidas;

            return ret;
        }

        /// <summary>
        /// Processa a vaga de uma turma
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqDivisaoTurma">Sequencial da divisão da turma a processar a vaga</param>
        /// <param name="itemDescricao">Descrição do item que está sendo processado</param>
        /// <param name="desabilitarFiltro">Desabilitar ou não o filtro de dados</param>
        /// <returns>Mensagem de erro caso não tenha vagas ou qualquer outro motivo</returns>
        public string ProcessarVagaTurma(long seqPessoaAtuacao, long seqDivisaoTurma, string itemDescricao, bool desabilitarFiltro = false)
        {
            if (desabilitarFiltro)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var dadosDivisao = this.SearchProjectionByKey(seqDivisaoTurma, x => new
            {
                SeqConfiguracaoComponentePrincipal = x.Turma.ConfiguracoesComponente.FirstOrDefault(c => c.Principal).Seq
            });

            if (dadosDivisao != null)
            {
                try
                {
                    IngressanteTurmaVO dadosIngressante = null;
                    long? seqMatrizCurricularOferta = null;
                    var tipoAtuacao = PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(seqPessoaAtuacao, desabilitarFiltro);
                    var dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao, desabilitarFiltro);

                    //Verificar se o vínculo da pessoa-atuação foi parametrizado para exigir oferta de matriz:
                    if (tipoAtuacao == TipoAtuacao.Aluno)
                        seqMatrizCurricularOferta = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(seqPessoaAtuacao, desabilitarFiltro).SeqMatrizCurricularOferta;
                    else if (tipoAtuacao == TipoAtuacao.Ingressante)
                    {
                        dadosIngressante = IngressanteDomainService.BuscarIngressanteMatrizOferta(seqPessoaAtuacao, desabilitarFiltro);
                        seqMatrizCurricularOferta = dadosIngressante.SeqMatrizCurricularOferta;
                    }

                    /*
					* Verificar se existe vaga na campanha quando NÃO exigir oferta de matriz RN_MAT_020 Exibição turmas na matrícula
					*/
                    if (dadosVinculo.ExigeOfertaMatrizCurricular.GetValueOrDefault() == false && dadosIngressante != null)
                    {
                        var campanhaOfertaAtual = ProcessoSeletivoOfertaDomainService.BuscarProcessoSeletivoOfertaQuantidades(dadosIngressante.SeqProcessoSeletivo, seqDivisaoTurma);
                        if (campanhaOfertaAtual != null)
                        {
                            if (campanhaOfertaAtual.ProcessoReservaVaga == true)
                            {
                                short quantidadeVagasCampanha = campanhaOfertaAtual.QuantidadeVagas;
                                short quantidadeVagasCampanhaOcupadas = campanhaOfertaAtual.QuantidadeVagasOcupadas;

                                //Se o resultado dessa subtração for um valor maior ou igual a 0:
                                if ((quantidadeVagasCampanha - quantidadeVagasCampanhaOcupadas) > 0)
                                {
                                    var vagasAtualizadasCampanha = quantidadeVagasCampanhaOcupadas + 1;
                                    ProcessoSeletivoOfertaDomainService.AtualizarQuantidadeVagasOcupadas(campanhaOfertaAtual.Seq, (short)vagasAtualizadasCampanha);
                                    return string.Empty;
                                }
                                else
                                {
                                    /*
									 * Motivo de situação do item, a situação “Vagas excedidas”.
									 */
                                    return $"{itemDescricao}  - Motivo: {MotivoSituacaoMatricula.VagasExcedidas.SMCGetDescription()}";
                                }
                            }
                        }
                        else
                        {
                            //Se for disciplina isolada e não tiver campanha retorna o item como cancelado
                            return $"{itemDescricao}  - Motivo: {MotivoSituacaoMatricula.ItemCancelado.SMCGetDescription()}";
                        }
                    }

                    /*
					* Verificar se existe vaga na Turma, de acordo com a regra:
					* Subtrair da quantidade de vagas da divisão, o somatório da quantidade de vagas reservadas com a quantidade de vagas ocupadas
					* [qtd_vagas - (qtd_vagas_reservadas + qtd_vagas_ocupadas)].
					*/
                    var divisaoAtual = BuscarDivisaoTurmaQuantidades(seqDivisaoTurma);
                    short quantidadeVagas = divisaoAtual.QuantidadeVagas;
                    short quantidadeVagasOcupadas = divisaoAtual.QuantidadeVagasOcupadas.GetValueOrDefault();
                    short quantidadeVagasReservadas = divisaoAtual.QuantidadeVagasReservadas.GetValueOrDefault();

                    //Se o resultado dessa subtração for um valor maior que 0:
                    if ((quantidadeVagas - (quantidadeVagasReservadas + quantidadeVagasOcupadas)) > 0)
                    {
                        /*
						* Verifica se o vínculo da pessoa atuação exige curso ou se o tipo da pessoa atuação é aluno
						*/

                        if (dadosVinculo.ExigeCurso.GetValueOrDefault() || tipoAtuacao == TipoAtuacao.Aluno)
                        {
                            var vagasAtualizadasDivisao = quantidadeVagasOcupadas + 1;
                            AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                            return string.Empty;
                        }

                        if (dadosVinculo.ExigeOfertaMatrizCurricular.GetValueOrDefault() == true)
                        {
                            /*
							 * Verificar se a quantidade de vagas ocupadas na matriz da oferta encontrada é menor que a quantidade de vagas reservadas na matriz,
							 * de acordo com a oferta de matriz associada à pessoa - atuação em questão
							 */

                            var restricaoMatriz = RestricaoTurmaMatrizDomainService.BuscarRestricaoTurmaMatrizPorTurmaConfiguracaoMatriz(dadosDivisao.SeqConfiguracaoComponentePrincipal, seqMatrizCurricularOferta);

                            /*
							 * Quando não possuir registro na restrição de matriz continuar o fluxo
							 * Verificado com Mariana e Humberto e futuramente será reavaliado esta regra
							 */

                            if (restricaoMatriz == null)
                            {
                                /*
								 * Salvar no campo quantidade de vaga ocupada da divisão o valor atual do campo + 1.
								 */

                                var vagasAtualizadasDivisao = quantidadeVagasOcupadas + 1;
                                AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                                return string.Empty;
                            }

                            short restricaoOcupada = restricaoMatriz.QuantidadeVagasOcupadas.GetValueOrDefault();
                            short restricaoReservadas = restricaoMatriz.QuantidadeVagasReservadas.GetValueOrDefault();

                            //Conversado com a Jessica se a quantidade reservado for 0 continuar com o processamento da divisão

                            if (restricaoOcupada < restricaoReservadas)
                            {
                                var vagasAtualizadasOferta = restricaoMatriz.QuantidadeVagasOcupadas + 1;
                                RestricaoTurmaMatrizDomainService.AtualizarQuantidadeVagasOculpadas(restricaoMatriz.Seq, (short)vagasAtualizadasOferta);

                                var vagasAtualizadasDivisao = quantidadeVagasOcupadas + 1;
                                AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                                return string.Empty;
                            }
                            else if (restricaoReservadas == 0)
                            {
                                var vagasAtualizadasDivisao = quantidadeVagasOcupadas + 1;
                                AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                                return string.Empty;
                            }
                            else
                            {
                                /*
								 * Motivo de situação do item, a situação “Vagas excedidas”.
								 */
                                return $"{itemDescricao}  - Motivo: {MotivoSituacaoMatricula.VagasExcedidas.SMCGetDescription()}";
                            }
                        }
                        else
                        {
                            /*
							 * Salvar no campo quantidade de vaga ocupada da divisão o valor atual do campo + 1.
							 */

                            var vagasAtualizadasDivisao = quantidadeVagasOcupadas + 1;
                            AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                            return string.Empty;
                        }
                    }
                    else
                    {
                        /*
						 * Motivo de situação do item, a situação “Vagas excedidas”.
						 */

                        return $"{itemDescricao}  - Motivo: {MotivoSituacaoMatricula.VagasExcedidas.SMCGetDescription()}";
                    }
                }
                catch (Exception ex)
                {
                    return $"{itemDescricao}  - Motivo: {MotivoSituacaoMatricula.ItemCancelado.SMCGetDescription()}";
                }
            }

            if (desabilitarFiltro)
            {
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            return string.Empty;
        }

        /// <summary>
        /// Libera uma vaga de uma turma
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqDivisaoTurma">Sequencial da divisão de turma</param>
        /// <param name="desabilitarFiltro">Desativar ou não os filtros</param>
        public void LiberarVagaTurma(long seqPessoaAtuacao, long seqDivisaoTurma, bool desabilitarFiltro = false)
        {
            if (desabilitarFiltro)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var dadosDivisao = this.SearchProjectionByKey(seqDivisaoTurma, x => new
            {
                SeqConfiguracaoComponentePrincipal = x.Turma.ConfiguracoesComponente.FirstOrDefault(c => c.Principal).Seq
            });

            if (dadosDivisao != null)
            {
                try
                {
                    IngressanteTurmaVO dadosIngressante = null;
                    long? seqMatrizCurricularOferta = null;
                    var tipoAtuacao = PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(seqPessoaAtuacao, desabilitarFiltro);
                    var dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao, desabilitarFiltro);

                    //Verificar se o vínculo da pessoa-atuação foi parametrizado para exigir oferta de matriz:
                    if (tipoAtuacao == TipoAtuacao.Aluno)
                        seqMatrizCurricularOferta = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(seqPessoaAtuacao, desabilitarFiltro).SeqMatrizCurricularOferta;
                    else if (tipoAtuacao == TipoAtuacao.Ingressante)
                    {
                        dadosIngressante = IngressanteDomainService.BuscarIngressanteMatrizOferta(seqPessoaAtuacao, desabilitarFiltro);
                        seqMatrizCurricularOferta = dadosIngressante.SeqMatrizCurricularOferta;
                    }

                    /*
					* Verificar se existe vaga na campanha quando NÃO exigir oferta de matriz RN_MAT_020 Exibição turmas na matrícula
					*/
                    if (dadosVinculo.ExigeOfertaMatrizCurricular.GetValueOrDefault() == false && dadosIngressante != null)
                    {
                        var campanhaOfertaAtual = ProcessoSeletivoOfertaDomainService.BuscarProcessoSeletivoOfertaQuantidades(dadosIngressante.SeqProcessoSeletivo, seqDivisaoTurma);
                        if (campanhaOfertaAtual != null && campanhaOfertaAtual.ProcessoReservaVaga == true)
                        {
                            short quantidadeVagasCampanha = campanhaOfertaAtual.QuantidadeVagas;
                            short quantidadeVagasCampanhaOcupadas = campanhaOfertaAtual.QuantidadeVagasOcupadas;

                            if (quantidadeVagasCampanhaOcupadas > 0)
                            {
                                var vagasAtualizadasCampanha = quantidadeVagasCampanhaOcupadas - 1;
                                ProcessoSeletivoOfertaDomainService.AtualizarQuantidadeVagasOcupadas(campanhaOfertaAtual.Seq, (short)vagasAtualizadasCampanha);
                                return;
                            }
                        }
                    }

                    var divisaoAtual = BuscarDivisaoTurmaQuantidades(seqDivisaoTurma);
                    short quantidadeVagas = divisaoAtual.QuantidadeVagas;
                    short quantidadeVagasOcupadas = divisaoAtual.QuantidadeVagasOcupadas.GetValueOrDefault();
                    short quantidadeVagasReservadas = divisaoAtual.QuantidadeVagasReservadas.GetValueOrDefault();

                    //Se o resultado dessa subtração for um valor maior ou igual a 0:
                    if (quantidadeVagasOcupadas > 0)
                    {
                        /*
						* Verifica se o vínculo da pessoa atuação exige curso ou se o tipo da pessoa atuação é aluno
						*/
                        if (dadosVinculo.ExigeCurso.GetValueOrDefault() || tipoAtuacao == TipoAtuacao.Aluno)
                        {
                            var vagasAtualizadasDivisao = quantidadeVagasOcupadas - 1;
                            AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                            return;
                        }

                        if (dadosVinculo.ExigeOfertaMatrizCurricular.GetValueOrDefault() == true)
                        {
                            var restricaoMatriz = RestricaoTurmaMatrizDomainService.BuscarRestricaoTurmaMatrizPorTurmaConfiguracaoMatriz(dadosDivisao.SeqConfiguracaoComponentePrincipal, seqMatrizCurricularOferta);

                            if (restricaoMatriz == null)
                            {
                                var vagasAtualizadasDivisao = quantidadeVagasOcupadas - 1;
                                AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                                return;
                            }

                            short restricaoOcupada = restricaoMatriz.QuantidadeVagasOcupadas.GetValueOrDefault();
                            short restricaoReservadas = restricaoMatriz.QuantidadeVagasReservadas.GetValueOrDefault();

                            if (restricaoOcupada > 0)
                            {
                                var vagasAtualizadasOferta = restricaoMatriz.QuantidadeVagasOcupadas - 1;
                                RestricaoTurmaMatrizDomainService.AtualizarQuantidadeVagasOculpadas(restricaoMatriz.Seq, (short)vagasAtualizadasOferta);

                                var vagasAtualizadasDivisao = quantidadeVagasOcupadas - 1;
                                AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                                return;
                            }
                            else if (restricaoOcupada == 0)
                            {
                                var vagasAtualizadasDivisao = quantidadeVagasOcupadas - 1;
                                AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                                return;
                            }
                        }
                        else
                        {
                            var vagasAtualizadasDivisao = quantidadeVagasOcupadas - 1;
                            AtualizarQuantidadeVagasOcupadas(seqDivisaoTurma, (short)vagasAtualizadasDivisao);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (desabilitarFiltro)
            {
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
        }

        /// <summary>
        /// Montar descrição divisao de turma - RN_TUR_026 - Exibição Descrição - Divisão Turma
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial divisão de turma</param>
        /// <returns>Descrição divisão de turma</returns>
        public string ObterDescricaoDivisaoTurma(long seqDivisaoTurma, long seqAluno = 0, bool telaDetalhes = false)
        {
            /*RN_TUR_023 - Exibição Codificação de turma
            A codificação de uma turma deverá ser exibida sempre com a seguinte concatenação de dados:
            [Código da Turma] + "." + [Número da Turma] + "." + [Número da Divisão da Configuração do
            Componente] + "." + [Número do Grupo].*/

            /*RN_TUR_026 - Exibição Descrição - Divisão Turma
            A descrição de uma divisão de turma deverá ser a seguinte concatenação de dados:
            [Descrição da configuração do componente] + ":" + [Descrição da configuração do componente
            substituto] + "-" + [Carga horária e Crédito do componente] + [label parametrizado] + [quebra de linha]
            [Codificação (RN_TUR_023 - Exibição Codificação de turma)] + "-" + [Descrição da organização da
            divisão do componente] + "-" + [Descrição do tipo de divisão do componente] + "-" + [Carga horária da 
            divisão da configuração do componente] + [label parametrizado]
            A organização na divisão do componente só deve ser exibida caso exista organização associada à
            divisão.
            ": " após a descrição da configuração do componente só deve ser exibida se houver assunto
            (componente substituto)
            Caso exista um currículo associado a pessoa-atuação logada ou do cabeçalho, verificar se uma das
            configurações de componente da turma está associada à oferta de matriz da pessoa-atuação em
            questão. Se sim, exibir os dados da configuração associada à oferta de matriz curricular da
            pessoa-atuação em questão. Se não, exibir os dados da configuração principal da turma.
            Caso não exista um currículo associado, exibir os dados da configuração principal da turma.
            O label parametrizado é o conteúdo do campo Formato em Parâmetros por Instituição e Nível de
            Ensino, para o tipo do componente em questão.
            Na ausência da carga horária, retirar o respectivo label.*/

            string retorno = string.Empty;
            DivisaoTurmaDetalhesVO divisaoTurma = BuscarDivisaoTurmaCabecalho(seqDivisaoTurma, telaDetalhes);
            long? seqMatrizCurricularOferta = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(seqAluno).SeqMatrizCurricularOferta;

            if (seqMatrizCurricularOferta.GetValueOrDefault() > 0)
            {
                RestricaoTurmaMatrizFilterSpecification spec = new RestricaoTurmaMatrizFilterSpecification()
                {
                    SeqMatrizCurricularOferta = seqMatrizCurricularOferta.Value,
                    SeqTurma = divisaoTurma.SeqTurma
                };

                var restricaoTurma = RestricaoTurmaMatrizDomainService.SearchProjectionBySpecification(spec, a => new DivisaoTurmaDetalhesVO()
                {
                    TurmaCodigo = a.TurmaConfiguracaoComponente.Turma.Codigo,
                    TurmaNumero = a.TurmaConfiguracaoComponente.Turma.Numero,
                    DescricaoTurmaConfiguracaoComponente = a.TurmaConfiguracaoComponente.Descricao,
                    DescricaoConfiguracaoComponente = a.TurmaConfiguracaoComponente.ConfiguracaoComponente.Descricao,
                    SeqNivelEnsino = a.TurmaConfiguracaoComponente.ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino,
                    SeqTipoComponenteCurricular = a.TurmaConfiguracaoComponente.ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                    SeqComponenteCurricular = a.TurmaConfiguracaoComponente.ConfiguracaoComponente.SeqComponenteCurricular,
                    TipoDivisaoDescricao = a.TurmaConfiguracaoComponente.Turma.DivisoesTurma.FirstOrDefault(b => b.Seq == seqDivisaoTurma).DivisaoComponente.TipoDivisaoComponente.Descricao,
                    Numero = a.TurmaConfiguracaoComponente.Turma.DivisoesTurma.FirstOrDefault(b => b.Seq == seqDivisaoTurma).DivisaoComponente.Numero,
                    CargaHoraria = a.TurmaConfiguracaoComponente.Turma.DivisoesTurma.FirstOrDefault(b => b.Seq == seqDivisaoTurma).DivisaoComponente.CargaHoraria,
                    //Credito = a.TurmaConfiguracaoComponente.Turma.DivisoesTurma.FirstOrDefault(b => b.Seq == seqDivisaoTurma).DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito,
                    CargaHorariaComponenteCurricular = a.TurmaConfiguracaoComponente.ConfiguracaoComponente.ComponenteCurricular.CargaHoraria,
                    CreditoComponenteCurricular = a.TurmaConfiguracaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito,
                    NumeroGrupo = a.TurmaConfiguracaoComponente.Turma.DivisoesTurma.FirstOrDefault(b => b.Seq == seqDivisaoTurma).NumeroGrupo,
                    DescricaoComponenteCurricularOrganizacao = a.TurmaConfiguracaoComponente.Turma.DivisoesTurma.FirstOrDefault(b => b.Seq == seqDivisaoTurma).DivisaoComponente.ComponenteCurricularOrganizacao.Descricao,
                    DescricaoComponenteCurricularAssunto = a.ComponenteCurricularAssunto.Descricao

                }).ToList();

                if (restricaoTurma.Any())
                {
                    var dadosRestricaoTurma = restricaoTurma.First();

                    MontarTurmaDescricaoFormatado(ref dadosRestricaoTurma, telaDetalhes);

                    retorno = dadosRestricaoTurma.TurmaDescricaoFormatado;
                }

                if (!string.IsNullOrEmpty(retorno))
                {
                    return retorno;
                }
            }

            return divisaoTurma.TurmaDescricaoFormatado;
        }

        public List<string> ValidarOfertaMatrizExcluidas(long seqTurma, List<long> seqsMatrizesCurricularesOfertasExcluidas)
        {
            if (seqsMatrizesCurricularesOfertasExcluidas != null && seqsMatrizesCurricularesOfertasExcluidas.Any())
            {
                // Recupera todos os SeqDivisaoTurma das turmas que possuem a matriz em suas RestricoesMatriz
                var dadosTurmas = RestricaoTurmaMatrizDomainService.SearchProjectionBySpecification(new RestricaoTurmaMatrizFilterSpecification
                {
                    SeqTurma = seqTurma,
                    SeqsMatrizCurricularOferta = seqsMatrizesCurricularesOfertasExcluidas
                }, x => new
                {
                    SeqCicloLetivoTurma = x.TurmaConfiguracaoComponente.Turma.SeqCicloLetivoInicio,
                    Divisoes = x.TurmaConfiguracaoComponente.Turma.DivisoesTurma.Select(d => d.Seq)
                }).ToList();

                var seqsDivisoesTurma = dadosTurmas?.SelectMany(t => t.Divisoes)?.Distinct()?.ToList();

                if (seqsDivisoesTurma != null && seqsDivisoesTurma.Any())
                {
                    var seqCicloLetivoTurma = dadosTurmas.FirstOrDefault().SeqCicloLetivoTurma;

                    // Verifica se existe algum aluno/ingressante com solicitação ativa que possua algum item das divisões acima
                    var dadosSolicitacoesExistentes = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(new SolicitacaoMatriculaItemFilterSpecification() { SeqsDivisoesTurma = seqsDivisoesTurma, SolicitacaoMatriculaAtiva = true }, x =>
                    new
                    {
                        SeqSolicitacaoMatricula = x.SeqSolicitacaoMatricula,
                        SeqMatrizCurricularOfertaIngressante = (long?)(x.SolicitacaoMatricula.PessoaAtuacao as Ingressante).SeqMatrizCurricularOferta,
                        SeqMatrizCurricularOfertaAluno = (long?)(x.SolicitacaoMatricula.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == seqCicloLetivoTurma).PlanosEstudo.FirstOrDefault(p => p.Atual).SeqMatrizCurricularOferta,

                        CodigoMatrizCurricularOfertaIngressante = (x.SolicitacaoMatricula.PessoaAtuacao as Ingressante).MatrizCurricularOferta.Codigo,
                        CodigoMatrizCurricularOfertaAluno = (x.SolicitacaoMatricula.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == seqCicloLetivoTurma).PlanosEstudo.FirstOrDefault(p => p.Atual).MatrizCurricularOferta.Codigo
                    }).ToList();

                    if (dadosSolicitacoesExistentes != null && dadosSolicitacoesExistentes.Any())
                    {
                        var matrizesProblema = dadosSolicitacoesExistentes.Where(s =>
                            (s.SeqMatrizCurricularOfertaAluno.HasValue && seqsMatrizesCurricularesOfertasExcluidas.Contains(s.SeqMatrizCurricularOfertaAluno.Value)) ||
                            (s.SeqMatrizCurricularOfertaIngressante.HasValue && seqsMatrizesCurricularesOfertasExcluidas.Contains(s.SeqMatrizCurricularOfertaIngressante.Value))).Select(x => x.CodigoMatrizCurricularOfertaAluno ?? x.CodigoMatrizCurricularOfertaIngressante).Distinct().ToList();

                        if (matrizesProblema != null && matrizesProblema.Any())
                            return matrizesProblema;
                    }

                    // Verificar se algum item do plano de estudo possui o seqdivisao da lista. Se sim, buscar a matriz e comparar.
                    var dadosPlanoEstudo = PlanoEstudoDomainService.SearchProjectionBySpecification(new PlanoEstudoFilterSpecification
                    {
                        SeqsDivisaoTurma = seqsDivisoesTurma.ToArray(),
                        Atual = true,
                        SeqCicloLetivo = seqCicloLetivoTurma
                    }, x => new
                    {
                        x.SeqMatrizCurricularOferta,
                        x.MatrizCurricularOferta.Codigo
                    }).Distinct().ToList();

                    if (dadosPlanoEstudo != null && dadosPlanoEstudo.Any())
                    {
                        var matrizesProblema = dadosPlanoEstudo.Where(s => s.SeqMatrizCurricularOferta.HasValue && seqsMatrizesCurricularesOfertasExcluidas.Contains(s.SeqMatrizCurricularOferta.Value)).Select(x => x.Codigo).Distinct().ToList();
                        if (matrizesProblema != null && matrizesProblema.Any())
                            return matrizesProblema;
                    }
                }
            }
            return null;
        }

        public DetalheDivisaoTurmaGradeVO BuscarDetalhesDivisaoTurmaGrade(long seqDivisaoTurma)
        {
            var divisaoTurma = this.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seqDivisaoTurma), p => new DetalheDivisaoTurmaGradeVO()
            {
                SeqCursoOfertaLocalidade = p.Turma.ConfiguracoesComponente.SelectMany(s => s.RestricoesTurmaMatriz)
                                                .FirstOrDefault(f => f.OfertaMatrizPrincipal)
                                                .CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                CodigoTurma = p.Turma.Codigo + "." + p.Turma.Numero,
                DescricaoCicloLetivoInicio = p.Turma.CicloLetivoInicio.Descricao,
                DescricaoTipoTurma = p.Turma.TipoTurma.Descricao,
                SituacaoTurmaAtual = p.Turma.HistoricosSituacao.Count > 0 ? p.Turma.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma : SituacaoTurma.Nenhum,
                DescricoesCursoOfertaLocalidadeTurno = p.Turma.ConfiguracoesComponente.SelectMany(w => w.RestricoesTurmaMatriz.Select(r => r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome + " - " + r.CursoOfertaLocalidadeTurno.Turno.Descricao)).Distinct().OrderBy(d => d).ToList(),
                DescricaoLocalidade = p.Localidade.Nome,
                QuantidadeVagasOcupadas = p.QuantidadeVagasOcupadas,
                CargaHorariaGrade = p.DivisaoComponente.CargaHorariaGrade,
                CargaHorariaLancada = p.EventosAula.Count(a => a.Data >= p.Turma.DataInicioPeriodoLetivo && a.Data <= p.Turma.DataFimPeriodoLetivo),
                SeqAgendaTurma = p.Turma.SeqAgendaTurma,
                DataInicioPeriodoLetivo = p.Turma.DataInicioPeriodoLetivo,
                DataFimPeriodoLetivo = p.Turma.DataFimPeriodoLetivo,
                TipoDistribuicaoAula = p.HistoricoConfiguracaoGradeAtual.TipoDistribuicaoAula,
                AulaSabado = p.HistoricoConfiguracaoGradeAtual.AulaSabado,
                TipoPulaFeriado = p.HistoricoConfiguracaoGradeAtual.TipoPulaFeriado,
                LocaisAula = p.EventosAula.Where(a => !string.IsNullOrEmpty(a.Local) && (a.Data >= p.Turma.DataInicioPeriodoLetivo && a.Data <= p.Turma.DataFimPeriodoLetivo)).Select(b => new DetalheDivisaoTurmaGradeLocalVO()
                {
                    DescricaoLocalAula = b.Local
                }).Distinct().OrderBy(c => c).ToList(),
                Professores = p.EventosAula.Where(a => a.Data >= p.Turma.DataInicioPeriodoLetivo && a.Data <= p.Turma.DataFimPeriodoLetivo).SelectMany(b => b.Colaboradores.Select(c => new DetalheDivisaoTurmaGradeProfessorVO()
                {
                    SeqPessoaAtuacao = c.Colaborador.Seq,
                    Nome = c.Colaborador.DadosPessoais.Nome,
                    NomeSocial = c.Colaborador.DadosPessoais.NomeSocial,
                    SeqPessoaAtuacaoProfessorSubstituto = c.ColaboradorSubstituto.Seq,
                    NomeProfessorSubstituto = c.ColaboradorSubstituto.DadosPessoais.Nome,
                    NomeSocialProfessorSubstituto = c.ColaboradorSubstituto.DadosPessoais.NomeSocial,
                    Formacao = c.Colaborador.FormacoesAcademicas.Any(d => d.TitulacaoMaxima) ? c.Colaborador.FormacoesAcademicas.FirstOrDefault(d => d.TitulacaoMaxima).Descricao : "Não cadastrada",
                    FormacaoProfessorSubstituto = c.ColaboradorSubstituto.FormacoesAcademicas.Any(d => d.TitulacaoMaxima) ? c.ColaboradorSubstituto.FormacoesAcademicas.FirstOrDefault(d => d.TitulacaoMaxima).Descricao : "Não cadastrada",
                    //Se tiver professor substituto vai contar a carga horária só para o substituto
                    CargaHoraria = p.EventosAula.Where(d => d.Data >= p.Turma.DataInicioPeriodoLetivo && d.Data <= p.Turma.DataFimPeriodoLetivo).SelectMany(d => d.Colaboradores).Count(d => (!d.SeqColaboradorSubstituto.HasValue && d.SeqColaborador == c.Colaborador.Seq) || (d.SeqColaboradorSubstituto.HasValue && d.SeqColaboradorSubstituto == c.Colaborador.Seq)),
                    CargaHorariaProfessorSubstituto = p.EventosAula.Where(d => d.Data >= p.Turma.DataInicioPeriodoLetivo && d.Data <= p.Turma.DataFimPeriodoLetivo).SelectMany(d => d.Colaboradores).Count(d => (!d.SeqColaboradorSubstituto.HasValue && d.SeqColaborador == c.ColaboradorSubstituto.Seq) || (d.SeqColaboradorSubstituto.HasValue && d.SeqColaboradorSubstituto == c.ColaboradorSubstituto.Seq))
                })).ToList()
            });

            if (divisaoTurma.TipoPulaFeriado == TipoPulaFeriado.NaoPula)
            {
                var cargaHoraria = this.SearchProjectionByKey(seqDivisaoTurma, p => new
                {
                    CargaHorariaLancada = p.EventosAula.Count(a => !a.Feriado && a.Data >= p.Turma.DataInicioPeriodoLetivo && a.Data <= p.Turma.DataFimPeriodoLetivo),
                    Professores = p.EventosAula.Where(a => !a.Feriado && a.Data >= p.Turma.DataInicioPeriodoLetivo && a.Data <= p.Turma.DataFimPeriodoLetivo).SelectMany(b => b.Colaboradores.Select(c => new
                    {
                        SeqPessoaAtuacao = c.SeqColaborador,
                        SeqPessoaAtuacaoProfessorSubstituto = c.SeqColaboradorSubstituto,
                        CargaHoraria = p.EventosAula.Where(d => !d.Feriado && d.Data >= p.Turma.DataInicioPeriodoLetivo && d.Data <= p.Turma.DataFimPeriodoLetivo).SelectMany(d => d.Colaboradores).Count(d => (!d.SeqColaboradorSubstituto.HasValue && d.SeqColaborador == c.Colaborador.Seq) || (d.SeqColaboradorSubstituto.HasValue && d.SeqColaboradorSubstituto == c.Colaborador.Seq)),
                        CargaHorariaProfessorSubstituto = p.EventosAula.Where(d => !d.Feriado && d.Data >= p.Turma.DataInicioPeriodoLetivo && d.Data <= p.Turma.DataFimPeriodoLetivo).SelectMany(d => d.Colaboradores).Count(d => (!d.SeqColaboradorSubstituto.HasValue && d.SeqColaborador == c.ColaboradorSubstituto.Seq) || (d.SeqColaboradorSubstituto.HasValue && d.SeqColaboradorSubstituto == c.ColaboradorSubstituto.Seq))
                    })).ToList()
                });

                divisaoTurma.CargaHorariaLancada = cargaHoraria.CargaHorariaLancada;
                foreach (var professores in divisaoTurma.Professores)
                {
                    var professoresFeriado = cargaHoraria
                        .Professores
                        .First(f => f.SeqPessoaAtuacao == professores.SeqPessoaAtuacao
                                 && f.SeqPessoaAtuacaoProfessorSubstituto == professores.SeqPessoaAtuacaoProfessorSubstituto);
                    professores.CargaHoraria = professoresFeriado.CargaHoraria;
                    professores.CargaHorariaProfessorSubstituto = professoresFeriado.CargaHorariaProfessorSubstituto;
                }
            }

            //Ajustar carga horaria do professor caso o mesmo na mesma divisão seja professor substituto e professor responsavel pela aula
            divisaoTurma.Professores.ForEach(professor =>
            {
                if (professor.CargaHorariaProfessorSubstituto > 0)
                {
                    int aulasNaoSubstituo = divisaoTurma.Professores.Where(w => w.SeqPessoaAtuacao == professor.SeqPessoaAtuacaoProfessorSubstituto && w.SeqPessoaAtuacaoProfessorSubstituto == null).Count();
                    professor.CargaHorariaProfessorSubstituto = professor.CargaHorariaProfessorSubstituto - aulasNaoSubstituo;

                    divisaoTurma.Professores.ForEach(professorNaoSubstituto =>
                    {
                        if (professorNaoSubstituto.SeqPessoaAtuacao == professor.SeqPessoaAtuacaoProfessorSubstituto)
                        {
                            professorNaoSubstituto.CargaHoraria = aulasNaoSubstituo;
                        }
                    });
                }
            });

            divisaoTurma.Professores = divisaoTurma.Professores.SMCDistinct(d => d.NomeProfessor).OrderBy(e => e.SeqPessoaAtuacaoOrdenacao).ThenBy(e => e.NomeProfessor).ToList();

            var informacoesDivisaoCabecalho = BuscarDivisaoTurmaCabecalho(seqDivisaoTurma);
            divisaoTurma.TurmaDescricaoFormatado = informacoesDivisaoCabecalho.TurmaDescricaoFormatado;

            if (divisaoTurma.SeqAgendaTurma.HasValue)
            {
                var tabelaHorario = this.TabelaHorarioService.BuscarTabelaHorarioAgendaTurma(new TabelaHorarioAgendaTurmaFiltroData()
                {
                    SeqAgenda = divisaoTurma.SeqAgendaTurma.Value,
                    DataInicioPeriodoLetivo = divisaoTurma.DataInicioPeriodoLetivo,
                    DataFimPeriodoLetivo = divisaoTurma.DataFimPeriodoLetivo
                });

                divisaoTurma.DescricaoTabelaHorario = tabelaHorario.Descricao;
            }

            return divisaoTurma;
        }

        /// <summary>
        /// Formata o código da divisão de turma segundo a regra RN_TUR_023 - Exibição Codificação de turma
        /// </summary>
        /// <param name="codigoTurma">Código da turma</param>
        /// <param name="numeroTurma">Número da turma</param>
        /// <param name="numeroDivisaoComponente">Número da divisão do componente</param>
        /// <param name="numeroGrupo">Número do grupo da divisão da turma</param>
        /// <returns>
        ///  A codificação de uma turma deverá ser exibida sempre com a seguinte concatenação de dados:
        ///  [Código da Turma] + "." + [Número da Turma] + "." + [Número da Divisão da Configuração do
        ///  Componente] + "." + [Número do Grupo].*/
        /// </returns>
        public static string FormatarCodigoDivisaoTurma(int codigoTurma, short numeroTurma, int numeroDivisaoComponente, int numeroGrupo)
        {
            /*RN_TUR_023 - Exibição Codificação de turma
            A codificação de uma turma deverá ser exibida sempre com a seguinte concatenação de dados:
            [Código da Turma] + "." + [Número da Turma] + "." + [Número da Divisão da Configuração do
            Componente] + "." + [Número do Grupo].*/
            return $"{codigoTurma}.{numeroTurma}.{numeroDivisaoComponente}.{numeroGrupo:d3}";
        }

        /// <summary>
		/// Buscar divisões de turma por aluno do ciclo atual
		/// </summary>
		/// <param name="seqAluno">Sequencial do aluno.</param>
		public List<DivisaoTurmaVO> BuscarDivisoesTurmaCicloAtualAluno(long seqAluno)
        {
            List<long> seqsDivisaoTurma = new List<long>();
            var seqAlunoCicloLetivo = AlunoDomainService.BuscarCicloLetivoAtual(seqAluno);
            var alunoSpec = new SMCSeqSpecification<Aluno>(seqAluno);
            var items = AlunoDomainService.SearchProjectionByKey(alunoSpec, x =>
                                x.Historicos.FirstOrDefault(f => f.Atual)
                                    .HistoricosCicloLetivo.FirstOrDefault(c => c.SeqCicloLetivo == seqAlunoCicloLetivo)
                                        .PlanosEstudo.FirstOrDefault(f => f.Atual)
                                        .Itens.Select(s => s.DivisaoTurma)).ToList();

            return items.TransformList<DivisaoTurmaVO>();
        }

        /// <summary>
        /// Retorna a descrição da turma com a seguinte concatenação
        ///   [Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma].
        /// </summary>
        /// <param name="seqDivisaoTurma"></param>
        /// <returns>[Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma]</returns>
        public string BuscarDescricaoTurmaConcatenadoPorDivisaoTurma(long seqDivisaoTurma)
        {
            return SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seqDivisaoTurma), a => new SMCDatasourceItem()
            {
                Descricao = a.Turma.Codigo + "." + a.Turma.Numero + "-" + a.Turma.ConfiguracoesComponente.Where(b => b.Principal).FirstOrDefault().Descricao
            })?.Descricao ?? string.Empty;
        }

        /// <summary>
        /// Buscar divisões de turma do aluno participa na turma
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Lista de divisões de turma que o aluno participa na turma</returns>
        public List<DivisaoTurmaVO> BuscarDivisoesTurmaPorAlunoParticipaTurma(long seqAluno, long seqTurma)
        {
            List<DivisaoTurma> retorno = new List<DivisaoTurma>();

            PlanoEstudoItemFilterSpecification spec = new PlanoEstudoItemFilterSpecification()
            {
                SeqAluno = seqAluno,
                SeqTurma = seqTurma,
                PlanoEstudoAtual = true
            };

            retorno = PlanoEstudoItemDomainService.SearchBySpecification(spec, i => i.DivisaoTurma.OrigemAvaliacao)
                                                  .Select(s => s.DivisaoTurma).ToList();

            return retorno.TransformList<DivisaoTurmaVO>();
        }

        /// <summary>
        ///  Buscar a divisões de turma para a grade horaria compartilhada
        /// </summary>
        /// <param name="seqTurma">Sequencial de turma</param>
        /// <returns>Divisões filtradas para turma</returns>
        public List<SMCDatasourceItem> BuscarDivisoesPorTurmaParaGradeCompartilhadaSelect(long seqTurma, List<long> seqsDivisaoTurmaSelecionadas = null)
        {
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            DivisaoTurmaFilterSpecification spec = new DivisaoTurmaFilterSpecification() { SeqTurma = seqTurma };

            seqsDivisaoTurmaSelecionadas = seqsDivisaoTurmaSelecionadas ?? new List<long>();

            var divisoes = SearchProjectionBySpecification(spec, p => new
            {
                p.Seq,
                p.DivisaoComponente.CargaHorariaGrade,
            }).ToList();

            divisoes.ForEach(divisao =>
            {
                EventoAulaFilterSpecification specEvento = new EventoAulaFilterSpecification() { SeqDivisaoTurma = divisao.Seq };
                var eventos = EventoAulaDomainService.SearchProjectionBySpecification(specEvento, p => new
                {
                    Professores = p.Colaboradores,
                    Local = p.Local
                }).ToList();

                /*NV06 - Serão listadas todas as divisões cadastradas para a turma selecionada cujo componente associado possui
                         carga horária de grade maior que zero.
                       - Serão listados somente as divisões da turma que não possuam evento de aula cadastrado para o ciclo letivo do
                         compartilhamento ou que tenha evento de aula cadastrado porém não poderá ter nenhum professor ou local
                         associado.*/
                if (divisao.CargaHorariaGrade > 0 || seqsDivisaoTurmaSelecionadas.Contains(divisao.Seq))
                {
                    if (seqsDivisaoTurmaSelecionadas.Contains(divisao.Seq) || !eventos.SMCAny() || eventos.All(a => !a.Professores.SMCAny() && string.IsNullOrEmpty(a.Local)))
                    {
                        SMCDatasourceItem item = new SMCDatasourceItem();
                        item.Seq = divisao.Seq;

                        item.Descricao = MontarDescricaoDivisaoTurmaRegraGRD020(divisao.Seq);
                        retorno.Add(item);
                    }
                }
            });

            return retorno;
        }

        /// <summary>
        /// Montar descrição da divisão de truma conforme a regra RN_GRD_020 - Exibe descrição divisão da turma
        /// </summary>
        /// <param name="seqDivisaoTurma"></param>
        /// <param name="quebrarLinha"></param>
        /// <returns></returns>
        public virtual string MontarDescricaoDivisaoTurmaRegraGRD020(long seqDivisaoTurma, bool quebrarLinha = false)
        {
            var dadosDescricao = this.SearchProjectionByKey(seqDivisaoTurma, p => new
            {
                CodigoTurma = p.Turma.Codigo,
                NumeroTurma = p.Turma.Numero,
                NumeroDivisaoComponente = p.DivisaoComponente.Numero,
                NumeroGrupoDivisicao = p.NumeroGrupo,
            });

            string GrupoFormatado = $"{dadosDescricao.CodigoTurma}.{dadosDescricao.NumeroTurma}.{dadosDescricao.NumeroDivisaoComponente}.{dadosDescricao.NumeroGrupoDivisicao:d3}";

            string retorno = $"{GrupoFormatado} - {BuscarDivisaoTurmaCabecalho(seqDivisaoTurma, quebraLinha: quebrarLinha).TurmaDescricaoFormatado}";

            return retorno;
        }

        /// <summary>
        /// Buscar se pelo menos uma divisão tem obrigatoriedade de matéria lecionada obrigatória
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns></returns>
        public bool MateriaLecionadaObrigatoria(long seqTurma)
        {
            var spec = new DivisaoTurmaFilterSpecification() { SeqTurma = seqTurma };

            var divisaoTurma = SearchProjectionBySpecification(spec, a => a.OrigemAvaliacao.MateriaLecionadaObrigatoria).ToList();

            var result = !divisaoTurma.SMCAny() ? divisaoTurma.FirstOrDefault(f => f.Value == true) : false;
            return result != null && result.Value;
        }

        public List<string> DescricoesDivisaoTurma(long seq)
        {
            var retorno = new List<string>();
            var spec = new DivisaoTurmaFilterSpecification() { SeqTurma = seq };

            var divisoesTurma = this.SearchProjectionBySpecification(spec, s => new
            {
                s.Seq,
                OrigemAvaliacao = new OrigemAvaliacaoVO()
                {
                    Seq = s.OrigemAvaliacao.Seq,
                    MateriaLecionadaObrigatoria = s.OrigemAvaliacao.MateriaLecionadaObrigatoria,
                    MateriaLecionada = s.OrigemAvaliacao.MateriaLecionada
                }
            }).ToList();

            var seqsDivisaoTurma = divisoesTurma.Where(w => w.OrigemAvaliacao.MateriaLecionadaObrigatoria == true &&
                                                            string.IsNullOrEmpty(w.OrigemAvaliacao.MateriaLecionada?.Trim())).ToList();

            foreach (var item in seqsDivisaoTurma)
            {
                var divisaoTurma = BuscarDivisaoTurmaCabecalho(item.Seq, quebraLinha: false);
                retorno.Add($"{divisaoTurma.GrupoFormatado} - {divisaoTurma.TurmaDescricaoFormatado}");
            }
            return retorno;
        }
    }
}