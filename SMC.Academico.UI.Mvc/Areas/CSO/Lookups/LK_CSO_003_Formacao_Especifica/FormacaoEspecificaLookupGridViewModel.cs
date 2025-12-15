using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class FormacaoEspecificaLookupGridViewModel : SMCViewModelBase, ISMCLookupData, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        public List<FormacaoEspecificaLookupGridItemViewModel> Hierarquia { get; set; }
    }
}