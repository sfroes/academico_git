using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class ProcessoLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }
    }
}