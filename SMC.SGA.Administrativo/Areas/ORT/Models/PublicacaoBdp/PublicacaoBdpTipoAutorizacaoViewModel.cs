using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class PublicacaoBdpTipoAutorizacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string TipoDescricao { get; set; }
    }
}