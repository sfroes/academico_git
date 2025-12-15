using SMC.Academico.DataRepository.Ioc;
using SMC.Academico.Domain.Areas.Shared.DomainServices;
using SMC.Academico.EntityRepository.Ioc;
using SMC.Academico.Service.Areas.ALN.Services;
using SMC.Academico.Service.Areas.APR.Services;
using SMC.Academico.Service.Areas.CAM.Services;
using SMC.Academico.Service.Areas.CNC.Services;
using SMC.Academico.Service.Areas.CSO.Services;
using SMC.Academico.Service.Areas.CUR.Services;
using SMC.Academico.Service.Areas.DCT.Services;
using SMC.Academico.Service.Areas.FIN.Services;
using SMC.Academico.Service.Areas.GRD.Services;
using SMC.Academico.Service.Areas.MAT.Services;
using SMC.Academico.Service.Areas.ORG.Services;
using SMC.Academico.Service.Areas.ORT.Services;
using SMC.Academico.Service.Areas.PES.Services;
using SMC.Academico.Service.Areas.SRC.Services;
using SMC.Academico.Service.Areas.TUR.Services;
using SMC.Academico.Service.Services;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data.SolicitacaoServico;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.AssinaturaDigital.ServiceContract.Areas.CAD.Interfaces;
using SMC.AssinaturaDigital.ServiceContract.Areas.DDG.Interfaces;
using SMC.AssinaturaDigital.ServiceContract.Areas.DOC.Interfaces;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Interfaces;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.DadosMestres.ServiceContract.Areas.PES.Interfaces;
using SMC.EstruturaOrganizacional.ServiceContract.Areas.ESO.Interfaces;
using SMC.Financeiro.BLT.Service;
using SMC.Financeiro.FIN.Service;
using SMC.Financeiro.Service.FIN;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Formularios.ServiceContract.Areas.FRM.Interfaces;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.DataFilters;
using SMC.Framework.Domain.DataFilters;
using SMC.Framework.Model.Jobs;
using SMC.Framework.Service;
using SMC.Google.Service.ADM;
using SMC.Infraestrutura.ServiceContract.Areas.PDF.Interfaces;
using SMC.IntegracaoAcademico.Service.Areas.IAC.Services;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;

//using SMC.Infraestrutura.ServiceContract.Areas.EML.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.PER.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.USU.Interfaces;

