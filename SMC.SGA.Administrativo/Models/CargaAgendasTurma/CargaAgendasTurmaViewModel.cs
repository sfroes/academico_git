using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Models
{
    public class CargaAgendasTurmaViewModel : SMCViewModelBase
    {
        [SMCRequired]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }
    }
}