using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class BloqueioEtapaSolicitacaoViewModel : SMCViewModelBase
    {
        public string Etapa { get; set; }

        public List<BloqueioEtapaSolicitacaoItemViewModel> Bloqueios { get; set; }

        [SMCHidden]
        public string BackUrl { get; set; }
    }
}