using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoConfiguracaoComponenteFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24)]
        [ConfiguracaoComponenteLookup(AutoSearch = false)]
        public ConfiguracaoComponenteLookupViewModel SeqConfiguracaoComponente { get; set; }

        [SMCOrder(3)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid4_24)]
        public bool? Ativo { get; set; }
    }
}