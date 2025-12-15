using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CicloLetivoLookupReturnViewModel : SMCViewModelBase, ISMCLookupViewModel
    {
        [SMCKey]
        public long? Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public string DescricaoRegimeLetivo { get; set; }
    }
}
