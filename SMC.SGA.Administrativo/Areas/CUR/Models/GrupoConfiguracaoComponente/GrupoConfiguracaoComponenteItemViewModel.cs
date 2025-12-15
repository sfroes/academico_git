using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoConfiguracaoComponenteItemViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [ConfiguracaoComponenteLookup(AutoSearch = false)]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid22_24)]
        public ConfiguracaoComponenteLookupReturnViewModel SeqConfiguracaoComponente { get; set; }
    }
}