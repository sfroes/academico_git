using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CampanhaCicloLetivoLookupViewModel : SMCViewModelBase
    {
        public short? AnoCicloLetivo { get; set; }

        public short? NumeroCicloLetivo { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }
    }
}