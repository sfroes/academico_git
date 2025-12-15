using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class TipoServicoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        [SMCFilter]
        public long Seq { get; set; }

        [SMCFilter]
        [SMCMaxLength(100)]
        [SMCSortable(true, true)]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid15_24)]
        public string Descricao { get; set; }
    }
}

 