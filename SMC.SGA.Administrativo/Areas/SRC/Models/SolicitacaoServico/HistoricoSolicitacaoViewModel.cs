using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class HistoricoSolicitacaoViewModel : SMCViewModelBase
    {
        public List<HistoricoSolicitacaoEtapaViewModel> Etapas { get; set; }

        public List<HistoricoSolicitacaoEtapaItemViewModel> Historicos { get; set; }
    }
}