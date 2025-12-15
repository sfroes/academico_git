using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.CUR.Exceptions.MatrizCurricularOferta;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Areas.TUR.Exceptions;
using SMC.Academico.Common.Areas.TUR.Exceptions.Turma;
using SMC.Academico.Common.Areas.TUR.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.GRD.Specifications;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Resources;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Caching;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.SGA.Administrativo.Areas.TUR.ValueObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SMC.Academico.Domain.Areas.TUR.DomainServices
{
    public class TurmaDomainService : AcademicoContextDomain<Turma>
    {
        #region Services

        private IAgendaService AgendaService
        {
            get { return this.Create<IAgendaService>(); }
        }

        private IEventoService EventoService
        {
            get { return this.Create<IEventoService>(); }
        }

        #endregion Services

        #region [ DomainService ]

        private DivisaoComponenteDomainService DivisaoComponenteDomainService => Create<DivisaoComponenteDomainService>();

        private CriterioAprovacaoDomainService CriterioAprovacaoDomainService => Create<CriterioAprovacaoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private TurmaHistoricoSituacaoDomainService TurmaHistoricoSituacaoDomainService => Create<TurmaHistoricoSituacaoDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();

        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService => Create<DivisaoMatrizCurricularComponenteDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private RequisitoDomainService RequisitoDomainService => Create<RequisitoDomainService>();

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();

        private TurmaConfiguracaoComponenteDomainService TurmaConfiguracaoComponenteDomainService => Create<TurmaConfiguracaoComponenteDomainService>();

        private TurmaHistoricoFechamentoDiarioDomainService TurmaHistoricoFechamentoDiarioDomainService => Create<TurmaHistoricoFechamentoDiarioDomainService>();

        private MensagemDomainService MensagemDomainService => Create<MensagemDomainService>();

        private RestricaoTurmaMatrizDomainService RestricaoTurmaMatrizDomainService => Create<RestricaoTurmaMatrizDomainService>();

        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();

        private SolicitacaoMatriculaItemHistoricoSituacaoDomainService SolicitacaoMatriculaItemHistoricoSituacaoDomainService => Create<SolicitacaoMatriculaItemHistoricoSituacaoDomainService>();

        private SituacaoItemMatriculaDomainService SituacaoItemMatriculaDomainService => Create<SituacaoItemMatriculaDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        private AulaDomainService AulaDomainService => Create<AulaDomainService>();

        private ComponenteCurricularDomainService ComponenteCurricularDomainService => Create<ComponenteCurricularDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();

        private EventoAulaColaboradorDomainService EventoAulaColaboradorDomainService => Create<EventoAulaColaboradorDomainService>();

        private HistoricoDivisaoTurmaConfiguracaoGradeDomainService HistoricoDivisaoTurmaConfiguracaoGradeDomainService => Create<HistoricoDivisaoTurmaConfiguracaoGradeDomainService>();

        private MatrizCurricularDivisaoComponenteDomainService MatrizCurricularDivisaoComponenteDomainService => Create<MatrizCurricularDivisaoComponenteDomainService>();

        private InstituicaoNivelCalendarioDomainService InstituicaoNivelCalendarioDomainService => Create<InstituicaoNivelCalendarioDomainService>();

        private ApuracaoAvaliacaoDomainService ApuracaoAvaliacaoDomainService => Create<ApuracaoAvaliacaoDomainService>();

        private EscalaApuracaoDomainService EscalaApuracaoDomainService => Create<EscalaApuracaoDomainService>();

        private AvaliacaoDomainService AvaliacaoDomainService => Create<AvaliacaoDomainService>();

        private SolicitacaoDispensaDomainService SolicitacaoDispensaDomainService => Create<SolicitacaoDispensaDomainService>();
        private TurmaColaboradorDomainService TurmaColaboradorDomainService => Create<TurmaColaboradorDomainService>();

        #endregion [ DomainService ]

        #region [ Queries ]

        #region [ _buscarSequenciaisTurmaGrupoProgramaSolicitacaoServico ]

        private string _buscarSequenciaisTurmaGrupoProgramaSolicitacaoServico =
                        @"  declare
	                               @SEQ_SOLICITACAO_SERVICO bigint,
	                               @SEQ_GRUPO_PROGRAMA bigint,
                                   @SEQ_CURSO_OFERTA_LOCALIDADE_TURNO bigint,
                                   @SEQ_LOCALIDADE bigint,
                                   @NAO_VALIDAR_LOCALIDADE bit

                               set @SEQ_GRUPO_PROGRAMA = {0}
                               set @SEQ_SOLICITACAO_SERVICO = {1}
                               set @SEQ_CURSO_OFERTA_LOCALIDADE_TURNO = {2}
                               set @NAO_VALIDAR_LOCALIDADE = 0

                               if(@SEQ_GRUPO_PROGRAMA = 0)
	                               select	@SEQ_GRUPO_PROGRAMA = seq_entidade_responsavel
	                               from	SRC.solicitacao_servico
	                               where	seq_solicitacao_servico = @SEQ_SOLICITACAO_SERVICO;

                               if(@SEQ_CURSO_OFERTA_LOCALIDADE_TURNO = 0)
								   set @NAO_VALIDAR_LOCALIDADE = 1
                               else
								   select @SEQ_LOCALIDADE = hei.seq_hierarquia_entidade_item_superior
								   from CSO.curso_oferta_localidade_turno colt
								   join CSO.curso_oferta_localidade col on col.seq_entidade = colt.seq_entidade_curso_oferta_localidade
								   join ORG.hierarquia_entidade_item hei on hei.seq_entidade = col.seq_entidade
								   where seq_curso_oferta_localidade_turno = @SEQ_CURSO_OFERTA_LOCALIDADE_TURNO
								   and seq_hierarquia_entidade = 3

                           select distinct t.seq_turma SeqTurma
                             from ORG.fn_buscar_entidade_filha_hierarquia(@SEQ_GRUPO_PROGRAMA, 1, 2) ec
                             join CSO.curso_oferta co
		                          on ec.seq_entidade = co.seq_entidade_curso
		                          and co.ind_ativo = 1
                             join cur.curriculo_curso_oferta cco
		                          on co.seq_curso_oferta = cco.seq_curso_oferta
                             join cur.matriz_curricular mc
		                          on cco.seq_curriculo_curso_oferta = mc.seq_curriculo_curso_oferta
                             join CUR.matriz_curricular_oferta mco
		                          on mc.seq_matriz_curricular = mco.seq_matriz_curricular
                             join CSO.curso_oferta_localidade_turno colt
								  on colt.seq_curso_oferta_localidade_turno = mco.seq_curso_oferta_localidade_turno
							 join CSO.curso_oferta_localidade col
								  on col.seq_entidade = colt.seq_entidade_curso_oferta_localidade
						     join ORG.hierarquia_entidade_item hei
								  on hei.seq_entidade = col.seq_entidade
								  and ( @NAO_VALIDAR_LOCALIDADE = 1 OR hei.seq_hierarquia_entidade_item_superior = @SEQ_LOCALIDADE)
                             join CUR.divisao_matriz_curricular_componente dmcc
		                          on mc.seq_matriz_curricular = dmcc.seq_matriz_curricular
                             join CUR.configuracao_componente cc
		                          on dmcc.seq_configuracao_componente = cc.seq_configuracao_componente
                             join CUR.componente_curricular cpc
		                          on cc.seq_componente_curricular = cpc.seq_componente_curricular
                             join CUR.curriculo_curso_oferta_grupo ccog
		                          on cco.seq_curriculo_curso_oferta = ccog.seq_curriculo_curso_oferta
                             join CUR.grupo_curricular gc
		                          on ccog.seq_grupo_curricular = gc.seq_grupo_curricular
                             join CUR.grupo_curricular_componente gcc
		                          on gc.seq_grupo_curricular = gcc.seq_grupo_curricular
		                          and cpc.seq_componente_curricular = gcc.seq_componente_curricular
                             join TUR.restricao_turma_matriz rtm
		                          on mco.seq_matriz_curricular_oferta = rtm.seq_matriz_curricular_oferta
                             join tur.turma_configuracao_componente tcc
		                          on rtm.seq_turma_configuracao_componente = tcc.seq_turma_configuracao_componente
		                          and dmcc.seq_configuracao_componente = tcc.seq_configuracao_componente
                             join tur.turma t
		                          on tcc.seq_turma = t.seq_turma
                             join SRC.processo p
		                          on t.seq_ciclo_letivo_inicio = p.seq_ciclo_letivo
                             join SRC.configuracao_processo cp
		                          on p.seq_processo = cp.seq_processo
                             join SRC.solicitacao_servico ss
		                          on cp.seq_configuracao_processo = ss.seq_configuracao_processo
		                          and ss.seq_solicitacao_servico = @SEQ_SOLICITACAO_SERVICO
                             order by  t.seq_turma";

        #endregion [ _buscarSequenciaisTurmaGrupoProgramaSolicitacaoServico ]

        #region [ _buscarSequenciaisTurmaMatrizOfertaLegenda ]

        private string _buscarSequenciaisTurmaMatrizOfertaLegenda =
                        @"declare
                            @SEQ_MATRIZ_CURRICULAR_OFERTA bigint,
                            @SEQ_SOLICITACAO_SERVICO bigint

                        set @SEQ_MATRIZ_CURRICULAR_OFERTA = {0}
                        set @SEQ_SOLICITACAO_SERVICO = {1}

                        select distinct t.seq_turma SeqTurma
                        from CUR.matriz_curricular_oferta mco
                        join CUR.matriz_curricular mc
	                        on mco.seq_matriz_curricular = mc.seq_matriz_curricular
                        join CUR.curriculo_curso_oferta cco
	                        on mc.seq_curriculo_curso_oferta = cco.seq_curriculo_curso_oferta
                        join CUR.divisao_matriz_curricular_componente dmcc
	                        on mc.seq_matriz_curricular = dmcc.seq_matriz_curricular
                        join CUR.configuracao_componente cc
	                        on dmcc.seq_configuracao_componente = cc.seq_configuracao_componente
                        join CUR.componente_curricular cpc
	                        on cc.seq_componente_curricular = cpc.seq_componente_curricular
                        join CUR.curriculo_curso_oferta_grupo ccog
	                        on cco.seq_curriculo_curso_oferta = ccog.seq_curriculo_curso_oferta
                        join CUR.grupo_curricular gc
	                        on ccog.seq_grupo_curricular = gc.seq_grupo_curricular
                        join CUR.grupo_curricular_componente gcc
	                        on gc.seq_grupo_curricular = gcc.seq_grupo_curricular
	                        and cpc.seq_componente_curricular = gcc.seq_componente_curricular
                        join TUR.restricao_turma_matriz rtm
	                        on mco.seq_matriz_curricular_oferta = rtm.seq_matriz_curricular_oferta
                        join tur.turma_configuracao_componente tcc
	                        on rtm.seq_turma_configuracao_componente = tcc.seq_turma_configuracao_componente
	                        and dmcc.seq_configuracao_componente = tcc.seq_configuracao_componente
                        join tur.turma t
	                        on tcc.seq_turma = t.seq_turma
                        join SRC.processo p
	                        on t.seq_ciclo_letivo_inicio = p.seq_ciclo_letivo
                        join SRC.configuracao_processo cp
	                        on p.seq_processo = cp.seq_processo
                        join SRC.solicitacao_servico ss
	                        on cp.seq_configuracao_processo = ss.seq_configuracao_processo
	                        and ss.seq_solicitacao_servico = @SEQ_SOLICITACAO_SERVICO
                        -- beneficio
                        left join	CUR.grupo_curricular_beneficio gcb
			                        on gc.seq_grupo_curricular = gcb.seq_grupo_curricular
                        left join	FIN.pessoa_atuacao_beneficio pab
			                        on gcb.seq_beneficio = pab.seq_beneficio
			                        and pab.seq_pessoa_atuacao = ss.seq_pessoa_atuacao
                        left join	FIN.beneficio_historico_vigencia bhv
			                        on pab.seq_pessoa_atuacao_beneficio = bhv.seq_pessoa_atuacao_beneficio
			                        and bhv.ind_atual = 1
			                        and bhv.dat_fim_vigencia >=
				                        case
					                        when exists (select 1 from ALN.ingressante where seq_pessoa_atuacao = ss.seq_pessoa_atuacao)
					                        then (select dat_admissao from ALN.ingressante where seq_pessoa_atuacao = ss.seq_pessoa_atuacao)

					                        when exists (select 1 from ALN.aluno where seq_pessoa_atuacao = ss.seq_pessoa_atuacao)
					                        then (select dat_admissao from ALN.aluno_historico ah1 where ah1.seq_atuacao_aluno = ss.seq_pessoa_atuacao and ah1.ind_atual = 1)
				                        end
                        left join	FIN.beneficio_historico_situacao bh
			                        on pab.seq_pessoa_atuacao_beneficio = bh.seq_pessoa_atuacao_beneficio
			                        and bh.idt_dom_situacao_chancela_beneficio = 3 -- Beneficio está deferido
			                        and	bh.ind_atual = 1
                        -- condição de obrigatoriedade
                        left join 	CUR.grupo_curricular_condicao_obrigatoriedade gcco
		                        on gc.seq_grupo_curricular = gcco.seq_grupo_curricular
                        left join	PES.pessoa_atuacao_condicao_obrigatoriedade paco
			                        on gcco.seq_condicao_obrigatoriedade = paco.seq_condicao_obrigatoriedade
			                        and paco.seq_pessoa_atuacao = ss.seq_pessoa_atuacao
			                        and paco.ind_ativo = 1
                        -- formação específica
                        left join	aln.ingressante_formacao_especifica ife
			                        on ife.seq_formacao_especifica in (select seq_formacao_especifica from CSO.fn_hierarquia_formacao_especifica(gc.seq_formacao_especifica))
			                        and ife.seq_atuacao_ingressante = ss.seq_pessoa_atuacao
                        left join	ALN.aluno_historico ah
			                        on  ah.ind_atual = 1
			                        and ah.seq_atuacao_aluno = ss.seq_pessoa_atuacao
                        left join	ALN.aluno_formacao af
			                        on af.seq_formacao_especifica in (select seq_formacao_especifica from CSO.fn_hierarquia_formacao_especifica(gc.seq_formacao_especifica))
			                        and af.seq_aluno_historico = ah.seq_aluno_historico
                        where	mco.seq_matriz_curricular_oferta = @SEQ_MATRIZ_CURRICULAR_OFERTA
                        and		(gcb.seq_grupo_curricular is null or pab.seq_pessoa_atuacao is not null)
                        and		(gcco.seq_grupo_curricular is null or paco.seq_pessoa_atuacao is not null)
                        and		(gc.seq_formacao_especifica is null or ife.seq_atuacao_ingressante is not null or af.seq_aluno_formacao is not null)
                        order by t.seq_turma";

        #endregion [ _buscarSequenciaisTurmaMatrizOfertaLegenda ]

        #region [ _buscarSequenciaisTurmaMatrizOfertaIngressante ]

        private string _buscarSequenciaisTurmaMatrizOfertaIngressante =
                        @"select distinct dt.seq_turma as SeqTurma
                             from	mat.solicitacao_matricula sm
                             join	mat.solicitacao_matricula_item smi
		                            on sm.seq_solicitacao_servico = smi.seq_solicitacao_matricula
                             join	tur.divisao_turma dt
		                            on smi.seq_divisao_turma = dt.seq_divisao_turma
                            where sm.seq_solicitacao_servico = {0}
						    and  dt.seq_turma is not null";

        #endregion [ _buscarSequenciaisTurmaMatrizOfertaIngressante ]

        #region [ _buscarDiarioTurmaCabecalho ]

        private string _buscarDiarioTurmaCabecalho =
                                        @"
                                        declare @SEQ_TURMA bigint
                                        set @SEQ_TURMA = {0}

                                        -- query para o cabeçalho
                                        select distinct
	                                        ecol.nom_entidade as NomeCursoOfertaLocalidade,
	                                        tu.dsc_turno as DescricaoTurno,
	                                        cast(t.cod_turma as varchar) + '.' + cast(t.num_turma as varchar) as CodigoTurma,
	                                        tcc.dsc_turma_configuracao_componente as DescricaoTurmaConfiguracaoComponente,
	                                        case
		                                        when t.seq_ciclo_letivo_inicio <> t.seq_ciclo_letivo_fim then
			                                        cast(cli.num_ciclo_letivo as varchar) + 'º/' + cast(cli.ano_ciclo_letivo as varchar) + ' - ' + cast(clf.num_ciclo_letivo as varchar) + 'º/' + cast(clf.ano_ciclo_letivo as varchar)
		                                        else
			                                        cast(clf.num_ciclo_letivo as varchar) + 'º/' + cast(clf.ano_ciclo_letivo as varchar)
	                                        end  as DescricaoCicloLetivo,
	                                        h.ind_diario_fechado as IndicadorDiarioFechado,
	                                        case
		                                        when h.ind_diario_fechado = 1 then h.dat_inclusao
		                                        else null
	                                        end as DataFechamentoDiario
                                        from	TUR.turma t
                                        join	TUR.turma_historico_fechamento_diario h
		                                        on t.seq_turma = h.seq_turma
		                                        and h.dat_inclusao = (select MAX(dat_inclusao) from TUR.turma_historico_fechamento_diario where seq_turma = h.seq_turma)
                                        join	TUR.turma_configuracao_componente tcc
		                                        on t.seq_turma = tcc.seq_turma
                                        join	TUR.restricao_turma_matriz rtm
		                                        on tcc.seq_turma_configuracao_componente = rtm.seq_turma_configuracao_componente
                                        join	CSO.curso_oferta_localidade_turno colt
		                                        on rtm.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
                                        join	ORG.entidade ecol
		                                        on colt.seq_entidade_curso_oferta_localidade = ecol.seq_entidade
                                        join	CSO.turno tu
		                                        on colt.seq_turno = tu.seq_turno
                                        join	CAM.ciclo_letivo cli
		                                        on t.seq_ciclo_letivo_inicio = cli.seq_ciclo_letivo
                                        join	CAM.ciclo_letivo clf
		                                        on t.seq_ciclo_letivo_fim = clf.seq_ciclo_letivo
                                        where	t.seq_turma = @SEQ_TURMA";

        #endregion [ _buscarDiarioTurmaCabecalho ]

        #region [ _buscarDiarioTurmaProfessor ]

        private string _buscarDiarioTurmaProfessor = @"select	distinct
	                                                        pa.seq_pessoa_atuacao as SeqColaborador,
	                                                        isnull(pdp.nom_social, pdp.nom_pessoa) as NomeProfessor,
	                                                        case
		                                                        when tc.seq_atuacao_colaborador = c.seq_pessoa_atuacao then cast(1 as bit)
		                                                        else cast(0 as bit)
	                                                        end as ProfessorTurma
                                                        from	TUR.turma t
                                                        left join	TUR.turma_colaborador tc
			                                                        on t.seq_turma = tc.seq_turma
                                                        left join	TUR.divisao_turma dt
			                                                        on t.seq_turma = dt.seq_turma
                                                        left join	TUR.divisao_turma_colaborador dtc
			                                                        on dt.seq_divisao_turma = dtc.seq_divisao_turma
                                                        join	DCT.colaborador c
		                                                        on (dtc.seq_atuacao_colaborador = c.seq_pessoa_atuacao
		                                                        or tc.seq_atuacao_colaborador = c.seq_pessoa_atuacao)
                                                        join	PES.pessoa_atuacao pa
		                                                        on c.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                                                        join	PES.pessoa_dados_pessoais pdp
		                                                        on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
                                                        where	t.seq_turma = {0}";

        //private string _buscarDiarioTurmaProfessor = @"
        //                        declare @SEQ_TURMA bigint
        //                        set @SEQ_TURMA = {0}

        //                        select	distinct
        //                            pa.seq_pessoa_atuacao as SeqColaborador,
        //                         isnull(pdp.nom_social, pdp.nom_pessoa) as NomeProfessor
        //                        from	TUR.turma t
        //                        join	TUR.divisao_turma dt
        //                          on t.seq_turma = dt.seq_turma
        //                        join	TUR.divisao_turma_colaborador dtc
        //                          on dt.seq_divisao_turma = dtc.seq_divisao_turma
        //                        join	DCT.colaborador c
        //                          on dtc.seq_atuacao_colaborador = c.seq_pessoa_atuacao
        //                        join	PES.pessoa_atuacao pa
        //                          on c.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
        //                        join	PES.pessoa_dados_pessoais pdp
        //                          on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
        //                        where	t.seq_turma = @SEQ_TURMA";

        #endregion [ _buscarDiarioTurmaProfessor ]

        #region [ _buscarDiarioTurmaAluno ]

        private string _buscarDiarioTurmaAluno => @"
declare
	@SEQ_TURMA bigint = {0},
	@SEQ_DIVISAO_TURMA bigint = {1},
    @SEQ_ALUNO bigint = {2}

select	distinct
    --Não apague estas duas linhas abaixo!!!
	isnull(he.seq_historico_escolar, 0) as Seq,
    ah.seq_atuacao_aluno as SeqPessoaAtuacao,
    --Não apague estas duas linhas acima!!!
    ah.seq_aluno_historico as SeqAlunoHistorico,
    ahcl.seq_aluno_historico_ciclo_letivo as SeqAlunoHistoricoCicloLetivo,
	isnull(ah.seq_curso_oferta_localidade_turno, 0) as SeqCursoOfertaLocalidadeTurno,
	case
		when ah.seq_curso_oferta_localidade_turno is null then 'Disciplina Isolada'
		else CONCAT(ecol.nom_entidade, ' - ', tn.dsc_turno)
	end as DescricaoCursoOfertaLocalidadeTurno,
	a.num_registro_academico as NumeroRegistroAcademico,
	upper(isnull(pdp.nom_social, pdp.nom_pessoa)) as NomeAluno,
	pe.seq_plano_estudo as SeqPlanoEstudo,
	he.num_nota as Nota,
	eai.dsc_escala_apuracao_item as DescricaoEscalaApuracaoItem,
    eai.ind_aprovado as Aprovado,
	he.num_faltas as Faltas,
	case
		when @SEQ_DIVISAO_TURMA is not null then
		(
			select	cast(isnull(sum(fq.num_falta), 0) as smallint) + cast(isnull(count(fqg.seq_apuracao_frequencia_grade), 0) as smallint)
			from	tur.divisao_turma dt
			left join	APR.aula au
						on dt.seq_divisao_turma = au.seq_divisao_turma
			left join	APR.apuracao_frequencia fq
						on au.seq_aula = fq.seq_aula
						and fq.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
			left join	grd.evento_aula ev
						on dt.seq_divisao_turma = ev.seq_divisao_turma
			left join	apr.apuracao_frequencia_grade fqg
						on ev.seq_evento_aula = fqg.seq_evento_aula
						and fqg.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
						and	fqg.idt_dom_frequencia = 2 -- Ausente
			where	dt.seq_divisao_turma = isnull(@SEQ_DIVISAO_TURMA, pei.seq_divisao_turma)
		)
		else
		(
			select	cast(isnull(sum(fq.num_falta), 0) as smallint) + cast(isnull(count(fqg.seq_apuracao_frequencia_grade), 0) as smallint)
			from	tur.divisao_turma dt
			left join	APR.aula au
						on dt.seq_divisao_turma = au.seq_divisao_turma
			left join	APR.apuracao_frequencia fq
						on au.seq_aula = fq.seq_aula
						and fq.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
			left join	grd.evento_aula ev
						on dt.seq_divisao_turma = ev.seq_divisao_turma
			left join	apr.apuracao_frequencia_grade fqg
						on ev.seq_evento_aula = fqg.seq_evento_aula
						and fqg.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
						and	fqg.idt_dom_frequencia = 2 -- Ausente
			where	dt.seq_turma = @SEQ_TURMA
		)
	end as SomaFaltasApuracao,
	d.dsc_dominio as DescricaoSituacaoHistoricoEscolar,
	ca.ind_apuracao_frequencia as IndicadorApuracaoFrequencia,
	ca.ind_apuracao_nota as IndicadorApuracaoNota,
    cc.ind_permite_aluno_sem_nota as IndicadorPermiteAlunoSemNota,
	ca.idt_dom_tipo_arredondamento as TipoArredondamento,
	ca.seq_escala_apuracao as SeqEscalaApuracao,
	ca.num_nota_maxima as NotaMaxima,
	ca.num_percentual_frequencia_aprovado as PercentualMinimoFrequencia,
	ca.num_percentual_nota_aprovado as PercentualMinimoNota,
	he.seq_historico_escolar as SeqHistoricoEscolar,
    he.seq_escala_apuracao_item as SeqEscalaApuracaoItem,
	com.seq_componente_curricular as SeqComponenteCurricular,
	ISNULL((	select	seq_componente_curricular_assunto
				from	TUR.restricao_turma_matriz r
				where	tcc.seq_turma_configuracao_componente = r.seq_turma_configuracao_componente
				and		r.seq_matriz_curricular_oferta = pe.seq_matriz_curricular_oferta),
			(	select 	top 1	seq_componente_curricular_assunto
				from	TUR.restricao_turma_matriz r2
				join	TUR.turma_configuracao_componente tcc2
						on r2.seq_turma_configuracao_componente = tcc2.seq_turma_configuracao_componente
						and tcc2.ind_principal = 1
						and	r2.ind_oferta_matriz_principal = 1
						and tcc2.seq_turma = t.seq_turma)) as SeqComponenteCurricularAssunto,
	com.qtd_carga_horaria as CargaHoraria,
    com.qtd_credito as Credito,
	CAST(
		isnull ((	select	top 1
					case
						when ccog.ind_obrigatorio = 1 then 0
						else 1
					end as ind_optativa
					from	cur.curriculo_curso_oferta_grupo ccog
					join	cur.grupo_curricular gc
							on ccog.seq_grupo_curricular = gc.seq_grupo_curricular
					join	cur.grupo_curricular_componente gcc
							on gc.seq_grupo_curricular = gcc.seq_grupo_curricular
							and gcc.seq_componente_curricular = com.seq_componente_curricular
					join	cur.curriculo_curso_oferta cco
							on ccog.seq_curriculo_curso_oferta = cco.seq_curriculo_curso_oferta
					join	cur.matriz_curricular mc
							on cco.seq_curriculo_curso_oferta = mc.seq_curriculo_curso_oferta
					join	cur.matriz_curricular_oferta mco
							on mc.seq_matriz_curricular = mco.seq_matriz_curricular
							and mco.seq_matriz_curricular_oferta = pe.seq_matriz_curricular_oferta), 1)
						as bit) as Optativa,
	isnull((	select	top 1 tdc.ind_gera_orientacao
		        from	cur.divisao_componente dc
		        join	cur.tipo_divisao_componente tdc
        				on dc.seq_tipo_divisao_componente = tdc.seq_tipo_divisao_componente
		        where	dc.seq_configuracao_componente = cc.seq_configuracao_componente ), 0) as TurmaOrientacao,
	case
		when ind_concede_formacao = 0 then cast(1 as bit)
		else cast(0 as bit)
	end as AlunoDI,
	case
		when sm.dsc_token = 'FORMADO' then cast(1 as bit)
		else cast(0 as bit)
	end as AlunoFormado,
	he.qtd_carga_horaria_realizada as CargaHorariaRealizada,
	mco.seq_matriz_curricular as SeqMatrizCurricular,
	case
		when @SEQ_DIVISAO_TURMA is not null then
		(
			select top 1 seq_divisao_componente
			from tur.divisao_turma
			where seq_divisao_turma = isnull(@SEQ_DIVISAO_TURMA, pei.seq_divisao_turma)
		)
		else NULL
	end as SeqDivisaoComponente,
    he.dsc_observacao as Observacao,
    case
		when @SEQ_DIVISAO_TURMA is not null then
			isnull((select dc.qtd_carga_horaria_grade
		    from	cur.divisao_componente dc
		    where	dc.seq_configuracao_componente = cc.seq_configuracao_componente and
			        dc.seq_divisao_componente = dt.seq_divisao_componente), 0)
		else
			isnull((select sum(dc.qtd_carga_horaria_grade) 
		    from	cur.divisao_componente dc
		    where	dc.seq_configuracao_componente = cc.seq_configuracao_componente ), 0)
	end as CargaHorariaGrade,
	case
		when @SEQ_DIVISAO_TURMA is not null then
			isnull((select count(idt_dom_situacao_apuracao_frequencia)
			from        tur.divisao_turma dt			
			left join	grd.evento_aula ev
						on dt.seq_divisao_turma = ev.seq_divisao_turma
						and idt_dom_situacao_apuracao_frequencia = 1 --executada
			where dt.seq_turma = @SEQ_TURMA and dt.seq_divisao_turma = isnull(@SEQ_DIVISAO_TURMA, pei.seq_divisao_turma)), 0)
		else
			isnull((select count(idt_dom_situacao_apuracao_frequencia)
			from        tur.divisao_turma dt			
			left join	grd.evento_aula ev
						on dt.seq_divisao_turma = ev.seq_divisao_turma
						and idt_dom_situacao_apuracao_frequencia = 1 --executada
			where dt.seq_turma = @SEQ_TURMA and dt.seq_divisao_turma  in (select seq_divisao_turma from aln.plano_estudo_item pei2 where pei2.seq_plano_estudo = pe.seq_plano_estudo)), 0) 
	end as CargaHorariaExecutada
from	TUR.turma t
join	TUR.divisao_turma dt
		on t.seq_turma = dt.seq_turma
join	ALN.plano_estudo_item pei
		on dt.seq_divisao_turma = pei.seq_divisao_turma
join	ALN.plano_estudo pe
		on pei.seq_plano_estudo = pe.seq_plano_estudo
		and pe.ind_atual = 1
left join	CUR.matriz_curricular_oferta mco
		on pe.seq_matriz_curricular_oferta = mco.seq_matriz_curricular_oferta
join	ALN.aluno_historico_ciclo_letivo ahcl
		on pe.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
		and ahcl.dat_exclusao is null
join	ALN.aluno_historico ah
		on ahcl.seq_aluno_historico = ah.seq_aluno_historico
left join	CSO.curso_oferta_localidade_turno colt
	    	on colt.seq_curso_oferta_localidade_turno = ah.seq_curso_oferta_localidade_turno
left join	CSO.curso_oferta_localidade col
	    	on col.seq_entidade = colt.seq_entidade_curso_oferta_localidade
left join	org.entidade ecol
	    	on ecol.seq_entidade = col.seq_entidade
left join	CSO.turno tn
	    	on tn.seq_turno = colt.seq_turno
join	ALN.aluno a
		on ah.seq_atuacao_aluno = a.seq_pessoa_atuacao
join	PES.pessoa_atuacao pa
		on a.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
join	PES.pessoa_dados_pessoais pdp
		on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
join	PES.pessoa p
		on pa.seq_pessoa = p.seq_pessoa
join	ALN.instituicao_nivel_tipo_vinculo_aluno initv
		on a.seq_tipo_vinculo_aluno = initv.seq_tipo_vinculo_aluno
join	ORG.instituicao_nivel ini
		on initv.seq_instituicao_nivel = ini.seq_instituicao_nivel
		and	ini.seq_nivel_ensino = ah.seq_nivel_ensino
		and	ini.seq_entidade_instituicao = p.seq_entidade_instituicao
join	APR.origem_avaliacao oa
		on t.seq_origem_avaliacao = oa.seq_origem_avaliacao
join	APR.criterio_aprovacao ca
		on oa.seq_criterio_aprovacao = ca.seq_criterio_aprovacao
join	TUR.turma_configuracao_componente tcc
		on t.seq_turma = tcc.seq_turma
		-- configuração de componente principal, caso não tenha restrição para a matriz do aluno em qualquer componente da turma
		and ((tcc.ind_principal = 1 and
		not exists (	select	1
						from	TUR.restricao_turma_matriz r1
						join	TUR.turma_configuracao_componente tcc1
								on r1.seq_turma_configuracao_componente = tcc1.seq_turma_configuracao_componente
								and tcc1.seq_turma = t.seq_turma
						where	r1.seq_matriz_curricular_oferta = pe.seq_matriz_curricular_oferta))
		-- ou configuração de componente da restrição da matriz do aluno
		or exists (	select	1
					from	TUR.restricao_turma_matriz r
					where	tcc.seq_turma_configuracao_componente = r.seq_turma_configuracao_componente
					and		r.seq_matriz_curricular_oferta = pe.seq_matriz_curricular_oferta))
join	CUR.configuracao_componente cc
		on tcc.seq_configuracao_componente = cc.seq_configuracao_componente
join	CUR.componente_curricular com
		on cc.seq_componente_curricular = com.seq_componente_curricular
left join	APR.historico_escolar he
			on ah.seq_aluno_historico = he.seq_aluno_historico
			and t.seq_origem_avaliacao = he.seq_origem_avaliacao
left join	APR.escala_apuracao_item eai
			on he.seq_escala_apuracao_item = eai.seq_escala_apuracao_item
left join	dominio d
			on he.idt_dom_situacao_historico_escolar = d.val_dominio
			and d.nom_dominio = 'situacao_historico_escolar'
-- join para buscar a ultima situação do aluno independente do ciclo letivo
join	aln.aluno_historico_ciclo_letivo ahcl2
		on ahcl2.seq_aluno_historico = ah.seq_aluno_historico
join	aln.aluno_historico_situacao ahs
		on ahcl2.seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
		and ahs.dat_exclusao is null
		and ahs.dat_inicio_situacao = (select	MAX(dat_inicio_situacao)
										from	ALN.aluno_historico_situacao ahs2
										join	aln.aluno_historico_ciclo_letivo ahcl3
												on ahs2.seq_aluno_historico_ciclo_letivo = ahcl3.seq_aluno_historico_ciclo_letivo
										where	ahcl3.seq_aluno_historico = ah.seq_aluno_historico
										and		ahs2.dat_exclusao is null
										and		ahs2.dat_inicio_situacao <= GETDATE())
join	mat.situacao_matricula sm
		on ahs.seq_situacao_matricula = sm.seq_situacao_matricula
where	t.seq_turma = @SEQ_TURMA
and		pei.seq_divisao_turma = isnull(@SEQ_DIVISAO_TURMA, pei.seq_divisao_turma)
and		a.seq_pessoa_atuacao = isnull(@SEQ_ALUNO, a.seq_pessoa_atuacao)
order by NomeAluno";

        #endregion [ _buscarDiarioTurmaAluno ]

        #region [ _buscarSequenciaisTurmaPorPrograma ]

        private string _buscarSequenciaisTurmaPorPrograma =
                        @"select distinct t.seq_turma as SeqTurma
                             from org.entidade er
                             join org.hierarquia_entidade_item hgp
		                          on hgp.seq_entidade = er.seq_entidade
                             join org.hierarquia_entidade_item hp
		                          on hgp.seq_hierarquia_entidade_item = hp.seq_hierarquia_entidade_item_superior
                             join org.hierarquia_entidade_item hc
		                          on hp.seq_hierarquia_entidade_item = hc.seq_hierarquia_entidade_item_superior
                             join org.entidade ec
		                          on ec.seq_entidade = hc.seq_entidade
                             join cso.curso_oferta co
		                          on ec.seq_entidade = co.seq_entidade_curso
                             join cur.curriculo_curso_oferta cco
		                          on co.seq_curso_oferta = cco.seq_curso_oferta
                             join cur.curriculo c
		                          on cco.seq_curriculo = c.seq_curriculo
                             join cur.matriz_curricular mc
		                          on cco.seq_curriculo_curso_oferta = mc.seq_curriculo_curso_oferta
                             join cur.matriz_curricular_oferta mco
		                          on mc.seq_matriz_curricular = mco.seq_matriz_curricular
                             join cur.divisao_matriz_curricular_componente dmcc
		                          on  mc.seq_matriz_curricular = dmcc.seq_matriz_curricular
                             join tur.restricao_turma_matriz rtm
		                          on mco.seq_matriz_curricular_oferta = rtm.seq_matriz_curricular_oferta
                             join tur.turma_configuracao_componente tcc
		                          on rtm.seq_turma_configuracao_componente = tcc.seq_turma_configuracao_componente
		                          and dmcc.seq_configuracao_componente = tcc.seq_configuracao_componente
                             join tur.turma t
		                          on tcc.seq_turma = t.seq_turma
                             where er.seq_entidade  = {0}
                             and t.seq_turma is not null";

        #endregion [ _buscarSequenciaisTurmaPorPrograma ]

        #region [ _buscarRelatorioTurmaPorCicloLetivo ]

        private string _buscarRelatorioTurmaPorCicloLetivo =
                        @"  SELECT   CO.seq_curso_oferta AS SeqCursoOferta,
                                     CO.dsc_curso_oferta AS DescricaoOferta,
                                     ECOL.nom_entidade AS DescricaoLocalidade,
                                     TU.dsc_turno AS DescricaoTurno,
                                     T.seq_turma AS SeqTurma,
                                     T.cod_turma AS CodigoTurma,
                                     T.num_turma AS NumeroTurma,
                                     TCC.dsc_turma_configuracao_componente AS DescricaoConfiguracaoTurma,
                                     TT.dsc_tipo_turma AS DescricaoTipoTurma,
                                     T.qtd_vagas AS QuantidadeVagas,
                                     (	SELECT SUM (DTQ.qtd_vagas_ocupadas)
	                                    FROM TUR.divisao_turma DTQ
	                                    WHERE DTQ.seq_turma = T.seq_turma
                                     ) AS QuantidadeVagasOcupadas,
                                     DC.num_divisao_componente AS DivisaoComponenteNumero,
                                     DC.qtd_carga_horaria AS DivisaoComponenteCargaHoraria,
                                     TDC.dsc_tipo_divisao_componente AS DivisaoComponenteDescricao,
                                     (	SELECT TOP 1 (INTCC.idt_dom_formato_carga_horaria)
	                                    FROM CUR.instituicao_nivel_tipo_divisao_componente INTDC
	                                    INNER JOIN CUR.instituicao_nivel_tipo_componente_curricular INTCC ON INTCC.seq_instituicao_nivel_tipo_componente_curricular = INTDC.seq_instituicao_nivel_tipo_componente_curricular
	                                    WHERE INTDC.seq_tipo_divisao_componente = TDC.seq_tipo_divisao_componente
                                     ) AS DivisaoComponenteFormato,
                                     DT.seq_divisao_turma AS SeqDivisaoTurma,
                                     DT.num_grupo AS DivisaoNumeroGrupo,
                                     DVTL.nom_entidade AS DivisaoLocalidade,
                                     DT.qtd_vagas AS DivisaoQuantidadeVagas,
                                     DT.dsc_informacoes_adicionais AS DivisaoInformacoes
                                    FROM TUR.turma T
                                    INNER JOIN TUR.turma_configuracao_componente TCC ON T.seq_turma = TCC.seq_turma
                                    LEFT JOIN TUR.restricao_turma_matriz RTM ON TCC.seq_turma_configuracao_componente = RTM.seq_turma_configuracao_componente
										                                     AND TCC.ind_principal = 'true'
                                    LEFT JOIN CSO.curso_oferta_localidade_turno COLT ON RTM.seq_curso_oferta_localidade_turno = COLT.seq_curso_oferta_localidade_turno
                                    LEFT JOIN CSO.curso_oferta_localidade COL ON COLT.seq_entidade_curso_oferta_localidade = COL.seq_entidade
                                    LEFT JOIN ORG.hierarquia_entidade_item ECOHEI ON COL.seq_entidade = ECOHEI.seq_entidade
                                    LEFT JOIN ORG.hierarquia_entidade_item ECOHEIS ON ECOHEI.seq_hierarquia_entidade_item_superior = ECOHEIS.seq_hierarquia_entidade_item
                                    LEFT JOIN ORG.entidade ECOL ON ECOHEIS.seq_entidade = ECOL.seq_entidade
                                    LEFT JOIN CSO.curso_oferta CO ON COL.seq_curso_oferta = CO.seq_curso_oferta
                                    LEFT JOIN CSO.turno TU ON COLT.seq_turno = TU.seq_turno
                                    INNER JOIN TUR.tipo_turma TT ON T.seq_tipo_turma = TT.seq_tipo_turma
                                    INNER JOIN TUR.divisao_turma DT ON T.seq_turma = DT.seq_turma
                                    INNER JOIN CUR.divisao_componente DC ON DT.seq_divisao_componente = DC.seq_divisao_componente
                                    INNER JOIN CUR.tipo_divisao_componente TDC ON DC.seq_tipo_divisao_componente = TDC.seq_tipo_divisao_componente
                                    LEFT JOIN ORG.entidade DVTL ON DT.seq_entidade_localidade = DVTL.seq_entidade
                                    WHERE T.seq_turma IN ({0})";

        #endregion [ _buscarRelatorioTurmaPorCicloLetivo ]

        #region [ _buscarRelatorioTurmaPorCicloLetivoComColaboradores ]

        private string _buscarRelatorioTurmaPorCicloLetivoComColaboradores =
                        @"
                            declare
	                                 @SEQ_NIVEL_ENSINO bigint

                                set  @SEQ_NIVEL_ENSINO = {0}

                            SELECT   CO.seq_curso_oferta AS SeqCursoOferta,
                                     CO.dsc_curso_oferta AS DescricaoOferta,
                                     ECOL.nom_entidade AS DescricaoLocalidade,
                                     TU.dsc_turno AS DescricaoTurno,
                                     T.seq_turma AS SeqTurma,
                                     T.cod_turma AS CodigoTurma,
                                     T.num_turma AS NumeroTurma,
                                     TCC.dsc_turma_configuracao_componente AS DescricaoConfiguracaoTurma,
                                     TT.dsc_tipo_turma AS DescricaoTipoTurma,
                                     T.qtd_vagas AS QuantidadeVagas,
                                     (	SELECT SUM (DTQ.qtd_vagas_ocupadas)
	                                    FROM TUR.divisao_turma DTQ
	                                    WHERE DTQ.seq_turma = T.seq_turma
                                     ) AS QuantidadeVagasOcupadas,
                                     DC.num_divisao_componente AS DivisaoComponenteNumero,
                                     DC.qtd_carga_horaria AS DivisaoComponenteCargaHoraria,
                                     TDC.dsc_tipo_divisao_componente AS DivisaoComponenteDescricao,
                                     (	SELECT TOP 1 (INTCC.idt_dom_formato_carga_horaria)
	                                    FROM CUR.instituicao_nivel_tipo_divisao_componente INTDC
	                                    INNER JOIN CUR.instituicao_nivel_tipo_componente_curricular INTCC ON INTCC.seq_instituicao_nivel_tipo_componente_curricular = INTDC.seq_instituicao_nivel_tipo_componente_curricular
	                                    WHERE INTDC.seq_tipo_divisao_componente = TDC.seq_tipo_divisao_componente
                                     ) AS DivisaoComponenteFormato,
                                     (  SELECT top 1 cco.dsc_componente_curricular_organizacao
										from cur.componente_curricular_organizacao cco
										where cco.seq_componente_curricular_organizacao = dc.seq_componente_curricular_organizacao
									 ) as DescricaoComponenteCurricularOrganizacao,
                                     DT.seq_divisao_turma AS SeqDivisaoTurma,
                                     DT.num_grupo AS DivisaoNumeroGrupo,
                                     DVTL.nom_entidade AS DivisaoLocalidade,
                                     DT.qtd_vagas AS DivisaoQuantidadeVagas,
                                     DT.dsc_informacoes_adicionais AS DivisaoInformacoes,
                                     TC.seq_atuacao_colaborador AS SeqPessoaColaborador,
		                             PDPC.nom_pessoa AS NomeColaborador,
		                             PDPC.nom_social AS NomeSocialColaborador,
									 DTC.seq_atuacao_colaborador AS SeqPessoaDivisaoColaborador,
		                             DTPDPC.nom_pessoa AS NomeDivisaoColaborador,
		                             DTPDPC.nom_social AS NomeSocialDivisaoColaborador
                                    FROM TUR.turma T
                                    INNER JOIN TUR.turma_configuracao_componente TCC ON T.seq_turma = TCC.seq_turma
                                    LEFT JOIN TUR.restricao_turma_matriz RTM ON TCC.seq_turma_configuracao_componente = RTM.seq_turma_configuracao_componente
										                                     AND TCC.ind_principal = 'true'
                                    LEFT JOIN CSO.curso_oferta_localidade_turno COLT ON RTM.seq_curso_oferta_localidade_turno = COLT.seq_curso_oferta_localidade_turno
                                    LEFT JOIN CSO.curso_oferta_localidade COL ON COLT.seq_entidade_curso_oferta_localidade = COL.seq_entidade
                                    LEFT JOIN ORG.hierarquia_entidade_item ECOHEI ON COL.seq_entidade = ECOHEI.seq_entidade
                                    LEFT JOIN ORG.hierarquia_entidade_item ECOHEIS ON ECOHEI.seq_hierarquia_entidade_item_superior = ECOHEIS.seq_hierarquia_entidade_item
                                    LEFT JOIN ORG.entidade ECOL ON ECOHEIS.seq_entidade = ECOL.seq_entidade
                                    LEFT JOIN CSO.curso_oferta CO ON COL.seq_curso_oferta = CO.seq_curso_oferta
                                    INNER JOIN CSO.curso C ON CO.seq_entidade_curso = C.seq_entidade
                                                         AND  ( @SEQ_NIVEL_ENSINO IS NULL OR @SEQ_NIVEL_ENSINO = c.seq_nivel_ensino)
                                    LEFT JOIN CSO.turno TU ON COLT.seq_turno = TU.seq_turno
                                    INNER JOIN TUR.tipo_turma TT ON T.seq_tipo_turma = TT.seq_tipo_turma
                                    INNER JOIN TUR.divisao_turma DT ON T.seq_turma = DT.seq_turma
                                    INNER JOIN CUR.divisao_componente DC ON DT.seq_divisao_componente = DC.seq_divisao_componente
                                    INNER JOIN CUR.tipo_divisao_componente TDC ON DC.seq_tipo_divisao_componente = TDC.seq_tipo_divisao_componente
                                    LEFT JOIN ORG.entidade DVTL ON DT.seq_entidade_localidade = DVTL.seq_entidade
                                    --Turma Colaborador
									LEFT JOIN TUR.turma_colaborador TC ON T.seq_turma = TC.seq_turma
									LEFT JOIN PES.pessoa_atuacao PAC ON TC.seq_atuacao_colaborador = PAC.seq_pessoa_atuacao
									LEFT JOIN PES.pessoa_dados_pessoais PDPC ON PAC.seq_pessoa_dados_pessoais = PDPC.seq_pessoa_dados_pessoais
									--Divisão Colaborador
									LEFT JOIN TUR.divisao_turma_colaborador DTC ON DT.seq_divisao_turma = DTC.seq_divisao_turma
									LEFT JOIN PES.pessoa_atuacao DTPAC ON DTC.seq_atuacao_colaborador = DTPAC.seq_pessoa_atuacao
									LEFT JOIN PES.pessoa_dados_pessoais DTPDPC ON DTPAC.seq_pessoa_dados_pessoais = DTPDPC.seq_pessoa_dados_pessoais
                                    WHERE T.seq_turma IN ({1})";

        #endregion [ _buscarRelatorioTurmaPorCicloLetivoComColaboradores ]

        #region [_sequenceTurma - codigo de turma]

        private string _buscarProximoValorSequence = @"select next value for TUR.cod_turma as CodigoTurma";

        #endregion [_sequenceTurma - codigo de turma]

        #endregion [ Queries ]

        /// <summary>
        /// O campo turma é composto do "Código da turma" + "." + "Número da turma".
        /// </summary>
        /// <returns>"Código da turma" + "." + "Número da turma"</returns>
        public string GerarCodigoNumeroTurma()
        {
            ///*O campo código da turma deverá ser um campo sequencial;
            //  O campo número da turma deverá ser um sequencial começando sempre de 1 por código da turma.*/

            // var codigo = this.SearchAll().Count() > 0 ? this.SearchAll().Select(s => s.Codigo).Max() + 1 : 1;
            int codigo = this.RawQuery<int>(_buscarProximoValorSequence, null).FirstOrDefault();
            var numero = 1;

            return $"{codigo}.{numero}";
        }

        public List<TurmaListarGrupoCursoVO> BuscarTurmasAluno(long seq, long? seqCicloLetivo, bool? apenasMatriculado)
        {
            var specDivisoes = new PlanoEstudoItemFilterSpecification() { SeqAluno = seq, SeqCicloLetivo = seqCicloLetivo, ApenasMatriculado = apenasMatriculado, SomenteTurma = true, PlanoEstudoAtual = true };
            var dadosPlanoEstudo = PlanoEstudoItemDomainService.SearchProjectionBySpecification(specDivisoes, x => new
            {
                SeqDivisaoTurma = x.SeqDivisaoTurma.Value,
                SeqDivisaoComponente = x.DivisaoTurma.SeqDivisaoComponente
            }).ToList();
            var seqsDivisoesTurma = dadosPlanoEstudo.Select(x => x.SeqDivisaoTurma).ToList();
            var seqsDivisoesComponente = dadosPlanoEstudo.Select(x => x.SeqDivisaoComponente).ToList();

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seq);

            if (seqsDivisoesTurma == null || !seqsDivisoesTurma.Any())
                return new List<TurmaListarGrupoCursoVO>();

            var spec = new TurmaFilterSpecification() { SeqsDivisoesTurma = seqsDivisoesTurma, SeqCicloLetivoInicio = seqCicloLetivo };
            spec.MaxResults = int.MaxValue;

            var dadosTurmas = SearchProjectionBySpecification(spec, p => new
            {
                Seq = p.Seq,
                Codigo = p.Codigo,
                Numero = p.Numero,

                SeqNivelEnsino = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.FirstOrDefault(w => w.Responsavel).SeqNivelEnsino,
                SeqNivelEnsinoPrincipal = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.FirstOrDefault(w => w.Responsavel).SeqNivelEnsino,

                SeqTipoComponenteCurricular = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                SeqTipoComponenteCurricularPrincipal = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,

                QuantidadeVagas = (short?)p.QuantidadeVagas,
                DescricaoTipoTurma = p.TipoTurma.Descricao,
                TurmaCompartilhada = p.TipoTurma.AssociacaoOfertaMatriz == AssociacaoOfertaMatriz.ExigeMaisDeUma,
                DescricaoCicloLetivoInicio = p.CicloLetivoInicio.Descricao,
                DescricaoCicloLetivoFim = p.CicloLetivoFim.Descricao,
                SeqCicloLetivoInicio = p.SeqCicloLetivoInicio,
                SeqCicloLetivoFim = (long?)p.SeqCicloLetivoFim,

                DescricaoConfiguracaoComponente = p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                DescricaoConfiguracaoComponentePrincipal = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).Descricao,

                DescricaoLocalidade = p.ConfiguracoesComponente.SelectMany(w => w.RestricoesTurmaMatriz).FirstOrDefault(r => r.SeqCursoOfertaLocalidadeTurno == dadosOrigem.SeqCursoOfertaLocalidadeTurno).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                DescricaoLocalidadePrincipal = p.ConfiguracoesComponente.SelectMany(w => w.RestricoesTurmaMatriz).FirstOrDefault(r => r.OfertaMatrizPrincipal).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,

                DescricaoTurno = p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso == dadosOrigem.SeqCurso)).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao,
                DescricaoTurnoPrincipal = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao,

                SituacaoTurmaAtual = (SituacaoTurma?)p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma : SituacaoTurma.Nenhum,
                SituacaoJustificativa = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().Justificativa : "",
                SeqOrigemAvaliacao = p.SeqOrigemAvaliacao,
                PermiteAvaliacaoParcial = p.OrigemAvaliacao.PermiteAvaliacaoParcial,
                DivisoesTurma = p.DivisoesTurma.Select(s => new DivisaoTurmaVO()
                {
                    Seq = s.Seq,
                    SeqTurma = s.SeqTurma,
                    SeqDivisaoComponente = s.SeqDivisaoComponente,
                    NumeroDivisaoComponente = s.DivisaoComponente.Numero,
                    NumeroGrupo = s.NumeroGrupo,
                    SeqOrigemMaterial = (long?)s.SeqOrigemMaterial
                }).ToList(),
                TurmaDivisoes = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault()
                    .ConfiguracaoComponente.DivisoesComponente.Where(d => seqsDivisoesComponente.Contains(d.Seq)).Select(s => new TurmaDivisoesVO()
                    {
                        SeqDivisaoComponente = s.Seq,
                        SeqTurma = p.Seq,
                        TipoDivisaoDescricao = s.TipoDivisaoComponente.Descricao,
                        GerarOrientacao = s.TipoDivisaoComponente.GeraOrientacao,
                        Numero = s.Numero,
                        CargaHoraria = s.CargaHoraria,
                        DescricaoComponenteCurricularOrganizacao = s.ComponenteCurricularOrganizacao.Descricao
                    }).ToList()
            }).ToList();

            var registros = new List<TurmaListarVO>();
            dadosTurmas?.ForEach(d =>
            {
                var turmaListarVO = SMCMapperHelper.Create<TurmaListarVO>(d);
                turmaListarVO.SeqNivelEnsino = d.SeqNivelEnsino ?? d.SeqNivelEnsinoPrincipal ?? 0;
                turmaListarVO.SeqTipoComponenteCurricular = d.SeqTipoComponenteCurricular ?? d.SeqTipoComponenteCurricularPrincipal ?? 0;
                turmaListarVO.DescricaoConfiguracaoComponente = d.DescricaoConfiguracaoComponente ?? d.DescricaoConfiguracaoComponentePrincipal;
                turmaListarVO.DescricaoLocalidade = d.DescricaoLocalidade ?? d.DescricaoLocalidadePrincipal;
                turmaListarVO.DescricaoTurno = d.DescricaoTurno ?? d.DescricaoTurnoPrincipal;
                registros.Add(turmaListarVO);
            });

            foreach (var item in registros)
            {
                var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(item.SeqNivelEnsino, item.SeqTipoComponenteCurricular);

                //Recuperando detalhes das divisoes para listagem da turma
                foreach (var divisao in item.TurmaDivisoes)
                {
                    if (tiposComponenteNivel != null)
                        divisao.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;

                    var divisaoItemDetalhe = item.DivisoesTurma.Where(w => seqsDivisoesTurma.Contains(w.Seq) && w.SeqDivisaoComponente == divisao.SeqDivisaoComponente).FirstOrDefault();

                    if (divisaoItemDetalhe != null)
                    {
                        divisao.Seq = divisaoItemDetalhe.Seq;
                        divisao.SeqOrigemMaterial = divisaoItemDetalhe.SeqOrigemMaterial;
                        divisao.TurmaCodigoFormatado = item.CodigoFormatado;
                        divisao.NumeroDivisaoComponente = divisaoItemDetalhe.NumeroDivisaoComponente;
                        divisao.NumeroGrupo = divisaoItemDetalhe.NumeroGrupo;
                    }

                    divisao.SeqTurma = item.Seq;
                    divisao.DivisaoComponenteDescricao = divisao.DescricaoFormatada;
                }
            }

            List<TurmaListarGrupoCursoVO> retorno = new List<TurmaListarGrupoCursoVO>();
            registros.GroupBy(g => g.DescricaoCursoLocalidadeTurno).SMCForEach(f =>
            {
                retorno.Add(new TurmaListarGrupoCursoVO() { DescricaoCursoLocalidadeTurno = f.First().DescricaoCursoLocalidadeTurno, Turmas = f.ToList() });
            });

            return retorno;
        }

        /// <summary>
        /// Buscar a turma
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="buscarDivisoesComponenteComDivisoesTurmaAssociadas"></param>
        /// <returns>Objeto turma com seus itens</returns>
        public TurmaVO BuscarTurma(long seq, bool buscarDivisoesComponenteComDivisoesTurmaAssociadas = false)
        {
            var turma = this.SearchProjectionByKey(new SMCSeqSpecification<Turma>(seq),
                                                   p => new TurmaVO()
                                                   {
                                                       Seq = p.Seq,
                                                       SeqTurmaOrigem = p.Seq,
                                                       Codigo = p.Codigo,
                                                       Numero = p.Numero,
                                                       SeqNivelEnsino = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino,
                                                       SeqTipoComponenteCurricular = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                                                       QuantidadeVagas = p.QuantidadeVagas,
                                                       SeqAgendaTurma = p.SeqAgendaTurma,
                                                       DataInicioPeriodoLetivo = p.DataInicioPeriodoLetivo,
                                                       DataFimPeriodoLetivo = p.DataFimPeriodoLetivo,
                                                       SeqTipoTurma = p.SeqTipoTurma,
                                                       SeqCicloLetivoInicio = p.SeqCicloLetivoInicio,
                                                       SeqCicloLetivoFim = p.SeqCicloLetivoFim,
                                                       DescricaoTipoTurma = p.TipoTurma.Descricao,
                                                       DescricaoCicloLetivoInicio = p.CicloLetivoInicio.Descricao,
                                                       DescricaoCicloLetivoFim = p.CicloLetivoFim.Descricao,
                                                       DescricaoConfiguracaoComponente = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().Descricao,
                                                       ConfiguracaoComponente = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().SeqConfiguracaoComponente,
                                                       SituacaoTurmaAtual = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma : SituacaoTurma.Nenhum,
                                                       SituacaoJustificativa = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().Justificativa : "",
                                                       DiarioFechado = p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
                                                       PossuiNotaLancada = p.OrigemAvaliacao.HistoricosEscolares.Any(),
                                                       TurmaConfiguracoesCabecalho = p.ConfiguracoesComponente.Select(s => new TurmaCabecalhoConfiguracoesVO()
                                                       {
                                                           SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                                                           DescricaoConfiguracaoComponente = s.Descricao,
                                                           DescricaoConfiguracaoComponenteOrdem = s.ConfiguracaoComponente.Descricao,
                                                           SeqComponenteCurricular = s.ConfiguracaoComponente.SeqComponenteCurricular,
                                                           ConfiguracaoPrincipal = s.Principal ? Common.Enums.LegendaPrincipal.Principal : Common.Enums.LegendaPrincipal.Nenhum,
                                                       }).OrderByDescending(o => o.ConfiguracaoPrincipal).ThenBy(t => t.DescricaoConfiguracaoComponenteOrdem).ToList(),
                                                       GrupoConfiguracoesCompartilhadas = p.ConfiguracoesComponente.Where(w => !w.Principal).Select(s => new TurmaGrupoConfiguracaoVO()
                                                       {
                                                           Seq = s.Seq,
                                                           SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                                                           SeqComponenteCurricular = s.ConfiguracaoComponente.SeqComponenteCurricular,
                                                           Descricao = s.Descricao,
                                                           Selecionado = true
                                                       }).ToList(),
                                                       TurmaOfertasMatriz = p.ConfiguracoesComponente.Select(s => new TurmaOfertaMatrizVO()
                                                       {
                                                           SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                                                           DescricaoConfiguracaoComponente = s.ConfiguracaoComponente.Codigo + " - " + s.ConfiguracaoComponente.Descricao,
                                                           SeqComponenteCurricular = s.ConfiguracaoComponente.SeqComponenteCurricular,
                                                           ConfiguracaoPrincipal = s.Principal ? Common.Enums.LegendaPrincipal.Principal : Common.Enums.LegendaPrincipal.Nenhum,
                                                           OfertasMatriz = s.RestricoesTurmaMatriz.Select(r => new TurmaOfertaMatrizDetailVO()
                                                           {
                                                               Seq = r.Seq,
                                                               OfertaMatrizPrincipal = r.OfertaMatrizPrincipal,
                                                               QuantidadeVagasOcupadas = r.QuantidadeVagasOcupadas,
                                                               QuantidadeVagasReservadas = r.QuantidadeVagasReservadas,
                                                               SeqMatrizCurricularOferta = r.SeqMatrizCurricularOferta,
                                                               SeqComponenteCurricularAssunto = r.SeqComponenteCurricularAssunto,
                                                               SeqTurmaConfiguracaoComponente = r.SeqTurmaConfiguracaoComponente,
                                                               DescricaoTurmaConfiguracaoComponente = r.TurmaConfiguracaoComponente.Descricao,
                                                               Codigo = r.MatrizCurricularOferta.Codigo,
                                                               DescricaoMatrizCurricular = r.MatrizCurricularOferta.MatrizCurricular.Descricao,
                                                               DescricaoUnidade = r.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                               DescricaoLocalidade = r.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                               DescricaoTurno = r.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                                               ReservaVagas = r.QuantidadeVagasReservadas,
                                                               SeqCursoOfertaLocalidadeTurno = r.SeqCursoOfertaLocalidadeTurno,
                                                               DescricaoCursoOfertaLocalidadeTurno = r.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome + " - " + r.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                                               DescricaoComplementarMatrizCurricular = r.MatrizCurricularOferta.MatrizCurricular.DescricaoComplementar
                                                           }).ToList(),
                                                       }).OrderByDescending(o => o.ConfiguracaoPrincipal).ToList(),
                                                       OrigemAvaliacao = new OrigemAvaliacaoVO()
                                                       {
                                                           Seq = p.OrigemAvaliacao.Seq,
                                                           SeqCriterioAprovacao = p.OrigemAvaliacao.SeqCriterioAprovacao,
                                                           QuantidadeGrupos = p.OrigemAvaliacao.QuantidadeGrupos,
                                                           QuantidadeProfessores = p.OrigemAvaliacao.QuantidadeProfessores,
                                                           NotaMaxima = p.OrigemAvaliacao.NotaMaxima,
                                                           ApurarFrequencia = p.OrigemAvaliacao.ApurarFrequencia,
                                                           SeqEscalaApuracao = p.OrigemAvaliacao.SeqEscalaApuracao,
                                                           SeqMatrizCurricularOferta = p.OrigemAvaliacao.SeqMatrizCurricularOferta,
                                                           MateriaLecionada = p.OrigemAvaliacao.MateriaLecionada,
                                                           TipoOrigemAvaliacao = p.OrigemAvaliacao.TipoOrigemAvaliacao,
                                                           Descricao = p.OrigemAvaliacao.Descricao,
                                                           PermiteAvaliacaoParcial = p.OrigemAvaliacao.PermiteAvaliacaoParcial,
                                                           MateriaLecionadaObrigatoria = p.OrigemAvaliacao.MateriaLecionadaObrigatoria
                                                       },
                                                       DivisoesTurma = p.DivisoesTurma.Select(s => new DivisaoTurmaVO()
                                                       {
                                                           Seq = s.Seq,
                                                           SeqDivisaoComponente = s.SeqDivisaoComponente,
                                                           SeqLocalidade = s.SeqLocalidade,
                                                           DescricaoLocalidade = s.Localidade.Nome,
                                                           SeqOrigemAvaliacao = s.SeqOrigemAvaliacao,
                                                           SeqTurma = s.SeqTurma,
                                                           NumeroGrupo = s.NumeroGrupo,
                                                           QuantidadeVagas = s.QuantidadeVagas,
                                                           QuantidadeVagasOcupadas = s.QuantidadeVagasOcupadas,
                                                           InformacoesAdicionais = s.InformacoesAdicionais,
                                                           NumeroDivisaoComponente = s.DivisaoComponente.Numero,
                                                           OrigemAvaliacao = new OrigemAvaliacaoVO()
                                                           {
                                                               Seq = s.OrigemAvaliacao.Seq,
                                                               SeqCriterioAprovacao = s.OrigemAvaliacao.SeqCriterioAprovacao,
                                                               QuantidadeGrupos = s.OrigemAvaliacao.QuantidadeGrupos,
                                                               QuantidadeProfessores = s.OrigemAvaliacao.QuantidadeProfessores,
                                                               NotaMaxima = s.OrigemAvaliacao.NotaMaxima,
                                                               ApurarFrequencia = s.OrigemAvaliacao.ApurarFrequencia,
                                                               SeqEscalaApuracao = s.OrigemAvaliacao.SeqEscalaApuracao,
                                                               SeqMatrizCurricularOferta = s.OrigemAvaliacao.SeqMatrizCurricularOferta,
                                                               MateriaLecionada = s.OrigemAvaliacao.MateriaLecionada,
                                                               TipoOrigemAvaliacao = s.OrigemAvaliacao.TipoOrigemAvaliacao,
                                                               Descricao = s.OrigemAvaliacao.Descricao,
                                                               PermiteAvaliacaoParcial = s.OrigemAvaliacao.PermiteAvaliacaoParcial,
                                                               MateriaLecionadaObrigatoria = s.OrigemAvaliacao.MateriaLecionadaObrigatoria
                                                           },
                                                           SeqHistoricoConfiguracaoGradeAtual = s.SeqHistoricoConfiguracaoGradeAtual
                                                       }).ToList(),
                                                       TurmaDivisoes = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault()
                                                       .ConfiguracaoComponente.DivisoesComponente.Select(s => new TurmaDivisoesVO()
                                                       {
                                                           SeqDivisaoComponente = s.Seq,
                                                           TipoDivisaoDescricao = s.TipoDivisaoComponente.Descricao,
                                                           Numero = s.Numero,
                                                           CargaHoraria = s.CargaHoraria,
                                                           DescricaoComponenteCurricularOrganizacao = s.ComponenteCurricularOrganizacao.Descricao
                                                       }).ToList(),
                                                       SeqComponenteCurricularAssunto = p.ConfiguracoesComponente.FirstOrDefault().RestricoesTurmaMatriz.FirstOrDefault().SeqComponenteCurricularAssunto,
                                                   });

            /*Verifica se irá mostrar todas as divisões componente (manter turma), ou se serão exibidas apenas as divisões componente
            que possuem divisões de turma (visualizar detalhes da turma)*/
            if (buscarDivisoesComponenteComDivisoesTurmaAssociadas)
            {
                var seqsDivisoesComponenteComDivisoesTurmaAssociadas = this.DivisaoTurmaDomainService.SearchProjectionBySpecification(new DivisaoTurmaFilterSpecification() { SeqTurma = seq }, p => p.DivisaoComponente.Seq).Distinct().ToList();

                turma.TurmaDivisoes = this.DivisaoComponenteDomainService.SearchProjectionBySpecification(new SMCContainsSpecification<DivisaoComponente, long>(p => p.Seq, seqsDivisoesComponenteComDivisoesTurmaAssociadas.ToArray()), s => new TurmaDivisoesVO()
                {
                    SeqDivisaoComponente = s.Seq,
                    TipoDivisaoDescricao = s.TipoDivisaoComponente.Descricao,
                    Numero = s.Numero,
                    CargaHoraria = s.CargaHoraria,
                    DescricaoComponenteCurricularOrganizacao = s.ComponenteCurricularOrganizacao.Descricao
                }).ToList();
            }

            if (turma == null) { throw new TurmaNaoEncontradaException(); }

            turma.EhOuPossuiDesdobramento = turma.Numero > 1 || this.Count(new TurmaFilterSpecification
            {
                Codigo = turma.Codigo,
            }) > 1;

            turma.TurmaOfertasMatriz.SMCForEach(f =>
            {
                f.OfertasMatrizDisplay = f.OfertasMatriz;
                f.DescricaoConfiguracaoComponente = ConfiguracaoComponenteDomainService.BuscarDescricaoConfiguracaoComponente(f.SeqConfiguracaoComponente);

                var configuracaoCabecalho = turma.TurmaConfiguracoesCabecalho?.FirstOrDefault(x => x.SeqConfiguracaoComponente == f.SeqConfiguracaoComponente);
                if (configuracaoCabecalho != null)
                {
                    configuracaoCabecalho.DescricaoConfiguracaoComponente = f.DescricaoConfiguracaoComponente;
                }
            });

            turma.ConfirmarAlteracao = false;

            var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(turma.SeqNivelEnsino, turma.SeqTipoComponenteCurricular);

            //Recuperando detalhes das divisoes para listagem da turma
            foreach (var divisao in turma.TurmaDivisoes)
            {
                if (tiposComponenteNivel != null)
                    divisao.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;

                divisao.SeqTurma = turma.Seq; // Atribui SeqTurma ao objeto TurmaDivisaoVO

                divisao.DivisoesComponentesDisplay = new List<TurmaDivisoesDetailVO>();
                divisao.DivisaoComponenteDescricao = divisao.DescricaoFormatada;
                turma.DivisoesTurma
                    .Where(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente)
                    .SMCForEach(f =>
                    {
                        TurmaDivisoesDetailVO divisaoBanco = new TurmaDivisoesDetailVO();
                        divisaoBanco.Seq = f.Seq;
                        divisaoBanco.SeqTurma = f.SeqTurma;
                        divisaoBanco.Turma = turma.CodigoFormatado;
                        divisaoBanco.DivisaoDescricao = f.NumeroDivisaoComponente.ToString();
                        divisaoBanco.GrupoNumero = f.NumeroGrupo.ToString().PadLeft(3, '0');
                        divisaoBanco.DivisaoVagas = f.QuantidadeVagas;
                        divisaoBanco.QuantidadeVagasOcupadas = f.QuantidadeVagasOcupadas;
                        divisaoBanco.DescricaoLocalidade = f.DescricaoLocalidade;
                        divisaoBanco.GeraOrientacao = divisao.GerarOrientacao;
                        divisaoBanco.InformacoesAdicionais = f.InformacoesAdicionais;
                        divisao.DivisoesComponentesDisplay.Add(divisaoBanco);
                        divisao.Seq = f.Seq;
                    });

                if (!turma.ConfirmarAlteracao && divisao.Seq.HasValue)
                {
                    turma.ExisteSolicitacaoMatriculaOuPlanoEstudo = DivisaoTurmaDomainService.VerificarDivisaoTurmaMatriculaPlano(divisao.Seq.Value);
                    turma.ConfirmarAlteracao = turma.ExisteSolicitacaoMatriculaOuPlanoEstudo;
                }
            }

            ValidarHabilitarCampos(turma);

            return turma;
        }

        #region [ Validar Habilitar Campos ]

        private void ValidarHabilitarCampos(TurmaVO turma)
        {
            turma.HabilitarInformacoesAdicionais = turma.ConfirmarAlteracao;

            /*
                Caso não estiver, verificar se já existe registro da origem avaliação da turma em pelo menos um histórico escolar. Caso exista, acionar a interface considerando:
                Apenas os seguintes campos deverão estar habilitados:

	                - Passo 1: Vagas.
	                - Passo 2: nenhum.
	                - Passo 3: Reserva de vagas.
	                - Passo 4: Localidade, Vagas, Informações adicionais, Inclusão e Exclusão de grupos, Vagas de grupo.
	                - Passo 5: nenhum.
                */

            if (turma.EhOuPossuiDesdobramento || turma.PossuiNotaLancada)
            {
                ValidarHabilitarCamposDesdobramento(turma);
            }
            /*Se não for, verificar se a turma existe em alguma solicitação de serviço ativa* ou em no
             * plano de estudo atual de algum aluno. Caso existir, exibir a seguinte mensagem:

	            "Alguns campos estarão desabilitados, pois a turma já está em uma solicitação de matrícula ou em um plano de estudo."

                 Ao selecionar a opção "Ok", acionar a interface de manutenção com os seguintes campos habilitados:

	            - Passo 1: Vagas, Situação, Justificativa do cancelamento.
	            - Passo 2: Inclusão de configuração para compartilhamento (não é permitido desmarcar uma configuração já selecionada).
	            - Passo 3: Inclusão de Oferta, Reserva de vagas.
	            - Passo 4: todos os campos habilitados.
	            - Passo 5: nenhum campo habilitado.*/
            else if (turma.Numero == 1 && turma.ExisteSolicitacaoMatriculaOuPlanoEstudo)
            {
                // Passo 1
                turma.HabilitarTipoTurma = false;
                turma.HabilitarConfiguracaoComponente = false;
                turma.HabilitarCicloLetivoInicio = false;
                turma.HabilitarCicloLetivoFim = false;

                // Passo 2
                turma.HabilitarGrupoConfiguracoesCompartilhadas = true;
                turma.HabilitarGrupoConfiguracoesCompartilhadasDesmarcar = true;

                // Passo 3

                foreach (var turmaOfertaMatriz in turma.TurmaOfertasMatriz)
                {
                    turmaOfertaMatriz.PermitirApenasInserirOfertas = false;
                    turmaOfertaMatriz.DesabilitarMatrizCurricularOferta = true;
                    turmaOfertaMatriz.DesabilitarOfertasMatrizConfiguracaoPrincipal = false;
                }

                //Passo 4
                turma.HabilitarLocalidades = true;

                //Passo 5
                turma.HabilitarComponenteCurricularAssunto = false;
            }
            //Edição de uma turma comum
            else if (turma.Seq > 0)
            {
                // As mesmas regras de cópia
                ValidarHabilitarCamposCopiarTurma(turma);
            }
            // Nova Turma
            else
            {
                HabilitarCamposTurma(turma);
            }

            // Garantir que não seja feita a regra de edição, ao efetuar a cópia da turma
            if (turma.Operacao == OperacaoTurma.Copiar) { ValidarHabilitarCamposCopiarTurma(turma); }

            turma.PeriodoLetivoReadOnly = !SMCSecurityHelper.Authorize(UC_TUR_001_01_02.PERMITE_ALTERAR_PERIODO_LETIVO_TURMA);
        }

        private void ValidarHabilitarCamposCopiarTurma(TurmaVO turma)
        {
            /*Acionar a interface com os seguintes campos habilitados:
		        - Passo 1: Ciclo letivo, Vagas, Situação, Justificativa do cancelamento..
		        - Passo 2: todos os campos habilitados.
		        - Passo 3: todos os campos habilitados.
		        - Passo 4: todos os campos habilitados.
		        - Passo 5: todos os campos habilitados.*/

            var habilitar = true;

            // Passo 1
            turma.HabilitarTipoTurma = false;
            turma.HabilitarConfiguracaoComponente = false;
            turma.HabilitarCicloLetivoInicio = habilitar;
            turma.HabilitarCicloLetivoFim = habilitar;

            // Passo 2
            turma.HabilitarGrupoConfiguracoesCompartilhadas = habilitar;
            turma.HabilitarGrupoConfiguracoesCompartilhadasDesmarcar = habilitar;

            // Passo 3
            if (turma.TurmaOfertasMatriz.SMCAny())
            {
                foreach (var turmaOfertaMatriz in turma.TurmaOfertasMatriz)
                {
                    turmaOfertaMatriz.PermitirApenasInserirOfertas = false;
                    turmaOfertaMatriz.DesabilitarAlteracaoOfertas = false;
                    turmaOfertaMatriz.DesabilitarMatrizCurricularOferta = false;
                    turmaOfertaMatriz.DesabilitarOfertasMatrizConfiguracaoPrincipal = false;
                }
            }

            //Passo 4
            turma.HabilitarLocalidades = habilitar;

            //Passo 5
            turma.HabilitarComponenteCurricularAssunto = habilitar;
        }

        private void HabilitarCamposTurma(TurmaVO turma, bool habilitar = true)
        {
            // Passo 1
            turma.HabilitarTipoTurma = habilitar;
            turma.HabilitarConfiguracaoComponente = habilitar;
            turma.HabilitarCicloLetivoInicio = habilitar;
            turma.HabilitarCicloLetivoFim = habilitar;

            // Passo 2
            turma.HabilitarGrupoConfiguracoesCompartilhadas = habilitar;
            turma.HabilitarGrupoConfiguracoesCompartilhadasDesmarcar = habilitar;

            // Passo 3
            if (turma.TurmaOfertasMatriz.SMCAny())
            {
                foreach (var turmaOfertaMatriz in turma.TurmaOfertasMatriz)
                {
                    turmaOfertaMatriz.DesabilitarAlteracaoOfertas = false;
                    turmaOfertaMatriz.DesabilitarMatrizCurricularOferta = false;
                    turmaOfertaMatriz.DesabilitarOfertasMatrizConfiguracaoPrincipal = false;
                }
            }

            //Passo 4
            turma.HabilitarLocalidades = habilitar;

            //Passo 5
            turma.HabilitarComponenteCurricularAssunto = habilitar;
        }

        private void ValidarHabilitarCamposDesdobramento(TurmaVO turma)
        {
            /* verificar se o número da turma é maior que 1. Se for, acionar a interface considerando que
             * apenas os seguintes campos deverão estar habilitados:
	                - Passo 1: Vagas, Situação, Justificativa do cancelamento.
	                - Passo 2: nenhum.
	                - Passo 3: Reserva de vagas.
	                - Passo 4: Localidade, Vagas, Informações adicionais, Inclusão e Exclusão de grupos, Vagas de grupo.
	                - Passo 5: nenhum.*/
            turma.HabilitarInformacoesAdicionais = turma.ConfirmarAlteracao;
            //if (turma.Numero > 1)
            //{
            // Passo 1
            turma.HabilitarTipoTurma = false;
            turma.HabilitarConfiguracaoComponente = false;
            turma.HabilitarCicloLetivoInicio = false;
            turma.HabilitarCicloLetivoFim = false;

            // Passo 2
            turma.HabilitarGrupoConfiguracoesCompartilhadas = false;
            turma.HabilitarGrupoConfiguracoesCompartilhadasDesmarcar = false;

            // Passo 3
            foreach (var turmaOfertaMatriz in turma.TurmaOfertasMatriz)
            {
                turmaOfertaMatriz.DesabilitarAlteracaoOfertas = true;
                turmaOfertaMatriz.DesabilitarMatrizCurricularOferta = true;
                turmaOfertaMatriz.DesabilitarOfertasMatrizConfiguracaoPrincipal = true;
            }

            //Passo 4
            turma.HabilitarLocalidades = true;

            //Passo 5
            turma.HabilitarComponenteCurricularAssunto = false;
            //}
        }

        #endregion [ Validar Habilitar Campos ]

        /// <summary>
        /// Buscar cabeçalho com os dados de turma
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Objeto com dados da turma</returns>
        public TurmaCabecalhoVO BuscarTurmaCabecalho(long seq)
        {
            var turma = this.SearchProjectionByKey(new SMCSeqSpecification<Turma>(seq),
                                                   p => new TurmaCabecalhoVO()
                                                   {
                                                       Seq = p.Seq,
                                                       Codigo = p.Codigo,
                                                       Numero = p.Numero,
                                                       CicloLetivoInicio = p.CicloLetivoInicio.Descricao,
                                                       CicloLetivoFim = p.CicloLetivoFim.Descricao,
                                                       Vagas = p.QuantidadeVagas,
                                                       DescricaoTipoTurma = p.TipoTurma.Descricao,
                                                       SituacaoTurmaAtual = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma : SituacaoTurma.Nenhum,
                                                       ColaboradoresBanco = p.Colaboradores.Select(s => s.Colaborador.DadosPessoais).ToList(),
                                                       DiarioFechado = p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
                                                       TurmaConfiguracoesCabecalho = p.ConfiguracoesComponente.Select(s => new TurmaCabecalhoConfiguracoesVO()
                                                       {
                                                           SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                                                           DescricaoConfiguracaoComponente = s.ConfiguracaoComponente.Descricao,
                                                           SeqComponenteCurricular = s.ConfiguracaoComponente.SeqComponenteCurricular,
                                                           ConfiguracaoPrincipal = s.Principal ? Common.Enums.LegendaPrincipal.Principal : Common.Enums.LegendaPrincipal.Nenhum,
                                                       }).OrderByDescending(o => o.ConfiguracaoPrincipal).ToList(),
                                                   });

            foreach (var configuracao in turma.TurmaConfiguracoesCabecalho)
            {
                configuracao.DescricaoConfiguracaoComponente = ConfiguracaoComponenteDomainService.BuscarDescricaoConfiguracaoComponente(configuracao.SeqConfiguracaoComponente);
            }

            if (turma != null)
            {
                if (turma.ColaboradoresBanco != null && turma.ColaboradoresBanco.Count > 0)
                {
                    turma.Colaboradores = new List<TurmaCabecalhoResponsavelVO>();
                    turma.ColaboradoresBanco.SMCForEach(f =>
                    {
                        var nomeCompleto = string.IsNullOrEmpty(f.NomeSocial) ? f.Nome : $"{f.NomeSocial} ({f.Nome})";

                        // RN_PES_023 - Nome e Nome Social - Visão Administrativo
                        turma.Colaboradores.Add(new TurmaCabecalhoResponsavelVO() { SeqColaborador = f.Seq, NomeColaborador = nomeCompleto });
                    });
                }
            }

            return turma;
        }

        /// <summary>
        /// Busca as turmas de acordo com os filtros
        /// </summary>
        /// <param name="filtros">Objeto turma filtro</param>
        /// <returns>SMCPagerData com a lista de turmas detalhadas</returns>
        public SMCPagerData<TurmaListarVO> BuscarTurmas(TurmaFiltroVO filtros)
        {
            var specTurma = filtros.Transform<TurmaFilterSpecification>();

            if (filtros.SeqsEntidadesResponsaveis != null && filtros.SeqsEntidadesResponsaveis.Count > 0)
                specTurma.SeqsEntidadeResponsavelPrograma = filtros.SeqsEntidadesResponsaveis;

            if (!string.IsNullOrEmpty(specTurma.CodigoFormatado))
            {
                var separacaoCodigo = specTurma.CodigoFormatado.Split('.');

                if (separacaoCodigo.Count() == 1 || string.IsNullOrEmpty(separacaoCodigo[1]))
                    specTurma.Codigo = Convert.ToInt32(separacaoCodigo[0]);
                else if (separacaoCodigo.Count() == 2)
                {
                    specTurma.Codigo = !string.IsNullOrEmpty(separacaoCodigo[0]) ? (int?)Convert.ToInt32(separacaoCodigo[0]) : null;
                    specTurma.Numero = Convert.ToInt16(separacaoCodigo[1]);
                }
                else
                    throw new TurmaCodigoFormatoInvalidoException();
            }

            if (specTurma.SituacaoTurmaDiario.HasValue && specTurma.SituacaoTurmaDiario.Value != SituacaoTurmaDiario.Nenhum)
                specTurma.SituacaoTurmaDiarioFlag = specTurma.SituacaoTurmaDiario.Value == SituacaoTurmaDiario.Fechado;

            int total;
            specTurma.SetOrderBy(o => o.Codigo);
            specTurma.SetOrderBy(o => o.Numero);
            specTurma.SetOrderBy(o => o.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).Descricao);

            var registros = this.SearchProjectionBySpecification(specTurma,
                                                                 p => new TurmaListarVO()
                                                                 {
                                                                     Seq = p.Seq,
                                                                     Codigo = p.Codigo,
                                                                     PermiteAvaliacaoParcial = p.OrigemAvaliacao.PermiteAvaliacaoParcial,
                                                                     Numero = p.Numero,
                                                                     SeqNivelEnsino = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino,
                                                                     SeqTipoComponenteCurricular = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                                                                     QuantidadeVagas = p.QuantidadeVagas,
                                                                     DescricaoTipoTurma = p.TipoTurma.Descricao,
                                                                     TurmaCompartilhada = p.TipoTurma.AssociacaoOfertaMatriz == AssociacaoOfertaMatriz.ExigeMaisDeUma,
                                                                     DescricaoCicloLetivoInicio = p.CicloLetivoInicio.Descricao,
                                                                     DescricaoCicloLetivoFim = p.CicloLetivoFim.Descricao,
                                                                     DescricaoConfiguracaoComponente = p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).Descricao,
                                                                     SituacaoTurmaAtual = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma : SituacaoTurma.Nenhum,
                                                                     DataInicioPeriodoLetivo = p.DataInicioPeriodoLetivo,
                                                                     DataFimPeriodoLetivo = p.DataFimPeriodoLetivo,
                                                                     SituacaoJustificativa = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().Justificativa : "",
                                                                     DiarioFechado = p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
                                                                     PossuiNotaLancada = p.OrigemAvaliacao.HistoricosEscolares.Any(),
                                                                     AgendaTurmaConfigurada = p.SeqAgendaTurma.HasValue,
                                                                     GradeConfigurada = p.DivisoesTurma.Any(a => a.SeqHistoricoConfiguracaoGradeAtual.HasValue),
                                                                     SeqAgendaTurma = p.SeqAgendaTurma,
                                                                     SeqOrigemAvaliacao = p.SeqOrigemAvaliacao,
                                                                     RestricaoTurmaMatrizOfertaPrincipal = p.ConfiguracoesComponente.SelectMany(c => c.RestricoesTurmaMatriz).Select(r => new RestricaoTurmaMatrizVO()
                                                                     {
                                                                         OfertaMatrizPrincipal = r.OfertaMatrizPrincipal,
                                                                         SeqMatrizCurricularOferta = r.SeqMatrizCurricularOferta
                                                                     }).FirstOrDefault(a => a.OfertaMatrizPrincipal),
                                                                     TurmaConfiguracoesCabecalho = p.ConfiguracoesComponente.Select(s => new TurmaCabecalhoConfiguracoesVO()
                                                                     {
                                                                         SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                                                                         DescricaoConfiguracaoComponente = s.Descricao,
                                                                         DescricaoConfiguracaoComponenteOrdem = s.ConfiguracaoComponente.Descricao,
                                                                         SeqComponenteCurricular = s.ConfiguracaoComponente.SeqComponenteCurricular,
                                                                         ConfiguracaoPrincipal = s.Principal ? Common.Enums.LegendaPrincipal.Principal : Common.Enums.LegendaPrincipal.Nenhum,
                                                                     }).OrderByDescending(o => o.ConfiguracaoPrincipal).ThenBy(t => t.DescricaoConfiguracaoComponenteOrdem).ToList(),
                                                                     TurmaOfertasMatriz = p.ConfiguracoesComponente.Select(s => new TurmaOfertaMatrizVO()
                                                                     {
                                                                         SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                                                                         DescricaoConfiguracaoComponente = s.Descricao,
                                                                         SeqComponenteCurricular = s.ConfiguracaoComponente.SeqComponenteCurricular,
                                                                         ConfiguracaoPrincipal = s.Principal ? Common.Enums.LegendaPrincipal.Principal : Common.Enums.LegendaPrincipal.Nenhum,
                                                                         OfertasMatrizDisplay = s.RestricoesTurmaMatriz.Select(r => new TurmaOfertaMatrizDetailVO()
                                                                         {
                                                                             Seq = r.Seq,
                                                                             SeqMatrizCurricularOferta = r.SeqMatrizCurricularOferta,
                                                                             OfertaMatrizPrincipal = r.OfertaMatrizPrincipal,
                                                                             Codigo = r.MatrizCurricularOferta.Codigo,
                                                                             DescricaoMatrizCurricular = r.MatrizCurricularOferta.MatrizCurricular.Descricao,
                                                                             DescricaoUnidade = r.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                                             DescricaoLocalidade = r.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                                             DescricaoTurno = r.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                                                             ReservaVagas = r.QuantidadeVagasReservadas,
                                                                             DescricaoTurmaConfiguracaoComponente = r.TurmaConfiguracaoComponente.Descricao,
                                                                             DescricaoComplementarMatrizCurricular = r.MatrizCurricularOferta.MatrizCurricular.DescricaoComplementar
                                                                         }).ToList()
                                                                     }).OrderByDescending(o => o.ConfiguracaoPrincipal).ToList(),
                                                                     DivisoesTurma = p.DivisoesTurma.Select(s => new DivisaoTurmaVO()
                                                                     {
                                                                         Seq = s.Seq,
                                                                         SeqTurma = s.SeqTurma,
                                                                         SeqDivisaoComponente = s.SeqDivisaoComponente,
                                                                         NumeroDivisaoComponente = s.DivisaoComponente.Numero,
                                                                         SeqLocalidade = s.SeqLocalidade,
                                                                         DescricaoLocalidade = s.Localidade.Nome,
                                                                         SeqOrigemAvaliacao = s.SeqOrigemAvaliacao,
                                                                         NumeroGrupo = s.NumeroGrupo,
                                                                         QuantidadeVagas = s.QuantidadeVagas,
                                                                         QuantidadeVagasOcupadas = s.QuantidadeVagasOcupadas,
                                                                         InformacoesAdicionais = s.InformacoesAdicionais,
                                                                         DivisaoTurmaPossuiConfiguracaoesGrade = s.HistoricosConfiguracaoGrade.Any(),
                                                                         OrigemAvaliacao = new OrigemAvaliacaoVO()
                                                                         {
                                                                             Seq = s.OrigemAvaliacao.Seq,
                                                                             SeqCriterioAprovacao = s.OrigemAvaliacao.SeqCriterioAprovacao,
                                                                             QuantidadeGrupos = s.OrigemAvaliacao.QuantidadeGrupos,
                                                                             QuantidadeProfessores = s.OrigemAvaliacao.QuantidadeProfessores,
                                                                             ApurarFrequencia = s.OrigemAvaliacao.ApurarFrequencia,
                                                                             NotaMaxima = s.OrigemAvaliacao.NotaMaxima,
                                                                             SeqEscalaApuracao = s.OrigemAvaliacao.SeqEscalaApuracao,
                                                                         },
                                                                     }).ToList(),
                                                                     TurmaDivisoes = p.DivisoesTurma.GroupBy(a => a.SeqDivisaoComponente).Select(s => new TurmaDivisoesVO()
                                                                     {
                                                                         SeqDivisaoComponente = s.Key,
                                                                         SeqTurma = p.Seq,
                                                                         TipoDivisaoDescricao = s.FirstOrDefault().DivisaoComponente.TipoDivisaoComponente.Descricao,
                                                                         GerarOrientacao = s.FirstOrDefault().DivisaoComponente.TipoDivisaoComponente.GeraOrientacao,
                                                                         Numero = s.FirstOrDefault().DivisaoComponente.Numero,
                                                                         CargaHoraria = s.FirstOrDefault().DivisaoComponente.CargaHoraria,
                                                                         DescricaoComponenteCurricularOrganizacao = s.FirstOrDefault().DivisaoComponente.ComponenteCurricularOrganizacao.Descricao,
                                                                     }).ToList()
                                                                     #region Trecho comentado onde se recupera as divisões componente a partir das configurações componentes da turma

                                                                     //TurmaDivisoes = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault()
                                                                     //.ConfiguracaoComponente.DivisoesComponente.Select(s => new TurmaDivisoesVO()
                                                                     //{
                                                                     //    SeqDivisaoComponente = s.Seq,
                                                                     //    SeqTurma = p.Seq,
                                                                     //    TipoDivisaoDescricao = s.TipoDivisaoComponente.Descricao,
                                                                     //    GerarOrientacao = s.TipoDivisaoComponente.GeraOrientacao,
                                                                     //    Numero = s.Numero,
                                                                     //    CargaHoraria = s.CargaHoraria,
                                                                     //    DescricaoComponenteCurricularOrganizacao = s.ComponenteCurricularOrganizacao.Descricao
                                                                     //}).ToList()

                                                                     #endregion
                                                                 }, out total).ToList();

            foreach (var item in registros)
            {
                item.ConfirmarAlteracao = false;
                item.DesativarExcluir = false;
                item.DesativarExcluirValidacaoEventoAula = false;
                item.EhOuPossuiDesdobramento = item.Numero > 1 || this.Count(new TurmaFilterSpecification
                {
                    Codigo = item.Codigo,
                }) > 1;
                item.PossuiDivisaoOrientacao = item.TurmaDivisoes.Any(a => a.GerarOrientacao);

                var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(item.SeqNivelEnsino, item.SeqTipoComponenteCurricular);

                //Recuperando detalhes das ofertas com as divisões
                foreach (var oferta in item.TurmaOfertasMatriz)
                {
                    oferta.OfertasMatrizDisplay.SMCForEach(f =>
                    {
                        var specOfertaDispaly = new DivisaoMatrizCurricularComponenteFilterSpecification()
                        {
                            SeqConfiguracaoComponente = oferta.SeqConfiguracaoComponente,
                            SeqMatrizCurricularOferta = f.SeqMatrizCurricularOferta
                        };
                        var divisaoOferta = DivisaoMatrizCurricularComponenteDomainService.BuscarDivisaoMatrizCurricularComponenteDetalhesNumero(specOfertaDispaly);
                        f.DivisaoCompleto = $"{divisaoOferta.DivisaoMatrizCurricularNumero} - {divisaoOferta.DivisaoMatrizCurricularDescricao}";
                    });

                    oferta.OfertasMatrizDisplay = oferta.OfertasMatrizDisplay.OrderBy(o => o.OfertaCompleto).ToList();
                }

                //Recuperando detalhes das divisoes para listagem da turma
                foreach (var divisao in item.TurmaDivisoes)
                {
                    if (tiposComponenteNivel != null)
                        divisao.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;

                    divisao.SeqTurma = item.Seq; // Atribui SeqTurma ao objeto TurmaDivisaoVO

                    divisao.DivisoesComponentesDisplay = new List<TurmaDivisoesDetailVO>();
                    divisao.DivisaoComponenteDescricao = divisao.DescricaoFormatada;
                    item.DivisoesTurma
                        .Where(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente)
                        .SMCForEach(f =>
                        {
                            var turmaCancelada = item.SituacaoTurmaAtual == SituacaoTurma.Cancelada;
                            var turmaVigente = DateTime.Now >= item.DataInicioPeriodoLetivo && DateTime.Now <= item.DataFimPeriodoLetivo;
                            var turmaComPeriodoEncerrado = item.DataFimPeriodoLetivo.Date < DateTime.Now.Date;
                            var origemAvaliacaoFrequencia = f.OrigemAvaliacao.ApurarFrequencia;
                            var turmaDiarioAberto = !item.DiarioFechado;

                            var divisaoTurmaPossuiConfiguracaoesGrade = f.DivisaoTurmaPossuiConfiguracaoesGrade;
                            var divisaoComponentePossuiConfiguracaoesGrade = false;

                            if (item.RestricaoTurmaMatrizOfertaPrincipal != null)
                            {
                                var matrizCurricularDivisaoComponente = this.MatrizCurricularDivisaoComponenteDomainService.SearchBySpecification(new MatrizCurricularDivisaoComponenteFilterSpecification() { SeqMatrizCurricularOferta = item.RestricaoTurmaMatrizOfertaPrincipal.SeqMatrizCurricularOferta, SeqDivisaoComponente = divisao.SeqDivisaoComponente }).FirstOrDefault();

                                if (matrizCurricularDivisaoComponente != null)
                                    divisaoComponentePossuiConfiguracaoesGrade = true;
                            }

                            TurmaDivisoesDetailVO divisaoBanco = new TurmaDivisoesDetailVO();
                            divisaoBanco.Seq = f.Seq;
                            divisaoBanco.SeqTurma = f.SeqTurma;
                            divisaoBanco.Turma = item.CodigoFormatado;
                            divisaoBanco.DivisaoDescricao = f.NumeroDivisaoComponente.ToString();
                            divisaoBanco.GrupoNumero = f.NumeroGrupo.ToString().PadLeft(3, '0');
                            divisaoBanco.DivisaoVagas = f.QuantidadeVagas;
                            divisaoBanco.QuantidadeVagasOcupadas = f.QuantidadeVagasOcupadas;
                            divisaoBanco.DescricaoLocalidade = f.DescricaoLocalidade;
                            divisaoBanco.GeraOrientacao = divisao.GerarOrientacao;
                            divisaoBanco.InformacoesAdicionais = f.InformacoesAdicionais;
                            divisaoBanco.SeqOrigemAvaliacao = f.SeqOrigemAvaliacao;

                            //Validações para habilitar botões e mensagem informativa
                            divisaoBanco.HabilitaBtnConfiguracaoGrade = true;
                            divisaoBanco.InstructionConfiguracaoGrade = string.Empty;
                            divisaoBanco.HabilitaBtnListaFrequencia = true;
                            divisaoBanco.InstructionListaFrequencia = string.Empty;
                            divisaoBanco.HabilitaBtnLancarFrequencia = true;
                            divisaoBanco.InstructionLancarFrequencia = string.Empty;
                            divisaoBanco.HabilitaBtnOrientacaoTurma = true;
                            divisaoBanco.InstructionOrientacaoTurma = string.Empty;

                            if (turmaCancelada)
                            {
                                divisaoBanco.HabilitaBtnConfiguracaoGrade = false;
                                divisaoBanco.InstructionConfiguracaoGrade = MessagesResource.Mensagem_Botoes_Turma_Cancelada;
                                divisaoBanco.HabilitaBtnListaFrequencia = false;
                                divisaoBanco.InstructionListaFrequencia = MessagesResource.Mensagem_Botoes_Turma_Cancelada;
                                divisaoBanco.HabilitaBtnLancarFrequencia = false;
                                divisaoBanco.InstructionLancarFrequencia = MessagesResource.Mensagem_Botoes_Turma_Cancelada;
                                divisaoBanco.HabilitaBtnOrientacaoTurma = false;
                                divisaoBanco.InstructionOrientacaoTurma = MessagesResource.Mensagem_Botoes_Turma_Cancelada;
                            }
                            else
                            {
                                #region Validação para habilitar configuração grade para turma vigente comentada conforme Task 40307

                                //if (!turmaVigente)
                                //{
                                //    divisaoBanco.HabilitaBtnConfiguracaoGrade = false;
                                //    divisaoBanco.InstructionConfiguracaoGrade = MessagesResource.Instruction_Turma_Nao_Vigente;
                                //}

                                #endregion

                                if (turmaComPeriodoEncerrado)
                                {
                                    divisaoBanco.HabilitaBtnConfiguracaoGrade = false;
                                    divisaoBanco.InstructionConfiguracaoGrade = MessagesResource.Instruction_Turma_Periodo_Encerrado;
                                }
                                else if (!divisaoComponentePossuiConfiguracaoesGrade)
                                {
                                    divisaoBanco.HabilitaBtnConfiguracaoGrade = false;
                                    divisaoBanco.InstructionConfiguracaoGrade = MessagesResource.Instruction_Divisao_Componente_Sem_Configuracao_Grade;
                                }

                                if (!origemAvaliacaoFrequencia.GetValueOrDefault())
                                {
                                    divisaoBanco.HabilitaBtnLancarFrequencia = false;
                                    divisaoBanco.InstructionLancarFrequencia = MessagesResource.Instruction_LancarFrequencia_Origem_Avaliacao_Frequencia;
                                }
                                else if (!turmaDiarioAberto)
                                {
                                    divisaoBanco.HabilitaBtnLancarFrequencia = false;
                                    divisaoBanco.InstructionLancarFrequencia = MessagesResource.Instruction_LancarFrequencia_Turma_Diario_Aberto;
                                }
                                else if (!item.SeqAgendaTurma.HasValue)
                                {
                                    divisaoBanco.HabilitaBtnLancarFrequencia = false;
                                    divisaoBanco.InstructionLancarFrequencia = MessagesResource.Instruction_LancarFrequencia_Agenda_Turma;
                                }
                                else if (divisaoTurmaPossuiConfiguracaoesGrade && item.SeqAgendaTurma.HasValue)
                                {
                                    divisaoBanco.HabilitaBtnLancarFrequencia = false;
                                    divisaoBanco.InstructionLancarFrequencia = MessagesResource.Instruction_LancarFrequencia_Grade_Configurada;
                                }


                                if (!divisaoTurmaPossuiConfiguracaoesGrade)
                                {
                                    divisaoBanco.HabilitaBtnConfiguracaoGrade = false;
                                    divisaoBanco.InstructionConfiguracaoGrade = MessagesResource.Instruction_ConfiguracaoGrade_Grade_Configurada;
                                }

                                if (!divisao.GerarOrientacao)
                                {
                                    divisaoBanco.HabilitaBtnOrientacaoTurma = false;
                                    divisaoBanco.InstructionOrientacaoTurma = MessagesResource.Instruction_Turma_Nao_Permite_Orientacao;
                                }
                            }

                            divisao.DivisoesComponentesDisplay.Add(divisaoBanco);

                            divisao.Seq = f.Seq;
                            divisao.TurmaDiarioAberto = turmaDiarioAberto;
                            divisao.TurmaCancelada = turmaCancelada;
                            divisao.TurmaVigente = turmaVigente;
                            divisao.DivisaoTurmaPossuiConfiguracaoesGrade = divisaoTurmaPossuiConfiguracaoesGrade;
                        });

                    if (!item.ConfirmarAlteracao && divisao.Seq.GetValueOrDefault() > 0)
                        item.ConfirmarAlteracao = DivisaoTurmaDomainService.VerificarDivisaoTurmaMatriculaPlano(divisao.Seq.Value);

                    //Verificar se a turma existe na solicitação de serviço ou em algum plano de estudo.
                    //Caso existir, o comando deve ser exibido desabilitado.
                    item.DivisoesTurma.ForEach(divisaoTurma =>
                    {
                        if (!item.DesativarExcluir && divisao.Seq.GetValueOrDefault() > 0)
                        {
                            item.DesativarExcluir = DivisaoTurmaDomainService.VerificarDivisaoTurmaMatriculaPlano(divisaoTurma.Seq, false);

                            if (item.DesativarExcluir)
                            {
                                item.MensagemDesativarExcluir = MessagesResource.MSG_Excluir_Turma_Desabilitado_Solicitacao;
                            }
                        }
                    });

                    if (!item.DesativarExcluir && !item.AulaLancada && divisao.Seq.GetValueOrDefault() > 0)
                    {
                        //Caso não exista, verificar se existe aula lançada para esta turma.
                        var specAula = new AulaFilterSpecification() { SeqDivisaoTurma = divisao.Seq.Value };
                        item.AulaLancada = AulaDomainService.Count(specAula) > 0;
                    }

                    if (!item.DesativarExcluir && !item.DesativarExcluirValidacaoEventoAula && item.DivisoesTurma.Any())
                    {
                        var seqsDivisaoTurma = item.DivisoesTurma.Select(a => a.Seq).Distinct().ToList();
                        var eventosAulaDivisoesTurma = this.EventoAulaDomainService.SearchBySpecification(new EventoAulaFilterSpecification() { SeqsDivisaoTurma = seqsDivisaoTurma }).ToList();

                        if (eventosAulaDivisoesTurma.Any())
                        {
                            item.DesativarExcluirValidacaoEventoAula = true;
                            item.MensagemDesativarExcluir = MessagesResource.MSG_Excluir_Turma_Desabilitado_Evento_Aula;
                        }
                    }
                }
            }
            return new SMCPagerData<TurmaListarVO>(registros, total);
        }

        /// <summary>
        /// Busca a turma para o retorno do lookup
        /// </summary>
        /// <param name="seq">Sequencial da turma selecionada</param>
        /// <returns>Dados da turma</returns>
        public TurmaListarVO BuscarTurmaLookup(long seq)
        {
            var descricaoConfiguracaoComponente = this.SearchProjectionByKey(seq, p => p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).Descricao);
            var turmaVo = new TurmaListarVO()
            {
                Seq = seq,
                DescricaoConfiguracaoComponente = descricaoConfiguracaoComponente
            };

            return turmaVo;
        }

        /// <summary>
        /// Buscar a situação atual da turma
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>O ultimo registro de histórico situação</returns>
        public TurmaHistoricoSituacaoVO BuscarTurmaSituacaoAtual(long seq)
        {
            var turma = this.SearchProjectionByKey(new SMCSeqSpecification<Turma>(seq),
                                                   p => new TurmaHistoricoSituacaoVO()
                                                   {
                                                       Seq = p.HistoricosSituacao.OrderByDescending(s => s.Seq).FirstOrDefault().Seq,
                                                       SituacaoTurma = p.HistoricosSituacao.OrderByDescending(s => s.Seq).FirstOrDefault().SituacaoTurma,
                                                       Justificativa = p.HistoricosSituacao.OrderByDescending(s => s.Seq).FirstOrDefault().Justificativa,
                                                   });

            return turma;
        }

        /// <summary>
        /// Retorna os sequenciais de turmas canceladas da lista de sequenciais passados
        /// </summary>
        /// <param name="seqsTurmas">Sequenciais das turmas</param>
        /// <returns>Sequenciais cancelados</returns>
        public IEnumerable<long> ValidarTurmasCanceladas(IEnumerable<long> seqsTurmas)
        {
            var turmas = this.SearchProjectionBySpecification(new SMCContainsSpecification<Turma, long>(p => p.Seq, seqsTurmas.ToArray()),
                                                   p => new
                                                   {
                                                       Seq = p.Seq,
                                                       SituacaoTurma = p.HistoricosSituacao.OrderByDescending(s => s.Seq).FirstOrDefault().SituacaoTurma
                                                   });

            return turmas.Where(t => t.SituacaoTurma == SituacaoTurma.Cancelada).Select(t => t.Seq).ToList();
        }

        #region [ Salvar Turma ]

        /// <summary>
        /// Grava uma turma com seus respectivos itens
        /// </summary>
        /// <param name="turma">Dados da turma a ser gravado</param>
        /// <returns>Sequencial da turma gravado</returns>
        public long SalvarTurma(TurmaVO turma)
        {
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                turma.Operacao = turma.OperacaoFix;

                //Step 2 - Grupo compartilhado
                //Inicia a lista da tabela turma_configuracao_componente
                ValidarConfiguracoesComponenteCompartilhados(turma);

                //Step 3 - Ofertas de matriz
                //Preenche as ofertas de matris das configurações de componente
                PreencherOfertasMatrizConfiguracoesComponente(turma);

                //Step 4 - Parâmetros
                //Criar registro de origem avaliação para a turma definindo o critério de aprovação adotado
                turma.OrigemAvaliacao = CriarOrigemAvaliacaoTurma(turma);

                //Step 5 - Divisões
                //Criar as divisões da turma conforme informado e adicionar na tabela origem avaliação um registro por divisão
                ValidacaoPersistirDivisoesTurma(turma);

                // Task 37580:TSK - Alterar implementação UC_TUR_001_01 -Turma
                // Não efetua a alteração do plano de estudos caso inclua uma configuração de componente nova. Faz agora só a alteração pela inclusão da oferta
                // Task 35767: Ao incluir uma configuração de componente, atualizar dados do plano de estudo/solicitações de um aluno
                // AtualizaConfiguracaoComponente(turma);
                List<long> listaSolicitacoesAtualizarDescricaoOriginal;
                List<long> listaSolicitacoesAtualizarDescricaoAtualizada;
                AtualizaOfertaMatriz(turma, out listaSolicitacoesAtualizarDescricaoOriginal, out listaSolicitacoesAtualizarDescricaoAtualizada);

                RegistrarHistoricos(turma);

                ValidarDuplicidadeTurma(turma);

                //Recuperando as datas do período letivo considerando que o curso NÃO é de oferta temporal, pois essa validação será implementada futuramente
                var seqsCursoOfertaLocalidadeTurno = turma.ConfiguracoesComponente.SelectMany(a => a.RestricoesTurmaMatriz).Select(a => a.SeqCursoOfertaLocalidadeTurno).Distinct().ToList();
                DateTime dataInicioPeriodoLetivo;
                DateTime dataFimPeriodoLetivo;

                if (seqsCursoOfertaLocalidadeTurno.Count() > 1)
                {
                    List<DateTime> listaDataInicioPeriodoLetivo = new List<DateTime>();
                    List<DateTime> listaDataFimPeriodoLetivo = new List<DateTime>();

                    foreach (var seqCursoOfertaLocalidadeTurno in seqsCursoOfertaLocalidadeTurno)
                    {
                        var eventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(turma.SeqCicloLetivoInicio, seqCursoOfertaLocalidadeTurno, Common.Areas.ALN.Enums.TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                        listaDataInicioPeriodoLetivo.Add(eventoLetivo.DataInicio);
                        listaDataFimPeriodoLetivo.Add(eventoLetivo.DataFim);
                    }

                    dataInicioPeriodoLetivo = listaDataInicioPeriodoLetivo.Min();
                    dataFimPeriodoLetivo = listaDataFimPeriodoLetivo.Max();
                }
                else
                {
                    var eventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(turma.SeqCicloLetivoInicio, seqsCursoOfertaLocalidadeTurno.FirstOrDefault(), Common.Areas.ALN.Enums.TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                    dataInicioPeriodoLetivo = eventoLetivo.DataInicio;
                    dataFimPeriodoLetivo = eventoLetivo.DataFim;
                }

                if (turma.DataInicioPeriodoLetivo.HasValue && turma.DataFimPeriodoLetivo.HasValue)
                {
                    /*Caso o período letivo da turma tenha sido informado para o usuário, é necessário que a data início e fim do
                    período letivo estejam inclusas no período letivo da curso_oferta_localidade_turno recuperado conforme RN_TUR_035 - Periodo Letivo da turma*/

                    if (turma.DataInicioPeriodoLetivo.Value < dataInicioPeriodoLetivo || turma.DataFimPeriodoLetivo.Value > dataFimPeriodoLetivo)
                        throw new TurmaPeriodoLetivoNaoContidoOfertaException();
                }
                else
                {
                    /*Ao salvar uma turma, se o período letivo não estiver sido informado pelo usuário, considerar o periodo letivo retornado
                     pela regra RN_TUR_035 - Periodo Letivo da turma*/

                    turma.DataInicioPeriodoLetivo = dataInicioPeriodoLetivo;
                    turma.DataFimPeriodoLetivo = dataFimPeriodoLetivo;
                }

                ValidarPeriodoLetivoEventoAula(turma);

                CriarAgenda(turma, dataInicioPeriodoLetivo, dataFimPeriodoLetivo);

                var registro = turma.Transform<Turma>();

                this.SaveEntity(registro);

                if (listaSolicitacoesAtualizarDescricaoOriginal.Any())
                {
                    foreach (var seqSolicitacao in listaSolicitacoesAtualizarDescricaoOriginal)
                    {
                        SolicitacaoServicoDomainService.AtualizarDescricao(seqSolicitacao, true, true);
                    }
                }

                if (listaSolicitacoesAtualizarDescricaoAtualizada.Any())
                {
                    foreach (var seqSolicitacao in listaSolicitacoesAtualizarDescricaoAtualizada)
                    {
                        SolicitacaoServicoDomainService.AtualizarDescricao(seqSolicitacao, false, true);
                    }
                }

                //Buscando as divisões da turma
                var divisoesTurma = this.DivisaoTurmaDomainService.SearchBySpecification(new DivisaoTurmaFilterSpecification() { SeqTurma = registro.Seq }).ToList();

                //Se está criando a turma...
                if (turma.Seq == 0)
                {
                    //Buscando a restrição matriz principal
                    var restricaoTurmaMatrizPrincipal = turma.ConfiguracoesComponente.SelectMany(a => a.RestricoesTurmaMatriz).FirstOrDefault(a => a.OfertaMatrizPrincipal);

                    foreach (var divisao in divisoesTurma)
                    {
                        if (restricaoTurmaMatrizPrincipal != null)
                        {
                            var matrizCurricularDivisaoComponente = this.MatrizCurricularDivisaoComponenteDomainService.SearchBySpecification(new MatrizCurricularDivisaoComponenteFilterSpecification() { SeqMatrizCurricularOferta = restricaoTurmaMatrizPrincipal.SeqMatrizCurricularOferta, SeqDivisaoComponente = divisao.SeqDivisaoComponente }).FirstOrDefault();

                            if (matrizCurricularDivisaoComponente != null)
                            {
                                //Criando os registros de configuração de grade para as divisões da turma
                                HistoricoDivisaoTurmaConfiguracaoGrade dominioConfiguracaoGrade = new HistoricoDivisaoTurmaConfiguracaoGrade()
                                {
                                    SeqDivisaoTurma = divisao.Seq,
                                    DataInicio = turma.DataInicioPeriodoLetivo.Value,
                                    AulaSabado = matrizCurricularDivisaoComponente.AulaSabado.GetValueOrDefault(),
                                    TipoDistribuicaoAula = matrizCurricularDivisaoComponente.TipoDistribuicaoAula.GetValueOrDefault(),
                                    TipoPagamentoAula = matrizCurricularDivisaoComponente.TipoPagamentoAula.GetValueOrDefault(),
                                    TipoPulaFeriado = matrizCurricularDivisaoComponente.TipoPulaFeriado.GetValueOrDefault()
                                };

                                this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.SaveEntity(dominioConfiguracaoGrade);
                            }
                        }
                    }
                }
                else
                {
                    //Buscando a restrição matriz principal
                    var restricaoTurmaMatrizPrincipal = turma.ConfiguracoesComponente.SelectMany(a => a.RestricoesTurmaMatriz).FirstOrDefault(a => a.OfertaMatrizPrincipal);

                    foreach (var divisao in divisoesTurma)
                    {
                        var configuracaoesGradeDivisaoTurma = this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.SearchBySpecification(new HistoricoDivisaoTurmaConfiguracaoGradeFilterSpecification() { SeqDivisaoTurma = divisao.Seq }).ToList();

                        if (!configuracaoesGradeDivisaoTurma.Any())
                        {
                            if (restricaoTurmaMatrizPrincipal != null)
                            {
                                var matrizCurricularDivisaoComponente = this.MatrizCurricularDivisaoComponenteDomainService.SearchBySpecification(new MatrizCurricularDivisaoComponenteFilterSpecification() { SeqMatrizCurricularOferta = restricaoTurmaMatrizPrincipal.SeqMatrizCurricularOferta, SeqDivisaoComponente = divisao.SeqDivisaoComponente }).FirstOrDefault();

                                if (matrizCurricularDivisaoComponente != null)
                                {
                                    //Criando os registros de configuração de grade para as divisões da turma
                                    HistoricoDivisaoTurmaConfiguracaoGrade dominioConfiguracaoGrade = new HistoricoDivisaoTurmaConfiguracaoGrade()
                                    {
                                        SeqDivisaoTurma = divisao.Seq,
                                        DataInicio = turma.DataInicioPeriodoLetivo.Value,
                                        AulaSabado = matrizCurricularDivisaoComponente.AulaSabado.GetValueOrDefault(),
                                        TipoDistribuicaoAula = matrizCurricularDivisaoComponente.TipoDistribuicaoAula.GetValueOrDefault(),
                                        TipoPagamentoAula = matrizCurricularDivisaoComponente.TipoPagamentoAula.GetValueOrDefault(),
                                        TipoPulaFeriado = matrizCurricularDivisaoComponente.TipoPulaFeriado.GetValueOrDefault()
                                    };

                                    this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.SaveEntity(dominioConfiguracaoGrade);
                                }
                            }
                        }
                    }

                    var divisoesTurmaParaAtualizar = turma.DivisoesTurma.Where(a => a.LimparNumeroGrupoEdicao.HasValue && a.LimparNumeroGrupoEdicao.Value).ToList();

                    foreach (var divisao in divisoesTurmaParaAtualizar)
                    {
                        /*Atualizando os números dos grupos na mão porque quando um grupo é excluído dá erro nesse número
                        e o banco não deixa salvar*/
                        var divisaoBanco = divisoesTurma.FirstOrDefault(a => a.Seq == divisao.Seq);
                        divisaoBanco.NumeroGrupo = divisao.NumeroGrupoValidado.Value;
                        this.DivisaoTurmaDomainService.SaveEntity(divisaoBanco);
                    }
                }

                transacao.Commit();
                return registro.Seq;
            }
        }

        private void AtualizaOfertaMatriz(TurmaVO turma, out List<long> listaSolicitacoesAtualizarDescricaoOriginal, out List<long> listaSolicitacoesAtualizarDescricaoAtualizada)
        {
            listaSolicitacoesAtualizarDescricaoOriginal = new List<long>();
            listaSolicitacoesAtualizarDescricaoAtualizada = new List<long>();

            // Na manutenção de turma, ao incluir uma oferta de matriz:
            if (turma.Seq > 0)
            {
                foreach (var turmaOfertaMatriz in turma.TurmaOfertasMatriz)
                {
                    foreach (var oferta in turmaOfertaMatriz.OfertasMatriz)
                    {
                        if (oferta.Seq == 0)
                        {
                            // Recupera a configuracao de componente desta oferta
                            if (oferta.SeqTurmaConfiguracaoComponente == 0)
                            {
                                oferta.SeqTurmaConfiguracaoComponente = TurmaConfiguracaoComponenteDomainService.SearchProjectionByKey(new TurmaConfiguracaoComponenteFilterSpecification
                                {
                                    SeqTurma = turma.Seq,
                                    SeqConfiguracaoComponente = turmaOfertaMatriz.SeqConfiguracaoComponente
                                }, x => x.Seq);
                            }
                            var seqConfiguracaoComponente = TurmaConfiguracaoComponenteDomainService.SearchProjectionByKey(oferta.SeqTurmaConfiguracaoComponente, x => x.SeqConfiguracaoComponente);

                            if (seqConfiguracaoComponente == 0)
                            {
                                seqConfiguracaoComponente = turmaOfertaMatriz.SeqConfiguracaoComponente;
                            }

                            // Recupera os seqs das divisões de turma
                            var seqsDivisoesTurma = turma.DivisoesTurma.Where(t => t.Seq > 0).Select(t => t.Seq).Distinct();

                            // 1. Verificar se a turma está em plano de estudo atual de algum aluno.
                            var dadosPlanoEstudo = PlanoEstudoDomainService.SearchProjectionBySpecification(new PlanoEstudoFilterSpecification
                            {
                                SeqsDivisaoTurma = seqsDivisoesTurma.ToArray(),
                                Atual = true,
                                SeqCicloLetivo = turma.SeqCicloLetivoInicio
                            }, x => new
                            {
                                SeqPlanoEstudo = x.Seq,
                                x.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno,
                                x.SeqMatrizCurricularOferta,
                                Itens = x.Itens.Select(pi => new
                                {
                                    pi.Seq,
                                    pi.SeqConfiguracaoComponente,
                                    pi.SeqDivisaoTurma,
                                    pi.SeqOrientacao,
                                    pi.SeqPlanoEstudo,
                                }),
                                x.SeqAlunoHistoricoCicloLetivo
                            }).Distinct().ToList();

                            if (dadosPlanoEstudo != null && dadosPlanoEstudo.Any())
                            {
                                // 1.1 Caso estiver, verificar se a oferta de matriz incluída está associada ao plano atual do aluno.
                                foreach (var planoEstudo in dadosPlanoEstudo.Where(p => p.SeqMatrizCurricularOferta.HasValue))
                                {
                                    // 1.1.1. Caso pertencer
                                    if (planoEstudo.SeqMatrizCurricularOferta == oferta.SeqMatrizCurricularOferta)
                                    {
                                        // 1.1.1.1. Atualizar o indicador de atual do plano para “não”;
                                        PlanoEstudoDomainService.UpdateFields(new PlanoEstudo { Seq = planoEstudo.SeqPlanoEstudo, Atual = false }, x => x.Atual);

                                        // 1.1.1.2. Criar um novo registro de plano de estudo, copiando todas as informações do plano que era o atual, com o indicador de atual com valor “sim” e com a
                                        // observação: “Plano criado devido a alteração da oferta de matriz na turma {turma.Codigo}.{turma.Numero}.”;

                                        var salvarPlanoEstudo = true;

                                        var planoEstudoNovo = new PlanoEstudo()
                                        {
                                            SeqAlunoHistoricoCicloLetivo = planoEstudo.SeqAlunoHistoricoCicloLetivo,
                                            SeqMatrizCurricularOferta = planoEstudo.SeqMatrizCurricularOferta,
                                            Atual = true,
                                            Observacao = $"Plano criado devido a alteração da oferta de matriz na turma {turma.Codigo}.{turma.Numero}."
                                        };

                                        // 1.1.1.3. Copiar todos os itens do plano de estudo que era o atual, atualizando o item correspondente à turma que está 
                                        // sendo alterada, com a configuração de componente correspondente à oferta de matriz incluída.
                                        // Realizar atualização para todos os alunos encontrados.  
                                        planoEstudoNovo.Itens = new List<PlanoEstudoItem>();
                                        foreach (var itemPlanoEstudo in planoEstudo.Itens)
                                        {
                                            var novoItem = new PlanoEstudoItem();
                                            novoItem.SeqDivisaoTurma = itemPlanoEstudo.SeqDivisaoTurma;
                                            novoItem.SeqOrientacao = itemPlanoEstudo.SeqOrientacao;
                                            novoItem.SeqConfiguracaoComponente = itemPlanoEstudo.SeqConfiguracaoComponente;

                                            // Caso seja a divisão desta turma que estou autalizando, mudo o configuração componente
                                            if (itemPlanoEstudo.SeqDivisaoTurma.HasValue && seqsDivisoesTurma.Contains(itemPlanoEstudo.SeqDivisaoTurma.Value))
                                            {
                                                if (novoItem.SeqConfiguracaoComponente == seqConfiguracaoComponente)
                                                    salvarPlanoEstudo = false;

                                                novoItem.SeqConfiguracaoComponente = seqConfiguracaoComponente;
                                            }

                                            planoEstudoNovo.Itens.Add(novoItem);
                                        }

                                        #region Comentando o trecho que verifica se é para salvar um plano pois se o plano de estudo tiver a matriz incluída associada, de qualquer forma deve ser feita a cópia do plano para um novo

                                        //if (salvarPlanoEstudo)
                                        //    PlanoEstudoDomainService.SaveEntity(planoEstudoNovo);

                                        #endregion

                                        PlanoEstudoDomainService.SaveEntity(planoEstudoNovo);
                                    }
                                }
                            }

                            // 2. Verificar se a turma está em uma solicitação de serviço ativa*.
                            var dadosSolicitacoesExistentes = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(new SolicitacaoMatriculaItemFilterSpecification()
                            {
                                SeqsDivisoesTurma = seqsDivisoesTurma.ToList(),
                                SolicitacaoMatriculaAtiva = true
                            }, x => new
                            {
                                x.Seq,
                                x.SeqSolicitacaoMatricula,
                                TipoAtuacao = x.SolicitacaoMatricula.PessoaAtuacao.TipoAtuacao,
                                x.SeqConfiguracaoComponente,
                                SeqMatrizCurricularOfertaIngressante = (long?)(x.SolicitacaoMatricula.PessoaAtuacao as Ingressante).SeqMatrizCurricularOferta,
                                SeqMatrizCurricularOfertaAluno = (long?)(x.SolicitacaoMatricula.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == turma.SeqCicloLetivoInicio).PlanosEstudo.FirstOrDefault(p => p.Atual).SeqMatrizCurricularOferta,
                                x.SolicitacaoMatricula.DescricaoOriginal,
                                x.SolicitacaoMatricula.DescricaoAtualizada,
                            }).ToList();

                            if (dadosSolicitacoesExistentes != null && dadosSolicitacoesExistentes.Any())
                            {
                                foreach (var itemSolicitacao in dadosSolicitacoesExistentes)
                                {
                                    long? seqMatrizCurricularOfertaConsiderar = null;
                                    if (itemSolicitacao.TipoAtuacao == TipoAtuacao.Ingressante)
                                        seqMatrizCurricularOfertaConsiderar = itemSolicitacao.SeqMatrizCurricularOfertaIngressante;
                                    else
                                        seqMatrizCurricularOfertaConsiderar = itemSolicitacao.SeqMatrizCurricularOfertaAluno;

                                    if (oferta.SeqMatrizCurricularOferta == seqMatrizCurricularOfertaConsiderar)
                                    {
                                        // Caso pertencer, atualizar o item da solicitação com o sequencial da configuração incluída. Realizar atualização para todas as solicitações encontradas.
                                        if (itemSolicitacao.SeqConfiguracaoComponente != seqConfiguracaoComponente)
                                            SolicitacaoMatriculaItemDomainService.UpdateFields(new SolicitacaoMatriculaItem { Seq = itemSolicitacao.Seq, SeqConfiguracaoComponente = seqConfiguracaoComponente }, x => x.SeqConfiguracaoComponente);

                                        // Atualizar a descrição da solicitação e a descrição atualizada conforme RN_MAT_114 – Solicitação – Descrição original/atualizada
                                        if (!string.IsNullOrEmpty(itemSolicitacao.DescricaoOriginal))
                                        {
                                            SolicitacaoServicoDomainService.AtualizarDescricao(itemSolicitacao.SeqSolicitacaoMatricula, true, true);
                                            listaSolicitacoesAtualizarDescricaoOriginal.Add(itemSolicitacao.SeqSolicitacaoMatricula);
                                        }

                                        if (!string.IsNullOrEmpty(itemSolicitacao.DescricaoAtualizada))
                                        {
                                            SolicitacaoServicoDomainService.AtualizarDescricao(itemSolicitacao.SeqSolicitacaoMatricula, false, true);
                                            listaSolicitacoesAtualizarDescricaoAtualizada.Add(itemSolicitacao.SeqSolicitacaoMatricula);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AtualizaConfiguracaoComponente(TurmaVO turma)
        {
            // Na manutenção de turma, ao incluir uma configuração de componente:
            if (turma.Seq > 0)
            {
                foreach (var configuracao in turma.ConfiguracoesComponente)
                {
                    if (configuracao.Seq == 0)
                    {
                        // Recupera os seqs das divisões de turma
                        var seqsDivisoesTurma = turma.DivisoesTurma.Where(t => t.Seq > 0).Select(t => t.Seq).Distinct();

                        // 1. Verificar se a turma está em plano de estudo atual de algum aluno.
                        var dadosPlanoEstudo = PlanoEstudoDomainService.SearchProjectionBySpecification(new PlanoEstudoFilterSpecification
                        {
                            SeqsDivisaoTurma = seqsDivisoesTurma.ToArray(),
                            Atual = true,
                            SeqCicloLetivo = turma.SeqCicloLetivoInicio
                        }, x => new
                        {
                            SeqPlanoEstudo = x.Seq,
                            x.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno,
                            x.SeqMatrizCurricularOferta,
                            x.Itens,
                            x.SeqAlunoHistoricoCicloLetivo
                        }).Distinct().ToList();

                        if (dadosPlanoEstudo != null && dadosPlanoEstudo.Any())
                        {
                            foreach (var planoEstudo in dadosPlanoEstudo.Where(p => p.SeqMatrizCurricularOferta.HasValue))
                            {
                                // 1.1. Caso estiver, verificar se a configuração de componente incluída pertence a oferta de matriz associada ao plano atual do aluno.
                                var seqsConfiguracoesComponenteAluno = MatrizCurricularOfertaDomainService.SearchProjectionByKey(planoEstudo.SeqMatrizCurricularOferta.Value, x => x.MatrizCurricular.ConfiguracoesComponente.Select(c => c.SeqConfiguracaoComponente)).ToList();

                                // 1.1.1. Caso pertencer
                                if (seqsConfiguracoesComponenteAluno.Contains(configuracao.SeqConfiguracaoComponente))
                                {
                                    // 1.1.1.1. Atualizar o indicador de atual do plano para “não”;
                                    PlanoEstudoDomainService.UpdateFields(new PlanoEstudo { Seq = planoEstudo.SeqPlanoEstudo, Atual = false }, x => x.Atual);

                                    // 1.1.1.2. Criar um novo registro de plano de estudo, copiando todas as informações do plano que era o atual, com o indicador de atual com valor “sim” e com a
                                    // observação: “Plano criado devido a alteração da configuração de componente na turma {turma.Codigo}.{turma.Numero}.”;

                                    var planoEstudoNovo = new PlanoEstudo()
                                    {
                                        SeqAlunoHistoricoCicloLetivo = planoEstudo.SeqAlunoHistoricoCicloLetivo,
                                        SeqMatrizCurricularOferta = planoEstudo.SeqMatrizCurricularOferta,
                                        Atual = true,
                                        Observacao = $"Plano criado devido a alteração da configuração de componente na turma {turma.Codigo}.{turma.Numero}."
                                    };

                                    /*  1.1.1.3. Copiar todos os itens do plano de estudo que era o atual, atualizando o item correspondente à turma que está sendo alterada, com a configuração de componente incluída.
                                        Realizar atualização para todos os alunos encontrados.  */
                                    planoEstudoNovo.Itens = new List<PlanoEstudoItem>();
                                    foreach (var itemPlanoEstudo in planoEstudo.Itens)
                                    {
                                        var novoItem = new PlanoEstudoItem();
                                        novoItem.SeqDivisaoTurma = itemPlanoEstudo.SeqDivisaoTurma;
                                        novoItem.SeqOrientacao = itemPlanoEstudo.SeqOrientacao;
                                        novoItem.SeqConfiguracaoComponente = itemPlanoEstudo.SeqConfiguracaoComponente;

                                        // Caso seja a divisão desta turma que estou autalizando, mudo o configuração componente
                                        if (itemPlanoEstudo.SeqDivisaoTurma.HasValue && seqsDivisoesTurma.Contains(itemPlanoEstudo.SeqDivisaoTurma.Value))
                                            novoItem.SeqConfiguracaoComponente = configuracao.SeqConfiguracaoComponente;

                                        planoEstudoNovo.Itens.Add(novoItem);
                                    }

                                    PlanoEstudoDomainService.SaveEntity(planoEstudoNovo);
                                }
                            }
                        }

                        // 2. Verificar se a turma está em uma solicitação de serviço.
                        var dadosSolicitacoesExistentes = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(new SolicitacaoMatriculaItemFilterSpecification()
                        {
                            SeqsDivisoesTurma = seqsDivisoesTurma.ToList(),
                            SolicitacaoMatriculaAtiva = true
                        }, x => new
                        {
                            x.Seq,
                            x.SeqSolicitacaoMatricula,
                            TipoAtuacao = x.SolicitacaoMatricula.PessoaAtuacao.TipoAtuacao,
                            x.SeqConfiguracaoComponente,
                            SeqMatrizCurricularOfertaIngressante = (long?)(x.SolicitacaoMatricula.PessoaAtuacao as Ingressante).SeqMatrizCurricularOferta,
                            SeqMatrizCurricularOfertaAluno = (long?)(x.SolicitacaoMatricula.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == turma.SeqCicloLetivoInicio).PlanosEstudo.FirstOrDefault(p => p.Atual).SeqMatrizCurricularOferta,
                            x.SolicitacaoMatricula.DescricaoOriginal,
                            x.SolicitacaoMatricula.DescricaoAtualizada
                        }).ToList();

                        if (dadosSolicitacoesExistentes != null && dadosSolicitacoesExistentes.Any())
                        {
                            foreach (var itemSolicitacao in dadosSolicitacoesExistentes)
                            {
                                long? seqMatrizCurricularOfertaConsiderar = null;
                                if (itemSolicitacao.TipoAtuacao == TipoAtuacao.Ingressante)
                                    seqMatrizCurricularOfertaConsiderar = itemSolicitacao.SeqMatrizCurricularOfertaIngressante;
                                else
                                    seqMatrizCurricularOfertaConsiderar = itemSolicitacao.SeqMatrizCurricularOfertaAluno;

                                // Verificar se a configuração de componente incluída pertence a oferta de matriz associada ao ingressante.
                                var seqsConfiguracoesComponenteIngressante = MatrizCurricularOfertaDomainService.SearchProjectionByKey(seqMatrizCurricularOfertaConsiderar.Value, x => x.MatrizCurricular.ConfiguracoesComponente.Select(c => c.SeqConfiguracaoComponente)).ToList();

                                // Caso pertencer, atualizar o item da solicitação com o sequencial da configuração incluída. Realizar atualização para todas as solicitações encontradas.
                                if (seqsConfiguracoesComponenteIngressante.Contains(configuracao.SeqConfiguracaoComponente))
                                    SolicitacaoMatriculaItemDomainService.UpdateFields(new SolicitacaoMatriculaItem { Seq = itemSolicitacao.Seq, SeqConfiguracaoComponente = configuracao.SeqConfiguracaoComponente }, x => x.SeqConfiguracaoComponente);

                                // Atualizar a descrição da solicitação e a descrição atualizada conforme RN_MAT_114 – Solicitação – Descrição original/atualizada
                                if (!string.IsNullOrEmpty(itemSolicitacao.DescricaoOriginal))
                                    SolicitacaoServicoDomainService.AtualizarDescricao(itemSolicitacao.SeqSolicitacaoMatricula, true, true);

                                if (!string.IsNullOrEmpty(itemSolicitacao.DescricaoAtualizada))
                                    SolicitacaoServicoDomainService.AtualizarDescricao(itemSolicitacao.SeqSolicitacaoMatricula, false, true);
                            }
                        }
                    }
                }
            }
        }

        private void ValidarConfiguracoesComponenteCompartilhados(TurmaVO turma)
        {
            if (turma.GrupoConfiguracoesCompartilhadas == null || turma.GrupoConfiguracoesCompartilhadas.Count == 0)
                turma.ConfiguracoesComponente = new List<TurmaGrupoConfiguracaoVO>();
            else
                turma.ConfiguracoesComponente = turma.GrupoConfiguracoesCompartilhadas.Where(w => w.Selecionado).ToList();

            if (turma.Seq == 0)
            {
                //Adicionar a configuração principal na lista
                turma.ConfiguracoesComponente.Add(new TurmaGrupoConfiguracaoVO()
                {
                    SeqConfiguracaoComponente = turma.ConfiguracaoComponente,
                    Principal = true,
                });
            }
            else
            {
                var configuracaoPrincipal = TurmaConfiguracaoComponenteDomainService.BuscarConfiguracaoPrincipalTurma(turma.Seq, turma.ConfiguracaoComponente);

                //Adicionar a configuração principal na lista
                turma.ConfiguracoesComponente.Add(configuracaoPrincipal);
            }
        }

        private void PreencherOfertasMatrizConfiguracoesComponente(TurmaVO turma)
        {
            var siglasEntidadesResponsaveisOfertasMatriz = ConfiguracaoComponenteDomainService.BuscarSiglasEntidadeResponsavelOfertasMatrizTurma(turma);
            var descricaoComponenteCurricularAssunto = ComponenteCurricularDomainService.BuscarDescricaoComponenteCurricularAssunto(turma.SeqComponenteCurricularAssunto);
            foreach (var configuracao in turma.ConfiguracoesComponente)
            {
                configuracao.Descricao = ConfiguracaoComponenteDomainService.GerarDescricaoConfiguracaoComponenteTurma(configuracao.SeqConfiguracaoComponente, descricaoComponenteCurricularAssunto, siglasEntidadesResponsaveisOfertasMatriz);

                var ofertasConfiguracao = turma.TurmaOfertasMatriz.Where(w => w.SeqConfiguracaoComponente == configuracao.SeqConfiguracaoComponente).SingleOrDefault();
                if (ofertasConfiguracao != null && ofertasConfiguracao.OfertasMatriz.Count > 0)
                {
                    configuracao.RestricoesTurmaMatriz = new List<RestricaoTurmaMatrizVO>();
                    ofertasConfiguracao.OfertasMatriz.ForEach(f =>
                    {
                        var OfertaMatrizSeqCursoOfertaLocalidadeTurno = MatrizCurricularOfertaDomainService.BuscarMatrizCurricularOfertaSeqCursoOfertaLocalidadeTurno(f.SeqMatrizCurricularOferta.Value, true);
                        if (OfertaMatrizSeqCursoOfertaLocalidadeTurno == 0) { throw new Exception($"Oferta de Matriz Curricular não encontrada: {f.SeqMatrizCurricularOferta.Value}"); }

                        var restricao = new RestricaoTurmaMatrizVO();
                        restricao.Seq = f.Seq;
                        restricao.OfertaMatrizPrincipal = f.OfertaMatrizPrincipal;
                        restricao.SeqMatrizCurricularOferta = f.SeqMatrizCurricularOferta;
                        restricao.SeqCursoOfertaLocalidadeTurno = OfertaMatrizSeqCursoOfertaLocalidadeTurno;
                        restricao.QuantidadeVagasReservadas = f.ReservaVagas ?? 0;
                        restricao.QuantidadeVagasOcupadas = f.QuantidadeVagasOcupadas ?? 0;
                        //Step 6 - Componente assunto
                        //Toda oferta tem o mesmo componente assunto
                        restricao.SeqComponenteCurricularAssunto = turma.SeqComponenteCurricularAssunto;
                        configuracao.RestricoesTurmaMatriz.Add(restricao);
                    });
                }
            }
        }

        private void ValidacaoPersistirDivisoesTurma(TurmaVO turma)
        {
            var divisaoTela = turma.DivisoesTurma;
            int maxNumeroGrupo = 0;

            if (turma.Seq > 0)
            {
                /*Verificando qual o maior numero de grupo, pois várias divisões podem ter sido excluidas ou adicionadas
                Se divisões forem excluidas tem que considerar o ultimo numero que está no banco
                Se divisões forem adicionadas tem que considerar o ultimo numero que está na tela*/
                var listaNumerosGrupos = new List<short>();
                listaNumerosGrupos.Add(turma.DivisoesTurma.Max(a => a.NumeroGrupo));

                foreach (var numeroGrupoTela in turma.TurmaDivisoes.SelectMany(a => a.DivisoesComponentes).Select(a => a.GrupoNumero))
                {
                    /*Como no objeto de turma divisões o grupo numero é uma string, os valores tem que ser convertidos
                     e adicionados na lista de short para recuperar o max*/
                    var numeroGrupoConvertido = Convert.ToInt16(numeroGrupoTela);
                    listaNumerosGrupos.Add(numeroGrupoConvertido);
                }

                maxNumeroGrupo = listaNumerosGrupos.Max();

                //Validando quais divisões de turma foram excluídas para excluir o histórico configuracao de grade
                var sequenciaisDivisoesTela = turma.TurmaDivisoes.SelectMany(a => a.DivisoesComponentes).Select(a => a.Seq).Where(a => a != 0).ToList();
                var divisoesExcluidas = turma.DivisoesTurma.Where(a => a.Seq != 0 && !sequenciaisDivisoesTela.Contains(a.Seq)).ToList();

                foreach (var divisaoExcluida in divisoesExcluidas)
                {
                    var divisaoTurmaExcluir = this.DivisaoTurmaDomainService.SearchByKey(new SMCSeqSpecification<DivisaoTurma>(divisaoExcluida.Seq), x => x.HistoricosConfiguracaoGrade);

                    foreach (var configuracaoGrade in divisaoTurmaExcluir.HistoricosConfiguracaoGrade)
                    {
                        if (divisaoTurmaExcluir.SeqHistoricoConfiguracaoGradeAtual == configuracaoGrade.Seq)
                        {
                            divisaoTurmaExcluir.SeqHistoricoConfiguracaoGradeAtual = null;
                            this.DivisaoTurmaDomainService.SaveEntity(divisaoTurmaExcluir);
                        }

                        this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.DeleteEntity(configuracaoGrade);
                    }
                }
            }

            turma.DivisoesTurma = new List<DivisaoTurmaVO>();

            var divisaoFiltro = new DivisaoMatrizCurricularComponenteFilterSpecification
            {
                SeqConfiguracaoComponente = turma.ConfiguracaoComponente
            };
            //divisaoFiltro.SeqMatrizCurricularOferta = parametroSelecionado.SeqOfertaMatriz;

            var consultaDivisoes = DivisaoComponenteDomainService.SearchProjectionBySpecification(new DivisaoComponenteFilterSpecification
            {
                SeqConfiguracaoComponente = turma.ConfiguracaoComponente
            }, x => new
            {
                SeqDivisaoComponente = x.Seq,
                x.SeqConfiguracaoComponente,
            }).ToList();

            //var consultaDivisoes = DivisaoMatrizCurricularComponenteDomainService.BuscarDivisaoMatrizCurricularComponente(divisaoFiltro);

            var parametros = turma.TurmaParametros.SelectMany(s => s.ParametrosOfertas).Where(w => w.Selecionado).Single();
            var divisaoOfertaMatrizPrincipal = parametros.DivisoesComponente;
            var divisaoOfertaMatriz = divisaoOfertaMatrizPrincipal.FirstOrDefault();
            var seqOfertaMatrizPrincipal = BuscarSeqOfertaMatrizPrincipal(turma);

            foreach (var divisao in turma.TurmaDivisoes)
            {
                var divisaoBanco = divisaoTela.Where(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente).FirstOrDefault();
                var divisaoConfig = consultaDivisoes.Where(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente).Single();

                //Origem avaliação da Divisão da turma
                var divisaoComponenteMatriz = parametros.DivisoesComponente.Where(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente).FirstOrDefault();

                if (divisaoComponenteMatriz == null)
                {
                    /* Se a divisão componente estiver vazia, pode ser que o parâmetro selecionado seja de um componente
                    compartilhado, que não tenha as mesmas divisões de turma da turma em questão. Sendo assim, teria que pegar
                    um dos parâmetros que seja da turma em questão (que seja da configuração de componente principal da turma),
                    mesmo que não seja o selecionado. */
                    var seqConfiguracaoComponentePrincipal = turma.ConfiguracoesComponente.FirstOrDefault(a => a.Principal).SeqConfiguracaoComponente;
                    divisaoComponenteMatriz = turma.TurmaParametros.Where(a => a.SeqConfiguracaoComponente == seqConfiguracaoComponentePrincipal).SelectMany(s => s.ParametrosOfertas).SelectMany(a => a.DivisoesComponente).FirstOrDefault(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente);
                }

                foreach (var item in divisao.DivisoesComponentes)
                {
                    var numeroGrupo = Convert.ToInt16(item.GrupoNumero);
                    var divisaoItem = divisaoTela.Where(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente && w.NumeroGrupo == numeroGrupo).FirstOrDefault();

                    var origemAvaliacao = CriarOrigemAvaliacaoDivisaoTurma(item.Seq, turma.Operacao, seqOfertaMatrizPrincipal, divisaoItem, divisaoComponenteMatriz);

                    //Condição criada para evitar null no campo QuantidadeVagasOcupadas | Task 52168 | BUG_UC_TUR_001_01
                    short qtdVagasOcupadas = 0;

                    if (divisaoItem != null)
                    {
                        if (divisaoItem.QuantidadeVagasOcupadas.HasValue)
                        {
                            qtdVagasOcupadas = divisaoItem.QuantidadeVagasOcupadas.Value;
                        }
                    }

                    var registroDivisaoTurma = new DivisaoTurmaVO
                    {
                        Seq = divisaoItem == null ? 0 : divisaoItem.Seq,
                        SeqDivisaoComponente = divisaoConfig.SeqDivisaoComponente,
                        NumeroGrupo = Convert.ToInt16(item.GrupoNumero),
                        QuantidadeVagas = item.DivisaoVagas.Value,
                        QuantidadeVagasOcupadas = qtdVagasOcupadas,
                        SeqLocalidade = item.SeqLocalidade == 0 ? (long?)null : item.SeqLocalidade,
                        OrigemAvaliacao = origemAvaliacao,
                        InformacoesAdicionais = item.InformacoesAdicionais
                    };

                    /*Se for a edição da turma tem que pegar o sequencial atualizado que está no mestre detalhes de divisão,
                    porque algum registro do objeto turma.DivisoesTurma pode ter sido excluido, e não é encontrado o item correto
                    buscando pelo numero do grupo, pois o grupo em questão pode ter sido excluido e o numero do grupo foi para outra divisão*/
                    if (turma.Seq > 0)
                    {
                        registroDivisaoTurma.Seq = item.Seq;
                        registroDivisaoTurma.QuantidadeVagasOcupadas = item.QuantidadeVagasOcupadas ?? 0;
                        var divisaoCompleta = divisaoTela.FirstOrDefault(a => a.Seq == item.Seq);
                        registroDivisaoTurma.SeqHistoricoConfiguracaoGradeAtual = divisaoCompleta?.SeqHistoricoConfiguracaoGradeAtual;

                        /*Se algum registro foi excluido, então a divisão posterior ao grupo que foi excluido vai ficar com o número
                        do grupo da divisão excluida, e então o numero do sequencial vai ser diferente, pois o registro com o numero do grupo
                        no objeto divisaoItem não existe mais na tela. Quando acontece esse cenário é preciso setar esse campo para
                        atualizar os números para conseguir salvar no banco.
                        O item atual está substituindo outra divisão que foi excluída.*/
                        if (divisaoItem != null && item.Seq != 0 && divisaoItem.Seq != item.Seq)
                        {
                            registroDivisaoTurma.LimparNumeroGrupoEdicao = true;
                            registroDivisaoTurma.NumeroGrupo = Convert.ToInt16(++maxNumeroGrupo);
                            registroDivisaoTurma.NumeroGrupoValidado = Convert.ToInt16(item.GrupoNumero);
                        }
                    }

                    turma.DivisoesTurma.Add(registroDivisaoTurma);
                }
            }
        }

        /// <summary>
        /// Registra o histórico de turma, criando ou atualizando o histórico de
        /// situação e Historico fechamento diário
        /// </summary>
        /// <param name="turma"></param>
        private void RegistrarHistoricos(TurmaVO turma)
        {
            // Se está criando a turma...
            if (turma.Seq == 0)
            {
                // Criar registro na tabela histórico com a situação definida
                turma.HistoricosSituacao = new List<TurmaHistoricoSituacaoVO>();
                TurmaHistoricoSituacaoVO historicoInicial = new TurmaHistoricoSituacaoVO();
                historicoInicial.SituacaoTurma = turma.SituacaoTurmaAtual;
                turma.HistoricosSituacao.Add(historicoInicial);

                // Criar registro na tabela histórico do diário com a situação definida
                turma.HistoricosFechamentoDiario = new List<TurmaHistoricoFechamentoDiarioVO>();
                TurmaHistoricoFechamentoDiarioVO fechamentoInicial = new TurmaHistoricoFechamentoDiarioVO();
                fechamentoInicial.DiarioFechado = false;
                turma.HistoricosFechamentoDiario.Add(fechamentoInicial);
            }
        }

        /// <summary>
        /// Verificar se o número da turma é igual a 1, verificar se estão exatamente iguais:
        /// - Associação de ofertas de matriz;
        /// - Associação de configuração de componente principal;
        /// - Associação de configurações de componente para compartilhamento;
        /// - Associação de assunto de componente;
        /// - Ciclo letivo.
        /// </summary>
        /// <param name="turma"></param>
        private void ValidarDuplicidadeTurma(TurmaVO turma)
        {
            if (turma.Numero == 1)
            {
                if (turma.GrupoConfiguracoesCompartilhadas == null) { turma.GrupoConfiguracoesCompartilhadas = new List<TurmaGrupoConfiguracaoVO>(); }
                turma.GrupoConfiguracoesCompartilhadas = turma.GrupoConfiguracoesCompartilhadas.Where(x => x.Selecionado).ToList();

                // As configurações compartilhadas devem ser comparadas, independentemente da Principal
                if (!turma.GrupoConfiguracoesCompartilhadas.SMCAny(x => x.SeqConfiguracaoComponente == turma.ConfiguracaoComponente))
                {
                    // Associação de configuração de componente principal;
                    turma.GrupoConfiguracoesCompartilhadas.Add(new TurmaGrupoConfiguracaoVO() { SeqConfiguracaoComponente = turma.ConfiguracaoComponente });
                }

                // Busco todos os seqs das turmas que possuem o mesmo Ciclo letivo
                var spec = new TurmaFilterSpecification()
                {
                    Numero = 1,
                    SeqCicloLetivoInicio = turma.SeqCicloLetivoInicio,
                    SeqsConfiguracoesComponentes = turma.GrupoConfiguracoesCompartilhadas.Select(x => x.SeqConfiguracaoComponente).ToList(),
                    SeqsMatrizCurricularOferta = turma.TurmaOfertasMatriz.SelectMany(o => o.OfertasMatriz.Select(x => x.SeqMatrizCurricularOferta.Value)).ToList(),
                    SeqComponenteCurricularAssunto = turma.SeqComponenteCurricularAssunto
                };
                var seqsTurma = SearchProjectionBySpecification(spec, x => x.Seq).ToList();

                if (!seqsTurma.SMCAny()) { return; }

                foreach (var seqTurma in seqsTurma.Where(x => x != turma.Seq))
                {
                    var turmaBase = BuscarTurma(seqTurma);

                    //  - Ciclo letivo.
                    if (turma.SeqCicloLetivoInicio != turmaBase.SeqCicloLetivoInicio) { continue; }

                    //- Associação de assunto de componente;
                    if (turma.SeqComponenteCurricularAssunto != turmaBase.SeqComponenteCurricularAssunto) { continue; }

                    if (turmaBase.GrupoConfiguracoesCompartilhadas == null) { turmaBase.GrupoConfiguracoesCompartilhadas = new List<TurmaGrupoConfiguracaoVO>(); }

                    // As configurações compartilhadas devem ser comparadas, independentemente de qual seja a Principal
                    if (!turmaBase.GrupoConfiguracoesCompartilhadas.SMCAny(x => x.SeqConfiguracaoComponente == turmaBase.ConfiguracaoComponente))
                    {
                        // Associação de configuração de componente principal;
                        turmaBase.GrupoConfiguracoesCompartilhadas.Add(new TurmaGrupoConfiguracaoVO() { SeqConfiguracaoComponente = turmaBase.ConfiguracaoComponente });
                    }
                    //- Associação de configurações de componente para compartilhamento;
                    if (turma.GrupoConfiguracoesCompartilhadas.Count == turmaBase.GrupoConfiguracoesCompartilhadas.Count)
                    {
                        foreach (var configuracaoCompatilhada in turma.GrupoConfiguracoesCompartilhadas)
                        {
                            var configuracaoCompatilhadaOrigem = turmaBase.GrupoConfiguracoesCompartilhadas.FirstOrDefault(x => x.SeqConfiguracaoComponente == configuracaoCompatilhada.SeqConfiguracaoComponente);
                            if (configuracaoCompatilhadaOrigem == null) { continue; }
                        }
                    }
                    else { continue; }
                    bool igual = true;
                    if (turma.TurmaOfertasMatriz.Count == turmaBase.TurmaOfertasMatriz.Count)
                    {
                        //  - Associação de ofertas de matriz;
                        foreach (var turmaOferta in turma.TurmaOfertasMatriz)
                        {
                            var turmaOfertaOrigem = turmaBase.TurmaOfertasMatriz.Where(t => t.SeqComponenteCurricular == turmaOferta.SeqComponenteCurricular).FirstOrDefault();
                            if (turmaOfertaOrigem != null && turmaOfertaOrigem.OfertasMatriz.SMCAny() && turmaOferta.OfertasMatriz.SMCAny())
                            {
                                if (turmaOfertaOrigem.OfertasMatriz.Count == turmaOferta.OfertasMatriz.Count)
                                {
                                    foreach (var ofertaMatriz in turmaOferta.OfertasMatriz)
                                    {
                                        var ofertaMatrizOrigem = turmaOfertaOrigem.OfertasMatriz.FirstOrDefault(c => c.SeqMatrizCurricularOferta == ofertaMatriz.SeqMatrizCurricularOferta);

                                        if (ofertaMatrizOrigem == null) { igual = false; continue; }
                                    }
                                }
                                else { igual = false; continue; }
                            }
                            else { igual = false; continue; }
                        }
                    }
                    else { continue; }

                    if (igual)
                    { /*Se estiverem iguais, abortar a operação e exibir a seguinte mensagem de erro:"*/
                        throw new TurmaDuplicadaException(turmaBase.CodigoFormatado);
                    }
                }
            }
        }

        private void CriarAgenda(TurmaVO turma, DateTime dataInicioPeriodoLetivo, DateTime dataFimPeriodoLetivo)
        {
            //A agenda está sendo somente criada e não está sendo atualizada porque a agenda é criada com o 
            //periodo letivo global, que é maior, e não somente com o periodo das aulas, que é o que fica na turma.
            //Sendo assim, não há a necessidade de atualizar a agenda pois ela sempre vai englobar o período da turma, 
            //e não é possível alterar um ciclo letivo de uma turma depois da mesma estar criada**

            //Se está criando a turma...
            if (turma.Seq == 0)
            {
                var seqConfiguracaoComponente = turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().SeqConfiguracaoComponente;
                var configuracaoComponente = this.ConfiguracaoComponenteDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoComponente>(seqConfiguracaoComponente), x => x.ComponenteCurricular.NiveisEnsino);
                var seqNivelEnsinoResponsável = configuracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino;

                var instituicaoNivelCalendario = this.InstituicaoNivelCalendarioDomainService.SearchBySpecification(new InstituicaoNivelCalendarioSpecification()
                {
                    SeqNivelEnsino = seqNivelEnsinoResponsável,
                    UsoCalendario = UsoCalendario.Turma
                }).ToList().FirstOrDefault();

                if (instituicaoNivelCalendario != null)
                {
                    var descricaoTurmaConfiguracaoComponente = turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().Descricao;
                    var descricaoAgenda = $"{turma.Codigo}.{turma.Numero} - {descricaoTurmaConfiguracaoComponente}";
                    var seqCalendario = instituicaoNivelCalendario.SeqCalendarioAgd;

                    long seqAgenda = this.AgendaService.SalvarAgenda(new AgendaData()
                    {
                        Descricao = descricaoAgenda,
                        SeqCalendario = seqCalendario,
                        DataInicio = dataInicioPeriodoLetivo, //turma.DataInicioPeriodoLetivo,
                        DataFim = dataFimPeriodoLetivo, //turma.DataFimPeriodoLetivo,
                        AgendaPublica = false,
                        OcultarDetalhes = false,
                        AgendaPessoal = false,
                        EnviaEmailCompromissoDiario = false,
                        PermiteEventoParticipanteExterno = false,
                        AssociarTabelaHorarioPadrao = true,
                        DataInicioTabelaHorario = dataInicioPeriodoLetivo,
                        DataFimTabelaHorario = dataFimPeriodoLetivo,
                        BloqueioEventoOutroHorario = false
                    });

                    turma.SeqAgendaTurma = seqAgenda;
                }
            }
        }

        public void CargaAgendasTurma(long seqCicloLetivo)
        {
            var listaTurmasAtualizar = new List<Turma>();
            var turmas = this.SearchBySpecification(new TurmaFilterSpecification() { SeqCicloLetivoInicio = seqCicloLetivo }, x => x.ConfiguracoesComponente[0].RestricoesTurmaMatriz).ToList();

            foreach (var turma in turmas)
            {
                var turmaAuxiliar = turma.Transform<TurmaVO>();
                turmaAuxiliar.Seq = 0;

                //Recuperando as datas do período letivo considerando que o curso NÃO é de oferta temporal, pois essa validação será implementada futuramente
                var seqsCursoOfertaLocalidadeTurno = turma.ConfiguracoesComponente.SelectMany(a => a.RestricoesTurmaMatriz).Select(a => a.SeqCursoOfertaLocalidadeTurno).Distinct().ToList();
                DateTime dataInicioPeriodoLetivo;
                DateTime dataFimPeriodoLetivo;

                if (seqsCursoOfertaLocalidadeTurno.Count() > 1)
                {
                    List<DateTime> listaDataInicioPeriodoLetivo = new List<DateTime>();
                    List<DateTime> listaDataFimPeriodoLetivo = new List<DateTime>();

                    foreach (var seqCursoOfertaLocalidadeTurno in seqsCursoOfertaLocalidadeTurno)
                    {
                        var eventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(turma.SeqCicloLetivoInicio, seqCursoOfertaLocalidadeTurno, Common.Areas.ALN.Enums.TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                        listaDataInicioPeriodoLetivo.Add(eventoLetivo.DataInicio);
                        listaDataFimPeriodoLetivo.Add(eventoLetivo.DataFim);
                    }

                    dataInicioPeriodoLetivo = listaDataInicioPeriodoLetivo.Min();
                    dataFimPeriodoLetivo = listaDataFimPeriodoLetivo.Max();
                }
                else
                {
                    var eventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(turma.SeqCicloLetivoInicio, seqsCursoOfertaLocalidadeTurno.FirstOrDefault(), Common.Areas.ALN.Enums.TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_LETIVO);
                    dataInicioPeriodoLetivo = eventoLetivo.DataInicio;
                    dataFimPeriodoLetivo = eventoLetivo.DataFim;
                }

                if (!turma.SeqAgendaTurma.HasValue)
                {
                    //Se a turma não tiver agenda, a agenda é criada e setada para a turma
                    CriarAgenda(turmaAuxiliar, dataInicioPeriodoLetivo, dataFimPeriodoLetivo);

                    if (turmaAuxiliar.SeqAgendaTurma.HasValue)
                    {
                        turma.SeqAgendaTurma = turmaAuxiliar.SeqAgendaTurma;
                        turma.ConfiguracoesComponente = null;
                        listaTurmasAtualizar.Add(turma);
                        //this.SaveEntity(turma);
                    }
                }
                else
                {
                    var existeAgenda = this.AgendaService.VerificaExistenciaAgenda(turma.SeqAgendaTurma.Value);

                    //Se a turma tiver agenda, é verificada se essa agenda realmente existe no AGD, se não existe, é criada
                    //nova agenda, e setada para a turma
                    if (!existeAgenda)
                    {
                        CriarAgenda(turmaAuxiliar, dataInicioPeriodoLetivo, dataFimPeriodoLetivo);

                        if (turmaAuxiliar.SeqAgendaTurma.HasValue)
                        {
                            turma.SeqAgendaTurma = turmaAuxiliar.SeqAgendaTurma;
                            turma.ConfiguracoesComponente = null;
                            listaTurmasAtualizar.Add(turma);
                            //this.SaveEntity(turma);
                        }
                    }
                }
            }

            this.BulkSaveEntity(listaTurmasAtualizar);
        }

        private OrigemAvaliacaoVO CriarOrigemAvaliacaoDivisaoTurma(long? seqDivisaoSalvar, OperacaoTurma operacaoTurma, long? seqOfertaMatrizPrincipal, DivisaoTurmaVO divisaoBanco, TurmaParametrosDetalheVO divisaoConfig)
        {
            /*Origem de avaliação da divisão de turma:
              - Critério de aprovação: nulo
              - Quantidade de grupos: da associação da divisão da oferta de matriz com a divisão do componente
              - Quantidade de professores: da associação da divisão da oferta de matriz com a divisão do componente
              - Nota máxima: da associação da divisão da oferta de matriz com a divisão do componente
              - Apuração por frequência: da associação da divisão da oferta de matriz com a divisão do componente
              - Escala de apuração: da associação da divisão da oferta de matriz com a divisão do componente
              - Matriz curricular oferta: da associação da divisão da oferta de matriz com a divisão do componente
              - Tipo origem avaliação: Divisão de turma;
              - Descrição matéria lecionada: nulo.

              Observação1: Em caso de desdobramento de turma os parâmetros da origem de avaliação de turma e da divisão de turma devem ser
              idênticos aos da turma original e não da oferta de matriz

              Observação2: cada turma ou divisão de turma tem uma origem de avaliação exclusiva. Ela nunca pode se repetir, mesmo sendo
              originada de um desdobramento ou cópia de turma.*/
            var origemAvaliacao = new OrigemAvaliacaoVO();

            switch (operacaoTurma)
            {
                // Novo\Editar
                case OperacaoTurma.Novo:
                case OperacaoTurma.Copiar:

                    if (!seqOfertaMatrizPrincipal.HasValue) { throw new Exception("Oferta de Matriz principal, não encontrada!"); }

                    if (seqDivisaoSalvar.GetValueOrDefault() > 0 && this.OrigemAvaliacaoDomainService.AlteracaoManualOrigemAvaliacao(divisaoBanco.SeqOrigemAvaliacao))
                        return origemAvaliacao = null;

                    //Critério de aprovação
                    origemAvaliacao.SeqCriterioAprovacao = null;
                    //Caso esteja editando uma turma
                    origemAvaliacao.Seq = seqDivisaoSalvar.GetValueOrDefault() > 0 ? divisaoBanco?.SeqOrigemAvaliacao ?? 0 : 0;
                    // Escala de apuração
                    origemAvaliacao.SeqEscalaApuracao = divisaoConfig.SeqEscalaApuracao;
                    // Quantidade de grupos
                    origemAvaliacao.QuantidadeGrupos = divisaoConfig.QuantidadeGrupos;
                    // Quantidade de professores
                    origemAvaliacao.QuantidadeProfessores = divisaoConfig.QuantidadeProfessores;
                    // Nota máxima
                    origemAvaliacao.NotaMaxima = divisaoConfig.NotaMaxima;
                    // Apuração por frequência
                    origemAvaliacao.ApurarFrequencia = divisaoConfig.ApurarFrequencia;
                    // Tipo origem avaliação
                    origemAvaliacao.TipoOrigemAvaliacao = TipoOrigemAvaliacao.DivisaoTurma;
                    // Matriz curricular oferta
                    origemAvaliacao.SeqMatrizCurricularOferta = seqOfertaMatrizPrincipal;
                    // Matéria lecionada obrigatória:
                    origemAvaliacao.MateriaLecionadaObrigatoria = divisaoConfig.MateriaLecionadaObrigatoria;

                    break;

                case OperacaoTurma.Desdobrar:

                    if (divisaoBanco.SeqOrigemAvaliacao != 0 && divisaoBanco.OrigemAvaliacao == null)
                    {
                        origemAvaliacao = OrigemAvaliacaoDomainService.SearchByKey(divisaoBanco.SeqOrigemAvaliacao).Transform<OrigemAvaliacaoVO>();
                    }
                    else
                    {
                        origemAvaliacao = SMCMapperHelper.Create<OrigemAvaliacaoVO>(null, divisaoBanco.OrigemAvaliacao);
                    }

                    origemAvaliacao.Seq = 0;

                    break;
            }

            return origemAvaliacao;
        }

        private long? BuscarSeqOfertaMatrizPrincipal(TurmaVO turma)
        {
            return turma?.TurmaOfertasMatriz?.SelectMany(o => o.OfertasMatriz.Where(z => z.OfertaMatrizPrincipal))?.FirstOrDefault()?.SeqMatrizCurricularOferta;
        }

        /// <summary>
        /// Criar registro de origem avaliação para a turma definindo o critério de aprovação adotado
        /// </summary>
        /// <param name="turma"></param>
        /// <returns></returns>
        private OrigemAvaliacaoVO CriarOrigemAvaliacaoTurma(TurmaVO turma)
        {
            /*Salvar na origem avaliação o seguintes campos da oferta de matriz principal, de acordo com a associação da configuração na restrição da matriz:
                    Origem de avaliação da turma:
                    - Critério de aprovação: associado a oferta de matriz;
                    - Quantidade de grupo: nulo;
                    - Quantidade de professor: nulo;
                    - Nota máxima: do critério da oferta de matriz;
                    - Apuração por frequência: do critério da oferta de matriz;
                    - Escala de apuração : do critério da oferta de matriz;
                    - Matriz curricular oferta: oferta de matriz principal;
                    - Tipo origem avaliação: Turma;
                    - Descrição matéria lecionada: nulo.

            Observação1: Em caso de desdobramento de turma os parâmetros da origem de avaliação de turma e da divisão de turma devem ser
            idênticos aos da turma original e não da oferta de matriz

            Observação2: cada turma ou divisão de turma tem uma origem de avaliação exclusiva. Ela nunca pode se repetir, mesmo sendo
            originada de um desdobramento ou cópia de turma.*/
            var origemAvaliacaoTurma = new OrigemAvaliacaoVO();
            switch (turma.Operacao)
            {
                case OperacaoTurma.Novo:
                case OperacaoTurma.Copiar:

                    if (turma.Seq > 0 && this.OrigemAvaliacaoDomainService.AlteracaoManualOrigemAvaliacao(turma.OrigemAvaliacao.Seq))
                        return origemAvaliacaoTurma = null;

                    var seqOfertaMatrizPrincipal = BuscarSeqOfertaMatrizPrincipal(turma);

                    if (!seqOfertaMatrizPrincipal.HasValue) { throw new Exception("Oferta de Matriz principal, não encontrada!"); }

                    var parametros = turma.TurmaParametros.SelectMany(s => s.ParametrosOfertas).Where(w => w.Selecionado).Single();
                    //Caso esteja editando uma turma
                    origemAvaliacaoTurma.Seq = (turma.Seq > 0) ? turma.OrigemAvaliacao.Seq : 0;
                    // Critério de aprovação
                    origemAvaliacaoTurma.SeqCriterioAprovacao = parametros.SeqCriterioAprovacao;
                    // Quantidade de grupo
                    origemAvaliacaoTurma.QuantidadeGrupos = null;
                    // Quantidade de professor
                    origemAvaliacaoTurma.QuantidadeProfessores = null;
                    // Escala de apuração
                    origemAvaliacaoTurma.SeqEscalaApuracao = parametros.SeqEscalaApuracao == 0 ? (Nullable<long>)null : parametros.SeqEscalaApuracao;
                    //  Apuração por frequência
                    origemAvaliacaoTurma.ApurarFrequencia = parametros.ApurarFrequencia;
                    // Matriz curricular oferta
                    origemAvaliacaoTurma.SeqMatrizCurricularOferta = seqOfertaMatrizPrincipal;
                    // Tipo origem avaliação
                    origemAvaliacaoTurma.TipoOrigemAvaliacao = TipoOrigemAvaliacao.Turma;
                    // Nota máxima
                    if (!string.IsNullOrEmpty(parametros.CriterioNotaMaxima))
                        origemAvaliacaoTurma.NotaMaxima = Convert.ToInt16(parametros.CriterioNotaMaxima);

                    origemAvaliacaoTurma.PermiteAvaliacaoParcial = PreencherPermiteAvaliacaoParcial(turma.SeqCicloLetivoInicio, parametros.SeqCriterioAprovacao);

                    break;

                case OperacaoTurma.Desdobrar:

                    origemAvaliacaoTurma = turma.OrigemAvaliacao;

                    origemAvaliacaoTurma.Seq = 0;

                    break;
            }

            return origemAvaliacaoTurma;
        }

        private bool? PreencherPermiteAvaliacaoParcial(long seqCicloLetivoInicio, long seqCriterioAprovacao)
        {
            /*Origem avaliação turma:
                    * Permite avaliação parcial: se a turma for de um ciclo menor que 2/2020, o valor deve ser "Não".
                    * Se for de um ciclo maior, valor do indicador de apuração da nota do critério de aprovação.
                       Origem avaliação divisão de turma: Permite avaliação parcial: nulo.*/
            var anoParam = int.Parse(ConfigurationManager.AppSettings["AnoCicloInicioAvaliacaoParcial"]?.ToString() ?? "2020");
            var numeroParam = int.Parse(ConfigurationManager.AppSettings["NumeroCicloInicioAvaliacaoParcial"]?.ToString() ?? "02");

            var cicloTurma = CicloLetivoDomainService.SearchProjectionByKey(seqCicloLetivoInicio, x => new
            {
                x.Ano,
                x.Numero
            });

            if ((cicloTurma.Ano == anoParam && cicloTurma.Numero < numeroParam) || cicloTurma.Ano < anoParam)
                return false;
            else
                // Recupera do critério de avaliação
                return CriterioAprovacaoDomainService.SearchProjectionByKey(seqCriterioAprovacao, x => x.ApuracaoNota);
        }

        /// <summary>
        /// Método para configurar o código e número da turma
        /// </summary>
        /// <param name="turma"></param>
        private void AtribuirCodigoNumeroTurma(TurmaVO turma)
        {
            if (turma.Seq > 0) { return; }

            switch (turma.Operacao)
            {
                case OperacaoTurma.Novo:
                case OperacaoTurma.Copiar:

                    var codigoFormatado = GerarCodigoNumeroTurma();

                    var codigoNumero = codigoFormatado.Split('.');
                    if (codigoNumero.Length == 2)
                    {
                        turma.Codigo = Convert.ToInt32(codigoNumero[0]);
                        turma.Numero = Convert.ToInt16(codigoNumero[1]);
                    }

                    break;

                case OperacaoTurma.Desdobrar:

                    turma.Numero = GerarNumeroDesdobramentoTurma(turma.Codigo);

                    break;
            }
        }

        public void ValidarPeriodoLetivoEventoAula(TurmaVO turma)
        {
            /* NV42: Verificar se existe registro de evento aula associada às divisões de turma, no ciclo letivo da turma, e se as datas
            destes eventos estão todos inclusos no período letivo informado para a turma. Se a regra acima não for atendida enviar 
            mensagem de erro: 'Alteração do período letivo não permitido. Existem eventos de aula criado para as divisões da turma e as 
            datas destes eventos estão fora do limite do período letivo que foi informado.' */
            if (turma.Seq != 0)
            {
                var specDivisao = new DivisaoTurmaFilterSpecification() { SeqTurma = turma.Seq };
                var seqsDivisaoTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisao, d => d.Seq).ToList();

                var specEventoAula = new EventoAulaFilterSpecification() { SeqsDivisaoTurma = seqsDivisaoTurma };
                var eventosAulaDivisao = EventoAulaDomainService.SearchBySpecification(specEventoAula);
                eventosAulaDivisao = eventosAulaDivisao.Where(a => !a.DataCancelamento.HasValue).ToList();

                foreach (var eventoAula in eventosAulaDivisao)
                {
                    var dataInicioEventoAula = eventoAula.Data.Add(eventoAula.HoraInicio);
                    var dataFimEventoAula = eventoAula.Data.Add(eventoAula.HoraInicio);

                    if (!(dataInicioEventoAula.Date >= turma.DataInicioPeriodoLetivo.Value.Date && dataFimEventoAula.Date <= turma.DataFimPeriodoLetivo.Value.Date))
                        throw new TurmaEventoAulaPeriodoLetivoException();
                }
            }
        }

        #endregion [ Salvar Turma ]

        #region [ Cancelar turma ]

        /// <summary>
        /// Cancela uma turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma a ser cancelada</param>
        /// <param name="justificativa">Justificativa de cancelamento</param>
        public void CancelarTurma(long seqTurma, string justificativa)
        {
            var dataCancelamento = DateTime.Now;
            var seqsEventosCancelar = new List<long>();

            // Se não informou a justificativa de cancelamento, erro
            if (string.IsNullOrEmpty(justificativa))
            {
                throw new JustificativaCancelamentoNaoInformadaException();
            }

            // Inicia o cancelamento da turma (abre transação)
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                // Atualiza a situação da turma para cancelada
                TurmaHistoricoSituacaoDomainService.AlterarSituacaoTurma(seqTurma, SituacaoTurma.Cancelada, justificativa);



                // Alterar o campo quantidade de vagas ocupadas da restrição de turma por matriz para o valor "0"
                var specRestricao = new RestricaoTurmaMatrizFilterSpecification() { SeqTurma = seqTurma };
                var seqsRestricao = RestricaoTurmaMatrizDomainService.SearchProjectionBySpecification(specRestricao, r => r.Seq);
                foreach (var seqRestricao in seqsRestricao)
                {
                    var restricaoZerada = new RestricaoTurmaMatriz()
                    {
                        Seq = seqRestricao,
                        QuantidadeVagasOcupadas = 0
                    };
                    RestricaoTurmaMatrizDomainService.UpdateFields<RestricaoTurmaMatriz>(restricaoZerada, r => r.QuantidadeVagasOcupadas);
                }

                // Para cada divisão da turma...
                var specDivisao = new DivisaoTurmaFilterSpecification() { SeqTurma = seqTurma };
                var seqsDivisaoTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisao, d => d.Seq);
                foreach (var seqDivisao in seqsDivisaoTurma)
                {
                    // Alterar o campo quantidade de vagas ocupadas da divisão da turma para o valor "0"
                    var divisaoZerada = new DivisaoTurma()
                    {
                        Seq = seqDivisao,
                        QuantidadeVagasOcupadas = 0
                    };
                    DivisaoTurmaDomainService.UpdateFields<DivisaoTurma>(divisaoZerada, d => d.QuantidadeVagasOcupadas);

                    // Verificar se existem solicitações de matricula ativas*, que possuem a turma como item.
                    // Caso exista: Criar um novo registro no histórico de situação para o item, com a situação
                    // que foi parametrizada no processo etapa em questão, para ser a final, com classificação
                    // “Cancelada” e atribuir o motivo “Pela instituição”;

                    // Busca as solicitações que possuem a divisão
                    var specSol = new SolicitacaoMatriculaItemFilterSpecification()
                    {
                        SeqDivisaoTurma = seqDivisao
                    };
                    var solItens = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(specSol, x => new
                    {
                        SeqSolicitacaoMatriculaItem = x.Seq,
                        CategoriaSituacao = x.SolicitacaoMatricula.SituacaoAtual.CategoriaSituacao,
                        SeqProcessoEtapa = x.SolicitacaoMatricula.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa,
                    });

                    // Para cada item de solicitação "ativa", alterar a situação do item.
                    // Considerar solicitação "ativa" aquelas cuja categoria da situação é "Novo" ou "Em andamento"
                    // (está diferente na documentação - já combinado com Jéssica)
                    foreach (var item in solItens.Where(i => i.CategoriaSituacao == CategoriaSituacao.Novo || i.CategoriaSituacao == CategoriaSituacao.EmAndamento))
                    {
                        // Task 41256 - Atualizar o campo pertence ao plano de estudos para o valor Não.
                        var solicitacaoItemCancelada = new SolicitacaoMatriculaItem()
                        {
                            Seq = item.SeqSolicitacaoMatriculaItem,
                            PertencePlanoEstudo = false
                        };
                        SolicitacaoMatriculaItemDomainService.UpdateFields<SolicitacaoMatriculaItem>(solicitacaoItemCancelada, d => d.PertencePlanoEstudo);

                        // Busca a situação de item de matrícula referente a cancelada para o processo etapa da solicitação
                        var sitItemCancelada = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(item.SeqProcessoEtapa, null, true, ClassificacaoSituacaoFinal.Cancelado);

                        // Inclui uma nova situação para o item
                        var novaSituacaoItem = new SolicitacaoMatriculaItemHistoricoSituacao()
                        {
                            DataInclusao = DateTime.Now,
                            SeqSituacaoItemMatricula = sitItemCancelada.Seq,
                            MotivoSituacaoMatricula = MotivoSituacaoMatricula.PelaInstituicao,
                            SeqSolicitacaoMatriculaItem = item.SeqSolicitacaoMatriculaItem
                        };
                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(novaSituacaoItem);
                    }

                    // Busca os eventos de aula da divisão
                    var eventosAulaDivisao = this.EventoAulaDomainService.SearchBySpecification(new EventoAulaFilterSpecification() { SeqDivisaoTurma = seqDivisao }, x => x.Colaboradores).ToList();
                    var eventosAulaDivisaoDataMaiorIgual = eventosAulaDivisao.Where(a => a.Data.Date >= dataCancelamento.Date).ToList();

                    foreach (var eventoAula in eventosAulaDivisaoDataMaiorIgual)
                    {
                        eventoAula.CodigoLocalSEF = null;
                        eventoAula.Local = null;
                        eventoAula.DataCancelamento = dataCancelamento;

                        this.EventoAulaDomainService.SaveEntity(eventoAula);

                        foreach (var colaborador in eventoAula.Colaboradores)
                        {
                            this.EventoAulaColaboradorDomainService.DeleteEntity(colaborador);
                        }

                        if (!string.IsNullOrEmpty(eventoAula.SeqEventoAgd))
                        {
                            if (Int64.TryParse(eventoAula.SeqEventoAgd, out long seqEvento))
                            {
                                seqsEventosCancelar.Add(seqEvento);
                            }
                        }
                    }

                    //TASK - 60927
                    //1 - Os professores que forem associados ou excluidos dos eventos de aula deverão ser tambem associados ou excluido da divisao_turma_colaborador, seja ele substituto ou não, conforme abaixo:
                    //Na inclusão e alteração do professor nos eventos de aula verificar se o mesmo já existe na divisao_turma_colaborador considerando o ciclo letivo da turma.
                    //Se não existir inclui-lo e se já existir atualizar a sua carga horária.
                    if (eventosAulaDivisao.Count() > 0)
                    {
                        var listaEventoAulaVo = new List<EventoAulaVO>();

                        //criando lista apenas para poder passar na chamada do metodo de atualização, como nele só é usado o Seq da divisão de turma, foi o unico mapeado.
                        foreach (var item in eventosAulaDivisao)
                        {
                            var eventoAulaVo = new EventoAulaVO()
                            {
                                SeqDivisaoTurma = item.SeqDivisaoTurma
                            };

                            listaEventoAulaVo.Add(eventoAulaVo);
                        }

                        EventoAulaDomainService.AtualizarProfessoresDivisao(listaEventoAulaVo);
                    }

                    //Na exclusão do professor nos evento de aula atualizar a carga horária do professor na divisão_turma_colaborador. Se for verificado que esta carga horária esta zerada,
                    //excluir o professor da divisao_turma_colaborador.
                    EventoAulaDomainService.AtualizarRemocaoProfessoresDivisao(seqDivisao);
                }

                var turmaColaboradorSpec = new TurmaColaboradorFilterSpecification { SeqTurma = seqTurma };
                var turmasColaborador = TurmaColaboradorDomainService.SearchBySpecification(turmaColaboradorSpec).ToList();

                //Excluir os professores do cadastro de professores responsáveis(turma_colaborador)
                if (turmasColaborador != null && turmasColaborador.Any())
                    foreach (var turmaColaborador in turmasColaborador)
                        TurmaColaboradorDomainService.DeleteEntity(turmaColaborador);

                if (seqsEventosCancelar.Any())
                {
                    EventoService.CancelarEventos(seqsEventosCancelar, dataCancelamento);
                }

                // Verificar se existe algum plano de estudo atual que possui a turma em questão como item.
                // Caso exista, excluir essa turma do plano de estudos do aluno

                // Busca os planos que tem alguma divisão da turma
                var specPlano = new PlanoEstudoFilterSpecification()
                {
                    SeqsDivisaoTurma = seqsDivisaoTurma.ToArray(),
                    Atual = true
                };
                var seqsPlanosComTurma = PlanoEstudoDomainService.SearchProjectionBySpecification(specPlano, x => x.Seq);
                foreach (var seqPlano in seqsPlanosComTurma)
                {
                    PlanoEstudoDomainService.ExcluirTurmaDoPlano(seqPlano, seqsDivisaoTurma.ToList());
                }

                // Finaliza a transação
                transacao.Commit();
            }
        }

        /// <summary>
        /// Valida se a turma pode ser cancelada
        /// </summary>
        /// <param name="turma"></param>
        /// <returns>True ->Emitir mensagem e Cancelar; False -> Prosseguir sem cancelar</returns>
        public void ValidarCancelarTurma(TurmaVO turma)
        {
            // Se o valor da situação for alterado de "Ofertada" para "Cancelada":
            if (turma.Seq > 0)
            {
                // 1. Verificar se existe nota lançada no histórico escolar para a turma em questão.
                ValidarCancelarTurmaNotaLancada(turma.Seq);

                // 1.2. Caso não exista, verificar se existe aluno que possui apenas esta turma como item do plano de estudo atual.
                ValidarCancelarTurmaApenasEstaComoPlanoEstudoAtual(turma);
            }
        }

        private void ValidarCancelarTurmaApenasEstaComoPlanoEstudoAtual(TurmaVO turma)
        {
            var seqsDivisaoTurma = turma.DivisoesTurma.Select(d => d.Seq).ToList();

            // 1.2. Caso não exista, verificar se existe aluno que possui apenas esta turma como item do plano de estudo atual.
            var specPlano = new PlanoEstudoFilterSpecification()
            {
                SeqsDivisaoTurma = seqsDivisaoTurma.ToArray(),
                Atual = true
            };
            var planosEstudosAtuais = PlanoEstudoDomainService.SearchBySpecification(specPlano, IncludesPlanoEstudo.Itens).ToList();

            if (planosEstudosAtuais.SMCAny())
            {
                var planosEstudosItemCorrespondente = new List<PlanoEstudo>();

                // Verificar se o plano  atual existe apenas o item correspondente a turma
                // (ALTERAÇÃO - Diferente das divisões da turma que está sendo cancelada)
                // - (verificar se existe aluno que possui apenas esta turma como item do plano de estudo atual).
                foreach (var planoEstudo in planosEstudosAtuais)
                {
                    var qtdItensSemDivisaoDaTurma = planoEstudo.Itens.Where(x => !x.SeqDivisaoTurma.HasValue || !seqsDivisaoTurma.Contains(x.SeqDivisaoTurma.Value)).Count();
                    if (qtdItensSemDivisaoDaTurma == 0)
                    {
                        planosEstudosItemCorrespondente.Add(planoEstudo);
                    }
                }

                // Recupero os Alunos do plano, que possuem apenas o Item de plano da turma, para exibir na mensagem .
                if (planosEstudosItemCorrespondente.SMCAny())
                {
                    var spec = new PlanoEstudoFilterSpecification() { Seqs = planosEstudosItemCorrespondente.Select(x => x.Seq).ToArray(), Atual = true };

                    var alunos = PlanoEstudoDomainService.SearchProjectionBySpecification(spec, x => new AlunoVO()
                    {
                        Nome = x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.NumeroRegistroAcademico + " - " + x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.Nome
                    }).ToList();

                    /*"Não é possível prosseguir. Os seguintes alunos estão matriculados apenas nesta turma:
                    - <RA 1> + "-" + <Nome do aluno 1>
                    - <RA 2> + "-" + <Nome do aluno 2>*/

                    var nomes = string.Join("<br />", alunos.Select(a => a.Nome).ToArray());

                    throw new TurmaCanceladaPlanoEstudoAlunoException(nomes);
                }
            }
        }

        /// <summary>
        /// Verificar se existe nota lançada no histórico escolar para a turma em questão
        /// </summary>
        /// <param name="seqTurma"></param>
        private void ValidarCancelarTurmaNotaLancada(long seqTurma)
        {
            // 1. Verificar se existe nota lançada no histórico escolar para a turma em questão.
            var notaLancada = SearchProjectionByKey(new SMCSeqSpecification<Turma>(seqTurma), x => x.OrigemAvaliacao.HistoricosEscolares.Count > 0);
            if (notaLancada)
            {
                /*1.1. Caso exista, abortar a operação e exibir a seguinte mensagem:
                  "Não é possível prosseguir. Já existe nota lançada para alunos desta turma."*/
                throw new TurmaCanceladaNotaLancadaException();
            }
        }

        #endregion [ Cancelar turma ]

        #region [ Ofertar Turma ]

        /// <summary>
        /// Reofertar a turma
        /// </summary>
        /// <param name="seq"></param>
        public void OfertarTurma(long seq)
        {
            if (seq == 0) { throw new Exception("Turma inválida"); }

            TurmaHistoricoSituacaoDomainService.AlterarSituacaoTurma(seq, SituacaoTurma.Ofertada, string.Empty);

            var seqsEventosDescancelar = new List<long>();

            var specDivisao = new DivisaoTurmaFilterSpecification() { SeqTurma = seq };
            var seqsDivisaoTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisao, d => d.Seq);
            foreach (var seqDivisao in seqsDivisaoTurma)
            {
                // Busca os eventos de aula da divisão
                var eventosAulaDivisao = this.EventoAulaDomainService.SearchBySpecification(new EventoAulaFilterSpecification() { SeqDivisaoTurma = seqDivisao }).ToList();
                var eventosAulaCancelados = eventosAulaDivisao.Where(a => a.DataCancelamento.HasValue).ToList();

                foreach (var eventoAula in eventosAulaCancelados)
                {
                    eventoAula.DataCancelamento = null;

                    this.EventoAulaDomainService.SaveEntity(eventoAula);

                    if (!string.IsNullOrEmpty(eventoAula.SeqEventoAgd))
                    {
                        if (Int64.TryParse(eventoAula.SeqEventoAgd, out long seqEvento))
                        {
                            seqsEventosDescancelar.Add(seqEvento);
                        }
                    }
                }
            }

            if (seqsEventosDescancelar.Any())
            {
                EventoService.DescancelarEventos(seqsEventosDescancelar);
            }
        }

        #endregion [ Ofertar Turma ]

        /// <summary>
        /// Busca as turmas de acordo com a pessoa atuação processo de matrícula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <param name="seqProcesso">Sequencial processo</param>
        /// <param name="seqGrupoPrograma">Sequencial do grupo de programa para disciplina eletiva</param>
        /// <param name="tokenServico">Token do Serviço</param>
        /// <returns>Lista de turmas detalhadas</returns>
        public SMCPagerData<TurmaMatriculaListarVO> BuscarTurmasPessoaAtuacaoEntidade(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long? seqProcesso, long? seqGrupoPrograma, string tokenServico)
        {
            //Buscar o ciclo letivo de acordo com o processo de matrícula
            long? seqCicloLetivo = null;
            if (seqProcesso.HasValue && seqProcesso > 0)
                seqCicloLetivo = ProcessoDomainService.BuscarCicloLetivoProcesso(seqProcesso.Value);

            //Recupera a matriz curricular oferta e o tipo de atuação
            InstituicaoNivelTipoVinculoAlunoVO dadosVinculo = new InstituicaoNivelTipoVinculoAlunoVO();
            PessoaAtuacaoDadosOrigemVO dadosOrigem = new PessoaAtuacaoDadosOrigemVO();
            List<long> seqsTurmasPertence = new List<long>();

            if (seqPessoaAtuacao > 0)
            {
                dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);
                dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);
            }

            if (dadosOrigem.SeqMatrizCurricularOferta > 0)
                seqsTurmasPertence = BuscarSequenciaisTurmaMatrizOfertaLegenda(dadosOrigem.SeqMatrizCurricularOferta, seqSolicitacaoMatricula);

            List<long> seqsTurma = new List<long>();

            seqGrupoPrograma = seqGrupoPrograma ?? 0;
            if (dadosVinculo.ExigeOfertaMatrizCurricular.HasValue && dadosVinculo.ExigeOfertaMatrizCurricular.Value == false && seqGrupoPrograma == 0)
                //Verificar com a Jessíca para disciplinas isoladas só retornar as relativas a campanha do ingressante
                seqsTurma = BuscarSequenciaisTurmaMatrizOfertaIngressante(seqSolicitacaoMatricula);
            else
            {
                seqsTurma = BuscarSequenciaisTurmaGrupoProgramaSolicitacaoServico(seqGrupoPrograma.Value, seqSolicitacaoMatricula, dadosOrigem.SeqCursoOfertaLocalidadeTurno);

                // Task 34126
                if (tokenServico == TOKEN_SERVICO.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA)
                {
                    // Modificação na regra para considerar turmas exceto as que possuam pelo menos uma oferta de matriz da mesma entidade responsável pelo aluno-histórico.
                    var seqPessoaAtuacaoSolicitacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(seqSolicitacaoMatricula, x => x.SeqPessoaAtuacao);
                    var seqEntidadeResponsavelAlunoHistorico = (long?)AlunoDomainService.SearchProjectionByKey(seqPessoaAtuacaoSolicitacao, x => x.Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo);
                    if (seqEntidadeResponsavelAlunoHistorico.HasValue)
                    {
                        // Recupera as turmas que estão na oferta da matriz da entidade responsável do aluno histórico
                        var seqsTurmasEntidadeAluno = BuscarSequenciaisTurmaGrupoProgramaSolicitacaoServico(seqEntidadeResponsavelAlunoHistorico.Value, seqSolicitacaoMatricula, dadosOrigem.SeqCursoOfertaLocalidadeTurno);

                        // Remove as turmas que estão na oferta de matriz da entidade responspavel do aluno histórico
                        seqsTurma = seqsTurma.Where(s => !seqsTurmasEntidadeAluno.Contains(s)).ToList();
                    }
                }
            }
            if (seqsTurma.Count == 0)
                return new SMCPagerData<TurmaMatriculaListarVO>(new List<TurmaMatriculaListarVO>());

            var specTurma = new TurmaFilterSpecification()
            {
                SeqsTurma = seqsTurma,
                SeqCicloLetivoInicio = seqCicloLetivo,
                SituacaoTurmaAtual = SituacaoTurma.Ofertada
            };

            specTurma.MaxResults = int.MaxValue;
            specTurma.SetOrderBy(p => p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().Descricao);

            var dadosTurma = this.SearchProjectionBySpecification(specTurma,
                p => new
                {
                    Seq = p.Seq,
                    Codigo = p.Codigo,
                    Numero = p.Numero,

                    QuantidadeVagas = (short?)p.QuantidadeVagas,
                    DescricaoTipoTurma = p.TipoTurma.Descricao,
                    AssociacaoOfertaMatrizTipoTurma = p.TipoTurma.AssociacaoOfertaMatriz,
                    DescricaoCicloLetivoInicio = p.CicloLetivoInicio.Descricao,
                    DescricaoCicloLetivoFim = p.CicloLetivoFim.Descricao,
                    HistoricosSituacao = p.HistoricosSituacao,

                    SeqNivelEnsino = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.FirstOrDefault(w => w.Responsavel).SeqNivelEnsino,
                    SeqNivelEnsinoPrincipal = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.FirstOrDefault(w => w.Responsavel).SeqNivelEnsino,
                    SeqTipoComponenteCurricular = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                    SeqTipoComponenteCurricularPrincipal = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                    DescricaoConfiguracaoComponente = p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).ConfiguracaoComponente.Descricao,
                    DescricaoConfiguracaoComponentePrincipal = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.Descricao,
                    DescricaoConfiguracaoComponenteTurma = p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                    ComponenteCurricular = p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).RestricoesTurmaMatriz.FirstOrDefault().ComponenteCurricularAssunto, // Nem toda REstricaoTurma possui ComponenteCurricularAssunto                    
                    //Linha mostra as restrições matriz, e em alguns casos o seq COmponenteCurricularAssunto é null
                    //RestricoesMatriz = p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).RestricoesTurmaMatriz, // Nem toda REstricaoTurma possui ComponenteCurricularAssunto                    
                    DescricaoConfiguracaoComponenteTurmaPrincipal = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).Descricao,
                    SeqConfiguracaoComponente = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).SeqConfiguracaoComponente,
                    SeqConfiguracaoComponentePrincipal = (long?)p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).SeqConfiguracaoComponente,
                    SituacaoTurmaAtual = (SituacaoTurma?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma,
                    Habilitar = false,
                    SeqIngressante = (long?)seqPessoaAtuacao,
                    DivisoesTurma = p.DivisoesTurma.Select(s => new DivisaoTurmaVO()
                    {
                        Seq = s.Seq,
                        SeqDivisaoComponente = s.SeqDivisaoComponente,
                        NumeroDivisaoComponente = s.DivisaoComponente.Numero,
                        SeqLocalidade = (long?)s.SeqLocalidade,
                        DescricaoLocalidade = s.Localidade.Nome,
                        SeqOrigemAvaliacao = s.SeqOrigemAvaliacao,
                        SeqTurma = s.SeqTurma,
                        NumeroGrupo = s.NumeroGrupo,
                        QuantidadeVagas = s.QuantidadeVagas,
                        QuantidadeVagasOcupadas = (short?)s.QuantidadeVagasOcupadas,
                        QuantidadeVagasReservadas = (short?)s.QuantidadeVagasReservadas,
                        InformacoesAdicionais = s.InformacoesAdicionais,
                        OrigemAvaliacao = new OrigemAvaliacaoVO()
                        {
                            Seq = s.OrigemAvaliacao.Seq,
                            SeqCriterioAprovacao = (long?)s.OrigemAvaliacao.SeqCriterioAprovacao,
                            QuantidadeGrupos = (short?)s.OrigemAvaliacao.QuantidadeGrupos,
                            QuantidadeProfessores = (short?)s.OrigemAvaliacao.QuantidadeProfessores,
                            ApurarFrequencia = (bool?)s.OrigemAvaliacao.ApurarFrequencia,
                            NotaMaxima = (short?)s.OrigemAvaliacao.NotaMaxima,
                            SeqEscalaApuracao = (long?)s.OrigemAvaliacao.SeqEscalaApuracao,
                        }
                    }).ToList(),
                    TurmaDivisoes = p.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).ConfiguracaoComponente.DivisoesComponente.Select(s => new TurmaDivisoesVO()
                    {
                        SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                        SeqDivisaoComponente = s.Seq,
                        TipoDivisaoDescricao = s.TipoDivisaoComponente.Descricao,
                        GerarOrientacao = s.TipoDivisaoComponente.GeraOrientacao,
                        SeqTipoOrientacao = s.TipoDivisaoComponente.SeqTipoOrientacao,
                        Numero = s.Numero,
                        CargaHoraria = s.CargaHoraria,
                        DescricaoComponenteCurricularOrganizacao = s.ComponenteCurricularOrganizacao.Descricao,
                        PermitirGrupo = s.PermiteGrupo
                    }).Where(w => p.DivisoesTurma.Select(d => d.SeqDivisaoComponente).Contains(w.SeqDivisaoComponente)).ToList(),
                    TurmaDivisoesPrincipal = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.DivisoesComponente.Select(s => new TurmaDivisoesVO()
                    {
                        SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                        SeqDivisaoComponente = s.Seq,
                        TipoDivisaoDescricao = s.TipoDivisaoComponente.Descricao,
                        GerarOrientacao = s.TipoDivisaoComponente.GeraOrientacao,
                        SeqTipoOrientacao = s.TipoDivisaoComponente.SeqTipoOrientacao,
                        Numero = s.Numero,
                        CargaHoraria = s.CargaHoraria,
                        DescricaoComponenteCurricularOrganizacao = s.ComponenteCurricularOrganizacao.Descricao,
                        PermitirGrupo = s.PermiteGrupo
                    }).Where(w => p.DivisoesTurma.Select(d => d.SeqDivisaoComponente).Contains(w.SeqDivisaoComponente)).ToList(),
                    SeqsConfiguracoesComponentes = p.ConfiguracoesComponente.Select(s => s.SeqConfiguracaoComponente).ToList()
                }).ToList();

            List<TurmaMatriculaListarVO> registros = new List<TurmaMatriculaListarVO>();
            dadosTurma.ForEach(r =>
            {
                var item = SMCMapperHelper.Create<TurmaMatriculaListarVO>(r);
                item.SeqNivelEnsino = r.SeqNivelEnsino ?? r.SeqNivelEnsinoPrincipal ?? 0;
                item.SeqTipoComponenteCurricular = r.SeqTipoComponenteCurricular ?? r.SeqTipoComponenteCurricularPrincipal ?? 0;
                item.DescricaoConfiguracaoComponente = r.DescricaoConfiguracaoComponente ?? r.DescricaoConfiguracaoComponentePrincipal;
                item.DescricaoConfiguracaoComponenteTurma = r.DescricaoConfiguracaoComponenteTurma ?? r.DescricaoConfiguracaoComponenteTurmaPrincipal;
                item.SeqConfiguracaoComponentePrincipal = r.SeqConfiguracaoComponente ?? r.SeqConfiguracaoComponentePrincipal ?? 0;

                if ((item.TurmaDivisoes == null || !item.TurmaDivisoes.Any()) && r.TurmaDivisoesPrincipal != null)
                    item.TurmaDivisoes = r.TurmaDivisoesPrincipal;

                //Exibir campo apenas quando o tipo de pessoa-atuação for aluno
                //ou for ingressante e o o processo seletivo não está configurado para fazer reserva de vaga.
                var exibirVagas = false;
                if (seqPessoaAtuacao == 0 || dadosOrigem.TipoAtuacao == TipoAtuacao.Aluno)
                    exibirVagas = true;
                else
                {
                    var ingressanteProcessoSeletivo = IngressanteDomainService.BuscarIngressanteProcessoSeletivo(seqPessoaAtuacao);
                    exibirVagas = !ingressanteProcessoSeletivo.ProcessoSeletivo.ReservaVaga;
                }

                ////TODO Task_24734 - Código comentado para o momento e assim que ocorrer acerto de dados será utlizado novamente
                ////Verificar tipos de divisões com a definição de exigir orientação
                //item.ObrigatorioOrientador = false;
                //if (item.TurmaDivisoes.Any(a => a.GerarOrientacao))
                //    item.ObrigatorioOrientador = OrientacaoPessoaAtuacaoDomainService.ValidarOrientacoesPessoaAtuacao(seqPessoaAtuacao, dadosVinculo.Seq);

                //Verificar se a configuração de componente da turma pertence a matriz curricular oferta do ingressante
                if (dadosOrigem.SeqMatrizCurricularOferta > 0)
                {
                    item.Pertence = seqsTurmasPertence.Contains(item.Seq) ? TurmaOfertaMatricula.ComponentePertence : TurmaOfertaMatricula.ComponenteNaoPertence;

                    if (dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante)
                    {
                        var validacaoPre = RequisitoDomainService.ValidarPreRequisitos(seqPessoaAtuacao, item.TurmaDivisoes.Select(s => s.SeqDivisaoComponente), null, null, null);

                        item.PreRequisito = !validacaoPre.Valido;
                    }
                }
                else
                    item.Pertence = TurmaOfertaMatricula.ComponenteNaoPertence;

                var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(item.SeqNivelEnsino, item.SeqTipoComponenteCurricular);

                item.TurmaMatriculaDivisoes = new List<TurmaMatriculaListarDetailVO>();

                //Recuperando detalhes das divisoes para listagem da turma
                foreach (var divisao in item.TurmaDivisoes)
                {
                    var matriculaDivisao = new TurmaMatriculaListarDetailVO();

                    matriculaDivisao.SeqPessoaAtuacao = seqPessoaAtuacao;

                    if (tiposComponenteNivel != null)
                        divisao.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;

                    matriculaDivisao.DivisaoTurmaDescricao = divisao.DescricaoFormatada;
                    matriculaDivisao.PermitirGrupo = divisao.PermitirGrupo;
                    matriculaDivisao.SeqConfiguracaoComponente = divisao.SeqConfiguracaoComponente;
                    matriculaDivisao.SeqDivisaoComponente = divisao.SeqDivisaoComponente;
                    matriculaDivisao.DivisoesTurmas = new List<SMCDatasourceItem>();

                    var divisoesSelect = item.DivisoesTurma.Where(w => w.NumeroDivisaoComponente == divisao.Numero)
                                                            .Select(s => new SMCDatasourceItem()
                                                            {
                                                                Seq = s.Seq,
                                                                Descricao = exibirVagas ?
                                                                $"{item.CodigoFormatado}.{s.NumeroDivisaoComponente}.{s.NumeroGrupo.ToString().PadLeft(3, '0')} - {s.QuantidadeVagasDisponiveis} vagas disponíveis" :
                                                                $"{item.CodigoFormatado}.{s.NumeroDivisaoComponente}.{s.NumeroGrupo.ToString().PadLeft(3, '0')}"
                                                            });

                    matriculaDivisao.DivisoesTurmas.AddRange(divisoesSelect);

                    if (matriculaDivisao.DivisoesTurmas.Count == 1)
                        matriculaDivisao.SeqDivisaoTurma = matriculaDivisao.DivisoesTurmas[0].Seq;

                    //Verifica se essa turma já existe na tabela de solicitação item
                    if (matriculaDivisao.SeqConfiguracaoComponente != null && (matriculaDivisao.SeqDivisaoTurma != null || matriculaDivisao.DivisoesTurmas.Count > 0))
                    {
                        var filtroItem = new SolicitacaoMatriculaItemFiltroVO
                        {
                            SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                            SeqConfiguracaoComponente = matriculaDivisao.SeqConfiguracaoComponente
                        };
                        if (matriculaDivisao.SeqDivisaoTurma.HasValue)
                        {
                            filtroItem.SeqDivisaoTurma = matriculaDivisao.SeqDivisaoTurma;
                        }
                        else
                        {
                            filtroItem.SeqsDivisoesTurma = matriculaDivisao.DivisoesTurmas.Select(s => s.Seq).ToList();
                            filtroItem.ClassificacaoSituacoesFinaisDiferentes = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.Cancelado | ClassificacaoSituacaoFinal.FinalizadoSemSucesso };
                        }

                        matriculaDivisao.Seq = SolicitacaoMatriculaItemDomainService.BuscarSequencialSolicitacaoMatriculaItem(filtroItem);
                    }

                    item.TurmaMatriculaDivisoes.Add(matriculaDivisao);
                    item.TurmaMatriculaDivisoes = item.TurmaMatriculaDivisoes.OrderBy(o => o.DivisaoTurmaDescricao).ToList();
                }

                registros.Add(item);
            });

            return new SMCPagerData<TurmaMatriculaListarVO>(registros);
        }

        /// <summary>
        /// Busca os sequencias de turmas com as configurações de co requisito de uma pessoa atuação
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <returns>Lista com os sequenciais das turmas e sequenciais das configurações</returns>
        public List<TurmaMatriculaListarVO> BuscarConfiguracaoTurmasCoRequisitoPessoaAtuacao(long seqSolicitacaoMatricula, long seqPessoaAtuacao)
        {
            List<long> seqsTurma = new List<long>();

            //0 - para utilizar o grupo de programa da solicitação de serviço
            seqsTurma = BuscarSequenciaisTurmaGrupoProgramaSolicitacaoServico(0, seqSolicitacaoMatricula, 0);

            var specTurma = new TurmaFilterSpecification()
            {
                SeqsTurma = seqsTurma,
                SituacaoTurmaAtual = SituacaoTurma.Ofertada
            };

            specTurma.MaxResults = int.MaxValue;
            var registros = this.SearchProjectionBySpecification(specTurma,
                                                                 p => new TurmaMatriculaListarVO()
                                                                 {
                                                                     Seq = p.Seq,
                                                                     SeqsConfiguracoesComponentes = p.ConfiguracoesComponente.Select(s => s.SeqConfiguracaoComponente).ToList()
                                                                 }).ToList();
            return registros;
        }


        /// <summary>
        /// Buscar os sequenciais das turmas de acordo o grupo de programa e a solicitação de serviço
        /// </summary>
        /// <param name="seqGrupoPrograma">Sequencial do grupo de programa</param>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqCursoOfertaLocalidadeTurno">Sequencial do curso oferta localidade turno</param>
        /// <returns>Lista de sequenciais das turmas para tela de seleção de turma</returns>
        private List<long> BuscarSequenciaisTurmaGrupoProgramaSolicitacaoServico(long seqGrupoPrograma, long seqSolicitacaoServico, long seqCursoOfertaLocalidadeTurno)
        {
            return RawQuery<long>(string.Format(_buscarSequenciaisTurmaGrupoProgramaSolicitacaoServico, seqGrupoPrograma, seqSolicitacaoServico, seqCursoOfertaLocalidadeTurno));
        }

        /// <summary>
        /// Buscar os sequenciais das turmas que mostra a legenda de acordo com a matriz curricular oferta
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Lista de sequenciais das turmas para tela de seleção de turma que vai mostrar a legenda</returns>
        public List<long> BuscarSequenciaisTurmaMatrizOfertaLegenda(long seqMatrizCurricularOferta, long seqSolicitacaoServico)
        {
            return RawQuery<long>(string.Format(_buscarSequenciaisTurmaMatrizOfertaLegenda, seqMatrizCurricularOferta, seqSolicitacaoServico));
        }

        /// <summary>
        /// Buscar os sequenciais das turmas de acordo com a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Lista de sequenciais das turmas para tela de seleção de turma</returns>
        private List<long> BuscarSequenciaisTurmaMatrizOfertaIngressante(long seqSolicitacaoMatricula)
        {
            return RawQuery<long>(string.Format(_buscarSequenciaisTurmaMatrizOfertaIngressante, seqSolicitacaoMatricula));
        }

        /// <summary>
        /// Buscar os sequenciais das turmas de acordo com o programa selecionado na disciplina eletiva
        /// </summary>
        /// <param name="seqPrograma">Sequencial do programa</param>
        /// <returns>Lista de sequenciais das turmas para tela de seleção de turma</returns>
        private List<long> BuscarSequenciaisTurmaPorPrograma(long seqPrograma)
        {
            return RawQuery<long>(string.Format(_buscarSequenciaisTurmaPorPrograma, seqPrograma));
        }

        /// <summary>
        /// Busca as turmas de acordo com o professor responsável ou professor da divisão
        /// </summary>
        /// <param name="seqProfessor">Sequencial do professor</param>
        /// <returns>Lista de turmas detalhadas</returns>
        public List<TurmaListarGrupoCursoVO> BuscarTurmasProfessor(long seqProfessor, bool? turmaSituacaoCancelada = null)
        {
            var spec = new TurmaFilterSpecification() { SeqColaborador = seqProfessor, TurmaSituacaoNaoCancelada = turmaSituacaoCancelada };
            spec.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(spec,
                p => new TurmaListarVO()
                {
                    Seq = p.Seq,
                    Codigo = p.Codigo,
                    Numero = p.Numero,
                    SeqCicloLetivoInicio = p.SeqCicloLetivoInicio,
                    SeqNivelEnsino = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino,
                    SeqLocalidade = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade,
                    SeqTurno = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.SeqTurno,
                    SeqCurso = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.SeqCurso,
                    SeqTipoComponenteCurricular = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                    QuantidadeVagas = p.QuantidadeVagas,
                    DescricaoTipoTurma = p.TipoTurma.Descricao,
                    TurmaCompartilhada = p.TipoTurma.AssociacaoOfertaMatriz == AssociacaoOfertaMatriz.ExigeMaisDeUma,
                    DescricaoCicloLetivoInicio = p.CicloLetivoInicio.Descricao,
                    DescricaoCicloLetivoFim = p.CicloLetivoFim.Descricao,
                    DescricaoConfiguracaoComponente = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).Descricao,
                    DescricaoLocalidade = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                    DescricaoTurno = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao,
                    SituacaoTurmaAtual = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma : SituacaoTurma.Nenhum,
                    SituacaoJustificativa = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().Justificativa : "",
                    SeqOrigemAvaliacao = p.SeqOrigemAvaliacao,
                    TipoEscalaApuracao = p.OrigemAvaliacao.EscalaApuracao.TipoEscalaApuracao,
                    DiarioFechado = p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
                    ResponsavelTurma = p.Colaboradores.Any(a => a.SeqColaborador == seqProfessor),//Verificar se o professor em questão e responsável pela turma
                    PermiteAvaliacaoParcial = p.OrigemAvaliacao.PermiteAvaliacaoParcial,
                    SeqAgendaTurma = p.SeqAgendaTurma,
                    DivisoesTurma = p.DivisoesTurma.Where(d => d.Colaboradores.Any(c => c.SeqColaborador == seqProfessor) || p.Colaboradores.Any(c => c.SeqColaborador == seqProfessor)).Select(s => new DivisaoTurmaVO()
                    {
                        Seq = s.Seq,
                        SeqTurma = s.SeqTurma,
                        SeqDivisaoComponente = s.SeqDivisaoComponente,
                        NumeroDivisaoComponente = s.DivisaoComponente.Numero,
                        NumeroGrupo = s.NumeroGrupo,
                        SeqOrigemAvaliacao = s.SeqOrigemAvaliacao,
                        TipoEscalaApuracao = s.OrigemAvaliacao.EscalaApuracao.TipoEscalaApuracao,
                        DiarioFechado = s.Turma.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
                        PermiteAvaliacaoParcial = s.OrigemAvaliacao.PermiteAvaliacaoParcial,
                        TipoDivisaoDescricao = s.DivisaoComponente.TipoDivisaoComponente.Descricao,
                        GerarOrientacao = s.DivisaoComponente.TipoDivisaoComponente.GeraOrientacao,
                        CargaHoraria = s.DivisaoComponente.CargaHoraria,
                        DescricaoComponenteCurricularOrganizacao = s.DivisaoComponente.ComponenteCurricularOrganizacao.Descricao,
                        DivisaoTurmaPossuiConfiguracaoesGrade = s.HistoricosConfiguracaoGrade.Any(),
                        OrigemAvaliacao = new OrigemAvaliacaoVO()
                        {
                            Seq = s.OrigemAvaliacao.Seq,
                            SeqCriterioAprovacao = s.OrigemAvaliacao.SeqCriterioAprovacao,
                            ApurarFrequencia = s.OrigemAvaliacao.ApurarFrequencia
                        }
                    }).ToList(),
                    TurmaDivisoes = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault()
                    .ConfiguracaoComponente.DivisoesComponente.Select(s => new TurmaDivisoesVO()
                    {
                        SeqDivisaoComponente = s.Seq,
                        SeqTurma = p.Seq,
                        TipoDivisaoDescricao = s.TipoDivisaoComponente.Descricao,
                        GerarOrientacao = s.TipoDivisaoComponente.GeraOrientacao,
                        Numero = s.Numero,
                        CargaHoraria = s.CargaHoraria,
                        DescricaoComponenteCurricularOrganizacao = s.ComponenteCurricularOrganizacao.Descricao
                    }).ToList()
                }).ToList();

            foreach (var item in registros)
            {
                var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(item.SeqNivelEnsino, item.SeqTipoComponenteCurricular);

                foreach (var divisao in item.DivisoesTurma)
                {
                    if (tiposComponenteNivel != null)
                        divisao.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;

                    divisao.TurmaCodigoFormatado = item.CodigoFormatado;

                    /*Valida se é permitido lançar frequencia para a divisão de turma
                    Exibir somente se:
                    1) a divisão de turma tem apuração por frequência = sim
                    2) a turma possui agenda configurada E
                    3) a divisão de turma tem configuração de grade cadastrada E
                    Se o diário estiver fechado - desabilitar o menu com a observação "Diário fechado" no "izinho"*/
                    divisao.PermiteLancamentoFrequencia = true;
                    if (!item.SeqAgendaTurma.HasValue ||
                        !divisao.DivisaoTurmaPossuiConfiguracaoesGrade ||
                        !divisao.OrigemAvaliacao.ApurarFrequencia.GetValueOrDefault())
                    {
                        divisao.PermiteLancamentoFrequencia = false;
                    }
                }

                //Recuperando detalhes das divisoes para listagem da turma
                foreach (var divisao in item.TurmaDivisoes)
                {
                    if (tiposComponenteNivel != null)
                        divisao.FormatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;

                    var divisaoItemDetalhe = item.DivisoesTurma.Where(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente).FirstOrDefault();
                    if (divisaoItemDetalhe != null)
                    {
                        divisao.Seq = divisaoItemDetalhe.Seq;
                        divisao.TurmaCodigoFormatado = item.CodigoFormatado;
                        divisao.NumeroDivisaoComponente = divisaoItemDetalhe.NumeroDivisaoComponente;
                        divisao.NumeroGrupo = divisaoItemDetalhe.NumeroGrupo;
                    }

                    divisao.SeqTurma = item.Seq;
                    divisao.DivisaoComponenteDescricao = divisao.DescricaoFormatada;
                }
            }

            List<TurmaListarGrupoCursoVO> retorno = new List<TurmaListarGrupoCursoVO>();
            registros.GroupBy(g => g.DescricaoCursoLocalidadeTurno).SMCForEach(f =>
            {
                retorno.Add(new TurmaListarGrupoCursoVO() { DescricaoCursoLocalidadeTurno = f.First().DescricaoCursoLocalidadeTurno, Turmas = f.ToList() });
            });

            return retorno;
        }

        /// <summary>
        /// Busca as turmas de acordo com o professor responsável ou professor da divisão (CACHE)
        /// </summary>
        /// <param name="seqProfessor">Sequencial do professor</param>
        /// <returns>Lista de turmas detalhadas</returns>
        public List<TurmaListarGrupoCursoVO> BuscarTurmasProfessorCache(long seqProfessor)
        {
            var keyCacheTemplate = "__SGA_TURMAS_PROFESSOR_" + seqProfessor;

            var turmaProfessor = SMCCacheManager.Get(keyCacheTemplate) as List<TurmaListarGrupoCursoVO>;
            if (turmaProfessor == null)
            {
                turmaProfessor = BuscarTurmasProfessor(seqProfessor);
                SMCCacheManager.Add(keyCacheTemplate, turmaProfessor, new TimeSpan(0, 10, 0));
            }

            return turmaProfessor;
        }

        /// <summary>
        /// Busca as turmas de acordo com o professor responsável ou professor da divisão para preencher as seleções de filtro
        /// </summary>
        /// <param name="seqProfessor">Sequencial do professor</param>
        /// <returns>Lista de turmas com datasources de filtro</returns>
        public List<TurmaListarVO> BuscarTurmasProfessorFiltros(long seqProfessor)
        {
            var spec = new TurmaFilterSpecification() { SeqColaborador = seqProfessor, TurmaSituacaoNaoCancelada = true };
            spec.MaxResults = int.MaxValue;

            var registros = this.SearchProjectionBySpecification(spec,
                                p => new TurmaListarVO()
                                {
                                    SeqCicloLetivoInicio = p.SeqCicloLetivoInicio,
                                    DescricaoCicloLetivoInicio = p.CicloLetivoInicio.Descricao,
                                    AnoNumeroCicloLetivo = p.CicloLetivoInicio.AnoNumeroCicloLetivo,
                                    SeqNivelEnsino = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino,
                                    DescricaoNivelEnsino = p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().NivelEnsino.Descricao,
                                    SeqLocalidade = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade,
                                    DescricaoLocalidade = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                    SeqTurno = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.SeqTurno,
                                    DescricaoTurno = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                    SeqCurso = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.SeqCurso,
                                    DescricaoCurso = p.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).RestricoesTurmaMatriz.FirstOrDefault(m => m.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.Curso.Nome,
                                }).ToList();

            return registros;
        }

        /// <summary>
        /// Busca dados de cabeçalho para relatorio de diario de turma.
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns></returns>
        public List<DiarioTurmaCabecalhoVO> BuscarDiarioTurmaCabecalho(long seqTurma)
        {
            return RawQuery<DiarioTurmaCabecalhoVO>(string.Format(_buscarDiarioTurmaCabecalho, seqTurma));
        }

        /// <summary>
        /// Busca dados de professores para relatório de diario de turma
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns></returns>
        public List<DiarioTurmaProfessorVO> BuscarDiarioTurmaProfessor(long seqTurma)
        {
            return RawQuery<DiarioTurmaProfessorVO>(string.Format(_buscarDiarioTurmaProfessor, seqTurma));
        }

        /// <summary>
        /// Busca dados de aluno para relatório de diario de turma
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns></returns>
        public List<DiarioTurmaAlunoVO> BuscarDiarioTurmaAluno(long seqTurma, long? seqDivisaoTurma, long? seqAluno, List<long> seqsOrientadores)
        {
            string query = string.Format(_buscarDiarioTurmaAluno, seqTurma, seqDivisaoTurma.HasValue ? seqDivisaoTurma.Value.ToString() : "NULL", seqAluno.HasValue ? seqAluno.Value.ToString() : "NULL");
            var dados = RawQuery<DiarioTurmaAlunoVO>(query);
            var professoresTurma = BuscarDiarioTurmaProfessor(seqTurma);
            var turmaOrientacao = dados.FirstOrDefault()?.TurmaOrientacao;

            // Percorre e verifica se a turma é de orientação
            if (turmaOrientacao.HasValue && turmaOrientacao.Value && seqsOrientadores != null && seqsOrientadores.Any())
            {
                var specItem = new PlanoEstudoItemFilterSpecification()
                {
                    SeqTurma = seqTurma,
                    PlanoEstudoAtual = true
                };
                var planosOrientados = PlanoEstudoItemDomainService.SearchProjectionBySpecification(specItem, p => new
                {
                    p.SeqPlanoEstudo,
                    SeqsColaboradoes = p.Orientacao.OrientacoesColaborador.Select(s => s.SeqColaborador).ToList()
                }).ToList();
                dados = dados.Where(d =>
                    // Caso seja um aluno com orientador
                    planosOrientados.FirstOrDefault(f => f.SeqPlanoEstudo == d.SeqPlanoEstudo)?
                        // Filtra apenas pelos orientadores selecionados
                        .SeqsColaboradoes.Any(a => seqsOrientadores.Contains(a)) ??
                    // Caso contrário já ignora esse aluno
                    false).ToList();
            }

            // Valida os alunos dispensados ou aprovados no componente da turma
            var dadosComponenteCurricular = this.SearchProjectionByKey(seqTurma, p =>
            new
            {
                p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.SeqComponenteCurricular,
                p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault().SeqComponenteCurricularAssunto
            });

            var specHistoricos = new HistoricoEscolarFilterSpecification()
            {
                SeqComponenteCurricular = dadosComponenteCurricular.SeqComponenteCurricular,
                SeqComponenteCurricularAssunto = dadosComponenteCurricular.SeqComponenteCurricularAssunto,
                SeqsAlunoHistorico = dados.Select(s => s.SeqAlunoHistorico).ToList(),
                SeqsExcluidos = dados.Where(w => w.SeqHistoricoEscolar.HasValue).Select(s => s.SeqHistoricoEscolar.Value).ToArray(),
                SituacoesHistoricoEscolar = new[] { SituacaoHistoricoEscolar.Aprovado, SituacaoHistoricoEscolar.Dispensado }
            };
            var situacaoAlunos = HistoricoEscolarDomainService.SearchProjectionBySpecification(specHistoricos, p => new
            {
                p.AlunoHistorico.SeqAluno,
                p.SituacaoHistoricoEscolar
            }).ToList();
            foreach (var aluno in dados)
            {
                var situacaoAluno = situacaoAlunos.FirstOrDefault(f => aluno.SeqPessoaAtuacao == f.SeqAluno);
                if (situacaoAluno != null)
                {
                    switch (situacaoAluno.SituacaoHistoricoEscolar)
                    {
                        case SituacaoHistoricoEscolar.Aprovado:
                            aluno.AlunoAprovado = true;
                            break;

                        case SituacaoHistoricoEscolar.Dispensado:
                            aluno.AlunoDispensado = true;
                            break;
                    }
                }
            }

            return dados?.OrderBy(a => a.NomeAluno).ToList();
        }

        /// <summary>
        /// Faz a validação para saber se pode ou não fechar um diário de turma
        /// </summary>
        public void VerificaFecharDiarioAlunoSemNota(long seqTurma, long seqColaborador, bool lancamentoNotaParcial = false)
        {
            //Lista de orientadores caso exista
            List<long> seqOrientadores = null;

            // Se for a secretária, não valida professor orientador
            if (SMCContext.ApplicationId != SIGLA_APLICACAO.SGA_ADMINISTRATIVO)
            {
                // Verifica se o professor é orientador da turma. Se for somente da divisão, não permite fechar o diário. (Bug 30424)
                var professores = BuscarDiarioTurmaProfessor(seqTurma);
                if (!professores.Any(p => p.SeqColaborador == seqColaborador && p.ProfessorTurma))
                {
                    throw new UnauthorizedAccessException();
                }
                else
                {
                    //Caso seja o orientador da turma busca todos os orientadores da turma
                    if (seqColaborador != 0)
                    {
                        seqOrientadores = new List<long>();
                        foreach (var professor in professores)
                        {
                            seqOrientadores.Add(professor.SeqColaborador);
                        }
                    }
                }
            }

            Turma t = SearchByKey(new SMCSeqSpecification<Turma>(seqTurma), a => a.ConfiguracoesComponente);
            ConfiguracaoComponente cc = ConfiguracaoComponenteDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoComponente>(t.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().SeqConfiguracaoComponente), a => a.ComponenteCurricular.NiveisEnsino);

            // Recupera os alunos
            var dados = HistoricoEscolarDomainService.LancarNotasFrequenciasFinais(seqTurma, seqOrientadores, lancamentoNotaParcial);

            //RN_APR_036 - Consistência nota e frequencia no histórico escolar
            long? seqAluno = null;
            if (dados.Lancamentos?.Count() == 1)
                seqAluno = dados.Lancamentos.FirstOrDefault().SeqPessoaAtuacao;

            ApuracaoAvaliacaoDomainService.ValidarTurmaPermiteApuracaoFrequencia(dados.SeqTurma, seqAluno);

            /*
            RN_APR_020 - Consistência fechamento de Diário de turma
            Verificar o parâmetro 'Permite fechar diário com alunos sem nota' na associação da principal configuração de componente da turma:
            se valor = 'Sim', Verificar se para cada aluno matriculado à turma existe uma situação final ou uma marcação "sem nota"
            se valor = 'Não', Verificar se para cada aluno matriculado à turma existe uma situação final

            Caso exista pendência de lançamento, exibir mensagem de erro conforme a rega:
            se 'Permite fechar diário com alunos sem nota' = 'Sim' e critério exigir frequência exibir:
            "Fechamento de diário da turma não permitido. É necessário que todos os alunos tenham frequência  e nota/conceito ou opção'Sem nota' lançados".

            se 'Permite fechar diário com alunos sem nota' = 'Sim' e critério não exigir frequência exibir:
            "Fechamento de diário da turma não permitido. É necessário que todos os alunos tenham nota/conceito ou opção'Sem nota' lançados".

            se 'Permite fechar diário com alunos sem nota' = 'Não'e critério exigir frequência exibir:
            "Fechamento de diário da turma não permitido. É necessário que todos os alunos tenham frequência  e nota/conceito lançados".

            se 'Permite fechar diário com alunos sem nota' = 'Não'e critério não exigir frequência exibir:
            "Fechamento de diário da turma não permitido. É necessário que todos os alunos tenham nota/conceito lançados".

            Se não houver pendência, gravar o histórico de fechamento de diário da turma.*/

            IEnumerable<LancamentoHistoricoEscolarDetalhesVO> alunosFaltantes = null;

            if (dados.Lancamentos != null)
            {
                if (cc.PermiteAlunoSemNota)
                    alunosFaltantes = dados.Lancamentos.Where(l => string.IsNullOrEmpty(l.DescricaoSituacaoHistoricoEscolar) && !l.SemNota && !l.AlunoFormado);
                else
                    alunosFaltantes = dados.Lancamentos.Where(l => string.IsNullOrEmpty(l.DescricaoSituacaoHistoricoEscolar) && !l.AlunoFormado);
            }

            if (alunosFaltantes != null && alunosFaltantes.Any())
            {
                if (cc.PermiteAlunoSemNota)
                {
                    if (alunosFaltantes.FirstOrDefault().IndicadorApuracaoFrequencia)
                        throw new SMCApplicationException("Fechamento de diário da turma não permitido. É necessário que todos os alunos tenham frequência e nota/conceito ou opção 'Sem nota' lançados.");
                    else
                        throw new SMCApplicationException("Fechamento de diário da turma não permitido. É necessário que todos os alunos tenham nota/conceito ou opção 'Sem nota' lançados.");
                }
                else
                {
                    if (alunosFaltantes.FirstOrDefault().IndicadorApuracaoFrequencia)
                        throw new SMCApplicationException("Fechamento de diário da turma não permitido. É necessário que todos os alunos tenham frequência e nota/conceito lançados.");
                    else
                        throw new SMCApplicationException("Fechamento de diário da turma não permitido. É necessário que todos os alunos tenham nota/conceito lançados.");
                }
            }

            /*
            2) Verificar se existe pelo menos uma divisão de turma que o ind_materia_lecionada_obrigatoria = SIM
            se sim verificar se existe texto(desconsiderar espaço em branco antes e depois) de matéria lecionada
            na origem de avaliação da turma.*/
            var materiaLecionadaObrigatoria = DivisaoTurmaDomainService.MateriaLecionadaObrigatoria(seqTurma);
            if (materiaLecionadaObrigatoria)
            {
                var materiasLecionadasPorDivisao = DivisaoTurmaDomainService.SearchProjectionBySpecification(new DivisaoTurmaFilterSpecification { SeqTurma = seqTurma },
                                                                                                   a => a.OrigemAvaliacao.MateriaLecionada).ToList();

                foreach (var materia in materiasLecionadasPorDivisao)
                {
                    if (string.IsNullOrEmpty(materia))
                        throw new SMCApplicationException("Operação não permitida, matéria lecionada não cadastrada.");
                }
            }
        }

        /// <summary>
        /// Fecha o diáro de uma turma.
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma.</param>
        /// <param name="seqColaborador">Sequencial do professor</param>
        /// <param name="lancamentoNotaParcial">Informa se foi chamado da tela de lançamento parcial</param>
        public void FecharDiarioTurma(long seqTurma, long seqColaborador, bool lancamentoNotaParcial = false)
        {
            // Faz as validações para saber se pode ou não fechar um diário de turma
            VerificaFecharDiarioAlunoSemNota(seqTurma, seqColaborador, lancamentoNotaParcial);

            Turma turma = SearchByKey(new SMCSeqSpecification<Turma>(seqTurma), a => a.ConfiguracoesComponente, 
                                                                                a => a.CicloLetivoInicio, 
                                                                                a => a.ConfiguracoesComponente[0].ConfiguracaoComponente, 
                                                                                a => a.ConfiguracoesComponente[0].RestricoesTurmaMatriz);
            ConfiguracaoComponente cc = ConfiguracaoComponenteDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoComponente>(turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().SeqConfiguracaoComponente), a => a.ComponenteCurricular.NiveisEnsino);

            TurmaHistoricoFechamentoDiario th = new TurmaHistoricoFechamentoDiario { DiarioFechado = true, SeqTurma = seqTurma };
            TurmaHistoricoFechamentoDiarioDomainService.SaveEntity(th);

            // Enviar mensagens para a linha do tempo dos alunos da turma
            var retorno = BuscarDiarioTurmaAluno(seqTurma, null, null, null);

            var descricaoCicloLetivoInicio = turma.CicloLetivoInicio.Descricao;
            var codigoTurma = turma.Codigo;
            var seqComponenteCurricular = turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.SeqComponenteCurricular;
            var seqComponenteCurricularAssunto = turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqComponenteCurricularAssunto;
            var listaHistoricoEscolarAprovado = new List<HistoricoEscolarAprovadoFiltroVO>();
            var historicoEscolarAprovado = new HistoricoEscolarAprovadoFiltroVO()
            {
                SeqComponenteCurricular = seqComponenteCurricular,
                SeqComponenteCurricularAssunto = seqComponenteCurricularAssunto,
                SeqTurma = seqTurma
            };
            listaHistoricoEscolarAprovado.Add(historicoEscolarAprovado);

            foreach (var aluno in retorno)
            {
                    if (aluno.Aprovado.HasValue && aluno.Aprovado.Value)
                    {
                        // Verifica se o aluno possui o mesmo componente em outro palno de estudo
                        var componentePlanoSemHistorico = SolicitacaoServicoDomainService.VerificarTurmasPlanoEstudoSemHistorico(aluno.SeqPessoaAtuacao, listaHistoricoEscolarAprovado);
                        if (componentePlanoSemHistorico != null && componentePlanoSemHistorico.Count > 0)
                        {
                            // Chama método que realiza a regra RN_APR_047
                            var justificativa = string.Format("Removido item aprovado na turma: {0}", turma.Codigo);
                            var situacaoHistorico = "Aprovado";
                            var lista = componentePlanoSemHistorico.Select(i => new PlanoEstudoAlterarVO() { SeqPlanoEstudo = i.SeqPlanoEstudo, SeqPlanoEstudoItemRemover = i.SeqPlanoEstudoItem }).ToList();
                            PlanoEstudoDomainService.RemoverItensPlanoEstudoAtual(lista, null, justificativa, descricaoCicloLetivoInicio, situacaoHistorico);
                        }

                        // Verifica se o aluno possui solicitação em aberto com o componente que está tendo o diário fechado
                        // RN_APR_048 - Atualização de solicitações ativas
                        SolicitacaoServicoDomainService.VerificarSolicitacaoAlteracaoPlanoEmAberto(aluno.SeqPessoaAtuacao, listaHistoricoEscolarAprovado);
                    }
            }

            List<long> listaAlunos = retorno.Select(a => a.SeqPessoaAtuacao).ToList();
            MensagemDomainService.EnviarMensagemPessoasAtuacao(listaAlunos, cc.ComponenteCurricular.SeqInstituicaoEnsino, cc.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino, TOKEN_TIPO_MENSAGEM.FECHAMENTO_DIARIO, CategoriaMensagem.LinhaDoTempo);
        }

        /// <summary>
        /// Realiza a consulta para o relatório de turmas por ciclo letivo de acordo com filtros da tela
        /// </summary>
        /// <param name="filtro">Objeto com filtros da tela</param>
        /// <returns>Lista de turmas para o relatório</returns>
        public List<TurmaCicloLetivoRelatorioVO> BuscarRelatorioTurmasPorCicloLetivo(TurmaCicloLetivoRelatorioFiltroVO filtro)
        {
            var specTurma = filtro.Transform<TurmaFilterSpecification>();

            if (filtro.SeqsEntidadesResponsaveis.Count > 0)
                specTurma.SeqsEntidadeResponsavelPrograma = filtro.SeqsEntidadesResponsaveis;

            specTurma.MaxResults = int.MaxValue;
            var seqsTurma = SearchProjectionBySpecification(specTurma, x => x.Seq, true).ToList();

            if (seqsTurma.Count == 0)
                return new List<TurmaCicloLetivoRelatorioVO>();

            string nivelFiltro = specTurma.SeqNivelEnsino.HasValue ? specTurma.SeqNivelEnsino.Value.ToString() : "NULL";

            var registros = RawQuery<TurmaCicloLetivoRelatorioVO>(string.Format(_buscarRelatorioTurmaPorCicloLetivoComColaboradores, nivelFiltro, string.Join(" , ", seqsTurma)));

            return registros;
        }

        /// <summary>
        /// As divisões de uma configuração de um componente deverão ser exibidas sempre com a seguinte concatenação de dados:
        /// [Número da divisão] + "-" + [Tipo divisão componente] + "-" + [Carga horária] + [Label parametrizado].
        /// O label parametrizado é o conteúdo do campo Formato em Parâmetros por Instituição e Nível de
        /// Ensino, para o tipo do componente em questão.
        /// Na ausência da carga horária ou do crédito, retirar o respectivo label.
        /// </summary>
        /// <param name="divisoesTurma"></param>
        private void FormatarDescricaoListaDivisaoComponente(IEnumerable<DivisaoTurmaCicloLetivoVO> divisoesTurma)
        {
            if (divisoesTurma == null) { return; }

            foreach (var divisao in divisoesTurma)
            {
                divisao.DivisaoComponente = FormatarDescricaoDivisaoComponente(divisao);
            }
        }

        /// <summary>
        /// As divisões de uma configuração de um componente deverão ser exibidas sempre com a seguinte concatenação de dados:
        /// [Número da divisão] + "-" + [Tipo divisão componente] + "-" + [Carga horária] + [Label parametrizado].
        /// O label parametrizado é o conteúdo do campo Formato em Parâmetros por Instituição e Nível de
        /// Ensino, para o tipo do componente em questão.
        /// Na ausência da carga horária ou do crédito, retirar o respectivo label.
        /// <param name="divisao"></param>
        /// <returns>Descrição formatada [Número da divisão] + "-" + [Tipo divisão componente] + "-" + [Carga horária] + [Label parametrizado]</returns>
        private string FormatarDescricaoDivisaoComponente(DivisaoTurmaCicloLetivoVO divisao)
        {
            string descricao = string.Empty;

            if (divisao == null || !divisao.NumeroDivisao.HasValue || string.IsNullOrEmpty(divisao.TipoDivisaoComponente)) { return descricao; }

            string LabelParametrizado = divisao.CargaHoraria.HasValue ? BuscarFormatoCargaHoraria(divisao.SeqTipoComponenteCurricular) : "";

            string cargaHoraria = string.IsNullOrEmpty(LabelParametrizado) || !divisao.CargaHoraria.HasValue ? string.Empty
                : $" - {divisao.CargaHoraria} - {LabelParametrizado}";

            descricao = $"{divisao.NumeroDivisao} - {divisao.TipoDivisaoComponente}{cargaHoraria}";

            return descricao;
        }

        /// <summary>
        /// Método de busca da instituicaoNivelTipoComponente, conforme o Seq do Tipo de componente curricular.
        /// Para retornar seu formato de carga horária
        /// </summary>
        /// <param name="seqTipoComponenteCurricular"></param>
        /// <returns> formato de carga horária</returns>
        private string BuscarFormatoCargaHoraria(long? seqTipoComponenteCurricular)
        {
            if (!seqTipoComponenteCurricular.HasValue) { return string.Empty; }

            var instituicaoNivelTipoComponente = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(0, seqTipoComponenteCurricular.Value);

            if (instituicaoNivelTipoComponente == null || instituicaoNivelTipoComponente.FormatoCargaHoraria == null) { return string.Empty; }

            return instituicaoNivelTipoComponente.FormatoCargaHoraria.SMCGetDescription();
        }

        /// <summary>
        /// Método que valida se o nome do colaborador deve ser exibido ou não para a Turma
        /// </summary>
        /// <param name="turmasConfiguracao"></param>
        private void ValidarMostrarColaboradorRelatorio(IEnumerable<TurmaConfiguracaoCicloLetivoVO> turmasConfiguracao)
        {
            if (turmasConfiguracao == null || turmasConfiguracao.Count() == 0) { return; }

            //TODO: Validar
            /*Método temporário*/
            foreach (var turmaConfiguracao in turmasConfiguracao)
            {
                //OcultarColaborador = x.DivisoesTurma.Select(df => df.DivisoesOrganizacao.Select(dos => dos.Colaboradores)).Count() > 1,

                //var temp = turmaConfiguracao.DivisoesTurma?.SelectMany(a => a.ItensDivisaoComponente.SelectMany(d => d.TopicosDivisao.SelectMany(t => t.TopicosDivisaoColaboradoresTurma)));

                var ItensDivisaoComponente = turmaConfiguracao.DivisoesTurma?.SelectMany(a => a.ItensDivisaoComponente);

                var TopicosDivisao = ItensDivisaoComponente.SelectMany(d => d.TopicosDivisao);

                var TopicosDivisaoColaboradoresTurma = TopicosDivisao.SelectMany(t => t.TopicosDivisaoColaboradoresTurma);

                turmaConfiguracao.OcultarColaborador = (TopicosDivisaoColaboradoresTurma?.Count() > 0 || turmaConfiguracao?.Colaboradores?.Count() == 0) ? true : false;
            }
        }

        /// <summary>
        /// Configura a Descrição do Assunto do componente curricular
        /// O componente curricular deverá ser exibido com a seguinte concatenação de dados:
        /// [Código do componente] + "-" + [Descrição] + "-" + [Carga horária] + [label parametrizado] + "-" +
        /// [Créditos] + "Créditos".
        /// O label parametrizado é o conteúdo do campo Formato em Parâmetros por Instituição e Nível de
        /// Ensino, para o tipo do componente em questão.
        /// Na ausência da carga horária ou do crédito, retirar o respectivo label.
        /// </summary>
        /// <param name="enumerable"></param>
        private void FormatarDescricaoAssuntoComponenteCurricular(IEnumerable<TurmaConfiguracaoCicloLetivoVO> turmasConfiguracao)
        {
            if (turmasConfiguracao == null || turmasConfiguracao.Count() == 0) { return; }

            foreach (var turmaConfiguracao in turmasConfiguracao)
            {
                if (turmaConfiguracao.SeqComponenteCurricularAssunto.HasValue)
                {
                    var filtroComponentes = new ConfiguracaoComponenteFilterSpecification() { SeqComponenteCurricular = turmaConfiguracao.SeqComponenteCurricularAssunto };
                    var componentesDescricaoCompleta = ConfiguracaoComponenteDomainService.BuscarConfiguracoesComponentes(filtroComponentes);
                    string descComponente = componentesDescricaoCompleta.FirstOrDefault(w => w.SeqComponenteCurricular == turmaConfiguracao.SeqComponenteCurricularAssunto)?.DescricaoFormatada;
                    turmaConfiguracao.ComponenteSubstituto = string.IsNullOrEmpty(descComponente) || descComponente == " - - " ? "" : descComponente;
                }
                else
                {
                    turmaConfiguracao.ComponenteSubstituto = null;
                }
            }
        }

        /// <summary>
        /// Retorna a quantidade de vagas ocupadas de uma divisão da turma ou o somatório do seus grupos de divisão
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma.</param>
        public long? BuscarQuantidadeVagasOcupadasTurma(long seqTurma)
        {
            var divisoesTurmaPorDivisaoComponente = this.SearchProjectionByKey(new SMCSeqSpecification<Turma>(seqTurma), p => p.DivisoesTurma.GroupBy(g => g.SeqDivisaoComponente)).ToList();
            var divisoesTurmaPrimeiraDivisaoComponente = divisoesTurmaPorDivisaoComponente.FirstOrDefault().ToList();
            var quantidadeVagasOcupadasTurma = divisoesTurmaPrimeiraDivisaoComponente.Sum(a => a.QuantidadeVagasOcupadas);

            return quantidadeVagasOcupadasTurma;
        }

        /// <summary>
        /// Desdobrar uma turma com seus relacionamentos criando novos registro zerando os sequenciais possíveis
        /// Mesmo código da turma atualizando apenas o número sequencial
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Sequencial da turma desdobrada</returns>
        public long DesdobrarTurma(long seq)
        {
            //Recupera os dados da turma que será desdobrada
            var turmaParcial = this.SearchByKey(new SMCSeqSpecification<Turma>(seq),
                                                    IncludesTurma.Colaboradores
                                                  | IncludesTurma.ConfiguracoesComponente
                                                  | IncludesTurma.DivisoesTurma
                                                  | IncludesTurma.HistoricosFechamentoDiario
                                                  //| IncludesTurma.HistoricosSituacao
                                                  | IncludesTurma.ConfiguracoesComponente_RestricoesTurmaMatriz
                                                  | IncludesTurma.DivisoesTurma_Colaboradores
                                                  | IncludesTurma.DivisoesTurma_DivisoesOrganizacao
                                                  | IncludesTurma.DivisoesTurma_Colaboradores_DivisoesOrganizacao
                                                  | IncludesTurma.DivisoesTurma_DivisoesOrganizacao_Colaboradores);

            if (turmaParcial != null)
            {
                var specSequencial = new TurmaFilterSpecification { Codigo = turmaParcial.Codigo };
                var sequencial = this.Max(specSequencial, m => m.Numero);

                turmaParcial.Seq = 0;
                turmaParcial.Numero = (short)(sequencial + 1);
                ZerarSequenciaisTurmaCopiarDesdobrar(turmaParcial);

                this.SaveEntity(turmaParcial);

                return turmaParcial.Seq;
            }
            else
                return seq;
        }

        /// <summary>
        /// Copiar uma turma com seus relacionamentos criando novos registro zerando os sequenciais possíveis
        /// Criar um novo código para turma
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Sequencial da turma copiada</returns>
        public long CopiarTurma(long seq)
        {
            //Recupera os dados da turma que será desdobrada
            var turmaParcial = this.SearchByKey(new SMCSeqSpecification<Turma>(seq),
                                                    IncludesTurma.Colaboradores
                                                  | IncludesTurma.ConfiguracoesComponente
                                                  | IncludesTurma.DivisoesTurma
                                                  | IncludesTurma.HistoricosFechamentoDiario
                                                  //| IncludesTurma.HistoricosSituacao
                                                  | IncludesTurma.ConfiguracoesComponente_RestricoesTurmaMatriz
                                                  | IncludesTurma.DivisoesTurma_Colaboradores
                                                  | IncludesTurma.DivisoesTurma_DivisoesOrganizacao
                                                  | IncludesTurma.DivisoesTurma_Colaboradores_DivisoesOrganizacao
                                                  | IncludesTurma.DivisoesTurma_DivisoesOrganizacao_Colaboradores);

            if (turmaParcial != null)
            {
                turmaParcial.Seq = 0;
                var codigoNumero = GerarCodigoNumeroTurma().Split('.');

                if (codigoNumero.Length == 2)
                {
                    turmaParcial.Codigo = Convert.ToInt16(codigoNumero[0]);
                    turmaParcial.Numero = Convert.ToInt16(codigoNumero[1]);
                }

                ZerarSequenciaisTurmaCopiarDesdobrar(turmaParcial);

                this.SaveEntity(turmaParcial);
                return turmaParcial.Seq;
            }
            else
                return seq;
        }

        private void ZerarSequenciaisTurmaCopiarDesdobrar(Turma turmaParcial)
        {
            turmaParcial.ConfiguracoesComponente.SMCForEach(f =>
            {
                f.Seq = 0;
                f.SeqTurma = 0;
                f.RestricoesTurmaMatriz.SMCForEach(r =>
                {
                    r.Seq = 0;
                    r.SeqTurmaConfiguracaoComponente = 0;
                });
            });
            turmaParcial.DivisoesTurma.SMCForEach(f =>
            {
                f.Seq = 0;
                f.SeqTurma = 0;
                f.QuantidadeVagas = 0;
                f.QuantidadeVagasOcupadas = 0;
                f.QuantidadeVagasReservadas = 0;
                f.Colaboradores.SMCForEach(r =>
                {
                    r.Seq = 0;
                    r.SeqDivisaoTurma = 0;
                });
            });
            turmaParcial.HistoricosFechamentoDiario.SMCForEach(f =>
            {
                f.Seq = 0;
                f.SeqTurma = 0;
            });
        }

        private void ZerarSequenciaisTurmaCopiarDesdobrar(TurmaVO turmaParcial)
        {
            /*Na inclusão, cópia e desdobramento, salvar os campos
           - "Quantidade de Vaga Ocupada" (não exibidos na interface), em cada divisão da turma, com valor igual a 0.
           - "Quantidade de Vaga Ocupada na matriz", para cada oferta de matriz associada à turma, com valor igual a 0.
           - "Quantidade de Vaga Reservada na matriz", caso não seja informado na interface, deverá receber o valor 0, para cada oferta de matriz associada à turma.*/

            turmaParcial.ConfiguracoesComponente.SMCForEach(f =>
            {
                f.Seq = 0;
                f.SeqTurma = 0;
                f.RestricoesTurmaMatriz.SMCForEach(r =>
                {
                    r.Seq = 0;
                    r.SeqTurmaConfiguracaoComponente = 0;
                });
            });
            turmaParcial.TurmaOfertasMatriz.SMCForEach(om =>
            {
                om.OfertasMatriz.SMCForEach(o =>
                {
                    o.QuantidadeVagasOcupadas = 0;
                });
            });
            turmaParcial.DivisoesTurma.SMCForEach(f =>
            {
                f.Seq = 0;
                f.SeqTurma = 0;
                f.QuantidadeVagasOcupadas = 0;
            });
            turmaParcial.HistoricosFechamentoDiario.SMCForEach(f =>
            {
                f.Seq = 0;
                f.SeqTurma = 0;
            });
        }

        /// <summary>
        /// Retorna a descrição da turma com a seguinte concatenação
        ///   [Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma].
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns>[Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma]</returns>
        public string BuscarDescricaoTurmaConcatenado(long seqTurma)
        {
            return SearchProjectionByKey(new SMCSeqSpecification<Turma>(seqTurma), a => new SMCDatasourceItem()
            {
                Descricao = a.Codigo + "." + a.Numero + " - " + a.ConfiguracoesComponente.Where(b => b.Principal).FirstOrDefault().Descricao
            })?.Descricao ?? string.Empty;
        }

        /// <summary>
        /// Retorna a descrição da turma para uma determinada matriz com a seguinte concatenação
        ///   [Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma].
        /// </summary>
        /// <param name="seqTurma"></param>
        /// <returns>[Código da Turma] + "." + [Número da Turma] + "-" + [Descrição da turma]</returns>
        public string BuscarDescricaoTurmaConcatenado(long seqTurma, long seqMatrizCurricularOferta)
        {
            var dadosTurma = this.SearchProjectionByKey(seqTurma, a => new
            {
                DescricaoConfiguracaoComponente = a.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == seqMatrizCurricularOferta)).Descricao,
                DescricaoConfiguracaoComponentePrincipal = a.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).Descricao,
                a.Codigo,
                a.Numero
            });
            if (dadosTurma == null)
                return string.Empty;

            return $"{dadosTurma.Codigo}.{dadosTurma.Numero} - {dadosTurma.DescricaoConfiguracaoComponente ?? dadosTurma.DescricaoConfiguracaoComponentePrincipal}";
        }

        /// <summary>
        /// Busca o sequencial da instituição de ensino da turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Sequencial da Instituição de ensino</returns>
        public long BuscarSeqInstituicaoEnsinoTurma(long seqTurma)
        {
            return this.SearchProjectionByKey(new SMCSeqSpecification<Turma>(seqTurma), x => x.ConfiguracoesComponente.FirstOrDefault(y => y.Principal).ConfiguracaoComponente.ComponenteCurricular.SeqInstituicaoEnsino);
        }

        /// <summary>
        /// Verifica se o diário de uma turma está fechado.
        /// </summary>
        /// <param name="filtro">Filtro da turma.</param>
        /// <returns>true, se estiver fechado ou false, se estiver aberto.</returns>
        public bool? DiarioTurmaFechado(TurmaFilterSpecification spec)
        {
            var historicoFechamentoDiario = SearchProjectionByKey(spec, a => a.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault());
            return historicoFechamentoDiario?.DiarioFechado;
        }

        /// <summary>
        /// Método que retorna uma nova turma, para ser Desmembrada, Copiada ou Criada.
        /// </summary>
        /// <returns>Objeto turma com seus itens</returns>
        public TurmaVO ExecutarOperacaoTurma(TurmaVO turma)
        {
            if (turma == null || turma.Operacao == OperacaoTurma.Novo || !turma.SeqTurma.HasValue) { return NovaTurma(); }

            var turmaParcial = BuscarTurma(turma.SeqTurma.Value);

            if (turmaParcial == null || turmaParcial.Seq == 0) { return NovaTurma(); }

            turma.OperacaoFix = turmaParcial.Operacao = turma.Operacao;
            switch (turma.Operacao)
            {
                case OperacaoTurma.Novo:

                    return NovaTurma();

                case OperacaoTurma.Copiar:

                    turmaParcial.Seq = 0;
                    turmaParcial.ConfirmarAlteracao = false;
                    break;

                case OperacaoTurma.Desdobrar:

                    turmaParcial.Seq = 0;
                    turmaParcial.ConfirmarAlteracao = true;
                    break;
            }

            ZerarSequenciaisTurmaCopiarDesdobrar(turmaParcial);

            AtribuirCodigoNumeroTurma(turmaParcial);

            // Valida se é desdobramento
            turmaParcial.EhOuPossuiDesdobramento = turmaParcial.Numero > 1 || this.Count(new TurmaFilterSpecification
            {
                Codigo = turma.Codigo,
            }) > 1;

            ValidarHabilitarCampos(turmaParcial);

            return turmaParcial;
        }

        public List<SMCDatasourceItem> BuscarTurmasPorCicloLetivoCursoOfertaLocalidadeTurnoSelect(long? seqCicloLetivo, long? seqCursoOfertaLocalidade, long? seqTurno)
        {
            if (!seqCicloLetivo.HasValue || !seqCursoOfertaLocalidade.HasValue || !seqTurno.HasValue)
                return new List<SMCDatasourceItem>();

            var specTurma = new TurmaCicloLetivoCursoOfertaLocalidadeTurnoFilterSpecification()
            {
                SeqCicloLetivo = seqCicloLetivo.GetValueOrDefault(),
                SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade.GetValueOrDefault(),
                SeqTurno = seqTurno
            };

            specTurma.SetOrderBy(t => t.ConfiguracoesComponente.FirstOrDefault().Descricao);

            var retorno = this.SearchProjectionBySpecification(specTurma, t => new
            {
                t.Seq,
                t.Codigo,
                t.Numero,
                t.ConfiguracoesComponente.FirstOrDefault(c => c.Principal).Descricao,
            }).ToList();

            return retorno.Select(r => new SMCDatasourceItem()
            {
                Seq = r.Seq,
                Descricao = $"{r.Codigo}.{r.Numero} - {r.Descricao}"
            }).ToList();
        }

        /// <summary>
        /// Cria uma nova turma com as regras dos campos aplicadas
        /// </summary>
        /// <returns></returns>
        private TurmaVO NovaTurma()
        {
            var turma = new TurmaVO();
            ValidarHabilitarCampos(turma);
            return turma;
        }

        private short GerarNumeroDesdobramentoTurma(int codigo)
        {
            var specSequencial = new TurmaFilterSpecification { Codigo = codigo };
            var sequencial = this.Max(specSequencial, m => m.Numero);

            return (short)(sequencial + 1);
        }

        public void ValidarConfiguracaoCompartilhdaRemovida(long seqTurma, List<long> seqsConfiguracoesCompartilhadas)
        {
            // Recupera quais as configurações de compartilhamento estão selecionadas
            var dadosConfiguracoesBanco = this.SearchProjectionByKey(seqTurma, x => x.ConfiguracoesComponente.Select(c => new
            {
                SeqConfiguracaoComponente = (long?)c.SeqConfiguracaoComponente,
                MatrizesCurricularesOferta = c.RestricoesTurmaMatriz.Select(r => new
                {
                    SeqMatrizCurricularOferta = (long?)r.SeqMatrizCurricularOferta,
                    r.MatrizCurricularOferta.Codigo
                }).ToList(),
                c.ConfiguracaoComponente.Codigo
            }))?.ToList();

            // Verifica se alguma foi removida
            var dadosRemovidos = dadosConfiguracoesBanco?.Where(b => b.SeqConfiguracaoComponente.HasValue && !seqsConfiguracoesCompartilhadas.Contains(b.SeqConfiguracaoComponente.Value)).ToList();

            // Recupera quais as matrizes fazem parte
            if (dadosRemovidos != null && dadosRemovidos.Any())
            {
                // Chama método que faz a validação de alunos e solicitações
                var matrizesProblema = DivisaoTurmaDomainService.ValidarOfertaMatrizExcluidas(seqTurma, dadosRemovidos.SelectMany(d => d.MatrizesCurricularesOferta.Where(s => s.SeqMatrizCurricularOferta.HasValue).Select(s => s.SeqMatrizCurricularOferta.Value)).ToList());
                if (matrizesProblema != null)
                {
                    // Recupera o código do componente curricular de acordo com a matriz que deu problema
                    var codigosExibicao = dadosConfiguracoesBanco.Where(d => d.MatrizesCurricularesOferta.Any(s => matrizesProblema.Contains(s.Codigo))).Select(s => s.Codigo).Distinct().ToList();
                    throw new MatrizCurricularOfertaConfiguracaoAlunoAssociadoExcluirException(codigosExibicao);
                }
            }
        }

        /// <summary>
        /// Buscar sequencial turma baseado em uma origem avaliação turma ou divisão de turma
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Sequencial da turma</returns>
        public long BuscarSeqTurmaPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            TipoOrigemAvaliacao tipoOrigemAvaliacao = OrigemAvaliacaoDomainService.BuscarOrigemAvaliacao(seqOrigemAvaliacao).TipoOrigemAvaliacao;
            long seqTurma = 0;

            if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                seqTurma = SearchProjectionBySpecification(new TurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => p.Seq).FirstOrDefault();
            }
            else if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.DivisaoTurma)
            {
                seqTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => p.SeqTurma).FirstOrDefault();
            }

            return seqTurma;
        }

        /// <summary>
        /// Verifica se existe lançamento de nota em uma turma para um determinado aluno em um ciclo letivo específico.
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <returns>Existe ou não lançamento de notas para turma</returns>
        public bool VerificarLancamentoNotaTurma(long seqAluno, long seqTurma, long seqCicloLetivo)
        {
            var seqAlunoHistorico = (long?)AlunoDomainService.SearchProjectionByKey(seqAluno, x => x.Historicos.FirstOrDefault(h => h.Atual).Seq);
            var seqOrigemAvaliacao = (long?)this.SearchProjectionByKey(seqTurma, x => x.SeqOrigemAvaliacao);

            // Verifica se existe lançamento de nota para tuma em questão
            return HistoricoEscolarDomainService.SearchProjectionBySpecification(new HistoricoEscolarFilterSpecification
            {
                SeqAlunoHistorico = seqAlunoHistorico.GetValueOrDefault(),
                SeqCicloLetivo = seqCicloLetivo,
                SeqOrigemAvaliacao = seqOrigemAvaliacao.GetValueOrDefault(),
                SituacoesHistoricoEscolar = new SituacaoHistoricoEscolar[] { SituacaoHistoricoEscolar.Aprovado, SituacaoHistoricoEscolar.Reprovado, SituacaoHistoricoEscolar.AlunoSemNota }
            }, x => x.SeqOrigemAvaliacao).Count() > 0;
        }

        public void ExcluirTurma(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<Turma>(seq));

                    /*RN_TUR_004 - Exclusão de Turma: Ao excluir uma turma que tenha associada (seq_agenda_turma preenchido),
                    excluir também a agenda no sistema de calendários conforme a RN_CLD_023 - Excluir agenda, passando
                    como parâmetro o seq_agenda_turma*/
                    if (configToDelete.SeqAgendaTurma.HasValue)
                    {
                        var existeAgenda = this.AgendaService.VerificaExistenciaAgenda(configToDelete.SeqAgendaTurma.Value);

                        //Se a turma tiver agenda, é verificada se essa agenda realmente existe no AGD, e se existir é excluida               
                        if (existeAgenda)
                            this.AgendaService.ExcluirAgenda(configToDelete.SeqAgendaTurma.Value);
                    }

                    var divisoesTurma = this.DivisaoTurmaDomainService.SearchBySpecification(new DivisaoTurmaFilterSpecification() { SeqTurma = seq }, x => x.HistoricosConfiguracaoGrade).ToList();

                    foreach (var divisaoTurma in divisoesTurma)
                    {
                        foreach (var configuracaoGrade in divisaoTurma.HistoricosConfiguracaoGrade)
                        {
                            if (divisaoTurma.SeqHistoricoConfiguracaoGradeAtual == configuracaoGrade.Seq)
                            {
                                divisaoTurma.SeqHistoricoConfiguracaoGradeAtual = null;
                                this.DivisaoTurmaDomainService.SaveEntity(divisaoTurma);
                            }

                            this.HistoricoDivisaoTurmaConfiguracaoGradeDomainService.DeleteEntity(configuracaoGrade);
                        }
                    }

                    this.DeleteEntity(configToDelete);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public void ValidarDatasPeriodoLetivo(TurmaVO turma)
        {
            if (turma.DataInicioPeriodoLetivo.HasValue && turma.DataFimPeriodoLetivo.HasValue && turma.DataFimPeriodoLetivo < turma.DataInicioPeriodoLetivo)
                throw new TurmaDataFimPeriodoLetivoException();
        }

        public void ValidarExclusaoDivisaoGrupos(TurmaVO turma)
        {
            if (turma.Seq > 0)
            {
                var divisoesTurma = this.DivisaoTurmaDomainService.SearchBySpecification(new DivisaoTurmaFilterSpecification() { SeqTurma = turma.Seq }, x => x.DivisaoComponente.ConfiguracaoComponente).ToList();
                string codigoDivisoesExcluidas = "";

                foreach (var divisaoBanco in divisoesTurma)
                {
                    #region Recuperando divisão através do número do grupo e turma

                    //var divisaoDescricao = divisaoBanco.DivisaoComponente.Numero.ToString();
                    //var grupoNumero = divisaoBanco.NumeroGrupo.ToString().PadLeft(3, '0');

                    //var divisaoTela = turma.TurmaDivisoes.SelectMany(a => a.DivisoesComponentes).FirstOrDefault(a => a.DivisaoDescricao == divisaoDescricao && a.GrupoNumero == grupoNumero);

                    #endregion Recuperando divisão através do número do grupo e turma

                    var divisaoTela = turma.TurmaDivisoes.SelectMany(a => a.DivisoesComponentes).FirstOrDefault(a => a.Seq == divisaoBanco.Seq);

                    //A divisão está sendo excluida, então tem que ser validado se ela pode mesmo ser excluída
                    if (divisaoTela == null)
                    {
                        var eventosDivisao = this.EventoAulaDomainService.SearchBySpecification(new EventoAulaFilterSpecification() { SeqDivisaoTurma = divisaoBanco.Seq }).ToList();

                        //Não permitir excluir grupo (divisão) se houver aluno matriculado nele
                        if (DivisaoTurmaDomainService.VerificarDivisaoTurmaMatriculaPlano(divisaoBanco.Seq, false))
                        {
                            codigoDivisoesExcluidas += $"</br>- {turma.Codigo}.{turma.Numero}.{divisaoBanco.DivisaoComponente.Numero}.{divisaoBanco.NumeroGrupo} ";
                        }

                        //Não permitir excluir grupo se existir eventos de aula criados para ele
                        if (eventosDivisao.Any())
                        {
                            throw new TurmaExcluirGrupoComEventoAulaException();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(codigoDivisoesExcluidas))
                {
                    codigoDivisoesExcluidas = codigoDivisoesExcluidas.Remove(codigoDivisoesExcluidas.Length - 1, 1);

                    throw new TurmaExcluirGrupoComEventoOuMatriculaException(codigoDivisoesExcluidas);
                }
            }
        }

        public void ValidarQuantidadeVagasOcupadasDivisoes(TurmaVO turma)
        {
            if (turma.Seq > 0)
            {
                bool exibirErro = false;
                StringBuilder descricoesDivisoes = new StringBuilder();

                var consultaDivisoes = DivisaoComponenteDomainService.SearchProjectionBySpecification(new DivisaoComponenteFilterSpecification
                {
                    SeqConfiguracaoComponente = turma.ConfiguracaoComponente
                }, x => new
                {
                    SeqDivisaoComponente = x.Seq,
                    x.Numero,
                    x.SeqConfiguracaoComponente,
                }).ToList();

                foreach (var divisao in turma.TurmaDivisoes)
                {
                    var divisaoConfig = consultaDivisoes.Where(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente).Single();

                    foreach (var item in divisao.DivisoesComponentes)
                    {
                        if (item.QuantidadeVagasOcupadas.HasValue && item.DivisaoVagas.Value < item.QuantidadeVagasOcupadas.Value)
                        {
                            exibirErro = true;
                            var codificacaoTurma = $"{turma.Codigo}.{turma.Numero}.{divisaoConfig.Numero}.{item.GrupoNumero}";
                            descricoesDivisoes.Append($"<br /> - {codificacaoTurma}: {item.QuantidadeVagasOcupadas.Value} {MessagesResource.MSG_Vagas_Ocupadas}");
                        }
                    }
                }

                if (exibirErro)
                    throw new TurmaQuantidadeVagasMenorVagasOcupadasException(descricoesDivisoes.ToString());
            }
        }

        public void ValidarQuantidadeGrupos(TurmaVO turma)
        {
            var count = 0;
            if (turma.Seq > 0) //Edição
            {
                var turmaBanco = this.SearchProjectionByKey(new SMCSeqSpecification<Turma>(turma.Seq), p => new
                {
                    p.Seq,
                    QuantidadeGrupos = p.DivisoesTurma.Select(s => s.OrigemAvaliacao).Select(sm => sm.QuantidadeGrupos).FirstOrDefault()
                });

                foreach (var divisao in turma.TurmaDivisoes)
                {
                    var quantidadeGrupoTurma = divisao.DivisoesComponentes.Count();
                    if (turmaBanco.QuantidadeGrupos != 0 && quantidadeGrupoTurma > turmaBanco.QuantidadeGrupos)
                        throw new QuantidadeGrupoExcedidoException();
                }
            }
            else //Inclusão
            {
                foreach (var divisao in turma.TurmaDivisoes)
                {
                    var quantidadeGrupoTurma = divisao.DivisoesComponentes.Count();
                    var divisoesComponente = turma.TurmaParametros.Where(w => w.SeqConfiguracaoComponente == turma.ConfiguracaoComponente)
                                                               .SelectMany(p => p.ParametrosOfertas).SelectMany(d => d.DivisoesComponente).ToList();
                    if (divisoesComponente != null)
                    {
                        var quantidadeGrupoMatriz = divisoesComponente[count].QuantidadeGrupos;
                        if (quantidadeGrupoMatriz != 0 && quantidadeGrupoTurma > quantidadeGrupoMatriz)
                            throw new QuantidadeGrupoExcedidoException();
                    }
                    count++;
                }
            }
        }

        /// <summary>
        /// Buscar o período letivo da turma baseado em uma origem avaliação, para validar o cadastro de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Período letivo da turma</returns>
        public (DateTime Periodoinicio, DateTime PeriodoFinal) BuscarPeriodoLetivoTurmaPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            TipoOrigemAvaliacao tipoOrigemAvaliacao = OrigemAvaliacaoDomainService.BuscarOrigemAvaliacao(seqOrigemAvaliacao).TipoOrigemAvaliacao;


            if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                var periodo = SearchProjectionBySpecification(new TurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => new { p.DataInicioPeriodoLetivo, p.DataFimPeriodoLetivo }).FirstOrDefault();
                return (periodo.DataInicioPeriodoLetivo, periodo.DataFimPeriodoLetivo);
            }
            else if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.DivisaoTurma)
            {
                var periodo = DivisaoTurmaDomainService.SearchProjectionBySpecification(new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => new { p.Turma.DataInicioPeriodoLetivo, p.Turma.DataFimPeriodoLetivo }).FirstOrDefault();
                return (periodo.DataInicioPeriodoLetivo, periodo.DataFimPeriodoLetivo);
            }

            return (new DateTime(), new DateTime());
        }

        /// <summary>
        /// Valida se o período da turma está encerrado
        /// </summary>
        /// <param name="seqDivisao">Sequencial da divisão da turma a ser validada</param>
        /// <param name="encerrado">True para encerrado ou false para vigente</param>
        /// <returns>True se a turma estiver no estado esperado</returns>
        public bool ValidarPeriodoTurma(long seqDivisao, bool encerrado)
        {
            var spec = new TurmaFilterSpecification()
            {
                SeqDivisaoTurma = seqDivisao,
                TurmasPeriodoEncerrado = encerrado
            };
            return Count(spec) == 1;
        }

        public ConfiguracaoTurmaVO ConfiguracaoTurma(long seq)
        {
            var spec = new TurmaFilterSpecification { Seq = seq };

            var retorno = this.SearchProjectionByKey(spec,
                                                     p => new ConfiguracaoTurmaVO()
                                                     {
                                                         Seq = p.Seq,
                                                         Codigo = p.Codigo,
                                                         Numero = p.Numero,
                                                         QuantidadeVagas = p.QuantidadeVagas,
                                                         DescricaoTipoTurma = p.TipoTurma.Descricao,
                                                         DescricaoCicloLetivoInicio = p.CicloLetivoInicio.Descricao,
                                                         SituacaoTurmaAtual = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma : SituacaoTurma.Nenhum,
                                                         SituacaoJustificativa = p.HistoricosSituacao.Count > 0 ? p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().Justificativa : "",
                                                         SeqOrigemAvaliacao = p.OrigemAvaliacao.Seq,
                                                         SeqCurriculoCursoOferta = p.OrigemAvaliacao.MatrizCurricularOferta.MatrizCurricular.CurriculoCursoOferta.Seq,
                                                         SeqCriterioAprovacao = p.OrigemAvaliacao.SeqCriterioAprovacao,
                                                         SeqEscalaApuracao = p.OrigemAvaliacao.SeqEscalaApuracao,
                                                         NotaMaxima = p.OrigemAvaliacao.CriterioAprovacao.NotaMaxima,
                                                         PercentualNotaAprovado = p.OrigemAvaliacao.CriterioAprovacao.PercentualNotaAprovado,
                                                         PercentualFrequenciaAprovado = p.OrigemAvaliacao.CriterioAprovacao.PercentualFrequenciaAprovado,
                                                         DescricaoEscalaApuracao = p.OrigemAvaliacao.CriterioAprovacao.EscalaApuracao.Descricao,
                                                         ApuracaoFrequencia = p.OrigemAvaliacao.CriterioAprovacao.ApuracaoFrequencia,
                                                         ApuracaoNota = p.OrigemAvaliacao.CriterioAprovacao.ApuracaoNota,
                                                         TipoEscalaApuracao = p.OrigemAvaliacao.CriterioAprovacao.EscalaApuracao.TipoEscalaApuracao,
                                                         SeqConfiguracaoComponente = p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).SeqConfiguracaoComponente,
                                                         SeqMatrizCurricularOferta = p.OrigemAvaliacao.SeqMatrizCurricularOferta,
                                                         PermiteAvaliacaoParcial = p.OrigemAvaliacao.PermiteAvaliacaoParcial,
                                                         OcorreuAlteracaoManual = p.OrigemAvaliacao.OcorreuAlteracaoManual.HasValue ? p.OrigemAvaliacao.OcorreuAlteracaoManual.Value : false,

                                                         DivisaoComponente = p.DivisoesTurma.Select(s => new ConfiguracaoTurmaDivisaoComponenteVO()
                                                         {
                                                             Seq = s.Seq,
                                                             SeqDivisaoComponente = s.SeqDivisaoComponente,
                                                             NumeroDivisaoComponente = s.DivisaoComponente.Numero,
                                                             SeqOrigemAvaliacao = s.SeqOrigemAvaliacao,
                                                             SeqTurma = s.SeqTurma,

                                                             TipoGestaoDivisaoComponente = s.DivisaoComponente.TipoDivisaoComponente.TipoGestaoDivisaoComponente,
                                                             SeqCriterioAprovacao = s.OrigemAvaliacao.SeqCriterioAprovacao,
                                                             QuantidadeGrupos = s.OrigemAvaliacao.QuantidadeGrupos,
                                                             QuantidadeProfessores = s.OrigemAvaliacao.QuantidadeProfessores,
                                                             NotaMaxima = s.OrigemAvaliacao.NotaMaxima,
                                                             ApurarFrequencia = s.OrigemAvaliacao.ApurarFrequencia,
                                                             MateriaLecionadaObrigatoria = s.OrigemAvaliacao.MateriaLecionadaObrigatoria,
                                                             SeqEscalaApuracao = s.OrigemAvaliacao.SeqEscalaApuracao,
                                                             TipoEscalaApuracao = s.OrigemAvaliacao.CriterioAprovacao.EscalaApuracao.TipoEscalaApuracao,
                                                         }).ToList(),
                                                     });

            retorno.ListaCriterioAprovacao = CriterioAprovacaoDomainService.BuscarCriteriosAprovacaoNivelEnsinoPorCurriculoCursoOfertaSelect(retorno.SeqCurriculoCursoOferta);
            retorno.ListaEscalaApuracao = EscalaApuracaoDomainService.BuscarEscalasApuracaoNaoConceitoNivelEnsinoSelect(retorno.SeqCurriculoCursoOferta);

            retorno.DivisaoComponente.SMCForEach(s => s.Descricao = DivisaoComponenteDomainService.BuscarDescricaoDivisaoCompoente(s.SeqDivisaoComponente));

            var groupByDivisaoComponente = retorno.DivisaoComponente.GroupBy(g => g.SeqDivisaoComponente).Select(s => s.Key).ToList();
            var divisoes = new List<ConfiguracaoTurmaDivisaoComponenteVO>();
            foreach (var seqDivisaoComponente in groupByDivisaoComponente)
            {
                var divisao = retorno.DivisaoComponente.Where(w => w.SeqDivisaoComponente == seqDivisaoComponente).FirstOrDefault();
                divisoes.Add(divisao);
            }
            retorno.DivisaoComponente = divisoes;

            return retorno;
        }

        public void SalvarConfiguracaoTurma(ConfiguracaoTurmaVO configuracaoTurma)
        {
            ValidarModelo(configuracaoTurma);

            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var origemAvaliacao = OrigemAvaliacaoDomainService.SearchByKey(new SMCSeqSpecification<OrigemAvaliacao>(configuracaoTurma.SeqOrigemAvaliacao));

                    origemAvaliacao.SeqCriterioAprovacao = configuracaoTurma.SeqCriterioAprovacao;
                    origemAvaliacao.SeqEscalaApuracao = configuracaoTurma.SeqEscalaApuracao;
                    origemAvaliacao.NotaMaxima = configuracaoTurma.NotaMaxima;
                    origemAvaliacao.PermiteAvaliacaoParcial = configuracaoTurma.PermiteAvaliacaoParcial;
                    origemAvaliacao.OcorreuAlteracaoManual = configuracaoTurma.OcorreuAlteracaoManual;

                    OrigemAvaliacaoDomainService.UpdateEntity(origemAvaliacao);

                    foreach (var divisao in configuracaoTurma.DivisaoComponente)
                    {
                        var specDivisaoTurma = new DivisaoTurmaFilterSpecification { SeqTurma = divisao.SeqTurma, SeqDivisaoComponente = divisao.SeqDivisaoComponente };
                        var divisoesTurma = DivisaoTurmaDomainService.SearchBySpecification(specDivisaoTurma).ToList();

                        foreach (var divisaoTurma in divisoesTurma)
                        {
                            var divisaoOrigemAvaliacao = OrigemAvaliacaoDomainService.SearchByKey(new SMCSeqSpecification<OrigemAvaliacao>(divisaoTurma.SeqOrigemAvaliacao));

                            divisaoOrigemAvaliacao.SeqCriterioAprovacao = divisao.SeqCriterioAprovacao;
                            divisaoOrigemAvaliacao.SeqEscalaApuracao = divisao.SeqEscalaApuracao;
                            divisaoOrigemAvaliacao.QuantidadeGrupos = divisao.QuantidadeGrupos;
                            divisaoOrigemAvaliacao.QuantidadeProfessores = divisao.QuantidadeProfessores;
                            divisaoOrigemAvaliacao.NotaMaxima = divisao.NotaMaxima;
                            divisaoOrigemAvaliacao.ApurarFrequencia = divisao.ApurarFrequencia;
                            divisaoOrigemAvaliacao.MateriaLecionadaObrigatoria = divisao.MateriaLecionadaObrigatoria;
                            divisaoOrigemAvaliacao.OcorreuAlteracaoManual = configuracaoTurma.OcorreuAlteracaoManual;

                            OrigemAvaliacaoDomainService.UpdateEntity(divisaoOrigemAvaliacao);
                        }
                    }
                    unityOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            }
        }

        private void ValidarModelo(ConfiguracaoTurmaVO modelo)
        {
            if (modelo.SeqEscalaApuracao.HasValue && !modelo.NotaMaxima.HasValue)
            {
                //Se só tiver escala, obrigar escala na divisão, se só tiver nota máxima obrigar nota máxima na divisão
                if (modelo.DivisaoComponente.Any(a => !a.SeqEscalaApuracao.HasValue || a.NotaMaxima.HasValue))
                    throw new DivisaoMatrizCurricularComponenteComApenasEscalaException();
            }

            if (!modelo.SeqEscalaApuracao.HasValue && modelo.NotaMaxima.HasValue)
            {
                //Se só tiver escala, obrigar escala na divisão, se só tiver nota máxima obrigar nota máxima na divisão
                if (modelo.DivisaoComponente.Any(a => a.SeqEscalaApuracao.HasValue || !a.NotaMaxima.HasValue))
                    throw new DivisaoMatrizCurricularComponenteComApenasNotaMaximaException();
            }

            //Para cada divisão da configuração do componente, permitir cadastrar ou a nota máxima ou a escala de apuração, nunca os dois
            if (modelo.DivisaoComponente.Any(a => a.SeqEscalaApuracao.HasValue && a.NotaMaxima.HasValue))
                throw new DivisaoMatrizCurricularComponenteNotaEscalaSelecionadosException();

            //1.Se o critério de aprovação selecionado para a configuração do componente tiver sido parametrizado
            //para exigir apuração por nota, é obrigatório que pelo menos uma divisão da configuração tenha nota
            //máxima cadastrada e para as divisões sem nota, somente permitir tipos de escalas de apuração
            //diferentes de “Conceito”. Em caso de violação, abortar a operação e exibir a mensagem:
            //MENSAGEM:
            //“O critério de aprovação selecionado exige apuração por nota, portanto pelo menos uma divisão da
            //configuração deve ter nota máxima e as divisões sem nota devem ter tipo de escala de apuração diferente de “Conceito”.
            if ((!modelo.SeqEscalaApuracao.HasValue && modelo.NotaMaxima.HasValue) || modelo.ApuracaoNota)
            {
                var divisoesComNotaMaxima = modelo.DivisaoComponente.Where(w => w.NotaMaxima.GetValueOrDefault() > 0).ToList();
                var divisoesSemNotaMaximaComEscalaConceito = modelo.DivisaoComponente.Except(divisoesComNotaMaxima).Where(a => a?.TipoEscalaApuracao == TipoEscalaApuracao.Conceito).ToList();

                if (!divisoesComNotaMaxima.Any() || divisoesSemNotaMaximaComEscalaConceito.Any())
                    throw new DivisaoMatrizCurricularComponenteApuracaoNotaException();

                //3.Se mais de uma divisão da configuração possuir nota máxima, o somatório das notas deve ser igual
                //ao valor da nota máxima do critério de aprovação da configuração do componente.Em caso de
                //violação, abortar a operação e exibir a mensagem:
                //MENSAGEM:
                //“O somatório das notas máximas das divisões deve ser igual à nota máxima do critério de aprovação selecionado”.
                if (divisoesComNotaMaxima.Sum(s => s.NotaMaxima ?? 0) != modelo.NotaMaxima)
                    throw new DivisaoMatrizCurricularComponenteNotaMaximaException();
            }

            //2.Se o critério de aprovação selecionado para a configuração do componente tiver sido parametrizado
            //para exigir apuração por frequência, é obrigatório que pelo menos uma divisão da configuração tenha
            //apuração por frequência. Em caso de violação, abortar a operação e exibir a mensagem:
            //MENSAGEM:
            //“O critério de aprovação selecionado exige apuração por frequência, portanto pelo menos uma divisão
            //da configuração deve ter apuração por frequência também".
            var divisoesComApuracaoFrequencia = modelo.DivisaoComponente.Where(a => a.ApurarFrequencia.HasValue).ToList();
            if (modelo.ApuracaoFrequencia && !divisoesComApuracaoFrequencia.Any(a => a.ApurarFrequencia.Value))
                throw new DivisaoMatrizCurricularComponenteApuracaoFrequenciaException();

            //4.Se o critério de aprovação possuir escala de apuração de tipo diferente de “Conceito”, todas as
            //divisões só podem ter escalas de tipo deferente de “Conceito”. Em caso de violação, abortar a operação
            //e exibir a mensagem:
            //MENSAGEM:
            //“O critério de aprovação selecionado possui escala de apuração de tipo diferente de “Conceito”,
            //portanto todas as divisões da configuração devem ter escala de tipo diferente de “Conceito” também”.
            if (!modelo.ApuracaoNota)
            {
                var divisoesComEscalaConceito = modelo.DivisaoComponente.Where(a => a?.TipoEscalaApuracao == TipoEscalaApuracao.Conceito).ToList();
                if (modelo?.TipoEscalaApuracao != TipoEscalaApuracao.Conceito && divisoesComEscalaConceito.Any())
                    throw new DivisaoMatrizCurricularComponenteConceitoException();
            }

            var specApuracaoAvaliacao = new ApuracaoAvaliacaoFilterSpecification { SeqOrigemAvaliacao = modelo.SeqOrigemAvaliacao };
            var notas = ApuracaoAvaliacaoDomainService.SearchBySpecification(specApuracaoAvaliacao).ToList();
            var specHistoricoEscolar = new HistoricoEscolarFilterSpecification() { SeqOrigemAvaliacao = modelo.SeqOrigemAvaliacao };
            var historicoEscolar = HistoricoEscolarDomainService.SearchBySpecification(specHistoricoEscolar).ToList();
            if (notas.Any(a => a.Nota.HasValue) || historicoEscolar.Any(a => a.Nota.HasValue))
                throw new TurmaDivisaoComNotaCadastradaException();

            var specTurma = new TurmaFilterSpecification { Seq = modelo.Seq, SeqOrigemAvaliacao = modelo.SeqOrigemAvaliacao };
            var turma = this.SearchByKey(specTurma, d => d.OrigemAvaliacao);

            foreach (var divisao in modelo.DivisaoComponente)
            {
                var specDivisaoTurma = new DivisaoTurmaFilterSpecification { SeqTurma = divisao.SeqTurma, SeqDivisaoComponente = divisao.SeqDivisaoComponente };
                var divisoesTurma = DivisaoTurmaDomainService.SearchBySpecification(specDivisaoTurma, d => d.OrigemAvaliacao).ToList();

                foreach (var divisaoTurma in divisoesTurma)
                {
                    var spec = new AvaliacaoFilterSpecification { SeqOrigemAvaliacao = divisaoTurma.SeqOrigemAvaliacao };
                    var avaliacoes = AvaliacaoDomainService.BuscarAvaliacoes(spec);

                    if (avaliacoes.Any() && modelo.PermiteAvaliacaoParcial.HasValue && turma.OrigemAvaliacao.PermiteAvaliacaoParcial != modelo.PermiteAvaliacaoParcial.Value && !modelo.PermiteAvaliacaoParcial.Value)
                        throw new TurmaDivisaoComAvaliacaoCadastradaException();
                }
            }
        }

        public void SalvarConfiguracaoTurmaComComponenteMatriz(ConfiguracaoTurmaVO configuracaoTurma)
        {
            var divisaoFiltroData = new DivisaoMatrizCurricularComponenteFilterSpecification
            {
                SeqConfiguracaoComponente = configuracaoTurma.SeqConfiguracaoComponente,
                SeqMatrizCurricularOferta = configuracaoTurma.SeqMatrizCurricularOferta
            };
            var divisaoMatrizCurricularComponente = this.DivisaoMatrizCurricularComponenteDomainService.BuscarDivisaoMatrizCurricularComponente(divisaoFiltroData);

            configuracaoTurma.SeqCriterioAprovacao = divisaoMatrizCurricularComponente.SeqCriterioAprovacao;
            configuracaoTurma.SeqEscalaApuracao = divisaoMatrizCurricularComponente.SeqEscalaApuracao;

            if (!string.IsNullOrEmpty(divisaoMatrizCurricularComponente.CriterioNotaMaxima))
                configuracaoTurma.NotaMaxima = short.Parse(divisaoMatrizCurricularComponente.CriterioNotaMaxima);
            else
                configuracaoTurma.NotaMaxima = null;

            var criterio = this.CriterioAprovacaoDomainService.BuscarCriterioAprovacao(configuracaoTurma.SeqCriterioAprovacao.Value);
            configuracaoTurma.ApuracaoFrequencia = criterio.ApuracaoFrequencia;
            configuracaoTurma.ApuracaoNota = criterio.ApuracaoNota;

            var seqCicloLetivoInicio = this.BuscarTurma(configuracaoTurma.Seq).SeqCicloLetivoInicio;
            configuracaoTurma.PermiteAvaliacaoParcial = PreencherPermiteAvaliacaoParcial(seqCicloLetivoInicio, configuracaoTurma.SeqCriterioAprovacao.Value);

            foreach (var divisao in configuracaoTurma.DivisaoComponente)
            {
                var divisaoComponente = divisaoMatrizCurricularComponente.DivisoesComponente.Where(w => w.SeqDivisaoComponente == divisao.SeqDivisaoComponente).FirstOrDefault();

                divisao.SeqCriterioAprovacao = null;
                divisao.SeqEscalaApuracao = divisaoComponente.SeqEscalaApuracao;
                divisao.QuantidadeGrupos = divisaoComponente.QuantidadeGrupos;
                divisao.QuantidadeProfessores = divisaoComponente.QuantidadeProfessores;
                divisao.NotaMaxima = divisaoComponente.NotaMaxima;
                divisao.ApurarFrequencia = divisaoComponente.ApurarFrequencia;
                divisao.MateriaLecionadaObrigatoria = divisaoComponente.MateriaLecionadaObrigatoria;
            }

            this.SalvarConfiguracaoTurma(configuracaoTurma);
        }
    }
}