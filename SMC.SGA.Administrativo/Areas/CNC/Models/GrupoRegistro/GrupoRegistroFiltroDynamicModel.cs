using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class GrupoRegistroFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid2_24,SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCMaxLength(255)]
        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24,SMCSize.Grid13_24, SMCSize.Grid13_24)]
        public string Descricao { get; set; }
    }
}