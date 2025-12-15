using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class HierarquiaClassificacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCStep(1)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }
    }
}