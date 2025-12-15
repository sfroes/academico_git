using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models.EntidadeImagem
{
    public class EntidadeImagemFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCHidden]
        public long? SeqEntidade { get; set; }
    }
}