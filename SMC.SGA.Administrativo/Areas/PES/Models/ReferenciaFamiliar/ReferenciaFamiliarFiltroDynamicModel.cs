using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ReferenciaFamiliarFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCHidden]
        [SMCParameter]
        public long? SeqPessoaAtuacao { get; set; }

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid12_24)]
        public string NomeParente { get; set; }

        [SMCOrder(1)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24)]
        public TipoParentesco? TipoParentesco { get; set; }
    }
}