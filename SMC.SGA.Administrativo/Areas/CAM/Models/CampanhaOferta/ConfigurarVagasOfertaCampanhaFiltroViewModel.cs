using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConfigurarVagasOfertaCampanhaFiltroViewModel : SMCPagerFilterData, ISMCMappable
    {
        [SMCHidden]
        public long SeqCampanha { get; set; }

        [SMCHidden]
        public List<long> SelectedValues { get; set; }
    }
}