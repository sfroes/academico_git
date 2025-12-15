using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ConfiguracaoComponenteDivisaoLookupViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        public short Numero { get; set; }
        
        [SMCDescription]       
        public string TipoDivisaoDescricao { get; set; }
        
        public short CargaHoraria { get; set; }
        
        public bool PermiteGrupo { get; set; }

        public long SeqConfiguracaoComponente { get; set; }
    }
}
