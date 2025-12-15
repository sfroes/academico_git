using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using SMC.SGA.Administrativo.Areas.PES.Views.Tag.App_LocalResources;
using SMC.Framework.Extensions;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TagDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCFilter]
        public override long Seq { get; set; }

        [SMCSize(SMCSize.Grid20_24)]
        [SMCRequired]
        [SMCOrder(1)]
        [SMCFilter]
        [SMCSortable(true, true)]
        [SMCRegularExpression(REGEX.TAG)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCOrder(2)]
        [SMCSelect(SortDirection = SMCSortDirection.Descending)]
        public TipoTag? TipoTag { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(TipoTag), nameof(TagController.HabilitarTipoPreenchimentoTag), "Tag", false)]
        public bool HabilitarTipoPreenchimentoTag { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCOrder(3)]
        [SMCDependency(nameof(TipoTag), nameof(TagController.PreencheTipoPreenchimento), "Tag", true)]
        [SMCSelect()]
        [SMCConditionalReadonly(nameof(HabilitarTipoPreenchimentoTag), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public TipoPreenchimentoTag? TipoPreenchimentoTag { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(TipoPreenchimentoTag), nameof(TagController.HabilitarQueryOrigem), "Tag", false, includedProperties: new[] { nameof(TipoTag) })]
        [SMCDependency(nameof(TipoTag), nameof(TagController.HabilitarQueryOrigem), "Tag", false, includedProperties: new[] { nameof(TipoPreenchimentoTag) })]
        public bool HabilitarQueryOrigem { get; set; }


        [SMCSize(SMCSize.Grid24_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(4)]
        [SMCMultiline]
        [SMCConditionalReadonly(nameof(HabilitarQueryOrigem), SMCConditionalOperation.Equals, false)]
        public string QueryOrigem { get; set; }

        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCMultiline]
        public string InformacaoTag { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .Service<ITagService>(edit: nameof(ITagService.BuscarTag),
                                      save: nameof(ITagService.SalvarTag))
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao, ((TagDynamicModel)x).Descricao))
                .Assert("MSG_Confirmacao_Tag_Automatico_Para_Manual", (controller, model) =>
                {
                    ITagService TagService = controller.Create<ITagService>();
                    var data = model.Transform<TagData>();
                    data.MensagemAutomaticoParaManual = true;
                    bool exibir = TagService.ExibirMensagem(data);
                    return exibir;
                })
                .Assert("MSG_Confirmacao_Tag_Manual_Para_Automatico", (controller, model) =>
                 {
                     ITagService TagService = controller.Create<ITagService>();
                     var data = model.Transform<TagData>();
                     data.MensagemManualParaAutomatico = true;
                     bool exibir = TagService.ExibirMensagem(data);
                     return exibir;
                 })
                .Tokens(tokenInsert: UC_PES_005_03_01.MANTER_TAG,
                        tokenEdit: UC_PES_005_03_01.MANTER_TAG,
                        tokenList: UC_PES_005_03_01.MANTER_TAG,
                        tokenRemove: UC_PES_005_03_01.MANTER_TAG);
        }

        #endregion Configurações
    }
}