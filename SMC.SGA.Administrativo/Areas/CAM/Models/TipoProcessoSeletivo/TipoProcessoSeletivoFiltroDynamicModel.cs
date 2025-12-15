using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class TipoProcessoSeletivoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCFilter]
        [SMCSize(SMCSize.Grid2_24)]
        public long Seq { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }
    }
}