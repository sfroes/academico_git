using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConvocacaoListarViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCOrder(0)]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqProcessoSeletivo { get; set; }

        [SMCHidden]
        public long? SeqPai { get; set; }

        [SMCDescription]
        [SMCOrder(1)]
        public string Descricao { get; set; }
    }
}