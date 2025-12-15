using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ReaberturaSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCRequired]
        [SMCMultiline]
        public string Observacao { get; set; }
    }
}