using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class CalculoTotalCargaHorariaViewModel : SMCViewModelBase
    {
        public int TotalHoras { get; set; }

        public int TotalHorasAula { get; set; }
        public int TotalCreditos { get; set; }
    }
}