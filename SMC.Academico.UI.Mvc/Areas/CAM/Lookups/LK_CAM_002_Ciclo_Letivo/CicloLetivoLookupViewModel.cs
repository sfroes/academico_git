using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CicloLetivoLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        public long? Seq { get; set; }

        [SMCDescription]
         public string Descricao { get; set; }
    }
}