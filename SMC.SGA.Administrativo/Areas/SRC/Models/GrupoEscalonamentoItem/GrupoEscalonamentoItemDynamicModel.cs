using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using SMC.SGA.Administrativo.Areas.SRC.Views.GrupoEscalonamentoItem.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoEscalonamentoItemDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IMotivoBloqueioService), nameof(IMotivoBloqueioService.BuscarMotivosBloqueioServicoParcelaSelect), values: new[] { nameof(SeqServico) })]
        public List<SMCDatasourceItem> MotivosBloqueio { get; set; } = new List<SMCDatasourceItem>();

        #endregion DataSources

        [SMCKey]
        [SMCHidden]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCHidden]
        public long SeqGrupoEscalonamento { get; set; }

        [SMCHidden]
        [SMCRequired]
        public long SeqEscalonamento { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCHidden]
        public string TokenServico { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public decimal? ValorPercentualBanco { get; set; }

        [SMCHidden]
        public int NumeroDivisaoParcelas { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public SMCMasterDetailList<GrupoEscalonamentoItemParcelaViewModel> Parcelas { get; set; }

        [SMCHidden]
        [SMCParameter]
        public bool ProcessoExpirado { get; set; }

        [SMCHidden]
        [SMCParameter]
        public bool EscalonamentoExpirado { get; set; }

        [SMCHidden]
        public bool ObrigatorioIdentificarParcela { get; set; }

        [SMCHidden]
        public bool CamposReadOnly { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa smc-sga-mensagem-multiplas-linhas")]
        [SMCConditionalDisplay(nameof(CamposReadOnly), SMCConditionalOperation.Equals, true)]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCIgnoreProp(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get; set; }

        [SMCHideLabel]
        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem")]
        [SMCDisplay]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemToken
        {
            get
            {
                if (TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU || TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA)
                    return UIResource.Mensagem_Informativa_Token_Renovacao_Reabertura;
                else if (TokenServico == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU)
                    return UIResource.Mensagem_Informativa_Token_Matricula_Ingressante;
                else if (TokenServico == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA)
                    return UIResource.Mensagem_Informativa_Token_Matricula_Ingressante_Isolada;

                return "";
            }
        }

        [SMCHidden]
        public bool HouveAlteracaoParcela { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenInsert: UC_SRC_002_06_03.CONFIGURAR_PARCELAS,
                           tokenEdit: UC_SRC_002_06_03.CONFIGURAR_PARCELAS,
                           tokenRemove: UC_SRC_002_06_03.CONFIGURAR_PARCELAS,
                           tokenList: UC_SRC_002_06_03.CONFIGURAR_PARCELAS)
                 
                   .Service<IGrupoEscalonamentoItemService>(save: nameof(IGrupoEscalonamentoItemService.SalvarGrupoEscalonamentoItem),
                                                            edit: nameof(IGrupoEscalonamentoItemService.BuscarGrupoEscalonamentoItem))
                     .Assert("Assert_Salvar_Grupo_Escalonamento_Item", (service, model) =>
                     {
                         var modelGrupo = (model as GrupoEscalonamentoItemDynamicModel);

                         var grupoEscalonamentoService = service.Create<IGrupoEscalonamentoItemService>();
                         var validaAssert = grupoEscalonamentoService.ConsistenciasValidadasSalvarGrupoEscalonamentoParcela(modelGrupo.Transform<GrupoEscalonamentoItemData>());

                         return validaAssert;
                     })
                   .Header(nameof(GrupoEscalonamentoItemController.CabecalhoGrupoEscalonamentoItem))
                   .RedirectIndexTo("Index", "GrupoEscalonamento", x => new
                   {
                       SeqProcesso = new SMCEncryptedLong((x as GrupoEscalonamentoItemDynamicModel).SeqProcesso)
                   })

                   .ButtonBackIndex("Index", "GrupoEscalonamento", model => new
                   {
                       SeqProcesso = SMCDESCrypto.EncryptNumberForURL((model as GrupoEscalonamentoItemDynamicModel).SeqProcesso)
                   })
                   .ViewPartialInsert("_DetailEdit")
                   .ViewPartialEdit("_DetailEdit");
        }
    }
}