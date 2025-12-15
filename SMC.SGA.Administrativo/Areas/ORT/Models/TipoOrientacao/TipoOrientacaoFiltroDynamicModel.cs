using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class TipoOrientacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCOrder(0)]
        [SMCFilter(true, true)]
        [SMCSortable(true)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCOrder(1)]
        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }
    }
}