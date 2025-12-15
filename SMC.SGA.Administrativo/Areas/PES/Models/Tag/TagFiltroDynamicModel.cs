using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TagFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        [SMCFilter]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCFilter]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        [SMCOrder(2)]
        [SMCSelect(SortDirection = SMCSortDirection.Descending)]
        [SMCFilter]
        public TipoTag? TipoTag { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        [SMCOrder(3)]
        [SMCSelect(SortDirection = SMCSortDirection.Descending)]
        [SMCFilter]
        public TipoPreenchimentoTag? TipoPreenchimentoTag { get; set; }
    }
}