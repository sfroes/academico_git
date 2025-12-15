using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;

namespace SMC.SGA.Professor.Areas.APR.Models.EntregaOnline
{
    public class EntregaOnlineViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid4_24, SMCSize.Grid1_24, SMCSize.Grid1_24)]
        public SituacaoEntregaOnline SituacaoEntrega { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid20_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataEntrega { get; set; }

        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        [SMCReadOnly]
        public string Observacao { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public Guid? UidArquivoAnexado { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(SMCDetailType.Tabular, HideMasterDetailButtons = true)]
        public SMCMasterDetailList<EntregaOnlineParticipanteViewModel> Participantes { get; set; }

        [SMCHidden]
        public bool BloquearBotaoDownload { get; set; }

        [SMCHidden]
        public bool BloquearBotaoLiberarNovaEntrega { get; set; }
    }
}