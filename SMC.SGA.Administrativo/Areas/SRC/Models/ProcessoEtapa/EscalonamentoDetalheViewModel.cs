using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class EscalonamentoDetalheViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInicio { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCMinDate(nameof(DataInicio))]
        [SMCMinDateNow]
        public DateTime DataFim { get; set; }
    }
}