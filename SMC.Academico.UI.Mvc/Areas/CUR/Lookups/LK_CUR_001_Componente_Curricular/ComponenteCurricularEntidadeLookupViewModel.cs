using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ComponenteCurricularEntidadeLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCDescription]       
        public string NomeEntidade { get; set; }
    }
}
