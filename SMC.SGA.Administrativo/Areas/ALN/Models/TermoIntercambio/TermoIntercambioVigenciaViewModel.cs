using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TermoIntercambioVigenciaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTermoIntercambio { get; set; }

        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCRequired]
        [SMCMaxDate("DataFim")]
        public DateTime DataInicio { get; set; }

        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCRequired]
        [SMCMinDate("DataInicio")]
        public DateTime DataFim { get; set; } 

        [SMCConditionalReadonly(nameof(TermoIntercambioDynamicModel.SomenteLeituraJustificativaPeriodoVigencia),true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid14_24)]
        public string Justificativa { get; set; }
    }
}