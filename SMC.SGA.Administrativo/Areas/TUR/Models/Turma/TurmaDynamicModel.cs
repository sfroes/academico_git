using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using SMC.SGA.Administrativo.Areas.TUR.Controllers;
using SMC.SGA.Administrativo.Areas.TUR.Views.Turma.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    [SMCStepConfiguration(ActionStep = "StepConfiguracaoPrincipal", Partial = "_StepConfiguracaoPrincipal")]
    [SMCStepConfiguration(ActionStep = "StepConfiguracaoCompartilhamento", Partial = "_StepConfiguracaoCompartilhado")]
    [SMCStepConfiguration(ActionStep = "StepOfertaMatriz", Partial = "_StepOfertaMatriz")]
    //[SMCStepConfiguration(ActionStep = "StepParametros", Partial = "_StepParametros")]
    [SMCStepConfiguration(ActionStep = "StepDivisoesParametros", Partial = "_StepDivisoes")]
    [SMCStepConfiguration(ActionStep = "StepComponenteSubstituto", Partial = "_StepComponenteSubstituto")]
    [SMCStepConfiguration(ActionStep = "StepDadosTurma", Partial = "_StepDadosTurma")]
    public class TurmaDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCSeq
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTurmaService), nameof(ITipoTurmaService.BuscarTiposTurmasSelect))]
        public List<SMCDatasourceItem> TiposTurmas { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> DivisoesLocalidades { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> EscalasApuracao { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> CriteriosAprovacao { get; set; }

        [SMCDataSource(SMCStorageType.None)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> ComponentesAssuntos { get; set; }

        #endregion [ DataSources ]

        #region [ Hidden ]

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCIgnoreProp]
        public int StepOrigem { get; set; }

        [SMCIgnoreProp]
        public TurmaCabecalhoViewModel Cabecalho { get; set; }

        [SMCIgnoreProp]
        public string DescricaoConfiguracaoPrincipal { get { return ConfiguracaoComponente?.ConfiguracaoComponenteDescricaoCompleta; } }

        [SMCIgnoreProp]
        public LegendaPrincipal LegendaConfiguracaoPrincipal { get { return LegendaPrincipal.Principal; } }

        [SMCIgnoreProp]
        public long? SeqConfiguracaoComponentePrincipal { get { return ConfiguracaoComponente?.Seq; } }

        [SMCIgnoreProp]
        public long? SeqComponenteCurricularPrincipal { get { return ConfiguracaoComponente?.SeqComponenteCurricular; } }

        [SMCIgnoreProp]
        public List<ConfiguracaoComponenteDivisaoViewModel> DivisoesConfiguracaoPrincipal { get; set; }

        [SMCIgnoreProp]
        public List<object> GridConfiguracaoCompartilhada { get; set; }

        [SMCIgnoreProp]
        public List<object> GridConfiguracaoCompartilhadaValoresSelecionadosBanco { get; set; }

        [SMCIgnoreProp]
        public string MensagemConfiguracaoCompartilhada { get; set; }

        [SMCIgnoreProp]
        public string MensagemOfertaCriterios { get; set; }

        public string MensagemExigeAssunto { get { return UIResource.MSG_Informacao_Turma_Exige_Assunto; } }

        public string MensagemSelecaoAssunto { get { return UIResource.MSG_Informacao_Turma_Selecao_Assunto; } }

        [SMCIgnoreProp]
        public bool ExigeAssuntoComponente { get; set; }

        [SMCIgnoreProp]
        public bool PermiteAvaliacaoParcial { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public TipoGestaoDivisaoComponente TipoGestaoDivisaoComponente { get { return TipoGestaoDivisaoComponente.Turma; } }

        [SMCHidden]
        [SMCStep(0)]
        public bool ConfiguracaoComponenteAtivo { get { return true; } }

        [SMCDependency(nameof(ConfiguracaoComponente) + ".SeqNivelEnsino")]
        [SMCHidden]
        [SMCStep(0)]
        public long? SeqNivel { get { return ConfiguracaoComponente?.SeqNivelEnsino; } }

        [SMCDependency(nameof(SeqCicloLetivoInicio) + ".Seq")]
        [SMCHidden]
        [SMCStep(0)]
        public long? SeqCicloLetivoInicioHidden { get { return SeqCicloLetivoInicio?.Seq; } }

        [SMCDependency(nameof(SeqCicloLetivoFim) + ".Seq")]
        [SMCHidden]
        [SMCStep(0)]
        public long? SeqCicloLetivoFimHidden { get { return SeqCicloLetivoFim?.Seq; } }

        [SMCIgnoreProp]
        public TurmaOrigemAvaliacaoViewModel OrigemAvaliacao { get; set; }

        [SMCIgnoreProp]
        public List<TurmaDivisoesCriterioViewModel> DivisoesTurma { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool ConfirmarAlteracao { get; set; }

        //Existe solicitação de matrícula ou plano de estudo?
        [SMCHidden]
        [SMCStep(0)]
        public bool ExisteSolicitacaoMatriculaOuPlanoEstudo { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool DiarioFechado { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool PossuiNotaLancada { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool EhOuPossuiDesdobramento { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public string MensagemSituacaoCancelada { get { return UIResource.MSG_Informacao_Turma_Cancelada; } }

        [SMCHidden]
        [SMCStep(0)]
        public bool PeriodoLetivoReadOnly { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long? SeqAgendaTurma { get; set; }

        #endregion [ Hidden ]

        #region [ Cabeçalho ]

        public List<TurmaCabecalhoConfiguracoesViewModel> TurmaConfiguracoesCabecalho { get; set; }

        #endregion

        #region [ Propriedades de Condição/Status dos Campos ]

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitarTipoTurma { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitarCicloLetivoInicio { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitarCicloLetivoFim { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitarConfiguracaoComponente { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitarGrupoConfiguracoesCompartilhadas { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        /// - Passo 2: Inclusão de configuração para compartilhamento (não é permitido desmarcar uma configuração já selecionada). 
        public bool HabilitarGrupoConfiguracoesCompartilhadasDesmarcar { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitarComponenteCurricularAssunto { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitarLocalidades { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitarInformacoesAdicionais { get; set; }

        #endregion [ Propriedades de Condição/Status dos Campus ]

        #region [ Operacoes Turma (Copiar, Desdobrar...)]

        public long? SeqTurmaOrigem { get; set; }

        public OperacaoTurma Operacao { get; set; }

        /// <summary>
        /// Recurso Emergencial, devido a falha no framework de preservar a última operação do dynamic
        /// </summary>
        public OperacaoTurma OperacaoFix { get; set; }

        #endregion [ Operacoes Turma (Copiar, Desdobrar...)]

        #region [ Wizard0 ]

        [SMCHidden]
        [SMCKey]
        [SMCReadOnly]
        [SMCStep(0)]
        public override long Seq { get; set; }

        [SMCHidden]
        public long? SeqTipoTurma { get; set; }

        [SMCHidden]
        [SMCReadOnly]
        [SMCStep(0)]
        public int Codigo { get; set; }

        [SMCHidden]
        [SMCReadOnly]
        [SMCStep(0)]
        public short Numero { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public string CodigoFormatado { get; set; }

        //[SMCConditionalReadonly(nameof(HabilitarTipoTurma), SMCConditionalOperation.Equals, "false", PersistentValue = true)]
        //[SMCRequired]
        // [SMCSelect(nameof(TiposTurmas), NameDescriptionField = nameof(DescricaoTipoTurma))]
        // [SMCStep(0)]
        //public long SeqTipoTurma { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa smc-sga-mensagem-multiplas-linhas")]
        //[SMCConditionalDisplay(nameof(SeqTipoTurma), SMCConditionalOperation.Equals, "2")]
        [SMCStep(0)]
        public string InformativoTurmaCompartilhada { get { return UIResource.MSG_Informacao_Turma_Compartilhada; } }

        [SMCStep(0)]
        [SMCHidden]
        public List<long> SeqsMatrizCurricular { get; set; }


        [ConfiguracaoComponenteLookup]
        [SMCConditionalReadonly(nameof(HabilitarConfiguracaoComponente), SMCConditionalOperation.Equals, "false", PersistentValue = true)]
        [SMCDependency(nameof(TipoGestaoDivisaoComponente))]
        [SMCDependency(nameof(SeqsMatrizCurricular))]
        [SMCDependency(nameof(ConfiguracaoComponenteAtivo))]
        [SMCRequired]
        [SMCStep(0)]
        public ConfiguracaoComponenteLookupViewModel ConfiguracaoComponente { get; set; }

        [CicloLetivoLookup]
        [SMCConditionalReadonly(nameof(HabilitarCicloLetivoInicio), SMCConditionalOperation.Equals, "false", PersistentValue = true)]
        [SMCDependency(nameof(SeqNivel))]
        [SMCInclude(Name = "CicloLetivo")]
        [SMCRequired]
        [SMCStep(0)]
        public CicloLetivoLookupViewModel SeqCicloLetivoInicio { get; set; }

        [CicloLetivoLookup]
        [SMCConditionalReadonly(nameof(HabilitarCicloLetivoFim), SMCConditionalOperation.Equals, "false", PersistentValue = true)]
        [SMCDependency(nameof(SeqNivel))]
        [SMCInclude(Name = "CicloLetivo")]
        [SMCRequired]
        [SMCStep(0)]
        public CicloLetivoLookupViewModel SeqCicloLetivoFim { get; set; }

        [SMCMask("9999")]
        [SMCMinValue(0)]
        [SMCRequired]
        [SMCStep(0)]
        public short? QuantidadeVagas { get; set; }

        [SMCStep(0)]
        [SMCConditionalReadonly(nameof(PeriodoLetivoReadOnly), true, PersistentValue = true)]
        [SMCDependency(nameof(SeqCicloLetivoInicio), nameof(TurmaController.PreencherCampoDataInicioPeriodoLetivo), "Turma", false)]
        public DateTime? DataInicioPeriodoLetivo { get; set; }

        [SMCStep(0)]
        //[SMCMinDate(nameof(DataInicioPeriodoLetivo))]
        [SMCConditionalReadonly(nameof(PeriodoLetivoReadOnly), true, PersistentValue = true)]
        [SMCDependency(nameof(SeqCicloLetivoInicio), nameof(TurmaController.PreencherCampoDataFimPeriodoLetivo), "Turma", false)]
        public DateTime? DataFimPeriodoLetivo { get; set; }

        [SMCHidden]
        [SMCSelect]
        [SMCStep(0)]
        public SituacaoTurma SituacaoTurmaAtual { get; set; }

        [SMCStep(0)]
        [SMCMultiline]
        [SMCRequired]
        public string SituacaoJustificativa { get; set; }

        #endregion [ Wizard0 ]

        #region [ Wizard1 ]

        [SMCConditionalReadonly(nameof(HabilitarGrupoConfiguracoesCompartilhadas), SMCConditionalOperation.Equals, "false", PersistentValue = true)]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1)]
        public List<TurmaGrupoConfiguracaoViewModel> GrupoConfiguracoesCompartilhadas { get; set; }

        #endregion

        #region [ Wizard2 ]

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(2)]
        public List<TurmaOfertaMatrizViewModel> TurmaOfertasMatriz { get; set; }

        #endregion

        #region [ Wizard3 ]

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(3)]
        public List<TurmaParametrosViewModel> TurmaParametros { get; set; }

        #endregion

        #region [ Wizard4 ]

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(4)]
        public List<TurmaDivisoesViewModel> TurmaDivisoes { get; set; }

        #endregion

        #region [ Wizard5 ]

        [SMCConditionalReadonly(nameof(HabilitarComponenteCurricularAssunto), SMCConditionalOperation.Equals, "false", PersistentValue = true)]
        [SMCOrder(1)]
        [SMCSelect(nameof(ComponentesAssuntos))]
        [SMCSize(SMCSize.Grid22_24)]
        [SMCStep(5)]
        public long? SeqComponenteCurricularAssunto { get; set; }

        //Utilizado apenas na conferência de dados e visualização de detalhes da turma
        public string DescricaoComponenteCurricularAssunto { get; set; }

        #endregion

        #region [ Cancelar Turma ]

        #endregion [ Cancelar Turma ]

        [SMCStep(0)]
        public string MensagemConfirmarCancelarTurma { get { return UIResource.MSG_Confirmar_Cancelar_Turma; } }


        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.CssClass(insert: "smc-sga-wizard-turma", edit: "smc-sga-wizard-turma")
                   .Detail<TurmaListarDynamicModel>("_DetailList")
                   .DisableInitialListing(true)
                   .HeaderIndexList("CabecalhoLista")
                   .ModalSize(SMCModalWindowSize.Largest, SMCModalWindowSize.Largest, SMCModalWindowSize.Largest)
                   .Service<ITurmaService>(edit: nameof(ITurmaService.BuscarTurma),
                                           index: nameof(ITurmaService.BuscarTurmas),
                                           save: nameof(ITurmaService.SalvarTurma),
                                           insert: nameof(ITurmaService.ExecutarOperacaoTurma),
                                           delete: nameof(ITurmaService.ExcluirTurma))
                   .ConfigureButton((btn, model, action) =>
                   {
                       btn.Action("Incluir", "Turma", new { operacao = OperacaoTurma.Novo });
                   })
                   .Tokens(tokenInsert: UC_TUR_001_01_02.MANTER_TURMA,
                              tokenEdit: UC_TUR_001_01_02.MANTER_TURMA,
                            tokenRemove: UC_TUR_001_01_02.MANTER_TURMA,
                              tokenList: UC_TUR_001_01_01.PESQUISAR_TURMA)
                   .Wizard(editMode: SMCDynamicWizardEditMode.Wizard)
                   .Javascript("Areas/TUR/Turma");
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);
        }

        #endregion [ Configurações ]
    }
}