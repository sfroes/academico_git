using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCopiaOfertaFiltroViewModel : SMCPagerViewModel
    {
        public long? SeqCampanhaOrigem { get; set; }

        public List<string> DesconsiderarTokensTipoOferta { get; set; }
    }
}