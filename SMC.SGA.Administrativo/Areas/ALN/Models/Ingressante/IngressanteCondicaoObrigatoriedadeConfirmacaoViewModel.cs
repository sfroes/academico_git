using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class IngressanteCondicaoObrigatoriedadeConfirmacaoViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid10_24)]
        public string CondicaoObrigatoriedade { get; set; }

        [SMCSize(SMCSize.Grid2_24)]
        public bool Ativa { get; set; }
    }
}