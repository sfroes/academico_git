using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.PES.Views.TipoMensagem.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TipoMensagemDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoMensagemService), nameof(ITipoMensagemService.BuscarTagsSelect), values: new[] { nameof(TipoTag) })]
        public List<SMCDatasourceItem> ListaTags { get; set; }

        #endregion DataSources

        [SMCHidden]
        [SMCIgnoreProp]
        public TipoTag TipoTag { get; set; } = TipoTag.Mensagem;

        [SMCKey]
        [SMCOrder(0)]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid16_24)]
        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCOrder(1)]
        public string Descricao { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public CategoriaMensagem CategoriaMensagem { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCOrder(4)]
        [SMCSelect]
        public bool? PermiteCadastroManual { get; set; }

        [SMCOrder(5)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCConditionalReadonly(nameof(CategoriaMensagem), SMCConditionalOperation.NotEqual, CategoriaMensagem.LinhaDoTempo)]
        public string ClasseCss { get; set; }

        [SMCOrder(6)]
        [SMCRequired]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        public SMCMasterDetailList<TipoMensagemTipoAtuacaoViewModel> TiposAtuacao { get; set; }

        [SMCOrder(7)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCConditionalDisplay(nameof(CategoriaMensagem), SMCConditionalOperation.Equals, CategoriaMensagem.Documento)]
        public SMCMasterDetailList<TipoMensagemTipoUsoViewModel> TiposUso { get; set; }

        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCConditionalDisplay(nameof(PermiteCadastroManual), SMCConditionalOperation.Equals, false)]
        public SMCMasterDetailList<TipoMensagemTagViewModel> Tags { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Service<ITipoMensagemService>(index: nameof(ITipoMensagemService.ListarTipoMensagem), save: nameof(ITipoMensagemService.SalvarTipoMensagem))
                .Detail<ITipoMensagemService>("_DetailList", allowSort: false)
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((TipoMensagemListarDynamicModel)x).Descricao))
                .Tokens(tokenList: UC_PES_005_01_02.MANTER_TIPO_MENSAGEM,
                        tokenEdit: UC_PES_005_01_02.MANTER_TIPO_MENSAGEM,
                        tokenRemove: UC_PES_005_01_02.MANTER_TIPO_MENSAGEM,
                        tokenInsert: UC_PES_005_01_02.MANTER_TIPO_MENSAGEM);
        }

        #endregion
    }
}