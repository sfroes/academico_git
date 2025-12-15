using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ConfiguracaoComponenteLookupReturnViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
        public string ConfiguracaoComponenteDescricaoCompleta { get; set; }

        public long? SeqComponenteCurricular { get; set; }

    }
}
