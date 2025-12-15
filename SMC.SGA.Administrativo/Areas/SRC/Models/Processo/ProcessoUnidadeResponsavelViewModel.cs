using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoUnidadeResponsavelViewModel : SMCViewModelBase
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCDescription]
        public string Nome { get; set; }
    }
}