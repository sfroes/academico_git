using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class HistoricoNavegacaoViewModel : SMCViewModelBase
    {
        public string Etapa { get; set; }

        public List<HistoricoNavegacaoItemViewModel> Paginas { get; set; }
    }
}