using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCopiaCicloLetivoItemViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid8_24)]
        [CicloLetivoLookup]
        [SMCRequired]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }
    }
}