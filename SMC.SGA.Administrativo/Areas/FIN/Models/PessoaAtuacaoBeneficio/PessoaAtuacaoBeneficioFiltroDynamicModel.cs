using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCHidden]
        [SMCParameter]
        public long SeqPessoaAtuacao { get; set; }

        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid4_24)]
        public bool Excluidos { get; set; }
    }
}