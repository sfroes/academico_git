using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class TipoNotificacaoDynamicModel : SMCDynamicViewModel
    {
        #region Data Sources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoNotificacaoService), nameof(ITipoNotificacaoService.BuscarTiposNotificacaoSelect))]
        public List<SMCDatasourceItem> TiposNotificacao { get; set; }

        #endregion
        
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCOrder(0)]
        [SMCRequired]
        [SMCSelect(nameof(TiposNotificacao))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public long SeqTipoNotificacao { get; set; }

        [SMCOrder(1)]
        [SMCMinLength(3)]
        [SMCMaxLength(100)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public string Token { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public PermiteReenvio PermiteReenvio { get; set; }

        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public bool PermiteAgendamento { get; set; }               

        [SMCOrder(4)]
        [SMCDetail(min: 1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(PermiteAgendamento), SMCConditionalOperation.Equals, true)]
        public SMCMasterDetailList<TipoNotificacaoAtributoAgendamentoViewModel> AtributosAgendamento { get; set; }

        [SMCHidden]
        public long SeqAuxiliar { get; set; }

        [SMCHidden]
        public bool ModoEdicao { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Auto)
                   .Service<ITipoNotificacaoService>(index: nameof(ITipoNotificacaoService.BuscarTiposNotificacao),
                                                     edit: nameof(ITipoNotificacaoService.BuscarTipoNotificacao),
                                                     save: nameof(ITipoNotificacaoService.Salvar),
                                                     delete: nameof(ITipoNotificacaoService.Excluir))
                   .Tokens(tokenInsert: UC_SRC_003_01_01.MANTER_TIPO_NOTIFICACAO,
                           tokenEdit: UC_SRC_003_01_01.MANTER_TIPO_NOTIFICACAO,
                           tokenRemove: UC_SRC_003_01_01.MANTER_TIPO_NOTIFICACAO,
                           tokenList: UC_SRC_003_01_01.MANTER_TIPO_NOTIFICACAO);
        }
    }
}