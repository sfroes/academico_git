using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.MAT.Controllers;
using System;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class ChancelaFiltroViewModel : SMCPagerViewModel
    {
        public Guid? Codigo { get; set; }

        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid6_24)]
        public bool ApenasProcessoVigente { get; set; } = true;

        [SMCDependency(nameof(ApenasProcessoVigente), nameof(ChancelaController.BuscarProcessosFiltroChancela), "Chancela", true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid10_24)]
        public long? SeqProcesso { get; set; }

    }
}