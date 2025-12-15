using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Professor.Areas.APR.Models.EntregaOnline
{
    public class LiberarNovaEntregaOnlineViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqEntregaOnline { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCMultiline]
        [SMCRequired]
        public string Observacao { get; set; }
    }
}