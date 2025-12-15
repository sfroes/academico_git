using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ParceriaIntercambioVigenciaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCMaxDate("DataFim")]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public DateTime DataInicio { get; set; }

        [SMCMinDate("DataInicio")]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }

        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.Equals, 0)]
        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        public string Justificativa { get; set; }
    }
}