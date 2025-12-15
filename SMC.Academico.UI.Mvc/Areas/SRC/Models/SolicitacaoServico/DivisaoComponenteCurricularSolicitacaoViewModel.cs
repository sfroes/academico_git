using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DivisaoComponenteCurricularSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCUnique]
        [SMCSelect(nameof(ComponenteCurricularSolicitacaoViewModel.DivisoesComponentesSelect), AutoSelectSingleItem = true, NameDescriptionField = nameof(DescricaoDivisaoComponente))]
        [SMCSize(SMCSize.Grid22_24, SMCSize.Grid22_24, SMCSize.Grid22_24, SMCSize.Grid22_24)]
        public long SeqDivisaoComponente { get; set; }

        [SMCIgnoreProp]
        public string DescricaoDivisaoComponente { get; set; }
    }
}