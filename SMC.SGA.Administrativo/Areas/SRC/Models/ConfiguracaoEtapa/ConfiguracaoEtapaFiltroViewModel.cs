using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoEtapaFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCParameter]
        [SMCFilter(true, true)]
        public long SeqProcesso { get; set; }  
    }
}