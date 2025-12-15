using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class TipoNotificacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid3_24)]
        [SMCFilter(true, true)]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid14_24)]
        [SMCFilter(true, true)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        [SMCFilter(true, true)]
        public bool? PermiteAgendamento { get; set; }
    }
}