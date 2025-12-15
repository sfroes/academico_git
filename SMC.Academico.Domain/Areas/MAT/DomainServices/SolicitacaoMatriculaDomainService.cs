using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.MAT.Exceptions;
using SMC.Academico.Common.Areas.MAT.Exceptions.Chancela;
using SMC.Academico.Common.Areas.MAT.Exceptions.Matricula;
using SMC.Academico.Common.Areas.MAT.Includes;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.TUR.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.Resources;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Helpers;
using SMC.Academico.Domain.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Financeiro.ServiceContract.BLT.Data;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Jobs.Service;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Security;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.PER.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using static iTextSharp.text.pdf.AcroFields;

namespace SMC.Academico.Domain.Areas.MAT.DomainServices
{
    public class SolicitacaoMatriculaDomainService : AcademicoContextDomain<SolicitacaoMatricula>
    {
        #region [ DomainServices ]

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService => Create<ConfiguracaoEtapaDomainService>();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();

        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService => Create<InstituicaoNivelTipoTermoIntercambioDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private AlunoHistoricoCicloLetivoDomainService AlunoHistoricoCicloLetivoDomainService => Create<AlunoHistoricoCicloLetivoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();

        private CampanhaOfertaDomainService CampanhaOfertaDomainService => Create<CampanhaOfertaDomainService>();

        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private ConfiguracaoProcessoDomainService ConfiguracaoProcessoDomainService => Create<ConfiguracaoProcessoDomainService>();

        private ContratoDomainService ContratoDomainService => Create<ContratoDomainService>();

        private CursoFormacaoEspecificaTitulacaoDomainService CursoFormacaoEspecificaTitulacaoDomainService => Create<CursoFormacaoEspecificaTitulacaoDomainService>();

        private DivisaoTurmaColaboradorDomainService DivisaoTurmaColaboradorDomainService => Create<DivisaoTurmaColaboradorDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private IngressanteHistoricoSituacaoDomainService IngressanteHistoricoSituacaoDomainService => Create<IngressanteHistoricoSituacaoDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private InstituicaoTipoEntidadeFormacaoEspecificaDomainService InstituicaoTipoEntidadeFormacaoEspecificaDomainService => Create<InstituicaoTipoEntidadeFormacaoEspecificaDomainService>();

        private MensagemDomainService MensagemDomainService => Create<MensagemDomainService>();

        private MotivoBloqueioDomainService MotivoBloqueioDomainService => Create<MotivoBloqueioDomainService>();

        private OrientacaoDomainService OrientacaoDomainService => Create<OrientacaoDomainService>();

        private OrientacaoPessoaAtuacaoDomainService OrientacaoPessoaAtuacaoDomainService => Create<OrientacaoPessoaAtuacaoDomainService>();

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService => Create<PessoaAtuacaoBeneficioDomainService>();

        private InstituicaoNivelBeneficioDomainService InstituicaoNivelBeneficioDomainService => Create<InstituicaoNivelBeneficioDomainService>();

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private PessoaAtuacaoEnderecoDomainService PessoaAtuacaoEnderecoDomainService => Create<PessoaAtuacaoEnderecoDomainService>();

        private PessoaAtuacaoTermoIntercambioDomainService PessoaAtuacaoTermoIntercambioDomainService => Create<PessoaAtuacaoTermoIntercambioDomainService>();

        private PeriodoIntercambioDomainService PeriodoIntercambioDomainService => Create<PeriodoIntercambioDomainService>();

        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();

        private PessoaEnderecoEletronicoDomainService PessoaEnderecoEletronicoDomainService => Create<PessoaEnderecoEletronicoDomainService>();

        private PessoaTelefoneDomainService PessoaTelefoneDomainService => Create<PessoaTelefoneDomainService>();

        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private ProcessoEtapaDomainService ProcessoEtapaDomainService => Create<ProcessoEtapaDomainService>();

        private ReferenciaFamiliarDomainService ReferenciaFamiliarDomainService => Create<ReferenciaFamiliarDomainService>();

        private RegistroDocumentoDomainService RegistroDocumentoDomainService => Create<RegistroDocumentoDomainService>();

        private SituacaoMatriculaDomainService SituacaoMatriculaDomainService => Create<SituacaoMatriculaDomainService>();

        private SolicitacaoHistoricoSituacaoDomainService SolicitacaoHistoricoSituacaoDomainService => Create<SolicitacaoHistoricoSituacaoDomainService>();

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();

        private SolicitacaoMatriculaItemHistoricoSituacaoDomainService SolicitacaoMatriculaItemHistoricoSituacaoDomainService => Create<SolicitacaoMatriculaItemHistoricoSituacaoDomainService>();

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService => Create<SolicitacaoServicoEtapaDomainService>();

        private TermoAdesaoDomainService TermoAdesaoDomainService => Create<TermoAdesaoDomainService>();

        private TipoTermoIntercambioDomainService TipoTermoIntercambioDomainService => Create<TipoTermoIntercambioDomainService>();

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();
        private TrabalhoAcademicoAutoriaDomainService TrabalhoAcademicoAutoriaDomainService => Create<TrabalhoAcademicoAutoriaDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

        private SituacaoItemMatriculaDomainService SituacaoItemMatriculaDomainService => Create<SituacaoItemMatriculaDomainService>();

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();

        private PessoaAtuacaoDocumentoDomainService PessoaAtuacaoDocumentoDomainService => Create<PessoaAtuacaoDocumentoDomainService>();

        private SolicitacaoDocumentoRequeridoDomainService SolicitacaoDocumentoRequeridoDomainService => Create<SolicitacaoDocumentoRequeridoDomainService>();

        private GrupoDocumentoRequeridoDomainService GrupoDocumentoRequeridoDomainService => Create<GrupoDocumentoRequeridoDomainService>();

        private EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();
        private InstituicaoNivelTipoTrabalhoDomainService InstituicaoNivelTipoTrabalhoDomainService => Create<InstituicaoNivelTipoTrabalhoDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();
        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService => Create<DocumentoRequeridoDomainService>();
        private TrabalhoAcademicoDivisaoComponenteDomainService TrabalhoAcademicoDivisaoComponenteDomainService => Create<TrabalhoAcademicoDivisaoComponenteDomainService>();
        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();
        private AplicacaoAvaliacaoDomainService AplicacaoAvaliacaoDomainService => Create<AplicacaoAvaliacaoDomainService>();
        private DivisaoComponenteDomainService DivisaoComponenteDomainService => Create<DivisaoComponenteDomainService>();
        private RequisitoDomainService RequisitoDomainService => Create<RequisitoDomainService>();
        private TermoIntercambioDomainService TermoIntercambioDomainService => Create<TermoIntercambioDomainService>();
        private ParceriaIntercambioTipoTermoDomainService ParceriaIntercambioTipoTermoDomainService => Create<ParceriaIntercambioTipoTermoDomainService>();

        #endregion [ DomainServices ]

        #region [ Services ]

        private IEtapaService EtapaService
        {
            get { return Create<IEtapaService>(); }
        }

        /// <summary>
        /// Integração com o SGA ANTIGO
        /// </summary>
        private IIntegracaoAcademicoService IntegracaoAcademicoService
        {
            get { return this.Create<IIntegracaoAcademicoService>(); }
        }

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService
        {
            get { return this.Create<IIntegracaoFinanceiroService>(); }
        }

        private IIntegracaoService IIntegracaoInscricaoService
        {
            get { return Create<IIntegracaoService>(); }
        }

        private INotificacaoService NotificacaoService
        {
            get { return Create<INotificacaoService>(); }
        }

        private IPerfilService PerfilService
        {
            get { return Create<IPerfilService>(); }
        }

        private DadosMestres.ServiceContract.Areas.GED.Interfaces.ITipoDocumentoService TipoDocumentoService
        {
            get { return this.Create<DadosMestres.ServiceContract.Areas.GED.Interfaces.ITipoDocumentoService>(); }
        }

        #endregion [ Services ]

        #region [ Queries ]

        #region [ _buscarProcessosOrientador ]

        private string _buscarProcessosOrientador =
            @"select distinct
                	pr.seq_processo as Seq,
                	pr.dsc_processo as Descricao
                from	src.processo pr
                join	src.processo_etapa pe
                		on pe.seq_processo = pr.seq_processo
                join	src.processo_etapa_filtro_dado pef
                		on pe.seq_processo_etapa = pef.seq_processo_etapa
                		and pef.idt_dom_filtro_dado = 1 -- orientador
                join	src.processo_unidade_responsavel pu
                		on pr.seq_processo = pu.seq_processo
                join	dct.colaborador_vinculo cv
                		on pu.seq_entidade = cv.seq_entidade_vinculo
                join	dct.colaborador c
                		on cv.seq_atuacao_colaborador = c.seq_pessoa_atuacao
                join	pes.pessoa_atuacao pa
                		on c.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                join	pes.pessoa p
                		on pa.seq_pessoa = p.seq_pessoa
                		and p.seq_usuario_sas = @SEQ_USUARIO_SAS
                join	src.configuracao_processo cp
                		on pr.seq_processo = cp.seq_processo
                join	src.solicitacao_servico ss
                		on cp.seq_configuracao_processo = ss.seq_configuracao_processo
                -- aluno
                left join	aln.aluno al
                			on ss.seq_pessoa_atuacao = al.seq_pessoa_atuacao
                -- ingresante
                left join	aln.ingressante i
                			on ss.seq_pessoa_atuacao = i.seq_pessoa_atuacao
                -- orientação
                join	ort.orientacao_pessoa_atuacao opa
                		on ss.seq_pessoa_atuacao = opa.seq_pessoa_atuacao
                join	ort.orientacao o
                		on opa.seq_orientacao = o.seq_orientacao
                		and ((	i.seq_pessoa_atuacao is not null
                				and getdate() between isnull(o.dat_inicio_orientacao, getdate()-1) and isnull(o.dat_fim_orientacao, getdate()+1))
                		or	 (	al.seq_pessoa_atuacao is not null
                				and getdate() between isnull(o.dat_inicio_orientacao, getdate()-1) and isnull(o.dat_fim_orientacao, getdate()+1))
                				and o.seq_tipo_orientacao in (select seq_tipo_orientacao from ort.tipo_orientacao where dsc_token = 'ORIENTACAO_CONCLUSAO_CURSO'))
                join	ort.orientacao_colaborador oc
                		on o.seq_orientacao = oc.seq_orientacao
                		and ((	i.seq_pessoa_atuacao is not null
                				and getdate() between isnull(oc.dat_inicio_orientacao, getdate()-1) and isnull(oc.dat_fim_orientacao, getdate()+1))
                		or	 (	al.seq_pessoa_atuacao is not null
                				and getdate() between isnull(oc.dat_inicio_orientacao, getdate()-1) and isnull(oc.dat_fim_orientacao, getdate()+1)))
                		and oc.seq_atuacao_colaborador = c.seq_pessoa_atuacao
                where	(	@IND_ATIVO = 0
                or		(	@IND_ATIVO = 1
                			and getdate() between pr.dat_inicio and pr.dat_fim))";

        #endregion [ _buscarProcessosOrientador ]

        #region [ _inserirSolicitacaoMatriculaPorSolicitacaoServico ]

        private string _inserirSolicitacaoMatriculaPorSolicitacaoServico =
                        @" INSERT INTO MAT.solicitacao_matricula (seq_solicitacao_servico) VALUES ({0})";

        #endregion [ _inserirSolicitacaoMatriculaPorSolicitacaoServico ]

        #region [ _selecionarAlunosRematricula ]

        private string _selecionarAlunosRematricula = @"

select
    ah.seq_entidade_vinculo as SeqEntidadeVinculo,
    ah.seq_atuacao_aluno as SeqPessoaAtuacao,
    isnull(pdp.nom_social, pdp.nom_pessoa) as NomeAluno,
    ah.seq_aluno_historico as SeqAlunoHistorico,
    ah.seq_nivel_ensino as SeqNivelEnsino,
    al.seq_tipo_vinculo_aluno as SeqTipoVinculoAluno,
    ah.seq_curso_oferta_localidade_turno as SeqCursoOfertaLocalidadeTurno,
    cu.seq_entidade_curso as SeqEntidadeCurso,
    p.seq_entidade_instituicao as SeqEntidadeInstituicaoEnsino,
    pe.seq_matriz_curricular_oferta as SeqMatrizCurriculaOferta,
    pr.seq_ciclo_letivo as SeqCicloLetivoProcesso
from	SRC.processo pr
join	SRC.processo_unidade_responsavel pru
        on pr.seq_processo = pru.seq_processo
		and pru.idt_dom_tipo_unidade_responsavel = 1 -- entidade responsável
join	ALN.aluno_historico ah
        on pru.seq_entidade = ah.seq_entidade_vinculo
        and ah.ind_atual = 1
join	ALN.aluno al
        on ah.seq_atuacao_aluno = al.seq_pessoa_atuacao
join	PES.pessoa_atuacao pa
        on al.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
join	PES.pessoa_dados_pessoais pdp
        on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
join	PES.pessoa p
        on pa.seq_pessoa = p.seq_pessoa
join	ORG.instituicao_nivel i
        on ah.seq_nivel_ensino = i.seq_nivel_ensino
        and p.seq_entidade_instituicao = i.seq_entidade_instituicao
join	ALN.instituicao_nivel_tipo_vinculo_aluno intv
        on i.seq_instituicao_nivel = intv.seq_instituicao_nivel
        and intv.seq_tipo_vinculo_aluno = al.seq_tipo_vinculo_aluno -- mari
left join	CSO.curso_oferta_localidade_turno colt
            on ah.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
left join	CSO.curso_oferta_localidade col
            on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
left join	CSO.curso_unidade cu
            on col.seq_entidade_curso_unidade = cu.seq_entidade
join	ALN.aluno_historico_ciclo_letivo ahcl
        on ah.seq_aluno_historico = ahcl.seq_aluno_historico
        and ahcl.dat_exclusao is null
left join	ALN.plano_estudo pe -- Lucas (Plano estudo)
            on ahcl.seq_aluno_historico_ciclo_letivo = pe.seq_aluno_historico_ciclo_letivo
            and pe.ind_atual = 1
join	CAM.ciclo_letivo cl
        on ahcl.seq_ciclo_letivo = cl.seq_ciclo_letivo
join	ALN.aluno_historico_situacao ahs
        on ahcl.seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
        and ahs.dat_exclusao is null
        and ahs.dat_inicio_situacao = (	select	MAX(dat_inicio_situacao)
            							from	ALN.aluno_historico_situacao
            							where	seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
                                        and		dat_exclusao is null
            							and		dat_inicio_situacao <= GETDATE())
join	SRC.servico_situacao_aluno ssa
        on pr.seq_servico = ssa.seq_servico
        and ahs.seq_situacao_matricula = ssa.seq_situacao_matricula
        and ssa.idt_dom_permissao_servico = 1 -- Criar solicitação
join	CAM.ciclo_letivo clpr
        on pr.seq_ciclo_letivo = clpr.seq_ciclo_letivo
join	CAM.regime_letivo rl
        on clpr.seq_regime_letivo = rl.seq_regime_letivo
where	pr.seq_processo = @SEQ_PROCESSO
-- situação de matrícula no ciclo letivo anterior ao do processo
and		ahcl.seq_ciclo_letivo = (	select	seq_ciclo_letivo
            						from	CAM.ciclo_letivo
            						where	seq_regime_letivo = clpr.seq_regime_letivo
									and		seq_entidade_instituicao = clpr.seq_entidade_instituicao
									and num_ciclo_letivo = case
            													when clpr.num_ciclo_letivo > 1 then clpr.num_ciclo_letivo - 1
            													else rl.num_itens_ano
            													end
            						and		ano_ciclo_letivo = case
            													when clpr.num_ciclo_letivo > 1 then clpr.ano_ciclo_letivo
            													else clpr.ano_ciclo_letivo - 1
            													end )
-- alunos cujo vinculo concedem formação
and	(	intv.ind_concede_formacao = 1
    -- ou alunos cujo vinculo não concede formação
    or ( intv.ind_concede_formacao = 0
		-- que possuem pelo menos um termo de intercâmbio (tabela ALN.pessoa_atuacao_termo_intercambio), cujo tipo esteja parametrizado por instituição, 
		-- nível e vínculo para conceder formação;
		and ( exists (
					select  1
					from    aln.pessoa_atuacao_termo_intercambio pati
					join    aln.termo_intercambio ti
							on  pati.seq_termo_intercambio = ti.seq_termo_intercambio
					join    aln.parceria_intercambio_tipo_termo pitt
							on  ti.seq_parceria_intercambio_tipo_termo = pitt.seq_parceria_intercambio_tipo_termo
					join    aln.instituicao_nivel_tipo_termo_intercambio intti
							on  pitt.seq_tipo_termo_intercambio = intti.seq_tipo_termo_intercambio
							and intv.seq_instituicao_nivel_tipo_vinculo_aluno = intti.seq_instituicao_nivel_tipo_vinculo_aluno
					where   intti.ind_concede_formacao = 1
					and		pati.seq_pessoa_atuacao = al.seq_pessoa_atuacao)
		-- que possuem apenas um termo de intercâmbio (tabela ALN.pessoa_atuacao_termo_intercambio), cujo tipo esteja parametrizado por instituição, nível 
		-- e vínculo para não conceder formação e data de fim do período de intercâmbio é maior que a data de início do ciclo letivo do processo de renovação
		or	exists (
					select	1
					from	aln.pessoa_atuacao_termo_intercambio pati
					join    aln.termo_intercambio ti
							on  pati.seq_termo_intercambio = ti.seq_termo_intercambio
					join    aln.parceria_intercambio_tipo_termo pitt
							on  ti.seq_parceria_intercambio_tipo_termo = pitt.seq_parceria_intercambio_tipo_termo
					join    aln.instituicao_nivel_tipo_termo_intercambio intti
							on  pitt.seq_tipo_termo_intercambio = intti.seq_tipo_termo_intercambio
							and intv.seq_instituicao_nivel_tipo_vinculo_aluno = intti.seq_instituicao_nivel_tipo_vinculo_aluno
					join	aln.periodo_intercambio pin
							on pati.seq_pessoa_atuacao_termo_intercambio = pin.seq_pessoa_atuacao_termo_intercambio, 
							CAM.fn_retorna_evento_ciclo_letivo (pr.seq_ciclo_letivo, ah.seq_curso_oferta_localidade_turno, null, 'PERIODO_CICLO_LETIVO') ev
					where   intti.ind_concede_formacao = 0
					and		pin.dat_fim > ev.dat_inicio_evento
					and		pati.seq_pessoa_atuacao = al.seq_pessoa_atuacao)
		)
	)
)
-- não existe historico-ciclo-letivo para o aluno no ciclo do processo
and		not exists(	select	1
        		    from	ALN.aluno_historico_ciclo_letivo ahcl1
        		    where	ahcl1.seq_aluno_historico = ah.seq_aluno_historico
        		    and		ahcl1.seq_ciclo_letivo = pr.seq_ciclo_letivo	)
order by isnull(pdp.nom_social, pdp.nom_pessoa)";

        #endregion [ _selecionarAlunosRematricula ]

        #region [ _buscaChancelasOrientadorTotal ]

        public string _buscaChancelasOrientadorTotal =
            @"select COUNT(*)

            from	src.solicitacao_servico ss
            join	src.configuracao_processo cp
            		on ss.seq_configuracao_processo = cp.seq_configuracao_processo
            join	src.processo pr
            		on cp.seq_processo = pr.seq_processo
            join	src.servico s
            		on pr.seq_servico = s.seq_servico
            join	src.processo_etapa pe
            		on pe.seq_processo = pr.seq_processo
            join	src.processo_etapa_filtro_dado pef
            		on pe.seq_processo_etapa = pef.seq_processo_etapa
            		and pef.idt_dom_filtro_dado = 1 -- orientador
            join	pes.pessoa_atuacao pa
            		on ss.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
            -- aluno
            left join	aln.aluno al
            			on pa.seq_pessoa_atuacao = al.seq_pessoa_atuacao
            -- ingresante
            left join	aln.ingressante i
            			on pa.seq_pessoa_atuacao = i.seq_pessoa_atuacao
            -- situação atual
            join	src.solicitacao_servico_etapa sse
            		on ss.seq_solicitacao_servico = sse.seq_solicitacao_servico
            join	src.solicitacao_historico_situacao hs
            		on sse.seq_solicitacao_servico_etapa = hs.seq_solicitacao_servico_etapa
            		and hs.dat_exclusao is null
            		and hs.dat_inclusao = (	select	max(hs2.dat_inclusao)
            								from	src.solicitacao_historico_situacao hs2
            								join	src.solicitacao_servico_etapa sse2
            										on hs2.seq_solicitacao_servico_etapa = sse2.seq_solicitacao_servico_etapa
            										and sse2.seq_solicitacao_servico = ss.seq_solicitacao_servico
                                            where   hs2.dat_exclusao is null)
            join	src.configuracao_etapa ce_a
            		on sse.seq_configuracao_etapa = ce_a.seq_configuracao_etapa
            join	src.processo_etapa pe_a
            		on ce_a.seq_processo_etapa = pe_a.seq_processo_etapa
            -- orientação
            join	ort.orientacao_pessoa_atuacao opa
            		on pa.seq_pessoa_atuacao = opa.seq_pessoa_atuacao
            join	ort.orientacao o
            		on opa.seq_orientacao = o.seq_orientacao
            		and ((	i.seq_pessoa_atuacao is not null
            				and getdate() between isnull(o.dat_inicio_orientacao, getdate()-1) and isnull(o.dat_fim_orientacao, getdate()+1))
            		or	 (	al.seq_pessoa_atuacao is not null
            				and getdate() between isnull(o.dat_inicio_orientacao, getdate()-1) and isnull(o.dat_fim_orientacao, getdate()+1))
            				and o.seq_tipo_orientacao in (select seq_tipo_orientacao from ort.tipo_orientacao where dsc_token = 'ORIENTACAO_CONCLUSAO_CURSO'))
            join	ort.orientacao_colaborador oc
            		on o.seq_orientacao = oc.seq_orientacao
            		and ((	i.seq_pessoa_atuacao is not null
            				and getdate() between isnull(oc.dat_inicio_orientacao, getdate()-1) and isnull(oc.dat_fim_orientacao, getdate()+1))
            		or	 (	al.seq_pessoa_atuacao is not null
            				and getdate() between isnull(oc.dat_inicio_orientacao, getdate()-1) and isnull(oc.dat_fim_orientacao, getdate()+1)))
            join	dct.colaborador c
            		on oc.seq_atuacao_colaborador = c.seq_pessoa_atuacao
            join	pes.pessoa_atuacao pa_c
            		on c.seq_pessoa_atuacao = pa_c.seq_pessoa_atuacao
            join	pes.pessoa p_c
            		on pa_c.seq_pessoa = p_c.seq_pessoa
            		and p_c.seq_usuario_sas = @SEQ_USUARIO_SAS
            where	(@SEQ_PROCESSO = 0 or pr.seq_processo = isnull(@SEQ_PROCESSO, pr.seq_processo))
            and	(	@IND_ATIVO = 0
            or		(	@IND_ATIVO = 1
            			and getdate() between pr.dat_inicio and pr.dat_fim)) and
						(@IND_CHANCELA = 0 or (@IND_CHANCELA = 1 and

						(select COUNT(seq_processo_etapa_filtro_dado)
							from src.processo_etapa_filtro_dado pefd_a
							where pefd_a.seq_processo_etapa = pe_a.seq_processo_etapa
							and pefd_a.idt_dom_filtro_dado = 1 --Orientador
							) > 0))
            and (@IND_CHANCELA = 0 or hs.seq_situacao_etapa_sgf in @SEQS_SITUACOES)";

        #endregion [ _buscaChancelasOrientadorTotal ]

        #region [ _buscarChancelasOrientadorTemplateProcesso]

        private string _buscarChancelasOrientadorTemplatesProcesso = @"select distinct

            s.seq_template_processo_sgf as SeqTemplateProcessoSGF

            from	src.solicitacao_servico ss
            join	src.configuracao_processo cp
            		on ss.seq_configuracao_processo = cp.seq_configuracao_processo
            join	src.processo pr
            		on cp.seq_processo = pr.seq_processo
            join	src.servico s
            		on pr.seq_servico = s.seq_servico
            join	src.processo_etapa pe
            		on pe.seq_processo = pr.seq_processo
            join	src.processo_etapa_filtro_dado pef
            		on pe.seq_processo_etapa = pef.seq_processo_etapa
            		and pef.idt_dom_filtro_dado = 1 -- orientador
            join	pes.pessoa_atuacao pa
            		on ss.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
            -- aluno
            left join	aln.aluno al
            			on pa.seq_pessoa_atuacao = al.seq_pessoa_atuacao
            -- ingresante
            left join	aln.ingressante i
            			on pa.seq_pessoa_atuacao = i.seq_pessoa_atuacao

            -- situação atual
            join	src.solicitacao_servico_etapa sse
            		on ss.seq_solicitacao_servico = sse.seq_solicitacao_servico
            join	src.solicitacao_historico_situacao hs
            		on sse.seq_solicitacao_servico_etapa = hs.seq_solicitacao_servico_etapa
            		and hs.dat_exclusao is null
            		and hs.dat_inclusao = (	select	max(hs2.dat_inclusao)
            								from	src.solicitacao_historico_situacao hs2
            								join	src.solicitacao_servico_etapa sse2
            										on hs2.seq_solicitacao_servico_etapa = sse2.seq_solicitacao_servico_etapa
            										and sse2.seq_solicitacao_servico = ss.seq_solicitacao_servico
                                            where   hs2.dat_exclusao is null)
            join	src.configuracao_etapa ce_a
            		on sse.seq_configuracao_etapa = ce_a.seq_configuracao_etapa
            join	src.processo_etapa pe_a
            		on ce_a.seq_processo_etapa = pe_a.seq_processo_etapa
            -- orientação
            join	ort.orientacao_pessoa_atuacao opa
            		on pa.seq_pessoa_atuacao = opa.seq_pessoa_atuacao
            join	ort.orientacao o
            		on opa.seq_orientacao = o.seq_orientacao
            		and ((	i.seq_pessoa_atuacao is not null
            				and getdate() between isnull(o.dat_inicio_orientacao, getdate()-1) and isnull(o.dat_fim_orientacao, getdate()+1))
            		or	 (	al.seq_pessoa_atuacao is not null
            				and getdate() between isnull(o.dat_inicio_orientacao, getdate()-1) and isnull(o.dat_fim_orientacao, getdate()+1))
            				and o.seq_tipo_orientacao in (select seq_tipo_orientacao from ort.tipo_orientacao where dsc_token = 'ORIENTACAO_CONCLUSAO_CURSO'))
            join	ort.orientacao_colaborador oc
            		on o.seq_orientacao = oc.seq_orientacao
            		and ((	i.seq_pessoa_atuacao is not null
            				and getdate() between isnull(oc.dat_inicio_orientacao, getdate()-1) and isnull(oc.dat_fim_orientacao, getdate()+1))
            		or	 (	al.seq_pessoa_atuacao is not null
            				and getdate() between isnull(oc.dat_inicio_orientacao, getdate()-1) and isnull(oc.dat_fim_orientacao, getdate()+1)))
            join	dct.colaborador c
            		on oc.seq_atuacao_colaborador = c.seq_pessoa_atuacao
            join	pes.pessoa_atuacao pa_c
            		on c.seq_pessoa_atuacao = pa_c.seq_pessoa_atuacao
            join	pes.pessoa p_c
            		on pa_c.seq_pessoa = p_c.seq_pessoa
            		and p_c.seq_usuario_sas = @SEQ_USUARIO_SAS
            where	(@SEQ_PROCESSO = 0 or pr.seq_processo = isnull(@SEQ_PROCESSO, pr.seq_processo))
            and	(	@IND_ATIVO = 0 or (	@IND_ATIVO = 1 and getdate() between pr.dat_inicio and pr.dat_fim))
            and (@IND_CHANCELA = 0 or (@IND_CHANCELA = 1 and
                    (select COUNT(seq_processo_etapa_filtro_dado)
                            from src.processo_etapa_filtro_dado pefd_a
							where pefd_a.seq_processo_etapa = pe_a.seq_processo_etapa
							and pefd_a.idt_dom_filtro_dado = 1 --Orientador
                            ) > 0))";

        #endregion [ _buscarChancelasOrientadorTemplateProcesso]

        #region [ _buscaChancelasOrientador ]

        private string _buscaChancelasOrientador =
            @"select distinct
            	-- processo
            	pr.seq_processo as SeqProcesso,
            	pr.dsc_processo as DescricaoProcesso,
            	-- etapa
            	case
            		when pe.idt_dom_situacao_etapa = 2 then cast(1 as bit)	-- liberada
            		else cast(0 as bit)
            	end as EtapaChancelaLiberada,
            	ce_a.seq_configuracao_etapa as SeqConfiguracaoEtapa,
            	pe_a.dsc_token as TokenEtapaSGF,
            	-- oferta de curso
            	isnull(co.dsc_curso_oferta, co_i.dsc_curso_oferta) as DescricaoOfertaCurso,
            	-- dados solicitacao
            	ss.seq_solicitacao_servico as Seq,
            	ss.num_protocolo as NumeroProtocolo,
            	ss.seq_pessoa_atuacao as SeqPessoaAtuacao,
            	dp.nom_social as NomeSocialSolicitante,
            	dp.nom_pessoa as NomeSolicitante,
            	case
            		when sm.cod_adesao is not null or pe_a.num_ordem >= 2 then cast(1 as bit)
            		else cast(0 as bit)
            	end as PermiteVisualizarPlanoEstudo,
            	-- situação
            	s.seq_template_processo_sgf as SeqTemplateProcessoSGF,
            	hs.seq_situacao_etapa_sgf as SeqSituacaoEtapaSGF,
                -- periodo chancela
                case
                    when ss.seq_grupo_escalonamento is not null then
                    (
            	        -- periodo escalonamento
                        select	convert(char(10), es.dat_inicio_escalonamento, 103) + ' a ' + convert(char(10), es.dat_fim_escalonamento, 103)
                        from	src.grupo_escalonamento ge
                        join	src.grupo_escalonamento_item gei
                                on ge.seq_grupo_escalonamento = gei.seq_grupo_escalonamento
                        join	src.escalonamento es
                                on gei.seq_escalonamento = es.seq_escalonamento
                                and es.seq_processo_etapa = pe.seq_processo_etapa
                        where	ge.seq_grupo_escalonamento = ss.seq_grupo_escalonamento
                    )
                    else convert(char(10), pe.dat_inicio_etapa, 103) + ' a ' + convert(char(10), pe.dat_fim_etapa, 103)
                end as PeriodoChancela,
            	-- chancela vigente
                case
                    when ss.seq_grupo_escalonamento is not null then
                    (
            	        -- escalonamento vigente
                        select
            			case
            				when getdate() between es2.dat_inicio_escalonamento and es2.dat_fim_escalonamento then cast(1 as bit)
            				else cast(0 as bit)
            			end
                        from	src.grupo_escalonamento ge2
                        join	src.grupo_escalonamento_item gei2
                                on ge2.seq_grupo_escalonamento = gei2.seq_grupo_escalonamento
                        join	src.escalonamento es2
                                on gei2.seq_escalonamento = es2.seq_escalonamento
                                and es2.seq_processo_etapa = pe.seq_processo_etapa
                        where	ge2.seq_grupo_escalonamento = ss.seq_grupo_escalonamento
                    )
                    when getdate() between pe.dat_inicio_etapa and pe.dat_fim_etapa then cast(1 as bit)
                    else cast(0 as bit)
                end as ChancelaVigente,
            	-- bloqueios
            	(
            		select	m.dsc_motivo_bloqueio as DescricaoMotivoBloqueio
            		from	src.configuracao_etapa_bloqueio ceb
            		join	pes.pessoa_atuacao_bloqueio pab
            				on ceb.seq_motivo_bloqueio = pab.seq_motivo_bloqueio
            				and pab.seq_pessoa_atuacao = ss.seq_pessoa_atuacao
            		join	pes.motivo_bloqueio m
            				on pab.seq_motivo_bloqueio = m.seq_motivo_bloqueio
            		where	ceb.seq_configuracao_etapa = ce.seq_configuracao_etapa
            		and		ceb.ind_impede_inicio_etapa = 1
            		and		pab.idt_dom_situacao_bloqueio = 1 -- bloqueado
            		and		pab.dat_bloqueio <= getdate()
            		for	json path
            	) as BloqueiosJson,
                --Usuário responsavel pelo atendimento
				(
            		select top 1 shus.seq_usuario_responsavel_sas as SeqUsuarioSas
            		from	src.solicitacao_historico_usuario_responsavel shus
            		where	shus.seq_solicitacao_servico = ss.seq_solicitacao_servico
            		order by shus.dat_inclusao desc
            	) as SeqUsuarioResponsavelAtendimentoSas,
                case
            		when (
							select COUNT(seq_processo_etapa_filtro_dado)
							from src.processo_etapa_filtro_dado pefd_a
							where pefd_a.seq_processo_etapa = pe_a.seq_processo_etapa
							and pefd_a.idt_dom_filtro_dado = 1 --Orientador
							) > 0 then cast(1 as bit)
            		else cast(0 as bit)
            	end as ProcessoEtapaFiltroOrientador
            from	src.solicitacao_servico ss
            join	mat.solicitacao_matricula sm
            		on ss.seq_solicitacao_servico = sm.seq_solicitacao_servico
            join	src.configuracao_processo cp
            		on ss.seq_configuracao_processo = cp.seq_configuracao_processo
            join	src.processo pr
            		on cp.seq_processo = pr.seq_processo
            join	src.servico s
            		on pr.seq_servico = s.seq_servico
            join	src.processo_etapa pe
            		on pe.seq_processo = pr.seq_processo
            join	src.processo_etapa_filtro_dado pef
            		on pe.seq_processo_etapa = pef.seq_processo_etapa
            		and pef.idt_dom_filtro_dado = 1 -- orientador
            join	src.configuracao_etapa ce
            		on pe.seq_processo_etapa = ce.seq_processo_etapa
            		and ce.seq_configuracao_processo = ss.seq_configuracao_processo
            join	pes.pessoa_atuacao pa
            		on ss.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
            join	pes.pessoa_dados_pessoais dp
            		on pa.seq_pessoa_dados_pessoais = dp.seq_pessoa_dados_pessoais
            -- aluno
            left join	aln.aluno al
            			on pa.seq_pessoa_atuacao = al.seq_pessoa_atuacao
            left join	aln.aluno_historico ah
            			on al.seq_pessoa_atuacao = ah.seq_atuacao_aluno
            			and ah.ind_atual = 1
            left join	cso.curso_oferta_localidade_turno colt
            			on ah.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
            left join	cso.curso_oferta_localidade col
            			on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
            left join	cso.curso_oferta co
            			on col.seq_curso_oferta = co.seq_curso_oferta
            -- ingresante
            left join	aln.ingressante i
            			on pa.seq_pessoa_atuacao = i.seq_pessoa_atuacao
            left join	aln.ingressante_oferta iof
            			on i.seq_pessoa_atuacao = iof.seq_atuacao_ingressante
            left join	cam.campanha_oferta_item coi
            			on iof.seq_campanha_oferta_item = coi.seq_campanha_oferta_item
            left join	cso.curso_oferta_localidade_turno colt_i
            			on coi.seq_curso_oferta_localidade_turno = colt_i.seq_curso_oferta_localidade_turno
            left join	cso.curso_oferta_localidade col_i
            			on colt_i.seq_entidade_curso_oferta_localidade = col_i.seq_entidade
            left join	cso.curso_oferta co_i
            			on col_i.seq_curso_oferta = co_i.seq_curso_oferta
            -- situação atual
            join	src.solicitacao_servico_etapa sse
            		on ss.seq_solicitacao_servico = sse.seq_solicitacao_servico
            join	src.solicitacao_historico_situacao hs
            		on sse.seq_solicitacao_servico_etapa = hs.seq_solicitacao_servico_etapa
            		and hs.dat_exclusao is null
            		and hs.dat_inclusao = (	select	max(hs2.dat_inclusao)
            								from	src.solicitacao_historico_situacao hs2
            								join	src.solicitacao_servico_etapa sse2
            										on hs2.seq_solicitacao_servico_etapa = sse2.seq_solicitacao_servico_etapa
            										and sse2.seq_solicitacao_servico = ss.seq_solicitacao_servico
                                            where   hs2.dat_exclusao is null)
            join	src.configuracao_etapa ce_a
            		on sse.seq_configuracao_etapa = ce_a.seq_configuracao_etapa
            join	src.processo_etapa pe_a
            		on ce_a.seq_processo_etapa = pe_a.seq_processo_etapa
            -- orientação
            join	ort.orientacao_pessoa_atuacao opa
            		on pa.seq_pessoa_atuacao = opa.seq_pessoa_atuacao
            join	ort.orientacao o
            		on opa.seq_orientacao = o.seq_orientacao
            		and ((	i.seq_pessoa_atuacao is not null
            				and getdate() between isnull(o.dat_inicio_orientacao, getdate()-1) and isnull(o.dat_fim_orientacao, getdate()+1))
            		or	 (	al.seq_pessoa_atuacao is not null
            				and getdate() between isnull(o.dat_inicio_orientacao, getdate()-1) and isnull(o.dat_fim_orientacao, getdate()+1))
            				and o.seq_tipo_orientacao in (select seq_tipo_orientacao from ort.tipo_orientacao where dsc_token = 'ORIENTACAO_CONCLUSAO_CURSO'))
            join	ort.orientacao_colaborador oc
            		on o.seq_orientacao = oc.seq_orientacao
            		and ((	i.seq_pessoa_atuacao is not null
            				and getdate() between isnull(oc.dat_inicio_orientacao, getdate()-1) and isnull(oc.dat_fim_orientacao, getdate()+1))
            		or	 (	al.seq_pessoa_atuacao is not null
            				and getdate() between isnull(oc.dat_inicio_orientacao, getdate()-1) and isnull(oc.dat_fim_orientacao, getdate()+1)))
            join	dct.colaborador c
            		on oc.seq_atuacao_colaborador = c.seq_pessoa_atuacao
            join	pes.pessoa_atuacao pa_c
            		on c.seq_pessoa_atuacao = pa_c.seq_pessoa_atuacao
            join	pes.pessoa p_c
            		on pa_c.seq_pessoa = p_c.seq_pessoa
            		and p_c.seq_usuario_sas = @SEQ_USUARIO_SAS
            where	(@SEQ_PROCESSO = 0 or pr.seq_processo = isnull(@SEQ_PROCESSO, pr.seq_processo))
            and	(	@IND_ATIVO = 0 or (	@IND_ATIVO = 1 and getdate() between pr.dat_inicio and pr.dat_fim))
            and (@IND_CHANCELA = 0 or (@IND_CHANCELA = 1 and
                    (select COUNT(seq_processo_etapa_filtro_dado)
                            from src.processo_etapa_filtro_dado pefd_a
							where pefd_a.seq_processo_etapa = pe_a.seq_processo_etapa
							and pefd_a.idt_dom_filtro_dado = 1 --Orientador
                            ) > 0))
            and (@IND_CHANCELA = 0 or hs.seq_situacao_etapa_sgf in @SEQS_SITUACOES)
            order by
            	pr.dsc_processo,
            	isnull(co.dsc_curso_oferta, co_i.dsc_curso_oferta),
            	dp.nom_pessoa,
            	dp.nom_social
            OFFSET     (@PAGINA -1) * @PORPAGINA ROWS
            FETCH NEXT @PORPAGINA ROWS ONLY";

        #endregion [ _buscaChancelasOrientador ]

        #endregion [ Queries ]

        private class _DadosComplexos
        {
            public List<string> NomesLocalidades { get; set; }
            public List<string> DescricoesModalidades { get; set; }
            public List<string> DescricoesOfertas { get; set; }
            public List<string> DescricoesTurnos { get; set; }
            public List<long> SeqsFormacoesEspecificas { get; set; }
            public long? SeqEntidadeResponsavelFormacaoEspecifica { get; set; }
            public bool Hibrido { get; set; }
        }

        private string DescricaoSituacao(SituacaoEtapaSolicitacaoMatricula situacaoEtapaIngressante)
        {
            switch (situacaoEtapaIngressante)
            {
                case SituacaoEtapaSolicitacaoMatricula.AguardandoPagamento:
                    return "Aguardando Pagamento";
                case SituacaoEtapaSolicitacaoMatricula.EmAndamento:
                    return "Em Andamento";
                case SituacaoEtapaSolicitacaoMatricula.Finalizada:
                    return "Finalizada";
                case SituacaoEtapaSolicitacaoMatricula.NaoFinalizada:
                    return "Não Finalizada";
                case SituacaoEtapaSolicitacaoMatricula.NaoIniciada:
                    return "Não Iniciada";
                default:
                    return "";
            }
        }


        public List<SolicitacaoMatriculaListaVO> BuscarSolicitacoesMatriculaLista(SolicitacaoMatriculaFilterSpecification spec)
        {
            /*	RN_MAT_027 - Filtro de ingressantes no login
				Buscar solicitações de serviço conforme os passos 1,2 e 3:

				1) Da pessoa que está logada
				2) Cujo tipo de serviço tenha um dos tokens "MATRICULA_INGRESSANTE" ou “MATRICULA_REABERTURA” ou "TRANSFERENCIA_INTERNA".
				3) Se o tipo da pessoa atuação for INGRESSANTE:
				Que esteja com a situação APTO_MATRICULA e a solicitação seja de um processo com data fim null ou futura, ou que esteja com a situação MATRICULADO, independente da vigência do processo.
				Se o tipo da pessoa atuação for ALUNO:
				Que esteja com o tipo da situação MATRICULADO, independente da vigência do processo.
				Se existir mais que uma pessoa-atuação acionar caso de uso UC_MAT_003_16_02 - Selecionar Ingressante.
				Se não existir nenhuma pessoa-atuação exibir mensagem abaixo e ao clicar em OK deslogar e retornar para a tela de login.
				Mensagem: "Acesso não permitido. Não existe processo seletivo apto para o processo de matrícula."*/

            // Considera apenas os aptos para matrícula
            spec.ApenasAptosParaMatricula = true;
            spec.ApenasProcessosAtuaisIngressante = true;
            spec.TokensTiposServico = new string[] { TOKEN_TIPO_SERVICO.MATRICULA_INGRESSANTE, TOKEN_TIPO_SERVICO.MATRICULA_REABERTURA, TOKEN_TIPO_SERVICO.TRANSFERENCIA_INTERNA };
            spec.MaxResults = int.MaxValue;
            spec.SetOrderByDescending(o => o.Seq);

            // Retorna a lista de ingressantes para Home
            var dados = this.SearchProjectionBySpecification(spec, x => new SolicitacaoMatriculaListaVO
            {
                SeqSolicitacaoServico = x.Seq,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                DescricaoFormaIngresso = (x.PessoaAtuacao as Ingressante).FormaIngresso.Descricao ?? (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).FormaIngresso.Descricao,
                DescricoesOfertas = (x.PessoaAtuacao as Ingressante).Ofertas.Select(y => y.CampanhaOferta.Descricao).ToList(),
                DescricaoProcesso = (x.PessoaAtuacao as Ingressante).ProcessoSeletivo.Descricao ?? x.ConfiguracaoProcesso.Processo.Descricao,
                DescricaoVinculo = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.Descricao ?? (x.PessoaAtuacao as Aluno).TipoVinculoAluno.Descricao,
                NomeInstituicaoEnsino = (x.PessoaAtuacao as Ingressante).ProcessoSeletivo.Campanha.EntidadeResponsavel.InstituicaoEnsino.Nome ?? x.EntidadeResponsavel.InstituicaoEnsino.Nome,
                SeqsTipoTermoIntercambioAssociados = x.PessoaAtuacao.TermosIntercambio.Select(t => t.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio),
                CicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Descricao,
                DataFimProcesso = x.ConfiguracaoProcesso.Processo.DataFim,
                SeqNivelEnsino = (long?)(x.PessoaAtuacao as Ingressante).SeqNivelEnsino ?? (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(a => a.Atual).SeqNivelEnsino,
                SeqTipoVinculo = (long?)(x.PessoaAtuacao as Ingressante).SeqTipoVinculoAluno ?? (x.PessoaAtuacao as Aluno).SeqTipoVinculoAluno,
                SeqInstituicao = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
                SeqProcesso = x.SeqConfiguracaoProcesso,
                SeqSituacaoAtual = x.SituacaoAtual.Seq,
                Etapas = x.Etapas.ToList()
            }).ToList();

            //NV02 - Exibir apenas registros que possuem processo de matrícula com a data fim igual ou maior que a data corrente do sistema
            dados = dados.Where(c => c.DataFimProcesso.HasValue && c.DataFimProcesso.Value.Date >= DateTime.Now.Date).ToList();

            foreach (var item in dados)
            {
                var etapaAtual = item.Etapas.Where(c => c.SeqSolicitacaoHistoricoSituacaoAtual == item.SeqSituacaoAtual).FirstOrDefault();

                if (etapaAtual != null)
                {
                    var etapa = SGFHelper.BuscarEtapas(item.SeqSolicitacaoServico).Where(c => c.SeqConfiguracaoEtapa == etapaAtual.SeqConfiguracaoEtapa).FirstOrDefault();

                    if (etapa != null)
                    {
                        item.DescricaoSituacao = $"{etapa.DescricaoEtapa.SMCSafeTrim()} - {DescricaoSituacao(etapa.SituacaoEtapaIngressante)}";
                    }
                }

                var specInstituicaoNivelTipoVinculoAluno = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
                {
                    SeqNivelEnsino = item.SeqNivelEnsino,
                    SeqTipoVinculoAluno = item.SeqTipoVinculo,
                    SeqInstituicao = item.SeqInstituicao
                };

                var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.SearchByKey(
                   specInstituicaoNivelTipoVinculoAluno, IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel_NivelEnsino);

                // Regra RN_ORG_049 (UC_MAT_003_16_02 - Selecionar Ingressante - NV06) Exibição campo apenas para Stricto
                // Exibir campos com NV06 (Ciclo Letivo e Vínculo) apenas para os níveis de ensino, de acordo com os tokens:
                // DOUTORADO_ACADEMICO / DOUTORADO_PROFISSIONAL / MESTRADO_ACADEMICO  / MESTRADO_PROFISSIONAL
                if ((instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO) ||
                    (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL) ||
                    (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO) ||
                    (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL))
                {
                    item.ExibirCicloLetivo = true;
                    item.DescricaoVinculo = PessoaAtuacaoDomainService.RecuperarDescricaoVinculo(item.SeqPessoaAtuacao, item.SeqsTipoTermoIntercambioAssociados, item.DescricaoVinculo);

                    item.ExibirVinculo = true;
                }

                // NV04 - Tirada do VO e passada para esta classe
                if (instituicaoNivelTipoVinculoAluno != null)
                {
                    item.ExigeCurso = instituicaoNivelTipoVinculoAluno.ExigeCurso;

                    //Versão antiga
                    //if (DescricoesOfertas != null)
                    //    return string.Join(", ", DescricoesOfertas?.ToArray());
                    //else
                    //    return string.Empty;

                    if (item.DescricoesOfertas != null)
                    {
                        if (instituicaoNivelTipoVinculoAluno.ExigeCurso)
                        {
                            item.DescricaoOferta = string.Join(", ", item.DescricoesOfertas?.ToArray());
                        }
                        else
                        {
                            if (item.DescricoesOfertas?.Count > 0)
                            {
                                item.DescricaoOferta += "<ul class='smc-sga-modal-curso-lista'>";

                                foreach (var itemDescricaoOferta in item.DescricoesOfertas)
                                {
                                    item.DescricaoOferta += $"<li>{itemDescricaoOferta}</li>";
                                }

                                item.DescricaoOferta += "</ul>";
                            }

                            //item.DescricaoOferta = string.Join("<br/>", item.DescricoesOfertas?.ToArray());
                        }
                    }
                    else
                    {
                        item.DescricaoOferta = string.Empty;
                    }
                }
            }

            return dados;
        }

        public (bool ExisteRematriculaAberta, long? SeqSolicitacaoServico, DateTime? DataInicioRematricula, DateTime? DataFimRematricula, TipoMatricula? TipoMatricula, string DescricaoProcesso) BuscarDadosRematricula(long? seqPessoaAtuacao, long? seqPessoa)
        {
            var situacoesMatriculaAberta = new CategoriaSituacao[] { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento };

            long? seqSolicitacaoMatriculaRenovacao = null;
            long? seqSolicitacaoMatriculaReabertura = null;
            string descricaoProcesso = null;

            if (seqPessoaAtuacao.HasValue)
            {
                var dadosSolicitacaoMatricula = PessoaAtuacaoDomainService.SearchProjectionByKey(seqPessoaAtuacao.Value, x => new
                {
                    DadosRenovacao = x.SolicitacoesServico.OfType<SolicitacaoMatricula>().Where(s => (s.ConfiguracaoProcesso.Processo.Servico.Token == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU || s.ConfiguracaoProcesso.Processo.Servico.Token == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_LATO_SENSU) && situacoesMatriculaAberta.Contains(s.SituacaoAtual.CategoriaSituacao) && s.ConfiguracaoProcesso.Processo.DataInicio <= DateTime.Now && (!s.ConfiguracaoProcesso.Processo.DataFim.HasValue || s.ConfiguracaoProcesso.Processo.DataFim >= DateTime.Now)).OrderByDescending(s => s.DataSolicitacao).Select(s => new
                    {
                        Seq = s.Seq,
                        DescricaoProcesso = s.ConfiguracaoProcesso.Processo.Descricao
                    }).FirstOrDefault(),
                    DadosReabertura = x.SolicitacoesServico.OfType<SolicitacaoMatricula>().Where(s => s.ConfiguracaoProcesso.Processo.Servico.Token == TOKEN_SERVICO.MATRICULA_REABERTURA && situacoesMatriculaAberta.Contains(s.SituacaoAtual.CategoriaSituacao) && s.ConfiguracaoProcesso.Processo.DataInicio <= DateTime.Now && (!s.ConfiguracaoProcesso.Processo.DataFim.HasValue || s.ConfiguracaoProcesso.Processo.DataFim >= DateTime.Now)).OrderByDescending(s => s.DataSolicitacao).Select(s => new
                    {
                        Seq = s.Seq,
                        DescricaoProcesso = s.ConfiguracaoProcesso.Processo.Descricao
                    }).FirstOrDefault(),
                });

                seqSolicitacaoMatriculaRenovacao = dadosSolicitacaoMatricula?.DadosRenovacao?.Seq;
                seqSolicitacaoMatriculaReabertura = dadosSolicitacaoMatricula?.DadosReabertura?.Seq;
                descricaoProcesso = dadosSolicitacaoMatricula?.DadosReabertura?.DescricaoProcesso ?? dadosSolicitacaoMatricula?.DadosRenovacao?.DescricaoProcesso;
            }
            else if (seqPessoa.HasValue)
            {
                var dadosSolicitacaoMatricula = PessoaDomainService.SearchProjectionByKey(seqPessoa.Value, x => new
                {
                    DadosRenovacao = x.Atuacoes.SelectMany(s => s.SolicitacoesServico.OfType<SolicitacaoMatricula>()).Where(s => (s.ConfiguracaoProcesso.Processo.Servico.Token == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU || s.ConfiguracaoProcesso.Processo.Servico.Token == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_LATO_SENSU) && situacoesMatriculaAberta.Contains(s.SituacaoAtual.CategoriaSituacao) && s.ConfiguracaoProcesso.Processo.DataInicio <= DateTime.Today && (!s.ConfiguracaoProcesso.Processo.DataFim.HasValue || s.ConfiguracaoProcesso.Processo.DataFim >= DateTime.Today)).OrderByDescending(s => s.DataSolicitacao).Select(s => new
                    {
                        Seq = s.Seq,
                        DescricaoProcesso = s.ConfiguracaoProcesso.Processo.Descricao
                    }).FirstOrDefault(),
                    DadosReabertura = x.Atuacoes.SelectMany(s => s.SolicitacoesServico.OfType<SolicitacaoMatricula>()).Where(s => s.ConfiguracaoProcesso.Processo.Servico.Token == TOKEN_SERVICO.MATRICULA_REABERTURA && situacoesMatriculaAberta.Contains(s.SituacaoAtual.CategoriaSituacao) && s.ConfiguracaoProcesso.Processo.DataInicio <= DateTime.Today && (!s.ConfiguracaoProcesso.Processo.DataFim.HasValue || s.ConfiguracaoProcesso.Processo.DataFim >= DateTime.Today)).OrderByDescending(s => s.DataSolicitacao).Select(s => new
                    {
                        Seq = s.Seq,
                        DescricaoProcesso = s.ConfiguracaoProcesso.Processo.Descricao
                    }).FirstOrDefault(),

                    SeqSolicitacaoMatriculaRenovacao = (long?)x.Atuacoes.SelectMany(p => p.SolicitacoesServico.OfType<SolicitacaoMatricula>()).Where(s => s.ConfiguracaoProcesso.Processo.Servico.Token == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU && situacoesMatriculaAberta.Contains(s.SituacaoAtual.CategoriaSituacao)).OrderByDescending(s => s.DataSolicitacao).FirstOrDefault().Seq,
                    SeqSolicitacaoMatriculaReabertura = (long?)x.Atuacoes.SelectMany(p => p.SolicitacoesServico.OfType<SolicitacaoMatricula>()).Where(s => s.ConfiguracaoProcesso.Processo.Servico.Token == TOKEN_SERVICO.MATRICULA_REABERTURA && situacoesMatriculaAberta.Contains(s.SituacaoAtual.CategoriaSituacao)).OrderByDescending(s => s.DataSolicitacao).FirstOrDefault().Seq,
                });

                seqSolicitacaoMatriculaRenovacao = dadosSolicitacaoMatricula?.DadosRenovacao?.Seq;
                seqSolicitacaoMatriculaReabertura = dadosSolicitacaoMatricula?.DadosReabertura?.Seq;
                descricaoProcesso = dadosSolicitacaoMatricula?.DadosReabertura?.DescricaoProcesso ?? dadosSolicitacaoMatricula?.DadosRenovacao?.DescricaoProcesso;
            }

            if (seqSolicitacaoMatriculaReabertura.HasValue || seqSolicitacaoMatriculaRenovacao.HasValue)
            {
                long? seqSolicitacaoMatricula = null;
                TipoMatricula? tipoMatricula = null;

                if (seqSolicitacaoMatriculaReabertura.HasValue)
                {
                    seqSolicitacaoMatricula = seqSolicitacaoMatriculaReabertura.Value;
                    tipoMatricula = TipoMatricula.ReaberturaMatricula;
                }
                else if (seqSolicitacaoMatriculaRenovacao.HasValue)
                {
                    seqSolicitacaoMatricula = seqSolicitacaoMatriculaRenovacao.Value;
                    tipoMatricula = TipoMatricula.RenovacaoMatricula;
                }

                if (seqSolicitacaoMatricula.HasValue)
                {
                    var datasEscalonamento = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula.Value), x => new
                    {
                        DataInicio = x.GrupoEscalonamento.Itens.Select(i => i.Escalonamento).OrderBy(i => i.DataInicio).FirstOrDefault().DataInicio,
                        DataFim = x.GrupoEscalonamento.Itens.Select(i => i.Escalonamento).OrderByDescending(i => i.DataFim).FirstOrDefault().DataFim,
                    });

                    return (true, seqSolicitacaoMatricula, datasEscalonamento.DataInicio, datasEscalonamento.DataFim, tipoMatricula, descricaoProcesso);
                }
            }
            return (false, null, null, null, null, null);
        }

        public SolicitacaoMatriculaCabecalhoVO BuscarCabecalhoMatricula(long seqSolicitacaoServico)
        {
            var ret = new SolicitacaoMatriculaCabecalhoVO();

            // Retorna os dados da solicitação
            var dadosBasicos = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoServico), x => new
            {
                DescricaoEntidadeResponsavel = x.EntidadeResponsavel.Nome,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                DescricaoProcessoSeletivo = (x.PessoaAtuacao as Ingressante).ProcessoSeletivo.Descricao,
                DescricaoTipoOrientacaoCurso = x.PessoaAtuacao.OrientacoesPessoaAtuacao.OrderByDescending(o => o.Seq).FirstOrDefault(f => f.Orientacao.TipoOrientacao.Token == TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO).Orientacao.TipoOrientacao.Descricao,
                DadosOrientadoresCurso = x.PessoaAtuacao.OrientacoesPessoaAtuacao.OrderByDescending(o => o.Seq).FirstOrDefault(f => f.Orientacao.TipoOrientacao.Token == TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO)
                                                                                                    .Orientacao.OrientacoesColaborador.Where(w => (w.DataInicioOrientacao == null || w.DataInicioOrientacao <= DateTime.Now) && (w.DataFimOrientacao == null || w.DataFimOrientacao >= DateTime.Now))
                                                                                                    .OrderBy(o => o.TipoParticipacaoOrientacao).Select(o => new
                                                                                                    {
                                                                                                        NomeSocial = o.Colaborador.DadosPessoais.NomeSocial,
                                                                                                        Nome = o.Colaborador.DadosPessoais.Nome,
                                                                                                        TipoParticipacao = o.TipoParticipacaoOrientacao
                                                                                                    }),
                DescricaoTipoOrientacaoProvisoria = x.PessoaAtuacao.OrientacoesPessoaAtuacao.OrderByDescending(o => o.Seq).FirstOrDefault(f => f.Orientacao.TipoOrientacao.Token == TOKEN_TIPO_ORIENTACAO.ORIENTACAO_ACADEMICA_PROVISORIA).Orientacao.TipoOrientacao.Descricao,
                DadosOrientadoresProvisoria = x.PessoaAtuacao.OrientacoesPessoaAtuacao.OrderByDescending(o => o.Seq).FirstOrDefault(f => f.Orientacao.TipoOrientacao.Token == TOKEN_TIPO_ORIENTACAO.ORIENTACAO_ACADEMICA_PROVISORIA)
                                                                                                    .Orientacao.OrientacoesColaborador.Where(w => (w.DataInicioOrientacao == null || w.DataInicioOrientacao <= DateTime.Now) && (w.DataFimOrientacao == null || w.DataFimOrientacao >= DateTime.Now))
                                                                                                    .OrderBy(o => o.TipoParticipacaoOrientacao).Select(o => new
                                                                                                    {
                                                                                                        NomeSocial = o.Colaborador.DadosPessoais.NomeSocial,
                                                                                                        Nome = o.Colaborador.DadosPessoais.Nome,
                                                                                                        TipoParticipacao = o.TipoParticipacaoOrientacao
                                                                                                    }),
                SeqsTipoTermoIntercambio = x.PessoaAtuacao.TermosIntercambio.Select(t => t.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio),
                DescricaoVinculo = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.Descricao ?? (x.PessoaAtuacao as Aluno).TipoVinculoAluno.Descricao,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token
            });

            // Busca os dados da origem do aluno/ingressante
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosBasicos.SeqPessoaAtuacao);

            // Monta a descrição do vinculo
            ret.DescricaoVinculo = PessoaAtuacaoDomainService.RecuperarDescricaoVinculo(dadosBasicos.SeqPessoaAtuacao, dadosBasicos.SeqsTipoTermoIntercambio, dadosBasicos.DescricaoVinculo);

            // Popula o ret
            //ret.DescricaoCicloLetivo = dadosBasicos.DescricaoCicloLetivo;
            ret.DescricaoEntidadeResponsavel = dadosBasicos.DescricaoEntidadeResponsavel;
            ret.DescricaoProcesso = dadosBasicos.DescricaoProcesso;
            ret.DescricaoProcessoSeletivo = dadosBasicos.DescricaoProcessoSeletivo;
            ret.NomeOrientador = string.Empty;

            // Define como exibir os orientadores colocando como prioridade a orientação de conclusão de curso
            if (dadosBasicos.DadosOrientadoresCurso != null || dadosBasicos.DadosOrientadoresProvisoria != null)
            {
                if (dadosBasicos.DadosOrientadoresCurso != null && dadosBasicos.DadosOrientadoresCurso.Count() > 0)
                {
                    foreach (var item in dadosBasicos.DadosOrientadoresCurso)
                    {
                        if (ret.NomeOrientador.Length > 0)
                            ret.NomeOrientador += ", ";

                        if (!string.IsNullOrWhiteSpace(item.NomeSocial))
                            ret.NomeOrientador += item.NomeSocial;
                        else
                            ret.NomeOrientador += item.Nome;

                        ret.NomeOrientador += " (" + SMCEnumHelper.GetDescription(item.TipoParticipacao) + ")";
                    }

                    ret.DescricaoTipoOrientacao = dadosBasicos.DescricaoTipoOrientacaoCurso;
                }
                else if (dadosBasicos.DadosOrientadoresProvisoria != null && dadosBasicos.DadosOrientadoresProvisoria.Count() > 0)
                {
                    foreach (var item in dadosBasicos.DadosOrientadoresProvisoria)
                    {
                        if (ret.NomeOrientador.Length > 0)
                            ret.NomeOrientador += ", ";

                        if (!string.IsNullOrWhiteSpace(item.NomeSocial))
                            ret.NomeOrientador += item.NomeSocial;
                        else
                            ret.NomeOrientador += item.Nome;

                        ret.NomeOrientador += " (" + SMCEnumHelper.GetDescription(item.TipoParticipacao) + ")";
                    }

                    ret.DescricaoTipoOrientacao = dadosBasicos.DescricaoTipoOrientacaoProvisoria;
                }
            }

            // Busca a parametriação do vinculo do aluno na instituição x nível de ensino
            var specIntv = new InstituicaoNivelTipoVinculoAlunoFilterSpecification
            {
                SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                SeqInstituicao = dadosOrigem.SeqInstituicaoEnsino
            };
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.SearchByKey(specIntv, IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel_NivelEnsino);

            // RN_ORG_049
            // Exibir campos apenas para os níveis de ensino, de acordo com os tokens:
            // DOUTORADO_ACADEMICO / DOUTORADO_PROFISSIONAL / MESTRADO_ACADEMICO  / MESTRADO_PROFISSIONAL
            if ((instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO) ||
                (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL) ||
                (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO) ||
                (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL))
            {
                ret.ExibeFormacoesEspecificas = true;
                ret.ExibeEntidadeResponsavelEVinculo = true;
            }

            // UC_MAT_003_16 - NV14, NV15, NV18, NV 19 e NV20
            #region [ Preenchimento e exibição dos contatos ]
            // NV19 - Exibir os campos de Contato do Setor de matrículas e Coordenação do Curso apenas para os níveis de ensino, de acordo com o token: GRADUACAO.
            if (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.GRADUACAO)
            {
                ret.ExibeContatoSetorMatricula = true;

                // O campo de contato da coordenação do curso usa as regras NV 19 e NV20
                if (dadosBasicos.TokenServico != TOKEN_SERVICO.MATRICULA_INGRESSANTE_DISCIPLINA_ISOLADA_GRADUACAO &&
                    dadosBasicos.TokenServico != TOKEN_SERVICO.TRANSFERENCIA_INTERNA_GRADUACAO)
                {
                    ret.ExibeContatoCoordenacaoCurso = true;
                }
            }

            // NV20 - Os campos "Secretaria acadêmica" e "Coordenação do curso" deverão ser exibidos somente para os processos cujo token 
            // do serviço é diferente de MATRICULA_INGRESSANTE_DISCIPLINA_ISOLADA_GRADUACAO e TRANSFERENCIA_INTERNA_GRADUACAO.
            if (dadosBasicos.TokenServico != TOKEN_SERVICO.MATRICULA_INGRESSANTE_DISCIPLINA_ISOLADA_GRADUACAO &&
                dadosBasicos.TokenServico != TOKEN_SERVICO.TRANSFERENCIA_INTERNA_GRADUACAO)
            {
                ret.ExibeContatoSecretariaAcademica = true;
            }

            // Busca os dados de contato do curso-oferta-localidade da pessoa atuação
            CursoOfertaLocalidadeVO cursoOfertaLocalidade = CursoOfertaLocalidadeDomainService.BuscarCursoOfertaLocalidade(dadosOrigem.SeqCursoOfertaLocalidade);

            #region [ NV14 - Preenchimento de contatos Secretaria Acadêmica ]

            if (ret.ExibeContatoSecretariaAcademica)
            {
                string telefoneSecretaria = "";
                string emailSecretaria = "";

                // Se exige curso (ou seja, não é DI)
                if (instituicaoNivelTipoVinculoAluno.ExigeCurso)
                {
                    var listaTelefonesSecretaria = new List<string>();

                    var listaTelefoneCategoriaSecretaria = cursoOfertaLocalidade?.Telefones?.Where(c => c.CategoriaTelefone == CategoriaTelefone.Secretaria).ToList();
                    listaTelefoneCategoriaSecretaria.SMCForEach(d =>
                    {
                        if (d != null && !string.IsNullOrEmpty(d.Numero))
                        {
                            listaTelefonesSecretaria.Add($"({d?.CodigoArea}) {d?.Numero.SMCSafeTrim()}");
                        }
                    });

                    telefoneSecretaria = string.Join(", ", listaTelefonesSecretaria.ToArray());

                    var emailCategoriaSecretaria = cursoOfertaLocalidade?.EnderecosEletronicos?.Where(c => c.TipoEnderecoEletronico == TipoEnderecoEletronico.Email && c.CategoriaEnderecoEletronico == CategoriaEnderecoEletronico.Secretaria).FirstOrDefault();
                    if (emailCategoriaSecretaria != null)
                        emailSecretaria = emailCategoriaSecretaria?.Descricao?.SMCSafeTrim();
                }
                else // Não exige curso (é DI)
                {
                    // Não é graduação, mostra os dados da entidade responsável
                    if (instituicaoNivelTipoVinculoAluno.InstituicaoNivel.NivelEnsino.Token != TOKEN_NIVEL_ENSINO.GRADUACAO)
                    {
                        var listaTelefonesSecretaria = new List<string>();

                        // Busca os dados da entidade responsável da pessoa atuação
                        var entidadeResponsavel = EntidadeDomainService.BuscarEntidade(dadosOrigem.SeqEntidadeResponsavel);

                        var listaTelefoneCategoriaSecretaria = entidadeResponsavel?.Telefones?.Where(c => c.CategoriaTelefone == CategoriaTelefone.Secretaria).ToList();
                        listaTelefoneCategoriaSecretaria.SMCForEach(d =>
                        {
                            if (d != null && !string.IsNullOrEmpty(d.Numero))
                            {
                                listaTelefonesSecretaria.Add($"({d?.CodigoArea}) {d?.Numero.SMCSafeTrim()}");
                            }
                        });
                        telefoneSecretaria = string.Join(", ", listaTelefonesSecretaria.ToArray());

                        var entidadeResponsavelEmailSecretaria = entidadeResponsavel?.EnderecosEletronicos?.Where(c => c.TipoEnderecoEletronico == TipoEnderecoEletronico.Email && c.CategoriaEnderecoEletronico == CategoriaEnderecoEletronico.Secretaria).FirstOrDefault();
                        if (entidadeResponsavelEmailSecretaria != null)
                            emailSecretaria = entidadeResponsavelEmailSecretaria?.Descricao?.SMCSafeTrim();
                    }
                }

                var contatosSecretaria = new string[] { telefoneSecretaria, emailSecretaria };
                ret.ContatoSecretariaAcademica = string.Join(" / ", contatosSecretaria.Where(c => !string.IsNullOrEmpty(c)));
            }

            #endregion

            #region [ NV15 - Preenchimento Contato Coordenação do Curso ]
            if (ret.ExibeContatoCoordenacaoCurso)
            {
                var emailCoodrenacao = cursoOfertaLocalidade?.EnderecosEletronicos?.Where(c => c.TipoEnderecoEletronico == TipoEnderecoEletronico.Email && c.CategoriaEnderecoEletronico == CategoriaEnderecoEletronico.Coordenacao).FirstOrDefault();
                if (emailCoodrenacao != null)
                {
                    ret.ContatoCoordenacaoCurso = emailCoodrenacao.Descricao.SMCSafeTrim();
                }
            }
            #endregion

            #region [ NV18 - Preenchimento Contato Setor de Matrículas ]
            if (ret.ExibeContatoSetorMatricula)
            {
                string telefonesMatriculasFormatado = "";
                var listaTelefonesSecretaria = new List<string>();

                var listaTelefoneCategoriaSecretaria = cursoOfertaLocalidade?.Telefones?.Where(c => c.CategoriaTelefone == CategoriaTelefone.Matriculas).ToList();
                listaTelefoneCategoriaSecretaria.SMCForEach(d =>
                {
                    if (d != null && !string.IsNullOrEmpty(d.Numero))
                    {
                        listaTelefonesSecretaria.Add($"({d?.CodigoArea}) {d?.Numero.SMCSafeTrim()}");
                    }
                });

                telefonesMatriculasFormatado = string.Join(", ", listaTelefonesSecretaria.ToArray());

                var emailCategoriaMatricula = cursoOfertaLocalidade?.EnderecosEletronicos?.Where(c => c.TipoEnderecoEletronico == TipoEnderecoEletronico.Email && c.CategoriaEnderecoEletronico == CategoriaEnderecoEletronico.Matriculas).FirstOrDefault();

                var listaContatosMatriculas = new string[] { telefonesMatriculasFormatado, emailCategoriaMatricula?.Descricao };

                var contatosMatriculaFormatados = string.Join(" / ", listaContatosMatriculas.Where(c => !string.IsNullOrEmpty(c)));

                ret.ContatoSetorMatricula = contatosMatriculaFormatados;
            }
            #endregion

            #endregion

            // UC_MAT_003_16 - NV06
            // Exibir campos Oferta de Curso, Localidade, Modalidade e Turno apenas quando o vínculo da pessoa-atuação exigir associação de curso, de acordo com a
            // parametrização por instituição e nível de ensino.
            if (instituicaoNivelTipoVinculoAluno != null && instituicaoNivelTipoVinculoAluno.ExigeCurso)
            {
                _DadosComplexos dadosComplexos;

                if (dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante)
                {
                    dadosComplexos = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoServico), x => new _DadosComplexos
                    {
                        NomesLocalidades = (x.PessoaAtuacao as Ingressante).Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome).ToList(),
                        DescricoesModalidades = (x.PessoaAtuacao as Ingressante).Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao).ToList(),
                        DescricoesOfertas = (x.PessoaAtuacao as Ingressante).Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao).ToList(),
                        DescricoesTurnos = (x.PessoaAtuacao as Ingressante).Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.Turno.Descricao).ToList(),
                        SeqsFormacoesEspecificas = (x.PessoaAtuacao as Ingressante).FormacoesEspecificas.Select(f => f.SeqFormacaoEspecifica).ToList(),
                        SeqEntidadeResponsavelFormacaoEspecifica = (x.PessoaAtuacao as Ingressante).FormacoesEspecificas.Select(f => f.FormacaoEspecifica.SeqEntidadeResponsavel).FirstOrDefault(),
                        Hibrido = (x.PessoaAtuacao as Ingressante).Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Hibrido).FirstOrDefault()
                    });
                }
                else
                {
                    dadosComplexos = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoServico), x => new _DadosComplexos
                    {
                        NomesLocalidades = new List<string> { (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome },
                        DescricoesModalidades = new List<string> { (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Modalidade.Descricao },
                        DescricoesOfertas = new List<string> { (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao },
                        DescricoesTurnos = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Turnos.Select(t => t.Turno.Descricao).ToList(),
                        SeqsFormacoesEspecificas = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).Formacoes.Where(a => a.DataInicio <= DateTime.Now && (!a.DataFim.HasValue || a.DataFim >= DateTime.Now)).Select(a => a.SeqFormacaoEspecifica).ToList(),
                        SeqEntidadeResponsavelFormacaoEspecifica = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).Formacoes.FirstOrDefault(a => a.DataInicio <= DateTime.Now && (!a.DataFim.HasValue || a.DataFim >= DateTime.Now)).FormacaoEspecifica.SeqEntidadeResponsavel,
                        Hibrido = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Hibrido
                    });
                }

                if (dadosComplexos != null)
                {
                    List<FormacaoEspecificaNodeVO> formacoesHierarquia = new List<FormacaoEspecificaNodeVO>();

                    if (dadosComplexos.SeqEntidadeResponsavelFormacaoEspecifica.HasValue)
                    {
                        var listaSeqsEntidadesResponsaveis = new List<long>();
                        listaSeqsEntidadesResponsaveis.Add(dadosComplexos.SeqEntidadeResponsavelFormacaoEspecifica.Value);
                        var formacoes = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasCabecalho(listaSeqsEntidadesResponsaveis);

                        if (formacoes != null)
                            foreach (var formacao in dadosComplexos.SeqsFormacoesEspecificas)
                                formacoesHierarquia.AddRange(FormacaoEspecificaDomainService.GerarHierarquiaFormacaoEspecifica(formacao, formacoes));
                    }

                    //ret.DescricaoCurso = string.Join(", ", dadosComplexos?.Cursos?.Select(c => c.Nome)?.Distinct()?.ToArray());
                    ret.DescricaoLocalidade = string.Join(", ", dadosComplexos?.NomesLocalidades?.ToArray());
                    ret.DescricaoModalidade = dadosComplexos.Hibrido ? "Híbrido" : string.Join(", ", dadosComplexos?.DescricoesModalidades?.ToArray()); // UC_MAT_003_16 - NV11
                    ret.DescricaoOferta = string.Join(", ", dadosComplexos?.DescricoesOfertas?.ToArray());
                    ret.DescricaoTurno = string.Join(", ", dadosComplexos?.DescricoesTurnos?.ToArray());
                    ret.ExigeCurso = true;
                    ret.FormacoesEspecificas = formacoesHierarquia.Select(f => new FormacoesEspecificasSolicitacaoMatriculaVO { DescricaoTipoFormacaoEspecifica = f.DescricaoTipoFormacaoEspecifica, DescricoesFormacoesEspecificas = f.Descricao }).ToList();
                }
            }

            return ret;
        }

        public CabecalhoMenuMatriculaVO BuscarCabecalhoMenu(long seqSolicitacaoMatricula)
        {
            var dadosBasicos = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                DescricaoUnidadeResponsavel = x.EntidadeResponsavel.Nome,
                LocalidadeUnidadeResponsavel = x.EntidadeResponsavel.Enderecos.FirstOrDefault(f => f.Correspondencia == true).NomeCidade,
                //DescricoesUnidadesResponsaveis = x.ConfiguracaoProcesso.Processo.UnidadesResponsaveis.Select(u => u.EntidadeResponsavel.Nome).ToList(),
                DescricaoNivelEnsino = (x.PessoaAtuacao as Ingressante).NivelEnsino.Descricao ?? (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).NivelEnsino.Descricao,
                DescricaoVinculo = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.Descricao ?? (x.PessoaAtuacao as Aluno).TipoVinculoAluno.Descricao,
                SeqsTipoTermoIntercambioAssociados = x.PessoaAtuacao.TermosIntercambio.Select(t => t.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio),
                SeqIngressante = x.SeqPessoaAtuacao,
                Pessoa = x.PessoaAtuacao.Pessoa.DadosPessoais.OrderByDescending(o => o.DataInclusao).FirstOrDefault(),
                DescricaoVinculoInstitucional = x.PessoaAtuacao.Descricao,
                DescricaoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Descricao,
                SeqNivelEnsino = (long?)(x.PessoaAtuacao as Ingressante).SeqNivelEnsino ?? (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(a => a.Atual).SeqNivelEnsino,
                SeqTipoVinculo = (long?)(x.PessoaAtuacao as Ingressante).SeqTipoVinculoAluno ?? (x.PessoaAtuacao as Aluno).SeqTipoVinculoAluno,
                SeqInstituicao = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
                DescricoesOfertas = (x.PessoaAtuacao as Ingressante).Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao).ToList(),
            });

            var ret = new CabecalhoMenuMatriculaVO();
            ret.DescricaoProcesso = dadosBasicos.DescricaoProcesso;
            ret.LocalidadeUnidadeResponsavel = dadosBasicos.LocalidadeUnidadeResponsavel;
            ret.DescricaoVinculoInstitucional = dadosBasicos.DescricaoVinculoInstitucional;
            ret.DescricaoCicloLetivo = dadosBasicos.DescricaoCicloLetivo;
            ret.NomeIngressante = dadosBasicos.Pessoa.NomeSocial ?? dadosBasicos.Pessoa.Nome;
            ret.SeqInstituicaoEnsino = dadosBasicos.SeqInstituicao;

            //Para comprovante de matricula desconsiderar o nome social
            ret.Nome = dadosBasicos.Pessoa.Nome;
            ret.SeqPessoa = dadosBasicos.Pessoa.SeqPessoa;

            var specInstituicaoNivelTipoVinculoAluno = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqNivelEnsino = dadosBasicos.SeqNivelEnsino,
                SeqTipoVinculoAluno = dadosBasicos.SeqTipoVinculo,
                SeqInstituicao = dadosBasicos.SeqInstituicao
            };

            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.SearchByKey(
               specInstituicaoNivelTipoVinculoAluno, IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel_NivelEnsino);

            // Regra RN_ORG_049 (BI_MAT_002 - NV07) Exibição campo apenas para Stricto
            // Exibir campos com NV07 (Entidade responsável, Nível de Ensino e Vínculo) apenas para os níveis de ensino, de acordo com os tokens:
            // DOUTORADO_ACADEMICO / DOUTORADO_PROFISSIONAL / MESTRADO_ACADEMICO  / MESTRADO_PROFISSIONAL
            if ((instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO) ||
                (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.DOUTORADO_PROFISSIONAL) ||
                (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO) ||
                (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL) ||
                (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.ESPECIALIZACAO))
            {
                ret.ExibeEntidadeResponsavel = true;
                ret.DescricaoUnidadeResponsavel = dadosBasicos.DescricaoUnidadeResponsavel;

                ret.ExibeNivelEnsino = true;
                ret.DescricaoNivelEnsino = dadosBasicos.DescricaoNivelEnsino;

                ret.DescricaoVinculo = PessoaAtuacaoDomainService.RecuperarDescricaoVinculo(dadosBasicos.SeqIngressante, dadosBasicos.SeqsTipoTermoIntercambioAssociados, dadosBasicos.DescricaoVinculo);
                ret.ExibeVinculo = true;
            }

            // NV08
            // Alteração task 49245 - Exibir o campo "Curso" somente para o nível de ensino cujo token é GRADUACAO.
            //if (instituicaoNivelTipoVinculoAluno != null && instituicaoNivelTipoVinculoAluno.ExigeCurso)
            if (instituicaoNivelTipoVinculoAluno?.InstituicaoNivel?.NivelEnsino?.Token == TOKEN_NIVEL_ENSINO.GRADUACAO)
            {
                ret.DescricaoCurso = string.Join(", ", dadosBasicos?.DescricoesOfertas?.ToArray());
                ret.ExibeDescricaoCurso = true;
            }

            return ret;
        }

        public ParcelasPagamentoSolicitacaoMatriculaVO BuscarParcelasPagamentoSolicitacaoMatricula(long seqSolicitacaoMatricula)
        {
            // Recupera os dados da solicitação
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                SeqCicloLetivo = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                TipoAluno = (x.PessoaAtuacao is Ingressante) ? TipoAluno.Calouro : TipoAluno.Veterano,
                CodAluno = (long?)(x.PessoaAtuacao as Ingressante).Seq ?? (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                SeqCondicaoPagamento = x.SeqCondicaoPagamentoGra,
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                Beneficios = x.PessoaAtuacao.Beneficios.Where(b => b.HistoricoSituacoes.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault().SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido).Select(s => new
                {
                    s.IncideParcelaMatricula,
                    s.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia,
                    s.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia,
                    s.Beneficio,
                    s.ExibeValoresTermoAdesao
                }).ToList()
                //TipoAluno = 4, // FIXO para GRA nesta situação
                //BeneficioIncluiDesbloqueioTemporario = x.PessoaAtuacao.Beneficios.Any(b => b.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido && b.IncideParcelaMatricula && b.Beneficio.IncluirDesbloqueioTemporario),
                //BeneficioDeferidoCobranca = (x.PessoaAtuacao is Ingressante) ? x.PessoaAtuacao.Beneficios.Any(b => b.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido && !b.Beneficio.RecebeCobranca && b.IncideParcelaMatricula)
                //                                                             : x.PessoaAtuacao.Beneficios.Any(b => b.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido && !b.Beneficio.RecebeCobranca),
                //NomeBeneficio = x.PessoaAtuacao.Beneficios.FirstOrDefault(b => b.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido && b.IncideParcelaMatricula).Beneficio.Descricao,
            });

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacao.SeqPessoaAtuacao);

            // Busca os dados do evento letivo
            var dadosEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosSolicitacao.SeqCicloLetivo.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, dadosSolicitacao.TipoAluno, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            var filtro = new ParcelasPagamentoAcademicoFiltroData
            {
                AnoCicloLetivo = dadosSolicitacao.AnoCicloLetivo,
                CodServicoOrigem = dadosOrigem.CodigoServicoOrigem,
                NumeroCicloLetivo = dadosSolicitacao.NumeroCicloLetivo,
                SeqIngressante = dadosSolicitacao.TipoAluno == TipoAluno.Calouro ? dadosSolicitacao.SeqPessoaAtuacao : (long)dadosSolicitacao.CodAluno,
                SeqOrigem = (int)dadosOrigem.SeqOrigem,
                TipoAluno = dadosSolicitacao.TipoAluno == TipoAluno.Calouro ? 4 : 1, // Fixo 4 no GRA (Ingressante) 1 no GRA (Veterano)
                DataInicioPeriodoLetivo = dadosEventoLetivo.DataInicio, //dataPeriodoLetivo.DataInicio, // TODO: Preencher
                DataFimPeriodoLetivo = DateTime.Parse("2018-12-31"), //dadosEventoLetivo.DataFim, //dataPeriodoLetivo.DataFim // TODO: Preencher
            };

            ParcelasPagamentoSolicitacaoMatriculaVO ret = null;
            var dadosParcelas = IntegracaoFinanceiroService.BuscarParcelasPagamentoAcademico(filtro);

            if (dadosParcelas != null)
            {
                var parcelas = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x =>
                                                          x.GrupoEscalonamento.Itens.SelectMany(i =>
                                                                    i.Parcelas.Select(p => new { @NumeroParcela = p.NumeroParcela, @Descricao = p.Descricao, @ServicoAdicional = p.ServicoAdicional, DataEncerramentoEscalonamento = i.Escalonamento.DataEncerramento }))).ToList();

                // Encontra a parcela P0 configurada no escalonamento
                var parcelaEscalonamentoP0Adicional = parcelas.FirstOrDefault(p => !p.NumeroParcela.HasValue && p.ServicoAdicional);
                var parcelaEscalonamentoP0 = parcelas.FirstOrDefault(p => !p.NumeroParcela.HasValue && !p.ServicoAdicional);

                ret = new ParcelasPagamentoSolicitacaoMatriculaVO();
                ret.Parcelas = new List<ParcelaPagamentoSolicitacaoMatriculaVO>();
                var nomeBeneficio = string.Empty;

                // Task 51540 - Caso a pessoa-atuação possua um benefício associado com o parâmetro "Exibe valores no termo de adesão" igual a NÃO, exibir o valor 0,00
                bool ExibeValores = !dadosSolicitacao.Beneficios.SMCAny(c => c.ExibeValoresTermoAdesao == false);

                //Validação do benefício de acordo com o tipo de atuação
                if (dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante)
                {
                    var beneficios = dadosSolicitacao.Beneficios.Where(w => w.IncideParcelaMatricula)?.Select(b => b?.Beneficio?.Descricao);

                    ret.BeneficioIncluiDesbloqueioTemporario = dadosSolicitacao.Beneficios.Any(a => a.Beneficio.IncluirDesbloqueioTemporario && a.IncideParcelaMatricula);
                    ret.BeneficioDeferidoCobranca = dadosSolicitacao.Beneficios.Any(a => !a.Beneficio.RecebeCobranca && a.IncideParcelaMatricula);
                    if (beneficios != null && beneficios.Any())
                        nomeBeneficio = string.Join(", ", beneficios);
                }
                else if (dadosOrigem.TipoAtuacao == TipoAtuacao.Aluno)
                {
                    var beneficios = dadosSolicitacao.Beneficios.Where(w => dadosParcelas.Any(a => !a.ServicoAdicional && w.DataInicioVigencia <= a.DataVencimentoParcela && a.DataVencimentoParcela <= w.DataFimVigencia))?.Select(b => b?.Beneficio?.Descricao);

                    var beneficioDesbloqueioTemporario = dadosSolicitacao.Beneficios.Where(w => w.Beneficio.IncluirDesbloqueioTemporario);
                    var beneficioNaoRecebeCobranca = dadosSolicitacao.Beneficios.Where(w => !w.Beneficio.RecebeCobranca);

                    ret.BeneficioIncluiDesbloqueioTemporario = dadosParcelas.Any(a => !a.ServicoAdicional && beneficioDesbloqueioTemporario.Any(b => b.DataInicioVigencia <= a.DataVencimentoParcela && a.DataVencimentoParcela <= b.DataFimVigencia));
                    ret.BeneficioDeferidoCobranca = dadosParcelas.Any(a => !a.ServicoAdicional && beneficioNaoRecebeCobranca.Any(b => b.DataInicioVigencia <= a.DataVencimentoParcela && a.DataVencimentoParcela <= b.DataFimVigencia));

                    if (beneficios != null && beneficios.Any())
                        nomeBeneficio = string.Join(", ", beneficios);
                }

                if (parcelas != null && parcelas.Any())
                {
                    dadosParcelas.GroupBy(d => d.NumeroParcela + "|" + d.ServicoAdicional).ToList().ForEach(e =>
                    {
                        var numeroParcelaGrupo = Convert.ToInt16(e.Key.Split('|').FirstOrDefault());
                        var isServicoAdicionalGrupo = Convert.ToBoolean(e.Key.Split('|').LastOrDefault());

                        var parcela = parcelas.FirstOrDefault(p => p.NumeroParcela == numeroParcelaGrupo);
                        parcela = parcela ?? (isServicoAdicionalGrupo ? parcelaEscalonamentoP0Adicional : parcelaEscalonamentoP0);

                        ret.Parcelas.Add(new ParcelaPagamentoSolicitacaoMatriculaVO
                        {
                            NumeroParcela = numeroParcelaGrupo,
                            Titulos = e.Select(t =>
                            {
                                var retTitulo = new ParcelaPagamentoResponsavelSolicitacaoMatriculaVO
                                {
                                    DescricaoTitulo = parcela.Descricao,
                                    // Fiz a regra abaixo pois na view que exibe as parcelas, é verificado se a pessoa responsável pelo boleto é a própria pessoa atuação (pelo seqPessoaAtuacao). Caso não seja, não exibe o link para gerar o boleto
                                    SeqResponsavel = (t.CodigoPessoa != t.CodigoResponsavelFinanceiro ? 0 : t.CodigoAluno),
                                    NomeResponsavel = t.NomeResponsavelFinanceiro,
                                    SeqTitulo = t.SeqTitulo,
                                    SeqServico = t.SeqServico,
                                    SituacaoTitulo = t.SituacaoTitulo,
                                    ValorTitulo = t.ValorTitulo,
                                };

                                return retTitulo;
                            }).ToList(),
                            DataVencimento = e.FirstOrDefault().DataVencimentoParcela,
                            NomeParcela = parcela.Descricao, //"P" + e.Key,
                            ValorBeneficio = ExibeValores ? e.FirstOrDefault().ValorBeneficio : 0,
                            ValorOutros = ExibeValores ? e.FirstOrDefault().ValorOutros : 0,
                            ValorPagar = ExibeValores ? e.FirstOrDefault().ValorPagar : 0,
                            ValorParcela = ExibeValores ? e.FirstOrDefault().ValorParcela : 0,
                            //NomeBeneficio = string.IsNullOrWhiteSpace(dadosSolicitacao.NomeBeneficio) ? (e.FirstOrDefault().ValorBeneficio > 0 ? "Benefício lançado no financeiro" : "-") : dadosSolicitacao.NomeBeneficio,
                            NomeBeneficio = string.IsNullOrWhiteSpace(nomeBeneficio) ? (e.FirstOrDefault().ValorBeneficio > 0 ? "Benefício lançado no financeiro" : "-") : nomeBeneficio,
                            EscalonamentoPossuiDataEncerramento = parcela.DataEncerramentoEscalonamento.HasValue
                        });
                        // Ao invés de mostrar o valor total da matrícula, estamos somando o valor da parcela por causa de parcela adicional, uma vez que ela não faz parte do valor da matrícula
                        // Estamos agrupando por numero da parcela e serviço adicional e pegando o primeiro registro para somar. Isso foi necessário pois estava somando duplicado caso houvesse mais de um título por parcela pois a procedure retorna o valor cheio da parcela, e não o valor por título.
                        ret.ValorTotalMatricula += ExibeValores ? e.FirstOrDefault().ValorParcela : 0; //e.Sum(s => s.ValorParcela); // FirstOrDefault()?.ValorMatricula ?? 0;
                    });
                }
                else
                    throw new SMCApplicationException("Nenhuma parcela foi encontrada para o grupo de escalonamento em questão.");
            }

            return ret;
        }

        public BoletoMatriculaVO GerarBoletoMatricula(long seqTitulo, long seqServico, long seqSolicitacaoMatricula)
        {
            // Recupera o sequencial do titulo corretamente
            var retorno = IntegracaoFinanceiroService.EmissaoBoletoWebRegistro((int)seqTitulo, (int)seqServico);

            if (retorno.Sucesso)
            {
                var seqTituloGerado = (int)retorno.Codigo;
                // Chama o serviço para geração dos dados usados para geração do boleto
                BoletoData dadosBoleto = IntegracaoFinanceiroService.BuscarBoleto(seqTituloGerado);
                dadosBoleto.ImagemBanco = SMCImageHelper.ImageToBase64(BuscarImagemBanco(dadosBoleto.Banco.Numero));
                dadosBoleto.ImagemCodigoBarras = SMCImageHelper.ImageToBase64(BuscarImagemCodigoBarras(dadosBoleto.CodigoBarras));

                var boletoMatricula = dadosBoleto.Transform<BoletoMatriculaVO>();

                // Recupera qual é a descrição do vínculo da pessoa atuação
                var dadosBasicos = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
                {
                    DescricaoVinculo = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.Descricao ?? (x.PessoaAtuacao as Aluno).TipoVinculoAluno.Descricao,
                    SeqIngressante = x.SeqPessoaAtuacao,
                    SeqsTipoTermoIntercambioAssociados = x.PessoaAtuacao.TermosIntercambio.Select(t => t.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio),
                    DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                });

                boletoMatricula.DescricaoVinculo = PessoaAtuacaoDomainService.RecuperarDescricaoVinculo(dadosBasicos.SeqIngressante, dadosBasicos.SeqsTipoTermoIntercambioAssociados, dadosBasicos.DescricaoVinculo);
                boletoMatricula.SeqIngressante = dadosBasicos.SeqIngressante;
                boletoMatricula.SeqSolicitacaoMatricula = seqSolicitacaoMatricula;
                boletoMatricula.DescricaoProcesso = dadosBasicos.DescricaoProcesso;

                return boletoMatricula;
            }
            else
            {
                //Ocorreu um ao registrar o Boleto
                throw new SMCApplicationException(retorno.Mensagem);
            }
        }

        // GET: /BoletoBancario/ImagemBanco/?codigoBanco=1
        private byte[] BuscarImagemBanco(int codigoBanco)
        {
            return IntegracaoFinanceiroService.BuscarImagemBanco(codigoBanco);
        }

        // GET: /BoletoBancario/ImagemCodigoBarras/codigoBarras=123456
        private byte[] BuscarImagemCodigoBarras(string codigoBarras)
        {
            return IntegracaoFinanceiroService.BuscarImagemCodigoBarras(codigoBarras);
        }

        /// <summary>
        /// Buscar a solicitação de matricula com os itens
        /// </summary>
        /// <param name="seq">Sequencial da solicitacao de matricula</param>
        /// <returns>Registro da solicitacao de matricula com os itens</returns>
        public SolicitacaoMatriculaVO BuscarSolicitacaoMatricula(long seq)
        {
            var ret = this.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seq), IncludesSolicitacaoMatricula.Itens);

            return ret.Transform<SolicitacaoMatriculaVO>();
        }

        public void SalvarCondicaoPagamento(long seqSolicitacaoMatricula, int? seqCondicaoPagamento)
        {
            var solicitacaoDados = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new { TermoAderido = x.CodigoAdesao.HasValue && x.DataAdesao.HasValue, SeqCondicaoPagamento = x.SeqCondicaoPagamentoGra });
            if (solicitacaoDados.SeqCondicaoPagamento != seqCondicaoPagamento)
            {
                if (!solicitacaoDados.TermoAderido)
                {
                    var solicitacao = this.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula));
                    solicitacao.SeqCondicaoPagamentoGra = seqCondicaoPagamento;
                    SaveEntity(solicitacao);
                }
                else
                {
                    throw new CondicaoPagamentoTermoAderidoException();
                }
            }
        }

        public SMCUploadFile InserirArquivoTermoAdesao(long seqSolicitacaoMatricula, byte[] arquivoConvertidoPdf)
        {
            var solicitacao = this.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula));

            var arquivo = new SMCUploadFile();
            arquivo.FileData = arquivoConvertidoPdf;
            arquivo.Description = "application/pdf";
            arquivo.Type = "application/pdf";
            arquivo.Name = "TermoAdesaoContrato.pdf";

            solicitacao.ArquivoTermoAdesao = arquivo.Transform<ArquivoAnexado>();

            SaveEntity(solicitacao);

            return solicitacao.ArquivoTermoAdesao.Transform<SMCUploadFile>();
        }

        /// <summary>
        /// Busca a condição de pagamento que foi selecionada para uma solicitação de matrícula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula"></param>
        /// <returns></returns>
        public CondicaoPagamentoAcademicoVO BuscarCondicaoPagamentoAcademico(long seqSolicitacaoMatricula)
        {
            return BuscarCondicoesPagamentoAcademico(seqSolicitacaoMatricula, true).FirstOrDefault();
        }

        public List<CondicaoPagamentoAcademicoVO> BuscarCondicoesPagamentoAcademico(long seqSolicitacaoMatricula, bool considerarCondicaoSelecionada = false, short? numeroDivisaoParcelasGrupoASerAssociado = null)
        {
            // Busca a pessoa atuação da solicitação
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula);
            var seqPessoaAtuacao = this.SearchProjectionByKey(specSolicitacao, x => x.PessoaAtuacao.Seq);

            // Verifica se a instituição nível do aluno exige curso.
            var instituicaoNiveltipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);
            var exigeCurso = instituicaoNiveltipoVinculoAluno?.ExigeCurso.Value ?? true;

            // Recupera os dados de origem
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            // Busca as informações para chamar a rotina de condição de pagamento do GRA
            var filtro = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new CondicaoPagamentoAcademicoFiltroData
            {
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                QuantidadeCreditos = !exigeCurso ? x.Itens
                    .Where(y => y.SeqDivisaoTurma.HasValue &&
                                y.HistoricosSituacao.OrderByDescending(s => s.Seq).FirstOrDefault()
                                    .SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                    .Select(d => new { d.DivisaoTurma.SeqTurma, d.DivisaoTurma.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito })
                    .GroupBy(d => d.SeqTurma)
                    .Select(d => d.FirstOrDefault())
                    .Sum(y => y.Credito) ?? 0 : 0,
                QuantidadeParcelasRetorno = x.GrupoEscalonamento.NumeroDivisaoParcelas ?? 0,
                SeqCondicaoPagamento = considerarCondicaoSelecionada ? x.SeqCondicaoPagamentoGra ?? 0 : 0,
                TipoAluno = x.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Ingressante ? 2 : (x.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Aluno ? (int)x.AlunosHistoricosCicloLetivo.OrderByDescending(a => a.Seq).FirstOrDefault().TipoAluno : 0),
                TipoVinculoAlunoFinanceiro = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
            });

            // Preenche as informações da origem do aluno
            filtro.SeqOrigem = (int)dadosOrigem.SeqOrigem;
            filtro.CodServicoOrigem = dadosOrigem.CodigoServicoOrigem;

            if (numeroDivisaoParcelasGrupoASerAssociado.HasValue)
            {
                filtro.QuantidadeParcelasRetorno = numeroDivisaoParcelasGrupoASerAssociado.Value;
            }

            /* Se a pessoa atuação possui benefício, verifica se deve alterar a quantidade de parcelas de retorno de acordo com o benefício
             REGRA: Quantidade de parcelas
             1.Se a pessoa-atuação em questão possuir algum benefício DEFERIDO associado, buscar na parametrização por Instituição-Nível - Benefício
             de acordo com o beneficio associado, a instituição e o nível de ensino da pessoa-atuação,  o número padrão de parcelas.Caso exista,
             enviar o número padrão de parcelas se for maior que 0.
             Se a pessoa - atuação possuir mais de um beneficio com o numero padrão de parcelas parametrizado, enviar o menor número.
             2.Se a pessoa - atuação em questão não possuir benefício associado, verificar se existe número de divisão cadastrado para o grupo de
             escalonamento associado à pessoa - atuação.Caso exista, enviar o número de divisão.
             Caso não atenda as condições acima(1. e 2.), deverá ser enviado 0 (zero).*/
            var specBeneficio = new PessoaAtuacaoBeneficioFilterSpecification()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                SituacaoChancelaBeneficio = SituacaoChancelaBeneficio.Deferido
            };
            var seqsBeneficio = PessoaAtuacaoBeneficioDomainService.SearchProjectionBySpecification(specBeneficio, x => x.SeqBeneficio);
            if (seqsBeneficio.SMCCount() > 0)
            {
                int qtdParcelasBeneficio = 9999;
                foreach (var seqBeneficio in seqsBeneficio)
                {
                    var specINB = new InstituicaoNivelBeneficioFilterSpecification()
                    {
                        SeqBeneficio = seqBeneficio,
                        SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                        SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino
                    };
                    var numero = InstituicaoNivelBeneficioDomainService.SearchProjectionByKey(specINB, x => x.NumeroParcelasPadraoCondicaoPagamento);
                    if (numero.HasValue && numero > 0 && numero < qtdParcelasBeneficio)
                        qtdParcelasBeneficio = numero.Value;
                }
                if (qtdParcelasBeneficio != 9999)
                    filtro.QuantidadeParcelasRetorno = qtdParcelasBeneficio;
            }

            return IntegracaoFinanceiroService.BuscarCondicoesPagamentoAcademico(filtro).TransformList<CondicaoPagamentoAcademicoVO>();
        }

        public CondicaoPagamentoAcademicoVO BuscarCondicaoPagamentoAcademicoCabecalho(long seqSolicitacaoMatricula)
        {
            // Busca a pessoa atuação da solicitação
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula);
            var seqPessoaAtuacao = this.SearchProjectionByKey(specSolicitacao, x => x.PessoaAtuacao.Seq);

            // Verifica se a instituição nível do aluno exige curso.
            var instituicaoNiveltipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);
            var exigeCurso = instituicaoNiveltipoVinculoAluno?.ExigeCurso.Value ?? true;

            // Recupera os dados de origem
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            // Busca as informações para chamar a rotina de condição de pagamento do GRA
            var filtro = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new CondicaoPagamentoAcademicoFiltroData
            {
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                QuantidadeCreditos = !exigeCurso ? x.Itens.Where(y => y.SeqDivisaoTurma.HasValue && y.HistoricosSituacao.OrderByDescending(s => s.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).Select(d => new { d.DivisaoTurma.DivisaoComponente.ConfiguracaoComponente.SeqComponenteCurricular, d.DivisaoTurma.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito }).GroupBy(d => d.SeqComponenteCurricular).Select(d => d.FirstOrDefault()).Sum(y => y.Credito) ?? 0 : 0,
                QuantidadeParcelasRetorno = 0,
                SeqCondicaoPagamento = x.SeqCondicaoPagamentoGra ?? 0,
                TipoAluno = x.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Ingressante ? 2 : (x.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Aluno ? (int)x.AlunosHistoricosCicloLetivo.OrderByDescending(a => a.Seq).FirstOrDefault().TipoAluno : 0),
                TipoVinculoAlunoFinanceiro = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
            });

            // Preenche as informações da origem do aluno
            filtro.SeqOrigem = (int)dadosOrigem.SeqOrigem;
            filtro.CodServicoOrigem = dadosOrigem.CodigoServicoOrigem;

            var registro = IntegracaoFinanceiroService.BuscarCondicoesPagamentoAcademico(filtro).TransformList<CondicaoPagamentoAcademicoVO>();
            return registro.FirstOrDefault();
        }

        /// <summary>
        /// Buscar a nova codição de pagamento conforme o gra, baseado nas parcelas do beneficio
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="quantidadeParcelas">Quantidade de parcelas do pagmento</param>
        /// <returns>Lista de condições de pagamento com aquelas parcelas</returns>
        public List<CondicaoPagamentoAcademicoVO> BuscarCondicoesPagamentoAcademicoBaseadoNovoBeneficio(long seqSolicitacaoMatricula, int quantidadeParcelas)
        {
            var pessoaAtuacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => x.PessoaAtuacao); var instituicaoNiveltipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(pessoaAtuacao.Seq);
            var exigeCurso = instituicaoNiveltipoVinculoAluno?.ExigeCurso.Value ?? true;

            // Recupera os dados de origem
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(pessoaAtuacao.Seq);

            var filtro = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new CondicaoPagamentoAcademicoFiltroData
            {
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                QuantidadeCreditos = !exigeCurso ? x.Itens.Where(y => y.SeqDivisaoTurma.HasValue && y.HistoricosSituacao.OrderByDescending(s => s.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).Select(d => new { d.DivisaoTurma.DivisaoComponente.ConfiguracaoComponente.SeqComponenteCurricular, d.DivisaoTurma.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito }).GroupBy(d => d.SeqComponenteCurricular).Select(d => d.FirstOrDefault()).Sum(y => y.Credito) ?? 0 : 0,
                QuantidadeParcelasRetorno = quantidadeParcelas,
                SeqCondicaoPagamento = 0,
                TipoAluno = x.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Ingressante ? 2 : (x.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Aluno ? (int)x.AlunosHistoricosCicloLetivo.OrderByDescending(a => a.Seq).FirstOrDefault().TipoAluno : 0),
                TipoVinculoAlunoFinanceiro = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
            });

            filtro.SeqOrigem = (int)dadosOrigem.SeqOrigem;
            filtro.CodServicoOrigem = dadosOrigem.CodigoServicoOrigem;

            return IntegracaoFinanceiroService.BuscarCondicoesPagamentoAcademico(filtro).TransformList<CondicaoPagamentoAcademicoVO>();
        }

        public List<InformacaoFinanceiraTermoAcademicoVO> BuscarInformacoesFinanceirasTermoAdesao(long seqSolicitacaoMatricula, bool considerarCondicaoSelecionada = false)
        {
            // Rever regra para aluno
            var dadosSolicitacao = this.SearchProjectionByKey(seqSolicitacaoMatricula, x => new
            {
                x.SeqPessoaAtuacao,
                x.SituacaoAtual.SeqSituacaoEtapaSgf,
                x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                x.PessoaAtuacao.TipoAtuacao
            });

            var instituicaoNiveltipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(dadosSolicitacao.SeqPessoaAtuacao);
            var exigeCurso = instituicaoNiveltipoVinculoAluno?.ExigeCurso.Value ?? true;

            InformacaoFinanceiraTermoAcademicoFiltroData filtroData = null;

            // Recupera os dados de origem
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacao.SeqPessoaAtuacao);

            if (dadosSolicitacao.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                filtroData = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new InformacaoFinanceiraTermoAcademicoFiltroData
                {
                    CodigoAluno = x.SeqPessoaAtuacao,
                    AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                    NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                    QuantidadeCreditos = !exigeCurso ? x.Itens
                        .Where(y => y.SeqDivisaoTurma.HasValue &&
                               y.HistoricosSituacao.OrderByDescending(s => s.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                        .Select(d => new { d.DivisaoTurma.SeqTurma, d.DivisaoTurma.DivisaoComponente.ConfiguracaoComponente.ComponenteCurricular.Credito })
                        .GroupBy(d => d.SeqTurma)
                        .Select(d => d.FirstOrDefault())
                        .Sum(y => y.Credito) ?? 0 : 0,
                    SeqCondicaoPagamento = considerarCondicaoSelecionada ? x.SeqCondicaoPagamentoGra ?? 0 : 0,
                    TipoVinculoAlunoFinanceiro = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
                    PercentualMultaServicoAdicional = x.ConfiguracaoProcesso.Processo.ValorPercentualServicoAdicional ?? 0
                });

                filtroData.TipoAluno = 4; // Se for ingressante informar fixo o tipo Ingressante do GRA

                // TASK 35714
                /*Quantidade de créditos: parâmetro enviado somente quando o vínculo da pessoa-atuação foi parametrizado por Instituição-Nível para não exigir curso.
                    - Caso a solicitação estiver com situação atual que é final de processo, enviar como parâmetro a quantidade de créditos
                      dos itens do plano de estudo mais atual do aluno.
                    - Caso a situação não for a final de processo, enviar a quantidade de créditos dos itens da matrícula que estão com
                      situação atual parametrizada para ser final, finalizada com sucesso.
                */

                // Recupera os dados do SGF
                var etapasSGF = SGFHelper.BuscarEtapasSGFCache(dadosSolicitacao.SeqTemplateProcessoSgf);

                // Obtém os sequenciais finais de processo do SGF
                var seqsSituacoesFinalProcesso = etapasSGF.SelectMany(e => e.Situacoes).Where(s => s.SituacaoFinalProcesso).Select(s => s.Seq).ToList();

                if (seqsSituacoesFinalProcesso.Contains(dadosSolicitacao.SeqSituacaoEtapaSgf))
                {
                    // Busca o seq do aluno vinculado à este ingressante
                    var seqPessoaAtuacaoPlanoEstudo = (long?)AlunoHistoricoDomainService.SearchProjectionByKey(new AlunoHistoricoFilterSpecification { SeqIngressante = dadosSolicitacao.SeqPessoaAtuacao }, x => x.SeqAluno);

                    if (seqPessoaAtuacaoPlanoEstudo.HasValue && seqPessoaAtuacaoPlanoEstudo.Value > 0)
                    {
                        // Buscas a quantidade de créditos do plano atual do aluno
                        var quantidadeCreditosPlanoEstudo = PlanoEstudoDomainService.SearchProjectionByKey(new PlanoEstudoFilterSpecification
                        {
                            SeqAluno = seqPessoaAtuacaoPlanoEstudo.Value,
                            Atual = true
                        }, x => x.Itens.Where(i => i.ConfiguracaoComponente.ComponenteCurricular.Credito.HasValue).Sum(i => i.ConfiguracaoComponente.ComponenteCurricular.Credito));
                        filtroData.QuantidadeCreditos = quantidadeCreditosPlanoEstudo ?? 0;
                    }
                }
            }
            else
            {
                filtroData = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new InformacaoFinanceiraTermoAcademicoFiltroData
                {
                    CodigoAluno = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao.Value,
                    AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                    NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                    TipoVinculoAlunoFinanceiro = (x.PessoaAtuacao as Aluno).TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
                    PercentualMultaServicoAdicional = x.ConfiguracaoProcesso.Processo.ValorPercentualServicoAdicional ?? 20
                    //Conversado com a Mariana quando não for cadastrado no banco o default deve ser 20 %
                });

                filtroData.TipoAluno = (int)TipoAluno.Veterano;
                filtroData.QuantidadeCreditos = 0;
                filtroData.SeqCondicaoPagamento = 0;
            }

            filtroData.SeqOrigem = (int)dadosOrigem.SeqOrigem;
            filtroData.CodServicoOrigem = dadosOrigem.CodigoServicoOrigem;

            var resultado = IntegracaoFinanceiroService.BuscarInformacoesFinanceirasTermoAcademico(filtroData).TransformList<InformacaoFinanceiraTermoAcademicoVO>();
            return resultado;
        }

        public CondicaoPagamentoSolicitacaoMatriculaVO BuscarCondicaoPagamentoSolicitacaoMatricula(long seqSolicitacaoMatricula, bool considerarApenasCondicaoPagamentoSelecionada = false)
        {
            // Busca no GRA as condições de pagamento e valor
            var condicoes = BuscarCondicoesPagamentoAcademico(seqSolicitacaoMatricula, considerarApenasCondicaoPagamentoSelecionada);

            var dadosCondicaoPagamento = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new { SeqCondicaoPagamentoGra = x.SeqCondicaoPagamentoGra, TermoAderido = x.CodigoAdesao.HasValue && x.DataAdesao.HasValue });
            var ret = new CondicaoPagamentoSolicitacaoMatriculaVO
            {
                SeqCondicaoPagamento = dadosCondicaoPagamento.SeqCondicaoPagamentoGra,
                ValorTotal = condicoes?.FirstOrDefault()?.ValorParcelas * condicoes?.FirstOrDefault()?.QuantidadeParcelas ?? 0,
                TermoAderido = dadosCondicaoPagamento.TermoAderido,
                CondicoesPagamento = condicoes.Select(x => new SMCDatasourceItem
                {
                    Descricao = string.Format(MessagesResource.MSG_Parcela_Condicao_Pagamento, x.QuantidadeParcelas, x.ValorParcelas.ToString("#,##0.00")),
                    Seq = x.SeqCondicaoPagamento
                }).ToList()
            };

            return ret;
        }

        /// <summary>
        /// Grava uma solicitação de matricula com seus respectivos itens
        /// </summary>
        /// <param name="solicitacaoMatriculaVO">Dados da solicitação de matricula a ser gravado</param>
        /// <returns>Sequencial da solicitação de matricula gravado</returns>
        /// <exception cref="TermoAdesaoSemContratoVigenteException">Caso não seja encontrado um contrato vigente vinculado ao curso ou ao nível de ensino</exception>
        public long CriarNovaSolicitacaoMatricula(SolicitacaoMatriculaVO solicitacaoMatriculaVO)
        {
            // FIX: Verificar o uso de 3 transações aninhadas
            //using (var unitOfWork = SMCUnitOfWork.Begin())
            //{
            var solicitacaoMatricula = solicitacaoMatriculaVO.Transform<SolicitacaoMatricula>();

            // 2. Associa a primeira etapa do sgf à solicitação de matricula
            var configuracaoProcesso = ConfiguracaoProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoProcesso>(solicitacaoMatricula.SeqConfiguracaoProcesso),
                                                                    x => new
                                                                    {
                                                                        ConfiguracoesEtapa = x.ConfiguracoesEtapa.Select(f => new
                                                                        {
                                                                            SeqConfiguracaoEtapa = f.Seq,
                                                                            f.ProcessoEtapa.SeqEtapaSgf
                                                                        }),
                                                                        // 5.2
                                                                        //
                                                                        // Esta consulta não estava sendo utilizada e estava quebrando a carga de ingressantes.
                                                                        // Como tem um FisrtOrDefault, caso não retorne resultados a consulta falha.
                                                                        //
                                                                        //SeqSituacaoItemMatricula = x.ConfiguracoesEtapa.SelectMany(
                                                                        //    f => f.ProcessoEtapa.SituacoesItemMatricula.Where(g => g.SituacaoInicial)).FirstOrDefault().Seq,
                                                                        DocumentosRequeridos = x.ConfiguracoesEtapa.SelectMany(g => g.DocumentosRequeridos),
                                                                        x.Processo.SeqServico
                                                                    });
            var seqsEtapaSgf = configuracaoProcesso.ConfiguracoesEtapa.Select(f => f.SeqEtapaSgf).ToArray();
            var primeiraEtapa = EtapaService.BuscarEtapas(seqsEtapaSgf).OrderBy(o => o.Ordem).First();

            solicitacaoMatricula.Etapas = new List<SolicitacaoServicoEtapa>()
            {
                new SolicitacaoServicoEtapa()
                {
                    // Associa a primeira etapa do sgf à solicitação de matricula
                    SeqConfiguracaoEtapa = configuracaoProcesso.ConfiguracoesEtapa.First(f => f.SeqEtapaSgf == primeiraEtapa.Seq).SeqConfiguracaoEtapa,
                    SolicitacaoServico = solicitacaoMatricula
                }
            };

            //var seqSituacaoItemMatricula = configuracaoProcesso.SeqSituacaoItemMatricula;
            ProcessaItensSolicitacaoMatricula(solicitacaoMatriculaVO, solicitacaoMatricula);

            var bloqueios = new List<PessoaAtuacaoBloqueio>();
            long? seqTipoMotivoBloqueio = null;

            // 6. Lista de documentos requeridos
            if (configuracaoProcesso.DocumentosRequeridos != null && configuracaoProcesso.DocumentosRequeridos.Any())
            {
                solicitacaoMatricula.DocumentosRequeridos = new List<SolicitacaoDocumentoRequerido>();
                foreach (var documento in configuracaoProcesso.DocumentosRequeridos)
                {
                    // 6.1.1. Inicar documento como aguardando entrega
                    var solicitacaoDocumento = new SolicitacaoDocumentoRequerido()
                    {
                        SeqDocumentoRequerido = documento.Seq,
                        SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega,
                        EntregueAnteriormente = false
                    };

                    // Busca os documentos do inscrito no GPI
                    if (solicitacaoMatriculaVO.Documentos != null && solicitacaoMatriculaVO.Documentos.Any())
                    {
                        //6.1.2 Atualiza a entrega caso o documento exista no gpi
                        var documentoGPI = solicitacaoMatriculaVO.Documentos.FirstOrDefault(f => f.SeqTipoDocumento == documento.SeqTipoDocumento);
                        if (documentoGPI != null)
                        {
                            solicitacaoDocumento.DataEntrega = documentoGPI.DataEntrega;
                            solicitacaoDocumento.FormaEntregaDocumento = documentoGPI.FormaEntregaDocumento;
                            solicitacaoDocumento.VersaoDocumento = documentoGPI.VersaoDocumento;
                            solicitacaoDocumento.Observacao = documentoGPI.Observacao;
                            solicitacaoDocumento.SituacaoEntregaDocumento = documentoGPI.SituacaoEntregaDocumento;
                            solicitacaoDocumento.EntregueAnteriormente = false;

                            if (documentoGPI.SeqArquivoAnexado.HasValue)
                            {
                                var arquivo = IIntegracaoInscricaoService.BuscarArquivoAnexado(documentoGPI.SeqArquivoAnexado.Value);
                                solicitacaoDocumento.ArquivoAnexado = arquivo.Transform<ArquivoAnexado>();
                            }

                            // 6.1.2.1 Bloqueio para documentos pendentes
                            if (documentoGPI.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)
                            {
                                if (seqTipoMotivoBloqueio == null)
                                {
                                    seqTipoMotivoBloqueio = MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.ENTREGA_DOCUMENTACAO);
                                }
                                var bloqueio = new PessoaAtuacaoBloqueio()
                                {
                                    SeqPessoaAtuacao = solicitacaoMatriculaVO.SeqPessoaAtuacao,
                                    DescricaoReferenciaAtuacao = solicitacaoMatriculaVO.DescricaoPessoaAtuacao,
                                    SeqMotivoBloqueio = seqTipoMotivoBloqueio.Value,
                                    DataBloqueio = documentoGPI.DataPrazoEntrega.GetValueOrDefault(),
                                    Descricao = "Pendência na entrega da documentação",
                                    SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                                    CadastroIntegracao = true,
                                    Itens = new List<PessoaAtuacaoBloqueioItem>()
                                    {
                                        new PessoaAtuacaoBloqueioItem()
                                        {
                                            Descricao = documentoGPI.DescricaoTipoDocumento,
                                            SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                                            CodigoIntegracaoSistemaOrigem = documento.SeqTipoDocumento.ToString()
                                        }
                                    }
                                };

                                bloqueios.Add(bloqueio);
                            }
                        }
                    }

                    solicitacaoMatricula.DocumentosRequeridos.Add(solicitacaoDocumento);
                }
            }

            // 7. Termo de adesão
            solicitacaoMatriculaVO.TermoAdesaoVO.SeqServico = configuracaoProcesso.SeqServico;
            solicitacaoMatricula.SeqTermoAdesao = TermoAdesaoDomainService.BuscarTermoAdesao(solicitacaoMatriculaVO.TermoAdesaoVO);

            this.SaveEntity(solicitacaoMatricula);

            // Grava os bloqueios de documentos obrigatórios pendentes
            foreach (var bloqueio in bloqueios)
            {
                bloqueio.SeqSolicitacaoServico = solicitacaoMatricula.Seq;
                PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueio);
            }

            // Atualiza a situação da documentação.
            RegistroDocumentoDomainService.ValidarDocumentoObrigatorio(solicitacaoMatricula.Seq);

            var situacaoInicialEtapaSgf = EtapaService.BuscarSituacoesDaEtapa(primeiraEtapa.Seq).First(f => f.SituacaoInicialEtapa).Seq;
            // 3. Inclui no historico de situações a situação que foi configurada na primeira etapa para ser a etapa inicial
            SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(solicitacaoMatricula.Etapas.First().Seq, situacaoInicialEtapaSgf);

            //unitOfWork.Commit();

            return solicitacaoMatricula.Seq;
            //}
        }

        /// <summary>
        /// Verifica se existe solicitação de matrícula para solicitação de serviço, se não existir cria um solicitação de matrícula apenas com o sequencial da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void CriarSolicitacaoMatriculaPorSolicitacaoServico(long seqSolicitacaoServico)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoServico));

            if (registro == null)
                ExecuteSqlCommand(string.Format(_inserirSolicitacaoMatriculaPorSolicitacaoServico, seqSolicitacaoServico));
        }

        public void AtualizarItensSolicitacaoMatricula(SolicitacaoMatriculaVO solicitacaoMatriculaVO, SolicitacaoMatricula solicitacaoMatricula)
        {
            ProcessaItensSolicitacaoMatricula(solicitacaoMatriculaVO, solicitacaoMatricula);

            this.SaveEntity(solicitacaoMatricula);
        }

        private void ProcessaItensSolicitacaoMatricula(SolicitacaoMatriculaVO solicitacaoMatriculaVO, SolicitacaoMatricula solicitacaoMatricula)
        {
            if (solicitacaoMatriculaVO.SeqsDivisaoTurma != null && solicitacaoMatriculaVO.SeqsDivisaoTurma.Any())
            {
                var seqSituacaoItemMatricula = ConfiguracaoProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoProcesso>(solicitacaoMatricula.SeqConfiguracaoProcesso),
                                                                    x => x.ConfiguracoesEtapa.SelectMany(
                                                                            f => f.ProcessoEtapa.SituacoesItemMatricula.Where(g => g.SituacaoInicial)).FirstOrDefault().Seq
                                                                    );

                solicitacaoMatricula.Itens = new List<SolicitacaoMatriculaItem>();

                // 5.1 Inclui registro de itens com os itens da oferta da campanha.
                foreach (var divisaoTurma in solicitacaoMatriculaVO.SeqsDivisaoTurma)
                {
                    solicitacaoMatricula.Itens.Add(new SolicitacaoMatriculaItem()
                    {
                        SeqDivisaoTurma = divisaoTurma.Seq,
                        SeqConfiguracaoComponente = divisaoTurma.SeqConfiguracaoComponente,
                        PertencePlanoEstudo = false,
                        // 5.2
                        HistoricosSituacao = new List<SolicitacaoMatriculaItemHistoricoSituacao>()
                            {
                                new SolicitacaoMatriculaItemHistoricoSituacao()
                                {
                                    SeqSituacaoItemMatricula = seqSituacaoItemMatricula
                                }
                            }
                    });
                }
            }
        }

        public void CriaBloqueiosFinanceirosMatricula(long seqSolicitacaoMatricula, long seqPessoaAtuacao, TipoAtuacao? tipoAtuacao = null)
        {
            var instituicaoNiveltipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);
            var exigeCurso = instituicaoNiveltipoVinculoAluno?.ExigeCurso.Value ?? true;

            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano, // Validar.. não ta na documentação. chamar?
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero, // Validar.. não ta na documentação. chamar?
                                                                                        //DescricaoVinculo = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.Descricao,
                                                                                        //SeqsTipoTermoIntercambio = (x.PessoaAtuacao as Ingressante).TermosIntercambio.Select(t => t.TermoIntercambio.SeqTipoTermoIntercambio),
                Parcelas = x.GrupoEscalonamento.Itens.SelectMany(i => i.Parcelas),
                DescricaoPessoaAtuacao = x.PessoaAtuacao.Descricao,
                SeqCicloLetivo = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                TipoAluno = (x.PessoaAtuacao is Ingressante) ? TipoAluno.Calouro : TipoAluno.Veterano,
                CodAluno = (long?)(x.PessoaAtuacao as Ingressante).Seq ?? (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao
            });
            // RN_MAT_013
            // 1. Se nenhuma parcela for retornada, não criar bloqueio.
            if (dadosSolicitacao.Parcelas != null && dadosSolicitacao.Parcelas.Any())
            {
                // Recupera os dados de origem
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

                // Busca os dados do evento letivo
                var dadosEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosSolicitacao.SeqCicloLetivo.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, dadosSolicitacao.TipoAluno, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                // Carrega os benefícios da pessoa atuação
                var beneficios = PessoaAtuacaoBeneficioDomainService.SearchBySpecification(new PessoaAtuacaoBeneficioFilterSpecification
                {
                    SeqPessoaAtuacao = seqPessoaAtuacao,
                    IncideParcelaMatricula = true,
                    DesbloqueioTemporario = true,
                    TipoAtuacao = tipoAtuacao,
                    DataInicioCicloLetivo = dadosEventoLetivo.DataInicio,
                    // Modificado pois agora este controle de incide ou não na parcela de matrícula é feito dentro da rotina de criar o bloqueio
                    SituacaoChancelaBeneficio = SituacaoChancelaBeneficio.Deferido
                }, IncludesPessoaAtuacaoBeneficio.ConfiguracaoBeneficio | IncludesPessoaAtuacaoBeneficio.HistoricoVigencias | IncludesPessoaAtuacaoBeneficio.Beneficio).ToList();

                beneficios.SMCForEach(f =>
                {
                    if (f.HistoricoVigencias.SMCAny() && f.ConfiguracaoBeneficio != null)
                    {
                        f.ConfiguracaoBeneficio.DataInicioValidade = f.HistoricoVigencias.FirstOrDefault(hv => hv.Atual).DataInicioVigencia;
                        f.ConfiguracaoBeneficio.DataFimValidade = f.HistoricoVigencias.FirstOrDefault(hv => hv.Atual).DataFimVigencia;
                    }
                });

                //RN_MAT_013 - Inclusão bloqueio financeiro parcela matrícula em aberto
                BloqueioFinanceiroParcelaMatriculaEmAberto(dadosSolicitacao.Parcelas, beneficios, new ParcelaFiltroData
                {
                    NumeroCicloLetivo = dadosSolicitacao.NumeroCicloLetivo,
                    AnoCicloLetivo = dadosSolicitacao.AnoCicloLetivo,
                    SeqPessoaAtuacao = dadosSolicitacao.CodAluno.GetValueOrDefault(),
                    SeqOrigem = (int)dadosOrigem.SeqOrigem,
                    CodServicoOrigem = dadosOrigem.CodigoServicoOrigem,
                    DataInicioPeriodoLetivo = dadosEventoLetivo.DataInicio,
                    DataFimPeriodoLetivo = DateTime.Parse("2018-12-31"), // FIX: Retirar data fixa quando sair da origem 1 (GRA) dadosEventoLetivo.DataFim,
                    TipoAluno = dadosSolicitacao.TipoAluno == TipoAluno.Calouro ? 4 : 1, // No GRA, quando for ingressante deve ser enviado o valor 4. Veterano, 1
                }, dadosSolicitacao.DescricaoPessoaAtuacao, seqPessoaAtuacao, seqSolicitacaoMatricula, dadosEventoLetivo);
            }
            else
                throw new SMCApplicationException("Nenhuma parcela foi encontrada para o grupo de escalonamento em questão.");
        }

        /// <summary>
        /// RN_MAT_013 - Inclusão bloqueio financeiro parcela matrícula em aberto
        /// </summary>
        /// <param name="seqIngressante"></param>
        public void BloqueioFinanceiroParcelaMatriculaEmAberto(IEnumerable<GrupoEscalonamentoItemParcela> parcelas, IEnumerable<PessoaAtuacaoBeneficio> beneficios, ParcelaFiltroData filtroFinanceiro, string descricaoPessoaAtuacao, long seqPessoaAtuacao, long? seqSolicitacaoServico, DatasEventoLetivoVO datasEventoLetivo)
        {
            // Busca as parcelas no financeiro
            var parcelasFinanceiro = IntegracaoFinanceiroService.BuscarParcelas(filtroFinanceiro);

            // Verifica os benefícios
            var beneficioDesbloqueioTemporario = beneficios.FirstOrDefault(b => b.Beneficio.IncluirDesbloqueioTemporario);
            var beneficioDesbloqueioFechada = beneficios.FirstOrDefault(b => !b.Beneficio.IncluirDesbloqueioTemporario);

            // Encontra a parcela P0 configurada no escalonamento
            var parcelaEscalonamentoP0Adicional = parcelas.FirstOrDefault(p => !p.NumeroParcela.HasValue && p.ServicoAdicional);
            var parcelaEscalonamentoP0 = parcelas.FirstOrDefault(p => !p.NumeroParcela.HasValue && !p.ServicoAdicional);

            // Para cada parcela do financeiro, cria um bloqueio para o pessoa atuação
            foreach (var parcelaFinanceiro in parcelasFinanceiro)
            {
                // Busca a parcela correspondente no escalonamento. Caso não ache, usa a P0
                var parcela = parcelas.FirstOrDefault(p => p.NumeroParcela == parcelaFinanceiro.NumeroParcela);
                parcela = parcela ?? (parcelaFinanceiro.ServicoAdicional ? parcelaEscalonamentoP0Adicional : parcelaEscalonamentoP0);

                if (parcela != null)
                {
                    var bloqueio = new PessoaAtuacaoBloqueio()
                    {
                        SeqMotivoBloqueio = parcela.SeqMotivoBloqueio,
                        DataAlteracao = DateTime.Now,
                        DataInclusao = DateTime.Now,
                        CadastroIntegracao = true,
                        DescricaoReferenciaAtuacao = descricaoPessoaAtuacao,
                        DataBloqueio = DateTime.Now,
                        Descricao = parcela.Descricao,
                        SeqPessoaAtuacao = seqPessoaAtuacao,
                        SeqSolicitacaoServico = seqSolicitacaoServico,
                        Itens = new List<PessoaAtuacaoBloqueioItem> {
                            new PessoaAtuacaoBloqueioItem
                            {
                                Descricao = parcela.Descricao,
                                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                                CodigoIntegracaoSistemaOrigem = parcelaFinanceiro.SeqParcela.ToString(),
                            }
                        }
                    };

                    /*1 Verificar se a pessoa-atuação possui um benefício associado, que na sua configuração possui "Inclui desbloqueio temporário?" igual a "Sim"*/

                    // Calouro/Ingressante: TipoAluno = 4
                    // Veterano/Aluno:  TipoAluno = 1
                    // Regra alterada a pedido da Ellen para igualar as regras de desbloqueio da seção de benefícios do administrativo
                    // Caso seja ingressante, apenas faz o desbloqueio se incide na parcela de matrícula
                    if (!parcelaFinanceiro.ServicoAdicional && beneficioDesbloqueioTemporario != null && (filtroFinanceiro.TipoAluno == 1 || (filtroFinanceiro.TipoAluno == 4 && beneficioDesbloqueioTemporario.IncideParcelaMatricula)))
                    {
                        // Task 52175 - Alteração regra RN_MAT_013
                        // SOMENTE para a pessoa - atuação do tipo de atuação "ALUNO", verificar TAMBÉM se a data início do ciclo letivo* do
                        // processo é maior ou igual a data início do benefício E a data início do ciclo letivo*é menor ou igual a data fim do benefício.
                        if (filtroFinanceiro.TipoAluno == 1)
                        {
                            if (beneficioDesbloqueioTemporario != null)
                            {
                                var vigencia = beneficioDesbloqueioTemporario.HistoricoVigencias.FirstOrDefault(h => h.Atual);
                                if (datasEventoLetivo.DataInicio >= vigencia.DataInicioVigencia && datasEventoLetivo.DataInicio <= vigencia.DataFimVigencia)
                                {
                                    bloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                    bloqueio.TipoDesbloqueio = TipoDesbloqueio.Temporario;
                                    bloqueio.DataDesbloqueioTemporario = DateTime.Now;
                                    bloqueio.UsuarioDesbloqueioTemporario = $"{beneficioDesbloqueioTemporario.Seq} - {beneficioDesbloqueioTemporario.Beneficio.Descricao}";
                                    bloqueio.JustificativaDesbloqueio = $"{beneficioDesbloqueioTemporario.Seq} - {beneficioDesbloqueioTemporario.Beneficio.Descricao}";
                                }
                                else
                                {
                                    bloqueio.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                                }
                            }
                            else
                            {
                                bloqueio.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                            }
                        }
                        else
                        {
                            /* Para TODAS as parcelas existentes incluir bloqueios financeiros para a pessoa-atuação, de acordo com os parâmetros
                                - Motivo bloqueio: sequencial dos bloqueios que foram parametrizados para as parcelas de matrícula do escalonamento da etapa em questão, associado à pessoa-atuação
                                - Situação: "Desbloqueado"
                                - Tipo de desbloqueio: "Temporário"
                                - Data do bloqueio: data atual do sistema
                                - Data do desbloqueio temporário: data atual do sistema
                                - Usuário de desbloqueio temporário: <seq_benefício> "-" <dsc_benefício> (benefício associado a pessoa-atuação)
                                - Data da última atualização: data corrente do sistema
                                - Indicador cadastro de integração: Sim
                                - Descrição bloqueio referência: descrição do vínculo institucional da pessoa-atuação em questão
                                - Descrição bloqueio: descrição da parcela em que o motivo bloqueio em questão foi associado no escalonamento da etapa associado à pessoa-atuação
                                - Justificativa do desbloqueio temporário: justificativa de desbloqueio cadastrada no benefício em questão, associado à pessoa-atuação
                                - Demais campos: nulos
                                1.1 Incluir itens para cada bloqueio incluído:
                                    - Item: descrição da parcela em que o motivo bloqueio em questão foi associado no escalonamento da etapa associado à pessoa-atuação.
                                    - Situação: "Bloqueado"
                                    - Item integração origem: sequencial referente à parcela no GRA
                                    - Demais campos: nulos*/

                            bloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                            bloqueio.TipoDesbloqueio = TipoDesbloqueio.Temporario;
                            bloqueio.DataDesbloqueioTemporario = DateTime.Now;
                            bloqueio.UsuarioDesbloqueioTemporario = $"{beneficioDesbloqueioTemporario.Seq} - {beneficioDesbloqueioTemporario.Beneficio.Descricao}";
                            bloqueio.JustificativaDesbloqueio = $"{beneficioDesbloqueioTemporario.Seq} - {beneficioDesbloqueioTemporario.Beneficio.Descricao}";
                        }
                    }

                    /*2 Se a pessoa-atuação não possuir benefício ou possuir um benefício que possui "Inclui desbloqueio temporário?" igual a "Não"
                    ou  se a pessoa-atuação não possui  benefício:*/
                    else
                    {
                        /*2.1 Verificar se serão retornadas parcelas, de acordo com a RN_MAT_032 - Retorna situação parcela no financeiro e para
                        CADA PARCELA FECHADA RETORNADA (situação= 1) incluir bloqueios financeiros para a pessoa-atuação, de acordo com os parâmetros:*/
                        if (parcelaFinanceiro.SituacaoParcela == Financeiro.Common.Areas.GRA.Enums.SituacaoParcela.Fechada)
                        {
                            /*  - Motivo bloqueio: sequencial dos bloqueios que foram parametrizados para as parcelas de matrícula do escalonamento da etapa em questão, associado à pessoa-atuação
                                - Situação: "Desbloqueado"
                                - Tipo de desbloqueio: "Temporário"
                                - Data do bloqueio: data atual do sistema
                                - Data do desbloqueio temporário: data atual do sistema
                                - Usuário de desbloqueio temporário: <seq_benefício> "-" <dsc_benefício> (benefício associado a pessoa-atuação)
                                - Data da última atualização: data corrente do sistema
                                - Indicador cadastro de integração: Sim
                                - Descrição bloqueio referência: descrição do vínculo institucional da pessoa-atuação em questão
                                - Descrição bloqueio: descrição da parcela em que o motivo bloqueio em questão foi associado no escalonamento da etapa associado à pessoa-atuação
                                - Justificativa do desbloqueio temporário: justificativa de desbloqueio cadastrada no benefício em questão, associado à pessoa-atuação
                                - Demais campos: nulos
                                    2.1.1 Incluir itens para cada bloqueio incluído:
                                        - Item: descrição da parcela em que o motivo bloqueio em questão foi associado no escalonamento da etapa associado à pessoa-atuação.
                                        - Situação: "Bloqueado"
                                        - Item integração origem: sequencial referente à parcela no GRA
                                        - Demais campos: nulos*/

                            // Obrigatoriamente, pelo fato de estar fechado, existe um benefício para este ingressante.

                            bloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                            bloqueio.TipoDesbloqueio = TipoDesbloqueio.Temporario;
                            bloqueio.DataDesbloqueioTemporario = DateTime.Now;

                            if (beneficioDesbloqueioFechada != null)
                            {
                                bloqueio.UsuarioDesbloqueioTemporario = $"{beneficioDesbloqueioFechada.Seq} - {beneficioDesbloqueioFechada.Beneficio.Descricao}";
                                bloqueio.JustificativaDesbloqueio = $"{beneficioDesbloqueioFechada.Seq} - {beneficioDesbloqueioFechada.Beneficio.Descricao}";
                            }
                            else
                            {
                                bloqueio.UsuarioDesbloqueioTemporario = $"Integração do Sistema Financeiro";
                                bloqueio.JustificativaDesbloqueio = $"Parcela fechada, de acordo com o retorno da integração do sistema financeiro.";
                            }
                        }
                        /*2.2 CADA PARCELA ABERTA RETORNADA (situação= 0), incluir bloqueios financeiros para a pessoa-atuação, de acordo com os parâmetros:*/
                        else if (parcelaFinanceiro.SituacaoParcela == Financeiro.Common.Areas.GRA.Enums.SituacaoParcela.Aberta)
                        {
                            /*  - Motivo bloqueio: sequencial dos bloqueios que foram parametrizados para as parcelas de matrícula do escalonamento da etapa em questão, associado à pessoa-atuação
                                - Situação: "Bloqueado"
                                - Data do bloqueio: data atual do sistema
                                - Data da última atualização: data corrente do sistema
                                - Indicador cadastro de integração: Sim
                                - Descrição bloqueio referência: descrição do vínculo institucional da pessoa-atuação em questão
                                - Descrição bloqueio: descrição da parcela em que o motivo bloqueio em questão foi associado no escalonamento da etapa associado à pessoa-atuação
                                - Demais campos: nulos
                                2.2.2 Incluir itens para cada bloqueio incluído:
                                    - Item: descrição da parcela em que o motivo bloqueio em questão foi associado no escalonamento da etapa associado à pessoa-atuação.
                                    - Situação: "Bloqueado"
                                    - Item integração origem: sequencial referente à parcela no GRA
                                    - Demais campos: nulos*/

                            bloqueio.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                        }
                    }

                    PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueio);
                }
                else
                    throw new SMCApplicationException("A parcela " + parcelaFinanceiro.NumeroParcela + " não foi encontrada para o grupo de escalonamento em questão.");
            }
        }

        /// <summary>
        /// Busca o sequencial do processo etapa de acordo com a solicitação de matrícula e a configuração etapa
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial solicitação matrícula</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial configuração etapa</param>
        /// <returns>Retorna o sequencial do processo etapa</returns>
        public long BuscarProcessoEtapaPorSolicitacaoMatricula(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa)
        {
            var dadosSolicitacao = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula),
                x => (long?)x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == seqConfiguracaoEtapa).ConfiguracaoEtapa.SeqProcessoEtapa);

            return dadosSolicitacao.GetValueOrDefault();
        }

        /// <summary>
        /// Buscar ciclo letivo do processor por solicitação matricula 
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Sequencial do ciclo letivo</returns>
        public long? BuscarCicloLetivoProcessoSolicitacaoMatricula(long seqSolicitacaoMatricula)
        {
            return this.SearchProjectionByKey(seqSolicitacaoMatricula, p => p.ConfiguracaoProcesso.Processo.SeqCicloLetivo);
        }

        #region [ Chancela ]

        /// <summary>
        /// Verifica se alguma atividade acadêmica da solicitação que esteja com situação finalizada com sucesso e pertença ao plano (ou seja, solicitação de exclusão), possui histórico escolar
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de serviço</param>
        /// <param name="desativarFiltroDados">Desativar filtro de dados ou não</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <returns>Lista de descrição dos componentes afetados</returns>
        public List<string> VerificarChancelaExclusaoAtividadesAcademicasComHistoricoEscolar(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, List<(long SeqItem, long SeqSituacaoItem)> seqsSolicitacaoMatriculaItem, bool desativarFiltroDados)
        {
            try
            {
                //Necessário remover o filtro global porque existe chancela de secretaria diferente ao do aluno no caso de disciplina eletiva
                if (desativarFiltroDados)
                    this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

                // Recupera os dados da chancela que está sendo feita
                var dadosChancela = this.SearchProjectionByKey(seqSolicitacaoMatricula, x => new
                {
                    TurmaPertence = x.Itens.Where(i => !i.SeqDivisaoTurma.HasValue && i.PertencePlanoEstudo).Select(i => new
                    {
                        i.Seq,
                        i.ConfiguracaoComponente.SeqComponenteCurricular,
                        i.SeqConfiguracaoComponente
                    }).ToList(),
                    SeqAlunoHistorico = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(a => a.Atual).Seq,
                    x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                    SituacoesItensChancela = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.Seq == seqConfiguracaoEtapa).ProcessoEtapa.SituacoesItemMatricula.OrderBy(s => s.Descricao).Select(i => new { SeqEtapa = i.ProcessoEtapa.SeqEtapaSgf, Descricao = i.Descricao, Seq = i.Seq, Classificacao = i.ClassificacaoSituacaoFinal, SituacaoInicial = i.SituacaoInicial }).ToList(),
                });

                // Sequencial da situação de sucesso da chancela
                var situacaoFinalSucessoChancela = dadosChancela.SituacoesItensChancela.FirstOrDefault(s => s.Classificacao == ClassificacaoSituacaoFinal.FinalizadoComSucesso);

                // Seleciona os seqs dos itens que estão com classificação de sucesso (deferido)
                var seqsItensARemover = seqsSolicitacaoMatriculaItem.Where(s => s.SeqSituacaoItem == situacaoFinalSucessoChancela.Seq).Select(i => i.SeqItem);

                // Verifica quais desses itens que estão com situação de deferimento pertencem ao plano
                var atividadesRemover = dadosChancela.TurmaPertence.Where(a => seqsItensARemover.Contains(a.Seq));

                // Caso esteja removendo algum item que pertence ao plano
                if (atividadesRemover != null && atividadesRemover.Any())
                {
                    // Caso tenha algum item a ser removido, verifica se existe lançamento de nota para o mesmo
                    var atividadesExcluidasComHistorico = HistoricoEscolarDomainService.SearchProjectionBySpecification(new HistoricoEscolarFilterSpecification
                    {
                        SeqAlunoHistorico = dadosChancela.SeqAlunoHistorico,
                        SeqCicloLetivo = dadosChancela.SeqCicloLetivo,
                        SeqsComponenteCurricular = atividadesRemover.Select(d => d.SeqComponenteCurricular).ToList(),
                        SituacoesHistoricoEscolar = new SituacaoHistoricoEscolar[] { SituacaoHistoricoEscolar.Aprovado, SituacaoHistoricoEscolar.Reprovado, SituacaoHistoricoEscolar.AlunoSemNota }
                    }, x => x.SeqComponenteCurricular).ToList();

                    // Caso tenha lançamento de nota, retorna a descrição do item
                    if (atividadesExcluidasComHistorico != null && atividadesExcluidasComHistorico.Any())
                    {
                        // Busca a descrição completa dos itens
                        var filtroComponentes = new ConfiguracaoComponenteFilterSpecification() { SeqsComponentesCurriculares = atividadesExcluidasComHistorico.ToArray() };
                        var componentesDescricaoCompleta = ConfiguracaoComponenteDomainService.BuscarConfiguracoesComponentes(filtroComponentes);

                        return componentesDescricaoCompleta.Where(c => atividadesExcluidasComHistorico.Any(a => a == c.SeqComponenteCurricular)).Select(c => c.DescricaoFormatada).ToList();
                    }
                }
                return null;
            }
            finally
            {
                //Necessário remover o filtro global porque existe chancela de secretaria diferente ao do aluno no caso de disciplina eletiva
                if (desativarFiltroDados)
                    this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
        }

        /// <summary>
        /// Verifica se alguma atividade acadêmica da solicitação que esteja com situação finalizada com sucesso e possui histórico escolar
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de serviço</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="seqsSolicitacaoMatriculaItem">Sequencial dos itens da solicitação de matrícula e suas situações selecionadas na tela</param>
        /// <returns>Lista de descrição dos componentes afetados</returns>
        public List<string> VerificarEfetivacaoAtividadesAcademicasComHistoricoEscolar(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, List<long> seqsSolicitacaoMatriculaItem)
        {
            try
            {
                // Recupera os dados da chancela que está sendo feita
                var dadosChancela = this.SearchProjectionByKey(seqSolicitacaoMatricula, x => new
                {
                    TurmaPertence = x.Itens.Where(i => !i.SeqDivisaoTurma.HasValue).Select(i => new
                    {
                        i.Seq,
                        i.ConfiguracaoComponente.SeqComponenteCurricular,
                        i.SeqConfiguracaoComponente
                    }).ToList(),
                    SeqAlunoHistorico = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(a => a.Atual).Seq
                });

                var atividadeEfetivadas = dadosChancela.TurmaPertence.Where(w => seqsSolicitacaoMatriculaItem.Contains(w.Seq)).ToList();
                // Caso esteja removendo algum item que pertence ao plano
                if (atividadeEfetivadas != null && atividadeEfetivadas.Any())
                {
                    // Caso tenha algum item a ser deferido, verifica se existe lançamento de nota para o mesmo
                    var atividadesDeferidasComHistorico = HistoricoEscolarDomainService.SearchProjectionBySpecification(new HistoricoEscolarFilterSpecification
                    {
                        SeqAlunoHistorico = dadosChancela.SeqAlunoHistorico,
                        SeqsComponenteCurricular = atividadeEfetivadas.Select(d => d.SeqComponenteCurricular).ToList(),
                        SituacoesHistoricoEscolar = new SituacaoHistoricoEscolar[] { SituacaoHistoricoEscolar.Aprovado, SituacaoHistoricoEscolar.Dispensado }
                    }, x => x.SeqComponenteCurricular).ToList();

                    // Caso tenha lançamento de nota, retorna a descrição do item
                    if (atividadesDeferidasComHistorico != null && atividadesDeferidasComHistorico.Any())
                    {
                        // Busca a descrição completa dos itens
                        var filtroComponentes = new ConfiguracaoComponenteFilterSpecification() { SeqsComponentesCurriculares = atividadesDeferidasComHistorico.ToArray() };
                        var componentesDescricaoCompleta = ConfiguracaoComponenteDomainService.BuscarConfiguracoesComponentes(filtroComponentes);

                        return componentesDescricaoCompleta.Where(c => atividadesDeferidasComHistorico.Any(a => a == c.SeqComponenteCurricular)).Select(c => c.DescricaoFormatada).ToList();
                    }
                }
                return null;
            }
            finally
            {

            }
        }


        /// <summary>
        /// Verifica se alguma turma da solicitação que esteja com situação finalizada com sucesso e pertença ao plano (ou seja, solicitação de exclusão), possui histórico escolar
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de serviço</param>
        /// <param name="desativarFiltroDados">Desativar filtro de dados ou não</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="seqsSolicitacaoMatriculaItem">Sequencial dos itens da solicitação de matrícula e suas situações selecionadas na tela</param>
        /// <returns>Lista de descrição dos componentes afetados</returns>
        public List<string> VerificarChancelaExclusaoTurmasComHistoricoEscolar(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, List<(long SeqItem, long SeqSituacaoItem)> seqsSolicitacaoMatriculaItem, bool desativarFiltroDados)
        {
            try
            {
                //Necessário remover o filtro global porque existe chancela de secretaria diferente ao do aluno no caso de disciplina eletiva
                if (desativarFiltroDados)
                    this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

                // Recupera os dados da chancela que está sendo feita
                var dadosChancela = this.SearchProjectionByKey(seqSolicitacaoMatricula, x => new
                {
                    TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                    TurmaPertence = x.Itens.Where(i => i.SeqDivisaoTurma.HasValue && i.PertencePlanoEstudo).Select(i => new
                    {
                        Seq = (long?)i.Seq,
                        SeqOrigemAvaliacao = (long?)i.DivisaoTurma.Turma.SeqOrigemAvaliacao,
                        //Codigo = i.DivisaoTurma.Turma.Codigo,
                        //Numero = i.DivisaoTurma.Turma.Numero
                    }).ToList(),

                    SeqAlunoHistorico = (long?)(x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(a => a.Atual).Seq,
                    SeqCicloLetivo = (long?)x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                    SituacoesItensChancela = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.Seq == seqConfiguracaoEtapa).ProcessoEtapa.SituacoesItemMatricula.OrderBy(s => s.Descricao).Select(i => new { SeqEtapa = i.ProcessoEtapa.SeqEtapaSgf, Descricao = i.Descricao, Seq = i.Seq, Classificacao = i.ClassificacaoSituacaoFinal, SituacaoInicial = i.SituacaoInicial }).ToList(),
                });

                if (dadosChancela.TipoAtuacao == TipoAtuacao.Aluno)
                {
                    // Sequencial da situação de sucesso da chancela
                    var situacaoFinalSucessoChancela = dadosChancela.SituacoesItensChancela.FirstOrDefault(s => s.Classificacao == ClassificacaoSituacaoFinal.FinalizadoComSucesso);

                    // Seleciona os seqs dos itens que estão com classificação de sucesso (deferido)
                    var seqsItensARemover = seqsSolicitacaoMatriculaItem.Where(s => s.SeqSituacaoItem == situacaoFinalSucessoChancela.Seq).Select(i => i.SeqItem);

                    // Verifica quais desses itens que estão com situação de deferimento pertencem ao plano
                    var turmasRemover = dadosChancela.TurmaPertence.Where(a => seqsItensARemover.Contains(a.Seq.GetValueOrDefault()));

                    // Caso esteja removendo algum item que pertence ao plano
                    if (turmasRemover != null && turmasRemover.Any())
                    {
                        // Caso tenha algum item a ser removido, verifica se existe lançamento de nota para o mesmo
                        var turmasExcluidasComHistorico = HistoricoEscolarDomainService.SearchProjectionBySpecification(new HistoricoEscolarFilterSpecification
                        {
                            SeqAlunoHistorico = dadosChancela.SeqAlunoHistorico,
                            SeqCicloLetivo = dadosChancela.SeqCicloLetivo,
                            SeqsOrigemAvaliacoes = turmasRemover.Select(d => d.SeqOrigemAvaliacao.GetValueOrDefault()).ToList(),
                            SituacoesHistoricoEscolar = new SituacaoHistoricoEscolar[] { SituacaoHistoricoEscolar.Aprovado, SituacaoHistoricoEscolar.Reprovado, SituacaoHistoricoEscolar.AlunoSemNota }
                        }, x => x.SeqOrigemAvaliacao).ToList();

                        // Caso tenha lançamento de nota, retorna a descrição do item
                        if (turmasExcluidasComHistorico != null && turmasExcluidasComHistorico.Any())
                        {
                            // Recupera todas as turmas e atividades para pegar a descrição correta
                            var turmasAtividades = SolicitacaoMatriculaItemDomainService.BuscarSolicitacaoMatriculaTurmasAtividades(seqSolicitacaoMatricula, seqConfiguracaoEtapa, false, false, desativarFiltroDados);
                            var turmasExcluidas = turmasAtividades.Turmas.Where(t => turmasRemover.Where(r => turmasExcluidasComHistorico.Contains(r.SeqOrigemAvaliacao)).Select(r => r.Seq).Contains(t.Seq)).Select(t => t.TurmaFormatado);
                            return turmasExcluidas.ToList();

                            // Retorna o código e número
                            //return turmasRemover.Where(r => turmasExcluidasComHistorico.Contains(r.SeqOrigemAvaliacao)).Select(t => t.Codigo + "." + t.Numero).ToList();
                        }
                    }
                }
                return null;
            }
            finally
            {
                //Necessário remover o filtro global porque existe chancela de secretaria diferente ao do aluno no caso de disciplina eletiva
                if (desativarFiltroDados)
                    this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
        }

        public List<SMCDatasourceItem> BuscarSituacoesItensChancela(long seqSolicitacao)
        {
            // Recupera a solicitação de matrícula pelo sequencial caso o orientador seja o próprio usuário
            var chancela = SearchProjectionByKey(new SolicitacaoMatriculaFilterSpecification { Seq = seqSolicitacao }, x => new
            {
                SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token == MatriculaTokens.CHANCELA_PLANO_ESTUDO).Seq,
                SeqTemplateProcessoSgf = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token == MatriculaTokens.CHANCELA_PLANO_ESTUDO).Seq).ConfiguracaoEtapa.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                TodasSituacoesItens = x.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(r => r.ProcessoEtapa.SituacoesItemMatricula.Where(s => s.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.Cancelado).OrderBy(s => s.Descricao).Select(i => new { SeqEtapa = r.ProcessoEtapa.SeqEtapaSgf, Descricao = i.Descricao, Seq = i.Seq, Classificacao = i.ClassificacaoSituacaoFinal, SituacaoInicial = i.SituacaoInicial })).ToList(),
            });

            // Verifica se a situação atual da solicitação está na etapa de chancela ou está com o sequencial da situação inicial da próxima etapa
            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(chancela.SeqTemplateProcessoSgf);
            var etapaChancela = etapasSGF.FirstOrDefault(e => e.Token == MatriculaTokens.CHANCELA_PLANO_ESTUDO);

            var situacoesItensChancela = chancela.TodasSituacoesItens.Where(a => a.SeqEtapa == etapaChancela.Seq).Select(a => new SMCDatasourceItem { Seq = a.Seq, Descricao = a.Descricao });

            return situacoesItensChancela.ToList();
        }

        /// <summary>
        /// Buscar as solicitações de matricula que precisam ser chancelada de acordo com o token do serviço
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de matricula</param>
        /// <param name="tokenEtapa">token definido do processo da etapa</param>
        /// <param name="orientacao">identifica se possui orientador</param>
        /// <param name="desativarFiltroDados">Desconsidera o filtro de dados</param>
        /// <returns></returns>
        public ChancelaVO BuscarSolicitacaoMatriculaChancela(long seq, string tokenEtapa, bool? orientacao, bool desabilitarFiltro = false)
        {
            //Necessário remover o filtro global porque existe chancela de secretaria diferente ao do aluno no caso de disciplina eletiva
            if (desabilitarFiltro)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var spec = new SolicitacaoMatriculaFilterSpecification();
            spec.Seq = seq;

            if (ColaboradorDomainService.ValidarColaboradorLogado() && orientacao.HasValue && orientacao.Value)
                spec.SeqColaborador = SMCContext.User.SMCGetSequencialUsuario();

            string tokensValidacao = tokenEtapa;
            bool exibirSelecaoTurmaAtividade = true;
            bool exibirEntidadeResponsavelTurma = false;
            bool exibirJustificativaSolicitacao = false;

            if (tokenEtapa == MatriculaTokens.CHANCELA_ALTERACAO_PLANO_ESTUDO
             || tokenEtapa == MatriculaTokens.CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA)
            {
                exibirSelecaoTurmaAtividade = false;
                exibirJustificativaSolicitacao = true;
            }

            if (tokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_ORIGEM
             || tokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO)
            {
                exibirSelecaoTurmaAtividade = false;
                exibirEntidadeResponsavelTurma = true;
                exibirJustificativaSolicitacao = true;
            }

            // Recupera a solicitação de matrícula pelo sequencial caso o orientador seja o próprio usuário
            var chancela = SearchProjectionByKey(spec, x => new
            {
                Seq = x.Seq,
                DescricaoJustificativa = x.JustificativaComplementar,
                // Filtros
                DataInicioChancela = x.SeqGrupoEscalonamento.HasValue ?
                                     x.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.Token == tokensValidacao).Escalonamento.DataInicio :
                                     x.ConfiguracaoProcesso.Processo.DataInicio,
                DataFimChancela = x.SeqGrupoEscalonamento.HasValue ?
                                  x.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.Token == tokensValidacao).Escalonamento.DataFim :
                                  x.ConfiguracaoProcesso.Processo.DataFim,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token == tokensValidacao).Seq,
                SeqTemplateProcessoSgf = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token == tokensValidacao).Seq).ConfiguracaoEtapa.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                SeqProcesso = x.ConfiguracaoProcesso.SeqProcesso,
                SeqCicloLetivo = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                SeqProcessoEtapa = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token == tokensValidacao).Seq).ConfiguracaoEtapa.SeqProcessoEtapa,
                SeqSolicitacaoServicoEtapa = x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.Token == tokensValidacao).Seq,
                //Dados
                BloqueiosInicio = x.PessoaAtuacao.Bloqueios.Where(b => x.ConfiguracaoProcesso.ConfiguracoesEtapa.Where(c => c.ProcessoEtapa.Token == tokensValidacao).SelectMany(cb => cb.ConfiguracoesBloqueio.Where(bb => bb.ImpedeInicioEtapa).Select(bb => bb.SeqMotivoBloqueio)).Contains(b.SeqMotivoBloqueio) && b.SituacaoBloqueio == SituacaoBloqueio.Bloqueado && b.DataBloqueio <= DateTime.Now).Select(b => b.MotivoBloqueio.Descricao),
                Nome = x.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                CPF = x.PessoaAtuacao.Pessoa.Cpf,
                Passaporte = x.PessoaAtuacao.Pessoa.NumeroPassaporte,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                SeqUnidadeResponsavel = x.EntidadeResponsavel.Seq,
                DescricaoUnidadeResponsavel = x.EntidadeResponsavel.Nome,
                // Bug 23963 Modificar a busca de Descrição para ingressante > ingressante_oferta > campanha_oferta
                DescricaoOfertaCampanha = (x.PessoaAtuacao as Ingressante).Ofertas.FirstOrDefault().CampanhaOferta.Descricao,
                SituacaoAtual = x.SituacaoAtual,
                SituacaoAnterior = x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.Token == tokensValidacao).HistoricosSituacao.OrderByDescending(e => e.Seq).FirstOrDefault(),
                TodasSituacoesItens = x.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(r => r.ProcessoEtapa.SituacoesItemMatricula.Where(s => s.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.Cancelado).OrderBy(s => s.Descricao).Select(i => new { SeqEtapa = r.ProcessoEtapa.SeqEtapaSgf, Descricao = i.Descricao, Seq = i.Seq, Classificacao = i.ClassificacaoSituacaoFinal, SituacaoInicial = i.SituacaoInicial, SituacaoFinal = i.SituacaoFinal })).ToList(),
                EtapaChancelaLiberada = x.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(r => r.ProcessoEtapa.SituacaoEtapa).FirstOrDefault() == SituacaoEtapa.Liberada,
                ChancelaReaberta = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token == tokensValidacao).Seq).HistoricosSituacao.Any(a => a.Observacao.Contains("Chancela reaberta")),
                DescricaoVinculo = (x.PessoaAtuacao as Aluno).Descricao,
                EtapaLiberada = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa == SituacaoEtapa.Liberada
            });


            if (!chancela.EtapaLiberada)
                throw new SMCApplicationException(SRC.Resources.MessagesResource.MSG_SituacaoEtapa);

            //Verifica se a situação atual da solicitação está na etapa de chancela ou está com o sequencial da situação inicial da próxima etapa
            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(chancela.SeqTemplateProcessoSgf);
            var etapaChancela = etapasSGF.FirstOrDefault(e => e.Token == tokensValidacao);
            var etapaAnterior = etapasSGF.OrderByDescending(e => e.Ordem).FirstOrDefault(e => e.Ordem < etapaChancela.Ordem);
            var proximaEtapa = etapasSGF.OrderBy(e => e.Ordem).FirstOrDefault(e => e.Ordem > etapaChancela.Ordem);
            var situacaoIntermediariaChancela = etapaChancela.Situacoes.FirstOrDefault(s => !s.SituacaoFinalEtapa && !s.SituacaoInicialEtapa && !s.SituacaoSolicitante && !s.SituacaoFinalProcesso);
            var situacaoInicialChancela = etapaChancela.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa && s.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.NaoAlterado);
            var situacaoFinalSucessoCancela = etapaChancela.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);
            var situacaoFinalSemSucessoCancela = etapaChancela.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso);
            var situacaoInicialProximaEtapa = proximaEtapa?.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa && s.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.NaoAlterado);

            var seqsSituacoesEtapaPossiveis = new List<long>();
            seqsSituacoesEtapaPossiveis.AddRange(etapaChancela.Situacoes.Where(s => !(s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado && s.SituacaoFinalEtapa)).Select(s => s.Seq));

            if (proximaEtapa != null)
                seqsSituacoesEtapaPossiveis.Add(proximaEtapa.Situacoes.Where(s => s.SituacaoInicialEtapa).Select(s => s.Seq).FirstOrDefault());

            // Não permite acessar caso a situação não seja uma permitida (qualquer uma da etapa de chancela ou a inicial da proxima etapa)
            if (!seqsSituacoesEtapaPossiveis.Contains(chancela.SituacaoAtual.SeqSituacaoEtapaSgf))
                throw new SMCApplicationException(MessagesResource.ERR_ChancelaEtapaInvalida);

            // Não permite entrar caso não esteja vigente
            if (chancela.DataInicioChancela > DateTime.Now || chancela.DataFimChancela < DateTime.Now)
                if (!ProcessoEtapaDomainService.ValidarDataEscalonamentoFinalProcesso(chancela.SeqProcessoEtapa))
                    throw new SMCApplicationException(MessagesResource.ERR_ChancelaPeriodoExpirou);

            if (chancela.BloqueiosInicio.Any())
                throw new ChancelaBloqueioEtapaException();

            // Recupera os itens de turmas e atividades a serem chanceladas
            var turmasAtividades = SolicitacaoMatriculaItemDomainService.BuscarSolicitacaoMatriculaTurmasAtividades(seq, chancela.SeqConfiguracaoEtapa, false, false, desabilitarFiltro);

            // Verifica todas as situações permitidas para os itens da matrícula
            var situacoesItensChancela = chancela.TodasSituacoesItens.Where(a => a.SeqEtapa == etapaChancela.Seq).Select(a => new SMCDatasourceItem { Seq = a.Seq, Descricao = a.Descricao });
            var situacaoInicioItemEtapaChancela = chancela.TodasSituacoesItens.Where(a => a.SeqEtapa == etapaChancela.Seq && a.SituacaoInicial && !a.SituacaoFinal).Select(a => new SMCDatasourceItem { Seq = a.Seq, Descricao = a.Descricao }).FirstOrDefault();
            var situacaoFinalSucessoItemEtapaChancela = chancela.TodasSituacoesItens.Where(a => a.SeqEtapa == etapaChancela.Seq && a.SituacaoFinal && a.Classificacao == ClassificacaoSituacaoFinal.FinalizadoComSucesso).Select(a => new SMCDatasourceItem { Seq = a.Seq, Descricao = a.Descricao }).FirstOrDefault();
            var situacaoFinalSemSucessoItemEtapaChancela = chancela.TodasSituacoesItens.Where(a => a.SeqEtapa == etapaChancela.Seq && a.SituacaoFinal && a.Classificacao == ClassificacaoSituacaoFinal.FinalizadoSemSucesso).Select(a => new SMCDatasourceItem { Seq = a.Seq, Descricao = a.Descricao }).FirstOrDefault();

            var seqsSituacoesItensPermitidos = new List<long>();
            seqsSituacoesItensPermitidos.AddRange(situacoesItensChancela.Select(a => a.Seq));

            SMCDatasourceItem situacaoInicialItemProximaEtapa = new SMCDatasourceItem();
            if (proximaEtapa != null)
            {
                situacaoInicialItemProximaEtapa = chancela.TodasSituacoesItens.Where(a => a.SeqEtapa == proximaEtapa.Seq && a.SituacaoInicial).Select(a => new SMCDatasourceItem { Seq = a.Seq, Descricao = a.Descricao }).FirstOrDefault();
                seqsSituacoesItensPermitidos.Add(situacaoInicialItemProximaEtapa.Seq);
            }

            // Cria o objeto de retorno para edição
            var ret = SMCMapperHelper.Create<ChancelaVO>(chancela);
            ret.ExibirSelecaoTurmaAtividade = exibirSelecaoTurmaAtividade;
            if (!string.IsNullOrWhiteSpace(chancela.NomeSocial))
                ret.Nome = $"{chancela.NomeSocial} ({chancela.Nome})";

            var instituicaoNiveltipoVinculoAluno = new InstituicaoNivelTipoVinculoAlunoVO();
            var tipoAtuacao = PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(chancela.SeqPessoaAtuacao);

            //Verificar se o vínculo da pessoa-atuação foi parametrizado para exigir oferta de matriz:
            instituicaoNiveltipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(chancela.SeqPessoaAtuacao, desabilitarFiltro);

            if (!(instituicaoNiveltipoVinculoAluno?.ExigeCurso.Value ?? true))
                ret.DescricaoOfertaCampanha = null;

            ret.SeqIngressante = chancela.SeqPessoaAtuacao;
            ret.SeqProcesso = chancela.SeqProcesso;
            ret.SeqCicloLetivo = chancela.SeqCicloLetivo;
            ret.SeqProcessoEtapa = chancela.SeqProcessoEtapa;
            ret.ExigirCurso = instituicaoNiveltipoVinculoAluno.ExigeCurso.GetValueOrDefault();
            ret.ExigirMatrizCurricularOferta = instituicaoNiveltipoVinculoAluno.ExigeOfertaMatrizCurricular.GetValueOrDefault();
            ret.LegendaPertencePlanoEstudo = turmasAtividades.Turmas.Any(a => a.PertencePlanoEstudo == true) || turmasAtividades.Atividades.Any(a => a.PertencePlanoEstudo == true);
            ret.SituacoesItens = chancela.TodasSituacoesItens.Where(a => a.SeqEtapa == etapaChancela.Seq && a.Classificacao != ClassificacaoSituacaoFinal.NaoAlterado).Select(a => new SMCDatasourceItem { Seq = a.Seq, Descricao = a.Descricao }).ToList();
            ret.SeqSituacaoFinalSucessoChancela = situacaoFinalSucessoItemEtapaChancela?.Seq;
            ret.SeqSituacaoInicioChancela = situacaoInicioItemEtapaChancela?.Seq;

            if (chancela.SituacaoAtual.SeqSituacaoEtapaSgf == situacaoInicialProximaEtapa?.Seq)
                ret.DescricaoSituacaoAtual = etapasSGF.SelectMany(e => e.Situacoes).FirstOrDefault(s => s.Seq == chancela.SituacaoAnterior.SeqSituacaoEtapaSgf).Descricao;
            else
                ret.DescricaoSituacaoAtual = etapasSGF.SelectMany(e => e.Situacoes).FirstOrDefault(s => s.Seq == chancela.SituacaoAtual.SeqSituacaoEtapaSgf).Descricao;

            // Preenche as turmas e atividades

            /* Listar todos os itens de matrícula associados à solicitação da pessoa-atuação:
             *  Com o tipo de gestão "Turma".
             *  Com a situação ATUAL em alguma situação configurada de acordo com a etapa, EXCETO a situação para ser a final com o valor da "Classificação Situação" igual a "Cancelado".
             *  Com a situação ATUAL em alguma situação configurada para as etapas posteriores. EXCETO a situação para ser a final com o valor da "Classificação Situação" igual a "Cancelado". */
            ret.Turmas = turmasAtividades.Turmas.Where(t => seqsSituacoesItensPermitidos.Any(s => t.TurmaMatriculaDivisoes.Any(d => d.SeqSituacaoItemMatricula.GetValueOrDefault() == s)))
                .Select(t => new ChancelaTurmaVO
                {
                    SeqTurma = t.SeqTurma ?? 0,
                    SeqSolicitacaoMatriculaItem = t.Seq,
                    TurmaFormatado = t.TurmaFormatado,
                    DescricaoTipoTurma = t.DescricaoTipoTurma,
                    Pertence = t.Pertence,
                    AssociacaoOfertaMatrizTipoTurma = t.AssociacaoOfertaMatrizTipoTurma.Value,
                    TurmaMatriculaDivisoes = t.TurmaMatriculaDivisoes.TransformList<ChancelaTurmaDivisoesVO>(),
                    LegendaPertencePlanoEstudo = ret.LegendaPertencePlanoEstudo,
                    ProgramaTurma = t.ProgramaTurma,
                    ExibirEntidadeResponsavelTurma = exibirEntidadeResponsavelTurma && chancela.SeqUnidadeResponsavel != t.SeqProgramaTurma,
                    SeqSituacaoFinalSemSucessoItemEtapaChancela = situacaoFinalSemSucessoItemEtapaChancela?.Seq,
                }).ToList();

            ret.Turmas.SMCForEach(f => f.TurmaMatriculaDivisoes.SMCForEach(fr =>
            {
                fr.SeqPessoaAtuacao = chancela.SeqPessoaAtuacao;
            }));

            /* Listar todos os itens de matrícula associados à solicitação da pessoa-atuação:
             *  Com o tipo de gestão diferente de "Turma".
             *  Com a situação ATUAL em alguma situação configurada de acordo com a etapa, EXCETO a situação para ser a final com o valor da "Classificação Situação" igual a "Cancelado".
             *  Com a situação ATUAL em alguma situação configurada para as etapas posteriores. EXCETO a situação para ser· a final com o valor da "Classificação Situação" igual a "Cancelado". */
            ret.Atividades = turmasAtividades.Atividades.Where(t => seqsSituacoesItensPermitidos.Any(s => s == t.SeqSituacao)).Select(a => new ChancelaAtividadeVO
            {
                Descricao = a.DescricaoConfiguracaoComponente,
                SeqItem = a.Seq,
                SeqSituacaoItemMatricula = a.HistoricoSeqSituacao.FirstOrDefault(h => seqsSituacoesItensPermitidos.Contains(h) && situacaoInicialItemProximaEtapa?.Seq != h),
                Pertence = TurmaOfertaMatricula.ComponentePertence,
                SituacaoPlanoEstudo = a.SituacaoPlanoEstudo,
                LegendaPertencePlanoEstudo = a.PertencePlanoEstudo.GetValueOrDefault(),
            }).ToList();

            // Define se a etapa esta finalizada
            ret.ChancelaFinalizada = false;

            // De acordo com a nova regra, ao entrar na tela de chancela, deve registrar a situação intermediária da etapa.
            // No caso, se a situação da solicitação for a inicial da etapa de chancela, define como intermediária
            if (chancela.SituacaoAtual.SeqSituacaoEtapaSgf == situacaoInicialChancela.Seq)
            {
                SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(chancela.SeqSolicitacaoServicoEtapa, situacaoIntermediariaChancela.Seq);
            }
            else
            {
                ret.ChancelaFinalizada = chancela.SituacaoAtual.SeqSituacaoEtapaSgf == situacaoFinalSucessoCancela.Seq ||
                                            chancela.SituacaoAtual.SeqSituacaoEtapaSgf == situacaoFinalSemSucessoCancela?.Seq ||
                                            chancela.SituacaoAtual.SeqSituacaoEtapaSgf == situacaoInicialProximaEtapa?.Seq;
            }

            ret.ChancelaReaberta = !ret.ChancelaFinalizada && chancela.ChancelaReaberta;
            ret.ExibirJustificativa = exibirJustificativaSolicitacao;

            //Qunado for chancela de destino não exibir os itens não alterados da chancela de origem
            if (tokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO)
            {
                ret.Turmas = ret.Turmas.Where(w => w.TurmaMatriculaDivisoes.Any(t => t.SituacaoPlanoEstudo != MatriculaPertencePlanoEstudo.NaoAlterado)).ToList();
                ret.Atividades = ret.Atividades.Where(w => w.SituacaoPlanoEstudo != MatriculaPertencePlanoEstudo.NaoAlterado).ToList();
            }

            //NV16
            // Exibir agrupamento quando o aluno:
            // * Estiver na situação MATRICULADO_MOBILIDADE. Exibir dados de intercâmbio referentes aos períodos de intercâmbio da situação em questão.
            // * Possui um tipo de termo que não concede formação e estiver na situação MATRICULADO.
            if (tipoAtuacao == TipoAtuacao.Aluno)
            {
                // Busca a situação atual do aluno
                var situacaoAtualAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(chancela.SeqPessoaAtuacao, desabilitarFiltro);

                // Se o aluno estiver em mobilidade, mostra os dados do intercâmbio
                if (situacaoAtualAluno.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE)
                {
                    ret.ExibirVisualizacaoDadosIntercambio = true;
                    ret.SeqCicloLetivoSituacao = situacaoAtualAluno.SeqAlunoHistoricoSituacao.GetValueOrDefault();
                    ret.SeqPeriodoIntercambio = situacaoAtualAluno.SeqPeriodoIntercambio.GetValueOrDefault();
                }
                // Observação Carol - Em conversa com a Raphaela foi informado que deve apresentar os dados do intercâmbio
                // caso o aluno esteja MATRICULADO e tenha uma situação futura com intercâmbio associado. 
                else if (situacaoAtualAluno.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO)
                {
                    var specSit = new AlunoHistoricoSituacaoFilterSpecification()
                    {
                        SeqPessoaAtuacaoAluno = chancela.SeqPessoaAtuacao,
                        SituacaoFutura = true
                    };
                    var sitFutura = AlunoHistoricoSituacaoDomainService.SearchProjectionBySpecification(specSit, s => new
                    {
                        Seq = s.Seq,
                        SeqPeriodoIntercambio = s.SeqPeriodoIntercambio,
                        SeqTipoTermoIntercambio = (long?)s.PeriodoIntercambio.PessoaAtuacaoTermoIntercambio.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio
                    });
                    foreach (var item in sitFutura)
                    {
                        if ((item.SeqPeriodoIntercambio.HasValue) &&
                           (!TipoTermoIntercambioDomainService.TipoTermoIntercambioConcedeFormacao(item.SeqTipoTermoIntercambio.Value, instituicaoNiveltipoVinculoAluno.Seq)))
                        {
                            ret.ExibirVisualizacaoDadosIntercambio = true;
                            ret.SeqCicloLetivoSituacao = item.Seq;
                            ret.SeqPeriodoIntercambio = item.SeqPeriodoIntercambio.GetValueOrDefault();
                            break;
                        }
                    }
                }
            }

            // Carrega as formações específicas para o cabeçalho
            var formacoesEspecificas = FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(chancela.SeqPessoaAtuacao, desabilitarFiltro).Select(x => x.DescricaoFormacaoEspecifica).ToList();
            ret.FormacoesEspecificas = formacoesEspecificas;

            //Necessário remover o filtro global porque existe chancela de secretaria diferente ao do aluno no caso de disciplina eletiva
            if (desabilitarFiltro)
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return ret;
        }

        /// <summary>
        /// Busca todas as solicitações que estão em grupos de escalonamentos ativos, com alguma situação da etapa de chancela ou a situação inicial da etapa posterior.
        /// </summary>
        /// <param name="spec">Filtro</param>
        /// <returns></returns>
        public SMCPagerData<ChancelaItemListaVO> BuscarChancelas(ChancelaFilterSpecification spec)
        {
            var seqUsuarioLogado = SMCContext.User.SMCGetSequencialUsuario();

            // Recupera os templates de processo
            var seqsTemplatesProcesso = RawQuery<long>(_buscarChancelasOrientadorTemplatesProcesso,
                new SMCFuncParameter("@SEQ_USUARIO_SAS", seqUsuarioLogado),
                new SMCFuncParameter("@SEQ_PROCESSO", spec.SeqProcesso ?? 0),
                new SMCFuncParameter("@IND_ATIVO", spec.ApenasProcessoVigente),
                new SMCFuncParameter("@IND_CHANCELA", spec.ApenasAguardandoChancela));

            var seqsSituacoesPossiveis = new List<long>();
            var seqsSituacoesDestaque = new List<long>();
            var dadosEtapas = new Dictionary<long, EtapaSimplificadaData[]>();
            var seqsEtapasChancela = new List<long>();

            if (seqsTemplatesProcesso != null)
            {
                // Busca as configurações das etapas dos templates de processo
                foreach (var seqTemplateprocessoSGF in seqsTemplatesProcesso.Distinct())
                {
                    var etapaSGF = SGFHelper.BuscarEtapasSGFCache(seqTemplateprocessoSGF);
                    dadosEtapas.Add(seqTemplateprocessoSGF, etapaSGF);

                    // Recupera a etapa de chancela
                    var etapaChancela = etapaSGF.FirstOrDefault(e => e.Token.Contains(MatriculaTokens.CHANCELA));

                    if (etapaChancela != null)
                    {
                        // Adiciona o sequencial da etapa de chancela
                        seqsEtapasChancela.Add(etapaChancela.Seq);

                        // Recupera a próxima etapa
                        var proximaEtapa = etapaSGF.FirstOrDefault(e => e.Ordem > etapaChancela.Ordem);

                        // Recupera os sequenciais possíveis
                        seqsSituacoesPossiveis.AddRange(etapaChancela.Situacoes.Where(s => !(s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado)).Select(s => s.Seq));
                        if (proximaEtapa != null)
                            seqsSituacoesPossiveis.Add(proximaEtapa.Situacoes.First(s => s.SituacaoInicialEtapa).Seq);

                        //Recupera os sequenciais para destaque da linha
                        seqsSituacoesDestaque.AddRange(etapaChancela.Situacoes.Where(s => !s.SituacaoFinalEtapa).Select(s => s.Seq));
                    }
                }
                seqsSituacoesPossiveis = seqsSituacoesPossiveis.Distinct().ToList();
                seqsSituacoesDestaque = seqsSituacoesDestaque.Distinct().ToList();
            }

            var itens = RawQuery<ChancelaProjectionVO>(_buscaChancelasOrientador,
                new SMCFuncParameter("@SEQ_USUARIO_SAS", seqUsuarioLogado),
                new SMCFuncParameter("@SEQ_PROCESSO", spec.SeqProcesso ?? 0),
                new SMCFuncParameter("@IND_ATIVO", spec.ApenasProcessoVigente),
                new SMCFuncParameter("@PAGINA", spec.PageNumber),
                new SMCFuncParameter("@PORPAGINA", spec.MaxResults),
                new SMCFuncParameter("@IND_CHANCELA", spec.ApenasAguardandoChancela),
                new SMCFuncParameter("@SEQS_SITUACOES", spec.ApenasAguardandoChancela ? seqsSituacoesDestaque : seqsSituacoesPossiveis));

            var total = RawQuery<int>(_buscaChancelasOrientadorTotal,
                new SMCFuncParameter("@SEQ_USUARIO_SAS", seqUsuarioLogado),
                new SMCFuncParameter("@SEQ_PROCESSO", spec.SeqProcesso ?? 0),
                new SMCFuncParameter("@IND_ATIVO", spec.ApenasProcessoVigente),
                new SMCFuncParameter("@IND_CHANCELA", spec.ApenasAguardandoChancela),
                new SMCFuncParameter("@SEQS_SITUACOES", spec.ApenasAguardandoChancela ? seqsSituacoesDestaque : seqsSituacoesPossiveis));

            if (itens != null)
            {
                var ret = itens.Select(i => new ChancelaItemListaVO
                {
                    Bloqueado = i.Bloqueios != null && i.Bloqueios.Count > 0,
                    Bloqueios = i.Bloqueios,
                    DataEscalonamento = i.PeriodoChancela,
                    DescricaoOfertaCurso = i.DescricaoOfertaCurso,
                    DescricaoProcesso = i.DescricaoProcesso,
                    DescricaoSituacao = dadosEtapas.SelectMany(d => d.Value).SelectMany(d => d.Situacoes).FirstOrDefault(s => s.Seq == i.SeqSituacaoEtapaSGF).Descricao,
                    Destaque = seqsSituacoesDestaque.Contains(i.SeqSituacaoEtapaSGF),
                    EscalonamentoVigente = i.ChancelaVigente,
                    EtapaChancelaLiberada = i.EtapaChancelaLiberada,
                    EtapaPermiteChancela = dadosEtapas.SelectMany(d => d.Value).SelectMany(d => d.Situacoes).FirstOrDefault(s => s.Seq == i.SeqSituacaoEtapaSGF).CategoriaSituacao != CategoriaSituacao.Encerrado
                                            && (i.ProcessoEtapaFiltroOrientador || i.TokenEtapaSGF.Contains(MatriculaTokens.EFETIVACAO))
                                            && seqsSituacoesPossiveis.Contains(i.SeqSituacaoEtapaSGF),
                    NomePessoaAtuacao = string.IsNullOrEmpty(i.NomeSocialSolicitante) ? i.NomeSolicitante : $"{i.NomeSocialSolicitante} ({i.NomeSolicitante})",
                    NumeroProtocolo = i.NumeroProtocolo,
                    PermiteVisualizarPlanoEstudo = i.PermiteVisualizarPlanoEstudo,
                    Seq = i.Seq,
                    SeqConfiguracaoEtapa = i.SeqConfiguracaoEtapa,
                    SeqPessoaAtuacao = i.SeqPessoaAtuacao,
                    SeqProcesso = i.SeqProcesso,
                    TokenEtapaSGF = i.TokenEtapaSGF,

                    //Mensagem de usuário responsável pelo atendimento
                    SolicitacaoComAtendimentoIniciado = i.SeqUsuarioResponsavelAtendimentoSas.HasValue,
                    UsuarioLogadoEResponsavelAtualPelaSolicitacao = i.SeqUsuarioResponsavelAtendimentoSas == seqUsuarioLogado,

                    //Exibir o botão de integralização curricular
                    ExibirIntegralizacao = HistoricoEscolarDomainService.VisualizarConsultaConcederFormacaoIntegralizacao(i.SeqPessoaAtuacao),
                }).ToList();

                return new SMCPagerData<ChancelaItemListaVO>(ret, total[0]);
            }
            return null;
        }

        /// <summary>
        /// Busca todos os processos das solicitações que estão em grupos de escalonamentos ativos,
        /// com alguma situação da etapa de chancela ou a situação inicial da etapa posterior para filtro da chancela do orientador
        /// </summary>
        /// <param name="apenasDataVigente">Filtro por data vigente no grupo de escalonamento do processo</param>
        /// <returns>Lista de processos para utilizar no filtro da chancela</returns>
        public List<SMCDatasourceItem> BuscarProcessosFiltroChancela(bool apenasProcessoVigente)
        {
            return this.RawQuery<SMCDatasourceItem>(_buscarProcessosOrientador,
                        new SMCFuncParameter("@SEQ_USUARIO_SAS", SMCContext.User.SMCGetSequencialUsuario()),
                        new SMCFuncParameter("@IND_ATIVO", apenasProcessoVigente));
        }

        /// <summary>
        /// Realiza o processo de reabrir a chancela voltando as situações dos itens no histórico
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de serviço</param>
        /// <returns>Sequencial da configuração etapa de chancela que reabriu</returns>
        public long ReabrirChancelaMatricula(long seq)
        {
            // FIX: Modificar para usar o método de voltar para etapa anterior SolicitacaoServicoDomainService.RetornarSolicitacaoParaEtapaAnterior
            // Recupera a solicitação de matrícula pelo sequencial caso o orientador seja o próprio usuário
            var chancela = SearchProjectionByKey(new SolicitacaoMatriculaFilterSpecification { Seq = seq }, x => new
            {
                Seq = x.Seq,
                SeqSolicitacaoServicoEtapa = x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.Token.Contains(MatriculaTokens.CHANCELA)).Seq,

                // Filtros
                DataInicioChancela = x.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.Token.Contains(MatriculaTokens.CHANCELA)).Escalonamento.DataInicio,
                DataFimChancela = x.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.Token.Contains(MatriculaTokens.CHANCELA)).Escalonamento.DataFim,
                SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token.Contains(MatriculaTokens.CHANCELA)).Seq,
                SeqTemplateProcessoSgf = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token.Contains(MatriculaTokens.CHANCELA)).Seq).ConfiguracaoEtapa.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                SeqProcesso = x.ConfiguracaoProcesso.SeqProcesso,
                SeqProcessoEtapa = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token.Contains(MatriculaTokens.CHANCELA)).Seq).ConfiguracaoEtapa.SeqProcessoEtapa,
                SituacaoAtual = x.SituacaoAtual,
                TodasSituacoesItens = x.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(r => r.ProcessoEtapa.SituacoesItemMatricula.Where(s => s.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.Cancelado).OrderBy(s => s.Descricao).Select(i => new { SeqEtapa = r.ProcessoEtapa.SeqEtapaSgf, Descricao = i.Descricao, Seq = i.Seq, Classificacao = i.ClassificacaoSituacaoFinal, SituacaoInicial = i.SituacaoInicial })).ToList(),
                Itens = x.Itens.Select(i => new { Item = i, Historico = i.HistoricosSituacao }),
                EtapaChancelaLiberada = x.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.Token.Contains(MatriculaTokens.CHANCELA)).Escalonamento.ProcessoEtapa.SituacaoEtapa == SituacaoEtapa.Liberada
            });

            if (!chancela.EtapaChancelaLiberada)
                throw new SMCApplicationException(SRC.Resources.MessagesResource.MSG_SituacaoEtapa);

            // Verifica se a situação atual da solicitação está na etapa de chancela ou está com o sequencial da situação inicial da próxima etapa
            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(chancela.SeqTemplateProcessoSgf);
            var etapaChancela = etapasSGF.FirstOrDefault(e => e.Token.Contains(MatriculaTokens.CHANCELA));
            var proximaEtapa = etapasSGF.OrderBy(e => e.Ordem).FirstOrDefault(e => e.Ordem > etapaChancela.Ordem);
            var situacaoFinalSucessoCancela = etapaChancela.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);
            var situacaoFinalSemSucessoCancela = etapaChancela.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso);
            var situacaoInicialProximaEtapa = proximaEtapa.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa);
            var situacaoIntermediariaChancela = etapaChancela.Situacoes.FirstOrDefault(s => !s.SituacaoFinalEtapa && !s.SituacaoInicialEtapa && !s.SituacaoSolicitante && !s.SituacaoFinalProcesso);

            if (chancela.SituacaoAtual.SeqSituacaoEtapaSgf != situacaoFinalSucessoCancela?.Seq &&
                chancela.SituacaoAtual.SeqSituacaoEtapaSgf != situacaoFinalSemSucessoCancela?.Seq &&
                chancela.SituacaoAtual.SeqSituacaoEtapaSgf != situacaoInicialProximaEtapa?.Seq)
                throw new SMCApplicationException($"A situação atual da solicitação não permite que a chancela seja reaberta.");

            if (chancela.DataInicioChancela > DateTime.Now || chancela.DataFimChancela < DateTime.Now)
                throw new SMCApplicationException("Período de chancela da solicitação não está vigente.");

            if (chancela.SituacaoAtual.SeqSituacaoEtapaSgf == situacaoInicialProximaEtapa.Seq)
            {
                chancela.SituacaoAtual.DataExclusao = DateTime.Now;

                // atualiza com a data de exclusão
                SolicitacaoHistoricoSituacaoDomainService.UpdateFields(chancela.SituacaoAtual, x => x.DataExclusao);
            }

            /* Ao acionar a funcionalidade “Reabrir Chancela”, as seguintes ações deverão ser realizadas:
                Incluir a situação intermediária da etapa no histórico de situações da solicitação de matrícula;*/
            SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(chancela.SeqSolicitacaoServicoEtapa, situacaoIntermediariaChancela.Seq, "Chancela reaberta");

            return chancela.SeqConfiguracaoEtapa;
        }

        /// <summary>
        /// Chancela a solicitação de serviço e dependendo a etapa cria o plano de estudo
        /// </summary>
        /// <param name="chancela">Objeto para ser chancelado</param>
        /// <param name="tokenEtapa">token definido do processo da etapa</param>
        /// <param name="desabilitarFiltro">Desabilita o filtro de HIERARQUIA_ENTIDADE_ORGANIZACIONAL</param>
        public void SalvarChancelaMatricula(ChancelaVO chancela, string token, bool desabilitarFiltro = false)
        {
            try
            {
                // Verifica se deve desabilitar/habilitar o filtro de dados
                if (desabilitarFiltro)
                {
                    this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

                    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    SolicitacaoHistoricoSituacaoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    SolicitacaoMatriculaItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    SolicitacaoServicoEtapaDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    PlanoEstudoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    MensagemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    AlunoHistoricoCicloLetivoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                }

                // Busca os dados da solicitação para chancela
                var spec = new SolicitacaoMatriculaFilterSpecification { Seq = chancela.Seq };

                #region [ Dados da Chancela ]

                var dadosChancela = SearchProjectionByKey(spec, x => new
                {
                    // Dados da solicitação
                    SeqSolicitacaoServico = x.Seq,
                    SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                    TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                    SeqCicloLetivo = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,

                    // Dados do aluno
                    SeqAlunoHistoricoCicloLetivo = (long?)(x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.OrderByDescending(o => o.DataInclusao).FirstOrDefault(f => f.SeqCicloLetivo == x.ConfiguracaoProcesso.Processo.SeqCicloLetivo).Seq,

                    // Dados do serviço/processo/etapa
                    SeqTemplateProcessoSgf = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                    ControleVagaEtapa = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.ControleVaga,
                    ControleVagaEtapaAnterior = x.Etapas.Where(w => w.ConfiguracaoEtapa.ProcessoEtapa.Ordem < x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.Token == token).ConfiguracaoEtapa.ProcessoEtapa.Ordem)
                                        .Any(s => s.ConfiguracaoEtapa.ProcessoEtapa.ControleVaga),
                    SeqSolicitacaoServicoEtapa = x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.Token == token).Seq,
                    SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.ProcessoEtapa.Token == token).Seq,
                    SeqsEtapas = x.Etapas.Select(s => new
                    {
                        SeqEtapaSGF = s.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                        SeqSolicitacaoServicoEtapa = s.Seq
                    }),
                    ConfiguracoesEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(c => new
                    {
                        Token = c.ProcessoEtapa.Token,
                        Seq = c.Seq
                    }),

                    // Dados de situação da solicitação
                    SituacaoAtualChancela = x.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.Token == token).SituacaoAtual,

                    // Dados de situação dos itens da solicitação
                    TodasSituacoesItens = x.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(r => r.ProcessoEtapa.SituacoesItemMatricula.Select(a => new
                    {
                        SeqEtapaSGF = r.ProcessoEtapa.SeqEtapaSgf,
                        Situacao = a
                    })),

                    SituacoesItensSemEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.SelectMany(r => r.ProcessoEtapa.SituacoesItemMatricula).ToList(),

                    UltimasSituacoesItens = x.Itens.Select(i => new
                    {
                        SeqItem = i.Seq,
                        UltimaSituacao = i.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault(),
                        SituacaoItemMatricula = i.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoItemMatricula,
                        Historicos = i.HistoricosSituacao.OrderByDescending(h => h.Seq).Select(h => h.SeqSituacaoItemMatricula),
                        i.SeqDivisaoTurma,
                        i.SeqConfiguracaoComponente,
                        i.PertencePlanoEstudo,
                    }),


                    // Bloqueios
                    Bloqueios = x.PessoaAtuacao.Bloqueios.Where(b => x.ConfiguracaoProcesso.ConfiguracoesEtapa.Where(c => c.ProcessoEtapa.Token == token).SelectMany(cb => cb.ConfiguracoesBloqueio.Where(bb => bb.ImpedeInicioEtapa || bb.ImpedeFimEtapa).Select(bb => bb.SeqMotivoBloqueio)).Contains(b.SeqMotivoBloqueio) && b.SituacaoBloqueio == SituacaoBloqueio.Bloqueado && b.DataBloqueio <= DateTime.Now).Select(b => b.MotivoBloqueio.Descricao),
                });

                #endregion

                // Carrega os tipos de vínculo para verificar criação de bloqueios
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosChancela.SeqPessoaAtuacao);
                var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(dadosOrigem.SeqInstituicaoEnsino, dadosOrigem.SeqNivelEnsino, dadosOrigem.SeqTipoVinculoAluno, IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio);

                // Verifica se a situação atual da solicitação está na etapa de chancela ou está com o sequencial da situação inicial da próxima etapa
                var etapasSGF = SGFHelper.BuscarEtapasSGFCache(dadosChancela.SeqTemplateProcessoSgf);
                var etapaChancela = etapasSGF.FirstOrDefault(e => e.Token == token);
                var proximaEtapa = etapasSGF.OrderBy(e => e.Ordem).FirstOrDefault(e => e.Ordem > etapaChancela.Ordem);

                // Recupera a situação final da etapa de chancela
                var situacaoFinalSucessoChancela = etapaChancela.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && !s.SituacaoFinalProcesso && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);
                var situacaoFinalProcessoSucessoChancela = etapaChancela.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.SituacaoFinalProcesso && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);
                var situacaoAtualSGF = etapaChancela.Situacoes.FirstOrDefault(s => s.Seq == dadosChancela.SituacaoAtualChancela.SeqSituacaoEtapaSgf);

                if (situacaoAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso ||
                    situacaoAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso ||
                    situacaoAtualSGF.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado)
                {
                    throw new SituacaoAtualSolicitacaoNaoPermiteChancelaException();
                }

                // Recupera as situações dos itens inicial e final da chancela
                var situacaoInicialItemChancela = dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == etapaChancela.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.SituacaoInicial && s.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.NaoAlterado);
                var situacaoFinalItemChancela = dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == etapaChancela.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.SituacaoFinal && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);
                var situacaoFinalItemChancelaSemSucesso = dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == etapaChancela.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.SituacaoFinal && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso);
                var situacaoNaoAlteradoItemChancela = dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == etapaChancela.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.SituacaoFinal && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado) ?? new SituacaoItemMatricula();

                // Recupera as situações dos itens na próxima etapa
                var situacaoInicialItemProximaEtapa = proximaEtapa == null ?
                                                      dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == etapaChancela.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.SituacaoInicial) :
                                                      dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == proximaEtapa.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.SituacaoInicial);
                var situacaoFinalItemChancelaCancelado = proximaEtapa == null ?
                                                         dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == etapaChancela.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.SituacaoFinal && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado) :
                                                         dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == proximaEtapa.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.SituacaoFinal && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado);
                var situacaoNaoAlteradoItemProximaEtapa = proximaEtapa == null ?
                                                      dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == etapaChancela.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado) :
                                                      dadosChancela.TodasSituacoesItens.Where(s => s.SeqEtapaSGF == proximaEtapa.Seq).Select(a => a.Situacao).FirstOrDefault(s => s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado);

                // NV14
                // Verificar se a turma possui histórico. Caso positivo, não permite exclusão
                /*Verificar se o item pertence ao plano de estudos e se está com o valor "Deferido".
                    1.1. Caso estiver e for uma turma, verificar se existe registro no histórico escolar do aluno para a origem avaliação da turma que está sendo excluída, com situação "Aprovado", 'Reprovado" ou "Aluno sem nota". Caso existir, abortar a operação e exibir a seguinte mensagem de erro:
                        "Não é possível excluir esta turma. Ela já possui lançamento de nota no histórico escolar."
                        1.1.1. Se não existir lançamento para turma no histórico ou existir lançamento com uma situação diferente das citadas anteriormente, prosseguir com o deferimento do item.*/
                if (chancela.Turmas != null && chancela.Turmas.Any())
                {
                    var exclusaoTurmasComNota = VerificarChancelaExclusaoTurmasComHistoricoEscolar(chancela.Seq, dadosChancela.SeqConfiguracaoEtapa, chancela.Turmas.SelectMany(t => t.TurmaMatriculaDivisoes).Select(t => (t.Seq ?? 0, t.SeqSituacaoItemMatricula)).ToList(), desabilitarFiltro);
                    if (exclusaoTurmasComNota != null && exclusaoTurmasComNota.Any())
                        throw new ChancelaExclusaoTurmaComHistoricoLancadoException(exclusaoTurmasComNota);
                }

                // Regras RN_MAT_053 - Chancela plano de estudo
                /// V1. Verificar se existe pelo menos um item em uma situação inicial de acordo com a regra:
                /// Verificar se pelo menos um dos itens de matrícula da solicitação da pessoa-atuação está com uma
                /// situação de item configurada de acordo com a etapa, para ser a inicial e não ser a final. Caso
                /// ocorra, abortar a operação e exibir mensagem de erro: "Chancela não permitida. Existe item na
                /// situação <situação inicial do item na etapa>".
                if ((chancela.Turmas != null && chancela.Turmas.SelectMany(t => t.TurmaMatriculaDivisoes).SMCAny(t => t.SeqSituacaoItemMatricula == situacaoInicialItemChancela.Seq)) ||
                    chancela.Atividades.SMCAny(t => t.SeqSituacaoItemMatricula == situacaoInicialItemChancela.Seq))
                {
                    throw new ChancelaItemSituacaoInicialException(situacaoInicialItemChancela.Descricao);
                }

                /// V2. Verificar se existe pelo menos um item deferido, de acordo com a regra:
                /// Se NENHUM item da solicitação  possuir o valor do campo "Pertence ao plano de estudo" igual a "Sim":
                /// 2.1. Verificar se pelo menos um dos itens de matrícula da solicitação da pessoa-atuação está com uma
                /// situação de item configurada de acordo com a etapa, para ser a final e com o valor da "Classificação
                /// Situação" igual a "Finalizado com sucesso". Caso não ocorra, abortar  exibir mensagem de erro: "Chancela
                /// não permitida. Pelo menos um item deve estar na situação <situação final e classificação finalizado
                /// com sucesso do item na etapa>".
                if (!chancela.Turmas.SMCAny(t => t.LegendaPertencePlanoEstudo == true || t.TurmaMatriculaDivisoes.Any(d => d.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq || d.SeqSituacaoItemMatricula == situacaoNaoAlteradoItemChancela.Seq)) &&
                    !chancela.Atividades.SMCAny(t => t.LegendaPertencePlanoEstudo == true || t.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq || t.SeqSituacaoItemMatricula == situacaoNaoAlteradoItemChancela.Seq))
                {
                    throw new ChancelaNenhumItemSituacaoFinalException(situacaoFinalItemChancela.Descricao);
                }

                /// 3. Verificar se a mesma situação esta sendo selecionada para as divisões de uma turma, de acordo com a regra:
                ///    - Se todas as divisões de uma turma, que estão sendo exibidas na tela, possuírem o campo "Pertence ao plano de estudo" igual a "Não". 
                ///    OU
                ///    - Se todas as divisões de uma turma, que estão sendo exibidas na tela, possuírem o campo ”Pertence ao plano de estudo” igual a “SIM”. 
                ///
                ///    Verificar, se a situação de item selecionada é a mesma para as divisões de uma turma. Caso NÃO seja, abortar a operação e exibir a seguinte mensagem de erro: 
                ///    Mensagem:
                ///    “Chancela não permitida. As divisões da turma abaixo devem conter a mesma situação."
                ///    - <Turma A>
                if (chancela.Turmas?.Any() ?? false)
                {
                    string retornoTurma = string.Empty;
                    foreach (var itemTurma in chancela.Turmas)
                    {

                        if (itemTurma.TurmaMatriculaDivisoes != null &&
                             (itemTurma.TurmaMatriculaDivisoes.All(t => t.PertencePlanoEstudo) ||
                              itemTurma.TurmaMatriculaDivisoes.All(t => !t.PertencePlanoEstudo)))
                        {
                            if (itemTurma.TurmaMatriculaDivisoes.Where(i => i.SeqSituacaoItemMatricula != situacaoFinalItemChancelaCancelado.Seq).GroupBy(i => i.SeqSituacaoItemMatricula).Count() > 1)
                            {
                                retornoTurma += "<br /> As divisões da turma abaixo devem conter a mesma situação: <br />";
                                retornoTurma += "- " + itemTurma.TurmaFormatado + "<br />";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(retornoTurma))
                    {
                        throw new ChancelaDivisoesSituacaoDiferenteException(retornoTurma);
                    }
                }

                /// V4. Somente para aluno, verificar se as turmas foram concluídas/dispensadas de acordo com a regra abaixo:
                /// * Somente se a pessoa-atuação for do tipo de atuação “Aluno” E possuir um vínculo ou tipo de termo de intercâmbio parametrizado por
                /// instituição -nível de ensino-vínculo para conceder formação de acordo com a instituição de ensino logada, o nível de ensino e o vínculo do aluno.
                /// 4.1. Verificar se a pessoa-atuação em questão contém em seu histórico escolar, para o curso oferta localidade, a conclusão de algum item da solicitação em
                ///  um ciclo letivo diferente do ciclo letivo do processo em alguma situação abaixo:
                /// 4.1.1 Pertence ao plano de estudo e a situação selecionada é a situação configurada para ser final com a classificação “Finalizada sem sucesso”
                /// 4.1.2 NÃO pertence ao plano de estudo e a situação selecionada é a situação configurada para ser final com a classificação “Finalizada com sucesso”
                /// Caso tenha, abortar a operação e exibir a seguinte mensagem de erro:
                /// "Seleção não permitida. As turmas abaixo já foram concluídas ou dispensadas."
                /// - <Turma A>
                /// - <Turma B>
                if (dadosChancela.TipoAtuacao == TipoAtuacao.Aluno)
                {
                    if ((instituicaoNivelTipoVinculoAluno.ConcedeFormacao) ||
                        (instituicaoNivelTipoVinculoAluno?.TiposTermoIntercambio.Any(t => t.ConcedeFormacao) ?? false))
                    {
                        List<long> seqsItensValidar = new List<long>();

                        if (chancela.Turmas != null)
                            foreach (var item in chancela.Turmas.SelectMany(t => t.TurmaMatriculaDivisoes))
                            {
                                var itemBanco = dadosChancela.UltimasSituacoesItens.FirstOrDefault(i => i.SeqItem == item.Seq);
                                if (item.SeqSituacaoItemMatricula == situacaoFinalItemChancelaSemSucesso.Seq && itemBanco.PertencePlanoEstudo)
                                    seqsItensValidar.Add(item.Seq.GetValueOrDefault());
                                else if (item.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq && !itemBanco.PertencePlanoEstudo)
                                    seqsItensValidar.Add(item.Seq.GetValueOrDefault());
                            }

                        // Agrupa os itens para validação
                        var seqsComponentesSelecionados = dadosChancela.UltimasSituacoesItens.Where(i => seqsItensValidar.Contains(i.SeqItem)).Select(i => (i.SeqConfiguracaoComponente, i.SeqDivisaoTurma)).ToList();

                        //Valida se ja foi aprovado ou dispensado na turma 
                        var validarComponentesCursados = HistoricoEscolarDomainService.VerificarHistoricoComponentesAprovadosDispensados(dadosChancela.SeqPessoaAtuacao, seqsComponentesSelecionados, dadosChancela.SeqCicloLetivo);
                        if (!string.IsNullOrEmpty(validarComponentesCursados))
                        {
                            throw new TurmaJaAprovadaDispensadaException(validarComponentesCursados);
                        }

                        if ((token == MatriculaTokens.CHANCELA_ALTERACAO_PLANO_ESTUDO ||
                             token == MatriculaTokens.CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA ||
                             token == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO)
                             && chancela.NaoAtualizarPlano == false)
                        {
                            var alunoHistoricoCicloLetivo = this.AlunoHistoricoCicloLetivoDomainService.SearchByKey(new SMCSeqSpecification<AlunoHistoricoCicloLetivo>(dadosChancela.SeqAlunoHistoricoCicloLetivo.Value));
                            var componentesDispensados = this.HistoricoEscolarDomainService.ObterComponentesCurricularesDispensados(alunoHistoricoCicloLetivo.SeqAlunoHistorico);
                            var seqsComponentesDispensados = componentesDispensados.Where(a => a.SeqComponenteCurricular.HasValue).Select(a => a.SeqComponenteCurricular).ToList();

                            var itensSolicitacao = this.SolicitacaoMatriculaItemDomainService.BuscarSolicitacaoMatriculaItensPlano(chancela.Seq);
                            var seqsConfiguracaoComponenteItensSolicitacao = itensSolicitacao.Where(a => a.SeqConfiguracaoComponente.HasValue).Select(a => a.SeqConfiguracaoComponente).ToArray();
                            var specConfiguracaoComponente = new ConfiguracaoComponenteFilterSpecification() { SeqConfiguracoesComponentes = seqsConfiguracaoComponenteItensSolicitacao };
                            var seqsComponentesItensSolicitacao = this.ConfiguracaoComponenteDomainService.SearchProjectionBySpecification(specConfiguracaoComponente, x => x.SeqComponenteCurricular).ToList();

                            var turmasDispensadas = string.Empty;

                            foreach (var seqComponenteDispensado in seqsComponentesDispensados)
                            {
                                if (seqsComponentesItensSolicitacao.Contains(seqComponenteDispensado.Value))
                                {
                                    var itensDispensado = itensSolicitacao.Where(a => a.SeqComponenteCurricular == seqComponenteDispensado &&
                                                                                 (a.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso && a.PertencePlanoEstudo.GetValueOrDefault()) &&
                                                                                 (a.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso && !a.PertencePlanoEstudo.GetValueOrDefault()))
                                                                                 .ToList();

                                    foreach (var itemDispensado in itensDispensado)
                                    {
                                        if (itemDispensado.SeqTurma.HasValue)
                                        {
                                            var descricaoTurma = this.TurmaDomainService.BuscarDescricaoTurmaConcatenado(itemDispensado.SeqTurma.Value);

                                            if (!turmasDispensadas.Contains(descricaoTurma))
                                                turmasDispensadas += $"<br/> - {descricaoTurma}";
                                        }
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(turmasDispensadas))
                                throw new TurmaJaAprovadaDispensadaException(turmasDispensadas);
                        }
                    }
                }

                /// V5. Verificar se existe pelo menos um item que permanecerá no plano de estudo, de acordo com a regra:
                /// Se pelo menos um item da solicitação possuir o valor do campo "Pertence ao plano de estudo" igual a "Sim":
                /// 5.1.Verificar se PELO MENOS UM item da solicitação da pessoa - atuação está com a situação ATUAL igual à
                /// situação:
                /// 5.1.1. Configurada de acordo com a etapa para ser a final com a classificação “não alterado”, OU
                /// 5.1.2. Se pertence ao plano de estudo e a situação selecionada é uma situação configurada de acordo com a
                /// etapa para ser final com a classificação “Finalizada sem sucesso”, OU
                /// 5.1.3.Se NÃO pertence ao plano de estudo e a situação selecionada é uma situação configurada de acordo com
                /// a etapa para ser final com a classificação “Finalizada com sucesso”.
                /// Caso nenhum dos itens da solicitação se encontrar em nenhuma das três condições acima, abortar a operação e
                /// exibir a seguinte mensagem de erro: "Chancela não permitida. É necessário conter pelo menos um item no plano de estudo do aluno".

                /// REGRA IMPLEMENTADA NO MÉTODO QUE CRIA O PLANO DE ESTUDOS DO ALUNO AlterarPlanoDeEstudoPorSolicitacaoMatricula

                /// V6. Verificar se a pessoa atuação possui bloqueio:
                /// Verificar se a pessoa - atuação possui bloqueio parametrizado para a etapa em questão e que impede o início
                /// da etapa.Caso possua, abortar a operação e exibir a seguinte mensagem: "Chancela não permitida. A pessoa possui bloqueio."
                if (dadosChancela.Bloqueios.SMCAny())
                {
                    throw new ChancelaBloqueioEtapaException();
                }

                /// 7. Verificar se existem turmas iguais na solicitação, de acordo com a regra: * Verificar se existe mais de um item na solicitação da pessoa-atuação, com situação configurada para
                ///    ser a final com classificação "Finalizado com sucesso" ou "Não alterado", cuja turma das divisões de
                ///    turma possuem o mesmo código. Caso isto ocorra, verificar se as divisões são da mesma turma. Se
                ///    não for, abortar a operação e exibir a seguinte mensagem de erro:
                ///    Mensagem:
                /// 
                ///    "Seleção não permitida. Não é possível cursar as turmas abaixo, pois elas são do mesmo componente: - <Turma A>
                ///       - <Turma B> Favor verificar detalhes das turmas, clicando em "Mais informações"."

                /// 8. 
                ///     Verificar se existem turmas iguais na solicitação, de acordo com a regra:
                ///     1. Verificar se existe mais de um item na solicitação da pessoa-atuação:
                ///         -> Que não pertence ao plano de estudos, com situação atual configurada para ser a final com classificação "Finalizado com sucesso" OU
                ///         -> Que pertence ao plano, com situação atual configurada para ser a final com classificação "Não alterado" OU
                ///         -> Que pertence ao plano, com situação atual configurada para ser a final com classificação "Finalizado sem sucesso"
                ///         -> Cujas configurações de componente das divisões da turma são iguais. 
                ///     Caso isto ocorra, verificar se as divisões são da mesma turma (considerar o sequencial da turma).
                ///     1.1. Se forem da mesma, prosseguir.
                ///         1.2.Se não forem da mesma, verificar se elas possuem assunto e se os assuntos são diferentes.
                ///         1.2.1.Se os assuntos forem diferentes, prosseguir.
                ///         1.2.2.Se não possuírem assuntos ou se os assuntos não forem diferentes, abortar a operação e exibir a seguinte mensagem de erro:
                ///         "Seleção não permitida. Não é possível cursar as turmas abaixo, pois elas são do mesmo componente:
                ///         - < Turma A >
                ///         - < Turma B >
                ///         Favor verificar detalhes das turmas, clicando em "Mais informações"."
                if (chancela.Turmas.SMCAny())
                {
                    List<long> seqsItensChancelados = new List<long>();
                    foreach (var item in chancela.Turmas)
                    {
                        foreach (var divisao in item.TurmaMatriculaDivisoes)
                        {
                            if (divisao.SituacaoPlanoEstudo == MatriculaPertencePlanoEstudo.Inclusao && divisao.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq)
                                seqsItensChancelados.Add(divisao.Seq.GetValueOrDefault());
                        }
                    }

                    var mensagemTurmasDuplicadas = SolicitacaoMatriculaItemDomainService.ValidarTurmasDuplicadas(chancela.Seq, dadosChancela.SeqPessoaAtuacao, chancela.SeqProcessoEtapa, null, seqsItensChancelados);
                    if (!string.IsNullOrEmpty(mensagemTurmasDuplicadas))
                        throw new TurmaIgualSelecionadaInvalidoException(mensagemTurmasDuplicadas);
                }

                /// A V9. deve ser realizada apos gravar a situação nos itens, pois faz leitura no banco.
                /// Se ficar antes dá erro!

                // Se passou em todas as VALIDAÇÕES, inicia a transação para chancela...
                using (var unitOfWork = SMCUnitOfWork.Begin())
                {
                    // Itens de turma...
                    if (chancela.Turmas != null)
                    {
                        foreach (var itemTurma in chancela.Turmas)
                        {
                            foreach (var item in itemTurma.TurmaMatriculaDivisoes)
                            {
                                // Busca a ultuma situação do item.
                                var ultimaSituacaoItem = dadosChancela.UltimasSituacoesItens.FirstOrDefault(i => i.SeqItem == item.Seq).UltimaSituacao;
                                var ultimaSituacaoItemEtapa = dadosChancela.UltimasSituacoesItens.FirstOrDefault(i => i.SeqItem == item.Seq).Historicos.Select(h => dadosChancela.TodasSituacoesItens.FirstOrDefault(t => t.Situacao.Seq == h)).Where(a => a.SeqEtapaSGF == etapaChancela.Seq).FirstOrDefault().Situacao;

                                /// 1. Para cada item de matricula cuja a situação selecionada é a situação configurada de acordo com a etapa para ser a
                                /// final com classificação "Finalizada sem sucesso"
                                /// 1.1.Verificar se a ultima situação item é uma situação configurada para ser a inicial da próxima etapa. Caso seja,
                                /// atribuir ao histórico da etapa posterior do item,  uma situação final com classificação "Cancelado" com motivo
                                /// "Por indeferimento".

                                // Caso seja um item em situação aguardando chancela e mudou para chancela indeferida
                                // ou
                                // Caso seja um item em situação chancela Deferida e mudou para chancela indeferida
                                // ou
                                // Caso seja um item em situação inicial da próxima etapa e mudou para chancela indeferida
                                if ((ultimaSituacaoItem.SeqSituacaoItemMatricula == situacaoInicialItemChancela.Seq && item.SeqSituacaoItemMatricula == situacaoFinalItemChancelaSemSucesso.Seq) ||
                                    (ultimaSituacaoItem.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq && item.SeqSituacaoItemMatricula == situacaoFinalItemChancelaSemSucesso.Seq) ||
                                    (ultimaSituacaoItem.SeqSituacaoItemMatricula == situacaoInicialItemProximaEtapa.Seq && item.SeqSituacaoItemMatricula == situacaoFinalItemChancelaSemSucesso.Seq))
                                {
                                    // Caso esteja na situação inicial da próxima etapa, devemos marcar o idt_dom_motivo_situacao_matricula
                                    // com valor 1 - Cancelado pela instituição
                                    if (ultimaSituacaoItem.SeqSituacaoItemMatricula == situacaoInicialItemProximaEtapa.Seq && proximaEtapa != null)
                                    {
                                        var historico = new SolicitacaoMatriculaItemHistoricoSituacao()
                                        {
                                            SeqSolicitacaoMatriculaItem = item.Seq.GetValueOrDefault(),
                                            SeqSituacaoItemMatricula = situacaoFinalItemChancelaCancelado.Seq,
                                            MotivoSituacaoMatricula = MotivoSituacaoMatricula.PorIndeferimento
                                        };
                                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
                                    }
                                }

                                /// 2. Para cada item de matrícula, salvar no histórico de situações do item a situação selecionada.
                                if (ultimaSituacaoItemEtapa.Seq != item.SeqSituacaoItemMatricula && item.SeqSituacaoItemMatricula > 0)
                                {
                                    var historico = new SolicitacaoMatriculaItemHistoricoSituacao()
                                    {
                                        SeqSolicitacaoMatriculaItem = item.Seq.GetValueOrDefault(),
                                        SeqSituacaoItemMatricula = item.SeqSituacaoItemMatricula
                                    };
                                    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
                                }

                                /// 3. Se for um item de turma e existe controle de vaga na etapa, verifica o que deve ser realizado para
                                /// o item de acordo com a RN_MAT_081 - Ocupação de vaga chancela
                                if (dadosChancela.ControleVagaEtapa)
                                {
                                    bool trocaGrupo = false;
                                    if (item.SituacaoPlanoEstudo == MatriculaPertencePlanoEstudo.Exclusao &&
                                        item.Motivo == MotivoSituacaoMatricula.PorTrocaDeGrupo &&
                                        item.PertencePlanoEstudo &&
                                        itemTurma.TurmaMatriculaDivisoes.Any(t => t.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq))
                                    {
                                        trocaGrupo = true;
                                    }

                                    var controleVaga = new ChancelaVagaVO()
                                    {
                                        SeqItem = item.Seq.GetValueOrDefault(),
                                        TurmaFormatado = itemTurma.TurmaFormatado,
                                        SeqSituacaoAtual = item.SeqSituacaoItemMatricula,
                                        SeqUltimoSituacao = ultimaSituacaoItem.SeqSituacaoItemMatricula,
                                        ControleVagaAnterior = dadosChancela.ControleVagaEtapaAnterior,
                                        AguardandoChancela = situacaoInicialItemChancela.Seq,
                                        Deferido = situacaoFinalItemChancela.Seq,
                                        Indeferido = situacaoFinalItemChancelaSemSucesso.Seq,
                                        InicialProximaEtapa = situacaoInicialItemProximaEtapa.Seq,
                                        PertencePlanoEstudo = SolicitacaoMatriculaItemDomainService.VerificarItemPertencePlanoDeEstudo(item.Seq.GetValueOrDefault()),
                                        ProximaEtapa = proximaEtapa != null,
                                        TrocaGrupo = trocaGrupo
                                    };
                                    OcuparDesocuparVagaChancela(controleVaga, desabilitarFiltro);
                                }
                            }
                        }
                    }

                    // Itens de atividade...
                    if (chancela.Atividades != null)
                    {
                        foreach (var item in chancela.Atividades)
                        {
                            // Busca a ultuma situação do item.
                            var ultimaSituacaoItem = dadosChancela.UltimasSituacoesItens.FirstOrDefault(i => i.SeqItem == item.SeqItem).UltimaSituacao;
                            var ultimaSituacaoItemEtapa = dadosChancela.UltimasSituacoesItens.FirstOrDefault(i => i.SeqItem == item.SeqItem).Historicos.Select(h => dadosChancela.TodasSituacoesItens.FirstOrDefault(t => t.Situacao.Seq == h)).Where(a => a.SeqEtapaSGF == etapaChancela.Seq).FirstOrDefault().Situacao;

                            /// 1. Para cada item de matricula cuja a situação selecionada é a situação configurada de acordo com a etapa para ser a
                            /// final com classificação "Finalizada sem sucesso"
                            /// 1.1.Verificar se a ultima situação item é uma situação configurada para ser a inicial da próxima etapa. Caso seja,
                            /// atribuir ao histórico da etapa posterior do item,  uma situação final com classificação "Cancelado" com motivo
                            /// "Por indeferimento".

                            // Caso seja um item em situação aguardando chancela e mudou para chancela indeferida
                            // ou
                            // Caso seja um item em situação chancela Deferida e mudou para chancela indeferida
                            // ou
                            // Caso seja um item em situação inicial da próxima etapa e mudou para chancela indeferida
                            if ((ultimaSituacaoItem.SeqSituacaoItemMatricula == situacaoInicialItemChancela.Seq && item.SeqSituacaoItemMatricula == situacaoFinalItemChancelaSemSucesso.Seq) ||
                                (ultimaSituacaoItem.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq && item.SeqSituacaoItemMatricula == situacaoFinalItemChancelaSemSucesso.Seq) ||
                                (ultimaSituacaoItem.SeqSituacaoItemMatricula == situacaoInicialItemProximaEtapa.Seq && item.SeqSituacaoItemMatricula == situacaoFinalItemChancelaSemSucesso.Seq))
                            {
                                // Caso esteja na situação inicial da próxima etapa, devemos marcar o idt_dom_motivo_situacao_matricula com valor 1 - Cancelado pela instituição
                                if (ultimaSituacaoItem.SeqSituacaoItemMatricula == situacaoInicialItemProximaEtapa.Seq && proximaEtapa != null)
                                {
                                    var historico = new SolicitacaoMatriculaItemHistoricoSituacao()
                                    {
                                        SeqSolicitacaoMatriculaItem = item.SeqItem,
                                        SeqSituacaoItemMatricula = situacaoFinalItemChancelaCancelado.Seq,
                                        MotivoSituacaoMatricula = MotivoSituacaoMatricula.PorIndeferimento
                                    };
                                    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
                                }
                            }

                            /// 2. Para cada item de matrícula, salvar no histórico de situações do item a situação selecionada.
                            if (ultimaSituacaoItemEtapa.Seq != item.SeqSituacaoItemMatricula && item.SeqSituacaoItemMatricula > 0)
                            {
                                var historico = new SolicitacaoMatriculaItemHistoricoSituacao()
                                {
                                    SeqSolicitacaoMatriculaItem = item.SeqItem,
                                    SeqSituacaoItemMatricula = item.SeqSituacaoItemMatricula
                                };
                                SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
                            }
                        }
                    }

                    /// VALIDAÇÃO DEVE SER REALIZADA APOS GRAVAR A NOVA SITUAÇÃO DOS ITENS, POIS A ROTINA RECUPERA A SITUAÇÃO DO BANCO!
                    /// V5. Verificar se existe mais de um item (turma) na solicitação com o mesmo código que vai para o plano de estudo, de
                    /// acordo com a regra:
                    /// SOMENTE quando a solicitação for de um processo de um serviço com o token "SOLICITACAO_ALTERACAO_PLANO_ESTUDO" ou
                    /// "SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA", verificar se existe na solicitação mais de uma turma, das
                    /// divisões de turma, com o mesmo código
                    /// 5.1.Se existir, verificar se pelo menos um desses itens possui o valor do campo “Pertence ao plano de estudo” igual
                    /// a “Sim” e a situação selecionada é a situação configurada para ser final com a classificação “finalizada SEM sucesso”
                    /// E se pelo menos um desses itens possui o valor do campo “Pertence ao plano de estudo” igual a “Não” e a situação
                    /// selecionada é a situação configurada para ser final com a classificação “finalizada COM sucesso”. Caso ocorra,
                    /// abortar a operação e exibir a seguinte mensagem de erro:
                    /// "Chancela não permitida. Não é possível cursar as turmas abaixo, pois elas são do mesmo componente:
                    /// - < Turma A >
                    /// - < Turma B >
                    /// Favor verificar detalhes das turmas, clicando em "Mais informações"."
                    var validarTurmaIgual = SolicitacaoMatriculaItemDomainService.ValidaSolicitacaoMatriculaItemTurmaDuplicada(dadosChancela.SeqSolicitacaoServico, true, desabilitarFiltro);
                    if (!validarTurmaIgual.valido)
                    {
                        throw new TurmaIgualSelecionadaInvalidoException($"</br> {string.Join("</br>", validarTurmaIgual.mensagemErro)}");
                    }

                    /// VALIDAÇÃO DEVE SER REALIZADA APOS GRAVAR A NOVA SITUAÇÃO DOS ITENS, POIS A ROTINA RECUPERA A SITUAÇÃO DO BANCO!
                    /// V7. Verificar có-requisitos
                    /// Caso não seja chancela de processo de inclusão de disciplina eletiva (tokens = "CHANCELA_DISCIPLINA_ELETIVA_DESTINO" ou
                    /// "CHANCELA_DISCIPLINA_ELETIVA_ORIGEM") executar as regras "RN_MAT_033 - Consistência có-requisito - gestão de componente igual"
                    /// e "RN_MAT_079 - Consistência có-requisito - gestão de componente diferente" para verificar os có-requisitos.
                    if (token != MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO && token != MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_ORIGEM)
                    {
                        var validarTodosItens = SolicitacaoMatriculaItemDomainService.ValidaSolicitacaoMatriculaItemCoRequisito(dadosChancela.SeqSolicitacaoServico, dadosChancela.SeqPessoaAtuacao, SeqProcessoEtapa: chancela.SeqProcessoEtapa);
                        if (!validarTodosItens.valido)
                        {
                            throw new ConfiguracaoComponenteCoRequisitoInvalidoException($"</br> {string.Join("</br>", validarTodosItens.mensagemErro)}");
                        }
                    }

                    /// 4. Alterar ao histórico de situações da solicitação de acordo com a RN_MAT_066 - Procedimentos ao finalizar etapa

                    /**************************************** RN_MAT_066 ****************************************/
                    /// Atribuir a situação final da etapa, de acordo com a regra:
                    /// 1. Verificar se o token da etapa é "CHANCELA_DISCIPLINA_ELETIVA_ORIGEM" E se pelo menos um
                    /// item NÃO está com a situação configurada de acordo com a etapa para ser final com a classificação
                    /// "Finalizada com sucesso".
                    /// 1.1. Caso seja, atribuir ao histórico de situações da solicitação uma situação configurada de
                    /// acordo com a etapa em questão, para ser a final da etapa, FINAL DE PROCESSO, com o valor da
                    /// “Classificação” igual a “Finalizada com sucesso”.
                    /// 1.2. Caso NÃO seja, atribuir ao histórico de situações da solicitação uma situação configurada de
                    /// acordo com a etapa em questão, para ser a final da etapa, com o valor da “Classificação” igual a
                    /// “Finalizada com sucesso”.
                    if (token == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_ORIGEM &&
                                    !chancela.Turmas.SelectMany(t => t.TurmaMatriculaDivisoes).SMCAny(t => t.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq) &&
                                    !chancela.Atividades.SMCAny(t => t.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq))
                    {
                        SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(dadosChancela.SeqSolicitacaoServicoEtapa, situacaoFinalProcessoSucessoChancela.Seq, chancela.Observacao);
                        chancela.NaoAtualizarPlano = true;

                        /// 2.1. Não atribuir ao histórico de situações a situação configurada para ser inicial da próxima etapa.
                        proximaEtapa = null;
                    }
                    else
                    {
                        if (situacaoFinalSucessoChancela != null && situacaoFinalSucessoChancela.Seq > 0)
                            SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(dadosChancela.SeqSolicitacaoServicoEtapa, situacaoFinalSucessoChancela.Seq, chancela.Observacao);
                        else
                            SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(dadosChancela.SeqSolicitacaoServicoEtapa, situacaoFinalProcessoSucessoChancela.Seq, chancela.Observacao);
                    }

                    /// Atribuir a situação inicial da etapa posterior para a solicitação, de acordo com a regra:
                    /// 2.2. Se existir uma etapa posterior a esta:
                    /// 2.2.1. Verificar se existe configuração da etapa posterior associada à solicitação da pessoa-atuação,
                    /// caso não exista:
                    /// 2.2.1.1. Associar à solicitação a configuração da etapa posterior de acordo com a configuração do
                    /// processo da pessoa-atuação.
                    /// 2.2.2. Atribuir ao histórico de situações da solicitação a situação configurada para ser inicial da
                    /// próxima etapa.
                    if (proximaEtapa != null)
                    {
                        // Busca a situação inicial da próxima etapa.
                        var situacaoInicialProximaEtapa = proximaEtapa.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa);

                        // Verifica se já existe a próxima etapa para a solicitação
                        if (!dadosChancela.SeqsEtapas.Any(s => s.SeqEtapaSGF == proximaEtapa.Seq))
                        {
                            // Não existe esta etapa para a solicitação. incluir
                            SolicitacaoServicoEtapa solicitacaoServicoEtapa = new SolicitacaoServicoEtapa
                            {
                                SeqConfiguracaoEtapa = dadosChancela.ConfiguracoesEtapa.FirstOrDefault(c => c.Token == proximaEtapa.Token).Seq,
                                SeqSolicitacaoServico = dadosChancela.SeqSolicitacaoServico,
                            };
                            SolicitacaoServicoEtapaDomainService.SaveEntity(solicitacaoServicoEtapa);

                            // Inclui o historico da nova etapa
                            SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(solicitacaoServicoEtapa.Seq, situacaoInicialProximaEtapa.Seq, null);
                        }
                        else
                        {
                            // Inclui apenas o histórico
                            var seqSolicitacaoEtapa = dadosChancela.SeqsEtapas.FirstOrDefault(s => s.SeqEtapaSGF == proximaEtapa.Seq).SeqSolicitacaoServicoEtapa;
                            SolicitacaoHistoricoSituacaoDomainService.AtualizarHistoricoSituacao(seqSolicitacaoEtapa, situacaoInicialProximaEtapa.Seq, null);
                        }

                        /// Atribuir a situação inicial da etapa posterior para o item de matrícula, de acordo com a regra:
                        /// 3.2. Verificar se existe situação de item de matrícula cadastrada para a etapa posterior,
                        /// configurada para ser a inicial, caso exista:
                        /// 3.2.1 Para cada item de matrícula cuja a situação ATUAL é uma situação final com a classificação
                        /// "Finalizada com sucesso", atribuir ao histórico de situações do item, uma situação configurada
                        /// para ser a inicial da próxima etapa.
                        /// 3.2.2 Para cada item de matrícula cuja a situação ATUAL é uma situação final com a classificação
                        /// "Não alterado", atribuir ao histórico de situações do item, uma situação configurada para ser a
                        /// inicial, final com classificação "Não alterado" da próxima etapa.
                        /// 3.2.3 Para cada item de matrícula com campo "Pertence ao plano de estudo" com valor igual a "Sim",
                        /// cuja a situação ATUAL é uma situação final e classificação "Cancelado" e motivo "Pelo solicitante",
                        /// atribuir ao histórico de situações do item, uma situação configurada para ser a inicial da próxima
                        /// etapa.

                        // Itens de turma...
                        if (chancela.Turmas != null)
                        {
                            foreach (var itemTurma in chancela.Turmas)
                            {
                                foreach (var item in itemTurma.TurmaMatriculaDivisoes.Where(i => i.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq))
                                {
                                    var ultimaSituacaoItemEtapa = dadosChancela.UltimasSituacoesItens.FirstOrDefault(i => i.SeqItem == item.Seq.GetValueOrDefault()).Historicos.Select(h => dadosChancela.TodasSituacoesItens.FirstOrDefault(t => t.Situacao.Seq == h)).Where(a => a.SeqEtapaSGF == etapaChancela.Seq).FirstOrDefault().Situacao;
                                    var ultimaSituacaoItemSemEtapa = dadosChancela.UltimasSituacoesItens.FirstOrDefault(i => i.SeqItem == item.Seq.GetValueOrDefault()).Historicos.Select(h => dadosChancela.TodasSituacoesItens.FirstOrDefault(t => t.Situacao.Seq == h)).OrderByDescending(o => o.SeqEtapaSGF).FirstOrDefault().Situacao;
                                    if (item.SeqSituacaoItemMatricula != situacaoFinalItemChancelaSemSucesso.Seq &&
                                        item.SeqSituacaoItemMatricula != situacaoFinalItemChancelaCancelado.Seq &&
                                        ultimaSituacaoItemEtapa.Seq != situacaoInicialItemProximaEtapa?.Seq &&
                                        ultimaSituacaoItemSemEtapa != situacaoInicialItemProximaEtapa)
                                    {
                                        var historico = new SolicitacaoMatriculaItemHistoricoSituacao
                                        {
                                            SeqSolicitacaoMatriculaItem = item.Seq.GetValueOrDefault(),
                                            SeqSituacaoItemMatricula = situacaoInicialItemProximaEtapa.Seq
                                        };
                                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
                                    }
                                }


                                // Turmas da chancela de origem para chancela de destino com situação não alterado
                                foreach (var item in itemTurma.TurmaMatriculaDivisoes.Where(i => i.SeqSituacaoItemMatricula == situacaoNaoAlteradoItemChancela.Seq))
                                {
                                    if (item.SeqSituacaoItemMatricula != situacaoNaoAlteradoItemProximaEtapa.Seq)
                                    {
                                        var historico = new SolicitacaoMatriculaItemHistoricoSituacao
                                        {
                                            SeqSolicitacaoMatriculaItem = item.Seq.GetValueOrDefault(),
                                            SeqSituacaoItemMatricula = situacaoNaoAlteradoItemProximaEtapa.Seq
                                        };
                                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
                                    }
                                }
                            }
                        }
                        // Itens de Atividade
                        if (chancela.Atividades != null)
                        {
                            foreach (var item in chancela.Atividades.Where(i => i.SeqSituacaoItemMatricula == situacaoFinalItemChancela.Seq))
                            {
                                var ultimaSituacaoItemEtapa = dadosChancela.UltimasSituacoesItens.FirstOrDefault(i => i.SeqItem == item.SeqItem).Historicos.Select(h => dadosChancela.TodasSituacoesItens.FirstOrDefault(t => t.Situacao.Seq == h)).Where(a => a.SeqEtapaSGF == etapaChancela.Seq).FirstOrDefault().Situacao;
                                var ultimaSituacaoItemSemEtapa = dadosChancela.UltimasSituacoesItens.FirstOrDefault(i => i.SeqItem == item.SeqItem).Historicos.Select(h => dadosChancela.TodasSituacoesItens.FirstOrDefault(t => t.Situacao.Seq == h)).OrderByDescending(o => o.SeqEtapaSGF).FirstOrDefault().Situacao;
                                if (item.SeqSituacaoItemMatricula != situacaoFinalItemChancelaSemSucesso.Seq &&
                                    item.SeqSituacaoItemMatricula != situacaoFinalItemChancelaCancelado.Seq &&
                                    ultimaSituacaoItemEtapa.Seq != situacaoInicialItemProximaEtapa?.Seq &&
                                    ultimaSituacaoItemSemEtapa != situacaoInicialItemProximaEtapa)
                                {
                                    var historico = new SolicitacaoMatriculaItemHistoricoSituacao
                                    {
                                        SeqSolicitacaoMatriculaItem = item.SeqItem,
                                        SeqSituacaoItemMatricula = situacaoInicialItemProximaEtapa.Seq
                                    };
                                    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
                                }
                            }

                            // Atividades da chancela de origem para chancela de destino com situação não alterado
                            foreach (var item in chancela.Atividades.Where(i => i.SeqSituacaoItemMatricula == situacaoNaoAlteradoItemChancela.Seq))
                            {
                                if (item.SeqSituacaoItemMatricula != situacaoNaoAlteradoItemProximaEtapa.Seq)
                                {
                                    var historico = new SolicitacaoMatriculaItemHistoricoSituacao
                                    {
                                        SeqSolicitacaoMatriculaItem = item.SeqItem,
                                        SeqSituacaoItemMatricula = situacaoNaoAlteradoItemProximaEtapa.Seq
                                    };
                                    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);
                                }
                            }
                        }
                    }
                    /**************************************** FIM RN_MAT_066 ****************************************/

                    /// 5. Quando a solicitação for de um processo de um serviço com um dos tokens:
                    /// - SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA
                    /// - SOLICITACAO_ALTERACAO_PLANO_ESTUDO
                    /// - SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA

                    if ((token == MatriculaTokens.CHANCELA_ALTERACAO_PLANO_ESTUDO ||
                         token == MatriculaTokens.CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA ||
                         token == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO)
                        && chancela.NaoAtualizarPlano == false)
                    {
                        /// 5.1 - Criar um novo plano de estudos para o aluno conforme a RN_MAT_084 - Criação plano de estudo

                        /**************************************** RN_MAT_084 ****************************************/
                        // Gravar o novo plano de estudo de acordo com a solicitação de matricula e cancelar o plano de estudo anterior
                        PlanoEstudoDomainService.AlterarPlanoDeEstudoPorSolicitacaoMatricula(dadosChancela.SeqPessoaAtuacao, chancela.Seq, dadosChancela.SeqAlunoHistoricoCicloLetivo.Value);

                        // De acordo com e-mail do Igor 13/11/2018 não atualizar o SGP para Disciplina Eletiva
                        if (token == MatriculaTokens.CHANCELA_ALTERACAO_PLANO_ESTUDO ||
                            token == MatriculaTokens.CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA)
                        {
                            var seqsConfiguracaoComponenteItensPlanoAtual = PlanoEstudoItemDomainService
                                .BuscarSeqsItensPlanoEstudoAtualAluno(dadosChancela.SeqPessoaAtuacao, dadosChancela.SeqCicloLetivo)
                                .Select(s => s.SeqConfiguracaoComponente)
                                .ToList();

                            // Recupera as turmas com situação final de chancela
                            var dadosChancelaTurma = SearchProjectionByKey(spec, x => new
                            {
                                CodigoAlunoSGP = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao.HasValue ? (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao.Value : 0,
                                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,

                                Turmas = x.Itens.Where(i => i.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso
                                || (
                                        (i.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault(f => seqsConfiguracaoComponenteItensPlanoAtual.Contains(f.SolicitacaoMatriculaItem.SeqConfiguracaoComponente)).SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado
                                        || i.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso)
                                        && i.PertencePlanoEstudo)
                                ).Select(t => new AlunoTurmaSGPData
                                {
                                    SeqComponenteCurricular = t.ConfiguracaoComponente.SeqComponenteCurricular,
                                    SeqTurma = (long?)t.DivisaoTurma.SeqTurma,
                                    Creditos = t.ConfiguracaoComponente.ComponenteCurricular.Credito
                                }).ToList()
                            });

                            PlanoEstudoDomainService.AjustarValorMensalidadePlano(dadosChancela.SeqAlunoHistoricoCicloLetivo.Value,
                                dadosChancelaTurma.CodigoAlunoSGP,
                                dadosChancelaTurma.AnoCicloLetivo,
                                dadosChancelaTurma.NumeroCicloLetivo,
                                dadosChancelaTurma.Turmas.ToList());
                        }
                        /**************************************** FIM RN_MAT_084 ****************************************/

                        /// 5.2 - Criar mensagem na linha do tempo para o solicitante, conforme a regra:
                        /// RN_SRC_067 - Solicitação - Criação de mensagem na linha do tempo sobre encerramento solicitação
                        MensagemDomainService.EnviarMensagemLinhaDoTempoEncerramentoSolicitacao(chancela.Seq, TOKEN_TIPO_MENSAGEM.ENCERRAMENTO_SOLICITACAO_SERVICO, desabilitarFiltro);
                    }

                    /// 6. Atualizar as informações no campo "Descrição da solicitação atualizada" de acordo com a
                    /// RN_MAT_114 - Solicitação - Descrição original/atualizada.
                    var solicitacao = new SolicitacaoMatricula
                    {
                        Seq = chancela.Seq,
                        DescricaoAtualizada = SolicitacaoMatriculaItemDomainService.GerarDescricaoItensSolicitacao(chancela.Seq, dadosChancela.SeqConfiguracaoEtapa, false, desabilitarFiltro)
                    };
                    UpdateFields(solicitacao, x => x.DescricaoAtualizada);

                    /// 7. Quando a solicitação for do processo do serviço de token SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA
                    /// e o token da etapa for CHANCELA_DISCIPLINA_ELETIVA_ORIGEM enviar uma notificação de acordo com a regra
                    /// RN_MAT_110 - Notificação - Chancela programa de destino - Secretaria.
                    if (proximaEtapa != null && token == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_ORIGEM)
                    {
                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotivicacaoSolicitacaoServicoUnidadeCompartilhada(chancela.Seq, dadosChancela.SeqConfiguracaoEtapa);
                    }

                    unitOfWork.Commit();
                }
            }
            catch (SMCApplicationException ex)
            {
                if (ex is PlanoEstudoSemItensException)
                    throw new PlanoEstudoSemItensException("Chancela não permitida.");
                throw ex;
            }
            catch (Exception e)
            {
                if (e.InnerException != null && (e.InnerException is SqlException) && (e.InnerException as SqlException).Number == 1205)
                    throw new SMCApplicationException("Ocorreu um problema de concorrência ao reservar/liberar a vaga para a turma. Por favor, tente novamente.");
                throw e;
            }
            finally
            {
                // Habilita novamente os filtros que foram desabilitados no início
                if (desabilitarFiltro)
                {
                    this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

                    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    SolicitacaoHistoricoSituacaoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    SolicitacaoMatriculaItemDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    SolicitacaoServicoEtapaDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    PlanoEstudoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    MensagemDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                    AlunoHistoricoCicloLetivoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                }
            }
        }

        public void OcuparDesocuparVagaChancela(ChancelaVagaVO controle, bool desabilitarFiltro = false)
        {
            // 1 - Desocupar a vaga da turma
            // 1.1 - Etapa Anterior possui controle de vaga,
            //       Situação Aguardando Chancela --> Situação Indeferido,
            //       Não pertence ao plano de estudo
            if (controle.ControleVagaAnterior
            && (controle.SeqUltimoSituacao == controle.AguardandoChancela && controle.SeqSituacaoAtual == controle.Indeferido)
            && !controle.PertencePlanoEstudo)
            {
                SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(controle.SeqItem, desabilitarFiltro);
                return;
            }

            // 1.2 - Etapa Anterior não possui controle de vaga,
            //       Situação Aguradando Chancela --> Situação Deferido,
            //       Pertence ao plano de estudo
            if (!controle.ControleVagaAnterior
            && (controle.SeqUltimoSituacao == controle.AguardandoChancela && controle.SeqSituacaoAtual == controle.Deferido)
            && controle.PertencePlanoEstudo)
            {
                SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(controle.SeqItem, desabilitarFiltro);
                return;
            }

            // 1.3 - Situação Deferido --> Situação Indeferido
            if (controle.SeqUltimoSituacao == controle.Deferido && controle.SeqSituacaoAtual == controle.Indeferido)
            {
                SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(controle.SeqItem, desabilitarFiltro);
                return;
            }

            // 1.4 - Situação Inicial Próxima Etapa --> Situação Indeferido
            // Verificar se existe proxima etapa porque quando não existe a ultima situação é igual a inicial da proxima realizado essa atribuição para outras validações
            if (controle.ProximaEtapa && controle.SeqUltimoSituacao == controle.InicialProximaEtapa && controle.SeqSituacaoAtual == controle.Indeferido)
            {
                SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(controle.SeqItem, desabilitarFiltro);
                return;
            }

            // 2.1.1.1 - Se está realizando a troca de grupo, Libera a vaga na turma anterior
            if (controle.TrocaGrupo)
            {
                SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(controle.SeqItem, desabilitarFiltro);
                return;
            }

            // 2 - Ocupar a vaga da turma
            // 2.1 - Etapa Anterior possui controle de vaga,
            //       Situação Aguardando Chancela --> Situação Indeferido,
            //       Pertence ao plano de estudo
            if (controle.ControleVagaAnterior
            && (controle.SeqUltimoSituacao == controle.AguardandoChancela && controle.SeqSituacaoAtual == controle.Indeferido)
            && controle.PertencePlanoEstudo)
            {
                var erro = SolicitacaoMatriculaItemDomainService.ProcessarVagaTurmaAtividadeIngressante(controle.SeqItem, controle.TurmaFormatado, desabilitarFiltro);
                if (!string.IsNullOrWhiteSpace(erro))
                    throw new SMCApplicationException(erro);
                else
                    return;
            }

            // 2.2 - Etapa Anterior não possui controle de vaga,
            //       Situação Aguradando Chancela --> Situação Deferido,
            //       Não pertence ao plano de estudo
            if (!controle.ControleVagaAnterior
            && (controle.SeqUltimoSituacao == controle.AguardandoChancela && controle.SeqSituacaoAtual == controle.Deferido)
            && !controle.PertencePlanoEstudo)
            {
                var erro = SolicitacaoMatriculaItemDomainService.ProcessarVagaTurmaAtividadeIngressante(controle.SeqItem, controle.TurmaFormatado, desabilitarFiltro);
                if (!string.IsNullOrWhiteSpace(erro))
                    throw new SMCApplicationException(erro);
                else
                    return;
            }

            // 2.3 - Situação Indeferido --> Situação Deferido
            if (controle.SeqUltimoSituacao == controle.Indeferido && controle.SeqSituacaoAtual == controle.Deferido)
            {
                var erro = SolicitacaoMatriculaItemDomainService.ProcessarVagaTurmaAtividadeIngressante(controle.SeqItem, controle.TurmaFormatado, desabilitarFiltro);
                if (!string.IsNullOrWhiteSpace(erro))
                    throw new SMCApplicationException(erro);
                else
                    return;
            }
        }

        #endregion [ Chancela ]

        #region [ Efetivação ]

        public PlanoEstudoAlunoJaMatriculadoVO VerificaAlunoJaMatriculado(long seqSolicitacaoMatricula)
        {
            var dadosIngressante = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                SeqIngressante = x.SeqPessoaAtuacao,
                SeqPessoa = x.PessoaAtuacao.SeqPessoa,
                SeqCicloLetivoProcesso = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                SeqTipoVinculoAluno = (x.PessoaAtuacao as Ingressante).SeqTipoVinculoAluno,
                SeqEntidadeResponsavel = (x.PessoaAtuacao as Ingressante).SeqEntidadeResponsavel
            });

            // Recuperar os alunos
            var dadosAluno = AlunoDomainService.SearchProjectionByKey(new AlunoFilterSpecification { SeqPessoa = dadosIngressante.SeqPessoa, SeqCicloLetivoHistoricoAtual = dadosIngressante.SeqCicloLetivoProcesso, SeqTipoVinculoAluno = dadosIngressante.SeqTipoVinculoAluno, SeqEntidadeResponsavel = dadosIngressante.SeqEntidadeResponsavel }, x => new
            {
                Seq = x.Seq,
                DadosPlanoEstudo = x.Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.OrderByDescending(h => h.Seq).FirstOrDefault().PlanosEstudo.Select(p => new
                {
                    Atual = p.Atual,
                    SeqAlunoHistoricoCicloLetivo = (long?)p.SeqAlunoHistoricoCicloLetivo,
                    SeqMatrizCurricularOferta = (long?)p.SeqMatrizCurricularOferta,
                    Itens = p.Itens,
                    SeqPlanoEstudoAtual = p.Seq
                }).FirstOrDefault(p => p.Atual)
            });

            if (dadosAluno != null)
                return new PlanoEstudoAlunoJaMatriculadoVO
                {
                    Seq = dadosAluno.Seq,
                    Itens = dadosAluno.DadosPlanoEstudo.Itens,
                    SeqAlunoHistoricoCicloLetivo = dadosAluno.DadosPlanoEstudo.SeqAlunoHistoricoCicloLetivo,
                    SeqMatrizCurricularOferta = dadosAluno.DadosPlanoEstudo.SeqMatrizCurricularOferta,
                    SeqPlanoEstudoAtual = dadosAluno.DadosPlanoEstudo.SeqPlanoEstudoAtual
                };

            return null;
        }

        public void EfetivarMatricula(EfetivacaoMatriculaVO model)
        {
            long seqAluno = 0;

            // Nova validação UC_MAT_003_15 - Antes de fazer qualquer alteração em banco
            var solicitacaoServico = SolicitacaoServicoDomainService.SearchByKey(model.SeqSolicitacaoServico);
            //var processoEtapa = ConfiguracaoEtapaDomainService.SearchByKey(model.SeqConfiguracaoEtapa)?.ProcessoEtapa;
            var processoEtapa = ConfiguracaoEtapaDomainService.SearchProjectionByKey(model.SeqConfiguracaoEtapa, x => new
            {
                x.SeqProcessoEtapa,
                x.ProcessoEtapa.Seq,
                x.ProcessoEtapa.ConfiguracoesEtapa,
                x.ConfiguracaoProcesso,
                x.ConfiguracoesBloqueio,
            });

            if (processoEtapa != null && processoEtapa.ConfiguracoesBloqueio.Count > 0)
            {
                var seqconfiguracaoEtapa = processoEtapa.ConfiguracoesBloqueio.FirstOrDefault().SeqConfiguracaoEtapa;
                var bloqueios = this.PessoaAtuacaoBloqueioDomainService.BuscarPessoaAtuacaoBloqueios(solicitacaoServico.SeqPessoaAtuacao, seqconfiguracaoEtapa, true);

                if (bloqueios.Count > 0)
                {
                    string descricaoMotivosBloqueios = "";

                    foreach (var bloqueio in bloqueios)
                    {
                        string motivoBloqueio = bloqueio.MotivoBloqueio?.Descricao;
                        string itensBloqueio = string.Join(", ", bloqueio.Itens?.Select(d => d.Descricao).ToArray());

                        descricaoMotivosBloqueios += $"- {motivoBloqueio}: {itensBloqueio} </br>";
                    }

                    throw new EfetivarMatriculaException(descricaoMotivosBloqueios);
                }
            }

            // Chama o método que verifica se todos os documentos obrigatórios foram enviados e atualiza a flag da solicitação
            var documentosSolicitacao = RegistroDocumentoDomainService.ValidarDocumentoObrigatorio(model.SeqSolicitacaoServico);

            // renovação de matrícula também passa por esse método, então o que é usado para efetivar a renovação será chamado aqui
            if (model.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU || model.TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA)
            {
                EfetivarRenovacaoMatricula(model);
            }
            else if (model.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_LATO_SENSU)
            {
                EfetivarRenovacaoMatriculaResidenciaMedica(model);
            }
            else
            {
                // Verificar se ingressante já é aluno matriculado
                var alunoJaMatriculado = VerificaAlunoJaMatriculado(model.SeqSolicitacaoServico);
                if (alunoJaMatriculado != null)
                    throw new IngressanteJaMatriculadoException();

                // Recupera os dados da etapa do sgf
                var dadosEtapa = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(model.SeqSolicitacaoServico), x => new
                {
                    SeqTemplateProcessoSGF = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                    SeqEtapaSGFAtual = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.Seq == model.SeqConfiguracaoEtapa).ProcessoEtapa.SeqEtapaSgf
                });

                // Busca as etapas do SGF para saber qual a próxima etapa
                var etapasSGF = SGFHelper.BuscarEtapasSGFCache(dadosEtapa.SeqTemplateProcessoSGF);

                // Recupera as etapas do sgf para saber as situações possíveis dos itens
                var etapaAtual = etapasSGF.Where(e => e.Seq == dadosEtapa.SeqEtapaSGFAtual).OrderBy(e => e.Ordem).FirstOrDefault();

                // Recupera os dados da solicitação
                #region [ Dados da solicitação ]
                var dadosSolicitacao = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(model.SeqSolicitacaoServico), x => new
                {
                    SeqSolicitacaoMatricula = x.Seq,

                    // Dados do solicitante (ingressante)
                    SeqIngressante = x.SeqPessoaAtuacao,
                    SeqPessoa = x.PessoaAtuacao.SeqPessoa,
                    NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                    NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                    SeqInstituicaoEnsino = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
                    SeqUsuarioSAS = x.PessoaAtuacao.Pessoa.SeqUsuarioSAS,

                    // Dados da adesão
                    SeqArquivoTermoAdesao = x.SeqArquivoTermoAdesao,
                    DataAdesaoContrato = x.DataAdesao,
                    CodigoAdesaoContrato = x.CodigoAdesao,

                    // Dados da documentação
                    SituacaoDocumentacao = x.SituacaoDocumentacao,

                    SeqCondicaoPagamento = x.SeqCondicaoPagamentoGra,

                    // Dados do curso / vinculo
                    SeqCurso = (long?)(x.PessoaAtuacao as Ingressante).Ofertas.Select(o => o.CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta).FirstOrDefault().SeqCurso,
                    SeqFormacaoEspecifica = (long?)(x.PessoaAtuacao as Ingressante).FormacoesEspecificas.FirstOrDefault(fe => fe.FormacaoEspecifica.TipoFormacaoEspecifica.Token != TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).SeqFormacaoEspecifica,
                    SeqFormacaoEspecificaAreaTematica = (long?)(x.PessoaAtuacao as Ingressante).FormacoesEspecificas.FirstOrDefault(fe => fe.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).SeqFormacaoEspecifica,
                    DataPrevisaoConclusao = (x.PessoaAtuacao as Ingressante).DataPrevisaoConclusao,
                    SeqNivelEnsino = (x.PessoaAtuacao as Ingressante).SeqNivelEnsino,
                    SeqTipoVinculoAluno = (x.PessoaAtuacao as Ingressante).SeqTipoVinculoAluno,
                    // Dados orientação
                    CodigoProfessorAdministrativo = (int?)x.PessoaAtuacao.OrientacoesPessoaAtuacao
                        .FirstOrDefault(f => f.Orientacao.TipoOrientacao.TrabalhoConclusaoCurso)
                        .Orientacao.OrientacoesColaborador
                        .FirstOrDefault(o => o.TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador).Colaborador.Professores.FirstOrDefault().CodigoProfessorAdministrativo,

                    // Dados do processo
                    SeqSituacaoCanceladoEtapa = x.ConfiguracaoProcesso,
                    AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                    NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,

                    // Itens para matrícula
                    Turmas = x.Itens.Where(i => i.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).Select(t => new AlunoTurmaSGPData
                    {
                        SeqComponenteCurricular = t.ConfiguracaoComponente.SeqComponenteCurricular,
                        SeqTurma = (long?)t.DivisaoTurma.SeqTurma,
                        Creditos = t.ConfiguracaoComponente.ComponenteCurricular.Credito
                    }),
                    Itens = x.Itens.Select(i => new
                    {
                        i.Seq,
                        SituacaoFinalEtapas = i.HistoricosSituacao
                                               .GroupBy(g => g.SituacaoItemMatricula.ProcessoEtapa.SeqEtapaSgf) // Agrupa por etapa
                                               .Select(s => new // Recupera classificação da última situação de cada etapa
                                               {
                                                   SeqEtapaSgf = s.Key,
                                                   s.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal
                                               }),
                        TemGrupos = i.DivisaoTurma.Turma.DivisoesTurma.Count(d => d.SeqDivisaoComponente == i.DivisaoTurma.SeqDivisaoComponente) > 1,
                        SeqDivisaoComponente = (long?)i.DivisaoTurma.SeqDivisaoComponente
                    })
                });
                #endregion

                // So permite prosseguir caso a situação seja entregue ou entregue com pendência, ou caso não tenha nenhum obrigatório
                if (dadosSolicitacao.SituacaoDocumentacao != SituacaoDocumentacao.Entregue && dadosSolicitacao.SituacaoDocumentacao != SituacaoDocumentacao.EntregueComPendencia && dadosSolicitacao.SituacaoDocumentacao != SituacaoDocumentacao.NaoRequerida && dadosSolicitacao.SituacaoDocumentacao != SituacaoDocumentacao.Nenhum)
                    throw new RegistroDocumentoPendenteException();

                // Recupera a parametrização de instituição x nivel x tipo de vinculo do aluno
                var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(dadosSolicitacao.SeqInstituicaoEnsino, dadosSolicitacao.SeqNivelEnsino, dadosSolicitacao.SeqTipoVinculoAluno, null);

                using (var unityOfWork = SMCUnitOfWork.Begin())
                {
                    // Recupera o sequencial do CursoFormacaoEspecifica
                    //long seqCursoFormacaoEspecifica = CursoFormacaoEspecificaDomainService.SearchProjectionByKey(new CursoFormacaoEspecificaFilterSpecification { SeqFormacaoEspecifica = dadosSolicitacao.SeqFormacaoEspecifica, SeqCurso = dadosSolicitacao.SeqCurso }, x => x.Seq);
                    //long? seqCursoFormacaoEspecificaAreaTematica = null;

                    //if (dadosSolicitacao.SeqFormacaoEspecificaAreaTematica.HasValue)
                    //    seqCursoFormacaoEspecificaAreaTematica = CursoFormacaoEspecificaDomainService.SearchProjectionByKey(new CursoFormacaoEspecificaFilterSpecification { SeqFormacaoEspecifica = dadosSolicitacao.SeqFormacaoEspecificaAreaTematica, SeqCurso = dadosSolicitacao.SeqCurso }, x => x.Seq);

                    // Cria o aluno novo a partir do ingressante
                    var aluno = CriarAlunoPeloIngressante(dadosSolicitacao.SeqIngressante, model.SeqSolicitacaoServico, dadosSolicitacao.SeqCurso);

                    // FIX: Não está utilizando a rotina abaixo. Não retirei pois no futuro irá precisar
                    // A rotina abaixo adiciona uma disciplina isolada num aluno que já existe no banco de dados, para o caso de ter 2 ingressantes de DI para a mesma pessoa.
                    // Verifica se o ingressante é ingressante de disciplina isolada e já possui um aluno para a mesma pessoa
                    //var alunoDisciplinaIsolada = RecuperarAlunoDisciplinasIsoladas(model.SeqSolicitacaoMatricula);

                    /*if (alunoDisciplinaIsolada != null)
                    {
                        // Pessoa já está matriculada em alguma disciplina isolada. Vamos apenas adicionar a nova.
                        var itensAntigos = alunoDisciplinaIsolada.Itens;

                        // Pega as novas disciplinas isoladas que o ingressante atual possui
                        var itensNovos = aluno.Historicos.FirstOrDefault().HistoricosCicloLetivo.FirstOrDefault().PlanosEstudo.FirstOrDefault().Itens;

                        // Faz o merge com os itens da solicitação atual
                        var itensMerge = new List<PlanoEstudoItem>();
                        if (itensAntigos != null) itensMerge.AddRange(itensAntigos);
                        if (itensNovos != null) itensMerge.AddRange(itensNovos);

                        var itensSalvar = itensNovos.Select(x => new PlanoEstudoItem
                        {
                            SeqConfiguracaoComponente = x.SeqConfiguracaoComponente,
                            SeqDivisaoTurma = x.SeqDivisaoTurma,
                            SeqOrientacao = x.SeqOrientacao
                        }).ToList();

                        // Cria o novo plano de estudos para a pessoa
                        var planoEstudo = new PlanoEstudo
                        {
                            SeqAlunoHistoricoCicloLetivo = alunoDisciplinaIsolada.SeqAlunoHistoricoCicloLetivo.GetValueOrDefault(),
                            Atual = true,
                            SeqMatrizCurricularOferta = alunoDisciplinaIsolada.SeqMatrizCurricularOferta,
                            SeqSolicitacaoMatricula = dadosSolicitacao.SeqSolicitacaoMatricula,
                            Itens = itensSalvar
                        };

                        // Desmarca o atual do item de plano de estudo atual que já existe no aluno
                        PlanoEstudoDomainService.UpdateFields(new PlanoEstudo { Seq = alunoDisciplinaIsolada.SeqPlanoEstudoAtual, Atual = false }, x => x.Atual);

                        // Inclui um novo plano de estudo
                        PlanoEstudoDomainService.SaveEntity(planoEstudo);
                    }
                    else
                    {*/

                    // Caso seja aluno de disciplina isolada e tenha modificação nas turmas
                    var alteracaoTurmaDisciplinaIsolada = false;
                    if (!instituicaoNivelTipoVinculoAluno.ExigeCurso) // aluno DI = vinculo que não exige curso
                    {
                        long seqEtapaInicial = etapasSGF.First(f => f.Ordem == 1).Seq,
                             seqEtapaAtual = etapaAtual.Seq;
                        foreach (var itemAtual in dadosSolicitacao.Itens)
                        {
                            if (itemAtual.TemGrupos)
                            {
                                // Tem grupo
                                /* Verificar se a quantidade de itens com a situação atual com classificação "Finalizado com sucesso" na etapa inicial é 
                                   igual a quantidade de itens com a situação atual com classificação "Finalizado com sucesso" na etapa atual.*/

                                // recupera os itens de mesma divisão de componente
                                var itensMesmaDivisaoComponente = dadosSolicitacao.Itens.Where(i => i.SeqDivisaoComponente == itemAtual.SeqDivisaoComponente);

                                int totalFinalizadoSucessoEtapaInicial = 0;
                                int totalFinalizadoSucessoEtapaAtual = 0;

                                // Destes itens, verifica quantos foram finalizados com sucesso na etapa inicial e quantos estão finalizados com sucesso na etapa atual
                                itensMesmaDivisaoComponente.SMCForEach(i =>
                                {
                                    var classificacaoSituacaoEtapaInicial = i.SituacaoFinalEtapas.FirstOrDefault(f => f.SeqEtapaSgf == seqEtapaInicial)?.ClassificacaoSituacaoFinal;
                                    var classificacaoSituacaoEtapaAtual = i.SituacaoFinalEtapas.FirstOrDefault(f => f.SeqEtapaSgf == seqEtapaAtual)?.ClassificacaoSituacaoFinal;

                                    if (classificacaoSituacaoEtapaInicial == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                                        totalFinalizadoSucessoEtapaInicial++;

                                    if (classificacaoSituacaoEtapaAtual == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                                        totalFinalizadoSucessoEtapaAtual++;
                                });

                                // Caso seja diferente o total finalizado com sucesso da etapa atual e da etapa final, houve alterações
                                alteracaoTurmaDisciplinaIsolada |= totalFinalizadoSucessoEtapaAtual != totalFinalizadoSucessoEtapaInicial;

                            }
                            else
                            {
                                // Não tem grupo. faz a validação que já fazia anteriormente.
                                var classificacaoSituacaoEtapaInicial = itemAtual.SituacaoFinalEtapas.FirstOrDefault(f => f.SeqEtapaSgf == seqEtapaInicial)?.ClassificacaoSituacaoFinal;
                                var classificacaoSituacaoEtapaAtual = itemAtual.SituacaoFinalEtapas.FirstOrDefault(f => f.SeqEtapaSgf == seqEtapaAtual)?.ClassificacaoSituacaoFinal;
                                alteracaoTurmaDisciplinaIsolada |= classificacaoSituacaoEtapaAtual == null ?
                                    classificacaoSituacaoEtapaInicial != ClassificacaoSituacaoFinal.Cancelado : // Caso não tenha situação na etapa atual, foi alterado caso a situação inicial não seja cancelado
                                    classificacaoSituacaoEtapaAtual != classificacaoSituacaoEtapaInicial; // Caso tenha, foi alterando quando a situação atual for diferente da inicial
                            }

                            // caso tenha alteração, não precisa validar os outros itens
                            if (alteracaoTurmaDisciplinaIsolada)
                                break;
                        }
                    }
                    if (alteracaoTurmaDisciplinaIsolada)
                    {
                        // Verificar se tem registro de documento para termo de adesão
                        var tipoDocumentoTermoAdesao = TipoDocumentoService.BuscarTipoDocumentoPorToken("TERMO_DE_ADESAO_AO_CONTRATO_DE_PRESTACAO_DE_SERVICOS_EDUCACIONAIS");

                        // Caso tenha o tipo de documento no dados mestre...
                        if (tipoDocumentoTermoAdesao != null)
                        {
                            // Procura se na minha lista de documentos o tipo de documento com o sequencial do termo de adesão está pendente
                            var documentoTermoAdesao = documentosSolicitacao?.FirstOrDefault(d => d.SeqTipoDocumento == tipoDocumentoTermoAdesao.Seq);

                            // Caso tenha sido entregue algum termo de adesão, alertar que não pode continuar se o termo de adesão não for pendente, uma vez que houve modificações na seleção de turma e deve ser regerado
                            if (documentoTermoAdesao != null && documentoTermoAdesao.Documentos.Any(d => d.SituacaoEntregaDocumento != SituacaoEntregaDocumento.Pendente))
                                throw new TermoDeAdesaoNaoPendente(); //throw new TermoDeAdesaoNaoPendente(tipoDocumentoTermoAdesao.Descricao);
                        }

                        // Remover o arquivo do termo de adesão atual do aluno
                        if (dadosSolicitacao.SeqArquivoTermoAdesao.GetValueOrDefault() > 0)
                        {
                            // Update no seq de arquivo para limpar
                            UpdateFields(new SolicitacaoMatricula { Seq = model.SeqSolicitacaoServico, DataAdesao = DateTime.Now, CodigoAdesao = Guid.NewGuid() }, x => x.SeqArquivoTermoAdesao, x => x.DataAdesao, x => x.CodigoAdesao);

                            // Remove o arquivo
                            ArquivoAnexadoDomainService.DeleteEntity(dadosSolicitacao.SeqArquivoTermoAdesao.GetValueOrDefault());
                        }

                        // Gerar novamente o termo de adesão
                        ContratoDomainService.GerarTermoAdesaoContrato(model.SeqSolicitacaoServico);
                    }

                    // Recupera o código de pessoa no CAD
                    var codigoPessoaCAD = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(dadosSolicitacao.SeqPessoa, TipoPessoa.Fisica, null);

                    // Recupera os dados de origem e curso oferta localidade turno
                    var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacao.SeqIngressante);

                    // Cria o data para enviar ao SGP
                    var alunoSGPData = new AlunoSGPData
                    {
                        AnoCicloLetivo = dadosSolicitacao.AnoCicloLetivo,
                        CodigoAdesaoContrato = dadosSolicitacao.CodigoAdesaoContrato.GetValueOrDefault(),
                        CodigoPessoaCAD = codigoPessoaCAD,
                        CodigoProfessorSGP = dadosSolicitacao.CodigoProfessorAdministrativo.GetValueOrDefault(),
                        DataAdesaoContrato = dadosSolicitacao.DataAdesaoContrato.GetValueOrDefault(),
                        DataPrevisaoConclusao = dadosSolicitacao.DataPrevisaoConclusao.GetValueOrDefault(),
                        NumeroCicloLetivo = dadosSolicitacao.NumeroCicloLetivo,
                        PreMatricula = false,
                        SeqCondicaoPagamento = dadosSolicitacao.SeqCondicaoPagamento.GetValueOrDefault(),
                        SeqFormacaoEspecifica = dadosSolicitacao.SeqFormacaoEspecificaAreaTematica.HasValue ? dadosSolicitacao.SeqFormacaoEspecificaAreaTematica.GetValueOrDefault() : dadosSolicitacao.SeqFormacaoEspecifica.GetValueOrDefault(),
                        SeqIngressante = dadosSolicitacao.SeqIngressante,
                        // Agrupa as turmas com mais de uma divisão
                        Turmas = dadosSolicitacao.Turmas.GroupBy(t => t.SeqTurma).Select(t => t.FirstOrDefault()).ToList(),
                        Usuario = SMCContext.User.SMCGetCodigoPessoa().GetValueOrDefault().ToString(),
                        DisciplinaIsolada = !instituicaoNivelTipoVinculoAluno.ExigeCurso, // DI = vinculo do aluno não exige curso
                        SeqCursoOfertaLocalidadeTurno = dadosOrigem.SeqCursoOfertaLocalidadeTurno
                    };

                    // Grava a lista de orientação partindo da pessoa atuação para depois de gravar o aluno poder gravar as orientações no fluxo Orientação --> OrientaçãoPessoaAtuação
                    var OrientacoesPessoaAtuacaoCompleta = aluno.OrientacoesPessoaAtuacao.Select(s => s.Orientacao).ToList();
                    aluno.OrientacoesPessoaAtuacao = new List<OrientacaoPessoaAtuacao>();

                    // Salva o aluno
                    AlunoDomainService.SaveEntity(aluno);

                    var OrientacoesAlunoHistorico = aluno.Historicos.FirstOrDefault(f => f.Atual).HistoricosCicloLetivo.SelectMany(h => h.PlanosEstudo.SelectMany(p => p.Itens.Where(w => w.SeqOrientacao.HasValue).Select(i => i.Orientacao))).ToList();
                    var OrientacoesTipoIntercambio = aluno.TermosIntercambio.Where(w => w.SeqOrientacao.HasValue).Select(s => s.Orientacao).ToList();
                    var OrientacoesGravadas = new List<Orientacao>();
                    OrientacoesGravadas.AddRange(OrientacoesAlunoHistorico);
                    OrientacoesGravadas.AddRange(OrientacoesTipoIntercambio);

                    foreach (var orientacaoCompleta in OrientacoesPessoaAtuacaoCompleta)
                    {
                        if (!OrientacoesGravadas.Any(a => a.SeqEntidadeInstituicao == orientacaoCompleta.SeqEntidadeInstituicao
                                                     && a.SeqNivelEnsino == orientacaoCompleta.SeqNivelEnsino
                                                     && a.SeqTipoOrientacao == orientacaoCompleta.SeqTipoOrientacao))
                        {
                            OrientacaoDomainService.SaveEntity(orientacaoCompleta);
                        }

                        OrientacaoPessoaAtuacaoDomainService.SaveEntity(new OrientacaoPessoaAtuacao() { SeqPessoaAtuacao = aluno.Seq, SeqOrientacao = orientacaoCompleta.Seq });
                    }

                    // Chama o SGP para criar o aluno
                    // MODIFICADO PARA USAR O REPOSITORY NA PROPRIA APLICACAO DEVIDO AO MSDTC
                    // Antes usava o serviço do integração acadêmico
                    //var codigoAlunoSGP = IntegracaoAcademicoRepository.CriarAlunoSGP(alunoSGPData);
                    var codigoAlunoSGP = IntegracaoAcademicoService.CriarAlunoSGP(alunoSGPData);

                    // Preenche o código de integração do sgp
                    aluno.CodigoAlunoMigracao = (int)codigoAlunoSGP;

                    // Atualiza o número de registro acadêmico com o seq do pessoa atuação do aluno
                    aluno.NumeroRegistroAcademico = aluno.Seq;

                    //AlunoDomainService.UpdateFields(new Aluno { Seq = aluno.Seq, NumeroRegistroAcademico = aluno.Seq }, x => x.NumeroRegistroAcademico);
                    // Está dando problema no update fields pois está limpando o objeto concreto aluno e não a nova instância que é passada
                    // Usa o ExecuteSqlCommand para atualizar
                    ExecuteSqlCommand("update aln.aluno set num_registro_academico = @num_registro_academico, cod_aluno_migracao = @cod_aluno_migracao where seq_pessoa_atuacao = @seq", new List<SMCFuncParameter> { new SMCFuncParameter("@num_registro_academico", aluno.Seq), new SMCFuncParameter("@cod_aluno_migracao", aluno.CodigoAlunoMigracao), new SMCFuncParameter("@seq", aluno.Seq) });

                    // Atualiza RN_ALN_027 criar orientação de turma (quando o ingressante está se matriculando em uma divisão de turma que exige orientação),
                    // associar o professor desta orientação à divisão de turma correspondente (Tabela TUR.DIVISAO_TURMA_COLABORADOR), caso ele ainda não esteja associado.
                    var seqsDivisoesColaboradores = aluno.Historicos.FirstOrDefault(h => h.Atual)
                                                         .HistoricosCicloLetivo.FirstOrDefault()
                                                         .PlanosEstudo.FirstOrDefault()
                                                         .Itens.Where(w => w.SeqDivisaoTurma.HasValue && w.Orientacao != null && w.Orientacao.OrientacoesColaborador != null)
                                                         .SelectMany(s => s.Orientacao.OrientacoesColaborador.Select(o => new DivisaoTurmaColaboradorVO()
                                                         {
                                                             SeqDivisaoTurma = s.SeqDivisaoTurma.GetValueOrDefault(),
                                                             SeqColaborador = o.SeqColaborador
                                                         })).ToList();
                    DivisaoTurmaColaboradorDomainService.AdicionarProfessorOrientador(seqsDivisoesColaboradores);

                    // Modifica o histórico de situação do ingressante
                    var ingressanteHistoricoSituacao = new IngressanteHistoricoSituacao
                    {
                        SeqIngressante = dadosSolicitacao.SeqIngressante,
                        SituacaoIngressante = SituacaoIngressante.Matriculado,
                    };
                    IngressanteHistoricoSituacaoDomainService.SaveEntity(ingressanteHistoricoSituacao);

                    // Inclui o aluno no perfil de aluno do SAS
                    if (dadosSolicitacao.SeqUsuarioSAS.HasValue && dadosSolicitacao.SeqUsuarioSAS.GetValueOrDefault() > 0)
                        PerfilService.IncluirParticipantePerfilAplicacaoToken(SIGLA_APLICACAO.SGA_ALUNO,
                                                                              TOKENS_PERFIS_APLICACAO.ALUNO,
                                                                              dadosSolicitacao.SeqUsuarioSAS.GetValueOrDefault());

                    //}

                    // Atualiza a descrição
                    var solicitacao = new SolicitacaoMatricula
                    {
                        Seq = dadosSolicitacao.SeqSolicitacaoMatricula,
                        DescricaoAtualizada = SolicitacaoMatriculaItemDomainService.GerarDescricaoItensSolicitacao(dadosSolicitacao.SeqSolicitacaoMatricula, model.SeqConfiguracaoEtapa, false, false)
                    };
                    UpdateFields(solicitacao, x => x.DescricaoAtualizada);

                    // Monta os dados para merge do envio de notificação
                    Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dadosSolicitacao.NomeSocialSolicitante) ? dadosSolicitacao.NomeSolicitante : dadosSolicitacao.NomeSocialSolicitante);

                    // Envia a notificação
                    var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                    {
                        SeqSolicitacaoServico = dadosSolicitacao.SeqSolicitacaoMatricula,
                        TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.CONFIRMACAO_MATRICULA,
                        DadosMerge = dadosMerge,
                        EnvioSolicitante = true,
                        ConfiguracaoPrimeiraEtapa = false
                    };
                    SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);

                    // Procedimentos de finalização da etapa...
                    SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);

                    unityOfWork.Commit();

                    seqAluno = aluno.Seq;
                }
            }

            SalvarDocumentoPessoaAtuacao(documentosSolicitacao, solicitacaoServico.SeqPessoaAtuacao, seqAluno, model.TokenServico, model.SeqSolicitacaoServico);
        }

        public void SalvarDocumentoPessoaAtuacao(List<DocumentoVO> documentosSolicitacao, long seqPessoaAtuacao, long seqAluno, string tokenServico, long seqSolicitacaoServico)
        {
            var docsParaSalvar = BuscarDocumentosDeferidoOuEmAnalisePorSolicitacao(seqSolicitacaoServico);

            var seqsDocumentos = docsParaSalvar.Select(c => c.SeqTipoDocumento).Distinct().ToList();

            var specPessoaAtuacaoDocumento = new PessoaAtuacaoDocumentoFilterSpecification()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao
            };

            var documentosSalvosPessoaAtuacaoDocumentos = PessoaAtuacaoDocumentoDomainService.SearchBySpecification(specPessoaAtuacaoDocumento).ToList();

            var seqsTipoDocumentoSalvo = documentosSalvosPessoaAtuacaoDocumentos.Select(d => d.SeqTipoDocumento).Distinct().ToList();

            var documentosParaSalvar = docsParaSalvar.Where(c => c.EntregueAnteriormente == false && c.DataEntrega.HasValue).ToList(); //.Where(c => seqsDocumentos.Any(d => d != c.SeqTipoDocumento)).ToList();

            if (documentosParaSalvar.Count > 0)
            {
                List<PessoaAtuacaoDocumento> documentosPessoaAtuacao = new List<PessoaAtuacaoDocumento>();

                foreach (var doc in documentosParaSalvar)
                {
                    //var docAnexo = doc.Documentos.FirstOrDefault();

                    documentosPessoaAtuacao.Add(new PessoaAtuacaoDocumento()
                    {
                        SeqPessoaAtuacao = tokenServico == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU ||
                                           tokenServico == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA
                                           ? seqAluno : seqPessoaAtuacao,
                        SeqTipoDocumento = doc.SeqTipoDocumento,
                        SeqSolicitacaoDocumentoRequerido = doc.Seq,
                        DataEntrega = doc.DataEntrega.Value,
                        Observacao = doc.ObservacaoSecretaria,
                        SeqArquivoAnexado = doc.SeqArquivoAnexado
                    });
                }

                PessoaAtuacaoDocumentoDomainService.BulkSaveEntity(documentosPessoaAtuacao);
            }
        }

        private List<DocumentoItemVO> BuscarDocumentosDeferidoOuEmAnalisePorSolicitacao(long seqSolicitacaoServico)
        {
            var specSolicitacaoServico = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);

            var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacaoServico, s => new
            {
                SeqsConfiguracoesEtapas = s.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(a => a.Seq),
                SexoPessoaAtuacao = s.PessoaAtuacao.DadosPessoais.Sexo,
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                DocumentosRequeridos = s.DocumentosRequeridos.ToList(),
                DocumentosEnviados = s.DocumentosRequeridos.Select(d => new DocumentoItemVO
                {
                    Seq = d.Seq,
                    SeqSolicitacaoServico = d.SeqSolicitacaoServico,
                    SeqDocumentoRequerido = d.SeqDocumentoRequerido,
                    SeqTipoDocumento = d.DocumentoRequerido.SeqTipoDocumento,
                    SeqArquivoAnexado = d.SeqArquivoAnexado,
                    ArquivoAnexado = d.SeqArquivoAnexado.HasValue ? new SMCUploadFile
                    {
                        FileData = d.ArquivoAnexado.Conteudo,
                        GuidFile = d.ArquivoAnexado.UidArquivo.ToString(),
                        Name = d.ArquivoAnexado.Nome,
                        Size = d.ArquivoAnexado.Tamanho,
                        Type = d.ArquivoAnexado.Tipo
                    } : null,
                    DataEntrega = d.DataEntrega,
                    FormaEntregaDocumento = d.FormaEntregaDocumento,
                    Observacao = d.Observacao,
                    ObservacaoSecretaria = d.ObservacaoSecretaria,
                    DescricaoInconformidade = d.DescricaoInconformidade,
                    VersaoDocumento = d.VersaoDocumento,
                    SituacaoEntregaDocumento = d.SituacaoEntregaDocumento,
                    SituacaoEntregaDocumentoInicial = d.SituacaoEntregaDocumento,
                    VersaoExigida = d.DocumentoRequerido.VersaoDocumento,
                    EntregaPosterior = d.EntregaPosterior,
                    DataPrazoEntrega = d.DataPrazoEntrega,
                    EntregueAnteriormente = d.EntregueAnteriormente
                }).ToList()
            });

            var documentosDeferidosOuEmAnalise = solicitacaoServico.DocumentosEnviados.Where(
                                             c => c.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                                  c.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)
                                             .ToList();

            return documentosDeferidosOuEmAnalise;
        }


        /// <summary>
        /// Realizar a efetivação da rematricula atualizando o histórico do aluno e os dados do SGP
        /// </summary>
        /// <param name="model">Dados da solicitação para efetivação</param>
        public void EfetivarRenovacaoMatricula(EfetivacaoMatriculaVO model)
        {
            // Busca os dados da solicitação
            var spec = new SMCSeqSpecification<SolicitacaoMatricula>(model.SeqSolicitacaoServico);
            var dados = this.SearchProjectionByKey(spec, x => new
            {
                // Dados do aluno
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                CodigoAlunoSGP = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                AlunoHistorico = (x.PessoaAtuacao as Aluno).Historicos.Select(h => new
                {
                    Atual = h.Atual,
                    SeqAlunoHistorico = h.Seq,
                    SeqNivelEnsino = h.SeqNivelEnsino,
                    SeqCursoOfertaLocalidadeTurno = h.SeqCursoOfertaLocalidadeTurno,
                    DataPrevisaoConclusao = h.PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataPrevisaoConclusao,
                    DataLimiteConclusao = h.PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataLimiteConclusao,
                    SeqFormacaoEspecifica = (long?)h.Formacoes.FirstOrDefault(fe => fe.FormacaoEspecifica.TipoFormacaoEspecifica.Token != TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).SeqFormacaoEspecifica,
                }).FirstOrDefault(ah => ah.Atual),
                SeqInstituicaoEnsino = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
                SeqTipoVinculoAluno = (x.PessoaAtuacao as Aluno).SeqTipoVinculoAluno,

                // Dados do processo
                SeqCicloLetivoProcesso = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                AnoCicloLetivoProcesso = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloLetivoProcesso = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                PercentualServicoAdicional = x.ConfiguracaoProcesso.Processo.ValorPercentualServicoAdicional,

                // Dados da solicitação
                Turmas = x.Itens.Where(i => i.HistoricosSituacao.OrderByDescending(h => h.Seq)
                                .FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                                .Select(t => new
                                {
                                    SeqComponenteCurricular = t.ConfiguracaoComponente.SeqComponenteCurricular,
                                    SeqTurma = (long?)t.DivisaoTurma.SeqTurma,
                                    Creditos = t.ConfiguracaoComponente.ComponenteCurricular.Credito,
                                    SeqConfiguracaoComponente = t.SeqConfiguracaoComponente,
                                    SeqDivisaoTurma = t.SeqDivisaoTurma
                                }),
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao
            });

            // Se a solicitação é de um processo que não possui ciclo letivo, erro
            if (!dados.SeqCicloLetivoProcesso.HasValue)
                throw new ProcessoSemCicloLetivoException();

            // Verificar se na solicitação tem algum componente já aprovado/dispensado (validar apenas as turmas)
            var componentesSol = dados.Turmas.Where(i => i.SeqDivisaoTurma.HasValue).Select(i => (i.SeqConfiguracaoComponente, i.SeqDivisaoTurma)).ToList();
            var strValidacao = HistoricoEscolarDomainService.VerificarHistoricoComponentesAprovadosDispensados(dados.SeqPessoaAtuacao, componentesSol, dados.SeqCicloLetivoProcesso);
            if (!string.IsNullOrEmpty(strValidacao))
                throw new TurmaJaAprovadaDispensadaException(strValidacao, "Efetivação");

            // Busca o periodo do ciclo letivo do processo do processo
            var cicloLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dados.SeqCicloLetivoProcesso.Value, dados.AlunoHistorico.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(), TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // Recupera a parametrização de instituição x nivel x tipo de vinculo do aluno
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(dados.SeqInstituicaoEnsino, dados.AlunoHistorico.SeqNivelEnsino, dados.SeqTipoVinculoAluno, IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio);

            // Inicia a transação
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                /************************************************************************/
                // 1. INCLUI NOVA SITUAÇÃO PARA O ALUNO NO CICLO LETIVO DO PROCESSO

                // Busca o historico de ciclo letivo do aluno no ciclo do processo
                var specHistorico = new AlunoHistoricoCicloLetivoFilterSpecification()
                {
                    SeqAlunoHistorico = dados.AlunoHistorico.SeqAlunoHistorico,
                    SeqCicloLetivo = dados.SeqCicloLetivoProcesso.Value
                };
                var seqHistorico = AlunoHistoricoCicloLetivoDomainService.SearchProjectionByKey(specHistorico, x => (long?)x.Seq);
                if (seqHistorico.HasValue)
                {
                    bool incluirSituacao = true;
                    DateTime dataSituacao = DateTime.Now;

                    // 1.1)	SITUAÇÃO MATRICULADO_MOBILIDADE:
                    // Se a ultima situação válida (não excluída) do aluno no ciclo letivo anterior ao ciclo do processo da
                    // solicitação for MATRICULADO_MOBILIDADE e a data de fim do período de intercâmbio associado a essa situação
                    // for IGUAL ou POSTERIOR à data de início do PERIODO_CICLO_LETIVO do ciclo letivo do processo da solicitação.
                    // Incluir a situação com os dados:
                    // - Situação = MATRICULADO_MOBILIDADE
                    // - Solicitação de serviço = solicitação de serviço sendo efetivada
                    // - Data de inicio da situação = data de inicio do PERIODO_CICLO_LETIVO do ciclo letivo do processo
                    // - Termo de intercâmbio = termo de intercâmbio da ultima situação do ciclo anterior
                    // - Observação = “Efetivação da matrícula”

                    // Buscar ultima situação do aluno no ciclo anterior ao do processo
                    var cicloAnterior = CicloLetivoDomainService.BuscarCicloLetivoAnterior(dados.SeqCicloLetivoProcesso.Value);
                    if (cicloAnterior.HasValue)
                    {
                        var situacaoAnterior = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAlunoNaData(dados.SeqPessoaAtuacao, cicloAnterior.Value, cicloLetivo.DataInicio.AddDays(-1));

                        // Se aluno em mobilidade...
                        if (situacaoAnterior != null)
                        {
                            if (situacaoAnterior.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE)
                            {
                                // Verifica se tem o período de intercâmbio associado na situação
                                if (!situacaoAnterior.SeqPeriodoIntercambio.HasValue)
                                    throw new AlunoMobilidadeSemTermoIntercambioException(!string.IsNullOrEmpty(dados.NomeSocialSolicitante) ? dados.NomeSocialSolicitante : dados.NomeSolicitante);

                                // Busca o período de mobilidade do aluno
                                var specPI = new PeriodoIntercambioFilterSpecification() { Seq = situacaoAnterior.SeqPeriodoIntercambio };
                                var perInter = PeriodoIntercambioDomainService.SearchProjectionByKey(specPI, p => new
                                {
                                    DataInicio = p.DataInicio,
                                    DataFim = p.DataFim
                                });

                                // Se termo termina no dia ou posterior ao inicio do período do ciclo letivo
                                if (perInter != null && perInter.DataFim >= cicloLetivo.DataInicio)
                                {
                                    // Incluir a situação de mobilidade
                                    var situacaoMobilidade = new IncluirAlunoHistoricoSituacaoVO()
                                    {
                                        SeqAluno = dados.SeqPessoaAtuacao,
                                        SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                                        SeqAlunoHistoricoCicloLetivo = seqHistorico,
                                        DataInicioSituacao = cicloLetivo.DataInicio,
                                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE,
                                        Observacao = "Efetivação da matrícula",
                                        SeqPeriodoIntercambio = situacaoAnterior.SeqPeriodoIntercambio
                                    };
                                    AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoMobilidade);

                                    // Se o periodo de mobilidade termina antes do fim do ciclo letivo
                                    if (perInter.DataFim < cicloLetivo.DataFim)
                                    {
                                        // Prossegue com as inclusões de situação, mas altera a data de inicio
                                        dataSituacao = perInter.DataFim.AddDays(1);
                                    }
                                    else
                                    {
                                        // Periodo de mobilidade ultrapassa o fim do ciclo letivo do processo
                                        // Não inclui outra situações para o ciclo
                                        incluirSituacao = false;
                                    }

                                    // Se inclui a situação de mobilidade com data de inicio = data inicio do ciclo letivo, e
                                    // a situação de APTO PARA MATRICULA foi criada com data posterior a data de inicio do ciclo
                                    // precisa excluir a situação de APTO para que a situação de mobilidade passe a valer apos a
                                    // efetivação
                                    /* Se existir a situação "APTO_MATRICULA" no ciclo letivo do processo, com data início maior ou 
                                     * igual a data início do ciclo letivo, incluir as informações abaixo:
                                     * - Data de exclusão: data atual (do sistema).
                                     * - Usuário de exclusão: usuário logado
                                     * - Observação de exclusão: "Excluído pois a preparação da renovação foi realizada após o início do ciclo letivo.."
                                     */
                                    var specApto = new AlunoHistoricoSituacaoFilterSpecification()
                                    {
                                        SeqAlunoHistoricoCicloLetivo = seqHistorico,
                                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                                        Excluido = false
                                    };
                                    var sitApto = AlunoHistoricoSituacaoDomainService.SearchByKey(specApto);
                                    if (sitApto != null && sitApto.DataInicioSituacao >= cicloLetivo.DataInicio)
                                    {
                                        var obsExclusao = "Excluído pois a preparação da renovação foi realizada após o início do ciclo letivo.";
                                        AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(sitApto.Seq, obsExclusao, model.SeqSolicitacaoServico);
                                    }
                                }
                            }
                        }
                    }


                    var pessoaAtuacaoIntercambioSpec = new PessoaAtuacaoTermoIntercambioFilterSpecification { SeqNivelEnsino = dados.AlunoHistorico.SeqNivelEnsino, SeqPessoaAtuacao = dados.SeqPessoaAtuacao, SeqTipoVinculo = dados.SeqTipoVinculoAluno };
                    var pessoaAtuacaoIntercambio = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(pessoaAtuacaoIntercambioSpec, x => x.SeqTermoIntercambio).ToArray();

                    var termoIntercambioSpec = new TermoIntercambioFilterSpecification { SeqsTermosIntercambio = pessoaAtuacaoIntercambio };
                    var termoIntercambio = TermoIntercambioDomainService.SearchProjectionBySpecification(termoIntercambioSpec, x => x.SeqParceriaIntercambioTipoTermo).ToArray();

                    var parceriaTipoTermoSpec = new ParceriaIntercambioTipoTermoFilterSpecification { SeqsParceriaIntercambioTipoTermo = termoIntercambio };
                    var parceriaTipoTermo = ParceriaIntercambioTipoTermoDomainService.SearchBySpecification(parceriaTipoTermoSpec);

                    var tipoTermoConcedeFormacao = false;
                    foreach (var item in parceriaTipoTermo)
                    {
                        var concedeFormacao = TipoTermoIntercambioDomainService.TipoTermoIntercambioConcedeFormacao(item.SeqTipoTermoIntercambio, dados.SeqTipoVinculoAluno, dados.AlunoHistorico.SeqNivelEnsino, dados.SeqInstituicaoEnsino);
                        if (concedeFormacao)
                            tipoTermoConcedeFormacao = true;
                    }


                    if (incluirSituacao)
                    {
                        // 1.2)	SITUAÇÃO MATRICULADO ou PROVAVEL_FORMANDO:
                        // Incluir a situação com os dados:
                        // - Situação = Se a data de deposito do trabalho acadêmico, do tipo de trabalho
                        // parametrizado de acordo com a instituição de ensino logada e o nível de ensino
                        // da pessoa-atuação em questão para gerar financeiro entrega trabalho, for DIFERENTE
                        // de null(ALUNO TEM DATA DE DEPOSITO) Gravar a situação de matrícula PROVAVEL_FORMANDO.
                        // Caso contrário, gravar a situação de matrícula MATRICULADO.
                        // - Solicitação de serviço = solicitação de serviço sendo efetivada
                        // - Data de inicio da situação = Se foi criada a situação 1.1 para o aluno em mobilidade,
                        // a data de inicio será UM DIA APÓS a data de fim do período de intercâmbio.Caso contrário
                        // será igual à data de efetivação (data atual do sistema)
                        // - Observação = “Efetivação da matrícula”

                        // Define a nova situação
                        var tokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                        var trabalho = TrabalhoAcademicoDomainService.BuscarDatasDepositoDefesaTrabalho(dados.SeqPessoaAtuacao);
                        if (trabalho.DataDeposito.HasValue)
                        {
                            tokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO;
                        }

                        //Se o aluno possuir data de autorização de segundo depósito em algum trabalho acadêmico, que pertence ao ciclo letivo do processo da solicitação
                        //(considerando o PERIODO_CICLO_LETIVO do ciclo letivo), a data de início será igual a maior data de autorização encontrada.
                        var specTrabalhoAcademicoAutoria = new TrabalhoAcademicoAutoriaSpecification { SeqAluno = dados.SeqPessoaAtuacao };

                        var trabalhoAcademicoAutoria = TrabalhoAcademicoAutoriaDomainService.SearchBySpecification(specTrabalhoAcademicoAutoria);
                        if (trabalhoAcademicoAutoria != null && trabalhoAcademicoAutoria.Count() > 0)
                        {
                            var seqsTrabalhoAcademico = trabalhoAcademicoAutoria.Select(x => x.SeqTrabalhoAcademico).ToList();
                            var specTrabalhoAcademico = new TrabalhoAcademicoAlunoSpecification { SeqsTrabalhoAcademico = seqsTrabalhoAcademico };

                            var trabalhoAcademico = TrabalhoAcademicoDomainService.SearchBySpecification(specTrabalhoAcademico);

                            if (trabalhoAcademico != null && trabalhoAcademico.Count() > 0)
                            {
                                //Caso possua um ou mais trabalhos acadêmicos, busca o registro que tenha a maior data de autorização segundo depósito respeitando o ciclo letivo
                                //DO PROCESSO e define a data situação com o seu valor
                                var trabalhoComMaiorData = trabalhoAcademico.Where(x => x.DataAutorizacaoSegundoDeposito.HasValue
                                                                             && x.DataAutorizacaoSegundoDeposito.Value >= cicloLetivo.DataInicio
                                                                             && x.DataAutorizacaoSegundoDeposito.Value <= cicloLetivo.DataFim)
                                                                             .OrderByDescending(x => x.DataAutorizacaoSegundoDeposito)
                                                                             .FirstOrDefault();

                                if (trabalhoComMaiorData != null)
                                    dataSituacao = trabalhoComMaiorData.DataAutorizacaoSegundoDeposito.Value;
                            }
                        }

                        // Inclui a nova situação
                        var incluiNovaSituacao = new IncluirAlunoHistoricoSituacaoVO()
                        {
                            SeqAluno = dados.SeqPessoaAtuacao,
                            SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                            SeqAlunoHistoricoCicloLetivo = seqHistorico,
                            DataInicioSituacao = dataSituacao,
                            TokenSituacao = tokenSituacao,
                            Observacao = "Efetivação da matrícula"
                        };


                        //Se o aluno for de um vínculo que está parametrizado para não conceder formação E um termo de intercâmbio cujo tipo está
                        //parametrizado para não conceder formação, a data início deverá ser igual a data início do PERIODO_CICLO_LETIVO do ciclo
                        //letivo do processo.Se existir a situação "APTO_MATRICULA" no ciclo letivo do processo, com data início maior ou igual a
                        //data início do ciclo letivo, incluir as informações:
                        //Data de exclusão: data atual(do sistema),
                        //Usuário de exclusão: usuário logado, Observação de exclusão:
                        //"Excluído, pois a preparação da renovação foi realizada após o início do ciclo letivo."
                        if (!instituicaoNivelTipoVinculoAluno.ConcedeFormacao && !tipoTermoConcedeFormacao)
                        {
                            incluiNovaSituacao.DataInicioSituacao = cicloLetivo.DataInicio;

                            var situacoesNoCiclo = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(dados.SeqPessoaAtuacao, cicloLetivo.SeqCicloLetivo);
                            if (situacoesNoCiclo.Any(x => x.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA && x.DataInicioSituacao >= cicloLetivo.DataInicio))
                            {

                                var specApto = new AlunoHistoricoSituacaoFilterSpecification()
                                {
                                    SeqAlunoHistoricoCicloLetivo = seqHistorico,
                                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA,
                                    Excluido = false
                                };

                                var sitApto = AlunoHistoricoSituacaoDomainService.SearchByKey(specApto);
                                if (sitApto != null && sitApto.DataInicioSituacao >= cicloLetivo.DataInicio)
                                {
                                    var obsExclusao = "Excluído, pois a preparação da renovação foi realizada após o início do ciclo letivo.";

                                    AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(sitApto.Seq, obsExclusao, model.SeqSolicitacaoServico);
                                }
                            }


                        }
                        AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(incluiNovaSituacao);



                        // 1.3)	SITUAÇÃO PRAZO_CONCLUSAO_ENCERRADO:
                        // Se a situação do aluno incluída no item 1.2 for MATRICULADO e o prazo de conclusão do
                        // aluno termina antes do fim do PERIODO_CICLO_LETIVO do ciclo letivo do processo da
                        // solicitação, incluir a situação com os dados:
                        // - Situação = PRAZO_CONCLUSAO_ENCERRADO
                        // - Solicitação de serviço = solicitação de serviço sendo efetivada
                        // - Data de inicio da situação = UM DIA APÓS a data de previsão de conclusão
                        // - Observação = “Efetivação da matrícula”
                        if ((instituicaoNivelTipoVinculoAluno.ConcedeFormacao || tipoTermoConcedeFormacao) && tokenSituacao == TOKENS_SITUACAO_MATRICULA.MATRICULADO)
                        {
                            // Verifica se deve incluir situação
                            if (dados.AlunoHistorico.DataPrevisaoConclusao >= cicloLetivo.DataInicio &&
                                dados.AlunoHistorico.DataPrevisaoConclusao <= cicloLetivo.DataFim)
                            {
                                var historicoPrazoEncerrado = new IncluirAlunoHistoricoSituacaoVO()
                                {
                                    SeqAluno = dados.SeqPessoaAtuacao,
                                    SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                                    SeqAlunoHistoricoCicloLetivo = seqHistorico,
                                    DataInicioSituacao = dados.AlunoHistorico.DataPrevisaoConclusao.AddDays(1),
                                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO,
                                    Observacao = "Efetivação da matrícula"
                                };
                                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(historicoPrazoEncerrado);
                            }
                        }

                        // 1.4) SITUAÇÃO TERMINO_INTERCAMBIO
                        // Se a pessoa-atuação em questão for de um vínculo configurado por instituição-nível de ensino-vínculo
                        // de acordo com a instituição de ensino logada, nível de ensino e vínculo da pessoa-atuação para exigir
                        // parceria de intercâmbio no ingresso e o aluno possui apenas um termo e esse termo está parametrizado para
                        // não conceder formação (na instituição x nivel x vinculo aluno), verificar se a data de previsão de conclusão
                        // esta dentro do ciclo letivo do processo. Caso esteja, incluir a situação de matrícula  com os dados:
                        // - Situação = TERMINO_INTERCAMBIO
                        // - Solicitação de serviço = solicitação de serviço que está sendo efetivada
                        // - Data de inicio da situação = data de previsão de conclusão mais um dia
                        // - Observação = “Efetivação da matrícula”
                        if (instituicaoNivelTipoVinculoAluno.ExigeParceriaIntercambioIngresso)
                        {
                            // Busca todos os termos associados ao aluno
                            var filterTermo = new PessoaAtuacaoTermoIntercambioFilterSpecification()
                            {
                                SeqPessoaAtuacao = dados.SeqPessoaAtuacao
                            };
                            var termosAluno = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(filterTermo, t => new
                            {
                                Seq = t.Seq,
                                SeqTipoTermoIntercambio = t.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio
                            });

                            // Se o aluno possui apenas 1 termo
                            if (termosAluno != null && termosAluno.Count() == 1)
                            {
                                // Busca os tipos de termo que estão configurados por instituição x nivel x tipo de vinculo,
                                // para NÃO conceder formação
                                long[] tiposTermo = instituicaoNivelTipoVinculoAluno.TiposTermoIntercambio.Where(t => !t.ConcedeFormacao).Select(t => t.SeqTipoTermoIntercambio).ToArray();

                                // Verifica se o único termo que o aluno possui é de um tipo que NÃO concede formação
                                // conforme a parametrização por instituição x nível x tipo de vinculo
                                // E a data de previsão de conclusão esta dentro do ciclo letivo do processo
                                // Se sim, inclui a situação
                                if (tiposTermo.Contains(termosAluno.FirstOrDefault().SeqTipoTermoIntercambio) &&
                                    dados.AlunoHistorico.DataPrevisaoConclusao >= cicloLetivo.DataInicio &&
                                    dados.AlunoHistorico.DataPrevisaoConclusao <= cicloLetivo.DataFim)
                                {
                                    var historicoTerminoIntercambio = new IncluirAlunoHistoricoSituacaoVO()
                                    {
                                        SeqAluno = dados.SeqPessoaAtuacao,
                                        SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                                        SeqAlunoHistoricoCicloLetivo = seqHistorico,
                                        DataInicioSituacao = dados.AlunoHistorico.DataPrevisaoConclusao.AddDays(1),
                                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.TERMINO_INTERCAMBIO,
                                        Observacao = "Efetivação da matrícula"
                                    };
                                    AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(historicoTerminoIntercambio);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Se não encontrou o histórico de ciclo letivo, erro, pois deveria existir
                    // O JOB de renovação ou o atendimento de solicitação de reabertura criam o histórico
                    // no ciclo letivo com a situação APTO_MATRICULA
                    throw new AlunoHistoricoCicloLetivoInvalidoException();
                }

                /************************************************************************/
                // 2. ALTERA O PLANO DE ESTUDOS DO ALUNO NO CICLO LETIVO DO PROCESSO

                // Gravar o novo plano de estudo de acordo com a solicitação de matricula e cancela (ind_atual = 0) o
                // plano de estudo anterior
                PlanoEstudoDomainService.AlterarPlanoDeEstudoPorSolicitacaoMatricula(dados.SeqPessoaAtuacao, model.SeqSolicitacaoServico, seqHistorico.GetValueOrDefault());

                /************************************************************************/
                // 3. CHAMA ROTINA DE INTEGRAÇÃO COM O SGP

                var turmaSGP = dados.Turmas.Select(t => new AlunoTurmaSGPData
                {
                    SeqComponenteCurricular = t.SeqComponenteCurricular,
                    SeqTurma = t.SeqTurma,
                    Creditos = t.Creditos
                }).ToList();

                // Cria objeto para enviar ao SGP
                var alunoVeteranoSGPData = new AlunoVeteranoSGPData
                {
                    CodigoAlunoSGP = dados.CodigoAlunoSGP.GetValueOrDefault(),
                    AnoCicloLetivo = dados.AnoCicloLetivoProcesso,
                    NumeroCicloLetivo = dados.NumeroCicloLetivoProcesso,
                    SeqFormacaoEspecifica = dados.AlunoHistorico.SeqFormacaoEspecifica.GetValueOrDefault(),
                    SeqCursoOfertaLocalidadeTurno = dados.AlunoHistorico.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(),
                    Turmas = turmaSGP,
                    Usuario = SMCContext.User.SMCGetCodigoPessoa().GetValueOrDefault().ToString(),
                    PercentualServicoAdicional = dados.PercentualServicoAdicional,
                    DataLimiteConclusao = dados.AlunoHistorico.DataLimiteConclusao
                };

                // Busca os dados do(s) orientador(es) do aluno
                var specOrientacao = new OrientacaoFilterSpecification()
                {
                    SeqPessoaAtuacao = dados.SeqPessoaAtuacao,
                    SomenteSemTipoTermoIntercambio = true,
                    TokenTipoOrientacao = TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO
                };
                var dadosOrientacao = OrientacaoDomainService.SearchProjectionByKey(specOrientacao, o => new
                {
                    OrientacoesColaborador = o.OrientacoesColaborador.Select(s => new
                    {
                        TipoParticipacaoOrientacao = s.TipoParticipacaoOrientacao,
                        Cpf = s.Colaborador.Pessoa.Cpf,
                        NumeroPassaporte = s.Colaborador.Pessoa.NumeroPassaporte,
                        CodigoProfessorAdministrativo = (int?)s.Colaborador.Professores.FirstOrDefault(p => p.SituacaoProfessor == SituacaoProfessor.Normal).CodigoProfessorAdministrativo
                    }).ToList()
                });

                // Verifica se tem orientação
                if (dadosOrientacao != null)
                {
                    // Atualiza o objeto do SGP com as informações do(s) orientador(es) do aluno
                    // So passar os dados de documento do orientador/coorientador apenas quando não houver código de professor.
                    foreach (var orientador in dadosOrientacao.OrientacoesColaborador)
                    {
                        if (orientador.TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador)
                        {
                            alunoVeteranoSGPData.CodigoProfessorSGPOrientador = orientador.CodigoProfessorAdministrativo;
                            if (alunoVeteranoSGPData.CodigoProfessorSGPOrientador.GetValueOrDefault() == 0)
                            {
                                if (string.IsNullOrEmpty(orientador.Cpf))
                                    alunoVeteranoSGPData.DocumentoProfessorSGPOrientador = orientador.NumeroPassaporte;
                                else
                                    alunoVeteranoSGPData.DocumentoProfessorSGPOrientador = orientador.Cpf;
                            }
                            else
                            {
                                alunoVeteranoSGPData.DocumentoProfessorSGPOrientador = null;
                                alunoVeteranoSGPData.DocumentoProfessorSGPOrientador = null;
                            }
                        }
                        else if (orientador.TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Coorientador)
                        {
                            alunoVeteranoSGPData.CodigoProfessorSGPCoorientador = orientador.CodigoProfessorAdministrativo;
                            if (alunoVeteranoSGPData.CodigoProfessorSGPCoorientador.GetValueOrDefault() == 0)
                            {
                                if (string.IsNullOrEmpty(orientador.Cpf))
                                    alunoVeteranoSGPData.DocumentoProfessorSGPCoorientador = orientador.NumeroPassaporte;
                                else
                                    alunoVeteranoSGPData.DocumentoProfessorSGPCoorientador = orientador.Cpf;
                            }
                            else
                            {
                                alunoVeteranoSGPData.DocumentoProfessorSGPCoorientador = null;
                                alunoVeteranoSGPData.DocumentoProfessorSGPCoorientador = null;
                            }
                        }
                    }
                }

                // Chama a rotina do SGP
                IntegracaoAcademicoService.AtualizarAlunoVeteranoSGP(alunoVeteranoSGPData);

                // Atualiza a descrição
                var solicitacao = new SolicitacaoMatricula
                {
                    Seq = model.SeqSolicitacaoServico,
                    DescricaoAtualizada = SolicitacaoMatriculaItemDomainService.GerarDescricaoItensSolicitacao(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa, false, false)
                };
                UpdateFields(solicitacao, x => x.DescricaoAtualizada);

                /************************************************************************/
                // 4. ENVIA NOTIFICAÇÃO

                if (dados.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU)
                {
                    // Monta os dados para merge
                    Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dados.NomeSocialSolicitante) ? dados.NomeSolicitante : dados.NomeSocialSolicitante);

                    // Envia a notificação
                    var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                    {
                        SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                        TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.CONFIRMACAO_RENOVACAO_MATRICULA,
                        DadosMerge = dadosMerge,
                        EnvioSolicitante = true,
                        ConfiguracaoPrimeiraEtapa = false
                    };
                    SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                }
                else if (dados.TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA)
                {
                    // Monta os dados para merge
                    Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dados.NomeSocialSolicitante) ? dados.NomeSolicitante : dados.NomeSocialSolicitante);
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dados.DescricaoProcesso);

                    // Envia a notificação
                    var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                    {
                        SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                        TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.CONFIRMACAO_MATRICULA_REABERTURA,
                        DadosMerge = dadosMerge,
                        EnvioSolicitante = true,
                        ConfiguracaoPrimeiraEtapa = false
                    };
                    SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                }

                /************************************************************************/
                // 5. FINALIZA A ETAPA DA SOLICITAÇÃO

                // Procedimentos de finalização da etapa...
                SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);

                // Criar mensagem de encerramento da solicitação na linha do tempo da pessoa atuação
                // Chama após finalizar a etapa para pegar a descrição da situação final
                MensagemDomainService.EnviarMensagemLinhaDoTempoEncerramentoSolicitacao(model.SeqSolicitacaoServico, TOKEN_TIPO_MENSAGEM.ENCERRAMENTO_SOLICITACAO_RENOVACAO_REABERTURA);

                unityOfWork.Commit();
            }
        }

        public Aluno CriarAlunoPeloIngressante(long seqIngressante, long seqSolicitacaoMatricula, long? seqCurso)
        {
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano, // Validar.. não ta na documentação. chamar?
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero, // Validar.. não ta na documentação. chamar?
                DescricaoVinculo = (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.Descricao,
                SeqsTipoTermoIntercambio = (x.PessoaAtuacao as Ingressante).TermosIntercambio.Select(t => t.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio),
                Parcelas = x.GrupoEscalonamento.Itens.SelectMany(i => i.Parcelas),
            });

            // Recupera a instituição de ensino logada
            var seqInstituicaoEnsinoLogada = GetDataFilter(FILTER.INSTITUICAO_ENSINO).FirstOrDefault();

            // Recupera os dados do ingressante
            var dadosIngressante = IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), x => new
            {
                SeqCicloLetivo = x.CampanhaCicloLetivo.SeqCicloLetivo,
                DataAdmissao = x.DataAdmissao,
                DataPrevisaoConclusao = x.DataPrevisaoConclusao,
                SeqCursoOfertaLocalidadeTurno = x.Ofertas.FirstOrDefault().CampanhaOfertaItem.SeqCursoOfertaLocalidadeTurno,
                SeqCurso = (long?)x.Ofertas.FirstOrDefault().CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                DescricaoDocumentoConclusao = x.Ofertas.FirstOrDefault().CampanhaOfertaItem.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.DescricaoDocumentoConclusao,
                SeqEntidadeResponsavel = x.SeqEntidadeResponsavel,
                SeqNivelEnsino = x.SeqNivelEnsino,
                SeqPessoa = x.SeqPessoa,
                SeqPessoaDadosPessoais = x.SeqPessoaDadosPessoais,
                TipoAtuacao = x.TipoAtuacao,
                Descricao = x.Descricao,
                SeqMatrizCurricularOferta = x.SeqMatrizCurricularOferta,
                Orientacoes = x.OrientacoesPessoaAtuacao.Select(o => o.Orientacao).Select(o => new
                {
                    TokenTipoOrientacao = o.TipoOrientacao.Token,
                    Orientacao = o,
                    OrientacoesColaborador = o.OrientacoesColaborador
                }),
                SeqsFormacoesEspecificas = x.FormacoesEspecificas.Select(f => f.SeqFormacaoEspecifica),
                SeqTipoTermoIntercambio = (long?)x.TermosIntercambio.FirstOrDefault().SeqTermoIntercambio,
                SeqTipoVinculoAluno = x.SeqTipoVinculoAluno,
                SeqCampanhaOferta = x.Ofertas.FirstOrDefault().SeqCampanhaOferta,
                CondicoesObrigatoriedade = x.CondicoesObrigatoriedade,
                SeqFormaIngresso = x.SeqFormaIngresso,
                SeqInstituicaoTransferenciaExterna = x.SeqInstituicaoTransferenciaExterna,
                CursoTransferenciaExterna = x.CursoTransferenciaExterna,
                SeqPessoaAtuacaoTermoIntercambio = (long?)x.TermosIntercambio.FirstOrDefault(c => c.Ativo == true).Seq
            });

            // Recupera o endereço de correspondência do ingressante
            var seqPessoaEnderecoCorrespondenciaIngressante = PessoaAtuacaoEnderecoDomainService.SearchProjectionByKey(new PessoaAtuacaoEnderecoFilterSpecification { SeqPessoaAtuacao = seqIngressante, EnderecoCorrespondencia = true }, x => x.SeqPessoaEndereco);

            // Recupera os dados de endereço eletrônico
            var dadosEnderecosEletronico = PessoaEnderecoEletronicoDomainService.SearchProjectionBySpecification(new PessoaEnderecoEletronicoFilterSpecification { SeqPessoa = dadosIngressante.SeqPessoa }, x => new
            {
                SeqEnderecoEletronico = x.SeqEnderecoEletronico,
                Seq = x.Seq
            });

            // Recupera os dados de telefones
            var dadosTelefones = PessoaTelefoneDomainService.SearchProjectionBySpecification(new PessoaTelefoneFilterSpecification { SeqPessoa = dadosIngressante.SeqPessoa }, x => new
            {
                SeqTelefone = x.SeqTelefone,
                Seq = x.Seq
            });

            // Recupera os dados da referência familiar
            var dadosReferenciaFamiliar = ReferenciaFamiliarDomainService.SearchProjectionBySpecification(new ReferenciaFamiliarFilterSpecification { SeqPessoaAtuacao = seqIngressante }, x => new
            {
                //Referencia = x,
                Enderecos = x.Enderecos,
                EnderecosEletronicos = x.EnderecosEletronicos,
                Telefones = x.Telefones,
                NomeParente = x.NomeParente,
                TipoParentesco = x.TipoParentesco
            });

            // Recupera os dados dos itens matriculados
            var dadosItensMatriculados = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(new SolicitacaoMatriculaItemFilterSpecification { SeqSolicitacaoMatricula = seqSolicitacaoMatricula, ClassificacaoSituacaoFinal = ClassificacaoSituacaoFinal.FinalizadoComSucesso }, x => new
            {
                SeqConfiguracaoComponente = x.SeqConfiguracaoComponente,
                SeqDivisaoTurma = x.SeqDivisaoTurma,
                GeraOrientacao = (bool?)x.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.GeraOrientacao,
                SeqTipoOrientacao = (long?)x.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.SeqTipoOrientacao,
                TipoParticipacaoOrientacao = x.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.TipoParticipacaoOrientacao
            });

            // Recupera os dados de bloqueios da pessoa atuação
            var dadosBloqueios = PessoaAtuacaoBloqueioDomainService.SearchProjectionBySpecification(new PessoaAtuacaoBloqueioFilterSpecification { BloqueadoOuDesbloqueadoTemporariamente = true, SeqPessoaAtuacao = seqIngressante }, x => new
            {
                JustificativaDesbloqueio = x.JustificativaDesbloqueio,
                Observacao = x.Observacao,
                TipoDesbloqueio = x.TipoDesbloqueio,
                UsuarioDesbloqueioEfetivo = x.UsuarioDesbloqueioEfetivo,
                UsuarioDesbloqueioTemporario = x.UsuarioDesbloqueioTemporario,
                SeqMotivoBloqueio = x.SeqMotivoBloqueio,
                CadastroIntegracao = x.CadastroIntegracao,
                DataBloqueio = x.DataBloqueio,
                DataDesbloqueioEfetivo = x.DataDesbloqueioEfetivo,
                DataDesbloqueioTemporario = x.DataDesbloqueioTemporario,
                Descricao = x.Descricao,
                DescricaoReferenciaAtuacao = x.DescricaoReferenciaAtuacao,
                Itens = x.Itens.Where(ib => ib.SituacaoBloqueio == SituacaoBloqueio.Bloqueado),
                Comprovantes = x.Comprovantes,
                SituacaoBloqueio = x.SituacaoBloqueio
            });

            // Recupera os dados de termo intercâmbio da pessoa atuação            
            var dadosTermosIntercambio = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(new PessoaAtuacaoTermoIntercambioFilterSpecification { SeqPessoaAtuacao = seqIngressante }, x => new
            {
                SeqPessoaAtuacaoTermoIntercambio = x.Seq,
                SeqTermoIntercambio = x.SeqTermoIntercambio,
                SeqTipoTermoIntercambio = x.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
                Arquivos = x.Arquivos,
                Orientacao = x.Orientacao,
                OrientacoesColaborador = x.Orientacao.OrientacoesColaborador,
                TipoMobilidade = x.TipoMobilidade,
                Periodos = x.Periodos.Select(p => new
                {
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim
                })
            }).ToList();

            // Recupera os dados de benefícios da pessoa atuação
            var dadosBeneficio = PessoaAtuacaoBeneficioDomainService.SearchProjectionBySpecification(new PessoaAtuacaoBeneficioFilterSpecification { SeqPessoaAtuacao = seqIngressante }, x => new
            {
                DataFimVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia,
                DataInicioVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia,
                FormaDeducao = x.FormaDeducao,
                IncideParcelaMatricula = x.IncideParcelaMatricula,
                SeqBeneficio = x.SeqBeneficio,
                SeqConfiguracaoBeneficio = x.SeqConfiguracaoBeneficio,
                // Bug 24582 - Ao criar o benefício do aluno a partir do benefício do ingressante não copiar o campo seq_contrato_beneficio_financeiro, pois, ele se refere apenas ao ingressante.
                //SeqContratoBeneficioFinanceiro = x.SeqContratoBeneficioFinanceiro,
                SituacaoChancelaBeneficio = x.HistoricoSituacoes.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault().SituacaoChancelaBeneficio,
                HistoricoVigencias = x.HistoricoVigencias,
                HiistoricoSituacoes = x.HistoricoSituacoes,
                ValorBeneficio = x.ValorBeneficio,
                ExibeValoresTermoAdesao = x.ExibeValoresTermoAdesao,
                ResponsaveisFinanceiro = x.ResponsaveisFinanceiro.Select(r => new
                {
                    SeqPessoaJuridica = r.SeqPessoaJuridica,
                    ValorPercentual = r.ValorPercentual,
                }),
                //Copiar arquivos associados ao beneficio
                x.ArquivosAnexo
            });

            // Recupera a parametrização de instituição x nivel x tipo de vinculo do aluno
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(seqInstituicaoEnsinoLogada, dadosIngressante.SeqNivelEnsino, dadosIngressante.SeqTipoVinculoAluno, IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao | IncludesInstituicaoNivelTipoVinculoAluno.TiposOrientacao_TipoOrientacao);

            // Recupera o sequencial da situação de matriculado
            var seqSituacaoMatriculado = SituacaoMatriculaDomainService.BuscarSituacaoMatriculaPorToken(TOKENS_SITUACAO_MATRICULA.MATRICULADO);

            // Copia os endereços do ingressante para o aluno
            var enderecoAluno = new PessoaAtuacaoEndereco
            {
                SeqPessoaEndereco = seqPessoaEnderecoCorrespondenciaIngressante,
                EnderecoCorrespondencia = EnderecoCorrespondencia.AcademicaFinanceira,
            };

            // Copia os telefones do ingressante para o aluno
            var telefones = dadosTelefones.Select(t => new PessoaTelefone
            {
                SeqTelefone = t.SeqTelefone,
                SeqPessoa = dadosIngressante.SeqPessoa,
                Seq = t.Seq
            });

            // Copia os endereços eletrônicos do ingressante para o aluno
            var emails = dadosEnderecosEletronico.Select(t => new PessoaEnderecoEletronico
            {
                SeqEnderecoEletronico = t.SeqEnderecoEletronico,
                SeqPessoa = dadosIngressante.SeqPessoa,
                Seq = t.Seq
            });

            // Copia os dados de referencia familiar do ingressante para o aluno
            var referenciaFamiliar = dadosReferenciaFamiliar.Select(t => new ReferenciaFamiliar
            {
                Enderecos = t.Enderecos,
                EnderecosEletronicos = t.EnderecosEletronicos,
                NomeParente = t.NomeParente,
                Telefones = t.Telefones,
                TipoParentesco = t.TipoParentesco
            });

            // Busca a data do período letivo para criar a orientação base para as orientações de turma
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqIngressante);
            DatasEventoLetivoVO dataPeriodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosIngressante.SeqCicloLetivo, dadosOrigem.SeqCursoOfertaLocalidadeTurno, TipoAluno.Calouro, TOKEN_TIPO_EVENTO.PERIODO_LETIVO);

            // Orientações base do ingressante
            var orientacoesBase = new OrientacaoVO()
            {
                SeqEntidadeInstituicao = seqInstituicaoEnsinoLogada,
                SeqNivelEnsino = dadosIngressante.SeqNivelEnsino,
                SeqTipoVinculoAluno = dadosIngressante.SeqTipoVinculoAluno,
                OrientacoesColaborador = dadosIngressante.Orientacoes.FirstOrDefault(o => o.TokenTipoOrientacao == TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO)
                                                        ?.OrientacoesColaborador?.Select(oc => new OrientacaoColaboradorVO
                                                        {
                                                            DataFimOrientacao = dataPeriodoLetivo.DataFim,
                                                            DataInicioOrientacao = dataPeriodoLetivo.DataInicio,
                                                            SeqColaborador = oc.SeqColaborador,
                                                            SeqInstituicaoExterna = oc.SeqInstituicaoExterna,
                                                            TipoParticipacaoOrientacao = oc.TipoParticipacaoOrientacao,
                                                        }).ToList(),
            };

            var titulacaoFormacao = CursoFormacaoEspecificaTitulacaoDomainService.BuscarTitulacaoFormacaoPorCursoFormacao(dadosIngressante.SeqCurso.GetValueOrDefault(), dadosIngressante.SeqsFormacoesEspecificas.ToList());

            // Criação do histórico do aluno
            var alunoHistorico = new AlunoHistorico
            {
                Atual = true,
                SeqCicloLetivo = dadosIngressante.SeqCicloLetivo,
                DataAdmissao = dadosIngressante.DataAdmissao,
                SeqIngressante = seqIngressante,
                SeqCursoOfertaLocalidadeTurno = dadosIngressante.SeqCursoOfertaLocalidadeTurno,
                SeqEntidadeVinculo = dadosIngressante.SeqEntidadeResponsavel.GetValueOrDefault(),
                SeqNivelEnsino = dadosIngressante.SeqNivelEnsino,
                SeqSolicitacaoServico = seqSolicitacaoMatricula,
                SeqFormaIngresso = dadosIngressante.SeqFormaIngresso,
                HistoricosCicloLetivo = new List<AlunoHistoricoCicloLetivo>
                {
                    new AlunoHistoricoCicloLetivo
                    {
                        SeqCicloLetivo = dadosIngressante.SeqCicloLetivo,
                        SeqSolicitacaoServico = seqSolicitacaoMatricula,
                        TipoAluno = TipoAluno.Calouro,
                        AlunoHistoricoSituacao = new  List<AlunoHistoricoSituacao>
                        {
                            new AlunoHistoricoSituacao
                            {
                                SeqSolicitacaoServico = seqSolicitacaoMatricula,
                                SeqSituacaoMatricula = seqSituacaoMatriculado,
                                DataInicioSituacao = DateTime.Now,
                                Observacao = "Efetivação da matrícula"
                            }
                        },
                        PlanosEstudo = new List<PlanoEstudo> {
                            new PlanoEstudo
                            {
                                Itens = dadosItensMatriculados.Select(i => new PlanoEstudoItem
                                {
                                    SeqConfiguracaoComponente = i.SeqConfiguracaoComponente,
                                    SeqDivisaoTurma = i.SeqDivisaoTurma,
                                    Orientacao = OrientacaoDomainService.CriarOrientacaoPlanoEstudoItem(i.GeraOrientacao.GetValueOrDefault(), i.SeqTipoOrientacao.GetValueOrDefault(), i.TipoParticipacaoOrientacao.GetValueOrDefault(), orientacoesBase),
                                }).ToList(),
                                SeqSolicitacaoServico = seqSolicitacaoMatricula,
                                SeqMatrizCurricularOferta = dadosIngressante.SeqMatrizCurricularOferta,
                                Atual = true,
                            }
                        }
                    }
                },
                Formacoes = dadosIngressante.SeqsFormacoesEspecificas.Select(f =>
                    new AlunoFormacao
                    {
                        SeqFormacaoEspecifica = f,
                        DataInicio = dadosIngressante.DataAdmissao,
                        SeqTitulacao = titulacaoFormacao.Any(t => t.SeqFormacaoEspecifica == f) ? titulacaoFormacao.First(t => t.SeqFormacaoEspecifica == f).SeqTitulacao : (long?)null,
                        DescricaoDocumentoConclusao = dadosIngressante.DescricaoDocumentoConclusao,
                        ApuracoesFormacao = new List<ApuracaoFormacao>
                        {
                            new ApuracaoFormacao
                            {
                                SituacaoAlunoFormacao = SituacaoAlunoFormacao.AConcluir,
                                DataInicio = dadosIngressante.DataAdmissao
                            }
                        }
                    }).ToList(),
                PrevisoesConclusao = new List<AlunoHistoricoPrevisaoConclusao>
                {
                    new AlunoHistoricoPrevisaoConclusao
                    {
                        DataLimiteConclusao = dadosIngressante.DataPrevisaoConclusao.GetValueOrDefault(),
                        DataPrevisaoConclusao = dadosIngressante.DataPrevisaoConclusao.GetValueOrDefault(),
                        SeqSolicitacaoServico = seqSolicitacaoMatricula
                    }
                }
            };

            // Inclui as orientações sem termo para o aluno
            var orientacoesPessoaAtuacao = dadosIngressante.Orientacoes.Where(o => !o.Orientacao.SeqTipoTermoIntercambio.HasValue).Select(o => new OrientacaoPessoaAtuacao
            {
                Orientacao = new Orientacao
                {
                    SeqEntidadeInstituicao = seqInstituicaoEnsinoLogada,
                    SeqNivelEnsino = o.Orientacao.SeqNivelEnsino,
                    SeqTipoOrientacao = o.Orientacao.SeqTipoOrientacao,
                    SeqTipoVinculoAluno = dadosIngressante.SeqTipoVinculoAluno,
                    OrientacoesColaborador = o.OrientacoesColaborador.Select(c => new OrientacaoColaborador
                    {
                        DataFimOrientacao = c.DataFimOrientacao,
                        DataInicioOrientacao = dadosIngressante.DataAdmissao,
                        SeqColaborador = c.SeqColaborador,
                        SeqInstituicaoExterna = c.SeqInstituicaoExterna,
                        TipoParticipacaoOrientacao = c.TipoParticipacaoOrientacao,
                    }).ToList()
                }
            }).ToList();

            // Duplica a pessoa atuação de ingressante para aluno
            bool termoConcedeFormacao = false;
            var aluno = new Aluno()
            {
                SeqPessoa = dadosIngressante.SeqPessoa,
                SeqPessoaDadosPessoais = dadosIngressante.SeqPessoaDadosPessoais,
                TipoAtuacao = TipoAtuacao.Aluno,
                Ativo = true,
                SeqTipoVinculoAluno = dadosIngressante.SeqTipoVinculoAluno,
                Descricao = dadosIngressante.Descricao,
                Enderecos = new List<PessoaAtuacaoEndereco> { enderecoAluno },
                Telefones = telefones.ToList(),
                EnderecosEletronicos = emails.ToList(),
                ReferenciasFamiliar = referenciaFamiliar.ToList(),
                Historicos = new List<AlunoHistorico> { alunoHistorico },
                OrientacoesPessoaAtuacao = orientacoesPessoaAtuacao,
                SeqInstituicaoTransferenciaExterna = dadosIngressante.SeqInstituicaoTransferenciaExterna,
                CursoTransferenciaExterna = dadosIngressante.CursoTransferenciaExterna,
                Beneficios = dadosBeneficio.Select(x => new PessoaAtuacaoBeneficio
                {
                    // FIX: Carol - Historico Vigencia
                    //DataFimVigencia = x.DataFimVigencia,
                    //DataInicioVigencia = x.DataInicioVigencia,

                    FormaDeducao = x.FormaDeducao,
                    IncideParcelaMatricula = x.IncideParcelaMatricula,
                    SeqBeneficio = x.SeqBeneficio,
                    SeqConfiguracaoBeneficio = x.SeqConfiguracaoBeneficio,
                    // Bug 24582 - Ao criar o benefício do aluno a partir do benefício do ingressante não copiar o campo seq_contrato_beneficio_financeiro, pois, ele se refere apenas ao ingressante.
                    //SeqContratoBeneficioFinanceiro = x.SeqContratoBeneficioFinanceiro,
                    HistoricoSituacoes = x.HiistoricoSituacoes.Select(xh => new BeneficioHistoricoSituacao
                    {
                        DataInicioSituacao = xh.DataInicioSituacao,
                        Observacao = xh.Observacao,
                        SituacaoChancelaBeneficio = xh.SituacaoChancelaBeneficio,
                    }).ToList(),
                    HistoricoVigencias = x.HistoricoVigencias.Select(xv => new BeneficioHistoricoVigencia
                    {
                        DataFimVigencia = xv.DataFimVigencia,
                        DataInicioVigencia = xv.DataInicioVigencia,
                        MotivoAlteracaoBeneficio = xv.MotivoAlteracaoBeneficio,
                        Observacao = xv.Observacao,
                        SeqMotivoAlteracaoBeneficio = xv.SeqMotivoAlteracaoBeneficio
                    }).ToList(),
                    //SituacaoChancelaBeneficio = x.SituacaoChancelaBeneficio,
                    ValorBeneficio = x.ValorBeneficio,
                    ArquivosAnexo = x.ArquivosAnexo.Select(aa => new PessoaAtuacaoBeneficioAnexo
                    {
                        SeqArquivoAnexado = aa.SeqArquivoAnexado,
                        Observacao = aa.Observacao
                    }).ToList(),
                    ResponsaveisFinanceiro = x.ResponsaveisFinanceiro.Select(r => new PessoaAtuacaoBeneficioResponsavelFinanceiro
                    {
                        SeqPessoaJuridica = r.SeqPessoaJuridica,
                        ValorPercentual = r.ValorPercentual,
                    }).ToList(),
                    ExibeValoresTermoAdesao = x.ExibeValoresTermoAdesao
                }).ToList(),
                Bloqueios = dadosBloqueios.Select(b => new PessoaAtuacaoBloqueio
                {
                    JustificativaDesbloqueio = b.JustificativaDesbloqueio,
                    Observacao = b.Observacao,
                    TipoDesbloqueio = b.TipoDesbloqueio,
                    UsuarioDesbloqueioEfetivo = b.UsuarioDesbloqueioEfetivo,
                    UsuarioDesbloqueioTemporario = b.UsuarioDesbloqueioTemporario,
                    SeqSolicitacaoServico = seqSolicitacaoMatricula,
                    SeqMotivoBloqueio = b.SeqMotivoBloqueio,
                    CadastroIntegracao = b.CadastroIntegracao,
                    DataBloqueio = b.DataBloqueio,
                    DataDesbloqueioEfetivo = b.DataDesbloqueioEfetivo,
                    DataDesbloqueioTemporario = b.DataDesbloqueioTemporario,
                    Descricao = b.Descricao,
                    DescricaoReferenciaAtuacao = b.DescricaoReferenciaAtuacao,
                    Itens = b.Itens.Select(i => new PessoaAtuacaoBloqueioItem
                    {
                        CodigoIntegracaoSistemaOrigem = i.CodigoIntegracaoSistemaOrigem,
                        DataDesbloqueio = i.DataDesbloqueio,
                        Descricao = i.Descricao,
                        SituacaoBloqueio = i.SituacaoBloqueio,
                        UsuarioDesbloqueio = i.UsuarioDesbloqueio,
                    }).ToList(),
                    Comprovantes = b.Comprovantes.Select(c => new PessoaAtuacaoBloqueioComprovante
                    {
                        SeqArquivoAnexado = c.SeqArquivoAnexado,
                        Descricao = c.Descricao,
                    }).ToList(),
                    SituacaoBloqueio = b.SituacaoBloqueio
                }).ToList(),
                CondicoesObrigatoriedade = dadosIngressante.CondicoesObrigatoriedade.Select(c => new PessoaAtuacaoCondicaoObrigatoriedade
                {
                    Ativo = c.Ativo,
                    SeqCondicaoObrigatoriedade = c.SeqCondicaoObrigatoriedade
                }).ToList(),
                TermosIntercambio = dadosTermosIntercambio.Select(t =>
                {
                    var concedeFormacao = TipoTermoIntercambioDomainService.TipoTermoIntercambioConcedeFormacao(t.SeqTipoTermoIntercambio, dadosIngressante.SeqTipoVinculoAluno, dadosIngressante.SeqNivelEnsino, seqInstituicaoEnsinoLogada);
                    termoConcedeFormacao = termoConcedeFormacao || concedeFormacao;

                    // Atualiza os dados do termo de intercambio do ingressante pra setar ativo = false (Task 27534)
                    PessoaAtuacaoTermoIntercambioDomainService.UpdateFields(new PessoaAtuacaoTermoIntercambio { Seq = t.SeqPessoaAtuacaoTermoIntercambio, Ativo = false }, x => x.Ativo);

                    return new PessoaAtuacaoTermoIntercambio
                    {
                        SeqTermoIntercambio = t.SeqTermoIntercambio,
                        Ativo = true,
                        TipoMobilidade = t.TipoMobilidade,
                        Arquivos = t.Arquivos.Select(a => new PessoaAtuacaoTermoIntercambioArquivo
                        {
                            SeqArquivoAnexado = a.SeqArquivoAnexado,
                        }).ToList(),
                        Orientacao = new Orientacao
                        {
                            OrientacoesColaborador = t.OrientacoesColaborador.Select(oc => new OrientacaoColaborador
                            {
                                DataFimOrientacao = dadosIngressante.DataPrevisaoConclusao,
                                DataInicioOrientacao = dadosIngressante.DataAdmissao,
                                SeqColaborador = oc.SeqColaborador,
                                SeqInstituicaoExterna = oc.SeqInstituicaoExterna,
                                TipoParticipacaoOrientacao = oc.TipoParticipacaoOrientacao,
                            }).ToList(),
                            SeqEntidadeInstituicao = seqInstituicaoEnsinoLogada,
                            SeqNivelEnsino = t.Orientacao.SeqNivelEnsino,
                            SeqOrigemMaterial = t.Orientacao.SeqOrigemMaterial,
                            SeqTipoOrientacao = t.Orientacao.SeqTipoOrientacao,
                            SeqTipoTermoIntercambio = t.Orientacao.SeqTipoTermoIntercambio,
                            SeqTipoVinculoAluno = t.Orientacao.SeqTipoVinculoAluno,
                        },
                        Periodos = t.Periodos.Select(p => new PeriodoIntercambio
                        {
                            DataInicio = p.DataInicio,
                            DataFim = p.DataFim,
                        }).ToList()
                    };
                }).ToList()
            };

            // Adiciono as orientações de turma na lista de orientações do aluno
            var orientacoesTurma = alunoHistorico.HistoricosCicloLetivo.SelectMany(h => h.PlanosEstudo.SelectMany(p => p.Itens.Select(i =>
            {
                var ret = new OrientacaoPessoaAtuacao
                {
                    Orientacao = i.Orientacao
                };
                return ret;
            }))).Where(o => o.Orientacao != null);
            (aluno.OrientacoesPessoaAtuacao as List<OrientacaoPessoaAtuacao>).AddRange(orientacoesTurma);

            // Adiciono as orientações de termo de intercambio na lista de orientações do aluno
            var orientacoesTermo = aluno.TermosIntercambio.Select(t =>
            {
                var ret = new OrientacaoPessoaAtuacao
                {
                    Orientacao = t.Orientacao
                };
                return ret;
            }).Where(o => o.Orientacao != null);
            (aluno.OrientacoesPessoaAtuacao as List<OrientacaoPessoaAtuacao>).AddRange(orientacoesTermo);

            // Inicia as listas
            aluno.Bloqueios = aluno.Bloqueios ?? new List<PessoaAtuacaoBloqueio>();
            aluno.OrientacoesPessoaAtuacao = aluno.OrientacoesPessoaAtuacao ?? new List<OrientacaoPessoaAtuacao>();

            /*RN_MAT_094 - Inclusão bloqueio orientação pendente
                Verificar se a pessoa-atuação do tipo aluno criada, possui orientação com o todos os tipos de
                orientação que estão parametrizadas por instituição-nível-vínculo-tipo orientação para ser exigido para
                aluno. Se a pessoa-atuação possuir termo de intercâmbio, a conferência deve considerar o tipo de termo do termo associado.
                Caso não possua, incluir bloqueio acadêmico para a pessoa-atuação, de acordo com os parâmetros:
                - Motivo bloqueio: ORIENTACAO_PENDENTE
                - Situação: "Bloqueado"
                - Data do bloqueio: data atual do sistema
                - Data da última atualização: data corrente do sistema
                - Indicador cadastro de integração: Sim
                - Descrição bloqueio referência: descrição do vínculo institucional da pessoa-atuação em questão, considerando a RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
                - Descrição bloqueio: "Cadastro de orientação pendente"
                - Sequencial da solicitação: sequencial da solicitação de matrícula do ingressante, referente ao aluno que acabou de ser criado.
                - Demais campos: nulos

                1. Incluir item de bloqueio para cada tipo de orientação que é exigido na orientação para aluno:
                - Descrição item: Tipo de orientação exigida para o aluno
                - Situação: "Bloqueado"
                - Item integração origem: sequencial do tipo de orientação que está parametrizado para ser exigido para o aluno
                - Demais campos: nulos*/

            // Cria lista de itens de bloqueios para fazer a validação
            var itensBloqueio = new List<PessoaAtuacaoBloqueioItem>();

            // Verifica se possui todos os tipos de orientação do tipo de termo de intercâmbio caso tenha associado tipo de termo
            // de intercâmbio ao ingressante
            if (instituicaoNivelTipoVinculoAluno.TiposTermoIntercambio != null)
            {
                var tipoTermoIntercambio = instituicaoNivelTipoVinculoAluno.TiposTermoIntercambio.FirstOrDefault(i => i.SeqTipoTermoIntercambio == dadosIngressante.SeqTipoTermoIntercambio);
                if (tipoTermoIntercambio != null)
                {
                    foreach (var item in tipoTermoIntercambio.TiposOrientacao.Where(t => t.CadastroOrientacaoAluno == CadastroOrientacao.Exige))
                    {
                        if (!aluno.OrientacoesPessoaAtuacao.Any(o => o.Orientacao.SeqTipoOrientacao == item.SeqTipoOrientacao) && !itensBloqueio.Any(i => i.CodigoIntegracaoSistemaOrigem == item.SeqTipoOrientacao.ToString()))
                        {
                            itensBloqueio.Add(new PessoaAtuacaoBloqueioItem
                            {
                                Descricao = item.TipoOrientacao.Descricao,
                                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                                CodigoIntegracaoSistemaOrigem = item.SeqTipoOrientacao.ToString(),
                            });
                        }
                    }
                }
            }

            // Verifica os tipos de orientação que não são de termos de intercâmbio
            if (instituicaoNivelTipoVinculoAluno.TiposOrientacao != null)
            {
                foreach (var item in instituicaoNivelTipoVinculoAluno.TiposOrientacao.Where(t => t.CadastroOrientacaoAluno == CadastroOrientacao.Exige && !t.SeqInstituicaoNivelTipoTermoIntercambio.HasValue))
                {
                    if (!aluno.OrientacoesPessoaAtuacao.Any(o => o.Orientacao.SeqTipoOrientacao == item.SeqTipoOrientacao && !itensBloqueio.Any(i => i.CodigoIntegracaoSistemaOrigem == item.SeqTipoOrientacao.ToString())))
                    {
                        itensBloqueio.Add(new PessoaAtuacaoBloqueioItem
                        {
                            Descricao = item.TipoOrientacao.Descricao,
                            SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                            CodigoIntegracaoSistemaOrigem = item.SeqTipoOrientacao.ToString(),
                        });
                    }
                }
            }

            // Caso tenha algum item, cria o bloqueio
            if (itensBloqueio.Any())
            {
                // Recupera o motivo de bloqueio de token ORIENTACAO_PENDENTE
                var seqMotivoOrientacaoPendente = MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.ORIENTACAO_PENDENTE);

                var dadosBloqueio = new PessoaAtuacaoBloqueio
                {
                    SeqMotivoBloqueio = seqMotivoOrientacaoPendente,
                    SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                    DataBloqueio = DateTime.Now,
                    CadastroIntegracao = true,
                    DescricaoReferenciaAtuacao = dadosIngressante.Descricao, // PessoaAtuacaoDomainService.RecuperarDescricaoVinculo(instituicaoNivelTipoVinculoAluno, dadosSolicitacao.SeqsTipoTermoIntercambio, dadosSolicitacao.DescricaoVinculo),
                    Descricao = "Cadastro de orientação pendente",
                    SeqSolicitacaoServico = seqSolicitacaoMatricula,
                    Itens = itensBloqueio
                };
                aluno.Bloqueios.Add(dadosBloqueio);
            }

            /*RN_MAT_095 - Inclusão bloqueio formação específica pendente
                Verificar se a pessoa-atuação do tipo aluno criada, possui formações específicas associadas dos tipos
                parametrizados para o tipo de pessoa atuação "Aluno", no parâmetro de tipo de formação específica por
                tipo de entidade. Caso não possua, incluir bloqueio acadêmico para a pessoa-atuação, de acordo com os parâmetros:
                - Motivo bloqueio: FORMACAO_ESPECIFICA_PENDENTE
                - Situação: "Bloqueado"
                - Data do bloqueio: data atual do sistema
                - Data da última atualização: data corrente do sistema
                - Indicador cadastro de integração: Sim
                - Descrição bloqueio referência: descrição do vínculo institucional da pessoa-atuação em questão, considerando a RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
                - Descrição bloqueio: "Associação de formação específica pendente"
                - Sequencial da solicitação: sequencial da solicitação de matrícula do ingressante, referente ao aluno que acabou de ser criado.
                - Demais campos: nulos*/
            // Segundo a Jéssica, verificar se tem termo de intercâmbio e se ele e se o tipo de termo concede formacao.
            // se nao tiver termo, olhar se o vinculo concede formação
            // tudo no parametro de instituicao nivel tipo de vinculo
            if (termoConcedeFormacao || instituicaoNivelTipoVinculoAluno.ConcedeFormacao)
            {
                var formacoesAtendidas = InstituicaoTipoEntidadeFormacaoEspecificaDomainService.ValidarObrigatoriedadeFormacoesPorCurso(dadosIngressante.SeqCurso.GetValueOrDefault(), dadosIngressante.SeqsFormacoesEspecificas, true);
                if (!formacoesAtendidas)
                {
                    var seqMotivoFormacaoEspecificaPendente = MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.FORMACAO_ESPECIFICA_PENDENTE);
                    var dadosBloqueio = new PessoaAtuacaoBloqueio
                    {
                        SeqMotivoBloqueio = seqMotivoFormacaoEspecificaPendente,
                        SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                        DataBloqueio = DateTime.Now,
                        CadastroIntegracao = true,
                        DescricaoReferenciaAtuacao = dadosIngressante.Descricao,// PessoaAtuacaoDomainService.RecuperarDescricaoVinculo(instituicaoNivelTipoVinculoAluno, dadosSolicitacao.SeqsTipoTermoIntercambio, dadosSolicitacao.DescricaoVinculo),
                        Descricao = "Associação de formação específica pendente",
                        SeqSolicitacaoServico = seqSolicitacaoMatricula,
                    };
                    aluno.Bloqueios.Add(dadosBloqueio);
                }
            }

            /*RN_MAT_096 - Inclusão bloqueio prazo de conclusão encerrado
                Incluir bloqueio acadêmico para a pessoa atuação do tipo "Aluno" criada, de conforme:
                - Motivo bloqueio: PRAZO_CONCLUSAO_CURSO_ENCERRADO
                - Situação: "Bloqueado"
                - Data do bloqueio: data prevista de conclusão do curso
                - Data da última atualização: data corrente do sistema
                - Indicador cadastro de integração: Sim
                - Descrição bloqueio referência: descrição do vínculo institucional da pessoa-atuação em questão, considerando a RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
                - Descrição bloqueio: "Prazo de conclusão de curso encerrado."
                - Sequencial da solicitação: sequencial da solicitação de matrícula do ingressante, referente ao aluno que acabou de ser criado.
                - Demais campos: nulos*/
            // Caso exige curso, cria um bloqueio de prazo de conclusão com início de bloqueio futuro
            if (instituicaoNivelTipoVinculoAluno.ConcedeFormacao || termoConcedeFormacao)
            {
                var seqMotivoPrazoConclusaoEncerrado = MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.PRAZO_CONCLUSAO_CURSO_ENCERRADO);
                var dadosBloqueio = new PessoaAtuacaoBloqueio
                {
                    SeqMotivoBloqueio = seqMotivoPrazoConclusaoEncerrado,
                    SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                    DataBloqueio = dadosIngressante.DataPrevisaoConclusao.GetValueOrDefault().AddDays(1),
                    CadastroIntegracao = true,
                    DescricaoReferenciaAtuacao = dadosIngressante.Descricao,// PessoaAtuacaoDomainService.RecuperarDescricaoVinculo(instituicaoNivelTipoVinculoAluno, dadosSolicitacao.SeqsTipoTermoIntercambio, dadosSolicitacao.DescricaoVinculo),
                    Descricao = "Prazo de conclusão de curso encerrado",
                    SeqSolicitacaoServico = seqSolicitacaoMatricula,
                };
                aluno.Bloqueios.Add(dadosBloqueio);
            }

            /// ATRIBUIR A SITUAÇÃO DE MATRÍCULA FUTURA E INATIVA PARA ALUNOS DE DISCIPLINA ISOLADA E ALUNOS DE INTERCÂMBIO
            /// Se a pessoa-atuação em questão for de um vínculo configurado por instituição-nível de ensino-vínculo de acordo
            /// com a instituição de ensino logada, nível de ensino e vínculo da pessoa-atuação para não exigir curso, incluir
            /// a situação de matrícula conforme:
            ///  - Situação = TERMINO_DISCIPLINA_ISOLADA
            ///  - Solicitação de serviço = solicitação de serviço que está sendo efetivada
            ///  - Data de inicio da situação = data de previsão de conclusão mais um dia
            ///  - Observação = “Efetivação da matrícula”
            if (!instituicaoNivelTipoVinculoAluno.ExigeCurso)
            {
                // Recupera o sequencial da situação TERMINO_DISCIPLINA_ISOLADA
                var seqSituacaoTerminoDI = SituacaoMatriculaDomainService.BuscarSituacaoMatriculaPorToken(TOKENS_SITUACAO_MATRICULA.TERMINO_DISCIPLINA_ISOLADA);

                // Inclui a situação no objeto aluno
                AlunoHistoricoSituacao sitTerminoDI = new AlunoHistoricoSituacao()
                {
                    SeqSolicitacaoServico = seqSolicitacaoMatricula,
                    SeqSituacaoMatricula = seqSituacaoTerminoDI,
                    DataInicioSituacao = dadosIngressante.DataPrevisaoConclusao.Value.AddDays(1),
                    Observacao = "Efetivação da matrícula"
                };
                aluno.Historicos.FirstOrDefault().HistoricosCicloLetivo.FirstOrDefault().AlunoHistoricoSituacao.Add(sitTerminoDI);
            }

            /// Se a pessoa-atuação em questão for de um vínculo configurado por instituição-nível de ensino-vínculo de acordo com a
            /// instituição de ensino logada, nível de ensino e vínculo da pessoa-atuação para exigir parceria de intercâmbio no ingresso,
            /// o tipo de termo está parametrizado para não conceder formação e a data de previsão de conclusão esta dentro do ciclo
            /// letivo do processo, incluir a situação de matrícula conforme:
            ///  - Situação = TERMINO_INTERCAMBIO
            ///  - Solicitação de serviço = solicitação de serviço que está sendo efetivada
            ///  - Data de inicio da situação = data de previsão de conclusão mais um dia
            ///  - Observação = “Efetivação da matrícula”
            if (instituicaoNivelTipoVinculoAluno.ExigeParceriaIntercambioIngresso && !termoConcedeFormacao)
            {
                // Busca a data de fim do intercambio
                var dataFimIntercambio = dadosTermosIntercambio.FirstOrDefault().Periodos.FirstOrDefault().DataFim;

                // Busca o período do ciclo letivo
                var datasCicloLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosIngressante.SeqCicloLetivo, dadosIngressante.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(), TipoAluno.Calouro, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                if (dataFimIntercambio >= datasCicloLetivo.DataInicio && dataFimIntercambio <= datasCicloLetivo.DataFim)
                {
                    // Recupera o sequencial da situação TERMINO_INTERCAMBIO
                    var seqSituacaoTerminoIntercambio = SituacaoMatriculaDomainService.BuscarSituacaoMatriculaPorToken(TOKENS_SITUACAO_MATRICULA.TERMINO_INTERCAMBIO);

                    // Inclui a situação no objeto aluno
                    AlunoHistoricoSituacao sitTerminoIntercambio = new AlunoHistoricoSituacao()
                    {
                        SeqSolicitacaoServico = seqSolicitacaoMatricula,
                        SeqSituacaoMatricula = seqSituacaoTerminoIntercambio,
                        DataInicioSituacao = dadosIngressante.DataPrevisaoConclusao.Value.AddDays(1),
                        Observacao = "Efetivação da matrícula"
                    };
                    aluno.Historicos.FirstOrDefault().HistoricosCicloLetivo.FirstOrDefault().AlunoHistoricoSituacao.Add(sitTerminoIntercambio);
                }
            }

            return aluno;
        }

        /// <summary>
        /// Efetivar a matricula de forma automatica
        /// </summary>
        /// <param name="filtro">Fitlro com o sequencial do historico de agendamento para o feedack</param>
        public void EfetivarRenovacaoMatriculaAutomatica(EfetivarRenovacaoMatriculaAutomaticaSATVO filtro)
        {
            var servico = Create<Jobs.EfetivarRenovacaoMatriculaAutomaticaWebJob>();
            servico.Execute(filtro);
        }


        /// <summary>
        /// Realizar a efetivação da rematricula atualizando o histórico do aluno e os dados do SGP
        /// </summary>
        /// <param name="model">Dados da solicitação para efetivação</param>
        public void EfetivarRenovacaoMatriculaResidenciaMedica(EfetivacaoMatriculaVO model)
        {
            // Busca os dados da solicitação
            var spec = new SMCSeqSpecification<SolicitacaoMatricula>(model.SeqSolicitacaoServico);
            var dados = this.SearchProjectionByKey(spec, x => new
            {
                // Dados do aluno
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                CodigoAlunoSGP = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                AlunoHistorico = (x.PessoaAtuacao as Aluno).Historicos.Select(h => new
                {
                    Atual = h.Atual,
                    SeqAlunoHistorico = h.Seq,
                    SeqNivelEnsino = h.SeqNivelEnsino,
                    SeqCursoOfertaLocalidadeTurno = h.SeqCursoOfertaLocalidadeTurno,
                    DataPrevisaoConclusao = h.PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataPrevisaoConclusao,
                    DataLimiteConclusao = h.PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataLimiteConclusao,
                    SeqFormacaoEspecifica = (long?)h.Formacoes.FirstOrDefault(fe => fe.FormacaoEspecifica.TipoFormacaoEspecifica.Token != TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).SeqFormacaoEspecifica,
                }).FirstOrDefault(ah => ah.Atual),
                SeqInstituicaoEnsino = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
                SeqTipoVinculoAluno = (x.PessoaAtuacao as Aluno).SeqTipoVinculoAluno,

                // Dados do processo
                SeqCicloLetivoProcesso = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                AnoCicloLetivoProcesso = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloLetivoProcesso = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                PercentualServicoAdicional = x.ConfiguracaoProcesso.Processo.ValorPercentualServicoAdicional,

                // Dados da solicitação
                Turmas = x.Itens.Where(i => i.HistoricosSituacao.OrderByDescending(h => h.Seq)
                                .FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                                .Select(t => new
                                {
                                    SeqComponenteCurricular = t.ConfiguracaoComponente.SeqComponenteCurricular,
                                    SeqTurma = (long?)t.DivisaoTurma.SeqTurma,
                                    Creditos = t.ConfiguracaoComponente.ComponenteCurricular.Credito,
                                    SeqConfiguracaoComponente = t.SeqConfiguracaoComponente,
                                    SeqDivisaoTurma = t.SeqDivisaoTurma
                                }),
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao
            });

            // Se a solicitação é de um processo que não possui ciclo letivo, erro
            if (!dados.SeqCicloLetivoProcesso.HasValue)
                throw new ProcessoSemCicloLetivoException();

            // Verificar se na solicitação tem algum componente já aprovado/dispensado (validar apenas as turmas)
            var componentesSol = dados.Turmas.Where(i => i.SeqDivisaoTurma.HasValue).Select(i => (i.SeqConfiguracaoComponente, i.SeqDivisaoTurma)).ToList();
            var strValidacao = HistoricoEscolarDomainService.VerificarHistoricoComponentesAprovadosDispensados(dados.SeqPessoaAtuacao, componentesSol, dados.SeqCicloLetivoProcesso);
            if (!string.IsNullOrEmpty(strValidacao))
                throw new TurmaJaAprovadaDispensadaException(strValidacao, "Efetivação");

            // Busca o periodo do ciclo letivo do processo do processo
            var cicloLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dados.SeqCicloLetivoProcesso.Value, dados.AlunoHistorico.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(), TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // Recupera a parametrização de instituição x nivel x tipo de vinculo do aluno
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(dados.SeqInstituicaoEnsino, dados.AlunoHistorico.SeqNivelEnsino, dados.SeqTipoVinculoAluno, IncludesInstituicaoNivelTipoVinculoAluno.TiposTermoIntercambio);

            // Inicia a transação
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                /************************************************************************/
                // 1. INCLUI NOVA SITUAÇÃO PARA O ALUNO NO CICLO LETIVO DO PROCESSO

                // Busca o historico de ciclo letivo do aluno no ciclo do processo
                var specHistorico = new AlunoHistoricoCicloLetivoFilterSpecification()
                {
                    SeqAlunoHistorico = dados.AlunoHistorico.SeqAlunoHistorico,
                    SeqCicloLetivo = dados.SeqCicloLetivoProcesso.Value
                };
                var seqHistorico = AlunoHistoricoCicloLetivoDomainService.SearchProjectionByKey(specHistorico, x => (long?)x.Seq);
                if (seqHistorico.HasValue)
                {
                    bool incluirSituacao = true;
                    DateTime dataSituacao = DateTime.Now;

                    var situacaoMatriculado = new IncluirAlunoHistoricoSituacaoVO()
                    {
                        SeqAluno = dados.SeqPessoaAtuacao,
                        SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                        SeqAlunoHistoricoCicloLetivo = seqHistorico,
                        DataInicioSituacao = dataSituacao,
                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO,
                        Observacao = "Efetivação da matrícula",
                    };

                    AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(situacaoMatriculado);

                    if (incluirSituacao)
                    {

                        var tokenSituacao = TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO;
                        var trabalho = TrabalhoAcademicoDomainService.BuscarDatasDepositoDefesaTrabalho(dados.SeqPessoaAtuacao);

                        //Se o aluno possuir data de autorização de segundo depósito em algum trabalho acadêmico, que pertence ao ciclo letivo do processo da solicitação
                        //(considerando o PERIODO_CICLO_LETIVO do ciclo letivo), a data de início será igual a maior data de autorização encontrada.
                        var specTrabalhoAcademicoAutoria = new TrabalhoAcademicoAutoriaSpecification { SeqAluno = dados.SeqPessoaAtuacao };

                        var trabalhoAcademicoAutoria = TrabalhoAcademicoAutoriaDomainService.SearchBySpecification(specTrabalhoAcademicoAutoria);
                        if (trabalhoAcademicoAutoria != null && trabalhoAcademicoAutoria.Count() > 0)
                        {
                            var seqsTrabalhoAcademico = trabalhoAcademicoAutoria.Select(x => x.SeqTrabalhoAcademico).ToList();
                            var specTrabalhoAcademico = new TrabalhoAcademicoAlunoSpecification { SeqsTrabalhoAcademico = seqsTrabalhoAcademico };

                            var trabalhoAcademico = TrabalhoAcademicoDomainService.SearchBySpecification(specTrabalhoAcademico);

                            if (trabalhoAcademico != null && trabalhoAcademico.Count() > 0)
                            {
                                //Caso possua um ou mais trabalhos acadêmicos, busca o registro que tenha a maior data de autorização segundo depósito respeitando o ciclo letivo
                                //DO PROCESSO e define a data situação com o seu valor
                                var trabalhoComMaiorData = trabalhoAcademico.Where(x => x.DataAutorizacaoSegundoDeposito.HasValue
                                                                             && x.DataAutorizacaoSegundoDeposito.Value >= cicloLetivo.DataInicio
                                                                             && x.DataAutorizacaoSegundoDeposito.Value <= cicloLetivo.DataFim)
                                                                             .OrderByDescending(x => x.DataAutorizacaoSegundoDeposito)
                                                                             .FirstOrDefault();

                                if (trabalhoComMaiorData != null)
                                    dataSituacao = trabalhoComMaiorData.DataAutorizacaoSegundoDeposito.Value;
                            }
                        }

                        // Inclui a nova situação
                        var incluiNovaSituacao = new IncluirAlunoHistoricoSituacaoVO()
                        {
                            SeqAluno = dados.SeqPessoaAtuacao,
                            SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                            SeqAlunoHistoricoCicloLetivo = seqHistorico,
                            DataInicioSituacao = dados.AlunoHistorico.DataPrevisaoConclusao.AddDays(1),
                            TokenSituacao = tokenSituacao,
                            Observacao = "Efetivação da matrícula"
                        };


                        AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(incluiNovaSituacao);

                    }
                }
                else
                {
                    // Se não encontrou o histórico de ciclo letivo, erro, pois deveria existir
                    // O JOB de renovação ou o atendimento de solicitação de reabertura criam o histórico
                    // no ciclo letivo com a situação APTO_MATRICULA
                    throw new AlunoHistoricoCicloLetivoInvalidoException();
                }

                /************************************************************************/
                // 2. ALTERA O PLANO DE ESTUDOS DO ALUNO NO CICLO LETIVO DO PROCESSO

                // Gravar o novo plano de estudo de acordo com a solicitação de matricula e cancela (ind_atual = 0) o
                // plano de estudo anterior
                PlanoEstudoDomainService.AlterarPlanoDeEstudoPorSolicitacaoMatricula(dados.SeqPessoaAtuacao, model.SeqSolicitacaoServico, seqHistorico.GetValueOrDefault());

                // Atualiza a descrição
                var solicitacao = new SolicitacaoMatricula
                {
                    Seq = model.SeqSolicitacaoServico,
                    DescricaoAtualizada = SolicitacaoMatriculaItemDomainService.GerarDescricaoItensSolicitacao(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa, false, false)
                };
                UpdateFields(solicitacao, x => x.DescricaoAtualizada);

                /************************************************************************/
                // 4. ENVIA NOTIFICAÇÃO

                if (dados.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_LATO_SENSU)
                {
                    // Monta os dados para merge
                    Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dados.NomeSocialSolicitante) ? dados.NomeSolicitante : dados.NomeSocialSolicitante);

                    // Envia a notificação
                    var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                    {
                        SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                        TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.CONFIRMACAO_RENOVACAO_MATRICULA,
                        DadosMerge = dadosMerge,
                        EnvioSolicitante = true,
                        ConfiguracaoPrimeiraEtapa = false
                    };
                    SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                }
                else if (dados.TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA)
                {
                    // Monta os dados para merge
                    Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(dados.NomeSocialSolicitante) ? dados.NomeSolicitante : dados.NomeSocialSolicitante);
                    dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dados.DescricaoProcesso);

                    // Envia a notificação
                    var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                    {
                        SeqSolicitacaoServico = model.SeqSolicitacaoServico,
                        TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.CONFIRMACAO_MATRICULA_REABERTURA,
                        DadosMerge = dadosMerge,
                        EnvioSolicitante = true,
                        ConfiguracaoPrimeiraEtapa = false
                    };
                    SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                }

                /************************************************************************/
                // 5. FINALIZA A ETAPA DA SOLICITAÇÃO

                // Procedimentos de finalização da etapa...
                SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso, null);

                // Criar mensagem de encerramento da solicitação na linha do tempo da pessoa atuação
                // Chama após finalizar a etapa para pegar a descrição da situação final
                MensagemDomainService.EnviarMensagemLinhaDoTempoEncerramentoSolicitacao(model.SeqSolicitacaoServico, TOKEN_TIPO_MENSAGEM.ENCERRAMENTO_SOLICITACAO_RENOVACAO_REABERTURA);

                unityOfWork.Commit();
            }
        }

        #endregion [ Efetivação ]

        #region [ Renovação ]

        public void CriarSolicitacoesRematricula(RematriculaJOBVO rematricula)
        {
            string descricaoProcesso = null;

            try
            {
#if !DEBUG
                Scheduler.LogInfo(new SMCSchedulerHistoryModel()
                {
                    SeqSchedulerHistory = rematricula.SeqHistoricoAgendamento,
                    Log = $"Inicializando Job Renovação de Matrícula. Processo {rematricula.SeqProcesso}",
                    DateTime = DateTime.Now
                });
#endif
                // Modifica o status do agendamento para executando.
                //ProcessoDomainService.AtualizarStatusAgendamento(rematricula.SeqProcesso, SituacaoAgendamento.Executando);

                // Recupera os dados do processo em questão
                var specProcesso = new SMCSeqSpecification<Processo>(rematricula.SeqProcesso);
                var dadosProcesso = ProcessoDomainService.SearchProjectionByKey(specProcesso, x => new DadosProcessoRematriculaVO
                {
                    SeqProcesso = x.Seq,
                    DescricaoProcesso = x.Descricao,
                    TokenServico = x.Servico.Token,
                    EscalonamentosFuturos = x.GruposEscalonamento.Where(g => g.Ativo && g.Itens.All(i => i.Escalonamento.DataFim > DateTime.Now)),
                    SeqServico = x.SeqServico,
                    SeqsRestricaoSolicitacaoSimultanea = x.Servico.RestricoesSolicitacaoSimultanea.Select(s => s.SeqServicoRestricao).ToList(),
                    OrigemSolicitacaoServico = x.Servico.OrigemSolicitacaoServico,
                    SeqCicloLetivo = x.SeqCicloLetivo,
                    SeqsEntidadesResponsaveis = x.UnidadesResponsaveis.Select(u => u.SeqEntidadeResponsavel)
                });
                descricaoProcesso = dadosProcesso.DescricaoProcesso;

                // Valida o token do processo para saber se é de renovação ou não
                if (dadosProcesso.TokenServico != TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU && dadosProcesso.TokenServico != TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_LATO_SENSU)
                    throw new ProcessoNaoRematriculaException();

                // Permite apenas criar renovação para processos que tenham apenas um escalonamento com datas futuras para as etapas.
                if (dadosProcesso.EscalonamentosFuturos.Count() != 1)
                    throw new ProcessoRematriculaComVariosEscalonamentosException();

                // Verifica se o processo possui ciclo letivo
                if (!dadosProcesso.SeqCicloLetivo.HasValue)
                    throw new ProcessoSemCicloLetivoException();

                // Busca o ciclo letivo anterior ao do processo
                var seqCicloLetivoAnterior = CicloLetivoDomainService.BuscarCicloLetivoAnterior(dadosProcesso.SeqCicloLetivo.Value);
                if (seqCicloLetivoAnterior.HasValue)
                {
                    // Validar se o processo de renovação do ciclo letivo anterior encontra-se encerrado (data de encerramento preenchida)
                    var specProcAnterior = new ProcessoFilterSpecification()
                    {
                        SeqCicloLetivo = seqCicloLetivoAnterior.Value,
                        SeqServico = dadosProcesso.SeqServico,
                        SeqsEntidadesResponsaveis = dadosProcesso.SeqsEntidadesResponsaveis.ToArray()
                    };
                    var datasEncerramento = ProcessoDomainService.SearchProjectionBySpecification(specProcAnterior, p => p.DataEncerramento);

                    if (datasEncerramento.Any(x => !x.HasValue))
                        throw new ProcessoAnteriorNaoEncerradoException();
                }

                // Recupera todos os alunos em situação de renovação para o processo
                var alunos = RawQuery<AlunoRematriculaVO>(_selecionarAlunosRematricula,
                    new SMCFuncParameter("SEQ_PROCESSO", dadosProcesso.SeqProcesso));

#if !DEBUG
                Scheduler.LogInfo(new SMCSchedulerHistoryModel()
                {
                    SeqSchedulerHistory = rematricula.SeqHistoricoAgendamento,
                    Log = $"Prováveis solicitações a serem criadas: {alunos.Count}",
                    DateTime = DateTime.Now
                });
#endif

                double processados = 0;
                double processadosComSucesso = 0;

                if (alunos.SMCAny())
                {
                    // Para cada aluno...
                    foreach (var aluno in alunos)
                    {
                        // Contabiliza o inicio da execução
                        processados += 1;

                        try
                        {
                            // Task 35601
                            // Verifica se o aluno possui matriz curricular oferta.
                            // Caso não, exibir mensagem de erro para o aluno.
                            if (aluno.SeqMatrizCurriculaOferta == null || aluno.SeqMatrizCurriculaOferta == 0)
                                throw new AlunoSemMatrizAssociadaException(aluno.NomeAluno);

                            var sitiuacaoAtiva = new List<CategoriaSituacao>() { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento };

                            // Busca dados complementares do aluno
                            var dadosAluno = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(aluno.SeqPessoaAtuacao), x => new
                            {
                                ExisteSolicitacaoNoProcesso = x.SolicitacoesServico.Any(s => s.ConfiguracaoProcesso.SeqProcesso == dadosProcesso.SeqProcesso),
                                BloqueioPrazo = x.Bloqueios.FirstOrDefault(b => b.MotivoBloqueio.Token == TOKEN_MOTIVO_BLOQUEIO.PRAZO_CONCLUSAO_CURSO_ENCERRADO),
                                ProtocolesRestricaoSolicitacaoSimultanea = x.SolicitacoesServico
                                    .Where(w => dadosProcesso.SeqsRestricaoSolicitacaoSimultanea.Contains(w.ConfiguracaoProcesso.Processo.SeqServico)
                                             && sitiuacaoAtiva.Contains(w.SituacaoAtual.CategoriaSituacao))
                                    .OrderBy(o => o.NumeroProtocolo)
                                    .Select(s => s.NumeroProtocolo)
                                    .ToList(),
                                Nome = x.DadosPessoais.Nome,
                                Seq = x.SeqPessoa
                            });

                            // Verifica se o aluno já possui uma solicitação de matrícula em aberto
                            if (dadosAluno.ExisteSolicitacaoNoProcesso)
                                throw new JaExisteSolicitacaoRematriculaParaAlunoException(aluno.NomeAluno);

                            if (dadosAluno.ProtocolesRestricaoSolicitacaoSimultanea.SMCAny())
                                throw new RestricaoSolicitacaoSimultaneaParaAlunoException(aluno.NomeAluno, dadosAluno.ProtocolesRestricaoSolicitacaoSimultanea);

                            // Busca o PERIODO_CICLO_LETIVO do ciclo do processo para o aluno
                            var evento = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(aluno.SeqCicloLetivoProcesso, aluno.SeqCursoOfertaLocalidadeTurno, TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                            var specTrabalhoAcademico = new TrabalhoAcademicoAutoriaSpecification() { SeqAluno = aluno.SeqPessoaAtuacao };

                            var trabalhoAcademicoAutoria = TrabalhoAcademicoAutoriaDomainService.SearchProjectionBySpecification(specTrabalhoAcademico, t => t);
                            if (trabalhoAcademicoAutoria != null)
                            {
                                foreach (var item in trabalhoAcademicoAutoria)
                                {
                                    var specTrabalhoComponente = new TrabalhoAcademicoDivisaoComponenteSpecification { SeqTrabalhoAcademico = item.SeqTrabalhoAcademico };
                                    var trabalhoComponente = TrabalhoAcademicoDivisaoComponenteDomainService.SearchByKey(specTrabalhoComponente);
                                    var trabalhoAcademico = TrabalhoAcademicoDomainService.SearchByKey(item.SeqTrabalhoAcademico);

                                    if (trabalhoComponente != null && trabalhoAcademico != null)
                                    {
                                        var origemAvaliacao = OrigemAvaliacaoDomainService.SearchByKey(trabalhoComponente.SeqOrigemAvaliacao);
                                        var specAplicacaoAvaliacao = new AplicacaoAvaliacaoFilterSpecification { SeqOrigemAvaliacao = origemAvaliacao.Seq };
                                        var aplicacaoAvaliacao = AplicacaoAvaliacaoDomainService.SearchBySpecification(specAplicacaoAvaliacao);

                                        var specInstituicaoNivelTipoTrabalho = new InstituicaoNivelTipoTrabalhoFilterSpecification()
                                        {
                                            SeqNivelEnsino = trabalhoAcademico.SeqNivelEnsino,
                                            SeqInstituicaoEnsino = trabalhoAcademico.SeqInstituicaoEnsino,
                                            SeqTipoTrabalho = trabalhoAcademico.SeqTipoTrabalho
                                        };

                                        var geraFinanceiro = InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionByKey(specInstituicaoNivelTipoTrabalho, i => i.GeraFinanceiroEntregaTrabalho);

                                        if (aplicacaoAvaliacao.Any(x => x.DataCancelamento == null) && geraFinanceiro)
                                        {
                                            var specTrabalhoDivisaoComponente = new TrabalhoAcademicoDivisaoComponenteSpecification { SeqTrabalhoAcademico = trabalhoAcademico.Seq };
                                            var trabalhoDivisaoComponente = TrabalhoAcademicoDivisaoComponenteDomainService.SearchBySpecification(specTrabalhoDivisaoComponente).ToList();

                                            if (trabalhoDivisaoComponente != null)
                                            {
                                                var seqsDivisaoComponente = new List<long>();
                                                var seqsConfiguracaoComponente = new List<long>();

                                                foreach (var trabalho in trabalhoDivisaoComponente)
                                                    seqsDivisaoComponente.Add(trabalho.SeqDivisaoComponente);

                                                var specDivisaoComponente = new DivisaoComponenteFilterSpecification { Seqs = seqsDivisaoComponente.ToArray() };
                                                var divisaoComponente = DivisaoComponenteDomainService.SearchBySpecification(specDivisaoComponente).ToList();


                                                foreach (var divisao in divisaoComponente)
                                                    seqsConfiguracaoComponente.Add(divisao.SeqConfiguracaoComponente);

                                                var possuiPreRequisitos = RequisitoDomainService.ValidarPreRequisitos(aluno.SeqPessoaAtuacao, seqsDivisaoComponente, seqsConfiguracaoComponente, null, null, null);

                                                if (possuiPreRequisitos.Valido)
                                                    throw new AlunoPossuiDefesaAgendadaException(dadosAluno.Nome);
                                            }
                                        }
                                    }
                                }
                            }

                            // Prepara objeto para criar solicitação de renovação
                            var dadosRenovacao = new SolicitacaoRematriculaVO()
                            {
                                // processo
                                SeqProcesso = dadosProcesso.SeqProcesso,
                                DescricaoProcesso = dadosProcesso.DescricaoProcesso,
                                SeqGrupoEscalonamento = dadosProcesso.EscalonamentosFuturos.First().Seq,
                                SeqCicloLetivoProcesso = aluno.SeqCicloLetivoProcesso,

                                // servico
                                OrigemSolicitacaoServico = dadosProcesso.OrigemSolicitacaoServico,
                                SeqServico = dadosProcesso.SeqServico,
                                TokenServico = dadosProcesso.TokenServico,

                                // aluno
                                SeqPessoaAtuacao = aluno.SeqPessoaAtuacao,
                                NomeAluno = aluno.NomeAluno,
                                SeqEntidadeVinculo = aluno.SeqEntidadeVinculo,
                                SeqTipoVinculoAluno = aluno.SeqTipoVinculoAluno,
                                SeqAlunoHistorico = aluno.SeqAlunoHistorico,
                                SeqCursoOfertaLocalidadeTurno = aluno.SeqCursoOfertaLocalidadeTurno,
                                SeqEntidadeCurso = aluno.SeqEntidadeCurso,
                                SeqEntidadeInstituicaoEnsino = aluno.SeqEntidadeInstituicaoEnsino,
                                SeqNivelEnsino = aluno.SeqNivelEnsino,
                                SeqMatrizCurriculaOferta = aluno.SeqMatrizCurriculaOferta,
                            };


                            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(aluno.SeqPessoaAtuacao);
                            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(dadosOrigem.SeqInstituicaoEnsino,
                                                                                                                                                                    dadosOrigem.SeqNivelEnsino,
                                                                                                                                                                    dadosOrigem.SeqTipoVinculoAluno,
                                                                                                                                                                   IncludesInstituicaoNivelTipoVinculoAluno.InstituicaoNivel_NivelEnsino);

                            var pessoaAtuacaoIntercambioSpec = new PessoaAtuacaoTermoIntercambioFilterSpecification { SeqNivelEnsino = dadosOrigem.SeqNivelEnsino, SeqPessoaAtuacao = aluno.SeqPessoaAtuacao, SeqTipoVinculo = dadosOrigem.SeqTipoVinculoAluno };
                            var pessoaAtuacaoIntercambio = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(pessoaAtuacaoIntercambioSpec, x => x.SeqTermoIntercambio).ToArray();

                            var termoIntercambioSpec = new TermoIntercambioFilterSpecification { SeqsTermosIntercambio = pessoaAtuacaoIntercambio };
                            var termoIntercambio = TermoIntercambioDomainService.SearchProjectionBySpecification(termoIntercambioSpec, x => x.SeqParceriaIntercambioTipoTermo).ToArray();

                            var parceriaTipoTermoSpec = new ParceriaIntercambioTipoTermoFilterSpecification { SeqsParceriaIntercambioTipoTermo = termoIntercambio };
                            var parceriaTipoTermo = ParceriaIntercambioTipoTermoDomainService.SearchBySpecification(parceriaTipoTermoSpec);

                            var tipoTermoConcedeFormacao = false;
                            foreach (var item in parceriaTipoTermo)
                            {
                                var concedeFormacao = TipoTermoIntercambioDomainService.TipoTermoIntercambioConcedeFormacao(item.SeqTipoTermoIntercambio, dadosOrigem.SeqTipoVinculoAluno, dadosOrigem.SeqNivelEnsino, dadosOrigem.SeqInstituicaoEnsino);
                                if (concedeFormacao)
                                    tipoTermoConcedeFormacao = true;
                            }

                            if (instituicaoNivelTipoVinculoAluno.ConcedeFormacao || tipoTermoConcedeFormacao)
                            {
                                // Verifica se deve criar bloqueio de impedimento de prazo encerrado
                                // Cria o bloqueio caso o bloqueio de prazo encerrado do aluno esteja bloqueado e a data
                                // de início do bloqueio seja posterior a data atual e anterior ao inicio do ciclo letivo
                                // do processo.
                                if (dadosAluno.BloqueioPrazo != null)
                                {
                                    if (dadosAluno.BloqueioPrazo.SituacaoBloqueio == SituacaoBloqueio.Bloqueado
                                    && dadosAluno.BloqueioPrazo.DataBloqueio > DateTime.Today
                                    && dadosAluno.BloqueioPrazo.DataBloqueio <= evento.DataInicio)
                                    {
                                        dadosRenovacao.CriaBloqueioImpedimentoPrazo = true;
                                    }
                                }
                            }

                            // Inicia a transação
                            using (var transacao = SMCUnitOfWork.Begin())
                            {
                                // Cria a Solicitação de renovação do aluno
                                CriarSolicitacaoRematriculaAluno(dadosRenovacao);

                                // Finaliza a transação
                                transacao.Commit();
                            }

                            // Contabiliza a execução com sucesso
                            processadosComSucesso += 1;
                        }
                        catch (Exception ex)
                        {
                            // Exibe o erro no log do job
                            var schedulerInfo = new SMCSchedulerHistoryModel()
                            {
                                SeqSchedulerHistory = rematricula.SeqHistoricoAgendamento,
                                Log = ex.Message,
                                OriginID = aluno.SeqPessoaAtuacao,
                                OriginName = string.Format("{0} - {1}", aluno.SeqPessoaAtuacao, aluno.NomeAluno),
                                DateTime = DateTime.Now
                            };
#if !DEBUG
                        Scheduler.LogError(schedulerInfo);
#endif
                        }
                        finally
                        {
#if !DEBUG
                        var log = new SMCSchedulerHistoryModel()
                        {
                            SeqSchedulerHistory = rematricula.SeqHistoricoAgendamento,
                            DateTime = DateTime.Now,
                            Progress = Convert.ToInt16(Math.Floor((processados / alunos.Count) * 100))
                        };

                        if (processados == alunos.Count)
                        {
                            log.Success = true;
                        }
                        Scheduler.Progress(log);
#endif
                        }
                    }
                }
                else
                {
#if !DEBUG
                    var log = new SMCSchedulerHistoryModel()
                    {
                        SeqSchedulerHistory = rematricula.SeqHistoricoAgendamento,
                        DateTime = DateTime.Now,
                        Progress = 100,
                        Success = true
                    };

                    Scheduler.Progress(log);
#endif
                }
#if !DEBUG
                // Registra o feedback do final da operação
                Scheduler.LogSucess(new SMCSchedulerHistoryModel()
                {
                    SeqSchedulerHistory = rematricula.SeqHistoricoAgendamento,
                    Log = $"JOB de renovação finalizado.",
                    DateTime = DateTime.Now
                });
#endif
            }
            catch (Exception ex)
            {
                var log = new SMCSchedulerHistoryModel()
                {
                    SeqSchedulerHistory = rematricula.SeqHistoricoAgendamento,
                    Log = ex.Message,
                    OriginID = rematricula.SeqProcesso,
                    OriginName = descricaoProcesso,
                    DateTime = DateTime.Now
                };
#if !DEBUG
                // Exibe o erro no log do job
                Scheduler.ExceptionEnd(log);
#endif
            }
            finally
            {
                // Atualiza o status.                
                ProcessoDomainService.AtualizarStatusAgendamento(rematricula.SeqProcesso, SituacaoAgendamento.Finalizado);
            }
        }

        /// <summary>
        /// Cria uma solicitação de renovação para um aluno (RN_SRC_103)
        /// </summary>
        /// <param name="dados">Dados para solicitação de renovação do aluno</param>
        /// <returns>Sequencial da solicitação criada</returns>
        public long? CriarSolicitacaoRematriculaAluno(SolicitacaoRematriculaVO dados)
        {
            long? seqSolicitacaoMatricula = null;

            // Cria a solicitação de renovação/reabertura (RN_MAT_103)
            SolicitacaoMatricula solicitacao = CriarSolicitacaoMatricula(dados);

            SaveEntity(solicitacao);

            seqSolicitacaoMatricula = solicitacao.Seq;

            // Cria o bloqueio de IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO (RN_MAT_111)
            if (dados.CriaBloqueioImpedimentoPrazo)
            {
                PessoaAtuacaoBloqueio bloqueio = new PessoaAtuacaoBloqueio()
                {
                    SeqPessoaAtuacao = dados.SeqPessoaAtuacao,
                    SeqMotivoBloqueio = this.MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO),
                    Descricao = "Impedimento de matrícula - Prazo de conclusão de curso encerrado",
                    SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                    DataBloqueio = DateTime.Now,
                    DescricaoReferenciaAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(dados.SeqPessoaAtuacao), p => p.Descricao),
                    CadastroIntegracao = true,
                    SeqSolicitacaoServico = seqSolicitacaoMatricula
                };

                PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueio);
            }

            // Enviar notificações conforme (RN_MAT_091)
            EnviarNotificacaoSolicitacaoRematricula(seqSolicitacaoMatricula.GetValueOrDefault(), dados.NomeAluno, dados.DescricaoProcesso);

            //// Incluir registro na linha do tempo conforme (RN_SRC_070)
            Dictionary<string, string> dicTagsMsg = new Dictionary<string, string>();
            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.PROTOCOLO_SOLICITACAO, solicitacao.NumeroProtocolo);
            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.PROCESSO_SOLICITACAO, dados.DescricaoProcesso);

            MensagemDomainService.EnviarMensagemPessoaAtuacao(solicitacao.SeqPessoaAtuacao,
                                                               dados.SeqEntidadeInstituicaoEnsino,
                                                               dados.SeqNivelEnsino,
                                                               TOKEN_TIPO_MENSAGEM.ABERTURA_SOLICITACAO_SERVICO,
                                                               CategoriaMensagem.LinhaDoTempo,
                                                               dicTagsMsg);

            // Define a nova situação de matrícula do aluno
            string tokenNovaSituacao = TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA;
            long seqNovaSituacao = SituacaoMatriculaDomainService.BuscarSituacaoMatriculaPorToken(tokenNovaSituacao);

            // Define a observação da nova situação de matrícula do aluno
            string obsNovaSituacao = "JOB prepara renovação.";
            if (dados.TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA)
                obsNovaSituacao = "Reabertura de matrícula.";

            // Caso o token do serviço seja de MATRICULA_REABERTURA, pode acontecer de já existir o historico ciclo
            // letivo para o ciclo do processo. Nesse caso, criar apenas a nova situação.
            bool criarHistoricoCiclo = true;
            AlunoHistoricoCicloLetivo alunoHistoricoCicloLetivo = null;
            if (dados.TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA)
            {
                var specHistorico = new AlunoHistoricoCicloLetivoFilterSpecification()
                {
                    SeqAlunoHistorico = dados.SeqAlunoHistorico,
                    SeqCicloLetivo = dados.SeqCicloLetivoProcesso
                };

                var seqHistorico = AlunoHistoricoCicloLetivoDomainService.SearchProjectionByKey(specHistorico, x => (long?)x.Seq);

                if (seqHistorico.HasValue)
                {
                    var incluiNovaSituacao = new IncluirAlunoHistoricoSituacaoVO()
                    {
                        SeqAluno = dados.SeqPessoaAtuacao,
                        SeqSolicitacaoServico = seqSolicitacaoMatricula,
                        SeqAlunoHistoricoCicloLetivo = seqHistorico,
                        DataInicioSituacao = DateTime.Now,
                        TokenSituacao = tokenNovaSituacao,
                        Observacao = obsNovaSituacao
                    };

                    AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(incluiNovaSituacao);
                    criarHistoricoCiclo = false;
                    alunoHistoricoCicloLetivo = new AlunoHistoricoCicloLetivo() { Seq = seqHistorico.Value };
                }
            }

            // Se deve criar o novo histórico de ciclo letivo...
            if (criarHistoricoCiclo)
            {
                // 6. Criar novo histórico-ciclo-letivo para o aluno e nova situação de matrícula
                alunoHistoricoCicloLetivo = new AlunoHistoricoCicloLetivo()
                {
                    SeqSolicitacaoServico = seqSolicitacaoMatricula,
                    SeqCicloLetivo = dados.SeqCicloLetivoProcesso,
                    SeqAlunoHistorico = dados.SeqAlunoHistorico,
                    TipoAluno = TipoAluno.Veterano,
                    AlunoHistoricoSituacao = new List<AlunoHistoricoSituacao>
                    {
                        new AlunoHistoricoSituacao
                        {
                            SeqSituacaoMatricula = seqNovaSituacao,
                            DataInicioSituacao = DateTime.Now,
                            SeqSolicitacaoServico = seqSolicitacaoMatricula,
                            Observacao = obsNovaSituacao
                        }
                    },
                    PlanosEstudo = new List<PlanoEstudo>
                    {
                        new PlanoEstudo
                        {
                            SeqMatrizCurricularOferta = dados.SeqMatrizCurriculaOferta,
                            SeqSolicitacaoServico = seqSolicitacaoMatricula,
                            Atual = true,
                            Observacao = obsNovaSituacao
                        }
                    }
                };

                // Salva o novo historico-ciclo-letivo
                AlunoHistoricoCicloLetivoDomainService.SaveEntity(alunoHistoricoCicloLetivo);
            }

            // Retorna o sequencial da solicitação, caso criada.
            return seqSolicitacaoMatricula;
        }

        private SolicitacaoMatricula CriarSolicitacaoMatricula(SolicitacaoRematriculaVO dados)
        {
            // Busca qual a configuração do processo que será utilizada para a solicitação de matricula
            var configuracaoProcesso = ConfiguracaoProcessoDomainService.BuscarProjecaoPorParametros(dados.SeqTipoVinculoAluno,
                                                                                                  dados.SeqEntidadeVinculo,
                                                                                                  dados.SeqProcesso,
                                                                                                  dados.SeqCursoOfertaLocalidadeTurno,
                                                                                                  dados.SeqNivelEnsino,
                                                                                                  x => new DadosConfiguracaoProcessoEtapaVO
                                                                                                  {
                                                                                                      SeqConfiguracaoProcesso = x.Seq,
                                                                                                      SeqSolicitacaoServico = x.ConfiguracoesEtapa.OrderBy(e => e.ProcessoEtapa.Ordem).FirstOrDefault().ConfiguracaoProcesso.Processo.SeqServico,
                                                                                                      Documentos = x.ConfiguracoesEtapa.SelectMany(c => c.DocumentosRequeridos),
                                                                                                      SeqPrimeiraEtapaSGF = x.ConfiguracoesEtapa.OrderBy(e => e.ProcessoEtapa.Ordem).FirstOrDefault().ProcessoEtapa.SeqEtapaSgf,
                                                                                                      SeqConfiguracaoPrimeiraEtapa = x.ConfiguracoesEtapa.OrderBy(e => e.ProcessoEtapa.Ordem).FirstOrDefault().Seq,
                                                                                                      SeqTemplateProcessoSGF = x.Processo.Servico.SeqTemplateProcessoSgf
                                                                                                  });

            if (configuracaoProcesso == null)
                throw new ConfiguracaoProcessoNaoEncontradaException(dados.NomeAluno);

            /*1. Criar solicitação de serviço e de matrícula para o aluno. */
            // Recupera qual a situação inicial da primeira etapa do processo do SGF
            var etapas = SGFHelper.BuscarEtapasSGFCache(configuracaoProcesso.SeqTemplateProcessoSGF);
            var etapaInicial = etapas.FirstOrDefault(e => e.Seq == configuracaoProcesso.SeqPrimeiraEtapaSGF);
            var situacaoInicial = etapaInicial.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa);

            var solicitacao = new SolicitacaoMatricula
            {
                SeqAlunoHistorico = dados.SeqAlunoHistorico,
                SeqConfiguracaoProcesso = configuracaoProcesso.SeqConfiguracaoProcesso,
                SeqEntidadeResponsavel = dados.SeqEntidadeVinculo,
                SeqGrupoEscalonamento = dados.SeqGrupoEscalonamento,
                SeqPessoaAtuacao = dados.SeqPessoaAtuacao,
                DataSolicitacao = DateTime.Now,
                DescricaoOriginal = "Solicitação de matrícula - " + dados.DescricaoProcesso,
                OrigemSolicitacaoServico = dados.OrigemSolicitacaoServico,
                DocumentosRequeridos = configuracaoProcesso.Documentos?.Select(d => new SolicitacaoDocumentoRequerido
                {
                    SeqDocumentoRequerido = d.Seq
                }).ToList(),
                //SituacaoDocumentacao = situacaoMatricula, -> Preenchido depois de inserir arquivos na solicitação
                Etapas = new List<SolicitacaoServicoEtapa>{
                    new SolicitacaoServicoEtapa
                    {
                        HistoricosSituacao = new  List<SolicitacaoHistoricoSituacao>
                        {
                            new SolicitacaoHistoricoSituacao
                            {
                                SeqSituacaoEtapaSgf = situacaoInicial.Seq,
                                CategoriaSituacao = situacaoInicial.CategoriaSituacao,
                            }
                        },
                        SeqConfiguracaoEtapa = configuracaoProcesso.SeqConfiguracaoPrimeiraEtapa
                    }
                }
            };

            // Buscar termo de adesão
            var filtroTermo = new TermoAdesaoSolicitacaoMatriculaVO()
            {
                SeqCurso = dados.SeqEntidadeCurso,
                SeqInstituicaoEnsino = dados.SeqEntidadeInstituicaoEnsino,
                SeqNivelEnsino = dados.SeqNivelEnsino,
                SeqServico = dados.SeqServico,
                SeqTipoVinculoAluno = dados.SeqTipoVinculoAluno,
                TipoOfertaExigeCurso = true
            };
            var seqTermoAdesao = TermoAdesaoDomainService.BuscarTermoAdesao(filtroTermo);

            if (seqTermoAdesao == 0)
                throw new TermoAdesaoNaoEncontradoException(dados.NomeAluno);

            solicitacao.SeqTermoAdesao = seqTermoAdesao;

            PreencherDocumentosEStatusDaSolicitacao(solicitacao, configuracaoProcesso.Documentos);

            return solicitacao;
        }

        private void PreencherDocumentosEStatusDaSolicitacao(SolicitacaoMatricula solicitacao, IEnumerable<DocumentoRequerido> docsNaConfiguracaoProcesso)
        {
            // Lista que pega os grupos e situações para verificar a situação final sem precisar fazer mais chamadas em banco
            List<DocumentoVO> listaDocumentosParaValidarSituacaoEntrega = new List<DocumentoVO>();
            var seqsTiposDocumentos = docsNaConfiguracaoProcesso.Select(c => c.SeqTipoDocumento);
            var specPessoaAtuacaoDocumento = new PessoaAtuacaoDocumentoFilterSpecification()
            {
                SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao,
                ListaSeqsTipoDocumento = seqsTiposDocumentos.ToArray()
            };
            var listaPessoaAtuacaoDocumentos = PessoaAtuacaoDocumentoDomainService.SearchBySpecification(specPessoaAtuacaoDocumento).ToList();

            foreach (var documentoNaSolicitacao in solicitacao.DocumentosRequeridos)
            {
                List<GrupoDocumentoVO> grupos = new List<GrupoDocumentoVO>();

                var documentoEquivalente = docsNaConfiguracaoProcesso.Where(c => c.Seq == documentoNaSolicitacao.SeqDocumentoRequerido).FirstOrDefault();
                var pessoaAtuacaoDocumento = listaPessoaAtuacaoDocumentos.Where(c => c.SeqTipoDocumento == documentoEquivalente.SeqTipoDocumento).FirstOrDefault();

                if (documentoEquivalente?.GruposDocumentoRequerido != null)
                {
                    List<long> seqsGrupoDocumento = new List<long>();
                    seqsGrupoDocumento.AddRange(documentoEquivalente.GruposDocumentoRequerido.Select(d => d.SeqGrupoDocumentoRequerido).Distinct());

                    var specGrupos = new GrupoDocumentoRequeridoFilterSpecification()
                    {
                        SeqsGrupoDocumento = seqsGrupoDocumento.ToArray()
                    };

                    var resultGrupos = GrupoDocumentoRequeridoDomainService.SearchBySpecification(specGrupos).ToList();

                    foreach (var novoGrupo in resultGrupos)
                    {
                        grupos.Add(new GrupoDocumentoVO()
                        {
                            Seq = novoGrupo.Seq,
                            NumeroMinimoDocumentosRequerido = novoGrupo.MinimoObrigatorio,
                            Descricao = novoGrupo.Descricao
                        });
                    }
                }

                // Para ambos os casos, o SeqDocumentoRequerido já vem preenchido de onde chama este método
                // FIX: Carol. Verificar pois o SeqDocumentoRequerido passou a não ser obrigatório!
                if (pessoaAtuacaoDocumento != null)
                {
                    if (pessoaAtuacaoDocumento.SeqSolicitacaoDocumentoRequerido.HasValue)
                    {
                        var documentoParaCopiar = SolicitacaoDocumentoRequeridoDomainService.SearchProjectionByKey(pessoaAtuacaoDocumento.SeqSolicitacaoDocumentoRequerido.Value, x => new
                        {
                            x.SituacaoEntregaDocumento,
                            x.FormaEntregaDocumento,
                            x.VersaoDocumento,
                            x.DataEntrega,
                            x.Observacao,
                            x.SeqArquivoAnexado,
                            x.DataPrazoEntrega,
                            x.DescricaoInconformidade,
                            x.EntregaPosterior,
                            x.ObservacaoSecretaria,
                            x.DocumentoRequerido
                        });
                        documentoNaSolicitacao.SituacaoEntregaDocumento = documentoParaCopiar.SituacaoEntregaDocumento;
                        documentoNaSolicitacao.FormaEntregaDocumento = documentoParaCopiar.FormaEntregaDocumento;
                        documentoNaSolicitacao.VersaoDocumento = documentoParaCopiar.VersaoDocumento;
                        documentoNaSolicitacao.DataEntrega = documentoParaCopiar.DataEntrega;
                        documentoNaSolicitacao.Observacao = documentoParaCopiar.Observacao;
                        documentoNaSolicitacao.SeqArquivoAnexado = documentoParaCopiar.SeqArquivoAnexado;
                        documentoNaSolicitacao.DataPrazoEntrega = documentoParaCopiar.DataPrazoEntrega;
                        documentoNaSolicitacao.DescricaoInconformidade = documentoParaCopiar.DescricaoInconformidade;
                        documentoNaSolicitacao.EntregaPosterior = documentoParaCopiar.EntregaPosterior;
                        documentoNaSolicitacao.ObservacaoSecretaria = documentoParaCopiar.ObservacaoSecretaria;
                        documentoNaSolicitacao.EntregueAnteriormente = true;

                        listaDocumentosParaValidarSituacaoEntrega.Add(new DocumentoVO()
                        {
                            Grupos = grupos?.Count > 0 ? grupos : null,
                            Documentos = new List<DocumentoItemVO>()
                            {
                                new DocumentoItemVO()
                                {
                                    SituacaoEntregaDocumento = documentoParaCopiar.SituacaoEntregaDocumento
                                }
                            },
                            SeqTipoDocumento = documentoEquivalente.SeqTipoDocumento
                        });
                    }

                    else
                    {
                        documentoNaSolicitacao.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido;
                        documentoNaSolicitacao.FormaEntregaDocumento = pessoaAtuacaoDocumento.SeqArquivoAnexado != null ? FormaEntregaDocumento.Upload : FormaEntregaDocumento.Nenhum;
                        documentoNaSolicitacao.VersaoDocumento = pessoaAtuacaoDocumento.SeqArquivoAnexado != null ? VersaoDocumento.CopiaSimples : VersaoDocumento.Nenhum;
                        documentoNaSolicitacao.DataEntrega = pessoaAtuacaoDocumento.DataEntrega;
                        documentoNaSolicitacao.SeqArquivoAnexado = pessoaAtuacaoDocumento.SeqArquivoAnexado;
                        documentoNaSolicitacao.EntregaPosterior = false;
                        documentoNaSolicitacao.ObservacaoSecretaria = pessoaAtuacaoDocumento.Observacao;
                        documentoNaSolicitacao.EntregueAnteriormente = true;

                        if (documentoEquivalente.Obrigatorio)
                        {
                            listaDocumentosParaValidarSituacaoEntrega.Add(new DocumentoVO()
                            {
                                Grupos = grupos?.Count > 0 ? grupos : null,
                                Documentos = new List<DocumentoItemVO>()
                                {
                                    new DocumentoItemVO()
                                    {
                                        SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido
                                    }
                                },
                                SeqTipoDocumento = documentoEquivalente.SeqTipoDocumento
                            });
                        }
                    }
                }
                else
                {
                    documentoNaSolicitacao.SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega;
                    documentoNaSolicitacao.EntregueAnteriormente = false;

                    if (documentoEquivalente.Obrigatorio)
                    {
                        listaDocumentosParaValidarSituacaoEntrega.Add(new DocumentoVO()
                        {
                            Grupos = grupos?.Count > 0 ? grupos : null,
                            Documentos = new List<DocumentoItemVO>()
                        {
                            new DocumentoItemVO()
                            {
                                SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega
                            }
                        },
                            SeqTipoDocumento = documentoEquivalente.SeqTipoDocumento
                        });
                    }
                }
            }

            solicitacao.SituacaoDocumentacao = RecuperarSituacaoDocumentacao(listaDocumentosParaValidarSituacaoEntrega);
        }

        private SituacaoDocumentacao RecuperarSituacaoDocumentacao(List<DocumentoVO> documentosNaSolicitacao)
        {
            var situacaoGrupos = RegistroDocumentoDomainService.ValidarSituacoesGruposDocumentos(documentosNaSolicitacao);
            var situacaoDocumentos = RegistroDocumentoDomainService.ValidarSituacoesDocumentosObrigatorios(documentosNaSolicitacao);
            SituacaoDocumentacao situacaoDocumentacao = RegistroDocumentoDomainService.ConfirmarSituacaoDocumento(situacaoDocumentos, situacaoGrupos);

            return situacaoDocumentacao;

            //Antigo
            //return (documentosRequeridos != null && documentosRequeridos.Any()) ?
            //       SituacaoDocumentacao.AguardandoEntrega : SituacaoDocumentacao.NaoRequerida;
        }

        /// <summary>
        /// Envia a notificação de solicitação de renovação de matrícula conforme RN_MAT_091
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço criada</param>
        /// <param name="nomeAluno">Nome do aluno</param>
        /// <param name="descricaoProcesso">Descrição do processo</param>
        private void EnviarNotificacaoSolicitacaoRematricula(long seqSolicitacaoServico, string nomeAluno, string descricaoProcesso)
        {
            var spec = new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoServico);
            var dadosSolicitacao = SearchProjectionByKey(spec, x => new
            {
                Escalonamento = x.GrupoEscalonamento.Itens.OrderBy(i => i.Escalonamento.ProcessoEtapa.Ordem).FirstOrDefault().Escalonamento,
                DescricaoCursoOferta = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao
            });

            Dictionary<string, string> dadosMerge = new Dictionary<string, string>
            {
                { TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, nomeAluno },
                { TOKEN_TAG_NOTIFICACAO.DAT_INICIO_ESCALONAMENTO, dadosSolicitacao.Escalonamento.DataInicio.ToString("dd/MM/yyyy") },
                { TOKEN_TAG_NOTIFICACAO.DAT_FIM_ESCALONAMENTO, dadosSolicitacao.Escalonamento.DataFim.ToString("dd/MM/yyyy") },
                { TOKEN_TAG_NOTIFICACAO.HOR_INICIO_ESCALONAMENTO, dadosSolicitacao.Escalonamento.DataInicio.ToString("HH:mm") },
                { TOKEN_TAG_NOTIFICACAO.HOR_FIM_ESCALONAMENTO, dadosSolicitacao.Escalonamento.DataFim.ToString("HH:mm") },
                { TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, descricaoProcesso },
                { TOKEN_TAG_NOTIFICACAO.CURSO_OFERTA, dadosSolicitacao.DescricaoCursoOferta}
            };

            // Envia a notificação
            var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.LIBERACAO_RENOVACAO_MATRICULA,
                DadosMerge = dadosMerge,
                EnvioSolicitante = true,
                ConfiguracaoPrimeiraEtapa = false
            };
            SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
        }

        #endregion [ Renovação ]

        public bool TemCodigoAdesao(long seqSolicitacaoMatricula)
        {
            var retorno = this.SearchProjectionByKey(seqSolicitacaoMatricula, x => x.CodigoAdesao.HasValue);
            return retorno;
        }

        private void SalvarDocumentosPessoaAtuacaoSolicitacaoDocumentoRequerido(SolicitacaoMatricula solicitacaoMatricula)
        {
            var specification = new PessoaAtuacaoDocumentoFilterSpecification() { SeqPessoaAtuacao = solicitacaoMatricula.SeqPessoaAtuacao };
            var docPessoaAtuacao = PessoaAtuacaoDocumentoDomainService.SearchBySpecification(specification);
            if (docPessoaAtuacao.Any())
            {
                foreach (var item in solicitacaoMatricula.DocumentosRequeridos)
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
                            item.SeqArquivoAnexado = docPessoa.SeqArquivoAnexado;
                            item.EntregaPosterior = false;
                            item.ObservacaoSecretaria = docPessoa.Observacao;
                            item.EntregueAnteriormente = true;
                        }
                    }
                }
            }
        }

        #region [ Seleção Plano de Estudo ]

        public List<TurmaOfertadaVO> BuscarTurmasGraduacaoSelecionadas(long seqSolicitacaoMatricula)
        {
            var listaTurmasSelecionadas = new List<TurmaSelecionadaData>();

            var solicitacaoMatricula = BuscarSolicitacaoMatriculaComItens(seqSolicitacaoMatricula);

            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                SeqConfiguracaoEtapa = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.Seq,
                (x.PessoaAtuacao as Ingressante).Convocado.CodigoAlunoSga
            });

            //var dadosIngressante = IngressanteDomainService.SearchByKey(dadosSolicitacao.SeqPessoaAtuacao, IncludesIngressante.Convocado);

            var paramsListagemTurmas = new TurmaSelecionadaParametrosData()
            {
                Ano = dadosSolicitacao.AnoCicloLetivo,
                Semestre = dadosSolicitacao.NumeroCicloLetivo,
                CodAluno = dadosSolicitacao.CodigoAlunoSga.Value
            };

            if (solicitacaoMatricula.Itens?.Count > 0)
            {
                paramsListagemTurmas.Turmas = new List<SeqTurmaSelecionadasData>();
                var itensFinalizadosComSucesso = solicitacaoMatricula.Itens.Where(c => c.HistoricosSituacao.OrderByDescending(d => d.DataInclusao).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).ToList();

                //var itensFinalizadosComSucesso = solicitacaoMatricula.Itens.Where(c => c.HistoricosSituacao.Any(d => d.SituacaoItemMatricula.SituacaoFinal == true
                //                                                                                                  && d.SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso))
                //                                                           .ToList();
                var seqTurmas = itensFinalizadosComSucesso.Select(c => c.SequencialTurmaSga).ToList();

                //Procurar os dados das turmas somente se existir alguma que esteja com a situação final Finalizada Com SUcesso
                if (seqTurmas.Count > 0)
                {
                    seqTurmas.SMCForEach(c => paramsListagemTurmas.Turmas.Add(new SeqTurmaSelecionadasData() { seq_turma = c.Value }));

                    listaTurmasSelecionadas = IntegracaoAcademicoService.RetornaListaSelecaoTurmas(paramsListagemTurmas);

                    // Preenche o SeqSolicitacaoMatriculaItem de cada um cruzando os dados com a turma selecionada 
                    foreach (var turmaSelecionada in listaTurmasSelecionadas)
                    {
                        var itemCorrespondente = solicitacaoMatricula.Itens
                                                                     .Where(c => c.SequencialTurmaSga == turmaSelecionada.SeqTurma
                                                                              && c.AnoTurmaSga == turmaSelecionada.AnoTurma
                                                                              && c.SemestreTurmaSga == turmaSelecionada.SemestreTurma)
                                                                     .FirstOrDefault();
                        if (itemCorrespondente != null)
                            turmaSelecionada.SeqSolicitacaoMatriculaItem = itemCorrespondente.Seq;
                    }
                }
            }
            else
            {
                if (dadosSolicitacao.TokenServico == TOKEN_SERVICO.MATRICULA_INGRESSANTE_GRADUACAO)
                {
                    paramsListagemTurmas.Periodo = 1;
                    listaTurmasSelecionadas = IntegracaoAcademicoService.RetornaListaSelecaoTurmas(paramsListagemTurmas);

                    var situacoesItemMatricula = ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(dadosSolicitacao.SeqConfiguracaoEtapa), x => new
                    {
                        x.ProcessoEtapa.SituacoesItemMatricula
                    });

                    var naoOptativas = listaTurmasSelecionadas.Where(c => c.DisciplinaOptativa == false).ToList();

                    foreach (var naoOptativa in naoOptativas)
                    {
                        IncluirNovaSolicitacaoMatricuaItem(seqSolicitacaoMatricula, naoOptativa, situacoesItemMatricula.SituacoesItemMatricula);
                    }
                }
                else
                {
                    return listaTurmasSelecionadas.TransformList<TurmaOfertadaVO>();
                }
            }

            var listaTransformada = listaTurmasSelecionadas.TransformList<TurmaOfertadaVO>();

            foreach (var turma in listaTransformada)
            {
                // O botão deve ser apresentado desabilitado caso o token do serviço da solicitação de matrícula 
                // for MATRICULA_INGRESSANTE_GRADUACAO e a turma não for optativa (ind_disciplina_optativa = 0)
                if (dadosSolicitacao.TokenServico == TOKEN_SERVICO.MATRICULA_INGRESSANTE_GRADUACAO && !turma.DisciplinaOptativa)
                {
                    turma.PermiteExclusao = false;
                }
                else
                {
                    turma.PermiteExclusao = true;
                }

                turma.SeqSolicitacaomatricula = seqSolicitacaoMatricula;
            }

            return listaTransformada;
        }

        /// <summary>
        /// Inclui uma nova Solicitacao Matricula Item e um Historico Situacao Matricula Item para ele
        /// </summary>
        public void IncluirNovaSolicitacaoMatricuaItem(long seqSolicitacaoMatricula, TurmaSelecionadaData turmaSelecionada, IList<SituacaoItemMatricula> listaSituacoesItemMatricula)
        {
            long? seqItemExistente = ItemExistenteEmSolicitacaoMatricula(seqSolicitacaoMatricula, turmaSelecionada);

            if (!seqItemExistente.HasValue)
            {
                var situacaoItemMatriculaInicial = listaSituacoesItemMatricula.Where(c => c.SituacaoInicial == true).FirstOrDefault();

                SolicitacaoMatriculaItem novoItem = new SolicitacaoMatriculaItem()
                {
                    SeqSolicitacaoMatricula = seqSolicitacaoMatricula,
                    PertencePlanoEstudo = false,
                    SemestreTurmaSga = (short?)turmaSelecionada.SemestreTurma,
                    AnoTurmaSga = (short?)turmaSelecionada.AnoTurma,
                    SequencialTurmaSga = turmaSelecionada.SeqTurma
                };

                SolicitacaoMatriculaItemDomainService.SaveEntity(novoItem);

                // Inclusão do seq no objeto VO para possível exclusão em tela
                turmaSelecionada.SeqSolicitacaoMatriculaItem = novoItem.Seq;

                SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(novoItem.Seq, situacaoItemMatriculaInicial.Seq, null);
            }
            else
            {
                var finalizadoComSucesso = listaSituacoesItemMatricula.Where(c => c.SituacaoInicial == true
                                                                               && c.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                                                                      .FirstOrDefault();

                SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(seqItemExistente.Value, finalizadoComSucesso.Seq, null);
            }
        }

        /// <summary>
        /// Recupera uma Solicitação de Matricula e seus respectivos itens.
        /// </summary>
        private SolicitacaoMatricula BuscarSolicitacaoMatriculaComItens(long seqSolicitacaoMatricula)
        {
            var solicitacaoMatricula = this.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), IncludesSolicitacaoMatricula.Itens
                                                                                                                              | IncludesSolicitacaoMatricula.Itens_HistoricosSituacao_SituacaoItemMatricula);
            return solicitacaoMatricula;
        }

        /// <summary>
        /// Verifica se o novo item existe dentro da lista de Itens da Solicitacao Matricula e retorna seu seq em c aso positivo
        /// </summary>
        private long? ItemExistenteEmSolicitacaoMatricula(long seqSolicitacaoMatricula, TurmaSelecionadaData novaTurma)
        {
            var solicitacaoMatricula = BuscarSolicitacaoMatriculaComItens(seqSolicitacaoMatricula);

            if (solicitacaoMatricula.Itens?.Count > 0)
            {
                var itemExistente = solicitacaoMatricula.Itens.FirstOrDefault(c => c.SequencialTurmaSga == novaTurma.SeqTurma
                                                                                && c.AnoTurmaSga == novaTurma.AnoTurma
                                                                                && c.SemestreTurmaSga == novaTurma.SemestreTurma);
                return itemExistente?.Seq;
            }
            else
            {
                return null;
            }
        }

        public void SelecaoPlanoEstudoExcluirTurma(long seqSolicitacaoMatriculaItem, long seqSolicitacaoMatricula)
        {
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                SeqConfiguracaoEtapa = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.Seq
            });

            var situacoesItemMatricula = ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(dadosSolicitacao.SeqConfiguracaoEtapa), x => new
            {
                x.ProcessoEtapa.SituacoesItemMatricula
            });

            var situacaoCancelado = situacoesItemMatricula.SituacoesItemMatricula.FirstOrDefault(c => c.SituacaoFinal == true
                                                                                                   && c.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado);

            var novoHistoricoSituacao = new SolicitacaoMatriculaItemHistoricoSituacao()
            {
                SeqSolicitacaoMatriculaItem = seqSolicitacaoMatriculaItem,
                SeqSituacaoItemMatricula = situacaoCancelado.Seq,
                MotivoSituacaoMatricula = MotivoSituacaoMatricula.PeloSolicitante
            };

            SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(novoHistoricoSituacao);
        }

        public void PlanoEstudosConsistirProsseguirEtapa(long seqSolicitacaoMatricula)
        {
            var solicitacaoMatricula = BuscarSolicitacaoMatriculaComItens(seqSolicitacaoMatricula);

            if (solicitacaoMatricula?.Itens.Count > 0)
            {
                // Verificar se existe pelo menos um item da solicitação com situação parametrizada para ser a inicial e final 
                // com classificação "Finalizada com sucesso", E campo "Pertence ao plano de estudo" igual a "NÃO".
                var itensFinalizadosComSicesso = solicitacaoMatricula.Itens.Where(c => c.HistoricosSituacao.Any(d => d.SituacaoItemMatricula?.SituacaoInicial == true
                                                                                                                  && d.SituacaoItemMatricula?.SituacaoFinal == true
                                                                                                                  && d.SituacaoItemMatricula?.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                                                                                                                  && c.PertencePlanoEstudo == false).ToList();

                // Caso não houver, abortar a operação e exibir a seguinte mensagem: "Não é possível prosseguir. É obrigatório selecionar pelo menos uma turma."
                if (itensFinalizadosComSicesso.Count() == 0)
                {
                    throw new SelecionarTurmaInvalidoException();
                }
            }
            else
            {
                throw new SelecionarTurmaInvalidoException();
            }
        }

        /// <summary>
        /// Retorna a lista de turmas que o Aluno pode se matricular. Este método não retorna turmas já matriculadas.
        /// </summary>
        public List<TurmaOfertadaVO> RetornaListaTurmasOfertadas(long seqSolicitacaoMatricula, string descricaoTurma = null)
        {
            var solicitacaoMatricula = BuscarSolicitacaoMatriculaComItens(seqSolicitacaoMatricula);

            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                SeqConfiguracaoEtapa = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.Seq
            });

            var dadosIngressante = IngressanteDomainService.SearchByKey(dadosSolicitacao.SeqPessoaAtuacao, IncludesIngressante.Convocado);

            var paramsListagemTurmas = new TurmaSelecionadaParametrosData()
            {
                Ano = dadosSolicitacao.AnoCicloLetivo,
                Semestre = dadosSolicitacao.NumeroCicloLetivo,
                CodAluno = dadosIngressante.Convocado.CodigoAlunoSga.Value
            };

            if (dadosSolicitacao.TokenServico == TOKEN_SERVICO.MATRICULA_INGRESSANTE_GRADUACAO)
            {
                paramsListagemTurmas.Periodo = 1;
                paramsListagemTurmas.NomeDisciplina = !string.IsNullOrEmpty(descricaoTurma) ? $"%{descricaoTurma}%" : null;
            }
            else if (dadosSolicitacao.TokenServico == TOKEN_SERVICO.MATRICULA_INGRESSANTE_DISCIPLINA_ISOLADA_GRADUACAO)
            {
                if (solicitacaoMatricula.Itens?.Count > 0)
                {
                    var itensCancelados = solicitacaoMatricula.Itens.Where(c => c.HistoricosSituacao.Any(d => d.SituacaoItemMatricula.SituacaoFinal == true
                                                                                                           && d.SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado
                                                                                                           && d.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PeloSolicitante)).ToList();
                    if (itensCancelados.Count > 0)
                    {
                        var seqTurmas = itensCancelados.Select(c => c.SequencialTurmaSga).ToList();
                        seqTurmas.SMCForEach(c => paramsListagemTurmas.Turmas.Add(new SeqTurmaSelecionadasData() { seq_turma = c.Value }));

                    }
                    else
                    {
                        return new List<TurmaOfertadaVO>();
                    }
                }
                else
                {
                    return new List<TurmaOfertadaVO>();
                }
            }
            else
            {
                paramsListagemTurmas.NomeDisciplina = !string.IsNullOrEmpty(descricaoTurma) ? $"%{descricaoTurma}%" : null;
            }

            var listaTurmasRetorno = IntegracaoAcademicoService.RetornaListaSelecaoTurmas(paramsListagemTurmas);

            // Descomentar caso seja preciso ver todas as matérias na modal de selecão de plano de estudo
            if (solicitacaoMatricula.Itens?.Count > 0)
            {
                var seqTurmas = solicitacaoMatricula.Itens.Where(c => c.HistoricosSituacao.Any(d => d.SituacaoItemMatricula.SituacaoFinal == true
                                                                                                 && d.SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso))
                                                          .Select(e => e.SequencialTurmaSga).ToList();

                listaTurmasRetorno.RemoveAll(item => seqTurmas.Any(item2 => item.SeqTurma == item2));
            }

            return listaTurmasRetorno.TransformList<TurmaOfertadaVO>();
        }

        /// <summary>
        /// Grava as turmas selecionadas em Plano de Estudo como Itens na Solicitação Matrícula
        /// </summary>
        public void PlanoEstudoSalvarTurmasSelecionadas(long seqSolicitacaoMatricula, List<long?> seqTurmas)
        {
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula), x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                AnoCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloLetivo = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                SeqConfiguracaoEtapa = x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.Seq
            });

            var situacoesItemMatricula = ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(dadosSolicitacao.SeqConfiguracaoEtapa), x => new
            {
                x.ProcessoEtapa.SituacoesItemMatricula
            });

            var dadosIngressante = IngressanteDomainService.SearchByKey(dadosSolicitacao.SeqPessoaAtuacao, IncludesIngressante.Convocado);

            var paramsListagemTurmas = new TurmaSelecionadaParametrosData()
            {
                Ano = dadosSolicitacao.AnoCicloLetivo,
                Semestre = dadosSolicitacao.NumeroCicloLetivo,
                CodAluno = dadosIngressante.Convocado.CodigoAlunoSga.Value,
                Turmas = new List<SeqTurmaSelecionadasData>()
            };

            seqTurmas.SMCForEach(c => paramsListagemTurmas.Turmas.Add(new SeqTurmaSelecionadasData() { seq_turma = Convert.ToInt32(c.Value) }));
            var listaTurmasRetorno = IntegracaoAcademicoService.RetornaListaSelecaoTurmas(paramsListagemTurmas);

            foreach (var turma in listaTurmasRetorno)
            {
                IncluirNovaSolicitacaoMatricuaItem(seqSolicitacaoMatricula, turma, situacoesItemMatricula.SituacoesItemMatricula);
            }
        }

        #region [ Seleção de ênfase ]

        private List<GrupoProposicaoComFormacoesCurriculoVO> RecuperarFormacoesCurriculo(int codAluno)
        {
            List<GrupoProposicaoComFormacoesCurriculoVO> lista = new List<GrupoProposicaoComFormacoesCurriculoVO>();
            var formacoesCurriculo = IntegracaoAcademicoService.BuscarFormacoesCurriculo(codAluno);

            var listaCodFormacoes = formacoesCurriculo.Select(c => c.CodGrupoProposicoes).Where(d => d.Value > 0).Distinct().ToList();

            foreach (var codGrupoProposicoes in listaCodFormacoes)
            {
                var listaFormacoes = formacoesCurriculo.Where(c => c.CodGrupoProposicoes == codGrupoProposicoes).ToList();
                var novoItem = new GrupoProposicaoComFormacoesCurriculoVO()
                {
                    CodGrupoProposicoes = Convert.ToInt32(codGrupoProposicoes),
                    FormacoesCurriculo = new List<FormacaoCurriculoVO>()
                };

                foreach (var formacao in listaFormacoes)
                {
                    var formacaoCurriculo = formacao.Transform<FormacaoCurriculoVO>();
                    novoItem.FormacoesCurriculo.Add(formacaoCurriculo);
                }

                lista.Add(novoItem);
            }

            return lista;
        }

        #endregion
        #endregion

        #region [Reabrir Solicitação]

        /// <summary>
        /// Realiza a reabertura de uma solicitação de matrícula
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencia da solicitação</param>
        /// <param name="primeiraEtapa">Indica se a reabertura é feita na primeira etapa</param>
        /// <param name="ultimaEtapa">Indica se a reabertura é feita na ultima etapa</param>
        public void ReabrirSolicitacao(long seqSolicitacaoServico, bool primeiraEtapa, bool ultimaEtapa)
        {
            // Indicador que informa se deve ou não atualizar a descrição da solicitação
            bool atualizarDescricaoSolicitacao = false;

            // Recupera os dados da solicitação
            var specSol = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);
            var dadosSolicitacao = SolicitacaoServicoDomainService.SearchProjectionByKey(specSol, s => new
            {
                NumeroProtocolo = s.NumeroProtocolo,
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                SeqCicloLetivo = s.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                SeqProcessoEtapa = s.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa
            });

            // Recupera os itens da solicitação
            var itensSolicitacao = SolicitacaoMatriculaItemDomainService.SearchProjectionBySpecification(new SolicitacaoMatriculaItemFilterSpecification
            {
                SeqSolicitacaoMatricula = seqSolicitacaoServico
            }, x => new
            {
                SeqSolicitacaoMatriculaItem = x.Seq,
                PertencePlanoEstudo = x.PertencePlanoEstudo,
                UltimoHistoricoSituacao = x.HistoricosSituacao.OrderByDescending(h => h.Seq).Select(h => new
                {
                    MotivoSituacaoMatricula = h.MotivoSituacaoMatricula,
                    ClassificacaoSituacaoFinal = h.SituacaoItemMatricula.ClassificacaoSituacaoFinal,
                    SituacaoInicial = h.SituacaoItemMatricula.SituacaoInicial,
                    SituacaoFinal = h.SituacaoItemMatricula.SituacaoFinal,
                    SeqSolicitacaoMatriculaItemHistoricoSituacao = h.Seq,
                    SeqSituacaoItemMatricula = h.SeqSituacaoItemMatricula,
                    SeqProcessoEtapa = h.SituacaoItemMatricula.SeqProcessoEtapa,
                }).FirstOrDefault(),
                PrimeiroHistoricoSituacao = x.HistoricosSituacao.OrderBy(h => h.Seq).Where(h => h.SituacaoItemMatricula.ProcessoEtapa.Seq == h.SolicitacaoMatriculaItem.SolicitacaoMatricula.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa).Select(h => new
                {
                    h.MotivoSituacaoMatricula,
                    h.SeqSituacaoItemMatricula
                }).FirstOrDefault(),
                SeqDivisaoTurma = x.SeqDivisaoTurma,
                CodigoTurma = (int?)x.DivisaoTurma.Turma.Codigo,
                NumeroTurma = (short?)x.DivisaoTurma.Turma.Numero
            }).ToList();

            // Recupera as situações possíveis para os itens
            var specSit = new SituacaoItemMatriculaFilterSpecification { SeqProcessoEtapa = dadosSolicitacao.SeqProcessoEtapa };
            var situacoesItens = SituacaoItemMatriculaDomainService.SearchProjectionBySpecification(specSit, x => new
            {
                x.Seq,
                x.SituacaoFinal,
                x.SituacaoInicial,
                x.ClassificacaoSituacaoFinal,
            });

            /* 1 Verificar se a etapa atual é a primeira etapa do processo. Se for: */
            if (primeiraEtapa)
            {
                /*  RETORNAR A SITUAÇÃO DOS ITENS NA PRIMEIRA ETAPA
                    1.1. Os itens que estão com a situação que foi configurada para ser a situação final, com classificação “Cancelado” e o motivo “Pela instituição” E com classificação "Cancelado" 
                    e o motivo "Por dispensa/aprovação", não devem ter atribuição de uma nova situação.*/
                var itensAtuais = itensSolicitacao.Where(i => !(i.UltimoHistoricoSituacao.SituacaoFinal &&
                                                i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PelaInstituicao ||
                                                i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PorDispensaAprovacao));

                // Armazena quais turmas estão canceladas para exibir o erro
                List<string> turmasCanceladas = new List<string>();
                foreach (var i in itensAtuais)
                {
                    if (i.PertencePlanoEstudo)
                    {
                        /*	1.2. Caso o 1.1 não ocorra e o item pertencer ao plano de estudo, atribuir a situação no histórico de situação no item que foi configurada
                            para ser a inicial, final, com classificação “Não alterado”.*/
                        var situacaoItemNaoAlterado = situacoesItens.FirstOrDefault(si => si.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado &&
                                                                                    si.SituacaoInicial &&
                                                                                    si.SituacaoFinal);

                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(i.SeqSolicitacaoMatriculaItem, situacaoItemNaoAlterado.Seq, null);
                    }
                    else
                    {
                        /* 1.3. Caso o 1.1 não ocorra e o item não pertencer ao plano de estudo, atribuir a situação no histórico de situação no item que foi configurada para
                                    ser a final, com classificação "Cancelada" e o motivo “Pelo solicitante”. */

                        // Recupera a situação da turma desta divisão
                        var dadosTurma = DivisaoTurmaDomainService.SearchProjectionByKey(i.SeqDivisaoTurma.Value, x => new
                        {
                            Situacao = x.Turma.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoTurma,
                            Codigo = x.Turma.Codigo,
                            Numero = x.Turma.Numero
                        });

                        if (dadosTurma.Situacao == Common.Areas.TUR.Enums.SituacaoTurma.Cancelada)
                        {
                            turmasCanceladas.Add($"- Turma {dadosTurma.Codigo}.{dadosTurma.Numero} - Motivo: Turma cancelada");
                            continue;
                        }

                        var solicitacaoItemCancelado = situacoesItens.FirstOrDefault(si => si.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado && si.SituacaoFinal);
                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(i.SeqSolicitacaoMatriculaItem, solicitacaoItemCancelado.Seq, MotivoSituacaoMatricula.PeloSolicitante);
                    }
                }

                if (turmasCanceladas.Any())
                    throw new SMCApplicationException($"Não é possível prosseguir. Existem itens da solicitação que impedem a sua reabertura: <br /><br /> {string.Join("<br />", turmasCanceladas.Distinct())}");

                // TASK 42046
                List<long> seqsDivisoesTurma = new List<long>();
                seqsDivisoesTurma.AddRange(itensAtuais.Where(w => w.SeqDivisaoTurma.HasValue).Select(s => s.SeqDivisaoTurma.Value).ToList());
                var turmaHorario = EventoAulaDomainService.ValidarColisaoHorariosDivisoes(seqsDivisoesTurma);
                if (turmaHorario != null && turmaHorario.Count > 0)
                {
                    List<string> descricaoTurmaHorario = new List<string>();
                    foreach (var seqDivisao in turmaHorario)
                    {
                        descricaoTurmaHorario.Add(DivisaoTurmaDomainService.BuscarDescricaoTurmaConcatenadoPorDivisaoTurma(seqDivisao));
                    }

                    throw new TurmaCoincidenciaHorarioInvalidoException($"</br> {string.Join("</br>", descricaoTurmaHorario)}");
                }
            }

            /* 2. Se não for a primeira, verificar se é a última etapa. Se não for: */
            else if (!primeiraEtapa && !ultimaEtapa)
            {
                /*  RETORNAR A SITUAÇÃO DOS ITENS NA SEGUNDA ETAPA (SÓ OCORRE NA SOLICITAÇÃO DE INCLUSÃO DE DISCIPLINA ELETIVA)
                   2.1. Os itens que estão com a situação que foi configurada para ser a situação final, com classificação “Cancelado” e o motivo “Pela instituição” E com classificação "Cancelado" 
                   e o motivo "Por dispensa/aprovação", não devem ter atribuição de uma nova situação..*/

                // Armazena quais turmas estão canceladas para exibir o erro
                List<string> turmasCanceladas = new List<string>();

                // Recupera os itens que são necessários para esta etapa
                var itensAtuais = itensSolicitacao.Where(i => i.PrimeiroHistoricoSituacao != null &&
                                            !(i.UltimoHistoricoSituacao.SituacaoFinal &&
                                                i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PelaInstituicao ||
                                                i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PorDispensaAprovacao)).ToList();

                foreach (var i in itensAtuais)
                {
                    /*  2.2. Para cada item da solicitação, com gestão turma, que não pertence ao plano de estudo e situação atual
                        do item foi parametrizada para ser a final com classificação "Finalizado sem sucesso", verificar se a
                        situação atual da turma é "Cancelada". Caso seja, abortar a operação e exibir a seguinte mensagem
                        informativa:
                        "Não é possível prosseguir. Existem itens da solicitação que impedem a sua reabertura:
                        - Turma + <código da turma>+.+<número da turma> + - Motivo: Turma cancelada"
                        Obs.: caso mais de uma turma for listada, exibi-las em ordem crescente do código e número.*/

                    if (!i.PertencePlanoEstudo && i.SeqDivisaoTurma.HasValue)
                    //&&
                    //i.UltimoHistoricoSituacao.SituacaoFinal &&
                    //i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso)
                    {
                        // Recupera a situação da turma desta divisão
                        var dadosTurma = DivisaoTurmaDomainService.SearchProjectionByKey(i.SeqDivisaoTurma.Value, x => new
                        {
                            Situacao = x.Turma.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoTurma,
                            Codigo = x.Turma.Codigo,
                            Numero = x.Turma.Numero
                        });

                        if (dadosTurma.Situacao == Common.Areas.TUR.Enums.SituacaoTurma.Cancelada)
                        {
                            turmasCanceladas.Add($"- Turma {dadosTurma.Codigo}.{dadosTurma.Numero} - Motivo: Turma cancelada");
                            continue;
                        }
                    }

                    /*	2.3. Aos demais itens, atribuir a situação inicial da etapa em questão a cada item da solicitação.*/
                    SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(i.SeqSolicitacaoMatriculaItem, i.PrimeiroHistoricoSituacao.SeqSituacaoItemMatricula, i.PrimeiroHistoricoSituacao.MotivoSituacaoMatricula);

                    /*  2.4. Atualizar as informações no campo "Descrição da solicitação atualizada" de acordo com a RN_MAT_114 - Solicitação - Descrição original/atualizada. */
                    atualizarDescricaoSolicitacao = true;
                }

                if (turmasCanceladas.Any())
                    throw new SMCApplicationException($"Não é possível prosseguir. Existem itens da solicitação que impedem a sua reabertura: <br /><br /> {string.Join("<br />", turmasCanceladas.Distinct())}");

                // TASK 42046
                List<long> seqsDivisoesTurma = new List<long>();
                seqsDivisoesTurma.AddRange(itensAtuais.Where(w => w.SeqDivisaoTurma.HasValue).Select(s => s.SeqDivisaoTurma.Value).ToList());
                var turmaHorario = this.EventoAulaDomainService.ValidarColisaoHorariosDivisoes(seqsDivisoesTurma);
                if (turmaHorario != null && turmaHorario.Count > 0)
                {
                    List<string> descricaoTurmaHorario = new List<string>();
                    foreach (var seqDivisao in turmaHorario)
                    {
                        descricaoTurmaHorario.Add(DivisaoTurmaDomainService.BuscarDescricaoTurmaConcatenadoPorDivisaoTurma(seqDivisao));
                    }

                    throw new TurmaCoincidenciaHorarioInvalidoException($"</br> {string.Join("</br>", descricaoTurmaHorario)}");
                }
            }

            /* 3. Se for a última etapa do processo */
            else if (ultimaEtapa)
            {
                /* RETORNAR A SITUAÇÃO DOS ITENS QUE NÃO FORAM DEFERIDOS NA ÚLTIMA ETAPA
                    3. Verificar se pelo menos um item está com a situação atual configurada de acordo com a etapa para ser final com a classificação "Finalizada com sucesso".*/

                var itensFinalizados = itensSolicitacao.Where(i => i.UltimoHistoricoSituacao.SituacaoFinal &&
                                                                    i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).ToList();

                // Armazena quais turmas estão canceladas para exibir o erro
                List<string> turmasCanceladas = new List<string>();

                // 3.1. Se nenhum estiver nesta situação:
                if (!itensFinalizados.Any())
                {
                    /*  3.1.1. Os itens que estão com a situação que foi configurada para ser a situação final, com classificação “Cancelado” e o motivo
                        “Pela instituição”, não devem ter atribuição de uma nova situação.*/

                    var itensAtuais = itensSolicitacao.Where(i => i.PrimeiroHistoricoSituacao != null &&
                                                !(i.UltimoHistoricoSituacao.SituacaoFinal &&
                                                    i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                    i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PelaInstituicao ||
                                                i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PorDispensaAprovacao)).ToList();

                    foreach (var i in itensAtuais)
                    {
                        if (!i.PertencePlanoEstudo && i.SeqDivisaoTurma.HasValue)
                        //&&
                        //i.UltimoHistoricoSituacao.SituacaoFinal &&
                        //i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso)
                        {
                            // Recupera a situação da turma desta divisão
                            var dadosTurma = DivisaoTurmaDomainService.SearchProjectionByKey(i.SeqDivisaoTurma.Value, x => new
                            {
                                Situacao = x.Turma.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoTurma,
                                Codigo = x.Turma.Codigo,
                                Numero = x.Turma.Numero
                            });

                            if (dadosTurma.Situacao == Common.Areas.TUR.Enums.SituacaoTurma.Cancelada)
                            {
                                turmasCanceladas.Add($"- Turma {dadosTurma.Codigo}.{dadosTurma.Numero} - Motivo: Turma cancelada");
                                continue;
                            }
                        }

                        /* 3.1.2. Aos demais itens, atribuir a situação inicial da etapa em questão a cada item da solicitação. */
                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(i.SeqSolicitacaoMatriculaItem, i.PrimeiroHistoricoSituacao.SeqSituacaoItemMatricula, i.PrimeiroHistoricoSituacao.MotivoSituacaoMatricula);

                        /*  3.1.3. Atualizar as informações no campo "Descrição da solicitação atualizada" de acordo com a RN_MAT_114 - Solicitação - Descrição original/atualizada.  */
                        atualizarDescricaoSolicitacao = true;
                    }

                    if (turmasCanceladas.Any())
                        throw new SMCApplicationException($"Não é possível prosseguir. Existem itens da solicitação que impedem a sua reabertura: <br /><br /> {string.Join("<br />", turmasCanceladas.Distinct())}");

                    //TASK 42046
                    List<long> seqsDivisoesTurma = new List<long>();
                    seqsDivisoesTurma.AddRange(itensAtuais.Where(w => w.SeqDivisaoTurma.HasValue).Select(s => s.SeqDivisaoTurma.Value).ToList());
                    var turmaHorario = this.EventoAulaDomainService.ValidarColisaoHorariosDivisoes(seqsDivisoesTurma);
                    if (turmaHorario != null && turmaHorario.Count > 0)
                    {
                        List<string> descricaoTurmaHorario = new List<string>();
                        foreach (var seqDivisao in turmaHorario)
                        {
                            descricaoTurmaHorario.Add(DivisaoTurmaDomainService.BuscarDescricaoTurmaConcatenadoPorDivisaoTurma(seqDivisao));
                        }

                        throw new TurmaCoincidenciaHorarioInvalidoException($"</br> {string.Join("</br>", descricaoTurmaHorario)}");
                    }

                }
                // 3.2. Se pelo menos um estiver nesta situação, realizar as seguintes ações:
                else
                {
                    // 3.2.1. Para cada item da solicitação, com gestão turma:

                    /* 3.2.1.3. Que pertence ao plano de estudos, verificar se existe coincidência de horário entre todos
                    eles. Caso exista coincidência, abortar a operação e exibir a seguinte mensagem de erro:
                    "Não é possível prosseguir. As seguintes turmas estão com coincidência de horário:
                    - <Descrição da Turma A>, <Descrição da Turma B> e <Descrição da Turma C>"
                    OBS:
                    - Caso exista mais de uma coincidência, elas devem ser listadas separadamente. */

                    var itensAtuais = itensSolicitacao.Where(i => i.PrimeiroHistoricoSituacao != null &&
                                                            !(i.UltimoHistoricoSituacao.SituacaoFinal &&
                                                            i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                            i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PelaInstituicao ||
                                                i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PorDispensaAprovacao)).ToList();

                    var itensVerificarCoincidencia = itensAtuais.Where(a => a.PertencePlanoEstudo).ToList();

                    List<long> seqsDivisoesTurma = new List<long>();
                    seqsDivisoesTurma.AddRange(itensVerificarCoincidencia.Where(w => w.SeqDivisaoTurma.HasValue).Select(s => s.SeqDivisaoTurma.Value).ToList());
                    var turmaHorario = this.EventoAulaDomainService.ValidarColisaoHorariosDivisoes(seqsDivisoesTurma);
                    if (turmaHorario != null && turmaHorario.Count > 0)
                    {
                        List<string> descricaoTurmaHorario = new List<string>();
                        foreach (var seqDivisao in turmaHorario)
                        {
                            descricaoTurmaHorario.Add(DivisaoTurmaDomainService.BuscarDescricaoTurmaConcatenadoPorDivisaoTurma(seqDivisao));
                        }

                        throw new TurmaCoincidenciaHorarioInvalidoException($"</br> {string.Join("</br>", descricaoTurmaHorario)}");
                    }

                    // OK Tudo certo. Volta o aluno para o plano anterior, considerando as regras de vagas de turmas
                    PlanoEstudoDomainService.RetornarAoPlanoEstudosAnterior(dadosSolicitacao.SeqPessoaAtuacao, dadosSolicitacao.SeqCicloLetivo.Value, seqSolicitacaoServico, $"Plano criado devido à reabertura da solicitação: {dadosSolicitacao.NumeroProtocolo}.");

                    itensSolicitacao.Where(i => i.PrimeiroHistoricoSituacao != null &&
                                                !(i.UltimoHistoricoSituacao.SituacaoFinal &&
                                                    i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                    i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PelaInstituicao ||
                                                i.UltimoHistoricoSituacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado &&
                                                i.UltimoHistoricoSituacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.PorDispensaAprovacao)).SMCForEach(i =>
                                                    {
                                                        /* 3.1.2. Aos demais itens, atribuir a situação inicial da etapa em questão a cada item da solicitação. */
                                                        SolicitacaoMatriculaItemHistoricoSituacaoDomainService.AtualizarHistoricoSolicitacaoMatriculaItem(i.SeqSolicitacaoMatriculaItem, i.PrimeiroHistoricoSituacao.SeqSituacaoItemMatricula, i.PrimeiroHistoricoSituacao.MotivoSituacaoMatricula);

                                                        /*  3.1.3. Atualizar as informações no campo "Descrição da solicitação atualizada" de acordo com a RN_MAT_114 - Solicitação - Descrição original/atualizada.  */
                                                        atualizarDescricaoSolicitacao = true;
                                                    });

                    atualizarDescricaoSolicitacao = true;
                }
            }

            // Faz a atualização da descrição da solicitação
            if (atualizarDescricaoSolicitacao)
                SolicitacaoServicoDomainService.AtualizarDescricao(seqSolicitacaoServico, false, true);
        }

        #endregion
    }
}
