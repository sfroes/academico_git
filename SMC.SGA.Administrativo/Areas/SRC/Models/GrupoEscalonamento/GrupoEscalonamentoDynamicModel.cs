using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.SGA.Administrativo.Areas.SRC.Views.GrupoEscalonamento.App_LocalResources;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Web;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Ioc;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoEscalonamentoDynamicModel : SMCDynamicViewModel
    {
        #region Propriedades Auxiliares

        [SMCHidden]
        public long SeqGrupoEscalonamento { get { return this.Seq; } }

        [SMCHidden]
        public bool ObrigarNumeroDivisaoParcelas { get; set; }

        [SMCHidden]
        public bool ExibeNumeroDivisaoParcelasDesabilitado { get; set; }

        [SMCHidden]
        public bool TodasParcelasLiberadas { get; set; }

        [SMCHidden]
        public bool ProcessoEncerrado { get; set; }

        [SMCHidden]
        public bool ExibeMensagemSalvar { get; set; }

        [SMCHidden]
        public bool ExibirLegenda { get; set; }

        [SMCHidden]
        public bool ExibeMensagemInformativa { get; set; }

        [SMCHidden]
        public bool ExibeMensagemTokenDisciplinaIsolada { get; set; }

        #endregion Propriedades Auxiliares

        [SMCReadOnly]
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        [SMCFilter]
        public long SeqProcesso { get; set; }

        [SMCOrder(1)]
        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid14_24)]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCSelect]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public bool? Ativo { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        [SMCMask("99")]
        [SMCMinValue(1)]
        [SMCConditionalRequired(nameof(ObrigarNumeroDivisaoParcelas), SMCConditionalOperation.Equals, true)]
        [SMCConditionalDisplay(nameof(ObrigarNumeroDivisaoParcelas), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(ObrigarNumeroDivisaoParcelas), SMCConditionalOperation.Equals, false, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(ExibeNumeroDivisaoParcelasDesabilitado), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R3")]
        [SMCConditionalRule("R1 || R2 || R3")]
        public short? NumeroDivisaoParcelas { get; set; }

        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(HideMasterDetailButtons = true)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public SMCMasterDetailList<GrupoEscalonamentoItemViewModel> Itens { get; set; }

        [SMCHidden]
        public bool CamposReadOnly { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa smc-sga-mensagem-multiplas-linhas")]
        [SMCConditionalDisplay(nameof(ExibeMensagemInformativa), SMCConditionalOperation.Equals, true)]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCIgnoreProp(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa smc-sga-mensagem-multiplas-linhas")]
        [SMCConditionalDisplay(nameof(ExibeMensagemTokenDisciplinaIsolada), SMCConditionalOperation.Equals, true)]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCIgnoreProp(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemTokenDisciplinaIsolada => UIResource.MensagemTokenDisciplinaIsolada;

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal(allowSaveNew: false)
                   .ModalSize(SMCModalWindowSize.Large)
                   .IgnoreFilterGeneration()
                   .ViewPartialInsert("_Edit")
                   .ViewPartialEdit("_Edit")
                   .HeaderIndex(nameof(GrupoEscalonamentoController.CabecalhoProcesso))
                   .Header(nameof(GrupoEscalonamentoController.CabecalhoCompleto))
                   .Ajax()
                   .Assert("_AssertSalvarGrupoEscalonamento", (modeldynamic) => { return (modeldynamic as GrupoEscalonamentoDynamicModel).ExibeMensagemSalvar; },
                                                              (modeldynamic) => { var modelAssert = new AssertSalvarGrupoEscalonamentoViewModel() { Mensagem = UIResource.MSG_Assert_Contato_Financeiro, BackUrl = HttpContext.Current.Request.UrlReferrer.ToString() }; return modelAssert; })
                   .Assert("_AssertSalvarGrupoEscalonamento",
                   (service, model) =>
                   {
                       var modelGrupoEscalonamento = (model as GrupoEscalonamentoDynamicModel);

                       var grupoEscalonamentoService = service.Create<IGrupoEscalonamentoService>();
                       grupoEscalonamentoService.ValidarModelo(modelGrupoEscalonamento.Transform<GrupoEscalonamentoData>());

                       bool exibeAssert = grupoEscalonamentoService.ValidarAssertSalvar(modelGrupoEscalonamento.Transform<GrupoEscalonamentoData>());

                       return exibeAssert;
                   },
                   (service, model) =>
                   {
                       var modelAssert = new AssertSalvarGrupoEscalonamentoViewModel() { Mensagem = UIResource.MSG_Assert_Parcelas_FatorDivisao, BackUrl = HttpContext.Current.Request.UrlReferrer.ToString() };

                       return modelAssert;
                   })
                   .RequiredIncomingParameters(nameof(SeqProcesso))
                   .ButtonBackIndex("Index", "Processo", model => new
                   {
                       SeqProcesso = SMCDESCrypto.EncryptNumberForURL((model as GrupoEscalonamentoDynamicModel).SeqProcesso)
                   })
                  .Detail<GrupoEscalonamentoListarDynamicModel>("_DetailList")
                  .Service<IGrupoEscalonamentoService>(index: nameof(IGrupoEscalonamentoService.BuscarGruposEscalonamento),
                                                        edit: nameof(IGrupoEscalonamentoService.BuscarGrupoEscalonamento),
                                                        insert: nameof(IGrupoEscalonamentoService.BuscarConfiguracaoEscalonamento),
                                                        save: nameof(IGrupoEscalonamentoService.SalvarGrupoEscalonamento),
                                                        delete: nameof(IGrupoEscalonamentoService.ExcluirGrupoEscalonamento))
                  
                   .ConfigureButton((controller, botao, model, action) =>
                   {
                       var itemParsed = model as GrupoEscalonamentoDynamicModel;

                       if (action == SMCDynamicButtonAction.Insert)
                       {
                           var processoService = controller.Create<IProcessoService>();
                           var habilitaBtnComPermissaoManutencaoProcesso = processoService.VerificarValidadeTokenManutencaoProcesso(itemParsed.SeqProcesso);
                           botao.Enabled(habilitaBtnComPermissaoManutencaoProcesso);
                       }
                   })
                  .Tokens(tokenInsert: UC_SRC_002_06_01.CRIAR_NOVO_GRUPO_ESCALONAMENTO,
                           tokenEdit: UC_SRC_002_06_02.MANTER_GRUPO_ESCALONAMENTO_PROCESSO,
                           tokenRemove: UC_SRC_002_06_02.MANTER_GRUPO_ESCALONAMENTO_PROCESSO,
                           tokenList: UC_SRC_002_06_01.PESQUISAR_GRUPO_ESCALONAMENTO_PROCESSO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Insert)
                Ativo = false;
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new ProcessoNavigationGroup(this);
        }
    }
}