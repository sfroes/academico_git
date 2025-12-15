using Newtonsoft.Json;
using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Common.Areas.CNC.Models;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Exceptions.SolicitacaoServico;
using SMC.Academico.Common.Areas.SRC.Includes;
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
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Resources;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Helpers;
using SMC.Academico.Domain.Models;
using SMC.Academico.Domain.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Financeiro.BLT.Common;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Financeiro.ServiceContract.BLT.Data;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.DataFilters;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoServicoDomainService : AcademicoContextDomain<SolicitacaoServico>
    {
        #region [ Querys ]

        private const string QUERY_RELATORIO_SOLICITACOES_COM_BLOQUEIO =
            @"
            declare
	            @LISTA_PROCESSOS varchar(255),
	            @SEQ_ETAPA_SGF bigint,
				@LISTA_SITUACOES varchar(255),
                @DATA_ATUAL DATETIME
            declare @TMP_PROCESSOS TABLE(item bigint)
            declare @TMP_SITUACOES TABLE(item bigint)

            set	@LISTA_PROCESSOS = {0}
			set @SEQ_ETAPA_SGF = {1}
			set	@LISTA_SITUACOES = {2}
            set @DATA_ATUAL = GETDATE()
            insert into @TMP_PROCESSOS select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_PROCESSOS, ',')
            insert into @TMP_SITUACOES select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_SITUACOES, ',')

			-- bloqueios da pessoa-atuacao
            select
	            -- solicitação/processo/etapa/situacao
	            ss.seq_solicitacao_servico as Seq,
	            ss.num_protocolo as NumeroProtocolo,
                ss.seq_pessoa_atuacao as SeqPessoaAtuacao,
	            case
		            when dp.nom_social is not null then rtrim(dp.nom_social) + ' (' + rtrim(dp.nom_pessoa) + ')'
		            else dp.nom_pessoa
	            end as Solicitante,
	            p.seq_processo as SeqProcesso,
	            p.dsc_processo as Processo,
	            p.dat_inicio as DataInicioProcesso,
	            pre.seq_etapa_sgf as SeqEtapaSGF,
	            pre.num_ordem as OrdemEtapa,
	            shs.idt_dom_categoria_situacao as CategoriaSituacao,
	            shs.seq_situacao_etapa_sgf as SeqSituacaoEtapaSGF,
                sr.seq_template_processo_sgf as SeqTemplateProcessoSGF,
	            -- bloqueio
	            pab.seq_motivo_bloqueio as SeqMotivoBloqueio,
	            tb.dsc_tipo_bloqueio as TipoBloqueio,
	            mb.dsc_motivo_bloqueio as MotivoBloqueio,
	            pab.dsc_referencia_atuacao as Referente,
	            pab.dat_bloqueio as DataBloqueio,
                ceb.ind_impede_inicio_etapa as ImpedeInicioEtapa,
                ceb.ind_impede_fim_etapa as ImpedeFimEtapa
            from	src.solicitacao_servico ss
            join	src.configuracao_processo cp
		            on ss.seq_configuracao_processo = cp.seq_configuracao_processo
            join	src.processo p
		            on cp.seq_processo = p.seq_processo
            join	src.servico sr
		            on p.seq_servico = sr.seq_servico
            join	pes.pessoa_atuacao pa
		            on ss.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
            join	pes.pessoa_dados_pessoais dp
		            on pa.seq_pessoa_dados_pessoais = dp.seq_pessoa_dados_pessoais
            join	src.solicitacao_servico_etapa sse
		            on ss.seq_solicitacao_servico = sse.seq_solicitacao_servico
		            and ss.seq_solicitacao_historico_situacao_atual = sse.seq_solicitacao_historico_situacao_atual
            join	src.solicitacao_historico_situacao shs
		            on sse.seq_solicitacao_servico_etapa = shs.seq_solicitacao_servico_etapa
		            and sse.seq_solicitacao_historico_situacao_atual = shs.seq_solicitacao_historico_situacao
            join	src.configuracao_etapa ce
		            on sse.seq_configuracao_etapa = ce.seq_configuracao_etapa
            join	src.processo_etapa pre
		            on ce.seq_processo_etapa = pre.seq_processo_etapa
            join	src.configuracao_etapa_bloqueio ceb
		            on ce.seq_configuracao_etapa = ceb.seq_configuracao_etapa
		            --and	ceb.ind_impede_fim_etapa = 1
		            and	ceb.idt_dom_ambito_bloqueio = 2	-- Pessoa-Atuação
            join	pes.pessoa_atuacao_bloqueio pab
		            on ss.seq_pessoa_atuacao = pab.seq_pessoa_atuacao
		            and pab.seq_motivo_bloqueio = ceb.seq_motivo_bloqueio
		            and pab.idt_dom_situacao_bloqueio = 1 -- Bloqueado
		            and pab.dat_bloqueio <= @DATA_ATUAL --shs.dat_inclusao
            join	pes.motivo_bloqueio mb
		            on pab.seq_motivo_bloqueio = mb.seq_motivo_bloqueio
            join	pes.tipo_bloqueio tb
		            on mb.seq_tipo_bloqueio = tb.seq_tipo_bloqueio
            where	p.seq_processo in (select item from @TMP_PROCESSOS)
            and		pre.seq_etapa_sgf = isnull(@SEQ_ETAPA_SGF, pre.seq_etapa_sgf)
			and     shs.seq_situacao_etapa_sgf not in (select item from @TMP_SITUACOES)

            union

            -- bloqueios da pessoa
            select
	            -- solicitação/processo/etapa/situacao
	            ss.seq_solicitacao_servico,
	            ss.num_protocolo,
                ss.seq_pessoa_atuacao,
	            case
		            when dp.nom_social is not null then rtrim(dp.nom_social) + ' (' + rtrim(dp.nom_pessoa) + ')'
		            else dp.nom_pessoa
	            end as nom_solicitante_formatado,
	            p.seq_processo,
	            p.dsc_processo,
	            p.dat_inicio,
	            pre.seq_etapa_sgf,
	            pre.num_ordem as num_ordem_etapa,
	            shs.idt_dom_categoria_situacao,
	            shs.seq_situacao_etapa_sgf,
                sr.seq_template_processo_sgf as SeqTemplateProcessoSGF,
	            -- bloqueio
	            pab.seq_motivo_bloqueio,
	            tb.dsc_tipo_bloqueio,
	            mb.dsc_motivo_bloqueio,
	            pab.dsc_referencia_atuacao,
	            pab.dat_bloqueio,
                ceb.ind_impede_inicio_etapa,
                ceb.ind_impede_fim_etapa
            from	src.solicitacao_servico ss
            join	src.configuracao_processo cp
		            on ss.seq_configuracao_processo = cp.seq_configuracao_processo
            join	src.processo p
		            on cp.seq_processo = p.seq_processo
            join	src.servico sr
		            on p.seq_servico = sr.seq_servico
            join	pes.pessoa_atuacao pa
		            on ss.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
            join	pes.pessoa_dados_pessoais dp
		            on pa.seq_pessoa_dados_pessoais = dp.seq_pessoa_dados_pessoais
            join	src.solicitacao_servico_etapa sse
		            on ss.seq_solicitacao_servico = sse.seq_solicitacao_servico
		            and ss.seq_solicitacao_historico_situacao_atual = sse.seq_solicitacao_historico_situacao_atual
            join	src.solicitacao_historico_situacao shs
		            on sse.seq_solicitacao_servico_etapa = shs.seq_solicitacao_servico_etapa
		            and sse.seq_solicitacao_historico_situacao_atual = shs.seq_solicitacao_historico_situacao
            join	src.configuracao_etapa ce
		            on sse.seq_configuracao_etapa = ce.seq_configuracao_etapa
            join	src.processo_etapa pre
		            on ce.seq_processo_etapa = pre.seq_processo_etapa
            join	src.configuracao_etapa_bloqueio ceb
		            on ce.seq_configuracao_etapa = ceb.seq_configuracao_etapa
		            --and	ceb.ind_impede_fim_etapa = 1
		            and	ceb.idt_dom_ambito_bloqueio = 1 -- Pessoa
            join	pes.pessoa_atuacao_bloqueio pab
		            on pab.seq_motivo_bloqueio = ceb.seq_motivo_bloqueio
		            and pab.idt_dom_situacao_bloqueio = 1 -- Bloqueado
		            and pab.dat_bloqueio <= @DATA_ATUAL --shs.dat_inclusao
            join	pes.pessoa_atuacao pa_pab
		            on pab.seq_pessoa_atuacao = pa_pab.seq_pessoa_atuacao
		            and pa_pab.seq_pessoa = pa.seq_pessoa
            join	pes.motivo_bloqueio mb
		            on pab.seq_motivo_bloqueio = mb.seq_motivo_bloqueio
            join	pes.tipo_bloqueio tb
		            on mb.seq_tipo_bloqueio = tb.seq_tipo_bloqueio
            where	p.seq_processo in (select item from @TMP_PROCESSOS)
            and		pre.seq_etapa_sgf = isnull(@SEQ_ETAPA_SGF, pre.seq_etapa_sgf)
			and     shs.seq_situacao_etapa_sgf not in (select item from @TMP_SITUACOES)

            order by
	            DataInicioProcesso desc,
	            Solicitante asc
            ";

        private const string QUERY_PLANO_ESTUDO_ITEM_SEM_HISTORICO_ESCOLAR =
            @"select distinct
	            cc.seq_componente_curricular as SeqComponenteCurricular,
	            co.dsc_componente_curricular as DescricaoComponenteCurricular,
	            r.seq_componente_curricular_assunto as SeqComponenteCurricularAssunto,
	            coa.dsc_componente_curricular as DescricaoComponenteCurricularAssunto,
	            ahcl.seq_ciclo_letivo as SeqCicloLetivo,
	            cl.dsc_ciclo_letivo as DescricaoCicloLetivo,
                i.seq_plano_estudo as SeqPlanoEstudo,
	            i.seq_plano_estudo_item as SeqPlanoEstudoItem
            from	ALN.plano_estudo_item i
            join	ALN.plano_estudo p
		            on i.seq_plano_estudo = p.seq_plano_estudo
		            and p.ind_atual = 1
            join	ALN.aluno_historico_ciclo_letivo ahcl
		            on p.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
            join	ALN.aluno_historico ah
		            on ahcl.seq_aluno_historico = ah.seq_aluno_historico
		            and ah.ind_atual = 1
            join	TUR.divisao_turma dt
		            on i.seq_divisao_turma = dt.seq_divisao_turma
            join	TUR.turma t
		            on dt.seq_turma = t.seq_turma
            join	TUR.turma_configuracao_componente tcc
		            on tcc.seq_turma = t.seq_turma
            join	CUR.configuracao_componente cc
		            on tcc.seq_configuracao_componente = cc.seq_configuracao_componente
            join	TUR.restricao_turma_matriz r
		            on tcc.seq_turma_configuracao_componente = r.seq_turma_configuracao_componente
            join	CUR.componente_curricular co
		            on cc.seq_componente_curricular = co.seq_componente_curricular
            left join	CUR.componente_curricular coa
			            on r.seq_componente_curricular_assunto = coa.seq_componente_curricular
            join	CAM.ciclo_letivo cl
		            on ahcl.seq_ciclo_letivo = cl.seq_ciclo_letivo
            where	i.seq_divisao_turma is not null
            and		not exists (select	1
					            from	APR.historico_escolar he
					            where	he.seq_origem_avaliacao = t.seq_origem_avaliacao
                                and     he.seq_aluno_historico = ah.seq_aluno_historico)
            and		ah.seq_atuacao_aluno = @SEQ_PESSOA_ATUACAO";

        #endregion [ Querys ]

        #region [ Services ]

        private IIntegracaoAcademicoService IntegracaoAcademicoService
        {
            get { return this.Create<IIntegracaoAcademicoService>(); }
        }

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService
        {
            get { return this.Create<IIntegracaoFinanceiroService>(); }
        }

        private IAplicacaoService AplicacaoService
        {
            get { return this.Create<IAplicacaoService>(); }
        }

        private IEtapaService EtapaService
        {
            get { return this.Create<IEtapaService>(); }
        }

        private INotificacaoService NotificacaoService
        {
            get { return this.Create<INotificacaoService>(); }
        }

        private ILocalidadeService LocalidadeService
        {
            get { return this.Create<ILocalidadeService>(); }
        }

        private ITipoDocumentoService TipoDocumentoService
        {
            get { return this.Create<ITipoDocumentoService>(); }
        }

        #endregion [ Services ]

        #region [ DomainServices ]

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();
        private ApuracaoFormacaoDomainService ApuracaoFormacaoDomainService => Create<ApuracaoFormacaoDomainService>();
        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();
        private TrabalhoAcademicoDivisaoComponenteDomainService TrabalhoAcademicoDivisaoComponenteDomainService => Create<TrabalhoAcademicoDivisaoComponenteDomainService>();
        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();
        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService => Create<DivisaoMatrizCurricularComponenteDomainService>();
        private SolicitacaoDocumentoConclusaoDomainService SolicitacaoDocumentoConclusaoDomainService => Create<SolicitacaoDocumentoConclusaoDomainService>();
        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();
        private InstituicaoNivelTipoDocumentoAcademicoDomainService InstituicaoNivelTipoDocumentoAcademicoDomainService => Create<InstituicaoNivelTipoDocumentoAcademicoDomainService>();
        private TipoDocumentoAcademicoServicoDomainService TipoDocumentoAcademicoServicoDomainService => Create<TipoDocumentoAcademicoServicoDomainService>();
        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();
        private SituacaoItemMatriculaDomainService SituacaoItemMatriculaDomainService => Create<SituacaoItemMatriculaDomainService>();
        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();
        private DivisaoComponenteDomainService DivisaoComponenteDomainService => Create<DivisaoComponenteDomainService>();
        private SolicitacaoMatriculaItemHistoricoSituacaoDomainService SolicitacaoMatriculaItemHistoricoSituacaoDomainService => Create<SolicitacaoMatriculaItemHistoricoSituacaoDomainService>();
        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();
        private AlunoHistoricoPrevisaoConclusaoDomainService AlunoHistoricoPrevisaoConclusaoDomainService => Create<AlunoHistoricoPrevisaoConclusaoDomainService>();
        private ComponenteCurricularDomainService ComponenteCurricularDomainService => Create<ComponenteCurricularDomainService>();
        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService => Create<ConfiguracaoEtapaDomainService>();
        private ConfiguracaoProcessoDomainService ConfiguracaoProcessoDomainService => Create<ConfiguracaoProcessoDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();
        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();
        private JustificativaSolicitacaoServicoDomainService JustificativaSolicitacaoServicoDomainService => Create<JustificativaSolicitacaoServicoDomainService>();
        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService => Create<MatrizCurricularOfertaDomainService>();
        private MensagemDomainService MensagemDomainService => Create<MensagemDomainService>();
        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();
        private ProcessoEtapaDomainService ProcessoEtapaDomainService => Create<ProcessoEtapaDomainService>();
        private RegistroDocumentoDomainService RegistroDocumentoDomainService => Create<RegistroDocumentoDomainService>();
        private RequisitoDomainService RequisitoDomainService => Create<RequisitoDomainService>();
        private RestricaoSolicitacaoSimultaneaDomainService RestricaoSolicitacaoSimultaneaDomainService => Create<RestricaoSolicitacaoSimultaneaDomainService>();
        private ServicoDomainService ServicoDomainService => Create<ServicoDomainService>();
        private SituacaoMatriculaDomainService SituacaoMatriculaDomainService => Create<SituacaoMatriculaDomainService>();
        private SolicitacaoArtigoDomainService SolicitacaoArtigoDomainService => Create<SolicitacaoArtigoDomainService>();
        private SolicitacaoAtividadeComplementarDomainService SolicitacaoAtividadeComplementarDomainService => Create<SolicitacaoAtividadeComplementarDomainService>();
        private SolicitacaoDadoFormularioDomainService SolicitacaoDadoFormularioDomainService => Create<SolicitacaoDadoFormularioDomainService>();
        private SolicitacaoDispensaDomainService SolicitacaoDispensaDomainService => Create<SolicitacaoDispensaDomainService>();
        private SolicitacaoHistoricoNavegacaoDomainService SolicitacaoHistoricoNavegacaoDomainService => Create<SolicitacaoHistoricoNavegacaoDomainService>();
        private SolicitacaoHistoricoSituacaoDomainService SolicitacaoHistoricoSituacaoDomainService => Create<SolicitacaoHistoricoSituacaoDomainService>();
        private SolicitacaoHistoricoUsuarioResponsavelDomainService SolicitacaoHistoricoUsuarioResponsavelDomainService => Create<SolicitacaoHistoricoUsuarioResponsavelDomainService>();
        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();
        private SolicitacaoReaberturaMatriculaDomainService SolicitacaoReaberturaMatriculaDomainService => Create<SolicitacaoReaberturaMatriculaDomainService>();
        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();
        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService => Create<SolicitacaoServicoEtapaDomainService>();
        private SituacaoDocumentoAcademicoDomainService SituacaoDocumentoAcademicoDomainService => Create<SituacaoDocumentoAcademicoDomainService>();
        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();
        private SolicitacaoIntercambioDomainService SolicitacaoIntercambioDomainService => Create<SolicitacaoIntercambioDomainService>();
        private DocumentoAcademicoHistoricoSituacaoDomainService DocumentoAcademicoHistoricoSituacaoDomainService => Create<DocumentoAcademicoHistoricoSituacaoDomainService>();
        private InstituicaoNivelTipoTrabalhoDomainService InstituicaoNivelTipoTrabalhoDomainService => Create<InstituicaoNivelTipoTrabalhoDomainService>();
        private DocumentoConclusaoDomainService DocumentoConclusaoDomainService => Create<DocumentoConclusaoDomainService>();
        private ServicoTaxaDomainService ServicoTaxaDomainService => Create<ServicoTaxaDomainService>();
        private SolicitacaoServicoBoletoTaxaDomainService SolicitacaoServicoBoletoTaxaDomainService => Create<SolicitacaoServicoBoletoTaxaDomainService>();
        private ServicoParametroEmissaoTaxaDomainService ServicoParametroEmissaoTaxaDomainService => Create<ServicoParametroEmissaoTaxaDomainService>();
        private SolicitacaoServicoBoletoTituloDomainService SolicitacaoServicoBoletoTituloDomainService => Create<SolicitacaoServicoBoletoTituloDomainService>();
        private SolicitacaoServicoBoletoDomainService SolicitacaoServicoBoletoDomainService => Create<SolicitacaoServicoBoletoDomainService>();
        private MotivoBloqueioDomainService MotivoBloqueioDomainService => Create<MotivoBloqueioDomainService>();
        private SolicitacaoDadoFormularioCampoDomainService SolicitacaoDadoFormularioCampoDomainService => Create<SolicitacaoDadoFormularioCampoDomainService>();
        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();
        private EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();
        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();
        private DocumentoConclusaoFormacaoDomainService DocumentoConclusaoFormacaoDomainService => Create<DocumentoConclusaoFormacaoDomainService>();
        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService => Create<CursoFormacaoEspecificaDomainService>();
        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();
        private TipoNotificacaoDomainService TipoNotificacaoDomainService => Create<TipoNotificacaoDomainService>();
        private ConfiguracaoEtapaBloqueioDomainService ConfiguracaoEtapaBloqueioDomainService => Create<ConfiguracaoEtapaBloqueioDomainService>();
        private ProgramaDomainService ProgramaDomainService => Create<ProgramaDomainService>();
        private ProgramaTipoAutorizacaoBdpDomainService ProgramaTipoAutorizacaoBdpDomainService => Create<ProgramaTipoAutorizacaoBdpDomainService>();
        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();
        private OrientacaoDomainService OrientacaoDomainService => Create<OrientacaoDomainService>();
        private PessoaAtuacaoDocumentoDomainService PessoaAtuacaoDocumentoDomainService => Create<PessoaAtuacaoDocumentoDomainService>();
        private SolicitacaoDocumentoRequeridoDomainService SolicitacaoDocumentoRequeridoDomainService => Create<SolicitacaoDocumentoRequeridoDomainService>();
        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService => Create<DocumentoRequeridoDomainService>();
        private ClassificacaoInvalidadeDocumentoDomainService ClassificacaoInvalidadeDocumentoDomainService => Create<ClassificacaoInvalidadeDocumentoDomainService>();
        private AplicacaoAvaliacaoDomainService AplicacaoAvaliacaoDomainService => Create<AplicacaoAvaliacaoDomainService>();
        private SolicitacaoTrabalhoAcademicoDomainService SolicitacaoTrabalhoAcademicoDomainService => Create<SolicitacaoTrabalhoAcademicoDomainService>();

        #endregion [ DomainServices ]

        #region [ Apis ]

        public SMCApiClient APIDiplomaGAD => SMCApiClient.Create("DiplomaGAD");

        public SMCApiClient APIHistoricoGAD => SMCApiClient.Create("HistoricoGAD");

        #endregion [ Apis ]

        public SMCPagerData<SolicitacaoServicoListarVO> ListarSolicitacoesServico(SolicitacaoServicoFilterSpecification spec)
        {
            // Recupera o sequencial da aplicação do SAS
            var seqAplicacaoSAS = AplicacaoService.BuscarAplicacaoPelaSigla(SIGLA_APLICACAO.SGA_ADMINISTRATIVO).Seq;

            // Armazena o sequencial do usuário logado
            var seqUsuarioLogado = SMCContext.User.SMCGetSequencialUsuario();

            // Armazena os dados das etapas do SGF que são utilizadas nos templates das solicitações exibidas
            Dictionary<long, EtapaSimplificadaData[]> dadosEtapas = new Dictionary<long, EtapaSimplificadaData[]>();

            // Armazena a lista de retorno dos dados
            var listaRet = new List<SolicitacaoServicoListarVO>();
            int total = 0;

            // Busca os templates de processo
            var seqsTemplateProcessoSGF = SearchProjectionBySpecification(spec, x => x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf).Distinct().ToList();

            // Busca as configurações
            foreach (var seqTemplateProcessoSGF in seqsTemplateProcessoSGF)
            {
                // Busca no SGF e armazena no dicionário
                var etapaSGF = SGFHelper.BuscarEtapasSGFCache(seqTemplateProcessoSGF);
                dadosEtapas.Add(seqTemplateProcessoSGF, etapaSGF);
            }

            // Verifica quais as situações são diferentes de finalizadas com sucesso para filtrar
            var seqsSituacoesNaoFinalizadasComSucesso = dadosEtapas.SelectMany(d => d.Value.SelectMany(e => e.Situacoes?.Where(s => s.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.FinalizadoComSucesso).Select(es => es.Seq))).Distinct().ToList();

            // Filtra pelas situações
            spec.SeqsSituacoesEtapa = seqsSituacoesNaoFinalizadasComSucesso;

            // Busca as configurações do processo

            var resultPrimario = SearchProjectionBySpecification(spec,
            s => new
            {
                Seq = s.Seq,
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                SeqSolicitacaoServicoEtapa = s.SituacaoAtual.SeqSolicitacaoServicoEtapa,
                NumeroProtocolo = s.NumeroProtocolo,
                Nome = s.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = s.PessoaAtuacao.DadosPessoais.NomeSocial,
                DescricaoProcesso = s.ConfiguracaoProcesso.Processo.Descricao,
                DataInclusao = s.DataInclusao,
                DataPrevistaSolucao = s.DataPrevistaSolucao,
                SituacaoDocumentacao = s.SituacaoDocumentacao,
                SeqProcesso = s.ConfiguracaoProcesso.SeqProcesso,
                CodigoAdesao = (s as SolicitacaoMatricula).CodigoAdesao,
                SeqEtapaAtual = s.SituacaoAtual.SeqSolicitacaoServicoEtapa,
                SeqSituacaoEtapaAtualSGF = s.SituacaoAtual.SeqSituacaoEtapaSgf,
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SolicitacaoPossuiGrupoEscalonamento = s.GrupoEscalonamento != null,
                SeqEntidadeCompartilhada = (long?)s.EntidadeCompartilhada.Seq,
                SolicitacaoComAtendimentoIniciado = s.UsuariosResponsaveis.Any(),
                UsuarioLogadoEResponsavelAtualPelaSolicitacao = s.UsuariosResponsaveis.OrderByDescending(o => o.Seq).FirstOrDefault().SeqUsuarioResponsavel == seqUsuarioLogado.Value,
                SeqConfiguracaoEtapaAtual = s.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                TokenAcessoAtendimento = s.ConfiguracaoProcesso.Processo.Servico.TokenAcessoAtendimento,
                GrupoEscalonamentoAtual = s.GrupoEscalonamento.Descricao,
                ProcessoEtapaAtual = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa,
                SeqSituacaoMatriculaAluno = (long?)s.AlunoHistorico.HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(o => !o.DataExclusao.HasValue).AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault(o => o.DataInicioSituacao <= DateTime.Today && !o.DataExclusao.HasValue).SeqSituacaoMatricula,
                SolicitacaoServicoEDeMatricula = s is SolicitacaoMatricula,
                SeqGrupoEscalonamento = s.SeqGrupoEscalonamento
            }, out total).ToList();

            foreach (var solServico in resultPrimario)
            {
                var resultSecundario = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(solServico.Seq), s => new
                {
                    SolicitacaoPossuiGrupoEscalonamentoVigente = s.GrupoEscalonamento.Itens.Any(i => i.Escalonamento.ProcessoEtapa.SeqEtapaSgf == solServico.ProcessoEtapaAtual.SeqEtapaSgf && (DateTime.Now >= i.Escalonamento.DataInicio && DateTime.Now <= s.ConfiguracaoProcesso.Processo.DataFim)),
                });

                var SolicitacaoPossuiProcessoEtapaVigente = true;

                if (solServico.ProcessoEtapaAtual.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia)
                    SolicitacaoPossuiProcessoEtapaVigente = DateTime.Now >= solServico.ProcessoEtapaAtual.DataInicio && (!solServico.ProcessoEtapaAtual.DataFim.HasValue || DateTime.Now <= solServico.ProcessoEtapaAtual.DataFim);
                else if (solServico.ProcessoEtapaAtual.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento)
                    SolicitacaoPossuiProcessoEtapaVigente = resultSecundario.SolicitacaoPossuiGrupoEscalonamentoVigente;

                var obj = new SolicitacaoServicoListarVO();
                obj.Seq = solServico.Seq;
                obj.SeqSolicitacaoMatricula = solServico.Seq;
                obj.SeqSolicitacaoServicoEtapa = solServico.SeqSolicitacaoServicoEtapa;
                obj.SeqIngressante = solServico.SeqPessoaAtuacao;
                obj.SeqProcesso = solServico.SeqProcesso;
                obj.NumeroProtocolo = solServico.NumeroProtocolo;
                obj.Nome = solServico.Nome;
                obj.NomeSocial = solServico.NomeSocial;
                obj.Solicitante = string.IsNullOrEmpty(obj.NomeSocial) ? obj.Nome : $"{obj.NomeSocial} ({obj.Nome})";
                obj.Processo = solServico.DescricaoProcesso;
                obj.DataInclusao = solServico.DataInclusao;
                obj.DataPrevistaSolucao = solServico.DataPrevistaSolucao;
                obj.SituacaoDocumentacao = solServico.SituacaoDocumentacao;
                obj.SituacaodDocumentacaoNaoRequerida = solServico.SituacaoDocumentacao == SituacaoDocumentacao.NaoRequerida;
                obj.CodigoAdesao = solServico.CodigoAdesao;
                obj.InstructionsChancela = string.Empty;
                obj.InstructionsEfetivacaoMatricula = string.Empty;
                obj.SolicitacaoServicoEDeMatricula = solServico.SolicitacaoServicoEDeMatricula;
                obj.SeqGrupoEscalonamento = solServico.SeqGrupoEscalonamento.HasValue ? solServico.SeqGrupoEscalonamento.Value : 0;

                obj.SituacaoAtualEtapaDiferenteLiberada = solServico.ProcessoEtapaAtual.SituacaoEtapa != SituacaoEtapa.Liberada;
                obj.EtapaAtualIndisponivelCentralAtendimento = !solServico.ProcessoEtapaAtual.CentralAtendimento;
                obj.SolicitacaoPossuiGrupoEscalonamentoComPeriodoNaoVigente = (solServico.SolicitacaoPossuiGrupoEscalonamento && !resultSecundario.SolicitacaoPossuiGrupoEscalonamentoVigente) || (!solServico.SolicitacaoPossuiGrupoEscalonamento && !SolicitacaoPossuiProcessoEtapaVigente);

                //Realiza um count para saber se o usuario logado tem acesso a entidade responsavel
                //O filtro de dados deverá tratar isso por padrão, caso o usuário não tenha acesso a entidade, o count retornará ZERO
                var UsuarioLogadoPossuiAcessoEntidadeCompartilhada = this.EntidadeDomainService.Count(new SMCSeqSpecification<Entidade>(solServico.SeqEntidadeCompartilhada ?? 0)) > 0;
                obj.SolicitacaoComEtapaAtualCompartilhadaEUsuarioNaoAssociado = solServico.ProcessoEtapaAtual.EtapaCompartilhada && !UsuarioLogadoPossuiAcessoEntidadeCompartilhada;

                obj.SolicitacaoPossuiUsuarioResponsavel = solServico.SolicitacaoComAtendimentoIniciado && !solServico.UsuarioLogadoEResponsavelAtualPelaSolicitacao;
                obj.UsuarioNaoPossuiAcessoARealizarAtendimento = !SMCSecurityHelper.Authorize(solServico.TokenAcessoAtendimento);
                obj.UsuarioLogadoEResponsavelAtualPelaSolicitacao = solServico.UsuarioLogadoEResponsavelAtualPelaSolicitacao;

                // Recupera a etapa atual do sgf
                var etapaAtualSGF = dadosEtapas[solServico.SeqTemplateProcessoSGF].FirstOrDefault(e => e.Seq == solServico.ProcessoEtapaAtual.SeqEtapaSgf);
                var situacaoAtualSGF = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == solServico.SeqSituacaoEtapaAtualSGF);

                if (situacaoAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                    obj.SolicitantePossuiBloqueiosVigentes = false;
                else
                    obj.SolicitantePossuiBloqueiosVigentes = PessoaAtuacaoBloqueioDomainService.PessoaAtuacaoPossuiBloqueios(solServico.SeqPessoaAtuacao, solServico.SeqConfiguracaoEtapaAtual, true);

                //Etapa disponível para realizar atendimento
                obj.EtapaNaoDisponivelParaAplicacao = etapaAtualSGF.SeqAplicacaoSAS != seqAplicacaoSAS;

                // Dados da etapa
                obj.EtapaAtual = $"{solServico.ProcessoEtapaAtual.Ordem}ª Etapa";
                obj.EtapaAtualCompleta = solServico.ProcessoEtapaAtual.DescricaoEtapa;
                obj.SeqProcessoEtapa = solServico.ProcessoEtapaAtual.Seq;
                obj.SeqConfiguracaoEtapa = solServico.SeqConfiguracaoEtapaAtual;
                obj.SituacaoAtual = $"{SMCEnumHelper.GetDescription(situacaoAtualSGF.CategoriaSituacao)} - {situacaoAtualSGF.Descricao}";
                obj.SituacaoClassificacaoFinalizadaSemSucesso = situacaoAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso;
                obj.SituacaoClassificacaoFinalizadaComSucesso = situacaoAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso;

                if (situacaoAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                    obj.PossuiBloqueio = PossuiBloqueio.Nao;
                else
                    obj.PossuiBloqueio = PessoaAtuacaoBloqueioDomainService.PessoaAtuacaoPossuiBloqueios(solServico.SeqPessoaAtuacao, solServico.SeqConfiguracaoEtapaAtual, true) ? PossuiBloqueio.Sim : PossuiBloqueio.Nao;

                obj.GrupoEscalonamentoAtual = solServico.GrupoEscalonamentoAtual;

                obj.SituacaoAtualSolicitacaoEncerradaOuConcluida = situacaoAtualSGF.CategoriaSituacao == CategoriaSituacao.Concluido || situacaoAtualSGF.CategoriaSituacao == CategoriaSituacao.Encerrado;

                obj.SituacaoAtualSolicitacaoEFimProcesso = situacaoAtualSGF.SituacaoFinalProcesso;

                obj.DescricaoLookupSolicitacaoReduzida = $"{obj.NumeroProtocolo} - {obj.Solicitante}";
                obj.DescricaoLookupSolicitacao = $"{obj.NumeroProtocolo} - {obj.Solicitante} - Grupo Atual: {obj.GrupoEscalonamentoAtual}";

                listaRet.Add(obj);
            }

            return new SMCPagerData<SolicitacaoServicoListarVO>(listaRet, total);
        }

        public DadosSolicitacaoPadraoVO BuscarDadosSolicitacaoPadrao(long seqSolicitacaoServico)
        {
            var dados = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                SeqServico = x.ConfiguracaoProcesso.Processo.SeqServico,
                ExigeJustificativa = x.ConfiguracaoProcesso.Processo.Servico.ExigeJustificativaSolicitacao,
                SeqJustificativa = x.SeqJustificativaSolicitacaoServico,
                ObservacoesJustificativa = x.JustificativaComplementar,
                SeqConfiguracaoEtapaAtual = x.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                TokenTipoServico = x.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token
            });

            if (dados.TokenServico == TOKEN_SERVICO.SOLICITACAO_REABERTURA)
            {
                // Cria a solicitação de reabertura
                SolicitacaoReaberturaMatriculaDomainService.CriarSolicitacaoReaberturaMatriculaPorSolicitacaoServico(seqSolicitacaoServico);
            }
            else if (dados.TokenServico == TOKEN_SERVICO.SOLICITACAO_INTERCAMBIO)
            {
                // Cria a solicitação de intercâmbio
                SolicitacaoIntercambioDomainService.CriarSolicitacaoIntercambioPorSolicitacaoServico(seqSolicitacaoServico);
            }
            else if (dados.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO)
            {
                // Cria a solicitação de intercâmbio
                SolicitacaoDocumentoConclusaoDomainService.CriarSolicitacaoDocumentoConclusaoPorSolicitacaoServico(seqSolicitacaoServico, dados.SeqServico);
            }

            return new DadosSolicitacaoPadraoVO
            {
                SeqServico = dados.SeqServico,
                ExigeJustificativa = dados.ExigeJustificativa,
                SeqJustificativa = dados.SeqJustificativa,
                ObservacoesJustificativa = dados.ObservacoesJustificativa,
                SeqConfiguracaoEtapaAtual = dados.SeqConfiguracaoEtapaAtual,
            };
        }

        public DadosFinaisSolicitacaoPadraoVO BuscarDadosFinaisSolicitacaoPadrao(long seqSolicitacaoServico, long? seqConfiguracaoEtapa)
        {
            if (!seqConfiguracaoEtapa.HasValue)
                seqConfiguracaoEtapa = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.Etapas.FirstOrDefault().SeqConfiguracaoEtapa);

            var dados = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                DadoFormulario = x.Formularios.Where(f => f.ConfiguracaoEtapaPagina.SeqConfiguracaoEtapa == seqConfiguracaoEtapa).Select(a => new
                {
                    DadosCampos = a.DadosCampos,
                    Seq = a.Seq,
                    SeqConfiguracaoEtapaPagina = a.SeqConfiguracaoEtapaPagina,
                    SeqFormulario = a.SeqFormulario,
                    SeqSolicitacaoServico = a.SeqSolicitacaoServico,
                    SeqVisao = a.SeqVisao,
                }).ToList(),
                NomesFormularios = x.Formularios.Where(f => f.ConfiguracaoEtapaPagina.SeqConfiguracaoEtapa == seqConfiguracaoEtapa).Select(a => new
                {
                    SeqFormulario = a.SeqFormulario,
                    NomePaginaFormulario = a.ConfiguracaoEtapaPagina.TituloPagina
                }),
                ExigeFormulario = x.Formularios.Any(f => f.ConfiguracaoEtapaPagina.SeqConfiguracaoEtapa == seqConfiguracaoEtapa),
                ExigeJustificativa = x.SeqJustificativaSolicitacaoServico.HasValue,
                ObservacoesJustificativa = x.JustificativaComplementar,
                SeqJustificativa = x.SeqJustificativaSolicitacaoServico ?? 0,
                DescricaoJustificativa = x.JustificativaSolicitacaoServico.Descricao,
                DescricaoOriginal = x.DescricaoOriginal,
                DescricaoAtualizada = x.DescricaoAtualizada,
                SeqSituacaoEtapaAtualSGF = x.SituacaoAtual.SeqSituacaoEtapaSgf,
                ObservacaoSituacaoAtual = x.SituacaoAtual.Observacao,
                SeqTemplateProcessoSGF = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf
            });

            // Recupera a etapa atual do sgf
            var etapaSGF = SGFHelper.BuscarEtapasSGFCache(dados.SeqTemplateProcessoSGF);
            var situacaoAtualSGF = etapaSGF.SelectMany(e => e.Situacoes).FirstOrDefault(e => e.Seq == dados.SeqSituacaoEtapaAtualSGF);

            return new DadosFinaisSolicitacaoPadraoVO
            {
                DadoFormulario = dados.DadoFormulario.Select(a => new SolicitacaoDadoFormulario
                {
                    DadosCampos = a.DadosCampos,
                    Seq = a.Seq,
                    SeqConfiguracaoEtapaPagina = a.SeqConfiguracaoEtapaPagina,
                    SeqFormulario = a.SeqFormulario,
                    SeqSolicitacaoServico = a.SeqSolicitacaoServico,
                    SeqVisao = a.SeqVisao,
                }).ToList(),
                ExigeFormulario = dados.ExigeFormulario,
                ExigeJustificativa = dados.ExigeJustificativa,
                ObservacoesJustificativa = dados.ObservacoesJustificativa,
                SeqJustificativa = dados.SeqJustificativa,
                NomesFormularios = dados.NomesFormularios.Select(x => new KeyValuePair<long, string>(x.SeqFormulario, x.NomePaginaFormulario)).ToList(),
                DescricaoJustificativa = dados.DescricaoJustificativa,
                DescricaoOriginal = dados.DescricaoOriginal,
                DescricaoAtualizada = dados.DescricaoAtualizada,
                SituacaoAtualSolicitacao = string.Format("{0} - {1}", situacaoAtualSGF.CategoriaSituacao.SMCGetDescription(), situacaoAtualSGF.Descricao),
                ObservacaoSituacaoAtual = dados.ObservacaoSituacaoAtual
            };
        }

        public void SalvarConfirmacaoSolicitacaoPadrao(long seqSolicitacaoServico, long seqPessoaAtuacao, long seqConfiguracaoEtapa)
        {
            // Busca os dados da solicitação
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                SeqServico = x.ConfiguracaoProcesso.Processo.SeqServico,
                NumeroProtocolo = x.NumeroProtocolo,
                NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                DescricaoCursoOfertaLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                DescricaoPessoaAtuacao = x.PessoaAtuacao.Descricao,
                TipoEmissaoTaxa = x.TipoEmissaoTaxa
            });

            // Verifica se tem bloqueio de final de etapa
            var bloqueios = PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(seqPessoaAtuacao, seqConfiguracaoEtapa, true);
            if (bloqueios != null && bloqueios.Any())
                throw new SolicitacaoServicoComBloqueioFimEtapaException("confirmar a solicitação");

            using (var tran = SMCUnitOfWork.Begin())
            {
                // Atualiza a data da solicitação e de prevista de conclusao
                AtualizarDatasSolicitacao(seqSolicitacaoServico);

                // Atualiza a descrição original da solicitação de acordo com o serviço
                AtualizarDescricao(seqSolicitacaoServico, true, false);

                // Monta os dados para merge de envio de notificações
                Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dadosSolicitacao.NomeSocialSolicitante) ? dadosSolicitacao.NomeSolicitante : string.Format("{0} ({1})", dadosSolicitacao.NomeSocialSolicitante, dadosSolicitacao.NomeSolicitante));
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dadosSolicitacao.DescricaoProcesso);
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NUM_PROTOCOLO, dadosSolicitacao.NumeroProtocolo);
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_OFERTA_CURSO_LOCALIDADE, dadosSolicitacao.DescricaoCursoOfertaLocalidade);

                // Envia a notificação de CRIAÇÃO DA SOLICITAÇÃO NO PORTAL
                var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.CRIACAO_SOLICITACAO_PORTAL_ALUNO,
                    DadosMerge = dadosMerge,
                    EnvioSolicitante = false,
                    ConfiguracaoPrimeiraEtapa = false
                };
                SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);

                // Envia uma notificação configurada para uma solicitação de serviço de token FINALIZACAO_SOLICITACAO_ORIENTADOR
                var parametrosOrientador = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.FINALIZACAO_SOLICITACAO_ORIENTADOR,
                    DadosMerge = dadosMerge,
                    EnvioSolicitante = false,
                    ConfiguracaoPrimeiraEtapa = false
                };
                SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotivicacaoSolicitacaoServicoOrientador(parametrosOrientador);

                // Envia uma notificação configurada para uma solicitação de serviço de token FINALIZACAO_SOLICITACAO_SECRETARIA
                var parametrosSecretaria = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.FINALIZACAO_SOLICITACAO_SECRETARIA,
                    DadosMerge = dadosMerge,
                    EnvioSolicitante = false,
                    ConfiguracaoPrimeiraEtapa = false
                };
                SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametrosSecretaria);

                var specTaxaSolicitacao = new SolicitacaoServicoBoletoTaxaFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
                var taxasSolicitacaoServico = this.SolicitacaoServicoBoletoTaxaDomainService.SearchBySpecification(specTaxaSolicitacao).ToList();
                var specTituloSolicitacao = new SolicitacaoServicoBoletoTituloFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
                var titulosSolicitacaoServico = this.SolicitacaoServicoBoletoTituloDomainService.SearchBySpecification(specTituloSolicitacao).ToList();

                //Se houver taxa associada a respectiva solicitação de serviço, consistir a regra:
                //RN_SRC_114 - Solicitação Padrão - Consistências ao confirmar solicitação com taxa de serviço.
                if (taxasSolicitacaoServico.Any())
                {
                    //Se o [tipo de emissão]* selecionado for igual a "Emissão Boleto":
                    //Acionar a rotina do sistema financeiro para geração do título CREI e o respectivo bloqueio
                    //financeiro, conforme RN_SRC_115 -Solicitação Padrão - Geração título financeiro

                    //Só faz a geração do título se não existir títulos para a taxa, pois o usuário pode deixar o processo
                    //sem confirmar, ir na central de solicitações, e fazer a emissão do boleto, que gera o titulo e bloqueios
                    if (dadosSolicitacao.TipoEmissaoTaxa.HasValue && dadosSolicitacao.TipoEmissaoTaxa.Value == TipoEmissaoTaxa.EmissaoBoleto && !titulosSolicitacaoServico.Any())
                    {
                        var modeloGeracaoTitulo = MontarDadosGeracaoTitulo(seqSolicitacaoServico);

                        foreach (var taxa in taxasSolicitacaoServico)
                        {
                            modeloGeracaoTitulo.SeqTaxa = taxa.SeqTaxaGra;

                            DadosGeracaoTituloBloqueiosVO dadosGerais = new DadosGeracaoTituloBloqueiosVO()
                            {
                                SeqSolicitacaoServico = seqSolicitacaoServico,
                                SeqSolicitacaoServicoBoleto = taxa.SeqSolicitacaoServicoBoleto,
                                SeqPessoaAtuacao = seqPessoaAtuacao,
                                NumeroProtocolo = dadosSolicitacao.NumeroProtocolo,
                                DescricaoProcesso = dadosSolicitacao.DescricaoProcesso,
                                DescricaoPessoaAtuacao = dadosSolicitacao.DescricaoPessoaAtuacao
                            };

                            int seqTitulo = GerarTituloBloqueios(modeloGeracaoTitulo, dadosGerais);
                        }
                    }
                }

                // Finaliza a etapa
                ProcedimentosFinalizarEtapa(seqSolicitacaoServico, seqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);

                // Executa procedimentos adicionais após a conclusão da etapa
                ProcedimentosAdicionaisAposConclusaoSolicitacaoPadrao(seqSolicitacaoServico);

                tran.Commit();
            }
        }

        public TituloCreiParametroData MontarDadosGeracaoTitulo(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                x.ConfiguracaoProcesso.Processo.SeqServico,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                x.PessoaAtuacao.Pessoa.Cpf,
                x.PessoaAtuacao.Pessoa.NumeroPassaporte,
                x.PessoaAtuacao.DadosPessoais.NumeroIdentidade,
                EnderecoAcademicoFinanceiro = x.PessoaAtuacao.Enderecos.FirstOrDefault(a => a.EnderecoCorrespondencia == EnderecoCorrespondencia.AcademicaFinanceira).PessoaEndereco.Endereco,
                EnderecoFinanceiro = x.PessoaAtuacao.Enderecos.FirstOrDefault(a => a.EnderecoCorrespondencia == EnderecoCorrespondencia.Financeira).PessoaEndereco.Endereco
            });

            var parametroEmissaoTaxaTipoBoleto = this.ServicoParametroEmissaoTaxaDomainService.SearchBySpecification(new ServicoParametroEmissaoTaxaFilterSpecification()
            {
                SeqServico = dadosSolicitacao.SeqServico,
                TipoEmissaoTaxa = TipoEmissaoTaxa.EmissaoBoleto
            }).FirstOrDefault();

            double numeroDiasPrazoVencimento = Convert.ToDouble(parametroEmissaoTaxaTipoBoleto?.NumeroDiasPrazoVencimentoTitulo.GetValueOrDefault());
            var dataCalculada = DateTime.Now.AddDays(numeroDiasPrazoVencimento);

            var modeloSolicitacaoCobrancaTaxa = PrepararModeloSolicitacaoCobrancaTaxa(seqSolicitacaoServico);

            //Se a pessoa-atuação possui CPF, enviar o código 1 (CPF), senão enviar 2 (Identidade)
            int tipoDocumento = !string.IsNullOrEmpty(dadosSolicitacao.Cpf) ? 1 : 2;

            //Se a pessoa-atuação possui CPF, enviar o CPF. Senão, se possuir RG, enviar o RG.Senão, enviar o Passaporte.
            string numeroDocumento = "";

            if (!string.IsNullOrEmpty(dadosSolicitacao.Cpf))
            {
                numeroDocumento = dadosSolicitacao.Cpf;
            }
            else if (!string.IsNullOrEmpty(dadosSolicitacao.NumeroIdentidade))
            {
                numeroDocumento = dadosSolicitacao.NumeroIdentidade;
            }
            else
            {
                numeroDocumento = dadosSolicitacao.NumeroPassaporte;
            }

            string logradouroEndereco, numeroEndereco, complementoEndereco, bairroEndereco, cidadeEndereco, cepEndereco;
            logradouroEndereco = numeroEndereco = complementoEndereco = bairroEndereco = cidadeEndereco = cepEndereco = "";

            if (dadosSolicitacao.EnderecoAcademicoFinanceiro != null)
            {
                logradouroEndereco = dadosSolicitacao.EnderecoAcademicoFinanceiro.Logradouro;
                numeroEndereco = dadosSolicitacao.EnderecoAcademicoFinanceiro.Numero;
                complementoEndereco = dadosSolicitacao.EnderecoAcademicoFinanceiro.Complemento;
                bairroEndereco = dadosSolicitacao.EnderecoAcademicoFinanceiro.Bairro;
                cidadeEndereco = dadosSolicitacao.EnderecoAcademicoFinanceiro.NomeCidade;
                cepEndereco = dadosSolicitacao.EnderecoAcademicoFinanceiro.Cep;
            }
            else if (dadosSolicitacao.EnderecoFinanceiro != null)
            {
                logradouroEndereco = dadosSolicitacao.EnderecoFinanceiro.Logradouro;
                numeroEndereco = dadosSolicitacao.EnderecoFinanceiro.Numero;
                complementoEndereco = dadosSolicitacao.EnderecoFinanceiro.Complemento;
                bairroEndereco = dadosSolicitacao.EnderecoFinanceiro.Bairro;
                cidadeEndereco = dadosSolicitacao.EnderecoFinanceiro.NomeCidade;
                cepEndereco = dadosSolicitacao.EnderecoFinanceiro.Cep;
            }

            var modeloTituloCrei = new TituloCreiParametroData()
            {
                TipoGeracao = 'A', //Avulso
                SeqOrigem = 1, //PUC Minas
                SeqOrigemCobranca = 1, //PUC Minas
                CodigoBanco = parametroEmissaoTaxaTipoBoleto?.CodigoBancoEmissaoTitulo,
                CodigoAgencia = parametroEmissaoTaxaTipoBoleto?.CodigoAgenciaEmissaoTitulo,
                CodigoConta = parametroEmissaoTaxaTipoBoleto?.CodigoContaEmissaoTitulo,
                CodigoCarteira = parametroEmissaoTaxaTipoBoleto?.CodigoCarteiraEmissaoTitulo,
                TipoCorrecaoTitulo = 3, //Não corrigido
                DataVencimento = dataCalculada,
                DataLimitePagamento = dataCalculada,
                DataValidadeTitulo = dataCalculada,
                SeqMensagemSistema = parametroEmissaoTaxaTipoBoleto?.SeqMensagemTitulo,
                ValorCobradoTaxa = modeloSolicitacaoCobrancaTaxa.ValorTotalTaxas,
                CodigoNucleo = modeloSolicitacaoCobrancaTaxa.CodigoNucleo,
                TipoDocumentoIdentidade = tipoDocumento,
                NumeroDocumento = numeroDocumento,
                LogradouroEndereco = logradouroEndereco,
                NumeroEndereco = numeroEndereco,
                ComplementoEndereco = complementoEndereco,
                BairroEndereco = bairroEndereco,
                CidadeEndereco = cidadeEndereco,
                CepEndereco = cepEndereco,
                NomePagador = dadosSolicitacao.NomeSolicitante,
                NomeUsuarioOperacao = SMCContext.User.SMCGetNome()
            };

            return modeloTituloCrei;
        }

        public int GerarTituloBloqueios(TituloCreiParametroData modeloTitulo, DadosGeracaoTituloBloqueiosVO modeloGeral)
        {
            if (modeloTitulo.ValorCobradoTaxa <= 0)
                throw new SolicitacaoServicoTituloSemValorException();

            //1. Gerar o título CREI no sistema financeiro (st_gera_titulo_crei), informando os seguintes parâmetros:
            TituloCreiRetornoData retornoTituloCrei = this.IntegracaoFinanceiroService.GerarTituloCrei(modeloTitulo);

            //2.O título gerado deverá ser associado no histórico de título da solicitação de serviço x boleto,
            //preenchendo os seguintes campos:
            SolicitacaoServicoBoletoTitulo dominioSolicitacaoServicoBoletoTitulo = new SolicitacaoServicoBoletoTitulo()
            {
                SeqSolicitacaoServicoBoleto = modeloGeral.SeqSolicitacaoServicoBoleto,
                SeqTituloGra = retornoTituloCrei.SeqTitulo,
                ValorTitulo = retornoTituloCrei.ValorTitulo.GetValueOrDefault(),
                DataGeracaoTitulo = DateTime.Now,
                DataVencimento = retornoTituloCrei.DataVencimento.GetValueOrDefault(),
                DataPagamento = null,
                DataCancelamento = null
            };

            this.SolicitacaoServicoBoletoTituloDomainService.SaveEntity(dominioSolicitacaoServicoBoletoTitulo);

            //3. Criar para a respectiva pessoa-atuação o seguinte bloqueio referente ao título CREI gerado pelo
            //sistema financeiro:
            var seqMotivoBloqueio = MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.PAGAMENTO_TAXA_SERVICO_ACADEMICO_EM_ABERTO);

            PessoaAtuacaoBloqueio dominioPessoaAtuacaoBloqueio = new PessoaAtuacaoBloqueio()
            {
                SeqPessoaAtuacao = modeloGeral.SeqPessoaAtuacao,
                SeqSolicitacaoServico = modeloGeral.SeqSolicitacaoServico,
                SeqMotivoBloqueio = seqMotivoBloqueio,
                Descricao = $"Pagamento de taxa de serviço acadêmico da solicitação: {modeloGeral.NumeroProtocolo} - {modeloGeral.DescricaoProcesso}",
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                DescricaoReferenciaAtuacao = modeloGeral.DescricaoPessoaAtuacao,
                CadastroIntegracao = true,
                DataBloqueio = DateTime.Now,
                Itens = new List<PessoaAtuacaoBloqueioItem>()
                {
                    new PessoaAtuacaoBloqueioItem()
                    {
                        Descricao = "Título CREI",
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                        CodigoIntegracaoSistemaOrigem = retornoTituloCrei.SeqTitulo.ToString()
                    }
                }.ToList()
            };

            this.PessoaAtuacaoBloqueioDomainService.SaveEntity(dominioPessoaAtuacaoBloqueio);

            return retornoTituloCrei.SeqTitulo;
        }

        public void ProcedimentosAdicionaisAposConclusaoSolicitacaoPadrao(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                TokenTipoServico = x.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token
            });

            if (dadosSolicitacao.TokenServico == TOKEN_SERVICO.DISPENSA_INDIVIDUAL)
            {
                this.SolicitacaoDispensaDomainService.EfetuarDispensaAutomatica(seqSolicitacaoServico);
            }
            else if (dadosSolicitacao.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO)
            {
                this.SolicitacaoDocumentoConclusaoDomainService.CriarBloqueioSolicitacaoDiplomaDanificado(seqSolicitacaoServico);
            }
        }

        /// <summary>
        /// Atualiza a data da solicitação e de previsão de atendimento de uma solicitação de serviço
        /// RN_SRC_017
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação a ser atualizada</param>
        public void AtualizarDatasSolicitacao(long seqSolicitacaoServico)
        {
            /*  Se o Tipo de Prazo da Etapa for igual a Dias úteis, então:
                Data Prevista de Solução = Data da solicitação + [Total do prazo em dias], portanto deverá ser considerando somente [Dias úteis]

                Senão se, o Tipo de Prazo da Etapa for igual a Dias corridos
                Data Prevista de Solução = Data da solicitação + [Total do prazo em dias]

                Senão se, o Tipo de Prazo da Etapa for igual a Escalonamento, então:
                Data Prevista de Solução = Data do escalonamento que se aplica ao solicitante da última etapa do processo

                Senão se, o Tipo de Prazo da Etapa for igual a Período de vigência, então:
                Data Prevista de Solução = Data Fim da última etapa do processo.

                [Total do prazo em dias] Somatório do prazo em dia de todas as etapas posteriores.
                [Dias úteis] Avaliar os feriados de acordo o calendário parametrizado para a Instituição e Nível de Ensino que se aplica a solicitação.  [PENDENTE] Avaliação de prioridade de  escopo para entrega de Março.
            */
            // Busca os dados da solicitação
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.ConfiguracaoProcesso.Processo.Etapas.Select(e => new
            {
                TipoPrazo = e.TipoPrazoEtapa,
                NumDias = e.NumeroDiasPrazoEtapa,
                SeqProcessoEtapa = e.Seq,
                e.Ordem,
                x.SeqGrupoEscalonamento,
                DataFimEscalonamento = (DateTime?)x.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.SeqProcessoEtapa == e.Seq).Escalonamento.DataFim,
                DataFimEtapa = (DateTime?)e.DataFim
            }));

            DateTime dataSolicitacao = DateTime.Now;
            DateTime? dataPrevista = null;

            if (dadosSolicitacao.Any(d => d.TipoPrazo == TipoPrazoEtapa.Escalonamento))
            {
                // pega a ultima etapa
                var ultimaEtapa = dadosSolicitacao.OrderBy(e => e.Ordem).LastOrDefault();
                dataPrevista = ultimaEtapa.DataFimEscalonamento;
            }
            else if (dadosSolicitacao.Any(d => d.TipoPrazo == TipoPrazoEtapa.PeriodoVigencia))
            {
                // pega a ultima etapa
                var ultimaEtapa = dadosSolicitacao.OrderBy(e => e.Ordem).LastOrDefault();
                dataPrevista = ultimaEtapa.DataFimEtapa;
            }
            else if (dadosSolicitacao.Any(d => d.TipoPrazo == TipoPrazoEtapa.DiasUteis || d.TipoPrazo == TipoPrazoEtapa.DiasCorridos))
            {
                dataPrevista = dataSolicitacao;

                foreach (var item in dadosSolicitacao.Where(d => d.TipoPrazo == TipoPrazoEtapa.DiasUteis || d.TipoPrazo == TipoPrazoEtapa.DiasCorridos))
                {
                    if (!item.NumDias.HasValue)
                        throw new ProcessoEtapaParametrosPrazoInvalidoException();

                    switch (item.TipoPrazo)
                    {
                        case TipoPrazoEtapa.DiasUteis:
                            dataPrevista = SMCDateTimeHelper.AddBusinessDays(dataPrevista.Value, item.NumDias.Value, null);
                            break;

                        case TipoPrazoEtapa.DiasCorridos:
                            dataPrevista = dataPrevista.Value.AddDays(item.NumDias.Value);
                            break;
                    }
                }
            }

            // Atualiza as datas
            this.UpdateFields<SolicitacaoServico>(new SolicitacaoServico
            {
                Seq = seqSolicitacaoServico,
                DataSolicitacao = dataSolicitacao,
                DataPrevistaSolucao = dataPrevista
            }, x => x.DataSolicitacao, x => x.DataPrevistaSolucao);
        }

        /// <summary>
        /// Atualiza a descrição de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Solicitação de serviço a ser atualizada</param>
        /// <param name="indOriginal">Flag para atualizar a descrição original ou atualizada</param>
        public string AtualizarDescricao(long seqSolicitacaoServico, bool indOriginal, bool desativarFiltroDados)
        {
            string ret = string.Empty;

            // Busca os dados da solicitação
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqConfiguracaoEtapa = x.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                NumeroProtocolo = x.NumeroProtocolo
            });

            // De acordo com o token, realiza a atualização da descrição original
            switch (dadosSolicitacao.TokenServico)
            {
                case TOKEN_SERVICO.ATIVIDADE_COMPLEMENTAR:
                    ret = this.SolicitacaoAtividadeComplementarDomainService.AtualizarDescricao(seqSolicitacaoServico, indOriginal);
                    break;

                case TOKEN_SERVICO.DISPENSA_INDIVIDUAL:
                    ret = this.SolicitacaoDispensaDomainService.AtualizarDescricao(seqSolicitacaoServico, indOriginal);
                    break;

                case TOKEN_SERVICO.PRORROGACAO_PRAZO_CONCLUSAO:
                    var spec = new AlunoHistoricoPrevisaoConclusaoFilterSpecification()
                    {
                        SeqPessoaAtuacao = dadosSolicitacao.SeqPessoaAtuacao,
                        AlunoHistoricoAtual = true
                    };
                    var previsao = AlunoHistoricoPrevisaoConclusaoDomainService.SearchBySpecification(spec).OrderByDescending(x => x.DataInclusao).FirstOrDefault();
                    ret = string.Format("Data prevista para conclusão de curso a ser prorrogada: {0}.", previsao.DataPrevisaoConclusao.ToShortDateString());
                    if (indOriginal)
                        this.UpdateFields<SolicitacaoServico>(new SolicitacaoServico { Seq = seqSolicitacaoServico, DescricaoOriginal = ret }, x => x.DescricaoOriginal);
                    else
                        this.UpdateFields<SolicitacaoServico>(new SolicitacaoServico { Seq = seqSolicitacaoServico, DescricaoAtualizada = ret }, x => x.DescricaoAtualizada);
                    break;

                case TOKEN_SERVICO.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA:
                case TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA:
                case TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO:
                case TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU:
                case TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU:
                case TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA:
                case TOKEN_SERVICO.MATRICULA_REABERTURA:
                    ret = SolicitacaoMatriculaItemDomainService.GerarDescricaoItensSolicitacao(seqSolicitacaoServico, dadosSolicitacao.SeqConfiguracaoEtapa, false, desativarFiltroDados);
                    if (indOriginal)
                        this.UpdateFields<SolicitacaoServico>(new SolicitacaoServico { Seq = seqSolicitacaoServico, DescricaoOriginal = ret }, x => x.DescricaoOriginal);
                    else
                        this.UpdateFields<SolicitacaoServico>(new SolicitacaoServico { Seq = seqSolicitacaoServico, DescricaoAtualizada = ret }, x => x.DescricaoAtualizada);
                    break;

                case TOKEN_SERVICO.SOLICITACAO_INTERCAMBIO:
                    ret = SolicitacaoIntercambioDomainService.AtualizarDescricao(seqSolicitacaoServico, indOriginal);
                    break;

                case TOKEN_SERVICO.EMISSAO_DOCUMENTO_ACADEMICO:
                    ret = AtualizarDescricaoSolicitacaoDocumentoAcademico(seqSolicitacaoServico, indOriginal);
                    break;

                case TOKEN_SERVICO.DEPOSITO_DISSERTACAO_TESE:
                    ret = AtualizarDescricaoSolicitacaoDeposito(seqSolicitacaoServico, indOriginal);
                    break;
                case TOKEN_SERVICO.ENTREGA_DOCUMENTACAO:
                case TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA:
                    {
                        var descricaoAtualizada = "Documentos entregues: ";

                        var solicitacaoDocRequeridoSpec = new SolicitacaoDocumentoRequeridoFilterSpecification
                        {
                            SeqSolicitacaoServico = seqSolicitacaoServico,
                            SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoValidacao
                        };
                        var solicitacoes = SolicitacaoDocumentoRequeridoDomainService.SearchBySpecification(solicitacaoDocRequeridoSpec);
                        if (solicitacoes != null && solicitacoes.Count() > 0)
                            foreach (var item in solicitacoes)
                            {
                                var documentoRequerido = DocumentoRequeridoDomainService.SearchByKey(new SMCSeqSpecification<DocumentoRequerido>(item.SeqDocumentoRequerido));
                                if (documentoRequerido != null)
                                {
                                    var descricaoTipoDocumento = TipoDocumentoService.BuscarDescricaoTipoDocumento(documentoRequerido.SeqTipoDocumento);
                                    descricaoAtualizada += "<br/>" + "- " + descricaoTipoDocumento + ": " + item.SituacaoEntregaDocumento.SMCGetDescription();

                                }
                            }

                        this.UpdateFields<SolicitacaoServico>(new SolicitacaoServico { Seq = seqSolicitacaoServico, DescricaoOriginal = descricaoAtualizada }, x => x.DescricaoOriginal);
                        break;
                    }

                case TOKEN_SERVICO.DEPOSITO_PROJETO_QUALIFICACAO:
                    ret = AtualizarDescricaoSolicitacaoDepositoProjetoQualificacao(seqSolicitacaoServico, indOriginal);
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Atualiza a descrição de solicitaçõe que são do documentos academicos (TOKEN_SERVICO.EMISSAO_DOCUMENTO_ACADEMICO)
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="indOriginal">Indicador se é para atualizar a descrição original ou atualizada</param>
        /// <returns>Descrição da solicitação</returns>
        private string AtualizarDescricaoSolicitacaoDocumentoAcademico(long seqSolicitacaoServico, bool indOriginal)
        {
            string ret = MessagesResource.MSG_Documentos_Solicitados;

            var specFormularioCampo = new SolicitacaoDadoFormularioCampoFilterSpecification()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                TokenElemento = TOKEN_ELEMENTO_FORMULARIO.TIPO_DOCUMENTO_DESCRICAO
            };

            var camposFormulario = this.SolicitacaoDadoFormularioCampoDomainService.SearchBySpecification(specFormularioCampo).ToList();

            foreach (var campo in camposFormulario)
            {
                if (campo.Valor.Contains("Outras declarações"))
                {
                    var valores = campo.Valor.Split('|');
                    var valor = valores[1] != null ? valores[1] : campo.Valor;
                    var valorObservacao = this.SolicitacaoDadoFormularioCampoDomainService.SearchBySpecification(new SolicitacaoDadoFormularioCampoFilterSpecification { SeqSolicitacaoServico = seqSolicitacaoServico, TokenElemento = "OBSERVACAO", IdCorrelacao = campo.IdCorrelacao }).First().Valor;
                    if (!string.IsNullOrEmpty(valorObservacao))
                        ret += $"<br /> - {valor}" + " (" + valorObservacao + ")";
                    else
                        ret += $"<br /> - {valor}";

                }
                else
                {
                    if (campo.Valor.Contains("|"))
                    {
                        var valores = campo.Valor.Split('|');
                        var valor = valores[1] != null ? valores[1] : campo.Valor;

                        ret += $"<br /> - {valor}";
                    }
                    else
                        ret += $"<br /> - {campo.Valor}";


                }
            }

            this.UpdateFields<SolicitacaoServico>(new SolicitacaoServico { Seq = seqSolicitacaoServico, DescricaoOriginal = ret }, x => x.DescricaoOriginal);

            return ret;
        }

        /// <summary>
        /// RN_SRC_122 - Atualizar a descrição de solicitação de depósito (TOKEN_SERVICO.DEPOSITO_DISSERTACAO_TESE)
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="indOriginal">Indicador se á para atualizar a descrição original ou atualizada</param>
        /// <returns>Descrição da solicitação</returns>
        private string AtualizarDescricaoSolicitacaoDeposito(long seqSolicitacaoServico, bool indOriginal)
        {
            string ret = string.Empty;

            // Se for a descrição original, formata o formulário preenchido na descrição
            if (indOriginal)
            {
                // Busca os dados do formulário preenchido para a solicitação
                var specFormularioCampo = new SolicitacaoDadoFormularioCampoFilterSpecification()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico
                };
                var camposFormulario = this.SolicitacaoDadoFormularioCampoDomainService.SearchBySpecification(specFormularioCampo).ToList();

                Func<SMCSize, SMCSize, SMCSize, SMCSize, string, object, string> GerarItem = (sizeMD, sizeXS, sizeSM, sizeLG, label, valor) => { return $"<div class=\"{SMCSizeHelper.GetSizeClasses(sizeMD, sizeXS, sizeSM, sizeLG)}\"><div class=\"smc-display\"><label>{label ?? string.Empty}</label><p>{valor ?? string.Empty}</p></div></div>"; };
                Func<string, string, string> GerarFieldset = (label, conteudo) => { return $"<fieldset><legend>{label ?? string.Empty}</legend>{conteudo ?? string.Empty}</fieldset>"; };

                // Monta a descrição da solicitação com os campos do formulário
                // Título
                var titulo = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.TITULO_TRABALHO);
                ret += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Título", titulo != null ? titulo.Valor : "-");

                // Orientador
                var orientador = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.ORIENTADOR_TRABALHO);
                ret += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Orientador", orientador != null ? orientador.Valor : "-");

                // Informações adicionais
                var info = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.INFORMACAO_ADICIONAL_DEPOSITO);
                ret += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Informações adicionais", info != null && !string.IsNullOrEmpty(info.Valor) ? info.Valor : "-");

                string conteudoDefesa = string.Empty;
                // Data defesa
                var dataDefesa = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.DATA_DEFESA_TRABALHO);
                conteudoDefesa += GerarItem(SMCSize.Grid5_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid5_24, "Data", dataDefesa != null && !string.IsNullOrEmpty(dataDefesa.Valor) ? dataDefesa.Valor : "-");

                // Horario defesa
                var horarioDefesa = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.HORA_DEFESA_TRABALHO);
                conteudoDefesa += GerarItem(SMCSize.Grid3_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid3_24, "Horário", horarioDefesa != null && !string.IsNullOrEmpty(horarioDefesa.Valor) ? horarioDefesa.Valor : "-");

                // Local/Plataforma defesa
                var localDefesa = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.LOCAL_DEFESA_TRABALHO);
                conteudoDefesa += GerarItem(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24, "Local/Plataforma", localDefesa != null && !string.IsNullOrEmpty(localDefesa.Valor) ? localDefesa.Valor : "-");

                string conteudoBanca = string.Empty;
                // Banca examinadora
                var membros = camposFormulario.Where(c => c.IdCorrelacao.HasValue).GroupBy(c => c.IdCorrelacao).ToList();
                foreach (var membro in membros)
                {
                    if (!string.IsNullOrEmpty(conteudoBanca))
                        conteudoBanca += "<hr>";

                    // Membro
                    var nome = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24, "Membro", nome != null & !string.IsNullOrEmpty(nome.Valor) ? nome.Valor : "-");

                    // Instituição
                    var instituicao = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.INSTITUICAO_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24, "Instituição", instituicao != null && !string.IsNullOrEmpty(instituicao.Valor) ? instituicao.Valor : "-");

                    // Atuação
                    var atuacao = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.ATUACAO_MEMBRO_BANCA_TRABALHO);
                    string valorAtuacao = "-";
                    if (atuacao != null && !string.IsNullOrEmpty(atuacao.Valor))
                    {
                        valorAtuacao = atuacao.Valor;
                        if (atuacao.Valor.Contains("|"))
                        {
                            var valores = atuacao.Valor.Split('|');
                            valorAtuacao = valores[1] != null ? valores[1] : atuacao.Valor;
                        }
                    }
                    conteudoBanca += GerarItem(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid4_24, "Atuação", valorAtuacao);

                    // Titulação
                    var titulacao = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.TITULACAO_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24, "Titulação", titulacao != null && !string.IsNullOrEmpty(titulacao.Valor) ? titulacao.Valor : "-");

                    // E-mail
                    var email = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.EMAIL_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24, "E-mail", email != null && !string.IsNullOrEmpty(email.Valor) ? email.Valor : "-");

                    // DDD
                    var ddd = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.DDD_TELEFONE_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid3_24, SMCSize.Grid3_24, SMCSize.Grid3_24, SMCSize.Grid3_24, "DDD", ddd != null && !string.IsNullOrEmpty(ddd.Valor) ? ddd.Valor : "-");

                    // Telefone
                    var telefone = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.TELEFONE_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid5_24, SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid5_24, "Telefone", telefone != null && !string.IsNullOrEmpty(telefone.Valor) ? telefone.Valor : "-");
                }

                conteudoDefesa += GerarFieldset("Banca Examinadora", conteudoBanca);
                ret += GerarFieldset("Informações da Defesa", conteudoDefesa);

                // Tem potencial para patente?
                var potencialPatente = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.POTENCIAL_PATENTE);
                if (potencialPatente.Valor.Contains("|"))
                {
                    var valores = potencialPatente.Valor.Split('|');
                    potencialPatente.Valor = valores[1] != null ? valores[1] : potencialPatente.Valor;
                }
                ret += GerarItem(SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24, "O trabalho pode gerar uma patente?", potencialPatente != null && !string.IsNullOrEmpty(potencialPatente.Valor) ? potencialPatente.Valor : "-");

                // Tem potencial para registro de software?
                var potencialRegistro = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.POTENCIAL_REGISTRO_SOFTWARE);
                if (potencialRegistro.Valor.Contains("|"))
                {
                    var valores = potencialRegistro.Valor.Split('|');
                    potencialRegistro.Valor = valores[1] != null ? valores[1] : potencialRegistro.Valor;
                }
                ret += GerarItem(SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24, "O trabalho pode gerar um registro de software?", potencialRegistro != null && !string.IsNullOrEmpty(potencialRegistro.Valor) ? potencialRegistro.Valor : "-");

                // Tem potencial para negócio?
                var potencialNegocio = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.POTENCIAL_NEGOCIO);
                if (potencialNegocio.Valor.Contains("|"))
                {
                    var valores = potencialNegocio.Valor.Split('|');
                    potencialNegocio.Valor = valores[1] != null ? valores[1] : potencialNegocio.Valor;
                }
                ret += GerarItem(SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24, "O trabalho pode gerar um negócio?", potencialNegocio != null && !string.IsNullOrEmpty(potencialNegocio.Valor) ? potencialNegocio.Valor : "-");

                this.UpdateFields<SolicitacaoServico>(new SolicitacaoServico { Seq = seqSolicitacaoServico, DescricaoOriginal = ret }, x => x.DescricaoOriginal);
            }

            return ret;
        }

        private string AtualizarDescricaoSolicitacaoDepositoProjetoQualificacao(long seqSolicitacaoServico, bool indOriginal)
        {
            string ret = string.Empty;

            // Se for a descrição original, formata o formulário preenchido na descrição
            if (indOriginal)
            {
                // Busca os dados do formulário preenchido para a solicitação
                var specFormularioCampo = new SolicitacaoDadoFormularioCampoFilterSpecification()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico
                };
                var camposFormulario = this.SolicitacaoDadoFormularioCampoDomainService.SearchBySpecification(specFormularioCampo).ToList();

                Func<SMCSize, SMCSize, SMCSize, SMCSize, string, object, string> GerarItem = (sizeMD, sizeXS, sizeSM, sizeLG, label, valor) => { return $"<div class=\"{SMCSizeHelper.GetSizeClasses(sizeMD, sizeXS, sizeSM, sizeLG)}\"><div class=\"smc-display\"><label>{label ?? string.Empty}</label><p>{valor ?? string.Empty}</p></div></div>"; };
                Func<string, string, string> GerarFieldset = (label, conteudo) => { return $"<fieldset><legend>{label ?? string.Empty}</legend>{conteudo ?? string.Empty}</fieldset>"; };

                // Monta a descrição da solicitação com os campos do formulário
                // Título
                var titulo = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.TITULO_TRABALHO);
                ret += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Título", titulo != null ? titulo.Valor : "-");

                // Orientador
                var orientador = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.ORIENTADOR_TRABALHO);
                ret += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Orientador", orientador != null ? orientador.Valor : "-");

                // Informações adicionais
                var info = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.INFORMACAO_ADICIONAL_DEPOSITO);
                ret += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Informações adicionais", info != null && !string.IsNullOrEmpty(info.Valor) ? info.Valor : "-");

                string conteudoDefesa = string.Empty;
                // Data defesa
                var dataDefesa = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.DATA_DEFESA_TRABALHO);
                conteudoDefesa += GerarItem(SMCSize.Grid5_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid5_24, "Data", dataDefesa != null && !string.IsNullOrEmpty(dataDefesa.Valor) ? dataDefesa.Valor : "-");

                // Horario defesa
                var horarioDefesa = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.HORA_DEFESA_TRABALHO);
                conteudoDefesa += GerarItem(SMCSize.Grid3_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid3_24, "Horário", horarioDefesa != null && !string.IsNullOrEmpty(horarioDefesa.Valor) ? horarioDefesa.Valor : "-");

                // Local/Plataforma defesa
                var localDefesa = camposFormulario.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.LOCAL_DEFESA_TRABALHO);
                conteudoDefesa += GerarItem(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24, "Local/Plataforma", localDefesa != null && !string.IsNullOrEmpty(localDefesa.Valor) ? localDefesa.Valor : "-");

                string conteudoBanca = string.Empty;
                // Banca examinadora
                var membros = camposFormulario.Where(c => c.IdCorrelacao.HasValue).GroupBy(c => c.IdCorrelacao).ToList();
                foreach (var membro in membros)
                {
                    if (!string.IsNullOrEmpty(conteudoBanca))
                        conteudoBanca += "<hr>";

                    // Membro
                    var nome = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24, "Membro", nome != null & !string.IsNullOrEmpty(nome.Valor) ? nome.Valor : "-");

                    // Instituição
                    var instituicao = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.INSTITUICAO_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24, "Instituição", instituicao != null && !string.IsNullOrEmpty(instituicao.Valor) ? instituicao.Valor : "-");

                    // Atuação
                    var atuacao = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.ATUACAO_MEMBRO_BANCA_TRABALHO);
                    string valorAtuacao = "-";
                    if (atuacao != null && !string.IsNullOrEmpty(atuacao.Valor))
                    {
                        valorAtuacao = atuacao.Valor;
                        if (atuacao.Valor.Contains("|"))
                        {
                            var valores = atuacao.Valor.Split('|');
                            valorAtuacao = valores[1] != null ? valores[1] : atuacao.Valor;
                        }
                    }
                    conteudoBanca += GerarItem(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid4_24, "Atuação", valorAtuacao);

                    // Titulação
                    var titulacao = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.TITULACAO_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24, "Titulação", titulacao != null && !string.IsNullOrEmpty(titulacao.Valor) ? titulacao.Valor : "-");

                    // E-mail
                    var email = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.EMAIL_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24, "E-mail", email != null && !string.IsNullOrEmpty(email.Valor) ? email.Valor : "-");

                    // DDD
                    var ddd = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.DDD_TELEFONE_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid3_24, SMCSize.Grid3_24, SMCSize.Grid3_24, SMCSize.Grid3_24, "DDD", ddd != null && !string.IsNullOrEmpty(ddd.Valor) ? ddd.Valor : "-");

                    // Telefone
                    var telefone = membro.FirstOrDefault(c => c.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.TELEFONE_MEMBRO_BANCA_TRABALHO);
                    conteudoBanca += GerarItem(SMCSize.Grid5_24, SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid5_24, "Telefone", telefone != null && !string.IsNullOrEmpty(telefone.Valor) ? telefone.Valor : "-");
                }

                conteudoDefesa += GerarFieldset("Banca Examinadora", conteudoBanca);
                ret += GerarFieldset("Informações da Defesa", conteudoDefesa);

                this.UpdateFields<SolicitacaoServico>(new SolicitacaoServico { Seq = seqSolicitacaoServico, DescricaoOriginal = ret }, x => x.DescricaoOriginal);
            }

            return ret;
        }

        public void ProcedimentosFinalizarEtapa(long seqSolicitacaoServico, long seqConfiguracaoEtapa, ClassificacaoSituacaoFinal classificacaoSituacaoFinal, string observacao)
        {
            // Busca os dados da solicitação para fazer a atualização do histórico dos itens e mudança da etapa.
            var dadosSolicitacaoServico = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                SeqTemplateProcessoSGF = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqEtapaSGFAtual = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.Seq == seqConfiguracaoEtapa).ProcessoEtapa.SeqEtapaSgf,
                ConfiguracoesEtapaSGF = x.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(c => new
                {
                    SeqConfiguracaoEtapa = c.Seq,
                    SeqEtapaSGF = c.ProcessoEtapa.SeqEtapaSgf
                }),
                SeqSolicitacaoServicoEtapa = (long)x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == seqConfiguracaoEtapa).Seq,

                // Considera os itens que não são cancelados, ou que são cancelados pelo solicitante e estejam no plano de estudo
                ItensSolicitacao = (x as SolicitacaoMatricula).Itens.Select(y => new
                {
                    Seq = y.Seq,
                    PertencePlanoEstudo = y.PertencePlanoEstudo,
                    DadosHistoricoAtual = y.HistoricosSituacao.OrderByDescending(s => s.Seq).Select(h => new
                    {
                        ClassificacaoSituacaoFinal = h.SituacaoItemMatricula.ClassificacaoSituacaoFinal,
                        MotivoSituacaoMatricula = h.MotivoSituacaoMatricula,
                    }).FirstOrDefault(),
                }).Where(i => i.DadosHistoricoAtual.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.Cancelado ||
                             (
                                  i.DadosHistoricoAtual.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                  i.PertencePlanoEstudo == true &&
                                  (
                                    i.DadosHistoricoAtual.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PeloSolicitante
                                    || i.DadosHistoricoAtual.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PorTrocaDeGrupo
                                   )
                              )),

                SeqProcesso = x.ConfiguracaoProcesso.SeqProcesso,
                EtapasSituacoesItemMatricula = x.ConfiguracaoProcesso.Processo.Etapas.Select(e => new
                {
                    SeqEtapaSgf = e.SeqEtapaSgf,
                    SituacoesItemMatricula = e.SituacoesItemMatricula
                })
            });

            // Busca as etapas do SGF para saber qual a próxima etapa
            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(dadosSolicitacaoServico.SeqTemplateProcessoSGF);

            // Recupera as etapas do sgf
            var etapaAtual = etapasSGF.Where(e => e.Seq == dadosSolicitacaoServico.SeqEtapaSGFAtual).OrderBy(e => e.Ordem).FirstOrDefault();
            var proximaEtapa = etapasSGF.Where(e => e.Seq != dadosSolicitacaoServico.SeqEtapaSGFAtual && e.Ordem > etapaAtual.Ordem).OrderBy(e => e.Ordem).FirstOrDefault();

            //Iniciando a transacao
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                var seqSituacaoFinal = etapaAtual.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal.GetValueOrDefault() == classificacaoSituacaoFinal).Seq;

                // Atualiza o histórico de situação com o sequencial da situação etapa configurado para ser a situação final recebido no parametro
                SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(dadosSolicitacaoServico.SeqSolicitacaoServicoEtapa, seqSituacaoFinal, observacao);

                // Insere a nova etapa caso exista
                if (proximaEtapa != null && classificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                {
                    if (dadosSolicitacaoServico.ItensSolicitacao != null && dadosSolicitacaoServico.ItensSolicitacao.Any())
                    {
                        // Busca a proxima etapa do processo
                        var processoEtapaProximaEtapa = dadosSolicitacaoServico.EtapasSituacoesItemMatricula.FirstOrDefault(e => e.SeqEtapaSgf == proximaEtapa.Seq);

                        // Atualiza o histórico dos itens com a situacao inicial da a proxima etapa
                        var situacoesItem = processoEtapaProximaEtapa.SituacoesItemMatricula.Where(i => i.SituacaoInicial);
                        var situacaoCancelado = processoEtapaProximaEtapa.SituacoesItemMatricula.Where(i => i.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado);
                        long seqProximaSituacaoItem;

                        dadosSolicitacaoServico.ItensSolicitacao.ToList().ForEach(i =>
                        {
                            MotivoSituacaoMatricula? motivo = null;
                            if (i.DadosHistoricoAtual.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado)
                                seqProximaSituacaoItem = situacoesItem.FirstOrDefault(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado).Seq;
                            else if (i.DadosHistoricoAtual.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado
                            && i.DadosHistoricoAtual.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PorTrocaDeGrupo)
                            {
                                seqProximaSituacaoItem = situacaoCancelado.FirstOrDefault().Seq;
                                motivo = MotivoSituacaoMatricula.PorTrocaDeGrupo;
                            }
                            else
                                seqProximaSituacaoItem = situacoesItem.FirstOrDefault(w => w.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.NaoAlterado).Seq;

                            SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(i.Seq, seqProximaSituacaoItem, motivo);
                        });
                    }

                    var seqConfiguracaoEtapaProxima = dadosSolicitacaoServico.ConfiguracoesEtapaSGF.FirstOrDefault(c => c.SeqEtapaSGF == proximaEtapa.Seq).SeqConfiguracaoEtapa;

                    // Verifica se já existe a solicitação serviço etapa da próxima etapa ou cria a mesma
                    var etapaNova = SolicitacaoServicoEtapaDomainService.SearchByKey(new SolicitacaoServicoEtapaFilterSpecification { SeqSolicitacaoServico = seqSolicitacaoServico, SeqConfiguracaoEtapa = seqConfiguracaoEtapaProxima }, IncludesSolicitacaoServicoEtapa.HistoricosSituacao);
                    if (etapaNova == null)
                    {
                        etapaNova = new SolicitacaoServicoEtapa
                        {
                            SeqConfiguracaoEtapa = dadosSolicitacaoServico.ConfiguracoesEtapaSGF.FirstOrDefault(c => c.SeqEtapaSGF == proximaEtapa.Seq).SeqConfiguracaoEtapa,
                            SeqSolicitacaoServico = seqSolicitacaoServico,
                        };
                    }
                    SolicitacaoServicoEtapaDomainService.SaveEntity(etapaNova);

                    // Adiciona o histórico
                    SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(etapaNova.Seq, proximaEtapa.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa).Seq, observacao);
                }

                // Commit
                transacao.Commit();
            }
        }

        public DadosConfirmacaoSolicitacaoPadraoVO BuscarDadosConfirmacaoSolicitacaoPadrao(long seqSolicitacaoServico)
        {
            var descricao = AtualizarDescricao(seqSolicitacaoServico, true, false);
            return new DadosConfirmacaoSolicitacaoPadraoVO
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                DescricaoOriginal = descricao
            };
        }

        public void SalvarDadosFormularioSolicitacaoPadrao(DadosFormularioSolicitacaoVO dados)
        {
            var dadosParsed = dados.Transform<SolicitacaoDadoFormulario>();
            if (dadosParsed != null)
            {
                SolicitacaoDadoFormularioDomainService.SaveEntity(dadosParsed);
            }
        }

        public void AtualizarUsuarioResponsavelAtendimento(long seqSolicitacaoServico)
        {
            var seqUsuarioLogado = User.SMCGetSequencialUsuario().Value;
            var seqUsuarioAtual = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => (long?)x.UsuariosResponsaveis.OrderByDescending(y => y.Seq).FirstOrDefault().SeqUsuarioResponsavel);

            if (seqUsuarioAtual != seqUsuarioLogado)
            {
                var historico = new SolicitacaoHistoricoUsuarioResponsavel
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    SeqUsuarioResponsavel = seqUsuarioLogado,
                };
                SolicitacaoHistoricoUsuarioResponsavelDomainService.SaveEntity(historico);
            }
        }

        public void RealizarAtendimento(long seqSolicitacaoServico, bool? situacao, string parecer)
        {
            // Busca os dados da solicitação
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);
            var dadosSolicitacao = this.SearchProjectionByKey(specSolicitacao, x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqConfiguracaoEtapaAtual = x.Etapas.OrderByDescending(e => e.Seq).FirstOrDefault().SeqConfiguracaoEtapa,
                SituacaoAtualIngressante = (SituacaoIngressante?)(x.PessoaAtuacao as Ingressante).HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoIngressante,
                TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                SituacoesAluno = x.ConfiguracaoProcesso.Processo.Servico.SituacoesAluno,
                SituacoesIngressante = x.ConfiguracaoProcesso.Processo.Servico.SituacoesIngressante,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                CodigoAlunoMigracao = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                DataSolicitacao = x.DataSolicitacao,
                NumeroProtocolo = x.NumeroProtocolo,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome
            });

            // Se está deferindo a solicitação (RN_SRC_068)
            if (situacao.GetValueOrDefault())
            {
                // Existirem bloqueios vigentes para o solicitante com a situação igual a "Bloqueado" e que estejam parametrizados
                // para a etapa atual e que impede o fim da etapa, então não permite o atendimento
                var bloqueios = PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(dadosSolicitacao.SeqPessoaAtuacao, dadosSolicitacao.SeqConfiguracaoEtapaAtual, true);
                if (bloqueios != null && bloqueios.Any())
                    throw new SolicitacaoServicoComBloqueioFimEtapaException("confirmar o atendimento");

                // A situação atual do solicitante não está parametrizada no serviço, como uma das possíveis situações que permitem
                // o ATENDIMENTO do serviço, então não permite o atendimento
                if (dadosSolicitacao.TipoAtuacao == TipoAtuacao.Aluno)
                {
                    // Busca a situação atual do aluno
                    var situacaoAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(dadosSolicitacao.SeqPessoaAtuacao);

                    // Valida se está parametrizado para a situação do aluno esse atendimento
                    if (!dadosSolicitacao.SituacoesAluno.Any(s => s.SeqSituacaoMatricula == situacaoAluno.SeqSituacao && s.PermissaoServico == PermissaoServico.AtenderSolicitacao))
                        throw new SolicitacaoServicoInativaParaSolicitanteSituacaoException(dadosSolicitacao.TipoAtuacao.SMCGetDescription(), situacaoAluno.Descricao, "deferir o atendimento");
                }
                else
                {
                    // Valida se está parametrizado para a situação do ingressante este atendimento
                    if (dadosSolicitacao.SituacoesIngressante == null || !dadosSolicitacao.SituacoesIngressante.Any(s => s.SituacaoIngressante == dadosSolicitacao.SituacaoAtualIngressante && s.PermissaoServico == PermissaoServico.AtenderSolicitacao))
                        throw new SolicitacaoServicoInativaParaSolicitanteSituacaoException(dadosSolicitacao.TipoAtuacao.SMCGetDescription(), dadosSolicitacao.SituacaoAtualIngressante.SMCGetDescription(), "deferir o atendimento");
                }
            }

            // Inicia a transação
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                // Cria dicionário para tags de envio de notificação
                Dictionary<string, string> dicTagsNot = new Dictionary<string, string>
                {
                    { TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dadosSolicitacao.NomeSocialSolicitante) ? dadosSolicitacao.NomeSolicitante : dadosSolicitacao.NomeSocialSolicitante },
                    { TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dadosSolicitacao.DescricaoProcesso },
                    { TOKEN_TAG_NOTIFICACAO.NUM_PROTOCOLO, dadosSolicitacao.NumeroProtocolo }
                };

                // Se está deferindo
                if (situacao.GetValueOrDefault())
                {
                    /*  EXECUTA O DEFERIMENTO POR TIPO DE SOLICITAÇÃO
                     *
                     *  TRANCAMENTO_MATRICULA, acionar RN_MAT_082
                     *  CANCELAMENTO_MATRICULA, acionar RN_MAT_083
                     *  ATIVIDADE_COMPLEMENTAR, acionar RN_SRC_055
                     *  DISPENSA_INDIVIDUAL, acionar RN_SRC_056
                     *  PRORROGACAO_PRAZO_CONCLUSAO, acionar RN_SRC_046
                     *  SOLICITACAO_REABERTURA, acionar a RN_SRC_075
                     *  SOLICITACAO_INTERCAMBIO, acionar a RN_SRC_069
                     *  DEPOSITO_DISSERTACAO_TESE, acionar a RN_SRC_110
                     *  EMISSAO_DOCUMENTO_ACADEMICO, aciona a RN_SRC_121
                    */
                    switch (dadosSolicitacao.TokenServico)
                    {
                        case TOKEN_SERVICO.ATIVIDADE_COMPLEMENTAR:
                            SolicitacaoAtividadeComplementarDomainService.DeferirSolicitacaoAtividadeComplementar(seqSolicitacaoServico);
                            break;

                        case TOKEN_SERVICO.CANCELAMENTO_MATRICULA:
                            this.CancelarMatriculaPorSolicitacao(seqSolicitacaoServico);
                            break;

                        case TOKEN_SERVICO.TRANCAMENTO_MATRICULA:
                            AlunoDomainService.TrancarMatricula(seqSolicitacaoServico);
                            break;

                        case TOKEN_SERVICO.DISPENSA_INDIVIDUAL:
                            SolicitacaoDispensaDomainService.DeferirSolicitacaoDispensa(seqSolicitacaoServico);
                            break;

                        case TOKEN_SERVICO.PRORROGACAO_PRAZO_CONCLUSAO:
                            // Defere a alteração de previsão de conclusão e ajusta o dicionário para mensagem
                            var dicProrrogacao = AlunoHistoricoPrevisaoConclusaoDomainService.DeferirProrrogacaoPrazoConclusao(seqSolicitacaoServico);
                            dicTagsNot = dicTagsNot.Union(dicProrrogacao).ToDictionary(k => k.Key, v => v.Value);
                            break;

                        case TOKEN_SERVICO.SOLICITACAO_REABERTURA:
                            // Defere a reabertura de matrícula e ajusta o dicionário para mensagem
                            var dicReabertura = SolicitacaoReaberturaMatriculaDomainService.DeferirReaberturaMatricula(seqSolicitacaoServico);
                            dicTagsNot = dicTagsNot.Union(dicReabertura).ToDictionary(k => k.Key, v => v.Value);
                            break;

                        case TOKEN_SERVICO.SOLICITACAO_INTERCAMBIO:
                            SolicitacaoIntercambioDomainService.DeferirSolicitacaoIntercambio(seqSolicitacaoServico);
                            break;

                        case TOKEN_SERVICO.DEPOSITO_DISSERTACAO_TESE:
                            this.DeferirSolicitacaoEntregaDissertacaoTese(seqSolicitacaoServico, dadosSolicitacao.SeqPessoaAtuacao);
                            break;

                        case TOKEN_SERVICO.EMISSAO_DOCUMENTO_ACADEMICO:
                            this.DeferirSolicitacaoEmissaoDocumentoAcademico(seqSolicitacaoServico);
                            break;
                        case TOKEN_SERVICO.DEPOSITO_PROJETO_QUALIFICACAO:
                            this.DeferirDepositoProjetoQualificacao(seqSolicitacaoServico, dadosSolicitacao.SeqPessoaAtuacao);
                            break;
                    }
                }

                // Alterar a situação da solicitação de serviço para a situação final da etapa com
                // classificação "Finalizada com sucesso" (DEFERIDO) ou "Finalizada sem sucesso" (INDEFERIDO)
                var classificacao = situacao.GetValueOrDefault() ? ClassificacaoSituacaoFinal.FinalizadoComSucesso : ClassificacaoSituacaoFinal.FinalizadoSemSucesso;
                this.ProcedimentosFinalizarEtapa(seqSolicitacaoServico, dadosSolicitacao.SeqConfiguracaoEtapaAtual, classificacao, parecer);

                // Criar mensagem de encerramento da solicitação na linha do tempo da pessoa atuação
                MensagemDomainService.EnviarMensagemLinhaDoTempoEncerramentoSolicitacao(seqSolicitacaoServico, TOKEN_TIPO_MENSAGEM.ENCERRAMENTO_SOLICITACAO_SERVICO);

                /* ENVIAR NOTIFICAÇÃO DE ATENDIMENTO DA SOLICITAÇÃO
				 *
				 * PRORROGACAO_PRAZO_CONCLUSAO, enviar notificação para o solicitante conforme RN_SRC_073
				 * REABERTURA_MATRICULA, enviar notificação para o solicitante conforme RN_SRC_064
				 * EMISSAO_DOCUMENTO_ACADEMICO, enviar notificação para o solicitante conforme RN_SRC_121
				 * DEMAIS TOKENS, enviar notificação para o solicitante conforme RN_SRC_071
				 */
                string tokenNotificacao = TOKEN_TIPO_NOTIFICACAO.ENCERRAMENTO_SOLICITACAO;
                switch (dadosSolicitacao.TokenServico)
                {
                    case TOKEN_SERVICO.PRORROGACAO_PRAZO_CONCLUSAO:
                        tokenNotificacao = situacao.GetValueOrDefault() ? TOKEN_TIPO_NOTIFICACAO.PRORROGACAO_DEFERIDA : TOKEN_TIPO_NOTIFICACAO.PRORROGACAO_INDEFERIDA;
                        break;

                    case TOKEN_SERVICO.SOLICITACAO_REABERTURA:
                        tokenNotificacao = situacao.GetValueOrDefault() ? TOKEN_TIPO_NOTIFICACAO.REABERTURA_MATRICULA_DEFERIDA : TOKEN_TIPO_NOTIFICACAO.REABERTURA_MATRICULA_INDEFERIDA;
                        break;

                    case TOKEN_SERVICO.EMISSAO_DOCUMENTO_ACADEMICO:
                        tokenNotificacao = situacao.GetValueOrDefault() ? TOKEN_TIPO_NOTIFICACAO.COMUNICADO_SOLICITACAO_SERVICO_DOCUMENTO_ACADEMICO : TOKEN_TIPO_NOTIFICACAO.ENCERRAMENTO_SOLICITACAO;
                        break;
                }
                // Envia a notificação
                var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    TokenNotificacao = tokenNotificacao,
                    DadosMerge = dicTagsNot,
                    EnvioSolicitante = true,
                    ConfiguracaoPrimeiraEtapa = false
                };
                SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);

                // Preenche a data de atendimento da solicitação
                var solicitacao = new SolicitacaoServico()
                {
                    Seq = seqSolicitacaoServico,
                    DataSolucao = DateTime.Now
                };
                this.UpdateFields<SolicitacaoServico>(solicitacao, s => s.DataSolucao);

                // Fim da transação
                transacao.Commit();
            }
        }

        /// <summary>
        /// Defere uma solicitação de emissão de documentos acadêmicos (RN_SRC_121 - Parte 1)
        /// 1.Preencher o campo "Situação do documento" da respectiva solicitação de serviço com a opção "Entregue"
        /// </summary>
        /// <param name="seqSolicitacaoServico"></param>
        private void DeferirSolicitacaoEmissaoDocumentoAcademico(long seqSolicitacaoServico)
        {
            var solicitacao = new SolicitacaoServico()
            {
                Seq = seqSolicitacaoServico,
                SituacaoDocumentacao = SituacaoDocumentacao.Entregue
            };
            this.UpdateFields(solicitacao, s => s.SituacaoDocumentacao);
        }

        /// <summary>
        /// Defere uma solicitação de entrega de dissertação e tese (RN_SRC_110)
        /// </summary>
        /// <param name="seqSolicitacaoServico"></param>
        /// <param name="seqPessoaAtuacao"></param>
        private void DeferirSolicitacaoEntregaDissertacaoTese(long seqSolicitacaoServico, long seqPessoaAtuacao)
        {
            using (var transacao = SMCUnitOfWork.Begin())
            {
                // Recupera os dados da pessoa atuação
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

                /// Ao deferir a solicitação:
                /// Verificar se já existe algum trabalho para o aluno com a data de depósito registrada, cujo tipo de
                /// trabalho parametrizado de acordo com a instituição de ensino logada e o nível de ensino da
                /// pessoa-atuação em questão gera financeiro na entrega trabalho e é obrigatória a entrega do trabalho no BDP.
                /// Caso afirmativo, emitir a mensagem de erro abaixo e abortar a operação:
                /// "O depósito da dissertação/tese para o agendamento da defesa deste(a) aluno(a) já foi registrado no sistema.
                /// Sendo assim esta solicitação só poderá ser indeferida."
                var trabalhosComDataDepositoRegistrada = TrabalhoAcademicoDomainService.SearchBySpecification(new TrabalhoAcademicoFilterSpecification
                {
                    SeqAluno = seqPessoaAtuacao,
                    PossuiDataDeposito = true
                }).ToList();
                var seqsTipoTrabalhoGeraFinanceiroEEntregaBDP = InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionBySpecification(new InstituicaoNivelTipoTrabalhoFilterSpecification
                {
                    SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    GeraFinanceiroEntregaTrabalho = true,
                    PublicacaoBibliotecaObrigatoria = true
                }, x => x.SeqTipoTrabalho).ToList();
                if (trabalhosComDataDepositoRegistrada.Any() && seqsTipoTrabalhoGeraFinanceiroEEntregaBDP.Any())
                {
                    foreach (var trabalho in trabalhosComDataDepositoRegistrada)
                    {
                        if (seqsTipoTrabalhoGeraFinanceiroEEntregaBDP.Contains(trabalho.SeqTipoTrabalho))
                            throw new SolicitacaoDepositoTrabalhoAcademicoJaRegistradaException();
                    }
                }

                // Recupera o seq tipo de trabalho
                var dadosTipoTrabalho = TrabalhoAcademicoDomainService.RecuperarDadosTipoTrabalhoAcademico(seqPessoaAtuacao);

                // Recupera os dados da solicitação
                var dadosSolicitacao = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
                {
                    x.DataSolicitacao,
                    NomeAluno = x.PessoaAtuacao.DadosPessoais.NomeSocial ?? x.PessoaAtuacao.DadosPessoais.Nome
                });

                // Recupera os dados que foram preenchidos no SGF
                var dadosSGF = BuscarDadosFormularioSolicitacaoPadrao(seqSolicitacaoServico, 0);

                // Recupera o título do trabalho que foi preenchido no SGF
                var tituloTrabalho = dadosSGF.DadosCampos.FirstOrDefault(d => d.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.TITULO_TRABALHO)?.Valor;

                // Busca as informações de potencial do trabalho que foi preenchido no SGF
                bool? potencialPatente = null;
                var potencialPatenteStr = dadosSGF.DadosCampos.FirstOrDefault(d => d.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.POTENCIAL_PATENTE)?.Valor;
                if (!string.IsNullOrEmpty(potencialPatenteStr))
                    potencialPatente = potencialPatenteStr.Equals("0|Não") ? false : true;

                bool? potencialRegistroSoftware = null;
                var potencialRegistroSoftwareStr = dadosSGF.DadosCampos.FirstOrDefault(d => d.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.POTENCIAL_REGISTRO_SOFTWARE)?.Valor;
                if (!string.IsNullOrEmpty(potencialRegistroSoftwareStr))
                    potencialRegistroSoftware = potencialRegistroSoftwareStr.Equals("0|Não") ? false : true;

                bool? potencialNegocio = null;
                var potencialNegocioStr = dadosSGF.DadosCampos.FirstOrDefault(d => d.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.POTENCIAL_NEGOCIO)?.Valor;
                if (!string.IsNullOrEmpty(potencialNegocioStr))
                    potencialNegocio = potencialNegocioStr.Equals("0|Não") ? false : true;

                /* Caso não exista, executar os passos descritos abaixo:
                        1. Inserir o trabalho acadêmico (tabela ort.trabalho_academico) com os atributos:
                            - dsc_titulo = Título informado no formulário configurado para a etapa de solicitação do processo em questão
                            - seq_entidade_instituicao = Instituição do aluno
                            - seq_nivel_ensino = Nível de ensino do aluno
                            - seq_tipo_trabalho = Tipo de trabalho parametrizado para instituição e nível de ensino do aluno e que esteja configurado para "Gerar transação financeira na entrega do trabalho".
                            - dat_deposito_secretaria: Data da solicitação
                            - dat_entrega_projeto = Null
                            - cod_origem_migracao = Null
                            - cod_origem_migracao = Null
                            - num_dias_duracao_autorizacao_parcial =  O numero de dias de duração da autorização parcial caso esteja parametrizada para a entidade tipo programa associada ao curso do aluno.
                            - ind_potencial_patente = Valor informado no formulário configurado para a etapa de solicitação do processo em questão
                            - ind_potencial_registro_software = Valor informado no formulário configurado para a etapa de solicitação do processo em questão
                            - ind_potencial_negocio = Valor informado no formulário configurado para a etapa de solicitação do processo em questão

                        2. Inserir o autor do trabalho (tabela ort.trabalho_academico_autoria) com os atributos:
                            · seq_trabalho_academico = Sequencial do trabalho inserido no passo acima
                            · seq_atuacao_aluno = Sequencial de atuação do aluno
                            · nom_autor = Nome do aluno
                            · nom_autor_formatado = RN_ORT_006 - Gerar nome formatado
                            · dsc_email_autor = Null
                */

                // Busca a configuração do programa associado
                var seqPrograma = ProgramaDomainService.BuscarProgramaPorAluno(seqPessoaAtuacao);
                var specPrograma = new ProgramaTipoAutorizacaoBdpFilterSpecification()
                {
                    SeqPrograma = seqPrograma,
                    TipoAutorizacao = TipoAutorizacao.Parcial
                };

                // Pega numeros de dias para autorização se houver
                var numerosDiasAutorizacao = ProgramaTipoAutorizacaoBdpDomainService
                                                .SearchProjectionByKey(specPrograma, p => p.NumeroDiasDuracaoAutorizacao);

                // Cria o trabalho Acadêmico
                var novoTrabalho = new TrabalhoAcademico
                {
                    SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    Titulo = tituloTrabalho,
                    SeqTipoTrabalho = dadosTipoTrabalho.SeqTipoTrabalho,
                    DataDepositoSecretaria = dadosSolicitacao.DataSolicitacao,
                    Autores = new List<TrabalhoAcademicoAutoria>
                    {
                        new TrabalhoAcademicoAutoria
                        {
                            SeqAluno = seqPessoaAtuacao,
                            NomeAutor = dadosSolicitacao.NomeAluno,
                            NomeAutorFormatado = TrabalhoAcademicoDomainService.FormatarNome(dadosSolicitacao.NomeAluno)
                        }
                    },
                    NumeroDiasDuracaoAutorizacaoParcial = numerosDiasAutorizacao,
                    PotencialPatente = potencialPatente,
                    PotencialRegistroSoftware = potencialRegistroSoftware,
                    PotencialNegocio = potencialNegocio
                };
                TrabalhoAcademicoDomainService.SaveEntity(novoTrabalho);

                /* 4. Selecionar o critério de aprovação (seq_criterio_aprovacao):
                    * Associado na matriz curricular do plano de estudo atual do aluno a configuração do componente da divisão selecionada no passo 3.
                   5. Selecionar a nota máxima e a escala de apruação do critério de aprovação retornado passo acima.*/
                var criterio = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao), a =>
                                 a.Historicos.FirstOrDefault(h => h.Atual)
                                  .HistoricosCicloLetivo.FirstOrDefault(h => h.SeqCicloLetivo == dadosOrigem.SeqCicloLetivo)
                                  .PlanosEstudo.FirstOrDefault(p => p.Atual)
                                  .MatrizCurricularOferta.MatrizCurricular.ConfiguracoesComponente.Where(c => c.DivisoesComponente.Any(d => d.SeqDivisaoComponente == dadosTipoTrabalho.SeqDivisaoComponente)).Select(t => new
                                  {
                                      SeqCriterioAprovacao = t.SeqCriterioAprovacao,
                                      //ApurarFrequencia = (bool?)t.CriterioAprovacao.ApuracaoFrequencia,
                                      NotaMaxima = t.CriterioAprovacao.NotaMaxima,
                                      SeqEscalaApuracao = t.CriterioAprovacao.SeqEscalaApuracao,
                                  }).FirstOrDefault());

                /* 6. Inserir a origem da avaliação (tabela apr.origem_avaliacai) com os atributos:
                    · seq_criterio_aprovacao = Sequencial do critério de aprovação selecionado no passo 4
                    · num_nota_maxima = Nota máxima selecionada no passo 5
                    · ind_apurar_frequencia = 0
                    · seq_escala_apuracao = Escala de apuração selecionada no passo 5
                    · idt_dom_tipo_origem_avaliacao = 3 -- "Trabalho Acadêmico"
                    · O restante dos atributos receberão o valor nulo.*/
                var origemAvaliacao = new OrigemAvaliacao
                {
                    SeqCriterioAprovacao = criterio.SeqCriterioAprovacao,
                    NotaMaxima = criterio.NotaMaxima,
                    ApurarFrequencia = false,
                    SeqEscalaApuracao = criterio.SeqEscalaApuracao,
                    TipoOrigemAvaliacao = TipoOrigemAvaliacao.TrabalhoAcademico,
                };
                OrigemAvaliacaoDomainService.SaveEntity(origemAvaliacao);

                /* 7. Inserir o componente curricular do trabalho (tabela ort.trabalho_academico_divisao_componente) com os atributos:
                    · seq_trabalho_academico = Sequencial do trabalho inserido no passo 1
                    · seq_divisao_componente = Sequencial da divisão do componente selecionado no passo 3
                    · seq_origem_avaliacao = Sequencial de origem de avaliação inserida no passo acima
                */
                var componenteTrabalho = new TrabalhoAcademicoDivisaoComponente
                {
                    SeqTrabalhoAcademico = novoTrabalho.Seq,
                    SeqDivisaoComponente = dadosTipoTrabalho.SeqDivisaoComponente.Value,
                    SeqOrigemAvaliacao = origemAvaliacao.Seq
                };
                TrabalhoAcademicoDivisaoComponenteDomainService.SaveEntity(componenteTrabalho);

                /* 8. Executar os passos descritos em RN_ORT_008 - Inclusão de Data De Depósito. */
                TrabalhoAcademicoDomainService.IncluirDataDeposito(seqPessoaAtuacao, dadosSolicitacao.DataSolicitacao, seqSolicitacaoServico);

                transacao.Commit();
            }
        }


        /// <summary>
        /// Defere uma solicitação de entrega de dissertação e tese (RN_SRC_110)
        /// </summary>
        /// <param name="seqSolicitacaoServico"></param>
        /// <param name="seqPessoaAtuacao"></param>
        private void DeferirDepositoProjetoQualificacao(long seqSolicitacaoServico, long seqPessoaAtuacao)
        {
            using (var transacao = SMCUnitOfWork.Begin())
            {
                // Recupera os dados da pessoa atuação
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);


                ///1.Selecionar o tipo de trabalho parametrizado para instituição e nível de ensino do aluno que seja
                ///'projeto de qualificação'.

                var tipoTrabalhoProjetoQualificacao = InstituicaoNivelTipoTrabalhoDomainService.SearchBySpecification(new InstituicaoNivelTipoTrabalhoFilterSpecification
                {
                    SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    TrabalhoQualificacao = true

                }).First();

                ///Regra retirada de acordo com a task 73761
                /// Ao deferir a solicitação:
                /// Verificar se já existe algum trabalho para o aluno cujo tipo de trabalho parametrizado de acordo com a
                ///instituição de ensino logada e o nível de ensino da pessoa - atuação é igual a 'projeto de qualificação'.
                ///Caso afirmativo, emitir a mensagem de erro abaixo e abortar a operação:
                ///"Já existe um trabalho do tipo 'projeto de qualificação' registrado para o aluno. Sendo assim esta
                ///solicitação só poderá ser indeferida."
                //var trabalhoProjetoQualificacao = TrabalhoAcademicoDomainService.SearchBySpecification(new TrabalhoAcademicoFilterSpecification
                //{
                //    SeqAluno = seqPessoaAtuacao,
                //    SeqTipoTrabalho = tipoTrabalhoProjetoQualificacao.SeqTipoTrabalho
                //}).ToList();

                //if (trabalhoProjetoQualificacao.Any())
                //    throw new SolicitacaoDepositoProjetoQualificacaoJaRegistradaException();

                // Recupera os dados da solicitação
                var dadosSolicitacao = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
                {
                    x.DataSolicitacao,
                    NomeAluno = x.PessoaAtuacao.DadosPessoais.NomeSocial ?? x.PessoaAtuacao.DadosPessoais.Nome
                });

                var solicitacaoTrabalhoAcademico = this.SolicitacaoTrabalhoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoTrabalhoAcademico>(seqSolicitacaoServico));
                // Recupera os dados que foram preenchidos no SGF
                var dadosSGF = BuscarDadosFormularioSolicitacaoPadrao(seqSolicitacaoServico, 0);

                // Recupera o título do trabalho que foi preenchido no SGF
                var tituloTrabalho = dadosSGF.DadosCampos.FirstOrDefault(d => d.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.TITULO_TRABALHO)?.Valor;
                ///3.Inserir o trabalho acadêmico(tabela ort.trabalho_academico) com os atributos:
                ///-dsc_titulo = Título informado no formulário configurado para a etapa de solicitação do processo em
                ///questão
                ///- seq_entidade_instituicao = Instituição do aluno
                ///- seq_nivel_ensino = Nível de ensino do aluno
                ///- seq_tipo_trabalho = Sequencial do tipo de trabalho selecionado no passo 1
                ///- dat_deposito_secretaria: Data da solicitação
                ///- dat_entrega_projeto = Null
                ///- cod_origem_migracao = Null
                ///- num_dias_duracao_autorizacao_parcial = null

                ///4. Inserir o autor do trabalho (tabela ort.trabalho_academico_autoria) com os atributos:
                ///-seq_trabalho_academico = Sequencial do trabalho inserido no passo acima
                ///-seq_atuacao_aluno = Sequencial de atuação do aluno
                ///- nom_autor = Nome do aluno
                ///- nom_autor_formatado = RN_ORT_006 - Gerar nome formatado
                ///- dsc_email_autor = Null
                var novoTrabalho = new TrabalhoAcademico
                {
                    Titulo = tituloTrabalho,
                    SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    SeqTipoTrabalho = tipoTrabalhoProjetoQualificacao.SeqTipoTrabalho,
                    DataDepositoSecretaria = null,
                    Autores = new List<TrabalhoAcademicoAutoria>
                    {
                        new TrabalhoAcademicoAutoria
                        {
                            SeqAluno = seqPessoaAtuacao,
                            NomeAutor = dadosSolicitacao.NomeAluno,
                            NomeAutorFormatado = TrabalhoAcademicoDomainService.FormatarNome(dadosSolicitacao.NomeAluno)
                        }
                    },
                    SeqSolicitacaoServico = seqSolicitacaoServico
                };
                TrabalhoAcademicoDomainService.SaveEntity(novoTrabalho);


                ///Regra retirada de acordo com a task 73761
                /// 2.Selecionar o componente curricular cuja configuração está na matriz curricular do plano de estudo
                ///atual do aluno e que possua uma divisão cujo tipo da divisão está configurado por “instituição / nível / tipo
                ///do componente” e esteja nesta configuração associado ao tipo de trabalho selecionado no passo 1.
                ///(cur.instituicao_nivel_tipo_divisao_componente).
                ///5.Selecionar a divisão de configuração de componente(seq_divisao_componente):
                ///*Cujo a divisão esteja associada a configuração de componente da matriz curricular do plano de estudo
                ///atual do aluno.
                ///* E também o tipo da divisão está configurado por “instituição / nível / tipo do componente” e esteja nesta
                ///configuração associado ao tipo de trabalho selecionado no passo 1.
                ///(cur.instituicao_nivel_tipo_divisao_componente).
                //var seqDivisaoComponente = DivisaoMatrizCurricularComponenteDomainService.BuscarDivisaoComponenteCurricularSelect(new DivisaoComponenteCurricularFiltroVO
                //{
                //    SeqAluno = seqPessoaAtuacao,
                //    SeqTipoTrabalho = tipoTrabalhoProjetoQualificacao.SeqTipoTrabalho,
                //    SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                //    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                //})?.FirstOrDefault()?.Seq;

                ///6.Selecionar o critério de aprovação(seq_criterio_aprovacao):
                ///*Associado na matriz curricular do plano de estudo atual do aluno a configuração do componente da
                ///divisão selecionada no passo acima.
                var criterio = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao), a =>
                                 a.Historicos.FirstOrDefault(h => h.Atual)
                                  .HistoricosCicloLetivo.FirstOrDefault(h => h.SeqCicloLetivo == dadosOrigem.SeqCicloLetivo)
                                  .PlanosEstudo.FirstOrDefault(p => p.Atual)
                                  .MatrizCurricularOferta.MatrizCurricular.ConfiguracoesComponente.Where(c => c.DivisoesComponente.Any(d => d.SeqDivisaoComponente == solicitacaoTrabalhoAcademico.SeqDivisaoComponente)).Select(t => new
                                  {
                                      SeqCriterioAprovacao = t.SeqCriterioAprovacao,
                                      //ApurarFrequencia = (bool?)t.CriterioAprovacao.ApuracaoFrequencia,
                                      NotaMaxima = t.CriterioAprovacao.NotaMaxima,
                                      SeqEscalaApuracao = t.CriterioAprovacao.SeqEscalaApuracao,
                                  }).FirstOrDefault());

                ///7.Selecionar a nota máxima e a escala de apuração do critério de aprovação retornado passo acima.
                ///8.Inserir a origem da avaliação(tabela apr.origem_avaliacai) com os atributos:
                ///-seq_criterio_aprovacao = Sequencial do critério de aprovação selecionado no passo 6
                ///- num_nota_maxima = Nota máxima selecionada no passo 7
                ///- ind_apurar_frequencia = 0
                ///- seq_escala_apuracao = Escala de apuração selecionada no passo 7
                ///- idt_dom_tipo_origem_avaliacao = 3-- "Trabalho Acadêmico"
                ///O restante dos atributos receberão o valor nulo.
                var origemAvaliacao = new OrigemAvaliacao
                {
                    SeqCriterioAprovacao = criterio.SeqCriterioAprovacao,
                    NotaMaxima = criterio.NotaMaxima,
                    ApurarFrequencia = false,
                    SeqEscalaApuracao = criterio.SeqEscalaApuracao,
                    TipoOrigemAvaliacao = TipoOrigemAvaliacao.TrabalhoAcademico,
                };
                OrigemAvaliacaoDomainService.SaveEntity(origemAvaliacao);

                ///9.Inserir o componente curricular do trabalho(tabela ort.trabalho_academico_divisao_componente)
                ///com os atributos:
                ///-seq_trabalho_academico = Sequencial do trabalho inserido no passo 3
                ///- seq_divisao_componente = Sequencial da divisão do componente selecionado no passo 5
                ///- seq_origem_avaliacao = Sequencial de origem de avaliação inserida no passo acima
                var componenteTrabalho = new TrabalhoAcademicoDivisaoComponente
                {
                    SeqTrabalhoAcademico = novoTrabalho.Seq,
                    SeqOrigemAvaliacao = origemAvaliacao.Seq,
                    SeqDivisaoComponente = solicitacaoTrabalhoAcademico.SeqDivisaoComponente.Value
                };
                TrabalhoAcademicoDivisaoComponenteDomainService.SaveEntity(componenteTrabalho);

                transacao.Commit();
            }
        }

        /// <summary>
        /// Realizar o cancelamento de matrícula do um aluno (RN_SRC_083)
        /// </summary>
        /// <param name="seqSolicitacaoServico"></param>
        public void CancelarMatriculaPorSolicitacao(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação para cancelamento
            var dadosCancelamento = this.SearchProjectionByKey(seqSolicitacaoServico, x => new CancelarMatriculaVO()
            {
                TokenSituacaoCancelamento = TOKENS_SITUACAO_MATRICULA.CANCELADO_SOLICITACAO_ALUNO,
                DataReferencia = x.DataSolicitacao,
                SeqSolicitacaoServico = x.Seq,
                Jubilado = false,
                CancelarBeneficio = true,
                TipoCancelamentoSGP = 0, // Cancelamento normal

                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqAlunoHistorico = x.SeqAlunoHistorico,
                CodigoAlunoSGP = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                NumeroProtocolo = x.NumeroProtocolo,
                SeqCicloLetivoReferencia = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo
            });

            // Ajusta a observação de cancelamento da solicitação
            dadosCancelamento.ObservacaoCancelamentoSolicitacao = string.Format("Solicitação cancelada devido ao deferimento da solicitação de cancelamento de matrícula nº {0}.", dadosCancelamento.NumeroProtocolo);

            // Se a solicitação não possui o seq-aluno-historico informado, erro
            if (!dadosCancelamento.SeqAlunoHistorico.HasValue)
                throw new SolicitacaoServicoSemAlunoHistoricoException();

            // Se o processo não possui ciclo letivo, erro
            if (!dadosCancelamento.SeqCicloLetivoReferencia.HasValue)
                throw new SolicitacaoServicoSemCicloLetivoException();

            // Se o aluno não possui código de migração, erro
            if (!dadosCancelamento.CodigoAlunoSGP.HasValue)
                throw new AlunoMigracaoInvalidoException();

            // Chama rotina de cancelamento de matrícula
            AlunoDomainService.CancelarMatricula(dadosCancelamento);
        }

        public DadosFormularioSolicitacaoVO BuscarDadosFormularioSolicitacaoPadrao(long seqSolicitacaoServico, long seqConfiguracaoEtapaPagina)
        {
            var dadosFormulario = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                SeqSolicitacaoDadoFormulario = (long?)x.Formularios.FirstOrDefault(f => seqConfiguracaoEtapaPagina == 0 || f.SeqConfiguracaoEtapaPagina == seqConfiguracaoEtapaPagina).Seq,
                SeqFormulario = x.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(c => c.ConfiguracoesPagina).FirstOrDefault(c => c.Seq == seqConfiguracaoEtapaPagina).SeqFormulario,
                SeqVisao = x.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(c => c.ConfiguracoesPagina).FirstOrDefault(c => c.Seq == seqConfiguracaoEtapaPagina).SeqVisaoFormulario,
            });

            var ret = new DadosFormularioSolicitacaoVO();
            if (dadosFormulario.SeqSolicitacaoDadoFormulario.HasValue)
                ret.DadosCampos = SolicitacaoDadoFormularioDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoDadoFormulario>(dadosFormulario.SeqSolicitacaoDadoFormulario.Value), x => x.DadosCampos).ToList();
            else
                ret.DadosCampos = new List<SolicitacaoDadoFormularioCampo>();

            ret.SeqConfiguracaoEtapaPagina = seqConfiguracaoEtapaPagina;
            ret.SeqFormulario = dadosFormulario.SeqFormulario.GetValueOrDefault();
            ret.SeqSolicitacaoServico = seqSolicitacaoServico;
            ret.SeqVisao = dadosFormulario.SeqVisao.GetValueOrDefault();
            ret.Seq = dadosFormulario.SeqSolicitacaoDadoFormulario.GetValueOrDefault();

            return ret;
        }

        /// <summary>
        /// Verifica se existe algum serviço em aberto que conflita com o serviço passado como parâmetro
        /// </summary>
        /// <param name="seqServico">Sequencial do serviço a ser validado</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Lista com as restrições encontradas</returns>
        public List<SolicitacaoDispensaRestricaoSolicitacaoSimultaneaVO> BuscarRestricoesSolicitacaoSimultanea(long seqServico, long seqPessoaAtuacao)
        {
            // Validar se pode criar a solicitação baseado na regra de solicitações duplicadas para o mesmo tipo
            var restricoes = RestricaoSolicitacaoSimultaneaDomainService.BuscarRestricoesSolicitacaoSimultanea(new RestricaoSolicitacaoSimultaneaFilterSpecification { SeqServico = seqServico });
            if (restricoes != null && restricoes.Count > 0)
            {
                // Recupera todas as solicitações dos serviços que possuem restrição, que estejam em andamento ou novas.
                var filtroSpec = new SolicitacaoServicoFilterSpecification
                {
                    SeqPessoaAtuacao = seqPessoaAtuacao,
                    SeqsServicos = restricoes.Select(r => r.SeqServicoRestricao).ToList(),
                    CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.EmAndamento, CategoriaSituacao.Novo, CategoriaSituacao.Concluido }
                };

                var solicitacoesAberto = SearchProjectionBySpecification(filtroSpec, x => new SolicitacaoDispensaRestricaoSolicitacaoSimultaneaVO()
                {
                    SeqSolicitacaoServico = x.Seq,
                    SeqServico = x.ConfiguracaoProcesso.Processo.SeqServico,
                    NumeroProtocolo = x.NumeroProtocolo,
                    Ordem = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.Ordem,
                    Processo = x.ConfiguracaoProcesso.Processo.Descricao,
                    Servico = x.ConfiguracaoProcesso.Processo.Servico.Descricao,
                    SeqTemplateProcessoSGF = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                    SeqEtapaAtualSGF = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                    SeqSituacaoEtapaAtualSGF = x.SituacaoAtual.SeqSituacaoEtapaSgf,
                }).ToList();

                foreach (var item in solicitacoesAberto)
                {
                    var etapasSGF = SGFHelper.BuscarEtapasSGFCache(item.SeqTemplateProcessoSGF);
                    var etapaAtualSGF = etapasSGF.FirstOrDefault(e => e.Seq == item.SeqEtapaAtualSGF);
                    var situacaoEtapaAtualSGF = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == item.SeqSituacaoEtapaAtualSGF);

                    item.SituacaoAtual = situacaoEtapaAtualSGF.Descricao;
                }

                return solicitacoesAberto;
            }
            return new List<SolicitacaoDispensaRestricaoSolicitacaoSimultaneaVO>();
        }

        public List<long> CriarSolicitacoesProrrogacaoManual(List<long> codigosMigracao)
        {
            // Recupera o código do processo
            var seqsServico = ServicoDomainService.SearchProjectionByKey(new ServicoFilterSpecification { Token = "PRORROGACAO_PRAZO_CONCLUSAO" }, x => new { SeqProcesso = x.Processos.FirstOrDefault().Seq, SeqServico = x.Seq, SeqConfiguracaoEtapa = x.Processos.FirstOrDefault().Configuracoes.FirstOrDefault().ConfiguracoesEtapa.FirstOrDefault().Seq });

            // Recupera o sequencial da justificativa outros
            var seqJustificativa = JustificativaSolicitacaoServicoDomainService.SearchProjectionByKey(new JustificativaSolicitacaoServicoFilterSpecification { Descricao = "Outros", SeqServico = seqsServico.SeqServico }, x => x.Seq);

            List<long> seqsConflitantes = new List<long>();

            // Percorre cada código de migração
            foreach (var codigoMigracao in codigosMigracao)
            {
                try
                {
                    // Recupera o seqPessoaAtuacao do aluno em questão
                    var seqPessoaAtuacao = AlunoDomainService.SearchProjectionByKey(new AlunoFilterSpecification { CodigoAlunoMigracao = codigoMigracao }, x => x.Seq);

                    // Cria a solicitação de serviço base
                    var dadosSolicitacao = CriarSolicitacaoServico(new CriarSolicitacaoVO { Origem = OrigemSolicitacaoServico.Presencial, SalvarMensagemLinhaTempo = false, SeqPessoaAtuacao = seqPessoaAtuacao, SeqProcesso = seqsServico.SeqProcesso });

                    // Recupera a solicitação de serviço
                    var solicitacao = SearchByKey(new SMCSeqSpecification<SolicitacaoServico>(dadosSolicitacao.SeqSolicitacaoServico));
                    solicitacao.JustificativaComplementar = "Solicitação de prorrogação de prazo de conclusão de curso criada automaticamente, conforme requerido pela secretaria.";
                    solicitacao.SeqJustificativaSolicitacaoServico = seqJustificativa;

                    // Informa os dados de justificativa e observação
                    UpdateFields(solicitacao, x => x.JustificativaComplementar, x => x.SeqJustificativaSolicitacaoServico);

                    // Recupera as configurações da etapa atual
                    var configuracaoEtapaAtual = ConfiguracaoEtapaDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(dadosSolicitacao.SeqConfiguracaoEtapa), IncludesConfiguracaoEtapa.ConfiguracoesPagina);
                    configuracaoEtapaAtual.ConfiguracoesPagina = configuracaoEtapaAtual.ConfiguracoesPagina.OrderBy(p => p.Ordem).ToList();
                    var ultimaPagina = configuracaoEtapaAtual.ConfiguracoesPagina.LastOrDefault();

                    // Adiciona o histórico de navegação
                    var historico = SolicitacaoHistoricoNavegacaoDomainService.BuscarSolicitacaoHistoricoNavegacao(dadosSolicitacao.SeqSolicitacaoServicoEtapa, ultimaPagina.Seq, true);

                    // Atualiza a data da solicitação e de prevista de conclusao
                    AtualizarDatasSolicitacao(dadosSolicitacao.SeqSolicitacaoServico);

                    // Atualiza a descrição original da solicitação de acordo com o serviço
                    AtualizarDescricao(dadosSolicitacao.SeqSolicitacaoServico, true, false);

                    // Finaliza a etapa
                    ProcedimentosFinalizarEtapa(dadosSolicitacao.SeqSolicitacaoServico, seqsServico.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);
                }
                catch (SolicitacaoServicoConflitanteException)
                {
                    seqsConflitantes.Add(codigoMigracao);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return seqsConflitantes;
        }

        public (long SeqSolicitacaoServico, long SeqConfiguracaoEtapa, long SeqSolicitacaoServicoEtapa) CriarSolicitacaoServico(CriarSolicitacaoVO model)
        {
            // Recupera os dados de origem
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(model.SeqPessoaAtuacao);

            // Identificar a configuração de processo que se aplica ao solicitante de acordo com o seu vínculo,
            // nível de ensino e, se houver, seu curso oferta localidade turno.
            // Recupero os dados do processo
            var dadosProcesso = ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(model.SeqProcesso), x => new
            {
                SeqTemplateProcessoSGF = (long?)x.Servico.SeqTemplateProcessoSgf,
                SeqConfiguracaoProcessoCurso = (long?)x.Configuracoes.FirstOrDefault(c => c.Cursos.Any(cu => cu.SeqCursoOfertaLocalidadeTurno == dadosOrigem.SeqCursoOfertaLocalidadeTurno) &&
                                                          c.TiposVinculoAluno.Any(t => t.SeqTipoVinculoAluno == dadosOrigem.SeqTipoVinculoAluno)).Seq,
                SeqConfiguracaoProcessoNivelEnsino = (long?)x.Configuracoes.FirstOrDefault(c => c.NiveisEnsino.Any(n => n.SeqNivelEnsino == dadosOrigem.SeqNivelEnsino) &&
                                                               c.TiposVinculoAluno.Any(t => t.SeqTipoVinculoAluno == dadosOrigem.SeqTipoVinculoAluno)).Seq,
                SeqServico = x.SeqServico,
                DescricaoServico = x.Servico.Descricao,
                DescricaoProcesso = x.Descricao,
                TokenServico = x.Servico.Token,
                TokenTipoServico = x.Servico.TipoServico.Token
            });

            // Recupera a entidade responsável direto no pessoa atuação
            var dadosPessoaAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(model.SeqPessoaAtuacao), x => new
            {
                SeqEntidadeResponsavel = (long?)(x as Aluno).Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo ?? (x as Ingressante).SeqEntidadeResponsavel,
                DataPrevisaoConclusaoAluno = dadosOrigem.TipoAtuacao == TipoAtuacao.Aluno ?
                                           (DateTime?)(x as Aluno).Historicos.FirstOrDefault(h => h.Atual).PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataPrevisaoConclusao :
                                           null,
                SeqAlunoHistoricoAtual = (x as Aluno).Historicos.FirstOrDefault(h => h.Atual).Seq,
            });

            if (dadosProcesso == null)
                throw new ServicoSemConfiguracoesException();

            if (!dadosProcesso.SeqConfiguracaoProcessoCurso.HasValue && !dadosProcesso.SeqConfiguracaoProcessoNivelEnsino.HasValue)
                throw new ServicoSemConfiguracoesException();

            if (!dadosProcesso.SeqTemplateProcessoSGF.HasValue)
                throw new ServicoSemTemplateSGFException();

            // RN_SRC_028 - Solicitação padrão - Iniciar processo (2.2)
            // Se o tipo de serviço for emissão de documento de conclusão
            if (dadosProcesso.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO)
            {
                /*  Se o aluno não possui nenhuma formação específica que o tipo esteja parametrizado permitindo a
                    emissão do [tipo de documento solicitado]* e, a [situação atual] da apuração da formação seja igual a
                    Formado. Abortar a operação e exibir a seguinte mensagem impeditiva: "Inclusão não permitida. Não
                    consta no seu histórico, formação que permita a emissão do documento de conclusão: [Descrição do
                    tipo de documento solicitado]"*/

                // Recupera os dados do TipoDocumentoAcademico de acordo com o serviço
                var dadosTipoDocumentoAcademico = TipoDocumentoAcademicoServicoDomainService.SearchProjectionByKey(new TipoDocumentoAcademicoServicoFilterSpecification
                {
                    SeqServico = dadosProcesso.SeqServico
                }, x => new
                {
                    SeqTipoDocumentoAcademico = x.SeqTipoDocumentoAcademico,
                    DescricaoTipoDocumentoAcademico = x.TipoDocumentoAcademico.Descricao
                });

                // Recupera o SeqInstituicaoNivel do aluno
                var seqInstituicaoNivel = InstituicaoNivelDomainService.SearchProjectionByKey(new InstituicaoNivelFilterSpecification
                {
                    SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    SeqCicloLetivo = dadosOrigem.SeqCicloLetivo
                }, x => x.Seq);

                // Recupera os SeqsTipoFormacaoEspecifica do aluno que estão com apuração formado
                var seqsTipoFormacaoEspecificaFormado = AlunoDomainService.SearchProjectionByKey(model.SeqPessoaAtuacao, x =>
                    x.Historicos.FirstOrDefault(h => h.Atual).Formacoes.Where(f => f.ApuracoesFormacao.OrderByDescending(d => d.DataInicio).FirstOrDefault(a => !a.DataExclusao.HasValue && a.DataInicio <= DateTime.Now).SituacaoAlunoFormacao == SituacaoAlunoFormacao.Formado)
                    .Select(f => f.FormacaoEspecifica.SeqTipoFormacaoEspecifica)).ToList();

                // Verifica se InstituicaoNivelTipoDocumentoAcademico para o seqInstituicaoNivel e seqTipoDocumentoAcademico possui parametrização para algum dos SeqTipoFormacaoEspecificas do aluno
                if (seqsTipoFormacaoEspecificaFormado == null || !seqsTipoFormacaoEspecificaFormado.Any() ||
                    InstituicaoNivelTipoDocumentoAcademicoDomainService.Count(new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification
                    {
                        SeqInstituicaoNivel = seqInstituicaoNivel,
                        SeqTipoDocumentoAcademico = dadosTipoDocumentoAcademico.SeqTipoDocumentoAcademico,
                        SeqsTipoFormacaoEspecifica = seqsTipoFormacaoEspecificaFormado
                    }) == 0)
                {
                    throw new FormacaoEspecificaNaoPermiteEmissaoDocumentoException(dadosTipoDocumentoAcademico.DescricaoTipoDocumentoAcademico);
                }
            }

            // Se o serviço é de prorrogação de prazo...
            if (dadosProcesso.TokenServico == TOKEN_SERVICO.PRORROGACAO_PRAZO_CONCLUSAO)
            {
                // Verifica se já atingiu a quantidade de meses mínima para abertura da solicitação conforme configurado
                // na matriz do aluno
                var quantidadeProrrogacao = MatrizCurricularOfertaDomainService.BuscarMatrizCurricularOferta(dadosOrigem.SeqMatrizCurricularOferta)?.QuantidadeMesesSolicitacaoProrrogacao;
                if (dadosPessoaAtuacao.DataPrevisaoConclusaoAluno.HasValue && quantidadeProrrogacao.HasValue)
                {
                    if (DateTime.Now < dadosPessoaAtuacao.DataPrevisaoConclusaoAluno.Value.AddMonths(-1 * quantidadeProrrogacao.Value))
                        throw new SolicitacaoProrrogacaoPrazoConclusaoForaPrazoException(quantidadeProrrogacao.Value.ToString(), dadosPessoaAtuacao.DataPrevisaoConclusaoAluno.Value.ToShortDateString());
                }

                // Verifica se o aluno já possui data de deposito cadastrada para o trabalho que gera cobrança financeira
                var trabalho = TrabalhoAcademicoDomainService.BuscarDatasDepositoDefesaTrabalho(model.SeqPessoaAtuacao);

                if (trabalho.DataDeposito.HasValue && trabalho.Situacao != SituacaoHistoricoEscolar.Reprovado)
                    throw new CriarSolicitacaoProrrogacaoPrazoConclusaoComDataDepositoException(trabalho.TituloTrabalho, trabalho.DataDeposito.Value);
            }

            // Verifica se não irá dar nenhum conflito para abertura de solicitações simultâneas
            var dadosConflitantes = BuscarRestricoesSolicitacaoSimultanea(dadosProcesso.SeqServico, model.SeqPessoaAtuacao);
            if (dadosConflitantes.Any())
                throw new SolicitacaoServicoConflitanteException(string.Join("", dadosConflitantes.Select(d => string.Format("<li>{0} - {1} - {2}° Etapa - {3}", d.NumeroProtocolo, d.Processo, d.Ordem, d.SituacaoAtual))));

            // Recupera a primeira etapa
            var etapas = SGFHelper.BuscarEtapasSGFCache(dadosProcesso.SeqTemplateProcessoSGF.GetValueOrDefault());
            var primeiraEtapa = etapas.OrderBy(e => e.Ordem).FirstOrDefault();

            // Busca as configurações da primeira etapa
            var seqConfiguracaoProcessoValida = dadosProcesso.SeqConfiguracaoProcessoCurso.HasValue ? dadosProcesso.SeqConfiguracaoProcessoCurso.Value : dadosProcesso.SeqConfiguracaoProcessoNivelEnsino.GetValueOrDefault();
            var dadosConfiguracao = ConfiguracaoProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoProcesso>(seqConfiguracaoProcessoValida), x => new
            {
                SeqConfiguracaoEtapa = x.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.SeqEtapaSgf == primeiraEtapa.Seq).Seq,
                DocumentosRequeridos = x.ConfiguracoesEtapa.SelectMany(s => s.DocumentosRequeridos)
            });

            // Verifica se existe algum bloqueio que impede a criação da solicitação
            var bloqueios = PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(model.SeqPessoaAtuacao, dadosConfiguracao.SeqConfiguracaoEtapa, false);
            if (bloqueios != null && bloqueios.Any())
            {
                // Faz o distinct pois estava exibindo a mesma mensagem de desbloqueio duas vezes, uma vez que o aluno tinha dois bloqueios de mesmo motivo.
                var bloqueiosDistinct = bloqueios.GroupBy(b => b.MotivoBloqueio.Seq).Select(b => b.FirstOrDefault()).ToList().Select(s => s.MotivoBloqueio.OrientacoesDesbloqueio);
                string mensagem = $"</br> {string.Join(",</br> ", bloqueiosDistinct.ToList())} .</br>";
                throw new SolicitacaoServicoComBloqueioException(dadosProcesso.DescricaoServico, mensagem);
            }

            /*RN_SRC_011 Inclusão solicitação serviço
				1. Salvar em Solicitação Serviço:
					Pessoa atuação: solicitante da solicitação.
					Configuração do processo: identificar a configuração de processo que se aplica ao solicitante de acordo com o seu vínculo, nível de ensino e, se houver seu curso oferta localidade turno.
					Número do protocolo: o protocolo deverá ser preenchido no seguinte formato: [Ano] + [.] + [Sequencial de 5 dígitos a partir de 10.000]. Por exemplo: 2018.10.001
					Data da solicitação: data atual (hoje).
					Data prevista de solução: nulo.
					Usuário responsável: nulo
					Data de solução: nulo.
					Descrição da solicitação atual:  nulo.
					Usuário responsável: nulo.
					Justificativa da solicitação: se o serviço relacionado com a solicitação tiver sido parametrizado para exigir justificativa da solicitação, preencher este campo com o valor selecionado.
					Descrição da Justificativa do solicitante: se o serviço relacionado com a solicitação tiver sido parametrizado para exigir justificativa da solicitação, preencher este campo com a descrição informada.
					Situação documentação: preenchido de acordo com o item 3 desse documento.
					Grupo de escalonamento: se o serviço do processo em questão tiver sido parametrizado para exigir grupo de escalonamento, preencher este campo com o grupo de escalonamento do solicitante.
					Histórico do aluno: se o solicitante for um aluno, preencher com o histórico atual do aluno, senão nulo.
					Descrição da solicitação original: [O Caso de uso que aciona a regra de negócio que define o preenchimento]
					Origem da solicitação: [O Caso de uso que aciona a regra de negócio que define o preenchimento]
			*/
            var solicitacao = new SolicitacaoServico
            {
                SeqPessoaAtuacao = model.SeqPessoaAtuacao,
                SeqConfiguracaoProcesso = seqConfiguracaoProcessoValida,
                DataSolicitacao = DateTime.Now,
                SeqAlunoHistorico = dadosPessoaAtuacao.SeqAlunoHistoricoAtual,
                DescricaoOriginal = dadosProcesso.DescricaoProcesso,
                SeqEntidadeResponsavel = dadosPessoaAtuacao.SeqEntidadeResponsavel.GetValueOrDefault(), // CursoOfertaLocalidadeTurnoDomainService.BuscarSeqEntidadeResponsavel(dadosOrigem.SeqCursoOfertaLocalidadeTurno),
                                                                                                        //NumeroProtocolo = Guid.NewGuid().ToString(),

                /*2.Salvar o histórico de situação da solicitação:
				    Criar um registro para a 1º etapa, em Solicitação Serviço Etapa associada a[Configuração de etapa] que se aplica ao solicitante
				    Criar um registro para a 1º etapa  em Solicitação Histórico Situação:
						Data inclusão = data atual
						Situação(SGF) = situação do SGF parametrizada como a inicial da 1º Página do fluxo de páginas da 1º etapa.*/

                Etapas = new List<SolicitacaoServicoEtapa>
                {
                    new SolicitacaoServicoEtapa
                    {
                        SeqConfiguracaoEtapa = dadosConfiguracao.SeqConfiguracaoEtapa,
                        HistoricosSituacao = new List<SolicitacaoHistoricoSituacao>
                        {
                            new SolicitacaoHistoricoSituacao
                            {
                                CategoriaSituacao = CategoriaSituacao.Novo,
                                SeqSituacaoEtapaSgf = primeiraEtapa.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa).Seq,
                            }
                        }
                    }
                },

                /*3. Criar um registro, em Solicitação Documento Requerido, para cada documento parametrizado de acordo com a [Configuração de etapa] que se aplica ao solicitante:
					Data de entrega = nulo
					Situação da entrega = Aguardando (nova) entrega
					Forma de entrega = nulo
					Versão do documento = nulo
					Arquivo = nulo*/
                DocumentosRequeridos = dadosConfiguracao.DocumentosRequeridos?.Select(d => new SolicitacaoDocumentoRequerido
                {
                    SeqDocumentoRequerido = d.Seq,
                    SituacaoEntregaDocumento = DadosMestres.Common.Areas.PES.Enums.SituacaoEntregaDocumento.AguardandoEntrega,
                    EntregueAnteriormente = false
                }).ToList(),
                SituacaoDocumentacao = dadosConfiguracao.DocumentosRequeridos?.Any() ?? false ? SituacaoDocumentacao.AguardandoEntrega : SituacaoDocumentacao.NaoRequerida,
                OrigemSolicitacaoServico = model.Origem
            };

            /*5. Se há parametrização de taxa para o respectivo serviço da solicitação:
               · Criar um registro de boleto para a solicitação e,
               · Associar todas as taxas parametrizadas no respectivo serviço.*/
            var servicoTaxas = this.ServicoTaxaDomainService.SearchBySpecification(new ServicoTaxaFilterSpecification() { SeqServico = dadosProcesso.SeqServico }).ToList();

            if (servicoTaxas.Any())
            {
                solicitacao.Boletos = new List<SolicitacaoServicoBoleto>()
                {
                    new SolicitacaoServicoBoleto()
                    {
                        Taxas = servicoTaxas.Select(a => new SolicitacaoServicoBoletoTaxa()
                        {
                            SeqTaxaGra = a.SeqTaxaGra
                        }).ToList()
                    }
                };
            }

            //Chamada para realizar pareamento de arquivos da solicitação de serviços entre as tabelas SolicitacaoDocumentoRequerido e PessoaAtuacaoDocumento
            if (dadosProcesso.TokenServico == TOKEN_SERVICO.ENTREGA_DOCUMENTACAO || dadosProcesso.TokenServico == TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA)
                this.SalvarDocumentosPessoaAtuacaoSolicitacaoDocumentoRequerido(solicitacao);

            SaveEntity(solicitacao);

            if (dadosProcesso.TokenServico == TOKEN_SERVICO.DEPOSITO_PROJETO_QUALIFICACAO)
                this.SolicitacaoTrabalhoAcademicoDomainService.CriarSolicitacaoTrabalhoAcademicoPorSolicitacaoServico(solicitacao.Seq);

            if (model.SalvarMensagemLinhaTempo)
            {
                // Após criar a solicitação deverá ser criada mensagem na linha do tempo para o solicitante,
                // conforme a regra: RN_SRC_070
                Dictionary<string, string> dicTagsMsg = new Dictionary<string, string>();
                dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.PROTOCOLO_SOLICITACAO, solicitacao.NumeroProtocolo);
                dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.PROCESSO_SOLICITACAO, dadosProcesso.DescricaoProcesso);
                MensagemDomainService.EnviarMensagemPessoaAtuacao(solicitacao.SeqPessoaAtuacao,
                                                                  dadosOrigem.SeqInstituicaoEnsino,
                                                                  dadosOrigem.SeqNivelEnsino,
                                                                  TOKEN_TIPO_MENSAGEM.ABERTURA_SOLICITACAO_SERVICO,
                                                                  CategoriaMensagem.LinhaDoTempo,
                                                                  dicTagsMsg);
            }
            return (solicitacao.Seq, dadosConfiguracao.SeqConfiguracaoEtapa, solicitacao.Etapas.FirstOrDefault().Seq);
        }

        public SMCPagerData<SolicitacaoServicoPessoaAtuacaoListaVO> BuscarSolicitacoesPessoaAtuacao(SolicitacaoServicoFilterSpecification spec)
        {
            // Retorna também as solicitações de todos os ingressantes que estão associados ao histórico do
            // aluno atual do aluno logado.
            var seqsIngressantes = AlunoDomainService.SearchProjectionByKey(spec.SeqPessoaAtuacao.Value, a => a.Historicos.Where(i => i.SeqIngressante.HasValue).Select(i => i.SeqIngressante.Value)).ToList();
            if (seqsIngressantes.SMCAny())
                spec.SeqsIngressantes = seqsIngressantes;

            int total = 0;
            var lista = new List<SolicitacaoServicoPessoaAtuacaoListaVO>();

            var dados = SearchProjectionBySpecification(spec, x => new
            {
                DataSolicitacao = x.DataSolicitacao,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                NumeroProtocolo = x.NumeroProtocolo,
                CategoriaSituacao = x.SituacaoAtual.CategoriaSituacao,
                SeqSituacaoEtapaSgf = (long?)x.SituacaoAtual.SeqSituacaoEtapaSgf,
                SeqTemplateProcessoSGF = (long?)x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqSolicitacaoServico = x.Seq,
                SeqConfiguracaoEtapaAtual = (long?)x.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                ExisteUsuarioResponsavel = x.UsuariosResponsaveis.Any(),
                OrigemSolicitacaoServico = x.OrigemSolicitacaoServico,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                Etapas = x.Etapas.Select(e => new
                {
                    SeqSolicitacaoServicoEtapa = e.Seq,
                    e.SeqConfiguracaoEtapa,
                    //e.SeqSolicitacaoServico,
                    SeqEtapaSgf = (long?)e.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                    SeqSituacaoEtapaSgf = (long?)e.SituacaoAtual.SeqSituacaoEtapaSgf,
                    DataEncerramento = e.ConfiguracaoEtapa.ProcessoEtapa.DataEncerramento,
                    //e.ConfiguracaoEtapa.OrientacaoEtapa,
                    //DataInicio = (DateTime?)(x.GrupoEscalonamento != null ? x.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.SeqProcessoEtapa == e.ConfiguracaoEtapa.SeqProcessoEtapa).Escalonamento.DataInicio : e.ConfiguracaoEtapa.ProcessoEtapa.DataInicio),
                    //DataFim = (DateTime?)(x.GrupoEscalonamento != null ? x.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.SeqProcessoEtapa == e.ConfiguracaoEtapa.SeqProcessoEtapa).Escalonamento.DataFim : e.ConfiguracaoEtapa.ProcessoEtapa.DataFim),
                    HistoricosNavegacao = e.HistoricosNavegacao.ToList()
                })
            }, out total).ToList();

            dados.SMCForEach(d =>
            {
                var etapasSGF = SGFHelper.BuscarEtapas(d.SeqSolicitacaoServico);
                var situacaoAtualSolicitacao = etapasSGF.SelectMany(e => e.Situacoes).FirstOrDefault(s => s.Seq == d.SeqSituacaoEtapaSgf);
                var tokensServicoPreMatricula = new string[] { TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU,
                                                                 TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA,
                                                                 TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU,
                                                                 TOKEN_SERVICO.MATRICULA_REABERTURA,
                                                                 TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_LATO_SENSU};

                var ret = new SolicitacaoServicoPessoaAtuacaoListaVO
                {
                    DataSolicitacao = d.DataSolicitacao,
                    DescricaoSituacaoEtapa = situacaoAtualSolicitacao?.Descricao ?? string.Empty,
                    DescricaoProcesso = d.DescricaoProcesso,
                    NumeroProtocolo = d.NumeroProtocolo,
                    CategoriaSituacao = d.CategoriaSituacao,
                    SeqSolicitacaoServico = d.SeqSolicitacaoServico,
                    Cancelado = situacaoAtualSolicitacao?.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado,
                    Encerrado = situacaoAtualSolicitacao?.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso || situacaoAtualSolicitacao?.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso,
                    ExisteUsuarioResponsavel = d.ExisteUsuarioResponsavel,
                    OrigemSolicitacaoServico = d.OrigemSolicitacaoServico,
                    SeqPessoaAtuacao = d.SeqPessoaAtuacao,
                    AbrirCentralPreMatricula = tokensServicoPreMatricula.Contains(d.TokenServico),
                    ProcessoEncerrado = d.Etapas.Any(e => e.DataEncerramento.HasValue)
                };

                //RN_ALN_044 - Página de serviço
                //Entrar no fluxo da última etapa configurada cuja aplicação seja SGA.Aluno
                var etapasSGFAplicacaoSGAAluno = etapasSGF.Where(a => a.PossuiFluxoNaAplicacaoSGAAluno).OrderByDescending(o => o.OrdemEtapaSGF).ToList();

                foreach (var etapaSGF in etapasSGFAplicacaoSGAAluno)
                {
                    var etapaSolicitacao = d.Etapas.FirstOrDefault(et => et.SeqEtapaSgf == etapaSGF.SeqEtapaSGF);

                    if (etapaSolicitacao != null)
                    {
                        ret.SeqConfiguracaoEtapa = etapaSolicitacao.SeqConfiguracaoEtapa;
                        break;
                    }
                }

                var etapas = new List<SolicitacaoServicoPessoaAtuacaoItemListaVO>();

                etapasSGF.SMCForEach(e =>
                {
                    var etapa = d.Etapas.FirstOrDefault(et => et.SeqEtapaSgf == e.SeqEtapaSGF);
                    EtapaSituacaoVO situacaoAtualEtapa = null;

                    if (etapa != null)
                        situacaoAtualEtapa = e.Situacoes.FirstOrDefault(et => et.Seq == etapa.SeqSituacaoEtapaSgf);

                    etapas.Add(new SolicitacaoServicoPessoaAtuacaoItemListaVO()
                    {
                        SeqSolicitacao = e.SeqSolicitacaoServico,
                        SeqConfiguracaoEtapa = e.SeqConfiguracaoEtapa,
                        SituacaoEtapa = situacaoAtualEtapa?.Descricao ?? "Não iniciada",
                        DescricaoEtapa = e.DescricaoEtapa,
                        Instrucoes = e.Instrucoes,
                        DataInicio = e.DataInicio,
                        DataFim = e.DataFim,
                        ExibirVisualizarPlanoEstudos = e.ExibirVisualizarPlanoEstudos
                    });
                });

                ret.Etapas = etapas;

                lista.Add(ret);
            });

            return new SMCPagerData<SolicitacaoServicoPessoaAtuacaoListaVO>(lista, total);
        }

        public List<SMCDatasourceItem> BuscarSolicitacoesPessoaAtuacaoTipoDocumentoSelect(long seqPessoaAtuacao, long seqTipoDocumento)
        {
            var spec = new SolicitacaoServicoFilterSpecification() { SeqPessoaAtuacao = seqPessoaAtuacao, SeqTipoDocumento = seqTipoDocumento };

            var resultPessoaAtuacao = this.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem()
            {
                Seq = s.Seq,
                Descricao = s.NumeroProtocolo + " - " + s.ConfiguracaoProcesso.Processo.Descricao
            }).ToList();

            //Apos recuperar os dados da pessoa atuação, descobre qual é o ingressante do histórico mais antigo do aluno referente a pessoa atuação
            //e unifica os dados, caso existam solicitações do o ingressante encontrado

            //Cria o spec do aluno, usando o seq da pessoa atuação
            var specAluno = new SMCSeqSpecification<Aluno>(seqPessoaAtuacao);

            //Recupera o seq do ingressante do historico mais antigo do aluno, da pessao atuação
            var seqIngressante = this.AlunoDomainService.SearchProjectionByKey(specAluno, p => p.Historicos.OrderBy(h => h.DataInclusao).FirstOrDefault().SeqIngressante);

            //Se encontrado ingressante
            if (seqIngressante.HasValue)
            {
                //Atualiza o spec passando o ingressante encontrado
                spec = new SolicitacaoServicoFilterSpecification() { SeqPessoaAtuacao = seqIngressante.Value, SeqTipoDocumento = seqTipoDocumento };

                //Recupera os dados do ingressante
                var resultIngressante = this.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem()
                {
                    Seq = s.Seq,
                    Descricao = s.NumeroProtocolo + " - " + s.ConfiguracaoProcesso.Processo.Descricao
                });

                //Caso encontre algum registro, adiciona os mesmos ao resultado
                if (resultIngressante != null && resultIngressante.Any())
                    resultPessoaAtuacao.AddRange(resultIngressante);
            }

            return resultPessoaAtuacao;
        }

        /// <summary>
        /// Busca o totalizador de solicitações de serviço de um determinado
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação relacionada</param>
        /// <returns>Totalizador de solicitações da pessoa atuação</returns>
        public TotalizadorSolicitacaoServicoVO BuscarTotalizadorSolicitacoesServico(long seqPessoaAtuacao)
        {
            var spec = new SolicitacaoServicoFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                // Task 34107 solicitou retirar este filtro
                //OrigensSolicitacaoServico = new List<OrigemSolicitacaoServico> { OrigemSolicitacaoServico.PortalAluno, OrigemSolicitacaoServico.Presencial }
            };

            // Considera também as solicitações dos ingressantes
            var seqsIngressantes = AlunoDomainService.SearchProjectionByKey(spec.SeqPessoaAtuacao.Value, a => a.Historicos.Where(i => i.SeqIngressante.HasValue).Select(i => i.SeqIngressante.Value)).ToList();
            if (seqsIngressantes.SMCAny())
                spec.SeqsIngressantes = seqsIngressantes;

            var ultimasSituacoes = this.SearchProjectionBySpecification(spec, x => x.SituacaoAtual.CategoriaSituacao).ToList();

            return new TotalizadorSolicitacaoServicoVO
            {
                Novas = ultimasSituacoes.Count(x => x == CategoriaSituacao.Novo),
                EmAndamento = ultimasSituacoes.Count(x => x == CategoriaSituacao.EmAndamento),
                Encerradas = ultimasSituacoes.Count(x => x == CategoriaSituacao.Encerrado),
                Concluidas = ultimasSituacoes.Count(x => x == CategoriaSituacao.Concluido),
            };
        }

        /// <summary>
        /// Atualiza os grupos de escalonamento da solicitação de serviço do ingressante da chamada
        /// </summary>
        /// <param name="seqChamada">Sequencial da chamada do convocado</param>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento selecionado</param>
        public void AtualizarSolicitacaoServicoGrupoEscalonamento(long seqCampanha, long seqGrupoEscalonamento)
        {
            var specIngressantes = new IngressanteFilterSpecification() { SeqChamada = seqCampanha };
            var ingressantes = this.IngressanteDomainService.SearchProjectionBySpecification(specIngressantes, p => p.Seq).ToList();

            if (ingressantes.Count > 0)
            {
                var specSolicitacoes = new SolicitacaoServicoFilterSpecification() { SeqsIngressantes = ingressantes };
                var solicitacoes = this.SearchBySpecification(specSolicitacoes).ToList();
                foreach (var registro in solicitacoes)
                {
                    registro.SeqGrupoEscalonamento = seqGrupoEscalonamento;
                    this.UpdateFields(registro, p => p.SeqGrupoEscalonamento);
                }
            }
        }

        /// <summary>
        /// Busca as situações de documentação pelo sequencial do processo
        /// </summary>
        /// <param name="seqsProcessos">Sequenciais dos processos</param>
        /// <returns>Lista com as situações de documentação</returns>
        public List<SMCDatasourceItem> BuscarSituacoesDocumentacaoDoProcessoSelect(List<long> seqsProcessos)
        {
            var lista = new List<SMCDatasourceItem>();

            if (seqsProcessos.Count > 0)
            {
                var spec = new SMCContainsSpecification<Processo, long>(p => p.Seq, seqsProcessos.ToArray());

                var processoPossuiConfiguracaoDocumentacao = this.ProcessoDomainService.SearchProjectionBySpecification(spec,
                    p => p.Etapas.Any(e => e.ConfiguracoesEtapa.Any(c => c.DocumentosRequeridos.Any())));

                if (!processoPossuiConfiguracaoDocumentacao.Any(p => p))
                {
                    lista.Add(new SMCDatasourceItem((long)SituacaoDocumentacao.NaoRequerida, SMCEnumHelper.GetDescription(SituacaoDocumentacao.NaoRequerida)));
                }
                else
                {
                    foreach (var item in SMCEnumHelper.GenerateKeyValuePair<SituacaoDocumentacao>())
                    {
                        if (item.Key != SituacaoDocumentacao.NaoRequerida)
                            lista.Add(new SMCDatasourceItem(((long)item.Key), SMCEnumHelper.GetDescription(item.Key)));
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Busca os dados se identificação do solicitante de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados de identificação do solicitante</returns>
        public DadosSolicitacaoVO BuscarDadosIdentificacaoSolicitante(long seqSolicitacaoServico)
        {
            //Cria o objeto de retorno
            var result = new DadosSolicitacaoVO()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                Solicitacao = new SolicitacaoVO(),
                SolicitacaoAtualizada = new SolicitacaoAtualizadaVO(),
                SolicitacaoOriginal = new SolicitacaoOriginalVO(),
                Solicitante = new SolicitanteVO(),
                HistoricosSolicitacao = new List<HistoricoSolicitacaoVO>(),
                NotificacoesSolicitacao = new List<NotificacaoSolicitacaoVO>(),
                DocumentosSolicitacao = new List<DocumentoSolicitacaoVO>(),
            };

            //Cria a specification da solicitacao de serviços
            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            //Recupera os dados necessários para retorno
            var solServico = this.SearchProjectionByKey(specSolicitacaoServico,
            s => new
            {
                PessoaSolicitante = s.PessoaAtuacao.Pessoa,
                PessoaAtuacaoSolicitante = s.PessoaAtuacao,
                EnderecosEletronicosSolicitante = s.PessoaAtuacao.EnderecosEletronicos.Select(e => e.EnderecoEletronico).ToList(),
                DadosPessoaisSolicitante = s.PessoaAtuacao.DadosPessoais,
                TelefonesSolicitante = s.PessoaAtuacao.Telefones.Select(t => t.Telefone).ToList(),
                Solicitacao = new
                {
                    s.DataPrevistaSolucao,
                    s.DataSolucao,
                    s.NumeroProtocolo,
                    s.Seq,
                    s.SituacaoDocumentacao,
                    s.DataSolicitacao,
                    s.DescricaoOriginal,
                    s.UsuarioAlteracao,
                    SituacaoAtual = new { s.SituacaoAtual.DataInclusao },
                    s.DescricaoAtualizada,
                    s.UsuarioInclusao
                },
                UsuarioResponsavelSolicitacao = s.UsuariosResponsaveis.OrderByDescending(o => o.Seq).FirstOrDefault().UsuarioInclusao,
                UsuariosResponsaveisSolicitacao = s.UsuariosResponsaveis.OrderByDescending(o => o.Seq).Select(u => new { u.UsuarioInclusao, u.DataInclusao }).ToList(),
                SeqGrupoEscalonamentoSolicitacao = (long?)s.GrupoEscalonamento.Seq,
                DescricaoGrupoEscalonamentoSolicitacao = s.GrupoEscalonamento.Descricao,
                CursoOfertaLocalidadeHistoricoAlunoSolicitacao = s.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                TurnoHistoricoAlunoSolicitacao = s.AlunoHistorico.CursoOfertaLocalidadeTurno.Turno.Descricao,
                ProcessoSolicitacao = s.ConfiguracaoProcesso.Processo.Descricao,
                SeqProcessoSolicitacao = (long?)s.ConfiguracaoProcesso.Processo.Seq,
                OrigemSolicitacaoServico = s.OrigemSolicitacaoServico,
                JustificativaSolicitacaoOriginal = s.JustificativaSolicitacaoServico.Descricao,
                JustificativaComplementarSolicitacaoOriginal = s.JustificativaComplementar,
                EAluno = s.PessoaAtuacao is Aluno,
                NumeroRegistroAcademico = (long?)(s.PessoaAtuacao is Aluno ? (s.PessoaAtuacao as Aluno).NumeroRegistroAcademico : 0),
                CodigoAlunoMigracao = s.PessoaAtuacao is Aluno ? (s.PessoaAtuacao as Aluno).CodigoAlunoMigracao : null,
            });

            //Cria o objeto de retorno - SOLICITANTE
            result.Solicitante.Seq = solServico.PessoaAtuacaoSolicitante.Seq;
            result.Solicitante.Cpf = solServico.PessoaSolicitante.Cpf;

            if (solServico.EnderecosEletronicosSolicitante.Count > 0)
            {
                result.Solicitante.EnderecoEletronico = solServico.EnderecosEletronicosSolicitante
                                                                  .Where(w => w.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                                                                  .OrderByDescending(o => o.DataAlteracao)
                                                                  .FirstOrDefault() == null ?
                                                        solServico.EnderecosEletronicosSolicitante
                                                                  .Where(w => w.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                                                                  .OrderByDescending(o => o.DataInclusao)
                                                                  .FirstOrDefault()
                                                                  .Descricao :
                                                        solServico.EnderecosEletronicosSolicitante
                                                                  .Where(w => w.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                                                                  .OrderByDescending(o => o.DataAlteracao)
                                                                  .FirstOrDefault()
                                                                  .Descricao;
            }
            result.Solicitante.Nome = solServico.DadosPessoaisSolicitante.Nome;
            result.Solicitante.NomeSocial = solServico.DadosPessoaisSolicitante.NomeSocial;
            result.Solicitante.NumeroPassaporte = solServico.PessoaSolicitante.NumeroPassaporte;

            if (solServico.TelefonesSolicitante.Count > 0)
            {
                result.Solicitante.Telefone = solServico.TelefonesSolicitante.OrderByDescending(o => o.DataAlteracao).FirstOrDefault() == null ? $"({solServico.TelefonesSolicitante.OrderByDescending(o => o.DataInclusao).FirstOrDefault().CodigoArea}) {solServico.TelefonesSolicitante.OrderByDescending(o => o.DataInclusao).FirstOrDefault().Numero}" : $"({solServico.TelefonesSolicitante.OrderByDescending(o => o.DataAlteracao).FirstOrDefault().CodigoArea}) {solServico.TelefonesSolicitante.OrderByDescending(o => o.DataAlteracao).FirstOrDefault().Numero}";
            }
            result.Solicitante.TipoAtuacao = solServico.PessoaAtuacaoSolicitante.TipoAtuacao;
            result.Solicitante.EAluno = solServico.EAluno;
            result.Solicitante.NumeroRegistroAcademico = solServico.NumeroRegistroAcademico.GetValueOrDefault();
            result.Solicitante.CodigoAlunoMigracao = solServico.CodigoAlunoMigracao;

            //Cria o objeto de retorno - SOLICITAÇÃO
            result.Solicitacao.DataPrevistaSolucao = solServico.Solicitacao.DataPrevistaSolucao;
            result.Solicitacao.DataSolucao = solServico.Solicitacao.DataSolucao;
            result.Solicitacao.SeqGrupoEscalonamento = solServico.SeqGrupoEscalonamentoSolicitacao;
            result.Solicitacao.GrupoEscalonamento = solServico.DescricaoGrupoEscalonamentoSolicitacao;

            var historicoAluno = string.Empty;

            if (!string.IsNullOrEmpty(solServico.CursoOfertaLocalidadeHistoricoAlunoSolicitacao))
            {
                if (!string.IsNullOrEmpty(solServico.TurnoHistoricoAlunoSolicitacao))
                    historicoAluno += $"{solServico.CursoOfertaLocalidadeHistoricoAlunoSolicitacao} - {solServico.TurnoHistoricoAlunoSolicitacao}";
                else
                    historicoAluno += $"{solServico.CursoOfertaLocalidadeHistoricoAlunoSolicitacao}";
            }
            else
            {
                if (!string.IsNullOrEmpty(solServico.TurnoHistoricoAlunoSolicitacao))
                    historicoAluno += $"{solServico.TurnoHistoricoAlunoSolicitacao}";
            }

            result.Solicitacao.HistoricoAluno = historicoAluno;
            result.Solicitacao.SeqProcesso = solServico.SeqProcessoSolicitacao.GetValueOrDefault();
            result.Solicitacao.Processo = solServico.ProcessoSolicitacao;
            result.Solicitacao.OrigemSolicitacaoServico = solServico.OrigemSolicitacaoServico;
            result.Solicitacao.Protocolo = solServico.Solicitacao.NumeroProtocolo;
            result.Solicitacao.Seq = solServico.Solicitacao.Seq;
            result.Solicitacao.SituacaoDocumentacao = solServico.Solicitacao.SituacaoDocumentacao;
            result.Solicitacao.UsuarioResponsavel = solServico.UsuarioResponsavelSolicitacao;

            if (solServico.UsuariosResponsaveisSolicitacao.Any())
            {
                result.Solicitacao.UsuariosResponsaveis = new List<string>();

                foreach (var outroUsuarioResponsavel in solServico.UsuariosResponsaveisSolicitacao)
                {
                    result.Solicitacao.UsuariosResponsaveis.Add($"{outroUsuarioResponsavel.DataInclusao.ToString("dd/MM/yy")} - {outroUsuarioResponsavel.UsuarioInclusao}");
                }
            }
            //Cria o objeto de retorno - SOLICITAÇÃO ORIGINAL
            result.SolicitacaoOriginal.CriadoPor = solServico.Solicitacao.UsuarioInclusao;
            result.SolicitacaoOriginal.DataCriacao = solServico.Solicitacao.DataSolicitacao;
            result.SolicitacaoOriginal.DescricaoSolicitacao = solServico.Solicitacao.DescricaoOriginal;
            result.SolicitacaoOriginal.Justificativa = solServico.JustificativaSolicitacaoOriginal;
            result.SolicitacaoOriginal.JustificativaComplementar = solServico.JustificativaComplementarSolicitacaoOriginal;

            //Cria o objeto de retorno - SOLICITAÇÃO ATUALIZADA
            result.SolicitacaoAtualizada.AtualizadoPor = solServico.Solicitacao.UsuarioAlteracao;
            result.SolicitacaoAtualizada.DataAtualizacao = solServico.Solicitacao.SituacaoAtual.DataInclusao;
            result.SolicitacaoAtualizada.DescricaoSolicitacao = solServico.Solicitacao.DescricaoAtualizada;

            return result;
        }

        /// <summary>
        /// Busca os historicos de uma solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Históricos de uma solicitação de serviços</returns>
        public DadosSolicitacaoVO BuscarHistoricosSolicitacao(long seqSolicitacaoServico, long seqPessoaAtuacao)
        {
            //Cria o objeto de retorno
            var result = new DadosSolicitacaoVO()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                Solicitacao = new SolicitacaoVO(),
                SolicitacaoAtualizada = new SolicitacaoAtualizadaVO(),
                SolicitacaoOriginal = new SolicitacaoOriginalVO(),
                Solicitante = new SolicitanteVO(),
                HistoricosSolicitacao = new List<HistoricoSolicitacaoVO>(),
                NotificacoesSolicitacao = new List<NotificacaoSolicitacaoVO>(),
                DocumentosSolicitacao = new List<DocumentoSolicitacaoVO>(),
            };

            //Cria a specification da solicitacao de serviços
            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            //Recupera os dados necessários para retorno
            var solServico = this.SearchProjectionByKey(specSolicitacaoServico,
            s => new
            {
                HistoricosSolicitacao = s.Etapas.Select(e => new
                {
                    SeqSolicitacaoServico = s.Seq,
                    SeqSolicitacaoServicoEtapa = e.Seq,
                    SeqTemplateProcessoSgf = e.ConfiguracaoEtapa.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                    SeqEtapaSgf = e.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                    SeqConfiguracaoEtapa = e.SeqConfiguracaoEtapa,
                    SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                    SeqSituacaoEtapaSgf = e.SituacaoAtual.SeqSituacaoEtapaSgf,
                    Historicos = e.HistoricosSituacao.Select(h =>
                  new
                  {
                      SeqSituacaoEtapaSgf = h.SeqSituacaoEtapaSgf,
                      SeqMotivoSituacaoSgf = h.SeqMotivoSituacaoSgf,
                      DataInicio = h.DataInclusao,
                      UsuarioResponsavel = h.UsuarioInclusao,
                      Observacao = h.Observacao,
                  }).ToList()
                }).ToList()
            });

            var novoHistoricoSolicitacao = new HistoricoSolicitacaoVO() { Etapas = new List<HistoricoSolicitacaoEtapaVO>(), Historicos = new List<HistoricoSolicitacaoEtapaItemVO>() };

            for (int i = 0; i < solServico.HistoricosSolicitacao.Count; i++)
            {
                var historico = solServico.HistoricosSolicitacao[i];

                var etapaAtualSGF = SGFHelper.BuscarEtapasSGFCache(historico.SeqTemplateProcessoSgf).FirstOrDefault(e => e.Seq == historico.SeqEtapaSgf);
                var situacaoAtualSGF = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == historico.SeqSituacaoEtapaSgf);

                var novaEtapa = new HistoricoSolicitacaoEtapaVO()
                {
                    SeqSolicitacaoServico = historico.SeqSolicitacaoServico,
                    SeqSolicitacaoServicoEtapa = historico.SeqSolicitacaoServicoEtapa,
                    Etapa = etapaAtualSGF.Descricao,
                };

                if (situacaoAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                {
                    novaEtapa.PossuiBloqueio = PossuiBloqueio.Nao;
                }
                else
                {
                    novaEtapa.PossuiBloqueio = PessoaAtuacaoBloqueioDomainService.PessoaAtuacaoPossuiBloqueios(historico.SeqPessoaAtuacao, historico.SeqConfiguracaoEtapa, true) ? PossuiBloqueio.Sim : PossuiBloqueio.Nao;
                }

                novoHistoricoSolicitacao.Etapas.Add(novaEtapa);

                for (int j = 0; j < historico.Historicos.Count; j++)
                {
                    var historicoItem = historico.Historicos[j];

                    var proximoHistoricoItem = j + 1 < historico.Historicos.Count ? historico.Historicos[j + 1] : null;

                    var novoHistoricoItem = new HistoricoSolicitacaoEtapaItemVO()
                    {
                        Etapa = $"{etapaAtualSGF.Ordem}º Etapa",
                        DataInicio = historicoItem.DataInicio,
                        DataFim = proximoHistoricoItem != null ? (DateTime?)proximoHistoricoItem.DataInicio : null,
                        UsuarioResponsavel = historicoItem.UsuarioResponsavel,
                    };

                    if (etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == historicoItem.SeqSituacaoEtapaSgf) != null)
                    {
                        novoHistoricoItem.SituacaoFinalEtapa = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == historicoItem.SeqSituacaoEtapaSgf).SituacaoFinalEtapa ? SituacaoFinalEtapa.Sim : SituacaoFinalEtapa.Nao;

                        novoHistoricoItem.Situacao = $"{SMCEnumHelper.GetDescription(etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == historicoItem.SeqSituacaoEtapaSgf).CategoriaSituacao)} - {etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == historicoItem.SeqSituacaoEtapaSgf).Descricao}";

                        if (historicoItem.SeqMotivoSituacaoSgf.HasValue)
                            novoHistoricoItem.Observacao = $"{etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == historicoItem.SeqSituacaoEtapaSgf).MotivosSituacao.FirstOrDefault(m => m.Seq == historicoItem.SeqMotivoSituacaoSgf.Value).Descricao} - {historicoItem.Observacao}";
                        else
                            novoHistoricoItem.Observacao = historicoItem.Observacao;
                    }
                    else
                    {
                        novoHistoricoItem.SituacaoFinalEtapa = SituacaoFinalEtapa.Nenhum;
                        novoHistoricoItem.Situacao = MessagesResource.MSG_NaoExisteSituacaoEtapaSGF;
                        novoHistoricoItem.Observacao = historicoItem.Observacao;
                    }

                    novoHistoricoSolicitacao.Historicos.Add(novoHistoricoItem);
                }
            }

            //Ordena o histórico por ordem cronológica
            novoHistoricoSolicitacao.Historicos = novoHistoricoSolicitacao.Historicos.OrderBy(o => o.DataInicio).ToList();

            result.HistoricosSolicitacao.Add(novoHistoricoSolicitacao);

            return result;
        }

        /// <summary>
        /// Busca as notificações de uma solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Notificações de uma solicitação de serviços</returns>
        public DadosSolicitacaoVO BuscarNotificacoesSolicitacao(NotificacaoSolicitacaoFiltroVO filtro)
        {
            //Cria o objeto de retorno
            var result = new DadosSolicitacaoVO()
            {
                SeqSolicitacaoServico = filtro.SeqSolicitacaoServico.Value,
                Solicitacao = new SolicitacaoVO(),
                SolicitacaoAtualizada = new SolicitacaoAtualizadaVO(),
                SolicitacaoOriginal = new SolicitacaoOriginalVO(),
                Solicitante = new SolicitanteVO(),
                HistoricosSolicitacao = new List<HistoricoSolicitacaoVO>(),
                NotificacoesSolicitacao = new List<NotificacaoSolicitacaoVO>(),
                DocumentosSolicitacao = new List<DocumentoSolicitacaoVO>(),
            };

            //Cria a specification da solicitacao de serviços
            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(filtro.SeqSolicitacaoServico.Value);

            //Recupera os dados necessários para retorno
            var solServico = this.SearchProjectionByKey(specSolicitacaoServico,
            s => new
            {
                SeqsNotificacoesEmailsDestinatarios = s.EnviosNotificacao.Select(e => e.SeqNotificacaoEmailDestinatario).ToList()
            });

            filtro.SeqsNotificacoesEmailDestinatario = solServico.SeqsNotificacoesEmailsDestinatarios;

            var notificacoes = this.NotificacaoService.ConsultaNotificacoes(filtro.Transform<NotificacaoEmailDestinatarioFiltroData>());

            //Cria o objeto de retorno - NOTIFICAÇÕES
            result.NotificacoesSolicitacao = notificacoes.TransformList<NotificacaoSolicitacaoVO>();

            return result;
        }

        /// <summary>
        /// Busca os documentos de uma solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Documentos de uma solicitação de serviços</returns>
        public DadosSolicitacaoVO BuscarDocumentosSolicitacao(long seqSolicitacaoServico, long seqPessoaAtuacao)
        {
            //Cria o objeto de retorno
            var result = new DadosSolicitacaoVO()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                Solicitacao = new SolicitacaoVO(),
                SolicitacaoAtualizada = new SolicitacaoAtualizadaVO(),
                SolicitacaoOriginal = new SolicitacaoOriginalVO(),
                Solicitante = new SolicitanteVO(),
                HistoricosSolicitacao = new List<HistoricoSolicitacaoVO>(),
                NotificacoesSolicitacao = new List<NotificacaoSolicitacaoVO>(),
                DocumentosConclusaoSolicitacao = new List<DocumentoConclusaoSolicitacaoVO>(),
                DocumentosSolicitacao = new List<DocumentoSolicitacaoVO>(),
            };

            //Cria a specification da solicitacao de serviços
            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            //Recupera os dados necessários para retorno
            var solServico = this.SearchProjectionByKey(specSolicitacaoServico,
            s => new
            {
                SolicitacaoServicoEDeMatricula = s is SolicitacaoMatricula,
                TokenTipoServico = s.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token,
                SolicitacaoPossuiCodigoAdesao = (s as SolicitacaoMatricula).CodigoAdesao.HasValue,
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqSituacaoEtapaAtualSGF = s.SituacaoAtual.SeqSituacaoEtapaSgf,
                SeqEtapaSgf = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                SeqProcessoEtapaAtual = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa,
                SeqCurso = (long?)s.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                DescricaoCurso = s.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                SexoAluno = s.PessoaAtuacao.DadosPessoais.Sexo,
                DocumentacaoSolicitacao = s.DocumentosRequeridos.Select(d => new
                {
                    d.DocumentoRequerido.SeqTipoDocumento,
                    d.ArquivoAnexado,
                    d.DataEntrega,
                    d.FormaEntregaDocumento,
                    d.Observacao,
                    d.VersaoDocumento,
                    d.DataPrazoEntrega,
                    d.SituacaoEntregaDocumento
                }).ToList(),
                DocumentacaoConclusaoSolicitacao = s.DocumentosAcademicos.Select(d => new
                {
                    SeqDocumentoConclusao = d.Seq,
                    DescricaoTipoDocumentoAcademico = d.TipoDocumentoAcademico.Descricao,
                    TokenTipoDocumentoAcademico = d.TipoDocumentoAcademico.Token,
                    DescricaoSituacaoDocumentoAcademicoAtual = d.SituacaoAtual.SituacaoDocumentoAcademico.Descricao,
                    d.SituacaoAtual.SituacaoDocumentoAcademico.ClasseSituacaoDocumento,
                    TipoInvalidade = (TipoInvalidade?)d.SituacaoAtual.ClassificacaoInvalidadeDocumento.TipoInvalidade,
                    d.NumeroViaDocumento,
                    d.SeqDocumentoGAD
                }).ToList()
            });

            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(solServico.SeqTemplateProcessoSGF);
            var etapaAtualSGF = etapasSGF.FirstOrDefault(e => e.Seq == solServico.SeqEtapaSgf);
            var situacaoEtapaAtualSGF = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == solServico.SeqSituacaoEtapaAtualSGF);

            //Busca os tipos de documentos
            var tiposDocumentos = TipoDocumentoService.BuscarTiposDocumentos();

            //Cria o objeto de retorno - DOCUMENTAÇÃO
            result.ExibirComprovanteMatriculaETermoAdesao = solServico.SolicitacaoServicoEDeMatricula &&
                                (solServico.TokenTipoServico == TOKEN_TIPO_SERVICO.MATRICULA_INGRESSANTE ||
                                    solServico.TokenTipoServico == TOKEN_TIPO_SERVICO.RENOVACAO_MATRICULA ||
                                    solServico.TokenTipoServico == TOKEN_TIPO_SERVICO.PLANO_ESTUDO ||
                                    solServico.TokenTipoServico == TOKEN_TIPO_SERVICO.MATRICULA_REABERTURA);
            result.SolicitacaoPossuiCodigoAdesao = solServico.SolicitacaoPossuiCodigoAdesao;
            result.SolicitacaoComMatriculaEfetivada = situacaoEtapaAtualSGF.SituacaoFinalProcesso && situacaoEtapaAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso;
            result.Solicitacao.TokenTipoServico = solServico.TokenTipoServico;
            result.Solicitacao.SeqProcessoEtapaAtual = solServico.SeqProcessoEtapaAtual;
            result.DocumentosConclusaoSolicitacao = solServico.DocumentacaoConclusaoSolicitacao.Select(d => new DocumentoConclusaoSolicitacaoVO()
            {
                SeqDocumentoAcademico = d.SeqDocumentoConclusao,
                DescricaoCurso = solServico.DescricaoCurso,
                DescricaoTipoDocumentoAcademico = d.DescricaoTipoDocumentoAcademico,
                DescricaoSituacaoDocumentoAcademicoAtual = d.ClasseSituacaoDocumento != ClasseSituacaoDocumento.Invalido ? d.DescricaoSituacaoDocumentoAcademicoAtual : d.TipoInvalidade.HasValue ? $"{d.DescricaoSituacaoDocumentoAcademicoAtual} ({d.TipoInvalidade.SMCGetDescription()})" : d.DescricaoSituacaoDocumentoAcademicoAtual,
                NumeroViaDocumento = d.NumeroViaDocumento,
                SeqDocumentoGAD = d.SeqDocumentoGAD,
                TokenTipoDocumentoAcademico = d.TokenTipoDocumentoAcademico
            }).OrderBy(o => o.SeqDocumentoAcademico).ToList();
            result.DocumentosSolicitacao = solServico.DocumentacaoSolicitacao.Select(d => new DocumentoSolicitacaoVO()
            {
                TipoDocumento = tiposDocumentos.FirstOrDefault(t => t.Seq == d.SeqTipoDocumento).Descricao,
                SeqArquivoAnexado = d.ArquivoAnexado?.Seq,
                UidArquivoAnexado = d.ArquivoAnexado?.UidArquivo,
                DataEntrega = d.DataEntrega,
                FormaEntregaDocumento = d.FormaEntregaDocumento,
                VersaoDocumento = d.VersaoDocumento,
                DataPrazoEntrega = d.DataPrazoEntrega,
                Observacao = d.Observacao,
                SituacaoEntregaDocumento = d.SituacaoEntregaDocumento
            }).ToList();

            foreach (var documentoSolicitacao in result.DocumentosSolicitacao)
            {
                if (documentoSolicitacao.SeqArquivoAnexado.HasValue)
                {
                    var arquivoAnexado = ArquivoAnexadoDomainService.SearchByKey(new SMCSeqSpecification<ArquivoAnexado>(documentoSolicitacao.SeqArquivoAnexado.Value));
                    documentoSolicitacao.ArquivoAnexado = arquivoAnexado.Transform<SMCUploadFile>();
                    documentoSolicitacao.ArquivoAnexado.GuidFile = arquivoAnexado.UidArquivo.ToString();
                }
            }

            foreach (var documentoConclusao in result.DocumentosConclusaoSolicitacao)
            {
                var specDocumentoConclusaoFormacao = new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = documentoConclusao.SeqDocumentoAcademico };
                var documentosConclusaoFormacao = DocumentoConclusaoFormacaoDomainService.SearchBySpecification(specDocumentoConclusaoFormacao, x => x.AlunoFormacao.FormacaoEspecifica, x => x.AlunoFormacao.Titulacao).ToList();

                if (documentosConclusaoFormacao.Any())
                {
                    var formacao = documentosConclusaoFormacao.First();

                    var seqCurso = solServico.SeqCurso;
                    var seqFormacaoEspecifica = formacao.AlunoFormacao.SeqFormacaoEspecifica;
                    var seqEntidadeResponsavel = formacao.AlunoFormacao.FormacaoEspecifica.SeqEntidadeResponsavel;

                    var specCursoFormacaoEspecifica = new CursoFormacaoEspecificaFilterSpecification() { SeqCurso = seqCurso, SeqFormacaoEspecifica = seqFormacaoEspecifica };
                    var cursoFormacaoEspecifica = CursoFormacaoEspecificaDomainService.SearchByKey(specCursoFormacaoEspecifica, x => x.GrauAcademico);

                    var descricaoFormacao = FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(seqFormacaoEspecifica, seqEntidadeResponsavel, true);

                    if (descricaoFormacao != null && descricaoFormacao.Any())
                    {
                        documentoConclusao.DescricoesFormacaoEspecifica = descricaoFormacao.Select(d => d.DescricaoFormacaoEspecifica).ToList();
                    }
                    else
                    {
                        var descricaoFormacaoEspecifica = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(new long[] { seqFormacaoEspecifica });
                        var hierarquiasFormacao = descricaoFormacaoEspecifica.SelectMany(a => a.Hierarquia).ToList();
                        documentoConclusao.DescricoesFormacaoEspecifica = hierarquiasFormacao.Select(a => $"[{a.DescricaoTipoFormacaoEspecifica}] {a.Descricao}").ToList();
                    }

                    if (cursoFormacaoEspecifica != null)
                    {
                        documentoConclusao.DescricaoGrauAcademico = cursoFormacaoEspecifica.GrauAcademico.Descricao;
                    }

                    documentoConclusao.DescricaoTitulacao = (documentoConclusao.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL ||
                                                             documentoConclusao.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL ||
                                                             documentoConclusao.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL ||
                                                             documentoConclusao.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA) &&
                                                             !string.IsNullOrEmpty(formacao.AlunoFormacao.Titulacao.DescricaoXSD) ? formacao.AlunoFormacao.Titulacao.DescricaoXSD :
                                                             solServico.SexoAluno == Sexo.Masculino ? formacao.AlunoFormacao.Titulacao.DescricaoMasculino : formacao.AlunoFormacao.Titulacao.DescricaoFeminino;
                }
            }

            return result;
        }

        /// <summary>
        /// Busca os dados do solicitante
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="desativarFiltroDados">Desabilita o filtro de dados de hierarquia_entidade_organizacional para atender o caso de entidades compartilhadas da tela de consulta de solicitações de serviço </param>
        /// <returns></returns>
        public DadosSolicitanteVO BuscarDadosSolicitante(long seqPessoaAtuacao, bool desativarFiltroDados = false)
        {
            if (desativarFiltroDados)
                this.PessoaAtuacaoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var result = this.PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao),
                p => new DadosSolicitanteVO()
                {
                    TipoAtuacao = p.TipoAtuacao,
                    Nome = p.DadosPessoais.Nome,
                    NomeSocial = p.DadosPessoais.NomeSocial,
                    Identificacao = new PessoaIdentificacaoVO()
                    {
                        CategoriaCnh = p.DadosPessoais.CategoriaCnh,
                        CodigoCidadeNaturalidade = p.DadosPessoais.CodigoCidadeNaturalidade,
                        CodigoPaisEmissaoPassaporte = p.Pessoa.CodigoPaisEmissaoPassaporte,
                        CodigoPaisNacionalidade = p.Pessoa.CodigoPaisNacionalidade,
                        Cpf = p.Pessoa.Cpf,
                        CsmDocumentoMilitar = p.DadosPessoais.CsmDocumentoMilitar,
                        DataEmissaoCnh = p.DadosPessoais.DataEmissaoCnh,
                        DataExpedicaoIdentidade = p.DadosPessoais.DataExpedicaoIdentidade,
                        DataNascimento = p.Pessoa.DataNascimento,
                        DataPisPasep = p.DadosPessoais.DataPisPasep,
                        DataValidadePassaporte = p.Pessoa.DataValidadePassaporte,
                        DataVencimentoCnh = p.DadosPessoais.DataVencimentoCnh,
                        DescricaoNaturalidadeEstrangeira = p.DadosPessoais.DescricaoNaturalidadeEstrangeira,
                        EstadoCivil = p.DadosPessoais.EstadoCivil,
                        Falecido = p.Pessoa.Falecido,
                        Filiacao = p.Pessoa.Filiacao.Select(f => new PessoaFiliacaoVO() { Seq = f.Seq, Nome = f.Nome, SeqPessoa = f.SeqPessoa, TipoParentesco = f.TipoParentesco }).ToList(),
                        NecessidadeEspecial = p.DadosPessoais.NecessidadeEspecial,
                        NumeroDocumentoMilitar = p.DadosPessoais.NumeroDocumentoMilitar,
                        NumeroIdentidade = p.DadosPessoais.NumeroIdentidade,
                        NumeroPassaporte = p.Pessoa.NumeroPassaporte,
                        NumeroPisPasep = p.DadosPessoais.NumeroPisPasep,
                        NumeroRegistroCnh = p.DadosPessoais.NumeroRegistroCnh,
                        NumeroSecaoTituloEleitor = p.DadosPessoais.NumeroSecaoTituloEleitor,
                        NumeroTituloEleitor = p.DadosPessoais.NumeroTituloEleitor,
                        NumeroZonaTituloEleitor = p.DadosPessoais.NumeroZonaTituloEleitor,
                        OrgaoEmissorIdentidade = p.DadosPessoais.OrgaoEmissorIdentidade,
                        RacaCor = p.DadosPessoais.RacaCor,
                        SeqArquivoFoto = p.DadosPessoais.SeqArquivoFoto,
                        Sexo = p.DadosPessoais.Sexo,
                        TipoDocumentoMilitar = p.DadosPessoais.TipoDocumentoMilitar,
                        TipoNacionalidade = p.Pessoa.TipoNacionalidade,
                        TipoNecessidadeEspecial = p.DadosPessoais.TipoNecessidadeEspecial,
                        TipoPisPasep = p.DadosPessoais.TipoPisPasep,
                        UfDocumentoMilitar = p.DadosPessoais.UfDocumentoMilitar,
                        UfIdentidade = p.DadosPessoais.UfIdentidade,
                        UfNaturalidade = p.DadosPessoais.UfNaturalidade,
                        UfTituloEleitor = p.DadosPessoais.UfTituloEleitor
                    },
                    Enderecos = p.Pessoa.Enderecos.Select(e => new EnderecoVO()
                    {
                        Bairro = e.Endereco.Bairro,
                        Cep = e.Endereco.Cep,
                        CodigoCidade = e.Endereco.CodigoCidade,
                        CodigoPais = e.Endereco.CodigoPais,
                        Complemento = e.Endereco.Complemento,
                        Correspondencia = e.Endereco.Correspondencia,
                        Logradouro = e.Endereco.Logradouro,
                        NomeCidade = e.Endereco.NomeCidade,
                        NomePais = e.Endereco.NomeCidade,
                        Numero = e.Endereco.Numero,
                        Seq = e.Endereco.Seq,
                        SeqEndereco = e.SeqEndereco,
                        SiglaUf = e.Endereco.SiglaUf,
                        TipoEndereco = e.Endereco.TipoEndereco
                    }).ToList(),
                    EnderecosEletronicos = p.Pessoa.EnderecosEletronicos.Select(e => new EnderecoEletronicoVO()
                    {
                        Descricao = e.EnderecoEletronico.Descricao,
                        Seq = e.EnderecoEletronico.Seq,
                        TipoEnderecoEletronico = e.EnderecoEletronico.TipoEnderecoEletronico
                    }).ToList(),
                    Telefones = p.Pessoa.Telefones.Select(t => new TelefoneVO()
                    {
                        CodigoArea = t.Telefone.CodigoArea,
                        CodigoPais = t.Telefone.CodigoPais,
                        Numero = t.Telefone.Numero,
                        Seq = t.Telefone.Seq,
                        TipoTelefone = t.Telefone.TipoTelefone
                    }).ToList()
                });

            if (result.Identificacao.CodigoCidadeNaturalidade.HasValue && !string.IsNullOrEmpty(result.Identificacao.UfNaturalidade))
                result.Identificacao.CidadeNaturalidade = this.LocalidadeService.BuscarCidade(result.Identificacao.CodigoCidadeNaturalidade.Value, result.Identificacao.UfNaturalidade).Nome.Trim();

            if (result.Identificacao.CodigoPaisEmissaoPassaporte.HasValue)
                result.Identificacao.PaisEmissaoPassaporte = this.LocalidadeService.BuscarPais(result.Identificacao.CodigoPaisEmissaoPassaporte.Value).Nome.Trim();

            result.Identificacao.PaisNacionalidade = this.LocalidadeService.BuscarPais(result.Identificacao.CodigoPaisNacionalidade).Nome.Trim();

            if (result.Identificacao.SeqArquivoFoto.HasValue)
            {
                var arquivoFoto = ArquivoAnexadoDomainService.SearchByKey(new SMCSeqSpecification<ArquivoAnexado>(result.Identificacao.SeqArquivoFoto.Value));
                result.Identificacao.ArquivoFoto = arquivoFoto.Transform<SMCUploadFile>();
                result.Identificacao.ArquivoFoto.GuidFile = arquivoFoto.UidArquivo.ToString();
            }

            if (desativarFiltroDados)
                this.PessoaAtuacaoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return result;
        }

        public HistoricoNavegacaoVO BuscarHistoricosNavegacao(long seqSolicitacaoServico, long seqSolicitacaoServicoEtapa)
        {
            var spec = new SolicitacaoServicoEtapaFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico, Seq = seqSolicitacaoServicoEtapa };

            var historicosNavegacao = this.SolicitacaoServicoEtapaDomainService.SearchProjectionByKey(spec,
                s => new HistoricoNavegacaoVO()
                {
                    Etapa = s.ConfiguracaoEtapa.ProcessoEtapa.DescricaoEtapa,
                    Paginas = s.HistoricosNavegacao.Select(h => new HistoricoNavegacaoItemVO()
                    {
                        Pagina = h.ConfiguracaoEtapaPagina.TituloPagina,
                        DataEntrada = h.DataEntrada,
                        DataSaida = h.DataSaida
                    }).ToList()
                });

            return historicosNavegacao;
        }

        public BloqueioEtapaSolicitacaoVO BuscarBloqueiosEtapaSolicitacao(long seqSolicitacaoServico, long seqSolicitacaoServicoEtapa)
        {
            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var result = this.SearchProjectionByKey(specSolicitacaoServico, s => new
            {
                SeqEtapaSgf = s.Etapas.FirstOrDefault(e => e.Seq == seqSolicitacaoServicoEtapa).ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqPessoaAtuacao = s.PessoaAtuacao.Seq,
                SeqConfiguracaoEtapa = s.Etapas.FirstOrDefault(e => e.Seq == seqSolicitacaoServicoEtapa).SeqConfiguracaoEtapa,
            });

            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(result.SeqTemplateProcessoSGF);
            var etapaAtualSGF = etapasSGF.FirstOrDefault(e => e.Seq == result.SeqEtapaSgf);

            var bloqueios = this.PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(result.SeqPessoaAtuacao, result.SeqConfiguracaoEtapa, true);
            var bloqueiosQueNaoImpemdeFim = this.PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(result.SeqPessoaAtuacao, result.SeqConfiguracaoEtapa, false);

            foreach (var bloqueioInicio in bloqueiosQueNaoImpemdeFim)
            {
                var bloqueioJaExiste = bloqueios.Where(c => c.Seq == bloqueioInicio.Seq).FirstOrDefault();

                if (bloqueioJaExiste == null)
                    bloqueios.Add(bloqueioInicio);
            }

            var bloqueiosEtapa = this.ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(result.SeqConfiguracaoEtapa), x => x.ConfiguracoesBloqueio)
                                     .Where(c => bloqueios.Any(d => d.SeqMotivoBloqueio == c.SeqMotivoBloqueio)).ToList();

            var retorno = new BloqueioEtapaSolicitacaoVO()
            {
                Etapa = etapaAtualSGF.Descricao,
                Bloqueios = bloqueios.Select(b => new BloqueioEtapaSolicitacaoItemVO()
                {
                    Seq = b.Seq,
                    TipoBloqueio = b.DescricaoTipoBloqueio,
                    MotivoBloqueio = b.MotivoBloqueio.Descricao,
                    Referente = b.DescricaoReferenciaAtuacao,
                    Bloqueio = b.Descricao,
                    SituacaoBloqueio = b.SituacaoBloqueio,
                    DescricaoSituacaoBloqueio = SMCEnumHelper.GetDescription(b.SituacaoBloqueio),
                    DataBloqueio = b.DataBloqueio,
                    TipoDesbloqueio = b.TipoDesbloqueio,
                    FormaDesbloqueioMotivo = b.MotivoBloqueio.FormaDesbloqueio,
                    TokenPermissaoDesbloqueio = b.MotivoBloqueio.TokenPermissaoDesbloqueio,
                    BloqueioImpedeInicioEtapa = bloqueiosEtapa.Where(c => c.SeqMotivoBloqueio == b.SeqMotivoBloqueio).FirstOrDefault().ImpedeInicioEtapa,
                    BloqueioImpedeFimEtapa = bloqueiosEtapa.Where(c => c.SeqMotivoBloqueio == b.SeqMotivoBloqueio).FirstOrDefault().ImpedeFimEtapa,
                }).ToList()
            };
            retorno.Bloqueios.SMCForEach(f => f.HabilitaBotaoDesbloqueio = !string.IsNullOrEmpty(f.TokenPermissaoDesbloqueio) && SMCSecurityHelper.Authorize(f.TokenPermissaoDesbloqueio));

            return retorno;
        }

        public DetalheNotificacaoSolicitacaoVO BuscarDetalheNotificacaoSolicitacao(long seqNotificacaoEmail, long? seqTipoNotificacao = null, long? seqSolicitacaoServico = null)
        {
            var notificacao = this.NotificacaoService.ConsultaNotificacao(seqNotificacaoEmail);
            var retorno = notificacao.Transform<DetalheNotificacaoSolicitacaoVO>();

            if (seqTipoNotificacao.HasValue)
            {
                var tipoNotificacao = this.TipoNotificacaoDomainService.BuscarTipoNotificacao(seqTipoNotificacao.Value);
                retorno.TokenTipoNotificacao = tipoNotificacao.Token;
                retorno.PermiteReenvio = tipoNotificacao.PermiteReenvio;

                var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico.Value), x => new
                {
                    Emails = x.PessoaAtuacao.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(d => d.EnderecoEletronico.Descricao).ToList(),
                    SeqPessoaAtuacao = x.PessoaAtuacao.Seq,
                    x.PessoaAtuacao.TipoAtuacao
                });

                switch (retorno.PermiteReenvio)
                {
                    case PermiteReenvio.ReenvioParaSolicitante:
                        retorno.TemEmail = dadosSolicitacao.Emails.Any();
                        break;

                    case PermiteReenvio.ReenvioParaOrientadorSolicitante:
                        if (dadosSolicitacao.TipoAtuacao == TipoAtuacao.Aluno)
                        {
                            var spec = new OrientacaoFilterSpecification() { SeqPessoaAtuacao = dadosSolicitacao.SeqPessoaAtuacao, TokenTipoOrientacao = TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO };
                            var result = OrientacaoDomainService.SearchByKey(spec, d => d.OrientacoesColaborador[0].Colaborador.EnderecosEletronicos[0].EnderecoEletronico);

                            retorno.TemEmail = RetornaEmailsValidos(result).Any();
                        }
                        else if (dadosSolicitacao.TipoAtuacao == TipoAtuacao.Ingressante)
                        {
                            var spec = new OrientacaoFilterSpecification() { SeqPessoaAtuacao = dadosSolicitacao.SeqPessoaAtuacao };
                            var result = OrientacaoDomainService.SearchByKey(spec, d => d.OrientacoesColaborador[0].Colaborador.EnderecosEletronicos[0].EnderecoEletronico);

                            retorno.TemEmail = RetornaEmailsValidos(result).Any();
                        }
                        break;

                    case PermiteReenvio.ReenvioParaDestinatarioConfiguracao:
                        var specSolicitacaoServicoEnvioNotificacao = new SolicitacaoServicoEnvioNotificacaoFilterSpecification()
                        {
                            SeqSolicitacaoServico = seqSolicitacaoServico,
                            SeqNotificacaoEmailDestinatario = seqNotificacaoEmail
                        };
                        var solicitacaoServicoEnvioNotificacao = SolicitacaoServicoEnvioNotificacaoDomainService.SearchByKey(specSolicitacaoServicoEnvioNotificacao, x => x.ProcessoEtapaConfiguracaoNotificacao);

                        var configuracaoNotificacaoEmail = this.NotificacaoService.BuscarConfiguracaoNotificacaoEmail(solicitacaoServicoEnvioNotificacao.ProcessoEtapaConfiguracaoNotificacao.SeqConfiguracaoTipoNotificacao);
                        retorno.TemEmail = configuracaoNotificacaoEmail.Emails.Any();
                        break;
                }
            }

            retorno.PermiteReenvio = retorno.PermiteReenvio != null ? retorno.PermiteReenvio : PermiteReenvio.NaoPermiteReenvio;

            return retorno;
        }

        public void EnviarNotificacaoSolicitacao(long seqSolicitacaoServico, string tokenTipoNotificacao = null)
        {
            using (var transacao = SMCUnitOfWork.Begin())
            {
                long seqProcessoEtapa = this.SearchProjectionByKey(seqSolicitacaoServico, p => p.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa);

                var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
                {
                    DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                    x.NumeroProtocolo,
                    NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                    NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                    x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(f => f.SeqProcessoEtapa == seqProcessoEtapa).ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(f => f.SeqProcessoEtapa == seqProcessoEtapa).TipoNotificacao.Token,
                    Emails = x.PessoaAtuacao.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(d => d.EnderecoEletronico.Descricao).ToList(),
                });

                string tokenTipoNotificacaoEnviar = dadosSolicitacao.Token;
                if (!string.IsNullOrEmpty(tokenTipoNotificacao))
                {
                    //Considerando que envio e reenvio vai utilizar o mesmo método
                    //Se for reenvio de notificação, não vai pegar o primeiro token, e sim o que o usuário selecionou para reenvio
                    //E garante também que pegará a configuração correta se tiver configurações para mais de um tipo de notificação
                    tokenTipoNotificacaoEnviar = tokenTipoNotificacao;
                }

                // Monta os dados para merge de envio de notificações
                var dadosMerge = new Dictionary<string, string>
                {
                    { TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dadosSolicitacao.NomeSocialSolicitante) ? dadosSolicitacao.NomeSolicitante : string.Format("{0} ({1})", dadosSolicitacao.NomeSocialSolicitante, dadosSolicitacao.NomeSolicitante) },
                    { TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dadosSolicitacao.DescricaoProcesso },
                    { TOKEN_TAG_NOTIFICACAO.NUM_PROTOCOLO, dadosSolicitacao.NumeroProtocolo }
                };

                // Envia a notificação de CRIAÇÃO DA SOLICITAÇÃO NO PORTAL
                var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    TokenNotificacao = tokenTipoNotificacaoEnviar,
                    DadosMerge = dadosMerge,
                    EnvioSolicitante = false,
                    ConfiguracaoPrimeiraEtapa = false,
                    Destinatarios = dadosSolicitacao.Emails
                };

                SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);

                transacao.Commit();
            }
        }

        public void ReenviarNotificacaoSolicitacao(long seqSolicitacaoServico, long seqNotificacaoEmailDestinatario, PermiteReenvio permiteReenvio)
        {
            using (var transacao = SMCUnitOfWork.Begin())
            {
                var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
                {
                    DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                    x.NumeroProtocolo,
                    NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                    NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                    Emails = x.PessoaAtuacao.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(d => d.EnderecoEletronico.Descricao).ToList(),
                    SeqPessoaAtuacao = x.PessoaAtuacao.Seq,
                    x.PessoaAtuacao.TipoAtuacao
                });

                var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    EnvioSolicitante = false,
                    ConfiguracaoPrimeiraEtapa = false
                };

                switch (permiteReenvio)
                {
                    case PermiteReenvio.ReenvioParaSolicitante:
                        parametros.Destinatarios = dadosSolicitacao.Emails;
                        break;

                    case PermiteReenvio.ReenvioParaOrientadorSolicitante:
                        if (dadosSolicitacao.TipoAtuacao == TipoAtuacao.Aluno)
                        {
                            var spec = new OrientacaoFilterSpecification() { SeqPessoaAtuacao = dadosSolicitacao.SeqPessoaAtuacao, TokenTipoOrientacao = TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO };
                            var result = OrientacaoDomainService.SearchByKey(spec, d => d.OrientacoesColaborador[0].Colaborador.EnderecosEletronicos[0].EnderecoEletronico);

                            parametros.Destinatarios = RetornaEmailsValidos(result);
                        }
                        else if (dadosSolicitacao.TipoAtuacao == TipoAtuacao.Ingressante)
                        {
                            var spec = new OrientacaoFilterSpecification() { SeqPessoaAtuacao = dadosSolicitacao.SeqPessoaAtuacao };
                            var result = OrientacaoDomainService.SearchByKey(spec, d => d.OrientacoesColaborador[0].Colaborador.EnderecosEletronicos[0].EnderecoEletronico);

                            parametros.Destinatarios = RetornaEmailsValidos(result);
                        }
                        break;

                    default:
                        break;
                }

                SolicitacaoServicoEnvioNotificacaoDomainService.ReenviarNotificacaoSolicitacaoServico(seqSolicitacaoServico, seqNotificacaoEmailDestinatario, parametros);

                transacao.Commit();
            }
        }

        private List<string> RetornaEmailsValidos(Orientacao result)
        {
            var emails = new List<string>();
            foreach (var item in result.OrientacoesColaborador)
            {
                if (!item.DataFimOrientacao.HasValue || (item.DataFimOrientacao.HasValue && DateTime.Now <= item.DataFimOrientacao))
                    emails = result.OrientacoesColaborador.SelectMany(sm => sm.Colaborador.EnderecosEletronicos.Select(s => s.EnderecoEletronico.Descricao)).ToList();
            }
            return emails;
        }

        public DadosSolicitacaoVO PrepararModeloSolicitacaoServico(long seqSolicitacaoServico)
        {
            // Desconsidera filtro de dados para esta consulta
            this.PessoaAtuacaoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            this.PlanoEstudoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var resultPrimario = this.SearchProjectionByKey(spec, s => new DadosSolicitacaoVO()
            {
                SeqSolicitacaoServico = s.Seq,
                SeqServico = s.ConfiguracaoProcesso.Processo.SeqServico,
                SeqPessoaAtuacao = s.PessoaAtuacao.Seq,
                SeqConfiguracaoEtapa = s.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa
            });

            var specSolicitacaoTaxas = new SolicitacaoServicoBoletoTaxaFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
            var taxasSolicitacaoServico = this.SolicitacaoServicoBoletoTaxaDomainService.SearchBySpecification(specSolicitacaoTaxas).ToList();
            resultPrimario.SolicitacaoPossuiTaxas = taxasSolicitacaoServico.Any();

            this.PessoaAtuacaoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            this.PlanoEstudoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return resultPrimario;
        }

        public long RetornarSolicitacaoParaEtapaAnterior(long seqSolicitacaoServico)
        {
            // Recupera a solicitação de matrícula pelo sequencial caso o orientador seja o próprio usuário
            var dadosBasicosSolicitacao = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                SeqTemplateProcessoSgf = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqSolicitacaoServicoEtapaAtual = x.SituacaoAtual.SeqSolicitacaoServicoEtapa,
                SeqConfiguracaoEtapaAtual = x.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                SeqEtapaAtualSGF = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                SeqHistoricoSituacaoAtual = x.SituacaoAtual.Seq,
                SeqSituacaoEtapaSGFAtual = x.SituacaoAtual.SeqSituacaoEtapaSgf,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                TokenTipoServico = x.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token,
                Documentos = x.DocumentosAcademicos.Select(d => new { DocumentoConclusao = (d as DocumentoConclusao) })
            });

            // Busca as etapas deste template
            var etapas = SGFHelper.BuscarEtapasSGFCache(dadosBasicosSolicitacao.SeqTemplateProcessoSgf);

            // Recupera a etapa atual
            var etapaAtual = etapas.FirstOrDefault(e => e.Seq == dadosBasicosSolicitacao.SeqEtapaAtualSGF);

            // Recupera qual é a etapa anterior
            var etapaAnterior = etapas.Where(e => e.Ordem < etapaAtual.Ordem).OrderByDescending(e => e.Ordem).FirstOrDefault();

            // Caso seja Emissão de Documento e etapa anterior igual a Assinatura e Registro, retornar a etapa anterior à etapa de Assinatura e Registro
            if (dadosBasicosSolicitacao.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO && etapaAnterior?.Token == TOKENS_ETAPA_SGF.ASSINATURA_REGISTRO_DOCUMENTO)
            {
                // Etapa anterior passa a ser a anterior da Assinatura e Registro
                etapaAnterior = etapas.Where(e => e.Ordem < etapaAtual.Ordem).OrderByDescending(e => e.Ordem).Skip(1).FirstOrDefault();
            }

            // Caso tenha etapa anterior
            if (etapaAnterior != null)
            {
                // Verifica se a situação atual é inicial da etapa atual
                var situacaoInicialEtapaAtual = etapaAtual.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa);
                if (dadosBasicosSolicitacao.SeqSituacaoEtapaSGFAtual != situacaoInicialEtapaAtual.Seq)
                    throw new SMCApplicationException("É possível voltar a solicitação para etapa anterior apenas se a mesma estiver na situação inicial da etapa corrente.");

                // Recupera qual é o SeqSolicitacaoServicoEtapaAnterior
                var dadosEtapaAnterior = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
                {
                    SeqSolicitacaoServicoEtapaAnterior = x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf == etapaAnterior.Seq).Seq,
                    SeqConfiguracaoEtapaAnterior = x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf == etapaAnterior.Seq).SeqConfiguracaoEtapa,
                    SeqConfiguracaoEtapaPaginaAnterior = x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf == etapaAnterior.Seq).ConfiguracaoEtapa.ConfiguracoesPagina.OrderBy(o => o.Ordem).FirstOrDefault().Seq,
                });

                // Valida a data de vigência da etapa Atual
                if (!VerificarVigenciaEtapa(seqSolicitacaoServico, dadosBasicosSolicitacao.SeqConfiguracaoEtapaAtual))
                    throw new SMCApplicationException("Etapa atual não está vigente. Para voltar à etapa anterior, é necessário que a etapa atual esteja vigente.");

                // Valida a data de vigência da etapa anterior
                if (!VerificarVigenciaEtapa(seqSolicitacaoServico, dadosEtapaAnterior.SeqConfiguracaoEtapaAnterior))
                    throw new SMCApplicationException("Etapa anterior não está vigente. Para voltar à etapa anterior, é necessário que a etapa anterior esteja vigente.");

                //Caso Token Serviço = SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU e Token Etapa Anterior = CHANCELA_PLANO_ESTUDO
                //OU   Token Serviço = SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU e Token Etapa Anterior = CHANCELA_RENOVACAO_MATRICULA
                //OU   Token Serviço = REABERTURA_MATRICULA e Token Etapa Anterior = CHANCELA_RENOVACAO_MATRICULA
                //Executar a regra: RN_MAT_052 - Reabrir chancela
                if (
                     (dadosBasicosSolicitacao.TokenServico == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU &&
                      etapaAnterior.Token == MatriculaTokens.CHANCELA_PLANO_ESTUDO) ||

                     (dadosBasicosSolicitacao.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU &&
                      etapaAnterior.Token == MatriculaTokens.CHANCELA_RENOVACAO_MATRICULA) ||

                     (dadosBasicosSolicitacao.TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA &&
                      etapaAnterior.Token == MatriculaTokens.CHANCELA_RENOVACAO_MATRICULA)
                   )
                {
                    using (var unitOfWork = SMCUnitOfWork.Begin())
                    {
                        try
                        {
                            // Atualiza a data de exclusão da situação atual
                            SolicitacaoHistoricoSituacaoDomainService.UpdateFields(new SolicitacaoHistoricoSituacao { Seq = dadosBasicosSolicitacao.SeqHistoricoSituacaoAtual, DataExclusao = DateTime.Now }, x => x.DataExclusao);

                            // Recupera a situação inicial da primeira página da etapa anterior
                            var seqSituacaoEtapaInicial = etapaAnterior.Paginas.OrderBy(o => o.Ordem).First().SeqSituacaoEtapaInicial.GetValueOrDefault();

                            // Recupera a situação correspondente a situação inicial da página da etapa anterior
                            var seqSituacaoEtapaAnterior = etapaAnterior.Situacoes.FirstOrDefault(s => s.Seq == seqSituacaoEtapaInicial).Seq;

                            // Atualiza o histórico informando que está na situação configurada para a etapa anterior
                            SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(dadosEtapaAnterior.SeqSolicitacaoServicoEtapaAnterior, seqSituacaoEtapaAnterior, "Chancela reaberta");

                            unitOfWork.Commit();
                        }
                        catch (Exception)
                        {
                            unitOfWork.Rollback();
                            throw;
                        }
                    }

                    return dadosEtapaAnterior.SeqConfiguracaoEtapaAnterior;
                }
                else if (dadosBasicosSolicitacao.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO)
                {
                    using (var unitOfWork = SMCUnitOfWork.Begin())
                    {
                        try
                        {
                            // 1. Inserir no histórico de situação da solicitação, a situação inicial da etapa que será retornada
                            // Recupera a situação inicial da primeira página da etapa anterior
                            var seqSituacaoEtapaInicial = etapaAnterior.Paginas.OrderBy(o => o.Ordem).First().SeqSituacaoEtapaInicial.GetValueOrDefault();

                            //Recupera a situação correspondente a situação inicial da página da etapa anterior
                            var seqSituacaoEtapaAnterior = etapaAnterior.Situacoes.FirstOrDefault(s => s.Seq == seqSituacaoEtapaInicial).Seq;

                            // Atualiza o histórico informando que está na situação configurada para a etapa anterior
                            SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(dadosEtapaAnterior.SeqSolicitacaoServicoEtapaAnterior, seqSituacaoEtapaAnterior, null);

                            // 2. Inserir no histórico de navegação de páginas, a primeira página da etapa que será retornada, de acordo com o número de ordem
                            var solicitacaoHistoricoNavegacao = new SolicitacaoHistoricoNavegacao()
                            {
                                DataEntrada = DateTime.Now,
                                SeqSolicitacaoServicoEtapa = dadosEtapaAnterior.SeqSolicitacaoServicoEtapaAnterior,
                                SeqConfiguracaoEtapaPagina = dadosEtapaAnterior.SeqConfiguracaoEtapaPaginaAnterior
                            };

                            SolicitacaoHistoricoNavegacaoDomainService.SaveEntity(solicitacaoHistoricoNavegacao);

                            /*
                              3. Inserir no histórico de situação do documento de conclusão em questão a situação de acordo com a etapa que será retornada, sendo que:
                                - Etapa que será retornada = EMISSAO_DOCUMENTO, então situação do documento = AGUARDANDO_ATUALIZACAO_DOCUMENTO
                                - Etapa que será retornada = IMPRESSAO_DOCUMENTO, então situação do documento = AGUARDANDO_IMPRESSAO
                            */
                            var specSituacaoDocumentoAcademico = new SituacaoDocumentoAcademicoFilterSpecification();

                            switch (etapaAnterior.Token)
                            {
                                case TOKENS_ETAPA_SGF.EMISSAO_DOCUMENTO:
                                    specSituacaoDocumentoAcademico.Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ATUALIZACAO_DOCUMENTO;
                                    break;

                                case TOKENS_ETAPA_SGF.IMPRESSAO_DOCUMENTO:
                                    specSituacaoDocumentoAcademico.Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_IMPRESSAO;
                                    break;

                                default:
                                    break;
                            }

                            // Recupera qual o sequencial da situação do documento de conclusão para ser inserido no histórico
                            var seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoDocumentoAcademico, s => s.Seq).FirstOrDefault();

                            // Para cada documento de conclusão
                            foreach (var documento in dadosBasicosSolicitacao.Documentos)
                            {
                                // Insere no histórico de situação do documento de conclusão
                                var documentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                                {
                                    SeqDocumentoAcademico = documento.DocumentoConclusao.Seq,
                                    SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico
                                };

                                DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(documentoAcademicoHistoricoSituacao);

                                //4. Se a etapa que será retornada = IMPRESSAO_DOCUMENTO:
                                if (etapaAnterior.Token == TOKENS_ETAPA_SGF.IMPRESSAO_DOCUMENTO)
                                {
                                    /*4.2. Atualizar as informações sobre impressão do documento de conclusão:

                                            ID Dados Pessoais = nulo
                                            Data Impressão = nulo
                                            Usuário Impressão = nulo
                                            Número de Série = nulo
                                    */

                                    documento.DocumentoConclusao.SeqPessoaDadosPessoais = null;
                                    documento.DocumentoConclusao.DataImpressao = null;
                                    documento.DocumentoConclusao.UsuarioImpressao = null;
                                    documento.DocumentoConclusao.NumeroSerie = null;

                                    DocumentoConclusaoDomainService.UpdateFields(documento.DocumentoConclusao, d => d.SeqPessoaDadosPessoais, d => d.DataImpressao, d => d.UsuarioImpressao, d => d.NumeroSerie);
                                }
                            }

                            unitOfWork.Commit();
                        }
                        catch (Exception)
                        {
                            unitOfWork.Rollback();
                            throw;
                        }
                    }

                    return dadosEtapaAnterior.SeqConfiguracaoEtapaAnterior;
                }
                else
                {
                    throw new SMCApplicationException("Operação não permitida. O token do serviço, o token do tipo de serviço ou o token da etapa anterior não permitem retornar a solicitação para a etapa anterior. ");
                }
            }

            return dadosBasicosSolicitacao.SeqConfiguracaoEtapaAtual;
        }

        public CabecalhoCancelamentoSolicitacaoVO BuscarDadosCabecalhoCancelamentoSolicitacao(long seqSolicitacaoServico)
        {
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var result = this.SearchProjectionByKey(spec, s => new
            {
                Processo = s.ConfiguracaoProcesso.Processo.Descricao,
                DescricaoServico = s.ConfiguracaoProcesso.Processo.Servico.Descricao,
                Protocolo = s.NumeroProtocolo,
                DataSolicitacao = s.DataSolicitacao,
                SeqSolicitacaoServico = s.Seq,
                Nome = s.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = s.PessoaAtuacao.DadosPessoais.NomeSocial,
                SeqConfiguracaoEtapa = s.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                SeqSituacaoEtapaSGF = s.SituacaoAtual.SeqSituacaoEtapaSgf,
            });

            var situacaoAtual = EtapaService.BuscarSituacaoEtapa(result.SeqSituacaoEtapaSGF)?.Situacao;

            if (situacaoAtual == null)
                throw new SMCApplicationException("Não foi possível identificar a configuração da situação atual da etapa corrente desta solicitação.");

            var retorno = new CabecalhoCancelamentoSolicitacaoVO()
            {
                EtapaAtual = SGFHelper.BuscarEtapa(result.SeqSolicitacaoServico, result.SeqConfiguracaoEtapa).DescricaoEtapa,
                Nome = result.Nome,
                NomeSocial = result.NomeSocial,
                Processo = result.Processo,
                Protocolo = result.Protocolo,
                SeqSolicitacaoServico = result.SeqSolicitacaoServico,
                SituacaoAtual = situacaoAtual.Descricao,
                DescricaoServico = result.DescricaoServico,
                DataSolicitacao = result.DataSolicitacao
            };

            return retorno;
        }

        public CabecalhoReaberturaSolicitacaoVO BuscarDadosCabecalhoReaberturaSolicitacao(long seqSolicitacaoServico)
        {
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var resultPrimario = this.SearchProjectionByKey(spec, s => new
            {
                Processo = s.ConfiguracaoProcesso.Processo.Descricao,
                Protocolo = s.NumeroProtocolo,
                SeqSolicitacaoServico = s.Seq,
                Nome = s.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = s.PessoaAtuacao.DadosPessoais.NomeSocial,
                SeqConfiguracaoEtapa = s.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                SeqSituacaoEtapaSGF = s.SituacaoAtual.SeqSituacaoEtapaSgf,
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqEtapaAtualSgf = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
            });

            var situacoesEtapaSgf = SGFHelper.BuscarEtapasSGFCache(resultPrimario.SeqTemplateProcessoSGF);

            var situacaoEtapaSgf = situacoesEtapaSgf.FirstOrDefault(e => e.Seq == resultPrimario.SeqEtapaAtualSgf).Situacoes
                                              .FirstOrDefault(s => s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);

            var resultSecundario = this.SearchProjectionByKey(spec, s => new
            {
                DataHoraEncerramento = (DateTime?)s.SituacaoAtual.DataInclusao
            });

            var retorno = new CabecalhoReaberturaSolicitacaoVO()
            {
                EtapaAtual = SGFHelper.BuscarEtapa(resultPrimario.SeqSolicitacaoServico, resultPrimario.SeqConfiguracaoEtapa).DescricaoEtapa,
                Nome = resultPrimario.Nome,
                NomeSocial = resultPrimario.NomeSocial,
                Processo = resultPrimario.Processo,
                Protocolo = resultPrimario.Protocolo,
                SeqSolicitacaoServico = resultPrimario.SeqSolicitacaoServico,
                SituacaoAtual = EtapaService.BuscarSituacaoEtapa(resultPrimario.SeqSituacaoEtapaSGF).Situacao.Descricao,
                DataHoraEncerramento = resultSecundario.DataHoraEncerramento
            };

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarSituacoesCancelamentoSolicitacaoSelect(long seqSolicitacaoServico)
        {
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var result = this.SearchProjectionByKey(spec, s => new
            {
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqEtapaAtualSgf = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
            });

            var situacoesEtapa = SGFHelper.BuscarEtapasSGFCache(result.SeqTemplateProcessoSGF);

            return situacoesEtapa.FirstOrDefault(e => e.Seq == result.SeqEtapaAtualSgf).Situacoes
                                 .Where(s => s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado)
                                 .Select(s => new SMCDatasourceItem()
                                 {
                                     Seq = s.SeqSituacao,
                                     Descricao = s.Descricao
                                 }).ToList();
        }

        /// <summary>
        /// Realiza o cancelamento de uma solicitação de serviço
        /// </summary>
        /// <param name="modelo"></param>
        public void SalvarCancelamentoSolicitacao(CancelamentoSolicitacaoVO modelo)
        {
            // Situações que devem ser consideradas par abuscar os itens
            List<ClassificacaoSituacaoFinal?> situacoesClassificacoes = new List<ClassificacaoSituacaoFinal?>
            {
                ClassificacaoSituacaoFinal.FinalizadoComSucesso,
                ClassificacaoSituacaoFinal.FinalizadoSemSucesso,
                ClassificacaoSituacaoFinal.Nenhum,
                null
            };

            // Busca os dados da solicitação de serviço a ser cancelada
            var spec = new SMCSeqSpecification<SolicitacaoServico>(modelo.SeqSolicitacaoServico);
            var dados = SearchProjectionByKey(spec, x => new
            {
                NumeroProtocolo = x.NumeroProtocolo,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                TokenTipoServico = x.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token,
                SeqTemplateProcessoSGF = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqEtapaSgf = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                SeqSolicitacaoServicoEtapa = x.SituacaoAtual.SeqSolicitacaoServicoEtapa,
                SeqProcessoEtapa = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa,
                SeqSolicitacaoServico = x.Seq,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                //SeqsSolicitacaoMatriculaItens = (x as SolicitacaoMatricula).Itens.Where(i => i.HistoricosSituacao.OrderByDescending(a => a.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.FinalizadoSemSucesso && i.HistoricosSituacao.OrderByDescending(a => a.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.Cancelado).Select(w => w.Seq)
                SolicitacaoMatriculaItens = (x as SolicitacaoMatricula).Itens.Where(i => situacoesClassificacoes.Contains(i.HistoricosSituacao.OrderByDescending(a => a.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal)).Select(w =>
                new
                {
                    Seq = w.Seq,
                    ClassificacaoSituacaoFinal = w.HistoricosSituacao.OrderByDescending(a => a.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal
                }),
                DocumentosConclusao = x.DocumentosAcademicos.Select(s => new
                {
                    s.Seq,
                    s.SeqDocumentoGAD,
                    TokenTipoDocumentoAcademico = s.TipoDocumentoAcademico.Token
                })
            });

            // Busca a situação cancelada para a etapa atual da solicitação
            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(dados.SeqTemplateProcessoSGF);
            var situacaoCancelado = etapasSGF.FirstOrDefault(e => e.Seq == dados.SeqEtapaSgf).Situacoes.FirstOrDefault(s => s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado);

            // Verifica se informou o motivo de cancelamento corretamente
            if (modelo.SeqMotivoCancelamento.HasValue)
            {
                if (situacaoCancelado.MotivosSituacao.Where(m => m.Seq == modelo.SeqMotivoCancelamento.Value).FirstOrDefault() == null)
                    throw new MotivoCancelamentoSolicitacaoInvalidoException(string.Empty);
            }
            else
            {
                // Não informou o motivo, mas informou o token do motivo
                if (!string.IsNullOrEmpty(modelo.TokenMotivoCancelamento))
                {
                    var motivo = situacaoCancelado.MotivosSituacao.Where(m => m.Token == modelo.TokenMotivoCancelamento).FirstOrDefault();
                    if (motivo == null)
                        throw new MotivoCancelamentoSolicitacaoInvalidoException(modelo.TokenMotivoCancelamento);
                    else
                        modelo.SeqMotivoCancelamento = motivo.Seq;
                }
            }

            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    // Cria um histórico de situação cancelada para a solicitação
                    SolicitacaoHistoricoSituacao novoHistoricoSituacao = new SolicitacaoHistoricoSituacao
                    {
                        CategoriaSituacao = situacaoCancelado.CategoriaSituacao,
                        SeqSituacaoEtapaSgf = situacaoCancelado.Seq,
                        SeqSolicitacaoServicoEtapa = dados.SeqSolicitacaoServicoEtapa,
                        Observacao = modelo.Observacao,
                        SeqMotivoSituacaoSgf = modelo.SeqMotivoCancelamento
                    };

                    // Salva o novo histórico
                    SolicitacaoHistoricoSituacaoDomainService.SaveEntity(novoHistoricoSituacao);

                    switch (dados.TokenServico)
                    {
                        case TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU:
                        case TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA:

                            //Chama a RN_SRC_076 - Rotina de cancelamento - Atualização do solicitante/ingressante
                            ProcessoEtapaDomainService.AtualizaDadosIngressante(dados.SeqProcessoEtapa, dados.SeqSolicitacaoServico, SGFConstants.CONVOCADO_DESISTENTE, SGFConstants.CONVOCADO_DESISTENTE_CANCELADO_PELA_CENTRAL_SOLICITACOES, "Situação da oferta de inscrição atualizada automaticamente devido ao cancelamento da solicitação de matrícula no Sistema Acadêmico");

                            foreach (var solicitacaoMatriculaItem in dados.SolicitacaoMatriculaItens)
                            {
                                //Chama a RN_MAT_035 - Ocupar / desocupar vaga processo matrícula.
                                if (solicitacaoMatriculaItem.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.FinalizadoSemSucesso)
                                    SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(solicitacaoMatriculaItem.Seq);

                                //Altera a situação dos itens de matrícula
                                SolicitacaoMatriculaItemDomainService.AlterarSolicitacaoMatriculaItemParaCancelado(solicitacaoMatriculaItem.Seq, dados.SeqProcessoEtapa, Common.Areas.MAT.Enums.MotivoSituacaoMatricula.SolicitacaoCancelada);
                            }

                            break;

                        case TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU:
                        case TOKEN_SERVICO.MATRICULA_REABERTURA:

                            foreach (var solicitacaoMatriculaItem in dados.SolicitacaoMatriculaItens)
                            {
                                //Chama a RN_MAT_035 - Ocupar / desocupar vaga processo matrícula.
                                if (solicitacaoMatriculaItem.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.FinalizadoSemSucesso)
                                    SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(solicitacaoMatriculaItem.Seq);

                                //Altera a situação dos itens de matrícula
                                SolicitacaoMatriculaItemDomainService.AlterarSolicitacaoMatriculaItemParaCancelado(solicitacaoMatriculaItem.Seq, dados.SeqProcessoEtapa, Common.Areas.MAT.Enums.MotivoSituacaoMatricula.SolicitacaoCancelada);
                            }

                            break;

                        case TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO:
                        case TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA:
                        case TOKEN_SERVICO.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA:

                            foreach (var solicitacaoMatriculaItem in dados.SolicitacaoMatriculaItens)
                            {
                                //Altera a situação dos itens de matrícula
                                SolicitacaoMatriculaItemDomainService.AlterarSolicitacaoMatriculaItemParaCancelado(solicitacaoMatriculaItem.Seq, dados.SeqProcessoEtapa, Common.Areas.MAT.Enums.MotivoSituacaoMatricula.SolicitacaoCancelada);
                            }

                            break;

                        case TOKEN_SERVICO.EMISSAO_HISTORICO_ESCOLAR_DIGITAL:
                        case TOKEN_SERVICO.EMISSAO_DIPLOMA_DIGITAL_1_VIA:

                            //Se o nível-ensino do aluno for igual a Graduação, deverá ser acionada a rotina do SGA
                            //st_altera_situacao_servico_diploma_digital para cancelar o serviço (tramite-serviço)
                            //correspondente à solicitação-serviço do Acadêmico.
                            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dados.SeqPessoaAtuacao);
                            var tokenNivelEnsino = NivelEnsinoDomainService.SearchProjectionByKey(new SMCSeqSpecification<NivelEnsino>(dadosOrigem.SeqNivelEnsino), x => x.Token);

                            if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
                            {
                                var usuarioOperacao = UsuarioLogado.RetornaUsuarioLogado();
                                IntegracaoAcademicoService.CancelarSolicitacaoDiplomaDigital(dados.NumeroProtocolo, usuarioOperacao);
                            }

                            //Atualizar o bloqueio igual a ENTREGA_DIPLOMA_DANIFICADO se a situação for igual a Bloqueado
                            var bloqueiosEntregaDiplomaDanificado = PessoaAtuacaoBloqueioDomainService.SearchBySpecification(new PessoaAtuacaoBloqueioFilterSpecification
                            {
                                SeqSolicitacaoServico = dados.SeqSolicitacaoServico,
                                SeqPessoaAtuacao = dados.SeqPessoaAtuacao,
                                TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.ENTREGA_DIPLOMA_DANIFICADO,
                                SituacaoBloqueio = SituacaoBloqueio.Bloqueado
                            }).ToList();

                            foreach (var bloqueio in bloqueiosEntregaDiplomaDanificado)
                            {
                                bloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                bloqueio.DataDesbloqueioEfetivo = DateTime.Now;
                                bloqueio.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                                bloqueio.JustificativaDesbloqueio = JUSTIFICATIVA_DESBLOQUEIO.SOLICITACAO_SERVICO_CANCELADA;
                                bloqueio.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;

                                PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueio);
                            }

                            if (dados.DocumentosConclusao != null && dados.DocumentosConclusao.Any())
                            {
                                long seqSituacaoDocumentoAcademico = 0;

                                if (modelo.SeqSituacaoDocumentoAcademico.HasValue)
                                {
                                    seqSituacaoDocumentoAcademico = modelo.SeqSituacaoDocumentoAcademico.Value;
                                }
                                else
                                {
                                    //Recuperar a situacao do documento de conclusão = Anulado
                                    var specSituacaoDocumentoAcademico = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO };
                                    seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionBySpecification(specSituacaoDocumentoAcademico, s => s.Seq).FirstOrDefault();

                                    //Caso não encontre o sequencial, dispara mensagem de erro
                                    if (seqSituacaoDocumentoAcademico == 0)
                                        throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO);
                                }

                                //Se houver documento de conclusão associado a solicitação de serviço, inserir no histórico de situação
                                //do documento de conclusão, a situação com token igual a ANULADO e a Observação = observação informada
                                foreach (var documentoConclusao in dados.DocumentosConclusao)
                                {
                                    var novoDocumentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                                    {
                                        SeqDocumentoAcademico = documentoConclusao.Seq,
                                        SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico,
                                        Observacao = modelo.Observacao,
                                        TipoInvalidade = modelo.TipoCancelamento,
                                        SeqClassificacaoInvalidadeDocumento = modelo.SeqClassificacaoInvalidadeDocumento
                                    };

                                    if (modelo.MotivoInvalidadeDocumento.HasValue)
                                        novoDocumentoAcademicoHistoricoSituacao.MotivoInvalidadeDocumento = modelo.MotivoInvalidadeDocumento;
                                    else
                                        novoDocumentoAcademicoHistoricoSituacao.MotivoInvalidadeDocumento = MotivoInvalidadeDocumento.DadosInconsistentes;

                                    DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(novoDocumentoAcademicoHistoricoSituacao);

                                    //Se houver documento-gad associado ao respectivo documento-conclusão da solicitação-serviço,
                                    //o mesmo deverá ser anulado, conforme RN_DDG_002 - Cancelar diploma, passando como
                                    //parâmetro a observação informada.
                                    if (documentoConclusao.SeqDocumentoGAD.HasValue)
                                    {
                                        var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
                                        string observacoes = string.Empty;

                                        if (!string.IsNullOrEmpty(modelo.ObservacaoDocumentoGAD))
                                            observacoes = modelo.ObservacaoDocumentoGAD;
                                        else
                                            observacoes = modelo.Observacao;

                                        var modeloDocumentoAcademico = new AtualizaStatusDocumentoAcademicoVO
                                        {
                                            SeqDocumentoAcademico = documentoConclusao.SeqDocumentoGAD.Value,
                                            Observacao = observacoes,
                                            TipoCancelamento = modelo.TipoCancelamento.SMCGetDescription(),
                                            UsuarioInclusao = usuarioInclusao
                                        };

                                        if (documentoConclusao.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                                        {
                                            if (modelo.SeqClassificacaoInvalidadeDocumento.HasValue)
                                            {
                                                var descricaoXSD = ClassificacaoInvalidadeDocumentoDomainService.BuscarClassificacaoInvalidadeDocumentoXSD(modelo.SeqClassificacaoInvalidadeDocumento.Value);
                                                modeloDocumentoAcademico.MotivoCancelamento = descricaoXSD;
                                            }

                                            var retornoGAD = APIDiplomaGAD.Execute<object>("Cancelar", modeloDocumentoAcademico);
                                            var retornoCancelarDiploma = JsonConvert.DeserializeObject<MensagemHttp>(retornoGAD.ToString());

                                            if (!string.IsNullOrEmpty(retornoCancelarDiploma.ErrorMessage))
                                                throw new Exception(retornoCancelarDiploma.ErrorMessage);
                                        }
                                        else
                                        {
                                            var retornoGAD = APIHistoricoGAD.Execute<object>("Cancelar", modeloDocumentoAcademico);
                                            var retornoCancelarHistoricoEscolar = JsonConvert.DeserializeObject<MensagemHttp>(retornoGAD.ToString());

                                            if (!string.IsNullOrEmpty(retornoCancelarHistoricoEscolar.ErrorMessage))
                                                throw new Exception(retornoCancelarHistoricoEscolar.ErrorMessage);
                                        }
                                    }
                                }
                            }

                            break;
                    }

                    //Solicitações que possuem título financeiro e o tipo da situação é igual a "Em aberto"
                    var specTitulo = new SolicitacaoServicoBoletoTituloFilterSpecification() { SeqSolicitacaoServico = modelo.SeqSolicitacaoServico };
                    var titulosSolicitacaoServico = this.SolicitacaoServicoBoletoTituloDomainService.SearchBySpecification(specTitulo).Where(a => !a.DataCancelamento.HasValue).ToList();

                    foreach (var titulo in titulosSolicitacaoServico)
                    {
                        var dadosTitulo = this.IntegracaoFinanceiroService.BuscarDadosTitulo(titulo.SeqTituloGra);

                        if (dadosTitulo != null && dadosTitulo.SituacaoTitulo == SituacaoTitulo.EmAberto)
                        {
                            /*Cancelar os títulos associados na solicitação e que o tipo da situação é igual a "Em aberto" e os
                            respectivos títulos no sistema financeiro.*/
                            this.IntegracaoFinanceiroService.CancelarTitulo(new CancelarTituloData()
                            {
                                SeqTitulo = titulo.SeqTituloGra,
                                UsuarioOperacao = SMCContext.User.SMCGetNome()
                            });

                            titulo.DataCancelamento = DateTime.Now;
                            this.SolicitacaoServicoBoletoTituloDomainService.UpdateFields(titulo, p => p.DataCancelamento);

                            /*Desbloquear os respectivos bloqueios dos títulos financeiros que foram cancelados*/
                            var bloqueioTaxaServicoAcademico = PessoaAtuacaoBloqueioDomainService.SearchByKey(new PessoaAtuacaoBloqueioFilterSpecification
                            {
                                CodigoIntegracaoSistemaOrigem = titulo.SeqTituloGra.ToString(),
                                TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.PAGAMENTO_TAXA_SERVICO_ACADEMICO_EM_ABERTO,
                                SituacaoBloqueio = SituacaoBloqueio.Bloqueado
                            }, x => x.Itens);

                            if (bloqueioTaxaServicoAcademico != null)
                            {
                                bloqueioTaxaServicoAcademico.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                bloqueioTaxaServicoAcademico.DataDesbloqueioEfetivo = DateTime.Now;
                                bloqueioTaxaServicoAcademico.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                                bloqueioTaxaServicoAcademico.JustificativaDesbloqueio = JUSTIFICATIVA_DESBLOQUEIO.SOLICITACAO_SERVICO_CANCELADA;
                                bloqueioTaxaServicoAcademico.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;

                                foreach (var itemBloqueio in bloqueioTaxaServicoAcademico.Itens)
                                {
                                    itemBloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                    itemBloqueio.DataDesbloqueio = DateTime.Now;
                                    itemBloqueio.UsuarioDesbloqueio = SMCContext.User.Identity.Name;
                                }

                                PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueioTaxaServicoAcademico);
                            }
                        }
                    }

                    // Atualiza a descrição da solicitação após cancelar a mesma.
                    AtualizarDescricao(modelo.SeqSolicitacaoServico, false, true);

                    unityOfWork.Commit();
                }
                catch (Exception)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Realiza a reabertura de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço a ser reaberta</param>
        /// <param name="observacao">Observação da reabertura</param>
        public void ReabrirSolicitacao(long seqSolicitacaoServico, string observacao)
        {
            // Busca os dados da solicitação para reabertura
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);
            var dadosSolicitacao = this.SearchProjectionByKey(spec, s => new
            {
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                NomeSocialSolicitante = s.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = s.PessoaAtuacao.DadosPessoais.Nome,
                NumeroProtocolo = s.NumeroProtocolo,
                DescricaoProcesso = s.ConfiguracaoProcesso.Processo.Descricao,
                TokenServico = s.ConfiguracaoProcesso.Processo.Servico.Token,
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqSolicitacaoServicoEtapa = s.SituacaoAtual.SeqSolicitacaoServicoEtapa,
                SeqEtapaAtualSGF = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                SeqConfiguracaoEtapaPaginaAtual = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ConfiguracoesPagina.OrderBy(o => o.Ordem).FirstOrDefault().Seq,
                TokenTipoServico = s.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token,
                DocumentosRequeridos = s.DocumentosRequeridos
            });

            // Busca a situação inicial da etapa atual da solicitação
            var dadosEtapasSGF = SGFHelper.BuscarEtapasSGFCache(dadosSolicitacao.SeqTemplateProcessoSGF)?.OrderBy(e => e.Ordem);
            var etapaAtualSGF = dadosEtapasSGF.FirstOrDefault(d => d.Seq == dadosSolicitacao.SeqEtapaAtualSGF);
            var situacaoInicialEtapaAtual = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa);

            // Busca os dados de origem da pessoa
            var dadosPessoa = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacao.SeqPessoaAtuacao);

            // Inicia a transação
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                try
                {
                    // Inclui nova situação (inicial da etapa) para a solicitação
                    var solicitacaoHistoricoSituacao = new SolicitacaoHistoricoSituacao()
                    {
                        Observacao = observacao,
                        SeqSituacaoEtapaSgf = situacaoInicialEtapaAtual.Seq,
                        CategoriaSituacao = situacaoInicialEtapaAtual.CategoriaSituacao,
                        SeqSolicitacaoServicoEtapa = dadosSolicitacao.SeqSolicitacaoServicoEtapa
                    };
                    this.SolicitacaoHistoricoSituacaoDomainService.SaveEntity(solicitacaoHistoricoSituacao);

                    // Inclui nova navegação (inicial da etapa) para a solicitação
                    var solicitacaoHistoricoNavegacao = new SolicitacaoHistoricoNavegacao()
                    {
                        SeqConfiguracaoEtapaPagina = dadosSolicitacao.SeqConfiguracaoEtapaPaginaAtual,
                        SeqSolicitacaoServicoEtapa = dadosSolicitacao.SeqSolicitacaoServicoEtapa,
                        DataEntrada = DateTime.Now,
                    };
                    this.SolicitacaoHistoricoNavegacaoDomainService.SaveEntity(solicitacaoHistoricoNavegacao);

                    /// De acordo com o token DO SERVIÇO realiza a reabertura
                    /// - DISPENSA_INDIVIDUAL => RN_SRC_057 - Solicitação - Reabertura de dispensa individual
                    /// - ATIVIDADE_COMPLEMENTAR => RN_SRC_058 - Solicitação - Reabertura de atividade complementar
                    /// - SOLICITACAO_ALTERACAO_PLANO_ESTUDO, SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA ou SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA => RN_SRC_087 - Solicitação - Reabertura do tipo de serviço Alterações no plano de estudos (task 32819)
                    switch (dadosSolicitacao.TokenServico)
                    {
                        case TOKEN_SERVICO.DISPENSA_INDIVIDUAL:
                            SolicitacaoDispensaDomainService.ReabrirSolicitacao(seqSolicitacaoServico);
                            break;

                        case TOKEN_SERVICO.ATIVIDADE_COMPLEMENTAR:
                            SolicitacaoAtividadeComplementarDomainService.ReabrirSolicitacao(seqSolicitacaoServico);
                            break;

                        case TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO:
                        case TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA:
                        case TOKEN_SERVICO.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA:
                            bool primeiraEtapa = etapaAtualSGF.Ordem == 1;
                            bool ultimaEtapa = etapaAtualSGF == dadosEtapasSGF.LastOrDefault();
                            SolicitacaoMatriculaDomainService.ReabrirSolicitacao(seqSolicitacaoServico, primeiraEtapa, ultimaEtapa);
                            break;
                    }
                    /// De acordo com o token DO TIPO DE SERVIÇO realiza a reabertura
                    /// - TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO => RN_CNC_073 - Documento Conclusão - Condições para reabertura solicitação serviço
                    if (dadosSolicitacao.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO)
                    {
                        SolicitacaoDocumentoConclusaoDomainService.VerificarDocumentoConclusaoAtivo(seqSolicitacaoServico);

                        SolicitacaoDocumentoConclusaoDomainService.ReabrirSolicitacao(seqSolicitacaoServico, observacao);
                    }

                    // Deverá ser enviado notificação para o solicitante independente do serviço,
                    // conforme a RN_SRC_079 - Reabrir Solicitação - Notificação sobre reabertura da solicitação.
                    Dictionary<string, string> dicTagsNot = new Dictionary<string, string>
                    {
                        { TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dadosSolicitacao.NomeSocialSolicitante) ? dadosSolicitacao.NomeSolicitante : dadosSolicitacao.NomeSocialSolicitante },
                        { TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dadosSolicitacao.DescricaoProcesso },
                        { TOKEN_TAG_NOTIFICACAO.NUM_PROTOCOLO, dadosSolicitacao.NumeroProtocolo }
                    };


                    if (dadosSolicitacao.TokenServico != TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA)
                    {
                        // Envia a notificação
                        var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                        {
                            SeqSolicitacaoServico = seqSolicitacaoServico,
                            TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.REABERTURA_SOLICITACAO,
                            DadosMerge = dicTagsNot,
                            EnvioSolicitante = true,
                            ConfiguracaoPrimeiraEtapa = false
                        };

                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);

                        // Deverá ser criado na linha do tempo a mensagem sobre reabertura independente do serviço,
                        // conforme a RN_SRC_038 - Solicitação - Criação de mensagem na linha do tempo sobre reabertura
                        // de solicitação
                        Dictionary<string, string> dicTagsMsg = new Dictionary<string, string>
                        {
                            { TOKEN_TAG_MENSAGEM.PROTOCOLO_SOLICITACAO, dadosSolicitacao.NumeroProtocolo },
                            { TOKEN_TAG_MENSAGEM.PROCESSO_SOLICITACAO, dadosSolicitacao.DescricaoProcesso }
                        };
                        MensagemDomainService.EnviarMensagemPessoaAtuacao(dadosSolicitacao.SeqPessoaAtuacao,
                                                                            dadosPessoa.SeqInstituicaoEnsino,
                                                                            dadosPessoa.SeqNivelEnsino,
                                                                            TOKEN_TIPO_MENSAGEM.REABERTURA_SOLICITACAO_SERVICO,
                                                                            CategoriaMensagem.LinhaDoTempo,
                                                                            dicTagsMsg);
                    }
                    else
                    {
                        //Seta como entregue anteriormente os documentos já anexados anteriormente para não duplicar ao realizar atendimento novamente
                        foreach (var documento in dadosSolicitacao.DocumentosRequeridos)
                        {
                            var specSolicitacao = new SolicitacaoDocumentoRequeridoFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico, SeqDocumentoRequerido = documento.SeqDocumentoRequerido };
                            var solicitacaoAtualizar = SolicitacaoDocumentoRequeridoDomainService.SearchByKey(specSolicitacao);

                            if (solicitacaoAtualizar != null && documento.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || documento.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)
                                solicitacaoAtualizar.EntregueAnteriormente = true;

                            SolicitacaoDocumentoRequeridoDomainService.SaveEntity(solicitacaoAtualizar);
                        }


                        dicTagsNot.Add(TOKEN_TAG_NOTIFICACAO.MOTIVO_REABERTURA, observacao);

                        // Envia a notificação
                        var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                        {
                            SeqSolicitacaoServico = seqSolicitacaoServico,
                            TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.REABERTURA_SOLICITACAO_ATUALIZACAO_DOCUMENTO,
                            DadosMerge = dicTagsNot,
                            EnvioSolicitante = false,
                            ConfiguracaoPrimeiraEtapa = false
                        };

                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);

                    }
                    // Fim da transação
                    transacao.Commit();
                }
                catch (Exception)
                {
                    transacao.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Verifica se os documentos obrigatórios de uma solicitação de serviço possuem arquivos anexados.
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço.</param>
        public void VerificaDocumentosSolicitacao(long seqSolicitacaoServico, long? seqConfiguracaoEtapa = null)
        {
            // Recupera os documentos salvos da solicitação.
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);
            var solicitacao = SearchByKey(spec, IncludesSolicitacaoServico.DocumentosRequeridos_DocumentoRequerido_GruposDocumentoRequerido);

            // REGRA DE NEGÓCIO SOLICITADA PELA LUCIANA
            // Caso não tenha sido selecionado em uma solicitação de dispensa itens cursados externamente, não deve validar os documentos obrigatórios
            var dadosSolicitacaoDispensa = SolicitacaoDispensaDomainService.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                ItensCursadosExternamente = x.OrigensExternas.Any(),
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token
            });

            if (dadosSolicitacaoDispensa != null &&
                dadosSolicitacaoDispensa.TokenServico == TOKEN_SERVICO.DISPENSA_INDIVIDUAL &&
                !dadosSolicitacaoDispensa.ItensCursadosExternamente)
                return;

            // Recupera os documentos requeridos da solicitação.
            var documentoRequeridos = RegistroDocumentoDomainService.BuscarDadosDocumentosRequeridos(new RegistroDocumentoVO
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                PermiteUploadArquivo = true
            });

            bool erro = false;
            var gruposInvalidos = new List<string>();

            var configuracaoProcesso = ConfiguracaoProcessoDomainService.SearchByKey(solicitacao.SeqConfiguracaoProcesso);
            var specProcesso = new ProcessoFilterSpecification() { Seq = configuracaoProcesso.SeqProcesso, TokenServico = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO };
            var processoEntregaDocumentacao = ProcessoDomainService.SearchBySpecification(specProcesso);

            //Se token de serviço não for ENTREGA_DOCUMENTACAO, valida normalmente a obrigatoriedade e quantidade mínima dos arquivos
            if (!processoEntregaDocumentacao.Any())
            {
                foreach (var documentoVO in documentoRequeridos)
                {
                    var documento = solicitacao.DocumentosRequeridos.FirstOrDefault(f => f.SeqDocumentoRequerido == documentoVO.SeqDocumentoRequerido);
                    // Verifica se o documento foi encontrado na lista de documentos requeridos.
                    if (documento != null)
                    {
                        string nomeGrupoErro = null;

                        // Verifica se o documento possui arquivo ou se encontra em um grupo em que outro documento possui arquivo.
                        if (!documento.SeqArquivoAnexado.HasValue
                                && (
                                    // Verifica se o documento é obrigatório e não possui grupos
                                    (documentoVO.Obrigatorio && !documentoVO.Grupos.Any())
                                    // ou, verifica se os grupos do arquivo possuem a quantidade minima de itens
                                    || (VerificaDocumentoPendentesGrupoDocumentos(solicitacao.DocumentosRequeridos, documentoVO.Grupos, out nomeGrupoErro))
                                    )
                                )
                        {
                            erro = true;
                            if (!string.IsNullOrWhiteSpace(nomeGrupoErro))
                            {
                                if (!gruposInvalidos.Contains(nomeGrupoErro))
                                    gruposInvalidos.Add(nomeGrupoErro);
                            }
                        }
                    }
                }
            }

            if (erro)
            {
                throw new DocumentoRequeridoObrigatorioNaoEnviadoException(string.Join(", ", gruposInvalidos));
            }
        }

        /// <summary>
        /// Verifica se uma etapa de uma solicitação de serviço está vigente e ativa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <returns></returns>
        public bool VerificarVigenciaEtapa(long seqSolicitacaoServico, long seqConfiguracaoEtapa)
        {
            IEnumerable<EtapaProjecaoVO> etapas = null;

            // Busca as etapas da solicitação de matricula
            var possuiEscalonamento = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.GrupoEscalonamento != null);
            if (possuiEscalonamento)
            {
                etapas = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.GrupoEscalonamento.Itens.Select(y =>
                    new EtapaProjecaoVO
                    {
                        SeqEtapaSGF = y.Escalonamento.ProcessoEtapa.SeqEtapaSgf,
                        SeqTemplateProcessoSgf = y.Escalonamento.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                        SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq,
                        DataInicio = y.Escalonamento.DataInicio,
                        DataFim = y.Escalonamento.DataFim,
                        SituacaoEtapa = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq).ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa,
                    })
                );
            }
            else
            {
                etapas = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(y =>
                    new EtapaProjecaoVO
                    {
                        SeqEtapaSGF = y.ProcessoEtapa.SeqEtapaSgf,
                        SeqTemplateProcessoSgf = y.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                        SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).Seq,
                        DataInicio = y.ProcessoEtapa.DataInicio ?? default(DateTime),
                        DataFim = y.ProcessoEtapa.DataFim,
                        SituacaoEtapa = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).Seq).ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa,
                    })
                );
            }

            var seqTemplateProcessoSGF = etapas.FirstOrDefault()?.SeqTemplateProcessoSgf;
            EtapaSimplificadaData[] etapasSGF = SGFHelper.BuscarEtapasSGFCache(seqTemplateProcessoSGF.Value);

            var etapaAtual = etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == seqConfiguracaoEtapa);
            var etapaAtualSGF = etapasSGF.FirstOrDefault(e => e.Seq == etapaAtual.SeqEtapaSGF);

            // Caso não esteja liberada, retorna falso
            if (etapaAtual.SituacaoEtapa != SituacaoEtapa.Liberada)
                return false;

            if (etapaAtual.DataInicio > DateTime.Now || etapaAtual.DataFim < DateTime.Now)
                return false;

            return true;
        }

        private bool VerificaDocumentoPendentesGrupoDocumentos(IList<SolicitacaoDocumentoRequerido> documentos, List<GrupoDocumentoVO> grupos, out string nomeGrupoErro)
        {
            nomeGrupoErro = string.Empty;
            foreach (var grupo in grupos)
            {
                // Verifica se o grupo possui número mínimo de documentos e se todos os itens do grupo não possuem arquivos.
                if (grupo.NumeroMinimoDocumentosRequerido > 0 &&
                            documentos.Where(f => f.DocumentoRequerido.GruposDocumentoRequerido.Any(g => g.SeqGrupoDocumentoRequerido == grupo.Seq))
                                        .Count(f => f.SeqArquivoAnexado.HasValue) < grupo.NumeroMinimoDocumentosRequerido)
                {
                    nomeGrupoErro = grupo.Descricao;
                    return true;
                }
            }

            return false;
        }

        public void SalvarSolicitacaoAtividadeComplementar(SolicitacaoAtividadeComplementarPaginaVO modelVO)
        {
            // Verifica se foi informado o tipo de publicação
            var tipoDivisaoComponente = DivisaoComponenteDomainService.SearchProjectionByKey(modelVO.SeqDivisaoComponente, x => x.TipoDivisaoComponente);

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                // Busca os dados da pessoa atuação
                var seqPessoaAtuacao = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(modelVO.SeqSolicitacaoServico), x => x.SeqPessoaAtuacao);

                // Recupera os dados de origem da pessoa atuação para saber a matriz
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

                // Valida os pré requisitos
                var preRequisitos = RequisitoDomainService.ValidarPreRequisitos(seqPessoaAtuacao, new List<long> { modelVO.SeqDivisaoComponente }, null, null, null);

                if (!preRequisitos.Valido)
                    throw new SolicitacaoAtividadeComplementarPreRequisitoNaoCursadoException(string.Join("<br/>", preRequisitos.MensagensErro));

                // Verifica se a solicitação já foi convertida pra ser uma solicitação de atividade complementar
                var solicitacao = SolicitacaoAtividadeComplementarDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoAtividadeComplementar>(modelVO.SeqSolicitacaoServico), x => x.SolicitacaoArtigo);
                var solicitacaoArtigo = modelVO.Transform<SolicitacaoArtigo>();

                // Caso não tenha sido convertida, converte já inserindo os dados corretos
                if (solicitacao == null)
                {
                    InsereNovaSolicitacaoAtividadeComplementar(modelVO);
                }
                else
                {
                    // Se estiver editando, preenche o valor do seq da solicitação de artigo.
                    var solicitacaoArtigoSalvo = solicitacao.SolicitacaoArtigo.FirstOrDefault();
                    if (solicitacaoArtigoSalvo != null)
                        solicitacaoArtigo.Seq = solicitacaoArtigoSalvo.Seq;

                    solicitacao.Descricao = modelVO.Descricao;
                    solicitacao.SeqDivisaoComponente = modelVO.SeqDivisaoComponente;
                    solicitacao.DataInicio = modelVO.DataInicio;
                    solicitacao.DataFim = modelVO.DataFim;
                    solicitacao.CargaHoraria = modelVO.CargaHoraria;

                    if (modelVO.SeqCicloLetivo.HasValue)
                        solicitacao.SeqCicloLetivo = modelVO.SeqCicloLetivo.Value;

                    solicitacao.SeqEscalaApuracaoItem = modelVO.SeqEscalaApuracaoItem;
                    solicitacao.Faltas = (short?)modelVO.Faltas;
                    solicitacao.Nota = (short?)modelVO.Nota;
                    solicitacao.SituacaoHistoricoEscolar = modelVO.SituacaoFinal;

                    SolicitacaoAtividadeComplementarDomainService.SaveEntity(solicitacao);
                }

                solicitacaoArtigo.SeqSolicitacaoAtividadeComplementar = modelVO.SeqSolicitacaoServico;
                SolicitacaoArtigoDomainService.SaveEntity(solicitacaoArtigo);

                unitOfWork.Commit();
            }
        }

        private void InsereNovaSolicitacaoAtividadeComplementar(SolicitacaoAtividadeComplementarPaginaVO modelVO)
        {
            var param = new List<SMCFuncParameter>()
            {
                new SMCFuncParameter("seq", modelVO.SeqSolicitacaoServico),
                new SMCFuncParameter("desc", modelVO.Descricao),
                new SMCFuncParameter("seqDivisaoComponente", modelVO.SeqDivisaoComponente),
                new SMCFuncParameter("datInicio", modelVO.DataInicio),
                new SMCFuncParameter("datFim", modelVO.DataFim),
                new SMCFuncParameter("cargaHoraria", modelVO.CargaHoraria),
                new SMCFuncParameter("seqCicloLetivo", modelVO.SeqCicloLetivo),
                new SMCFuncParameter("seqEscalaApuracaoItem", modelVO.SeqEscalaApuracaoItem),
                new SMCFuncParameter("faltas", modelVO.Faltas),
                new SMCFuncParameter("nota", modelVO.Nota),
                new SMCFuncParameter("situacaoHistoricoEscolar", modelVO.SituacaoFinal),
            };

            ExecuteSqlCommand(@"SET IDENTITY_INSERT SRC.solicitacao_atividade_complementar ON

								INSERT INTO SRC.solicitacao_atividade_complementar
								(seq_solicitacao_servico, dsc_atividade_complementar, seq_divisao_componente,
									dat_inicio, dat_fim, qtd_carga_horaria, seq_ciclo_letivo,
									seq_escala_apuracao_item, num_falta, num_nota, idt_dom_situacao_historico_escolar)
								VALUES (@seq, @desc, @seqDivisaoComponente, @datInicio, @datFim, @cargaHoraria, @seqCicloLetivo, @seqEscalaApuracaoItem, @faltas, @nota, @situacaoHistoricoEscolar)

								SET IDENTITY_INSERT SRC.solicitacao_atividade_complementar OFF", param);
        }

        /// <summary>
        /// Atualiza o campo entidade compartilhada da solicitação de serviço, utilizado para disciplina eletiva com o valor do grupo de programa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqEntidadeCompartilhada">Sequencial da entidade complartilhada (grupo de programa)</param>
        public void AtualizarSolicitacaoServicoEntidadeCompartilhada(long seqSolicitacaoServico, long? seqEntidadeCompartilhada)
        {
            var solicitacaoServico = this.SearchByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico));
            if (solicitacaoServico.SeqEntidadeResponsavel != seqEntidadeCompartilhada && solicitacaoServico.SeqEntidadeCompartilhada != seqEntidadeCompartilhada)
            {
                solicitacaoServico.SeqEntidadeCompartilhada = seqEntidadeCompartilhada;
                this.UpdateFields(solicitacaoServico, p => p.SeqEntidadeCompartilhada);
            }
        }

        public List<SMCDatasourceItem> BuscarMotivosCancelamentoSolicitacaoSelect(long seqSolicitacaoServico)
        {
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var result = this.SearchProjectionByKey(spec, s => new
            {
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqEtapaAtualSgf = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                TokenTipoServico = s.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token
            });

            var situacoesEtapa = SGFHelper.BuscarEtapasSGFCache(result.SeqTemplateProcessoSGF);

            if (result.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO)
            {
                var tokensPermitidos = new List<string>() { TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.SOLICITACAO_CRIADA_INDEVIDAMENTE, TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.DADOS_INCONSISTENTES };

                return situacoesEtapa.FirstOrDefault(e => e.Seq == result.SeqEtapaAtualSgf).Situacoes
                                     .Where(s => s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado)
                                     .SelectMany(s => s.MotivosSituacao)
                                     .Where(m => tokensPermitidos.Contains(m.Token))
                                     .OrderBy(m => m.Descricao).Select(m => new SMCDatasourceItem()
                                     {
                                         Seq = m.Seq,
                                         Descricao = m.Descricao
                                     }).ToList();
            }

            var tokensNaoPermitidos = new List<string>()
            {
               TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.REALIZACAO_MATRICULA_AUTOMATICA,
               TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.MATRICULA_CANCELADA,
               TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.MATRICULA_TRANCADA,
               TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.SOLICITACAO_CANCELADA_PORTAL_ALUNO,
               TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.CURSO_NAO_POSSUI_RECONHECIMENTO,
               TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.ALUNO_NAO_APTO_EMISSAO_DOCUMENTO_CONCLUSAO,
               TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.ALUNO_INDEFERIU_DADOS_DOCUMENTO
            };

            return situacoesEtapa.FirstOrDefault(e => e.Seq == result.SeqEtapaAtualSgf).Situacoes
                                 .Where(s => s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado)
                                 .SelectMany(s => s.MotivosSituacao)
                                 .Where(m => !tokensNaoPermitidos.Contains(m.Token))
                                 .OrderBy(m => m.Descricao).Select(m => new SMCDatasourceItem()
                                 {
                                     Seq = m.Seq,
                                     Descricao = m.Descricao
                                 }).ToList();
        }

        public bool ValidarMotivoCancelamentoSolicitacao(long seqSolicitacaoServico)
        {
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var result = this.SearchProjectionByKey(spec, s => new
            {
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqEtapaAtualSgf = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
            });

            var situacoesEtapa = SGFHelper.BuscarEtapasSGFCache(result.SeqTemplateProcessoSGF);

            var exigeMotivo = situacoesEtapa.FirstOrDefault(e => e.Seq == result.SeqEtapaAtualSgf).Situacoes
                     .Where(s => s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado).FirstOrDefault().ExigeMotivo;

            return exigeMotivo;
        }

        public bool ValidarTipoCancelamentoSolicitacao(long seqSolicitacaoServico)
        {
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var retorno = this.SearchProjectionByKey(spec, s => new
            {
                TokenTipoServico = s.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token,
                s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.Token
            });

            var exigeTipoCancelamento = (retorno.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO) &&
                                        (retorno.Token == TOKEN_ETAPA_SOLICITACAO.ASSINATURA_DOCUMENTO_DIGITAL ||
                                         retorno.Token == TOKEN_ETAPA_SOLICITACAO.ENTREGA_DOCUMENTO_DIGITAL);

            return exigeTipoCancelamento;
        }

        /// <summary>
        /// Busca o número de protocolo da solicitação de serviço para mensagens e links
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de serviço</param>
        /// <returns>Número de protocolo</returns>
        public string BuscarNumeroProtocoloSolicitacaoServico(long seq)
        {
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seq);

            var result = this.SearchProjectionByKey(spec, s => s.NumeroProtocolo);

            return result;
        }

        /// <summary>
        /// Busca os dados da solicitação de serviço para detalhes da modal de acordo com sequencial ou protocolo
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="protocolo">Número do protocolo</param>
        /// <returns>Objeto de solicitacao de serviço simplificado para modal</returns>
        public DadosModalSolicitacaoServicoVO BuscarDadosModalSolicitacaoServico(long? seqSolicitacaoServico, string protocolo)
        {
            var spec = new SolicitacaoServicoFilterSpecification() { Seq = seqSolicitacaoServico, NumeroProtocolo = protocolo };

            var dados = this.SearchProjectionByKey(spec, x => new
            {
                SeqSolicitacaoServico = x.Seq,
                NumeroProtocolo = x.NumeroProtocolo,
                Processo = x.ConfiguracaoProcesso.Processo.Descricao,
                SeqTemplateProcessoSGF = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                UsuarioInclusao = x.UsuarioInclusao,
                UsuarioAlteracao = x.UsuarioAlteracao,
                DataInclusao = x.DataInclusao,
                DataAlteracao = x.DataAlteracao,
                SeqServico = x.ConfiguracaoProcesso.Processo.SeqServico,
                SeqProcessoEtapaAtual = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa,
                SeqConfiguracaoEtapa = x.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                SituacaoAtual = x.SituacaoAtual,
                ExigeJustificativa = x.SeqJustificativaSolicitacaoServico.HasValue,
                JustificativaComplementar = x.JustificativaComplementar,
                Justificativa = x.JustificativaSolicitacaoServico.Descricao,
                DescricaoOriginal = x.DescricaoOriginal,
                DescricaoAtualizada = x.DescricaoAtualizada,
                SituacaoDocumentacao = x.SituacaoDocumentacao,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                TokenTipoServico = x.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token,
                UsuarioResponsavel = x.UsuariosResponsaveis.OrderByDescending(o => o.Seq).FirstOrDefault().UsuarioInclusao,
            });

            var etapaSGF = SGFHelper.BuscarEtapasSGFCache(dados.SeqTemplateProcessoSGF);
            var situacaoAtualSGF = etapaSGF.SelectMany(e => e.Situacoes).FirstOrDefault(e => e.Seq == dados.SituacaoAtual.SeqSituacaoEtapaSgf);

            var retorno = new DadosModalSolicitacaoServicoVO()
            {
                SeqSolicitacaoServico = dados.SeqSolicitacaoServico,
                SeqPessoaAtuacao = dados.SeqPessoaAtuacao,
                Solicitacao = new DadosModalSolicitacaoVO()
                {
                    Seq = dados.SeqSolicitacaoServico,
                    SeqServico = dados.SeqServico,
                    SeqConfiguracaoEtapa = dados.SeqConfiguracaoEtapa,
                    NumeroProtocolo = dados.NumeroProtocolo,
                    Processo = dados.Processo,
                    SeqProcessoEtapaAtual = dados.SeqProcessoEtapaAtual,
                    SituacaoDocumentacao = dados.SituacaoDocumentacao,
                    TokenTipoServico = dados.TokenTipoServico
                },
                SolicitacaoAtual = new DadosModalSolicitacaoAtualVO()
                {
                    DataSituacao = dados.SituacaoAtual.DataAlteracao.HasValue ? dados.SituacaoAtual.DataAlteracao.Value : dados.SituacaoAtual.DataInclusao,
                    Descricao = string.Format("{0} | {1}", situacaoAtualSGF.CategoriaSituacao.SMCGetDescription(), situacaoAtualSGF.Descricao),
                    UsuarioResponsavel = string.IsNullOrEmpty(dados.UsuarioResponsavel) ? "Não há usuário responsável" : dados.UsuarioResponsavel,
                    Observacao = dados.SituacaoAtual.Observacao
                },
                SolicitacaoOriginal = new DadosModalSolicitacaoOriginalVO()
                {
                    CriadoPor = dados.UsuarioInclusao,
                    DataCriacao = dados.DataInclusao,
                    DescricaoSolicitacao = dados.DescricaoOriginal,
                    ExigeJustificativa = dados.ExigeJustificativa,
                    Justificativa = dados.Justificativa,
                    JustificativaComplementar = dados.JustificativaComplementar
                },
                SolicitacaoAtualizada = new DadosModalSolicitacaoAtualizadaVO()
                {
                    AtualizadoPor = dados.UsuarioAlteracao,
                    DataAtualizacao = dados.DataAlteracao,
                    DescricaoSolicitacao = dados.DescricaoAtualizada
                },
                ExibirAbaDocumentos = dados.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_ACADEMICO && dados.SituacaoDocumentacao != SituacaoDocumentacao.Entregue ? false : true
            };

            var specSolicitacaoTaxas = new SolicitacaoServicoBoletoTaxaFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
            var taxasSolicitacaoServico = this.SolicitacaoServicoBoletoTaxaDomainService.SearchBySpecification(specSolicitacaoTaxas).ToList();
            retorno.SolicitacaoPossuiTaxas = taxasSolicitacaoServico.Any();

            return retorno;
        }

        /// <summary>
        /// Busca o sequencial do ciclo letivo do processo da solicitação da pessoa atuação para realizar a busca de nomes da turma
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Sequencial do ciclo letivo</returns>
        public long? BuscarCicloLetivoSolicitacaoPorPessoaAtuacao(long seqPessoaAtuacao)
        {
            // Recupera todas as solicitações dos serviços que estejam em andamento ou novas.
            var filtroSpec = new SolicitacaoServicoFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.EmAndamento, CategoriaSituacao.Novo }
            };

            var seqCicloSolicitacao = SearchProjectionBySpecification(filtroSpec, x => x.ConfiguracaoProcesso.Processo.SeqCicloLetivo).LastOrDefault();

            return seqCicloSolicitacao;
        }

        /// <summary>
        /// Busca o campo justificativa complementar da solicitação de serviço
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de serviço</param>
        /// <returns>Justificativa complementar</returns>
        public string BuscarSolicitacaoServicoJustificativaComplementar(long seq)
        {
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seq);

            var result = this.SearchProjectionByKey(spec, s => s.JustificativaComplementar);

            return result;
        }

        /// <summary>
        /// Atualiza o campo justificativa complementar da solicitação de serviço, utilizado para disciplina eletiva com o valor do grupo de programa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="justificativa">Justificativa complementar da solicitação de serviço</param>
        public void AtualizarSolicitacaoServicoJustificativaComplementar(long seqSolicitacaoServico, string justificativa)
        {
            var solicitacaoServico = this.SearchByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico));
            if (!string.IsNullOrEmpty(justificativa))
            {
                solicitacaoServico.JustificativaComplementar = justificativa;
                this.UpdateFields(solicitacaoServico, p => p.JustificativaComplementar);
            }
        }

        /// <summary>
        /// Busca as situações de matricula de acordo com o tipo de atuação informado
        /// </summary>
        /// <param name="tipoAtuacao">Tipo de atuação informado</param>
        /// <returns>Lista de situações de matrícula encontradas</returns>
        public List<SMCDatasourceItem> BuscarSituacoesMatriculaPorTipoAtuacaoSelect(TipoAtuacao tipoAtuacao)
        {
            var result = new List<SMCDatasourceItem>();

            switch (tipoAtuacao)
            {
                case TipoAtuacao.Aluno:

                    result = SituacaoMatriculaDomainService.BuscarSituacoesMatriculasDaInstituicaoSelect(new SituacaoMatriculaFiltroVO());

                    break;

                case TipoAtuacao.Ingressante:

                    result = SMCEnumHelper.GenerateKeyValuePair<SituacaoIngressante>().Select(s => new SMCDatasourceItem()
                    {
                        Seq = (long)s.Key,
                        Descricao = s.Value
                    }).ToList();

                    break;
            }

            return result;
        }

        public BotoesSolicitacaoVO GerarBotoesSolicitacao(long seqSolicitacaoServico)
        {
            // Desconsidera filtro de dados para esta consulta
            this.PessoaAtuacaoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            this.PlanoEstudoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            // Recupera o sequencial da aplicação do SAS
            var seqAplicacaoSAS = AplicacaoService.BuscarAplicacaoPelaSigla(SIGLA_APLICACAO.SGA_ADMINISTRATIVO).Seq;

            var seqUsuarioLogado = SMCContext.User.SMCGetSequencialUsuario();

            //Recupera todas as entidades responsáveis do usuário logado
            var seqsEntidadesResponsaveisUsuarioLogado = SMCDataFilterHelper.GetFilters().FirstOrDefault(f => f.Key == FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL).Value;

            var resultPrimario = this.SearchProjectionByKey(spec, s => new
            {
                SeqSolicitacaoServico = s.Seq,
                SeqServico = s.ConfiguracaoProcesso.Processo.SeqServico,
                SeqPessoaAtuacao = s.PessoaAtuacao.Seq,
                SeqSituacaoEtapaAtualSGF = s.SituacaoAtual.SeqSituacaoEtapaSgf,
                SeqSituacaoPrimeiraEtapaSGF = s.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.Ordem == 1).SituacaoAtual.SeqSituacaoEtapaSgf,
                SituacaoDocumentacao = s.SituacaoDocumentacao,
                TipoAtuacao = s.PessoaAtuacao.TipoAtuacao,
                PermiteReabrirSolicitacao = s.ConfiguracaoProcesso.Processo.Servico.PermiteReabrirSolicitacao,
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SolicitacaoServicoEDeMatricula = s is SolicitacaoMatricula,
                RealizadaAdesaoContrato = (s as SolicitacaoMatricula).CodigoAdesao.HasValue,
                SeqConfiguracaoEtapaAtual = s.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                SolicitacaoPossuiGrupoEscalonamento = s.GrupoEscalonamento != null,
                SeqEntidadeCompartilhada = (long?)s.EntidadeCompartilhada.Seq,
                SeqEntidadeResponsavel = s.SeqEntidadeResponsavel,
                SolicitacaoComAtendimentoIniciado = s.UsuariosResponsaveis.Any(),
                UsuarioLogadoEResponsavelAtualPelaSolicitacao = s.UsuariosResponsaveis.OrderByDescending(o => o.Seq).FirstOrDefault().SeqUsuarioResponsavel == seqUsuarioLogado.Value,
                TokenAtendimentoServico = s.ConfiguracaoProcesso.Processo.Servico.TokenAcessoAtendimento,
                SolicitacaoAssociadaGrupoEscalonamento = s.SeqGrupoEscalonamento.HasValue,
                ProcessoEtapaAtual = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa,
                SituacaoIngressanteAtual = (SituacaoIngressante?)(s.PessoaAtuacao as Ingressante).HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoIngressante,
                SeqPrimeiraEtapaSGF = s.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.Ordem == 1).ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                NumeroOrdemEtapaAtual = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.Ordem,
                TipoPrazoReabertura = s.ConfiguracaoProcesso.Processo.Servico.TipoPrazoReabertura,
                NumeroDiasPrazoReabertura = s.ConfiguracaoProcesso.Processo.Servico.NumeroDiasPrazoReabertura,
                DataInclusaoSituacaoAtual = s.SituacaoAtual.DataInclusao,
                TokenServico = s.ConfiguracaoProcesso.Processo.Servico.Token,
                TokenTipoServico = s.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token,
                SeqCicloLetivo = (long?)s.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                NumeroProtocolo = s.NumeroProtocolo,
                DataSolicitacao = s.DataSolicitacao,
                SeqHierarquiaEntidadeItem = s.EntidadeResponsavel.HierarquiasEntidades.FirstOrDefault().Seq
            });

            bool situacaoSolicitantePermiteReabrirSolicitacao = false;
            bool solicitacaoPossuiGrupoEscalonamentoVigente = false;
            bool configEtapaPossuiPaginaUploadDocumento = false;

            if (resultPrimario.SeqConfiguracaoEtapaAtual > 0)
            {
                var specConfigEtapa = new ConfiguracaoEtapaFilterSpecification() { Seq = resultPrimario.SeqConfiguracaoEtapaAtual };
                var configEtapa = ConfiguracaoEtapaDomainService.SearchProjectionByKey(specConfigEtapa, x => new
                {
                    x.Seq,
                    x.SeqConfiguracaoProcesso,
                    x.Descricao,
                    x.ConfiguracoesPagina
                });

                configEtapaPossuiPaginaUploadDocumento = configEtapa.ConfiguracoesPagina.SMCAny(c => c.ConfiguracaoDocumento == ConfiguracaoDocumento.UploadDocumento);
            }

            if (resultPrimario.TipoAtuacao == TipoAtuacao.Aluno)
            {
                //FIX: Corrigir após definição dos analistas de negócio com relação a apresentação da tela de consultas dados da solicitação de serviços.
                //Buscar a situacao atual da pessao atuacao desativando o filtro de dados de hierarquia_entidade_organizacional
                var seqSituacaoMatriculaAtual = this.AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(resultPrimario.SeqPessoaAtuacao, true)?.SeqSituacao;

                if (seqSituacaoMatriculaAtual == null)
                    throw new SolicitacaoServicoNaoEncontrouSituacaoMatriculaAtualException();

                var resultSecundario = this.SearchProjectionByKey(spec, s => new
                {
                    SituacaoSolicitantePermiteReabrirSolicitacao = s.ConfiguracaoProcesso.Processo.Servico.SituacoesAluno.Any(ss => ss.SeqSituacaoMatricula == seqSituacaoMatriculaAtual && ss.PermissaoServico == PermissaoServico.ReabrirSolicitacao),
                    SolicitacaoPossuiGrupoEscalonamentoVigente = s.GrupoEscalonamento.Itens.Any(i => i.Escalonamento.ProcessoEtapa.SeqEtapaSgf == resultPrimario.ProcessoEtapaAtual.SeqEtapaSgf && (DateTime.Now >= i.Escalonamento.DataInicio && DateTime.Now <= s.ConfiguracaoProcesso.Processo.DataFim)),
                });

                situacaoSolicitantePermiteReabrirSolicitacao = resultSecundario.SituacaoSolicitantePermiteReabrirSolicitacao;
                solicitacaoPossuiGrupoEscalonamentoVigente = resultSecundario.SolicitacaoPossuiGrupoEscalonamentoVigente;
            }
            else if (resultPrimario.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                var resultSecundario = this.SearchProjectionByKey(spec, s => new
                {
                    SituacaoSolicitantePermiteReabrirSolicitacao = s.ConfiguracaoProcesso.Processo.Servico.SituacoesIngressante.Any(si => si.SituacaoIngressante == resultPrimario.SituacaoIngressanteAtual),
                    SolicitacaoPossuiGrupoEscalonamentoVigente = s.GrupoEscalonamento.Itens.Any(i => i.Escalonamento.ProcessoEtapa.SeqEtapaSgf == resultPrimario.ProcessoEtapaAtual.SeqEtapaSgf && (DateTime.Now >= i.Escalonamento.DataInicio && DateTime.Now <= s.ConfiguracaoProcesso.Processo.DataFim)),
                });

                situacaoSolicitantePermiteReabrirSolicitacao = resultSecundario.SituacaoSolicitantePermiteReabrirSolicitacao;
                solicitacaoPossuiGrupoEscalonamentoVigente = resultSecundario.SolicitacaoPossuiGrupoEscalonamentoVigente;
            }

            var SolicitacaoPossuiProcessoEtapaVigente = true;

            if (resultPrimario.ProcessoEtapaAtual.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia)
                SolicitacaoPossuiProcessoEtapaVigente = DateTime.Now >= resultPrimario.ProcessoEtapaAtual.DataInicio && (!resultPrimario.ProcessoEtapaAtual.DataFim.HasValue || DateTime.Now <= resultPrimario.ProcessoEtapaAtual.DataFim);
            else if (resultPrimario.ProcessoEtapaAtual.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento)
                SolicitacaoPossuiProcessoEtapaVigente = solicitacaoPossuiGrupoEscalonamentoVigente;

            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(resultPrimario.SeqTemplateProcessoSGF);
            var etapaAtualSGF = etapasSGF.FirstOrDefault(e => e.Seq == resultPrimario.ProcessoEtapaAtual.SeqEtapaSgf);
            var situacaoEtapaAtualSGF = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == resultPrimario.SeqSituacaoEtapaAtualSGF);

            var primeiraEtapaSGF = etapasSGF.FirstOrDefault(e => e.Seq == resultPrimario.SeqPrimeiraEtapaSGF);

            //Realiza um count para saber se o usuario logado tem acesso a entidade responsavel
            //O filtro de dados deverá tratar isso por padrão, caso o usuário não tenha acesso a entidade, o count retornará ZERO
            var UsuarioLogadoPossuiAcessoEntidadeCompartilhada = this.EntidadeDomainService.Count(new SMCSeqSpecification<Entidade>(resultPrimario.SeqEntidadeCompartilhada ?? 0)) > 0;

            //Verifica se o prazo para reabertura da solicitação está expirado
            DateTime dataLimiteReabertura = DateTime.MinValue;
            if (resultPrimario.TipoPrazoReabertura == TipoPrazoReabertura.DiasCorridos)
            {
                dataLimiteReabertura = resultPrimario.DataInclusaoSituacaoAtual.AddDays(resultPrimario.NumeroDiasPrazoReabertura.GetValueOrDefault());
            }
            else
            {
                dataLimiteReabertura = SMCDateTimeHelper.AddBusinessDays(resultPrimario.DataInclusaoSituacaoAtual, resultPrimario.NumeroDiasPrazoReabertura.GetValueOrDefault(), null);
            }

            var solicitacaoServico = new BotoesSolicitacaoVO()
            {
                SeqSolicitacaoServico = resultPrimario.SeqSolicitacaoServico,
                SeqServico = resultPrimario.SeqServico,
                TokenServico = resultPrimario.TokenServico,
                SeqPessoaAtuacao = resultPrimario.SeqPessoaAtuacao,
                SeqConfiguracaoEtapa = resultPrimario.SeqConfiguracaoEtapaAtual,
                SituacaoClassificacaoFinalizadaSemSucesso = situacaoEtapaAtualSGF != null ? situacaoEtapaAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso : false,
                SituacaoClassificacaoFinalizadaComSucesso = situacaoEtapaAtualSGF != null ? situacaoEtapaAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso : false,
                SituacaodDocumentacaoNaoRequerida = resultPrimario.SituacaoDocumentacao == SituacaoDocumentacao.NaoRequerida,
                EtapaAtualPossuiParametrizacaoDeSituacaoDeCancelamento = etapasSGF.FirstOrDefault(s => s.Seq == resultPrimario.ProcessoEtapaAtual.SeqEtapaSgf).Situacoes.Any(s => s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado),

                PrazoParaReabrirSolicitacaoExpirado = DateTime.Now.Date > dataLimiteReabertura.Date,

                PermiteReabrirSolicitacao = resultPrimario.PermiteReabrirSolicitacao,
                SolicitacaoAssociadaGrupoEscalonamento = resultPrimario.SolicitacaoAssociadaGrupoEscalonamento,
                SituacaoSolicitanteNaoPermiteReabrirSolicitacao = !situacaoSolicitantePermiteReabrirSolicitacao,
                SituacaoAtualSolicitacaoEncerradaComSucesso = situacaoEtapaAtualSGF != null ? situacaoEtapaAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso : false,

                SolicitacaoServicoEDeMatricula = resultPrimario.SolicitacaoServicoEDeMatricula,
                NaoRealizadaAdezaoContrato = !resultPrimario.RealizadaAdesaoContrato,
                SituacaoAtualSolicitacaoEncerradaOuConcluida = situacaoEtapaAtualSGF != null ? situacaoEtapaAtualSGF.CategoriaSituacao == CategoriaSituacao.Concluido || situacaoEtapaAtualSGF.CategoriaSituacao == CategoriaSituacao.Encerrado : false,
                SituacaoAtualSolicitacaoDiferenteEncerrada = situacaoEtapaAtualSGF != null ? situacaoEtapaAtualSGF.CategoriaSituacao != CategoriaSituacao.Encerrado : false,
                SituacaoAtualEtapaDiferenteLiberada = resultPrimario.ProcessoEtapaAtual.SituacaoEtapa != SituacaoEtapa.Liberada,
                EtapaAtualIndisponivelCentralAtendimento = !resultPrimario.ProcessoEtapaAtual.CentralAtendimento,

                SolicitacaoPossuiGrupoEscalonamentoComPeriodoNaoVigente = (resultPrimario.SolicitacaoPossuiGrupoEscalonamento && !solicitacaoPossuiGrupoEscalonamentoVigente) || (!resultPrimario.SolicitacaoPossuiGrupoEscalonamento && !SolicitacaoPossuiProcessoEtapaVigente),
                SolicitacaoComEtapaAtualCompartilhadaEUsuarioNaoAssociado = resultPrimario.ProcessoEtapaAtual.EtapaCompartilhada && !UsuarioLogadoPossuiAcessoEntidadeCompartilhada,

                SolicitacaoComEtapaAtualNaoCompartilhadaEEntidadeUsuarioLogadoNaoResponsavel = seqsEntidadesResponsaveisUsuarioLogado != null && seqsEntidadesResponsaveisUsuarioLogado.Any() ? !resultPrimario.ProcessoEtapaAtual.EtapaCompartilhada && !seqsEntidadesResponsaveisUsuarioLogado.Contains(resultPrimario.SeqHierarquiaEntidadeItem) : false,

                SolicitacaoPossuiUsuarioResponsavel = resultPrimario.SolicitacaoComAtendimentoIniciado && !resultPrimario.UsuarioLogadoEResponsavelAtualPelaSolicitacao,
                UsuarioLogadoEResponsavelAtualPelaSolicitacao = resultPrimario.UsuarioLogadoEResponsavelAtualPelaSolicitacao,
                UsuarioNaoPossuiAcessoARealizarAtendimento = !SMCSecurityHelper.Authorize(resultPrimario.TokenAtendimentoServico),
                EtapaNaoDisponivelParaAplicacao = etapaAtualSGF.SeqAplicacaoSAS != seqAplicacaoSAS,
                SituacaoAtualSolicitacaoEFimProcesso = situacaoEtapaAtualSGF != null ? situacaoEtapaAtualSGF.SituacaoFinalProcesso : false,
                PrimeiraEtapaComSituacaoFinalEtapaEFinalizadaComSucesso = resultPrimario.SeqSituacaoPrimeiraEtapaSGF == primeiraEtapaSGF.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).Seq,

                DescricaoEtapaAtual = etapaAtualSGF.Descricao,

                SituacaoAtualSolicitacaoEInicialEtapa = situacaoEtapaAtualSGF != null ? situacaoEtapaAtualSGF.SituacaoInicialEtapa : false,
                EtapaAtualVigente = VerificarVigenciaEtapa(resultPrimario.SeqSolicitacaoServico, resultPrimario.SeqConfiguracaoEtapaAtual),

                ConfigEtapaPossuiPaginaUploadDocumento = configEtapaPossuiPaginaUploadDocumento,
                PermiteFinalizarSolicitacaoCRA = resultPrimario.TokenServico == TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA && situacaoEtapaAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado
            };

            solicitacaoServico.OrientacaoAtendimento = resultPrimario.ProcessoEtapaAtual.OrientacaoAtendimento;

            if (situacaoEtapaAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                solicitacaoServico.SolicitantePossuiBloqueiosVigentes = false;
            else
                solicitacaoServico.SolicitantePossuiBloqueiosVigentes = PessoaAtuacaoBloqueioDomainService.PessoaAtuacaoPossuiBloqueios(resultPrimario.SeqPessoaAtuacao, resultPrimario.SeqConfiguracaoEtapaAtual, false); // False pois está verificando o início do atendimento

            //Recupera a etapa anterior
            var etapaAnteriorSgf = etapasSGF.Where(e => e.Ordem < resultPrimario.NumeroOrdemEtapaAtual).OrderByDescending(e => e.Ordem).FirstOrDefault();

            if (etapaAnteriorSgf != null)
            {
                solicitacaoServico.DescricaoEtapaAnterior = etapaAnteriorSgf.Descricao;

                var resultTerciario = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
                {
                    SeqConfiguracaoEtapaAnterior = x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf == etapaAnteriorSgf.Seq).SeqConfiguracaoEtapa,
                });

                // Verificar pois são usadas apenas no botão de reabrir chancela
                solicitacaoServico.EtapaAnteriorVigente = VerificarVigenciaEtapa(resultPrimario.SeqSolicitacaoServico, resultTerciario.SeqConfiguracaoEtapaAnterior);
                solicitacaoServico.EtapaAtualEAnteriorAssociadasMesmaAplicacao = etapaAtualSGF.SeqAplicacaoSAS == etapaAnteriorSgf.SeqAplicacaoSAS;
            }

            #region Regras para liberar ou não o botão de reabrir solicitação

            /* Regras para liberar ou não o botão de reabrir solicitação

				LEGENDAS:
				[Situações parametrizadas]*= se o solicitante for Aluno, verificar se a situação do aluno, conforme a RN_ALN_056 - Critério para identificação da situação do aluno. está parametrizada para o serviço permitindo a criação. Senão, se o solicitante for Ingressante, verificar se última situação está parametrizada para o serviço permitindo a criação.

				[Período da etapa]*= o período da etapa é a Data de Início da Etapa atual e a Data Fim do Processo.

				[Solicitações que esteja em aberto]* = é considerado que uma solicitação esteja em aberto, se a situação atual da solicitação tiver a seguinte parametrização:
				- Classificação de situação final = NULO.

				[Prazo para reabrir a solicitação expirou]* = é considerado que o prazo para reabrir uma solicitação expirou quando a data atual (hoje) for maior que a data limite de reabertura. A data limite de reabertura deve ser calculada da seguinte forma:

				Se o Tipo de Prazo da Reabertura for igual a Dias Úteis, então:
				- Data limite de reabertura = [data de encerramento da solicitação]* + prazo para reabertura parametrizado para o respectivo serviço, portanto deverá ser considerado somente [dias úteis]
				Senão, Se o Tipo de Prazo da Reabertura for igual a Dias Corridos, então:
				- Data limite de reabertura = [data de encerramento da solicitação]* + prazo para reabertura parametrizado para o respectivo serviço.

				[Data de encerramento da solicitação]* = refere-se a data de início da situação atual da solicitação.

				[Dias úteis] Avaliar os feriados de acordo o calendário parametrizado para a Instituição e Nível de Ensino que se aplica a solicitação.

				[PENDENTE] O escopo para analise de feriados consta para entrega futura.

				1. Se a solicitação está associada a um grupo de escalonamento, o botão deverá ser desabilitado com a seguinte mensagem informativa:
					"Opção indisponível. Esta solicitação possui grupo de escalonamento e caso seja necessário reabrir, ele deve ser alterado."
			*/
            if (resultPrimario.SolicitacaoAssociadaGrupoEscalonamento)
            {
                solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. Esta solicitação possui grupo de escalonamento e caso seja necessário reabrir, ele deve ser alterado.";
            }

            /*
				2. Senão, se o serviço não permite reabrir solicitação o botão deverá ser desabilitado com a seguinte mensagem informativa:
					"Opção indisponível. O tipo de serviço não permite que a solicitação seja reaberta."
			*/
            else if (resultPrimario.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.NaoPermite ||
                     resultPrimario.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.Nenhum)
            {
                solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. O tipo de serviço não permite que a solicitação seja reaberta.";
            }

            /*
				3. Senão, se o serviço permite reabrir todas as solicitações exceto as finalizadas com sucesso e a situação atual da solicitação for igual a finalizada com sucesso, o botão deverá ser desabilitado com a seguinte mensagem informativa:
					"Opção indisponível. Não é possível reabrir esse tipo de solicitação quando a mesma foi finalizada com sucesso.”
			*/
            else if (resultPrimario.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.PermiteExcetoFinalizadaComSucesso &&
                     solicitacaoServico.SituacaoAtualSolicitacaoEncerradaComSucesso)
            {
                solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. Não é possível reabrir esse tipo de solicitação quando a mesma foi finalizada com sucesso.";
            }

            /*
				4. Senão, se o serviço permite reabrir todas as solicitações OU todas as solicitações exceto as finalizadas com sucesso e a situação atual da solicitação é diferente de atualizada com sucesso, verificar se:
			*/
            else if (resultPrimario.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.PermiteTodas || (
                     resultPrimario.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.PermiteExcetoFinalizadaComSucesso && !solicitacaoServico.SituacaoAtualSolicitacaoEncerradaComSucesso))
            {
                /*
					4.1. Senão, se o usuário logado não possui acesso ao [token do respectivo serviço]*, então o botão deve ser desabilitado com a seguinte mensagem informativa:
						“Opção indisponível. O usuário logado não possui permissão para reabrir esse tipo de solicitação.”
				*/
                if (solicitacaoServico.UsuarioNaoPossuiAcessoARealizarAtendimento)
                {
                    solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                    solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. O usuário logado não possui permissão para reabrir esse tipo de solicitação.";
                }

                /*
					4.2. Se a situação atual da solicitação for diferente de "Encerrado", o botão deverá ser desabilitado com a seguinte mensagem informativa:
                        "Opção indisponível. A reabertura de uma solicitação aplica-se somente a solicitações que a categoria da situação atual seja igual a Encerrado.".
				*/
                else if (solicitacaoServico.SituacaoAtualSolicitacaoDiferenteEncerrada)
                {
                    solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                    solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. A reabertura de uma solicitação aplica-se somente a solicitações que a categoria da situação atual seja igual a Encerrado.";
                }

                /*
					4.3. Senão, se a etapa atual da solicitação estiver parametrizada permitindo que seja compartilhada e o usuário logado não está associado a entidade compartilhada da solicitação, então o botão deve ser desabilitado com a seguinte mensagem informativa:
						"Opção indisponível. A etapa atual da solicitação está compartilhada e o usuário logado não está associado a entidade compartilhada com a solicitação.".
				*/
                else if (solicitacaoServico.SolicitacaoComEtapaAtualCompartilhadaEUsuarioNaoAssociado)
                {
                    solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                    solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. A etapa atual da solicitação está compartilhada e o usuário logado não está associado a entidade compartilhada com a solicitação.";
                }

                /*
					4.4. Senão, se a etapa atual da solicitação não estiver parametrizada como etapa compartilhada e, a entidade do usuário logado não é a entidade responsável da solicitação, então o botão deve ser desabilitado com a seguinte mensagem informativa:
						"Opção indisponível.  A etapa atual da solicitação não está compartilhada e, o usuário logado não está associado à entidade responsável da solicitação."
				*/
                else if (solicitacaoServico.SolicitacaoComEtapaAtualNaoCompartilhadaEEntidadeUsuarioLogadoNaoResponsavel)
                {
                    solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                    solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível.  A etapa atual da solicitação não está compartilhada e, o usuário logado não está associado à entidade responsável da solicitação.";
                }

                /*
					4.5. Senão, se o [prazo para reabrir a solicitação expirou]*, o botão deverá ser desabilitado com a seguinte mensagem informativa:
						“Opção indisponível. O prazo permitido para reabrir a solicitação após o encerramento expirou.”
				*/
                else if (solicitacaoServico.PrazoParaReabrirSolicitacaoExpirado)
                {
                    solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                    solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. O prazo permitido para reabrir a solicitação após o encerramento expirou.";
                }

                /*
					4.6. Senão, se a situação do solicitante não está de acordo com as [situações parametrizadas]*, o botão deverá ser desabilitado com a seguinte mensagem informativa:
						"Opção indisponível. Para o tipo de serviço da solicitação, a situação atual do solicitante não permite que a solicitação seja reaberta."
				*/
                else if (solicitacaoServico.SituacaoSolicitanteNaoPermiteReabrirSolicitacao)
                {
                    solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                    solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. Para o tipo de serviço da solicitação, a situação atual do solicitante não permite que a solicitação seja reaberta.";
                }

                /*
					4.7. Senão, se a pessoa-atuação possui pelo menos um bloqueio com situação “Bloqueado”, cuja data é menor ou igual a data corrente do sistema, está parametrizado para a etapa atual e que impede o início da etapa, o botão deverá ser desabilitado com a seguinte mensagem informativa:
						"Opção indisponível. Há bloqueio(s) associado(s) ao solicitante que impede(m) a reabertura.”.
				*/
                else if (solicitacaoServico.SolicitantePossuiBloqueiosVigentes)
                {
                    solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                    solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. Há bloqueio(s) associado(s) ao solicitante que impede(m) a reabertura.";
                }

                /*
					4.8. Senão, se a situação da etapa atual da solicitação for diferente de “Liberada”, o botão deverá ser desabilitado com a seguinte mensagem informativa:
						"Opção indisponível. A etapa atual da solicitação está diferente de liberada.”.
				*/
                else if (solicitacaoServico.SituacaoAtualEtapaDiferenteLiberada)
                {
                    solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                    solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. A etapa atual da solicitação está diferente de liberada.";
                }

                /*

					4.9. Senão, se o [período da etapa]* não está vigente, o botão deve ser desabilitado com a seguinte mensagem informativa:
						"Opção indisponível. O período para realização desse atendimento não está vigente.".
				*/
                else if (!solicitacaoServico.EtapaAtualVigente)
                {
                    solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                    solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = "Opção indisponível. O período para realização desse atendimento não está vigente.";
                }

                /*
					4.11. Quando o token do serviço for igual à SOLICITACAO_ALTERACAO_PLANO_ESTUDO, SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA ou SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA,
				*/
                else if (resultPrimario.TokenServico == TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO ||
                         resultPrimario.TokenServico == TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA ||
                         resultPrimario.TokenServico == TOKEN_SERVICO.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA)
                {
                    // Recupera os dados do plano de estudo
                    var dadosPlanoEstudoAtual = PlanoEstudoDomainService.SearchProjectionByKey(new PlanoEstudoFilterSpecification
                    {
                        SeqCicloLetivo = resultPrimario.SeqCicloLetivo,
                        Atual = true,
                        SeqAluno = resultPrimario.SeqPessoaAtuacao
                    }, x => new
                    {
                        SeqPlanoEstudo = x.Seq,
                        SeqSolicitacaoServico = x.SeqSolicitacaoServico,
                        DataCriacaoPlano = x.DataInclusao,
                        NumeroProtocolo = x.SolicitacaoServico.NumeroProtocolo
                    });

                    // 4.11.1. Verificar se o plano atual do aluno, do ciclo letivo do processo da solicitação em questão, possui solicitação associada.
                    if (dadosPlanoEstudoAtual.SeqSolicitacaoServico.HasValue)
                    {
                        // Caso possuir, verificar se a solicitação a ser reaberta é a solicitação associada ao plano OU
                        // se a data e hora da solicitação a ser reaberta é posterior à data e hora de criação do plano atual
                        //  4.11.1.1. Caso seja, permitir a reabertura;
                        //  4.11.1.2. Caso não seja, o comando deve ser desabilitado com a seguinte mensagem informativa:
                        if (seqSolicitacaoServico != dadosPlanoEstudoAtual.SeqSolicitacaoServico &&
                            resultPrimario.DataSolicitacao <= dadosPlanoEstudoAtual.DataCriacaoPlano)
                        {
                            solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                            solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = $"Opção indisponível. A solicitação {dadosPlanoEstudoAtual.NumeroProtocolo} foi atendida posteriormente a esta solicitação, para atualização no plano de estudos.";
                        }
                    }
                    // 4.11.2. Caso o plano não possua solicitação
                    else
                    {
                        // Verificar se a data e hora da solicitação a ser reaberta é posterior à data e hora de criação do plano de estudos atual, do ciclo letivo do processo da solicitação em questão.
                        // 4.11.2.1. Caso seja, permitir a reabertura;
                        // 4.11.2.2. Caso não seja, o comando deve ser desabilitado com a seguinte mensagem informativa:
                        if (resultPrimario.DataSolicitacao <= dadosPlanoEstudoAtual.DataCriacaoPlano)
                        {
                            solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                            solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = $"Opção indisponível. Foi realizada uma atualização no plano de estudo após o atendimento desta solicitação, por meio de acerto de dados.";
                        }
                    }
                }

                /*
                    4.10. Senão, se o serviço possui parametrização de restrição de solicitação e, para o solicitante há pelo menos uma [solicitação que esteja em aberto]* para os serviços parametrizados como restritivos, o botão deverá ser desabilitado com a seguinte mensagem informativa:
		            “Opção indisponível. Não é possível reabrir a solicitação, pois foram identificadas as seguintes solicitações em aberto: [Protocolo] - [Descrição Processo] - [Nro ordem Etapa atual +’ º Etapa’] - [Situação atual]".
		            O serviço dessas solicitações são restritivos comparado ao serviço da solicitação selecionada.
                */
                var restricoesSolicitacoesSimultaneas = this.BuscarRestricoesSolicitacaoSimultanea(resultPrimario.SeqServico, resultPrimario.SeqPessoaAtuacao);
                if (restricoesSolicitacoesSimultaneas.SMCAny())
                {
                    var restricoes = string.Empty;
                    foreach (var item in restricoesSolicitacoesSimultaneas)
                    {
                        if (item.SeqSolicitacaoServico != seqSolicitacaoServico)
                        {
                            restricoes += $"{item.NumeroProtocolo} - {item.Processo} - {item.Ordem}º Etapa - {item.SituacaoAtual} <br />";
                        }
                    }

                    if (!string.IsNullOrEmpty(restricoes))
                    {
                        solicitacaoServico.BotaoReabrirSolicitacaoHabilitado = false;
                        solicitacaoServico.MensagemInformativaBotaoReabrirSolicitacao = $"Opção indisponível. Não é possível reabrir a solicitação, pois foram identificadas as seguintes solicitações em aberto: {restricoes}";
                    }
                }
            }

            #endregion Regras para liberar ou não o botão de reabrir solicitação

            //Regras para formatação da mensagem de confirmação para retornar a etapa anterior
            solicitacaoServico.MensagemConfirmacaoRetornarEtapaAnterior = string.Format(MessagesResource.MSG_ConfirmacaoRetornarEtapaAnterior, solicitacaoServico.DescricaoEtapaAtual, solicitacaoServico.DescricaoEtapaAnterior);

            if (resultPrimario.TokenTipoServico == TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO)
            {
                if (etapaAnteriorSgf != null && etapaAnteriorSgf.Token == TOKENS_ETAPA_SGF.ASSINATURA_REGISTRO_DOCUMENTO)
                    solicitacaoServico.MensagemConfirmacaoRetornarEtapaAnterior = MessagesResource.MSG_ConfirmacaoRetornarEtapaAnteriorEmissaoDocumentoConclusao;
            }

            // Reconsidera filtro de dados para esta consulta
            this.PessoaAtuacaoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            this.PlanoEstudoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return solicitacaoServico;
        }

        public bool VerificarConfiguracaoPossuiSolicitacaoServicoEmAberto(long seqConfiguracaoProcesso)
        {
            #region Validação de solicitações em aberto levando em consideração a categoria da situaçao do histórico atual que seja Novo, Em Andamento ou Concluído

            var listaSolicitacoesEmAberto = this.SearchBySpecification(new SolicitacaoServicoFilterSpecification()
            {
                SeqConfiguracaoProcesso = seqConfiguracaoProcesso,
                CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento, CategoriaSituacao.Concluido }
            }).ToList();

            if (listaSolicitacoesEmAberto.Any())
                return true;

            #endregion Validação de solicitações em aberto levando em consideração a categoria da situaçao do histórico atual que seja Novo, Em Andamento ou Concluído

            #region Validação de solicitações em aberto levando em consideração a situação da etapa no SGF que não estejam como SituacaoFinalProcesso

            //var listaSolicitacoes = this.SearchBySpecification(new SolicitacaoServicoFilterSpecification() { SeqConfiguracaoProcesso = seqConfiguracaoProcesso }, x => x.SituacaoAtual).ToList();
            //var listaSeqsSituacaoEtapaSGF = listaSolicitacoes.Where(a => a.SeqSolicitacaoHistoricoSituacaoAtual.HasValue).Select(a => a.SituacaoAtual.SeqSituacaoEtapaSgf).Distinct().ToList();

            //foreach (var seqSituacaoEtapaSGF in listaSeqsSituacaoEtapaSGF)
            //{
            //    var situacaoAtualSolicitacao = this.EtapaService.BuscarSituacaoEtapa(seqSituacaoEtapaSGF);

            //    if (!situacaoAtualSolicitacao.SituacaoFinalProcesso)
            //        return true;
            //}

            #endregion Validação de solicitações em aberto levando em consideração a situação da etapa no SGF que não estejam como SituacaoFinalProcesso

            return false;
        }

        public SolicitacaoCobrancaTaxaVO PrepararModeloSolicitacaoCobrancaTaxa(long seqSolicitacaoServico)
        {
            var modelo = new SolicitacaoCobrancaTaxaVO()
            {
                ExisteTaxaAssociada = false,
                ExisteTaxaSemValor = false,
                ExisteTaxaComValorIncorreto = false,
                ExibeMensagemInformativaTaxasSemValor = false
            };

            var listaTaxasItens = new List<SolicitacaoCobrancaTaxaItemVO>();
            var spec = new SolicitacaoServicoBoletoTaxaFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
            decimal valorTotalTaxas = (decimal)0.0;

            int codNucleo = 0;

            try
            {
                var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
                {
                    TipoEmissaoTaxa = x.TipoEmissaoTaxa,
                    DescricaoCursoOfertaLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                    SeqLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade,
                    NomeLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    SeqPaiLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.SeqEntidade,
                    NomePaiLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.Entidade.Nome,
                    CodigoSEOPaiLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.Entidade.CodigoUnidadeSeo
                });

                if (dadosSolicitacao.TipoEmissaoTaxa.HasValue)
                    modelo.TipoEmissaoTaxa = dadosSolicitacao.TipoEmissaoTaxa.Value;

                if (dadosSolicitacao.CodigoSEOPaiLocalidade.HasValue)
                {
                    codNucleo = dadosSolicitacao.CodigoSEOPaiLocalidade.Value;
                    modelo.CodigoNucleo = dadosSolicitacao.CodigoSEOPaiLocalidade.Value;
                }
            }
            catch (Exception)
            {
                codNucleo = 0;
            }

            var taxasSolicitacaoServico = this.SolicitacaoServicoBoletoTaxaDomainService.SearchBySpecification(spec).ToList();

            if (taxasSolicitacaoServico.Any())
            {
                var todosValoresTaxas = this.IntegracaoFinanceiroService.ConsultarValoresTaxasView(taxasSolicitacaoServico.Select(a => a.SeqTaxaGra).Distinct().ToList());

                var taxas = todosValoresTaxas.OrderBy(o => o.SeqTaxa).Select(a => new
                {
                    a.SeqTaxa,
                    a.DescricaoTaxa
                }).SMCDistinct(a => a.SeqTaxa).ToList();

                foreach (var taxa in taxas)
                {
                    SolicitacaoCobrancaTaxaItemVO solicitacaoCobrancaTaxaItem = new SolicitacaoCobrancaTaxaItemVO()
                    {
                        DescricaoTaxa = taxa.DescricaoTaxa
                    };

                    var taxaNucleo = todosValoresTaxas.FirstOrDefault(a => a.SeqTaxa == taxa.SeqTaxa && a.CodigoNucleo == codNucleo);

                    if (taxaNucleo != null)
                    {
                        solicitacaoCobrancaTaxaItem.ValorTaxa = taxaNucleo.ValorTaxa.SMCMoeda();
                        valorTotalTaxas += taxaNucleo.ValorTaxa;

                        string valorTaxa = taxaNucleo.ValorTaxa.ToString().Replace(",", "").Replace(".", "");

                        //Verificando se o valor da taxa é igual ou menor que 0 (zero)
                        if (valorTaxa.All(a => a == '0') || taxaNucleo.ValorTaxa < (decimal)0)
                            modelo.ExisteTaxaComValorIncorreto = true;
                    }
                    else
                    {
                        solicitacaoCobrancaTaxaItem.ValorTaxa = "-";
                        modelo.ExisteTaxaSemValor = true;
                        modelo.ExibeMensagemInformativaTaxasSemValor = true;
                    }

                    listaTaxasItens.Add(solicitacaoCobrancaTaxaItem);
                }

                if (listaTaxasItens.Any())
                {
                    modelo.ExisteTaxaAssociada = true;
                    modelo.Taxas = listaTaxasItens;
                    modelo.ValorTotalTaxas = valorTotalTaxas;
                    modelo.Taxas = modelo.Taxas.OrderBy(o => o.DescricaoTaxa).ToList();
                }
            }

            return modelo;
        }

        public void AtualizarSolicitacaoServicoTipoEmissaoTaxa(long seqSolicitacaoServico, TipoEmissaoTaxa tipoEmissaoTaxa)
        {
            var solicitacaoServico = this.SearchByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico));
            solicitacaoServico.TipoEmissaoTaxa = tipoEmissaoTaxa;

            this.UpdateFields(solicitacaoServico, p => p.TipoEmissaoTaxa);
        }

        public long ProcedimentosReemissaoTitulo(long seqTitulo, long seqTaxa, long seqServico, long seqSolicitacaoServico)
        {
            long seqNovoTitulo = 0;

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    //Busca os dados da solicitação e do título
                    var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
                    {
                        x.SeqPessoaAtuacao,
                        x.NumeroProtocolo,
                        DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                        DescricaoPessoaAtuacao = x.PessoaAtuacao.Descricao,
                    });

                    var boletoCrei = new BoletoData();
                    var solicitacaoServicoBoletoTitulo = new SolicitacaoServicoBoletoTitulo();

                    if (seqTitulo > 0)
                    {
                        boletoCrei = this.IntegracaoFinanceiroService.BuscarBoletoCrei(new BoletoFiltroData()
                        {
                            SeqTitulo = (int?)seqTitulo,
                            Crei = true,
                            Sistema = SistemaBoleto.SGA
                        });

                        //1. O título atual deverá ser:
                        //· Cancelado no sistema financeiro e,
                        //· Cancelado no histórico de títulos da solicitação de serviço e,
                        //· O respectivo bloqueio deverá ser desbloqueado com a data corrente(hoje) e, o tipo do desbloqueio
                        //deve ser igual a "Efetivo" e Justificativa igual a "Título cancelado"

                        //CANCELANDO O TÍTULO NO FINANCEIRO
                        this.IntegracaoFinanceiroService.CancelarTitulo(new CancelarTituloData()
                        {
                            SeqTitulo = (int)seqTitulo,
                            UsuarioOperacao = SMCContext.User.SMCGetNome()
                        });

                        //CANCELANDO O TÍTULO NO HISTÓRICO DE TÍTULO DA SOLICITAÇÃO DE SERVIÇO
                        var specTituloSolicitacao = new SolicitacaoServicoBoletoTituloFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico, SeqTituloGra = (int?)seqTitulo };
                        solicitacaoServicoBoletoTitulo = this.SolicitacaoServicoBoletoTituloDomainService.SearchByKey(specTituloSolicitacao);
                        solicitacaoServicoBoletoTitulo.DataCancelamento = DateTime.Now;

                        this.SolicitacaoServicoBoletoTituloDomainService.UpdateFields(solicitacaoServicoBoletoTitulo, p => p.DataCancelamento);

                        //DESBLOQUEANDO O BLOQUEIO
                        var bloqueioTaxaServicoAcademico = PessoaAtuacaoBloqueioDomainService.SearchByKey(new PessoaAtuacaoBloqueioFilterSpecification
                        {
                            CodigoIntegracaoSistemaOrigem = seqTitulo.ToString(),
                            TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.PAGAMENTO_TAXA_SERVICO_ACADEMICO_EM_ABERTO,
                            SituacaoBloqueio = SituacaoBloqueio.Bloqueado
                        }, x => x.Itens);

                        if (bloqueioTaxaServicoAcademico != null)
                        {
                            bloqueioTaxaServicoAcademico.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                            bloqueioTaxaServicoAcademico.DataDesbloqueioEfetivo = DateTime.Now;
                            bloqueioTaxaServicoAcademico.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                            bloqueioTaxaServicoAcademico.JustificativaDesbloqueio = "Título cancelado";
                            bloqueioTaxaServicoAcademico.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;

                            foreach (var itemBloqueio in bloqueioTaxaServicoAcademico.Itens)
                            {
                                itemBloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                itemBloqueio.DataDesbloqueio = DateTime.Now;
                                itemBloqueio.UsuarioDesbloqueio = SMCContext.User.Identity.Name;
                            }

                            PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueioTaxaServicoAcademico);
                        }
                    }
                    else
                    {
                        var taxa = this.IntegracaoFinanceiroService.BuscarTaxa((int)seqTaxa);

                        boletoCrei = new BoletoData()
                        {
                            Taxas = new List<BoletoTaxaData>()
                            {
                                new TaxaCreiData() { Descricao = taxa.DescricaoTaxa }
                            }
                        };

                        var solicitacaoServicoBoleto = this.SolicitacaoServicoBoletoDomainService.SearchBySpecification(new SolicitacaoServicoBoletoFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico }).FirstOrDefault();
                        solicitacaoServicoBoletoTitulo.SeqSolicitacaoServicoBoleto = solicitacaoServicoBoleto.Seq;
                    }

                    //2. Deverá ser gerado o novo título e o respectivo bloqueio financeiro, conforme a: RN_SRC_115 - Solicitação Padrão - Geração título financeiro
                    var taxasBoleto = boletoCrei.Taxas.TransformList<TaxaCreiData>();
                    var descricaoTaxaTitulo = taxasBoleto.FirstOrDefault()?.Descricao;

                    var specTaxaSolicitacao = new SolicitacaoServicoBoletoTaxaFilterSpecification() { SeqSolicitacaoServico = seqSolicitacaoServico };
                    var taxasSolicitacaoServico = this.SolicitacaoServicoBoletoTaxaDomainService.SearchBySpecification(specTaxaSolicitacao).ToList();

                    if (taxasSolicitacaoServico.Any())
                    {
                        var todosValoresTaxas = this.IntegracaoFinanceiroService.ConsultarValoresTaxasView(taxasSolicitacaoServico.Select(a => a.SeqTaxaGra).Distinct().ToList());

                        var taxas = todosValoresTaxas.OrderBy(o => o.SeqTaxa).Select(a => new
                        {
                            a.SeqTaxa,
                            a.DescricaoTaxa
                        }).SMCDistinct(a => a.SeqTaxa).ToList();

                        var seqTaxaTitulo = taxas.FirstOrDefault(a => a.DescricaoTaxa == descricaoTaxaTitulo)?.SeqTaxa;

                        var modeloGeracaoTitulo = MontarDadosGeracaoTitulo(seqSolicitacaoServico);
                        modeloGeracaoTitulo.SeqTaxa = seqTaxaTitulo;

                        DadosGeracaoTituloBloqueiosVO dadosGerais = new DadosGeracaoTituloBloqueiosVO()
                        {
                            SeqSolicitacaoServico = seqSolicitacaoServico,
                            SeqSolicitacaoServicoBoleto = solicitacaoServicoBoletoTitulo.SeqSolicitacaoServicoBoleto,
                            SeqPessoaAtuacao = dadosSolicitacao.SeqPessoaAtuacao,
                            NumeroProtocolo = dadosSolicitacao.NumeroProtocolo,
                            DescricaoProcesso = dadosSolicitacao.DescricaoProcesso,
                            DescricaoPessoaAtuacao = dadosSolicitacao.DescricaoPessoaAtuacao
                        };

                        seqNovoTitulo = GerarTituloBloqueios(modeloGeracaoTitulo, dadosGerais);
                    }

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }

            return seqNovoTitulo;
        }

        #region [ Dados Relatório Solicitações Com Bloqueios ]

        public List<DadosRelatorioSolicitacoesBloqueioVO> BuscarDadosRelatorioSolicitacoesBloqueio(RelatorioSolicitacoesBloqueioFiltroVO filtro)
        {
            var servico = this.ServicoDomainService.SearchByKey(new SMCSeqSpecification<Servico>(filtro.SeqServico.Value));

            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(servico.SeqTemplateProcessoSgf).ToList();

            List<long> listaSituacoesSGFFinalizadasComSucesso = etapasSGF.SelectMany(a => a.Situacoes)
                .Where(a => a.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                .Select(a => a.Seq).Distinct().ToList();

            string valueSeqsProcessos = $"'{string.Join(",", filtro.SeqsProcessos)}'";
            string valueSeqProcessoEtapa = filtro.SeqProcessoEtapa.HasValue ? filtro.SeqProcessoEtapa.Value.ToString() : "NULL";
            string valueSeqsSituacoes = $"'{string.Join(",", listaSituacoesSGFFinalizadasComSucesso)}'";

            //TENTEI DE TODAS AS MANEIRAS ENVIAR PARAMETRO NULO COM SMCFUNCPARAMETER MAS NÃO DEU CERTO
            var listaCompleta = this.RawQuery<DadosRelatorioSolicitacoesBloqueioCompletoVO>
                                (string.Format(QUERY_RELATORIO_SOLICITACOES_COM_BLOQUEIO,
                                valueSeqsProcessos,
                                valueSeqProcessoEtapa,
                                valueSeqsSituacoes));

            var lista = listaCompleta.OrderBy(o => o.Processo).Select(a => new DadosRelatorioSolicitacoesBloqueioVO()
            {
                Seq = a.SeqProcesso,
                Processo = a.Processo
            }).SMCDistinct(a => a.Seq).ToList();

            foreach (var itemSolicitacao in lista)
            {
                var listaConsultaBloqueios = listaCompleta.Where(a => a.Processo == itemSolicitacao.Processo).OrderBy(o => o.Solicitante).ToList();
                var listaBloqueios = new List<DadosRelatorioSolicitacoesBloqueioItemVO>();

                foreach (var itemBloqueio in listaConsultaBloqueios)
                {
                    var bloqueio = new DadosRelatorioSolicitacoesBloqueioItemVO()
                    {
                        Seq = itemBloqueio.SeqProcesso,
                        NumeroProtocolo = itemBloqueio.NumeroProtocolo,
                        SeqPessoaAtuacao = itemBloqueio.SeqPessoaAtuacao,
                        Solicitante = itemBloqueio.Solicitante,
                        EtapaAtual = $"{itemBloqueio.OrdemEtapa}ª Etapa",
                        TipoBloqueio = itemBloqueio.TipoBloqueio,
                        MotivoBloqueio = itemBloqueio.MotivoBloqueio,
                        Referente = itemBloqueio.Referente,
                        DataBloqueio = itemBloqueio.DataBloqueio,
                        ImpedeInicioEtapa = itemBloqueio.ImpedeInicioEtapa,
                        ImpedeFimEtapa = itemBloqueio.ImpedeFimEtapa
                    };

                    var etapaAtualSGF = etapasSGF.FirstOrDefault(e => e.Seq == itemBloqueio.SeqEtapaSGF);
                    var situacaoAtualSGF = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.Seq == itemBloqueio.SeqSituacaoEtapaSGF);

                    var teste = this.SearchProjectionByKey(itemSolicitacao.Seq, x => new
                    {
                        x.ConfiguracaoProcesso,
                        x.Etapas,
                        x.PessoaAtuacaoBloqueios
                    });
                    var numero = teste.PessoaAtuacaoBloqueios.FirstOrDefault().Seq;
                    var teste2 = ConfiguracaoEtapaBloqueioDomainService.SearchByKey(numero);

                    bloqueio.SituacaoAtual = situacaoAtualSGF != null ? $"{SMCEnumHelper.GetDescription(situacaoAtualSGF.CategoriaSituacao)} - {situacaoAtualSGF.Descricao}" : MessagesResource.MSG_NaoExisteSituacaoEtapaSGF;

                    listaBloqueios.Add(bloqueio);
                }

                itemSolicitacao.Bloqueios = listaBloqueios;
            }

            return lista;
        }

        #endregion [ Dados Relatório Solicitações Com Bloqueios ]

        public void FinalizarSolicitacaoDiplomaDigital(long SeqSolicitacaoDocumentoConclusao, string tokenTipoDocumentoAcademico)
        {
            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(SeqSolicitacaoDocumentoConclusao);
            var solicitacaoServico = this.SearchProjectionByKey(specSolicitacaoServico, s => new
            {
                SeqSolicitacaoServico = s.Seq,
                DescricaoProcesso = s.ConfiguracaoProcesso.Processo.Descricao,
                s.NumeroProtocolo,
                NomeSocialSolicitante = s.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = s.PessoaAtuacao.DadosPessoais.Nome,
                CodigoMigracao = s.AlunoHistorico.Aluno.CodigoAlunoMigracao,
                Emails = s.PessoaAtuacao.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(d => d.EnderecoEletronico.Descricao).ToList(),
                SeqTemplateProcessoSGF = s.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.Token,
                s.SituacaoAtual.SeqSituacaoEtapaSgf,
                s.SituacaoAtual.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                Etapas = s.ConfiguracaoProcesso.Processo.Etapas.Select(a => new
                {
                    a.SeqEtapaSgf,
                    Paginas = a.ConfiguracoesEtapa.SelectMany(b => b.ConfiguracoesPagina).ToList()
                })
            });

            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(solicitacaoServico.SeqTemplateProcessoSGF);
            var etapaAtual = etapasSGF.FirstOrDefault(f => f.Seq == solicitacaoServico.SeqEtapaSgf);
            var situacao = etapaAtual.Situacoes.FirstOrDefault(f => f.Seq == solicitacaoServico.SeqSituacaoEtapaSgf);
            var proximaEtapa = etapasSGF.Where(w => w.Seq != solicitacaoServico.SeqEtapaSgf && w.Ordem > etapaAtual.Ordem).OrderBy(o => o.Ordem).FirstOrDefault();
            var proximaEtapaSGA = solicitacaoServico.Etapas.FirstOrDefault(f => f.SeqEtapaSgf == proximaEtapa.Seq);
            var proximaPaginaSGA = proximaEtapaSGA.Paginas.OrderBy(o => o.Ordem).FirstOrDefault().Seq;

            if (solicitacaoServico.Token.Equals(TOKEN_ETAPA_SOLICITACAO.ASSINATURA_DOCUMENTO_DIGITAL) && !situacao.SituacaoFinalEtapa)
            {
                using (var transacao = SMCUnitOfWork.Begin())
                {
                    var solicitacao = new SolicitacaoServico()
                    {
                        Seq = solicitacaoServico.SeqSolicitacaoServico,
                        DataSolucao = DateTime.Now
                    };
                    this.UpdateFields<SolicitacaoServico>(solicitacao, s => s.DataSolucao);

                    ProcedimentosFinalizarEtapa(solicitacaoServico.SeqSolicitacaoServico, solicitacaoServico.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);

                    var solicitacaoServicoEtapaAtual = this.SearchProjectionByKey(specSolicitacaoServico, s => new { s.SituacaoAtual.SeqSolicitacaoServicoEtapa });
                    var solicitacaoHistoricoNavegacao = new SolicitacaoHistoricoNavegacao()
                    {
                        SeqConfiguracaoEtapaPagina = proximaPaginaSGA,
                        SeqSolicitacaoServicoEtapa = solicitacaoServicoEtapaAtual.SeqSolicitacaoServicoEtapa,
                        DataEntrada = DateTime.Now,
                    };
                    this.SolicitacaoHistoricoNavegacaoDomainService.SaveEntity(solicitacaoHistoricoNavegacao);

                    var usuarioInclusao = string.Empty;
                    var sequencialUsuario = SMCContext.User?.SMCGetSequencialUsuario();
                    var nomeReduzido = SMCContext.User?.SMCGetNomeReduzido();

                    if (solicitacaoServico.CodigoMigracao.HasValue)
                    {
                        //Deverá ser enviada mensagem mobile para o aluno informando sobre a conclusão do diploma.
                        if (sequencialUsuario != null && nomeReduzido != null)
                            usuarioInclusao = $"{sequencialUsuario}/{nomeReduzido.ToUpper()}";
                        else
                            usuarioInclusao = SMCContext.User?.Identity?.Name;
                        var codigoMigracao = Convert.ToString(solicitacaoServico.CodigoMigracao);
                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoDiplomaDigitalFinalizado(usuarioInclusao, codigoMigracao, tokenTipoDocumentoAcademico);
                    }

                    var dadosMerge = new Dictionary<string, string>
                    {
                        { TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(solicitacaoServico.NomeSocialSolicitante) ? solicitacaoServico.NomeSolicitante : solicitacaoServico.NomeSocialSolicitante },
                        { TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, solicitacaoServico.DescricaoProcesso },
                        { TOKEN_TAG_NOTIFICACAO.NUM_PROTOCOLO, solicitacaoServico.NumeroProtocolo }
                    };

                    var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                    {
                        SeqSolicitacaoServico = solicitacaoServico.SeqSolicitacaoServico,
                        TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.COMUNICADO_ALUNO_CONCLUSAO_DOCUMENTO_DIGITAL,
                        DadosMerge = dadosMerge,
                        EnvioSolicitante = false,
                        ConfiguracaoPrimeiraEtapa = false,
                        Destinatarios = solicitacaoServico.Emails
                    };
                    SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);

                    transacao.Commit();
                }
            }
        }

        public string BuscarTokenEtapaAtualSolicitacao(long seqSolicitacaoServico)
        {
            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);
            var tokenEtapaAtualSolicitacao = this.SearchProjectionByKey(specSolicitacaoServico, x => x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.Token);

            return tokenEtapaAtualSolicitacao;
        }

        /// <summary>
        /// Atualizar o termo de entrega da documentação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequecial da solicitação de serviços</param>
        /// <param name="compromissoEntregaDocumentacao">Falg de compormisso de entrega da documentação</param>
        public void AtualizarTermoEntregaDocumentacao(long seqSolicitacaoServico, bool? compromissoEntregaDocumentacao)
        {
            var solicitacao = new SolicitacaoServico() { Seq = seqSolicitacaoServico, CompromissoEntregaDocumentacao = compromissoEntregaDocumentacao };
            this.UpdateFields(solicitacao, x => x.CompromissoEntregaDocumentacao);
        }

        /// <summary>
        /// Buscar se o termo de entraga de documentação foi aceito
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitacao serviço</param>
        /// <returns></returns>
        public bool BuscarTermoEntregaDocumentacaoFoiAceito(long seqSolicitacaoServico)
        {
            bool retorno = this.SearchProjectionByKey(seqSolicitacaoServico, p => p.CompromissoEntregaDocumentacao).GetValueOrDefault();

            return retorno;
        }

        private void SalvarDocumentosPessoaAtuacaoSolicitacaoDocumentoRequerido(SolicitacaoServico solicitacaoServico)
        {
            var specification = new PessoaAtuacaoDocumentoFilterSpecification() { SeqPessoaAtuacao = solicitacaoServico.SeqPessoaAtuacao };

            var docPessoaAtuacao = PessoaAtuacaoDocumentoDomainService.SearchBySpecification(specification);
            if (docPessoaAtuacao.Any())
            {
                foreach (var item in solicitacaoServico.DocumentosRequeridos)
                {
                    var documentoRequerido = DocumentoRequeridoDomainService.SearchByKey(item.SeqDocumentoRequerido);
                    var docPessoa = docPessoaAtuacao.FirstOrDefault(d => d.SeqTipoDocumento == documentoRequerido.SeqTipoDocumento);

                    if (docPessoa != null)
                    {
                        if (docPessoa.SeqSolicitacaoDocumentoRequerido.HasValue)
                        {
                            var solicitacaoDocPessoa = SolicitacaoDocumentoRequeridoDomainService.SearchByKey(docPessoa.SeqSolicitacaoDocumentoRequerido.Value);

                            item.SituacaoEntregaDocumento = solicitacaoDocPessoa.SituacaoEntregaDocumento;
                            item.FormaEntregaDocumento = solicitacaoDocPessoa.FormaEntregaDocumento;
                            item.VersaoDocumento = solicitacaoDocPessoa.VersaoDocumento;
                            item.DataEntrega = docPessoa.DataEntrega;
                            item.Observacao = solicitacaoDocPessoa.Observacao;
                            item.SeqArquivoAnexado = docPessoa.SeqArquivoAnexado;
                            item.DataPrazoEntrega = solicitacaoDocPessoa.DataPrazoEntrega;
                            item.DescricaoInconformidade = solicitacaoDocPessoa.DescricaoInconformidade;
                            item.EntregaPosterior = solicitacaoDocPessoa.EntregaPosterior;
                            item.ObservacaoSecretaria = docPessoa.Observacao;
                            item.EntregueAnteriormente = true;
                        }
                        else
                        {
                            item.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido;
                            item.FormaEntregaDocumento = docPessoa.SeqArquivoAnexado != null ? FormaEntregaDocumento.Upload : FormaEntregaDocumento.Nenhum;
                            item.VersaoDocumento = docPessoa.SeqArquivoAnexado != null ? VersaoDocumento.CopiaSimples : VersaoDocumento.Nenhum;
                            item.DataEntrega = docPessoa.DataEntrega;
                            item.ArquivoAnexado = docPessoa.ArquivoAnexado;
                            item.SeqArquivoAnexado = docPessoa.SeqArquivoAnexado;
                            item.EntregaPosterior = false;
                            item.ObservacaoSecretaria = docPessoa.Observacao;
                            item.EntregueAnteriormente = true;
                        }
                    }
                }
            }
        }

        public string BuscarDescricaoTipoDocumento(long seqTipoDocumento)
        {
            return TipoDocumentoService.BuscarDescricaoTipoDocumento(seqTipoDocumento);
        }

        /// <summary>
        /// Verificar se existe item no plano de estudo e sem historico lançado para componentes
        /// sendo dispensados ou aprovados
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação a ser verificada</param>
        /// <param name="itensVerificar">Lista de componentes curriculares para verificar</param>
        /// <returns>Lista de Plano de Estudo Item sem histórico lançado</returns>
        public List<PlanoEstudoItemSemHistoricoVO> VerificarTurmasPlanoEstudoSemHistorico(long seqPessoaAtuacao, List<HistoricoEscolarAprovadoFiltroVO> itensVerificar)
        {
            // Variável de retorno
            var retorno = new List<PlanoEstudoItemSemHistoricoVO>();

            // Busca os planos de estudo item de plano com ind_atual que não gerou histórico para o aluno
            var componentesSemHistorico = this.RawQuery<PlanoEstudoItemSemHistoricoVO>(QUERY_PLANO_ESTUDO_ITEM_SEM_HISTORICO_ESCOLAR,
                                                                                    new SMCFuncParameter("SEQ_PESSOA_ATUACAO", seqPessoaAtuacao));

            // Para cada item sem histórico, verifico se está sendo dispensado nesta solicitação
            foreach (var item in componentesSemHistorico)
            {
                if (!item.SeqComponenteCurricularAssunto.HasValue &&
                    itensVerificar.Any(d => d.SeqComponenteCurricular == item.SeqComponenteCurricular))
                {
                    retorno.Add(item);
                }
                if (item.SeqComponenteCurricularAssunto.HasValue &&
                    itensVerificar.Any(d => d.SeqComponenteCurricular == item.SeqComponenteCurricular &&
                                            d.SeqComponenteCurricularAssunto == item.SeqComponenteCurricularAssunto))
                {
                    retorno.Add(item);
                }
            }

            return retorno;
        }

        /// <summary>
        /// Verifica se existe alguma solicitação de alteração de plano de ensino conforme RN_APR_048 - Atualização de solicitações ativas
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno a ser verificado</param>
        /// <param name="itensVerificar">Itens de alteração de plano a serem verificados</param>
        public void VerificarSolicitacaoAlteracaoPlanoEmAberto(long seqPessoaAtuacao, List<HistoricoEscolarAprovadoFiltroVO> itensVerificar)
        {
            // - Realizar as seguintes ações, em todas as solicitações ativas* de um processo, cujo token do serviço seja 
            // SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA ou SOLICITACAO_ALTERACAO_PLANO_ESTUDO, em que a turma dispensada ou 
            // aprovada seja um item da solicitação: 
            // * Para encontrar solicitações ativas: verificar se a situação atual da solicitação possui categoria diferente
            // de “Encerrado”. 
            var spec = new SolicitacaoMatriculaFilterSpecification()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                CategoriasSituacao = new[] { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento, CategoriaSituacao.Concluido },
                TokensServico = new[] { TOKEN_SERVICO.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA, TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO }
            };
            var solAbertas = SolicitacaoMatriculaDomainService.SearchProjectionBySpecification(spec, s => new
            {
                SeqSolicitacaoServico = s.Seq,
                SeqConfiguracaoEtapa = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.Seq,
                s.DescricaoAtualizada,
                Itens = s.Itens.Select(i => new
                {
                    i.Seq,
                    i.SeqDivisaoTurma,
                    SeqTurma = (long?)i.DivisaoTurma.Turma.Seq,
                    ConfiguracoesComponente = i.DivisaoTurma.Turma.ConfiguracoesComponente.Select(c => new
                    {
                        c.SeqConfiguracaoComponente,
                        c.ConfiguracaoComponente.SeqComponenteCurricular,
                        ListaAssuntos = c.RestricoesTurmaMatriz.Select(r => r.SeqComponenteCurricularAssunto).ToList()
                    }).ToList(),
                }).ToList()
            }).ToList();

            foreach (var solicitacao in solAbertas)
            {
                // Busca a situação do item cancelada
                var situacoesItemMatricula = ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(solicitacao.SeqConfiguracaoEtapa), x => x.ProcessoEtapa.SituacoesItemMatricula).ToList();
                var sitCancelada = situacoesItemMatricula.Where(s => s.SituacaoFinal && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado).FirstOrDefault();

                bool itemAlterado = false;

                foreach (var itemValidar in itensVerificar)
                {
                    // Compara o componente/assunto
                    foreach (var item in solicitacao.Itens.Where(i => i.ConfiguracoesComponente.Any(c => c.SeqComponenteCurricular == itemValidar.SeqComponenteCurricular && (!itemValidar.SeqComponenteCurricularAssunto.HasValue || c.ListaAssuntos.Contains(itemValidar.SeqComponenteCurricularAssunto)))))
                    {
                        if (item.SeqTurma == itemValidar.SeqTurma)
                            continue;

                        // Altera a situação para cancelada por dispensa ou aprovação
                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(item.Seq, sitCancelada.Seq, MotivoSituacaoMatricula.PorDispensaAprovacao);
                        itemAlterado = true;
                    }
                }

                // Caso o campo "Descrição da solicitação atualizada" esteja preenchido, ele deve ser atualizado conforme:
                // RN_MAT_114 - Solicitação - Descrição original/atualizada
                if (itemAlterado && !string.IsNullOrEmpty(solicitacao.DescricaoAtualizada))
                    this.AtualizarDescricao(solicitacao.SeqSolicitacaoServico, false, true);
            }
        }


        public void SalvarSelecaoComponenteCurricular(long seqSolicitacaoServico, long seqDivisaoComponente)
        {
            var solicitacao = SolicitacaoTrabalhoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoTrabalhoAcademico>(seqSolicitacaoServico));
            if (solicitacao != null)
            {
                solicitacao.SeqDivisaoComponente = seqDivisaoComponente;
                SolicitacaoTrabalhoAcademicoDomainService.UpdateEntity(solicitacao);
            }
            else
            {
                SolicitacaoTrabalhoAcademicoDomainService.CriarSolicitacaoTrabalhoAcademicoPorSolicitacaoServico(seqSolicitacaoServico);
                var solicitacaoCriada = SolicitacaoTrabalhoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoTrabalhoAcademico>(seqSolicitacaoServico));
                if (solicitacaoCriada != null)
                {
                    solicitacaoCriada.SeqDivisaoComponente = seqDivisaoComponente;
                    SolicitacaoTrabalhoAcademicoDomainService.UpdateEntity(solicitacaoCriada);
                }
            }
        }
    }
}