namespace SMC.Academico.Service.Ioc
{
    /// <summary>
    /// Classe para mapeamento de serviços de outros dominios
    /// </summary>
    public class ServiceIocMapping : SMCServiceIocMapping
    {
        /// <summary>
        /// Configurações de ioc
        /// </summary>
        protected override void Configure()
        {
            // Serviços de Segurança
            this.Services
                .Register<IUsuarioService>()
                .Register<IAplicacaoService>()
                .Register<IPerfilService>()
                .Register<IFuncionalidadeService>()
                .Register<ISMCDataFilterService>()
                .Register<IEstruturaOrganizacionalService>()
                .Register<IPdfService>()
                .Register<IAutenticacaoService>()
                .Register<IGoogleService>();

            this.Container.RegisterType<ISMCDataFilterProviderDomainService, DataFilterProviderDomainService>();

            // Serviços de Infraestrutura
            //this.Services
            //    .Register<IEmailService>();
            this.Services.Register<ISMCReportMergeService>();

            // Serviços de Integração Financeiro (GRA)
            //APONTANDO O INTEGRACAO FINANCEIRO PARA LOCAL. LEMBRAR DA CONNECTION STRING
            this.Services
                .Register<IIntegracaoFinanceiroService, IntegracaoFinanceiroService>()
                .Register<IFinanceiroService, FinanceiroService>();

            // this.Container.ConfigureMap<SMC.Financeiro.Service.Ioc.ServiceIocMapping>();
            this.Container.ConfigureMap<SMC.Financeiro.Service.Ioc.ServiceIocMapping>();

            // Serviço do Integração Acadêmico (Local)
            this.Services
                .Register<IIntegracaoAcademicoService, IntegracaoAcademicoService>();
            this.Container.ConfigureMap<SMC.IntegracaoAcademico.Service.Ioc.ServiceIocMapping>();

            // Serviços de Localidade
            this.Services
                .Register<ILocalidadeService>();

            // Serviços de Pessoas
            this.Services
                .Register<SMC.Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService>();

            // Serviços de Avaliação Permanente
            this.Services
                .Register<IAmostraService>();

            // Serviços de Formulário
            this.Services
                .Register<IFormularioService>()
                .Register<ITipoTemplateProcessoService>()
                //.Register<IIntegracaoAcademicoService>() // COMENTADO POIS AGORA USA LOCAL NAO MAIS WCF
                .Register<IEtapaService>()
                .Register<ISituacaoService>()
                .Register<ISituacaoEtapaService>()
                .Register<IClasseTemplateProcessoService>()
                .Register<ITemplateProcessoService>()
                .Register<IPaginaService>()
                .Register<Formularios.ServiceContract.Areas.FRM.Interfaces.IUnidadeResponsavelService>();

            // Serviços do AGD (Calendários)
            this.Services
                .Register<Calendarios.ServiceContract.Areas.CLD.Interfaces.IUnidadeResponsavelService>()
                .Register<Calendarios.ServiceContract.Areas.CLD.Interfaces.ITipoEventoService>()
                .Register<Calendarios.ServiceContract.Areas.CLD.Interfaces.IAgendaService>()
                .Register<Calendarios.ServiceContract.Areas.CLD.Interfaces.ICalendarioService>()
                .Register<Calendarios.ServiceContract.Areas.CLD.Interfaces.ITipoCalendarioService>()
                .Register<Calendarios.ServiceContract.Areas.CLD.Interfaces.IEventoService>()
                .Register<Calendarios.ServiceContract.Areas.CLD.Interfaces.IHorarioService>()
                .Register<Calendarios.ServiceContract.Areas.CLD.Interfaces.ITabelaHorarioService>()
                .Register<Calendarios.ServiceContract.Areas.ESF.Interfaces.ILocalSEFService>();

            // Serviços do GPI
            this.Services
                .Register<Inscricoes.ServiceContract.Areas.RES.Interfaces.IUnidadeResponsavelService>()
                .Register<Inscricoes.ServiceContract.Areas.INS.Interfaces.IIntegracaoService>()
                .Register<Inscricoes.ServiceContract.Areas.INS.Interfaces.IProcessoService>()
                .Register<Inscricoes.ServiceContract.Areas.INS.Interfaces.IInscricaoService>()
                .Register<Inscricoes.ServiceContract.Areas.INS.Interfaces.IInscricaoOfertaHistoricoSituacaoService>()
                .Register<Inscricoes.ServiceContract.Areas.INS.Interfaces.IHierarquiaOfertaService>()
                .Register<Inscricoes.ServiceContract.Areas.INS.Interfaces.IItemHierarquiaOfertaService>();

            // Serviços do SAT
            this.Services
                .Register<AgendadorTarefa.ServiceContract.Areas.ATS.Interfaces.IAgendamentoService>()
                .Register<AgendadorTarefa.ServiceContract.Areas.ATS.Interfaces.IHistoricoAgendamentoService>()
                .Register<ISMCReportProgressJobService>();

            // Serviços do DadosMestres
            this.Services
                .Register<IIntegracaoDadoMestreService>()
                .Register<ITipoDocumentoService>()
                .Register<IContextoBibliotecaService>()
                .Register<IProcessoGedService>()
                .Register<IConfiguracaoProcessoGedService>();

            // Serviços de Notificacao
            this.Services
                .Register<INotificacaoService>()
                .Register<Notificacoes.ServiceContract.Areas.NTF.Interfaces.ITipoNotificacaoService>();

            // Assinatura Digital
            this.Services
                .Register<IConfiguracaoDocumentoService>()
                .Register<IConfiguracaoDocumentoAcademicoService>();

            // Sistema Origem
            this.Services
                .Register<ISistemaOrigemService>();

            // Serviços do Academico
            this.Services
                .Register<IArquivoAnexadoService, ArquivoAnexadoService>()
                .Register<IAPRDynamicService, APRDynamicService>()
                .Register<ICAMDynamicService, CAMDynamicService>()
                .Register<ICNCDynamicService, CNCDynamicService>()
                .Register<ICSODynamicService, CSODynamicService>()
                .Register<IDCTDynamicService, DCTDynamicService>()
                .Register<IORGDynamicService, ORGDynamicService>()
                .Register<IFINDynamicService, FINDynamicService>()
                .Register<IORTDynamicService, ORTDynamicService>()
                .Register<SMC.Academico.ServiceContract.Areas.PES.Interfaces.IPESDynamicService, SMC.Academico.Service.Areas.PES.Services.PESDynamicService>()
                .Register<ICURDynamicService, CURDynamicService>()
                .Register<ITURDynamicService, TURDynamicService>()
                .Register<IALNDynamicService, ALNDynamicService>()
                .Register<IMATDynamicService, MATDynamicService>()
                .Register<ISRCDynamicService, SRCDynamicService>()
                .Register<IGRDDynamicService, GRDDynamicService>()
                .Register<IEntidadeService, EntidadeService>()
                .Register<IEntidadeImagemService, EntidadeImagemService>()
                .Register<IEntidadeConfiguracaoNotificacaoService, EntidadeConfiguracaoNotificacaoService>()
                .Register<IHierarquiaEntidadeService, HierarquiaEntidadeService>()
                .Register<IHierarquiaEntidadeItemService, HierarquiaEntidadeItemService>()
                .Register<IInstituicaoEnsinoService, InstituicaoEnsinoService>()
                .Register<IInstituicaoNivelService, InstituicaoNivelService>()
                .Register<IInstituicaoTipoEntidadeService, InstituicaoTipoEntidadeService>()
                .Register<INivelEnsinoService, NivelEnsinoService>()
                .Register<ITipoHierarquiaClassificacaoService, TipoHierarquiaClassificacaoService>()
                .Register<ITipoHierarquiaEntidadeService, TipoHierarquiaEntidadeService>()
                .Register<SMC.Academico.ServiceContract.Areas.CSO.Interfaces.IHierarquiaClassificacaoService, SMC.Academico.Service.Areas.CSO.Services.HierarquiaClassificacaoService>()
                .Register<ITipoClassificacaoService, TipoClassificacaoService>()
                .Register<IRegimeLetivoService, RegimeLetivoService>()
                .Register<ITipoFormacaoEspecificaService, TipoFormacaoEspecificaService>()
                .Register<IGrauAcademicoService, GrauAcademicoService>()
                .Register<IClassificacaoService, ClassificacaoService>()
                .Register<IInstituicaoNivelModeloRelatorioService, InstituicaoNivelModeloRelatorioService>()
                .Register<IEntidadeHistoricoSituacaoService, EntidadeHistoricoSituacaoService>()
                .Register<IProgramaService, ProgramaService>()
                .Register<ICursoService, CursoService>()
                .Register<ITipoCursoService, TipoCursoService>()
                .Register<IInstituicaoTipoEntidadeHierarquiaClassificacaoService, InstituicaoTipoEntidadeHierarquiaClassificacaoService>()
                .Register<IFormacaoEspecificaService, FormacaoEspecificaService>()
                .Register<IInstituicaoNivelTipoFormacaoEspecificaService, InstituicaoNivelTipoFormacaoEspecificaService>()
                .Register<IInstituicaoNivelTipoOfertaCursoService, InstituicaoNivelTipoOfertaCursoService>()
                .Register<ICursoFormacaoEspecificaService, CursoFormacaoEspecificaService>()
                .Register<ICursoUnidadeService, CursoUnidadeService>()
                .Register<ICursoOfertaService, CursoOfertaService>()
                .Register<ICursoOfertaLocalidadeService, CursoOfertaLocalidadeService>()
                .Register<IInstituicaoNivelModalidadeService, InstituicaoNivelModalidadeService>()
                .Register<IInstituicaoNivelTurnoService, InstituicaoNivelTurnoService>()
                .Register<ITipoDivisaoComponenteService, TipoDivisaoComponenteService>()
                .Register<ICicloLetivoService, CicloLetivoService>()
                .Register<ITipoOfertaService, TipoOfertaService>()
                .Register<IConvocacaoService, ConvocacaoService>()
                .Register<IPessoaAtuacaoCondicaoObrigatoriedadeService, PessoaAtuacaoCondicaoObrigatoriedadeService>()
                .Register<IChamadaService, ChamadaService>()
                .Register<IProcessoSeletivoService, ProcessoSeletivoService>()
                .Register<IProcessoSeletivoOfertaService, ProcessoSeletivoOfertaService>()
                .Register<ICampanhaCicloLetivoService, CampanhaCicloLetivoService>()
                .Register<IInstituicaoNivelCalendarioService, InstituicaoNivelCalendarioService>()
                .Register<ITitulacaoService, TitulacaoService>()
                .Register<ITipoApostilamentoService, TipoApostilamentoService>()
                .Register<ITipoDocumentoAcademicoService, TipoDocumentoAcademicoService>()
                .Register<IDocumentoConclusaoService, DocumentoConclusaoService>()
                .Register<ISituacaoDocumentoAcademicoService, SituacaoDocumentoAcademicoService>()
                .Register<IInstituicaoNivelTipoDocumentoAcademicoService, InstituicaoNivelTipoDocumentoAcademicoService>()
                .Register<IDocumentoConclusaoApostilamentoService, DocumentoConclusaoApostilamentoService>()
                .Register<IInstituicaoNivelRegimeLetivoService, InstituicaoNivelRegimeLetivoService>()
                .Register<ITipoOrientacaoService, TipoOrientacaoService>()
                .Register<IInstituicaoNivelTipoComponenteCurricularService, InstituicaoNivelTipoComponenteCurricularService>()
                .Register<IComponenteCurricularService, ComponenteCurricularService>()
                .Register<ITipoConfiguracaoGrupoCurricularService, TipoConfiguracaoGrupoCurricularService>()
                .Register<IConfiguracaoComponenteService, ConfiguracaoComponenteService>()
                .Register<IEscalaApuracaoService, EscalaApuracaoService>()
                .Register<ITipoDivisaoCurricularService, TipoDivisaoCurricularService>()
                .Register<IDivisaoCurricularService, DivisaoCurricularService>()
                .Register<IInstituicaoNivelEscalaApuracaoService, InstituicaoNivelEscalaApuracaoService>()
                .Register<ICriterioAprovacaoService, CriterioAprovacaoService>()
                .Register<ISolicitacaoServicoService, SolicitacaoServicoService>()
                .Register<ISolicitacaoServicoBoletoTituloService, SolicitacaoServicoBoletoTituloService>()
                .Register<IInstituicaoNivelTipoTermoIntercambioService, InstituicaoNivelTipoTermoIntercambioService>()
                .Register<IInstituicaoNivelCriterioAprovacaoService, InstituicaoNivelCriterioAprovacaoService>()
                .Register<ICurriculoService, CurriculoService>()
                .Register<IProgramaPropostaService, ProgramaPropostaService>()
                .Register<IGrupoConfiguracaoComponenteService, GrupoConfiguracaoComponenteService>()
                .Register<IGrupoCurricularService, GrupoCurricularService>()
                .Register<ICurriculoCursoOfertaService, CurriculoCursoOfertaService>()
                .Register<ITipoGrupoCurricularService, TipoGrupoCurricularService>()
                .Register<IModalidadeService, ModalidadeService>()
                .Register<ITurnoService, TurnoService>()
                .Register<IMatrizCurricularService, MatrizCurricularService>()
                .Register<IHistoricoSituacaoMatrizCurricularOfertaService, HistoricoSituacaoMatrizCurricularOfertaService>()
                .Register<ICurriculoCursoOfertaGrupoService, CurriculoCursoOfertaGrupoService>()
                .Register<IDivisaoMatrizCurricularService, DivisaoMatrizCurricularService>()
                .Register<IMatrizCurricularConfiguracaoComponenteService, MatrizCurricularConfiguracaoComponenteService>()
                .Register<IDivisaoMatrizCurricularComponenteService, DivisaoMatrizCurricularComponenteService>()
                .Register<IDivisaoComponenteService, DivisaoComponenteService>()
                .Register<IConsultaDivisoesMatrizCurricularService, ConsultaDivisoesMatrizCurricularService>()
                .Register<IRequisitoService, RequisitoService>()
                .Register<IDispensaService, DispensaService>()
                .Register<IPessoaAtuacaoService, PessoaAtuacaoService>()
                .Register<IPessoaEnderecoService, PessoaEnderecoService>()
                .Register<SMC.Academico.ServiceContract.Areas.PES.Interfaces.IPessoaService, SMC.Academico.Service.Areas.PES.Services.PessoaService>()
                .Register<IColaboradorService, ColaboradorService>()
                .Register<IPessoaAtuacaoEnderecoService, PessoaAtuacaoEnderecoService>()
                .Register<IPessoaTelefoneService, PessoaTelefoneService>()
                .Register<IPessoaEnderecoEletronicoService, PessoaEnderecoEletronicoService>()
                .Register<ITipoHierarquiaEntidadeItemService, TipoHierarquiaEntidadeItemService>()
                .Register<IInstituicaoExternaService, InstituicaoExternaService>()
                .Register<ICategoriaInstituicaoEnsinoService, CategoriaInstituicaoEnsinoService>()
                .Register<IInstituicaoTipoEntidadeVinculoColaboradorService, InstituicaoTipoEntidadeVinculoColaboradorService>()
                .Register<IReferenciaFamiliarService, ReferenciaFamiliarService>()
                .Register<ITipoBloqueioService, TipoBloqueioService>()
                .Register<IMotivoBloqueioService, MotivoBloqueioService>()
                .Register<ITipoVinculoColaboradorService, TipoVinculoColaboradorService>()
                .Register<IPessoaAtuacaoBloqueioService, PessoaAtuacaoBloqueioService>()
                .Register<IColaboradorVinculoService, ColaboradorVinculoService>()
                .Register<ITipoAgendaService, TipoAgendaService>()
                .Register<IInstituicaoTipoEventoService, InstituicaoTipoEventoService>()
                .Register<IInstituicaoTipoAtuacaoService, InstituicaoTipoAtuacaoService>()
                .Register<IInstituicaoTipoEventoService, InstituicaoTipoEventoService>()
                .Register<ICicloLetivoTipoEventoService, CicloLetivoTipoEventoService>()
                .Register<ISituacaoEntidadeService, SituacaoEntidadeService>()
                .Register<IMatrizCurricularDivisaoComponenteService, MatrizCurricularDivisaoComponenteService>()
                .Register<IBeneficioService, BeneficioService>()
                .Register<ICondicaoObrigatoriedadeService, CondicaoObrigatoriedadeService>()
                .Register<IPessoaJuridicaService, PessoaJuridicaService>()
                .Register<IInstituicaoLocalidadeAgendaService, InstituicaoLocalidadeAgendaService>()
                .Register<IInstituicaoNivelBeneficioService, InstituicaoNivelBeneficioService>()
                .Register<ITipoTurmaService, TipoTurmaService>()
                .Register<ITurmaService, TurmaService>()
                .Register<IInstituicaoNivelBeneficioService, InstituicaoNivelBeneficioService>()
                .Register<IConfiguracaoBeneficioService, ConfiguracaoBeneficioService>()
                .Register<IEventoLetivoService, EventoLetivoService>()
                .Register<IBeneficioHistoticoValorAuxilioService, BeneficioHistoticoValorAuxilioService>()
                .Register<ITipoTermoIntercambioService, TipoTermoIntercambioService>()
                .Register<IParceriaIntercambioService, ParceriaIntercambioService>()
                .Register<ITermoIntercambioService, TermoIntercambioService>()
                .Register<IIngressanteService, IngressanteService>()
                .Register<IParceriaIntercambioInstituicaoExternaService, ParceriaIntercambioInstituicaoExternaService>()
                .Register<ITipoEventoService, TipoEventoService>()
                .Register<ICalendarioService, CalendarioService>()
                .Register<IServicoService, ServicoService>()
                .Register<IProcessoService, ProcessoService>()
                .Register<IConfiguracaoProcessoService, ConfiguracaoProcessoService>()
                .Register<IConfiguracaoEtapaService, ConfiguracaoEtapaService>()
                .Register<IConfiguracaoEtapaPaginaService, ConfiguracaoEtapaPaginaService>()
                .Register<ITextoSecaoPaginaService, TextoSecaoPaginaService>()
                .Register<IArquivoSecaoPaginaService, ArquivoSecaoPaginaService>()
                .Register<IConfiguracaoEtapaBloqueioService, ConfiguracaoEtapaBloqueioService>()
                .Register<IDocumentoRequeridoService, DocumentoRequeridoService>()
                .Register<IGrupoDocumentoRequeridoService, GrupoDocumentoRequeridoService>()
                .Register<IPosicaoConsolidadaService, PosicaoConsolidadaService>()
                .Register<IInstituicaoNivelTipoOrientacaoService, InstituicaoNivelTipoOrientacaoService>()
                .Register<ISolicitacaoMatriculaService, SolicitacaoMatriculaService>()
                .Register<ISolicitacaoMatriculaItemService, SolicitacaoMatriculaItemService>()
                .Register<ISolicitacaoServicoEtapaService, SolicitacaoServicoEtapaService>()
                .Register<ITipoServicoService, TipoServicoService>()
                .Register<IProcessoEtapaService, ProcessoEtapaService>()
                .Register<ISolicitacaoHistoricoNavegacaoService, SolicitacaoHistoricoNavegacaoService>()
                .Register<ISolicitacaoHistoricoSituacaoService, SolicitacaoHistoricoSituacaoService>()
                .Register<IGrupoEscalonamentoService, GrupoEscalonamentoService>()
                .Register<IGrupoEscalonamentoItemService, GrupoEscalonamentoItemService>()
                .Register<ISituacaoMatriculaService, SituacaoMatriculaService>()
                .Register<IFormaIngressoService, FormaIngressoService>()
                .Register<IInstituicaoNivelTipoVinculoAlunoService, InstituicaoNivelTipoVinculoAlunoService>()
                .Register<ITipoVinculoAlunoService, TipoVinculoAlunoService>()
                .Register<ITermoAdesaoService, TermoAdesaoService>()
                .Register<IContratoService, ContratoService>()
                .Register<ITipoProcessoSeletivoService, TipoProcessoSeletivoService>()
                .Register<IInstituicaoNivelTipoAtividadeColaboradorService, InstituicaoNivelTipoAtividadeColaboradorService>()
                .Register<IPessoaAtuacaoBeneficio, PessoaAtuacaoBeneficioService>()
                .Register<ICampanhaService, CampanhaService>()
                .Register<ICampanhaOfertaService, CampanhaOfertaService>()
                .Register<IDivisaoTurmaService, DivisaoTurmaService>()
                .Register<IMatrizCurricularOfertaService, MatrizCurricularOfertaService>()
                .Register<IInstituicaoNivelTipoOrientacaoParticipacaoService, InstituicaoNivelTipoOrientacaoParticipacaoService>()
                .Register<IIngressanteDesistenteService, IngressanteDesistenteService>()
                .Register<ITipoEntidadeService, TipoEntidadeService>()
                .Register<IEscalonamentoService, EscalonamentoService>()
                .Register<ICursoOfertaLocalidadeTurnoService, CursoOfertaLocalidadeTurnoService>()
                .Register<IProcessoUnidadeResponsavelService, ProcessoUnidadeResponsavelService>()
                .Register<IPeriodicoService, PeriodicoService>()
                .Register<IMensagemService, MensagemService>()
                .Register<IMensagemPessoaAtuacaoService, MensagemPessoaAtuacaoService>()
                .Register<ITipoMensagemService, TipoMensagemService>()
                .Register<IInstituicaoNivelTipoMensagemService, InstituicaoNivelTipoMensagemService>()
                .Register<IClassificacaoPeriodicoService, ClassificacaoPeriodicoService>()
                .Register<IQualisPeriodicoService, QualisPeriodicoService>()
                .Register<IOrientacaoService, OrientacaoService>()
                .Register<IRegistroDocumentoService, RegistroDocumentoService>()
                .Register<IAlunoService, AlunoService>()
                .Register<IMaterialService, MaterialService>()
                .Register<ITipoSituacaoMatriculaService, TipoSituacaoMatriculaService>()
                .Register<ITurmaColaboradorService, TurmaColaboradorService>()
                .Register<IDivisaoTurmaColaboradorService, DivisaoTurmaColaboradorService>()
                .Register<ITipoRelatorioServicoService, TipoRelatorioServicoService>()
                .Register<ServiceContract.Areas.ALN.Interfaces.ITipoRelatorioService, Areas.ALN.Services.TipoRelatorioService>()
                .Register<ServiceContract.Areas.DCT.Interfaces.ITipoRelatorioService, Areas.DCT.Services.TipoRelatorioService>()
                .Register<IPessoaAtuacaoTermoIntercambioService, PessoaAtuacaoTermoIntercambioService>()
                .Register<IPeriodoIntercambioService, PeriodoIntercambioService>()
                .Register<ITipoTrabalhoService, TipoTrabalhoService>()
                .Register<ITrabalhoAcademicoService, TrabalhoAcademicoService>()
                .Register<IHistoricoEscolarService, HistoricoEscolarService>()
                .Register<IAlunoHistoricoService, AlunoHistoricoService>()
                .Register<IPlanoEstudoItemService, PlanoEstudoItemService>()
                .Register<IEscalaApuracaoItemService, EscalaApuracaoItemService>()
                .Register<IPublicacaoBdpService, PublicacaoBdpService>()
                .Register<ISGFHelperService, SGFHelperService>()
                .Register<IAplicacaoAvaliacaoService, AplicacaoAvaliacaoService>()
                .Register<IJustificativaSolicitacaoServicoService, JustificativaSolicitacaoServicoService>()
                .Register<IInstituicaoNivelTipoMembroBancaService, InstituicaoNivelTipoMembroBancaService>()
                .Register<IProcessoEtapaConfiguracaoNotificacaoService, ProcessoEtapaConfiguracaoNotificacaoService>()
                .Register<ServiceContract.Areas.SRC.Interfaces.ITipoNotificacaoService, TipoNotificacaoService>()
                .Register<IParametroEnvioNotificacaoService, ParametroEnvioNotificacaoService>()
                .Register<IApuracaoAvaliacaoService, ApuracaoAvaliacaoService>()
                .Register<ISolicitacaoDispensaService, SolicitacaoDispensaService>()
                .Register<IAlunoFormacaoService, AlunoFormacaoService>()
                .Register<IGrupoCurricularComponenteService, GrupoCurricularComponenteService>()
                .Register<IViewCentralSolicitacaoServicoService, ViewCentralSolicitacaoServicoService>()
                .Register<IComponenteCurricularEmentaService, ComponenteCurricularEmentaService>()
                .Register<IPlanoEstudoService, PlanoEstudoService>()
                .Register<IAlunoHistoricoCicloLetivoService, AlunoHistoricoCicloLetivoService>()
                .Register<ITurmaHistoricoFechamentoDiarioService, TurmaHistoricoFechamentoDiarioService>()
                .Register<IAulaService, AulaService>()
                .Register<IProgramaTipoAutorizacaoBdpService, ProgramaTipoAutorizacaoBdpService>()
                .Register<IMotivoAlteracaoBeneficio, MotivoAlteracaoBeneficioService>()
                .Register<IColaboradorVinculoCursoService, ColaboradorVinculoCursoService>()
                .Register<ITitulacaoDocumentoComprobatorioService, TitulacaoDocumentoComprobatorioService>()
                .Register<IFormacaoAcademicaService, FormacaoAcademicaService>()
                .Register<IPessoaAtuacaoAmostraPpaService, PessoaAtuacaoAmostraPpaService>()
                .Register<IFormacaoAcademicaService, FormacaoAcademicaService>()
                .Register<IAtoNormativoService, AtoNormativoService>()
                .Register<ITipoAtoNormativoService, TipoAtoNormativoService>()
                .Register<IAssuntoNormativoService, AssuntoNormativoService>()
                .Register<IOrigemAvaliacaoService, OrigemAvaliacaoService>()
                .Register<IAvaliacaoService, AvaliacaoService>()
                .Register<IEntregaOnlineService, EntregaOnlineService>()
                .Register<ILogAtualizacaoColaboradorService, LogAtualizacaoColaboradorService>()
                .Register<IColaboradorAptoComponenteService, ColaboradorAptoComponenteService>()
                .Register<ISolicitacaoDocumentoConclusaoService, SolicitacaoDocumentoConclusaoService>()
                .Register<IEventoAulaService, EventoAulaService>()
                .Register<IHistoricoDivisaoTurmaConfiguracaoGradeService, HistoricoDivisaoTurmaConfiguracaoGradeService>()
                .Register<IInstituicaoNivelTipoDivisaoComponenteCurricularService, InstituicaoNivelTipoDivisaoComponenteCurricularService>()
                .Register<IApuracaoFrequenciaGradeService, ApuracaoFrequenciaGradeService>()
                .Register<IMantenedoraService, MantenedoraService>()
                .Register<IInstituicaoModeloRelatorioService, InstituicaoModeloRelatorioService>()
                .Register<ITipoFuncionarioService, TipoFuncionarioService>()
                .Register<IInstituicaoTipoFuncionarioService, InstituicaoTipoFuncionarioService>()
                .Register<IInstituicaoTipoEntidadeTipoFuncionarioService, InstituicaoTipoEntidadeTipoFuncionarioService>()
                .Register<IFuncionarioService, FuncionarioService>()
                .Register<IFuncionarioVinculoService, FuncionarioVinculoService>()
                .Register<IInstituicaoNivelSistemaOrigemService, InstituicaoNivelSistemaOrigemService>()
                .Register<IGrupoRegistroService, GrupoRegistroService>()
                .Register<IOrgaoRegistroService, OrgaoRegistroService>()
                .Register<IGradeHorariaCompartilhadService, GradeHoraraCompartilhadaService>()
                .Register<IConfiguracaoAvaliacaoPpaService, ConfiguracaoAvaliacaoPpaService>()
                .Register<IConfiguracaoAvaliacaoPpaTurmaService, ConfiguracaoAvaliacaoPpaTurmaService>()
                .Register<IPessoaAtuacaoDocumentoService, PessoaAtuacaoDocumentoService>()
                .Register<IClassificacaoInvalidadeDocumentoService, ClassificacaoInvalidadeDocumentoService>()
                .Register<ServiceContract.Areas.PES.Interfaces.ITagService, TagService>()
                .Register<IInstituicaoNivelTipoDocumentoModeloRelatorioService, InstituicaoNivelTipoDocumentoModeloRelatorioService>()
                .Register<IConfiguracaoNumeracaoTrabalhoService, ConfiguracaoNumeracaoTrabalhoService>()
                .Register<IDeclaracaoGenericaService, DeclaracaoGenericaService>()
                .Register<IEnvioNotificacaoService, EnvioNotificacaoService>()
                .Register<IEnvioNotificacaoDestinatarioService, EnvioNotificacaoDestinatarioService>()
                .Register<ISolicitacaoTrabalhoAcademicoService, SolicitacaoTrabalhoAcademicoService>()
                .Register<SMC.Academico.ServiceContract.Areas.CNC.Interfaces.IDocumentoAcademicoService, SMC.Academico.Service.Areas.CNC.Services.DocumentoAcademicoService>()
                .Register<IDocumentoAcademicoCurriculoService, DocumentoAcademicoCurriculoService>();


            this.Repositories.Map<EntityIocMapping>();
            this.Repositories.Map<DataRepositoryIocMapping>();
        }
    }
}