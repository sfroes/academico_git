using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class PlanoEstudoTurmaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public string backUrl { get; set; }
        public string NomePeriodoFormatado { get; set; }
        public List<PlanoEstudoOfertaItemViewModel> TurmaOfertadaItens { get; set; }
    }
}