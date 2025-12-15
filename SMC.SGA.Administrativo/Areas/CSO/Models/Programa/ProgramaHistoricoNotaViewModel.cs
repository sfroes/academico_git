using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ProgramaHistoricoNotaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCMask("9")]
        [SMCMaxValue(7)]
        [SMCMinValue(1)]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public short? ValorNota { get; set; }

        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicio { get; set; }

        [SMCMinDate("DataInicio")]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }
    }
}