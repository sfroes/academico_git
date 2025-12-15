using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CampanhaLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        public long? Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        [SMCHidden]
        public long SeqEntidadeResponsavel { get; set; }

        public List<CampanhaCicloLetivoLookupViewModel> CiclosLetivos { get; set; }
    }
}