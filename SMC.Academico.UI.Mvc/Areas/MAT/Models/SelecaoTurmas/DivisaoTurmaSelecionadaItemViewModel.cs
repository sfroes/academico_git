using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Models
{
    public class DivisaoTurmaSelecionadaItemViewModel : SMCViewModelBase
    {
        public string NomePeriodoFormatado { get; set; }

        public List<TurmaSelecionadaItemViewModel> TurmaOfertadaItens { get; set; }
    }
}
